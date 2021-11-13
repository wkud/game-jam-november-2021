using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MapController : MonoBehaviour
{
    public static MapController Instance = null;
    [SerializeField] int stepsToBoss = 20;//-1 for starting node
    [SerializeField] MapNode[] nodesPrefabs = new MapNode[0];//nodes/room prefabs 
    [SerializeField] Transform[] nodesSpawnPoints = new Transform[3];//used on start to create node tree, then used to move camera
    List<MapNode> allNodes = new List<MapNode>();
    MapNode currentlySelectedNode;
    [SerializeField] Transform nodesContainer;//here i spawn nodes
    [SerializeField] Transform playerPawn;//current position indicator
    float yAxisSpawnShift = 2.5f;//space between 2 nodes in y axis

    public event UnityAction OnIntersectionsRemoved;

    public void Initialize()
    {
        MapController.Instance = this;
        SpawnNodes();
        UnlockNextRooms();
        nodesSpawnPoints[1].transform.position += new Vector3(0, yAxisSpawnShift , 0);
        playerPawn.position = currentlySelectedNode.transform.position;
    }
    void SpawnNodes()//trzeba dorobic potem losowanie typów nodeów
    {
        
        Vector3 spawnPos;
        //create first node
        MapNode nodeInstance = Instantiate(nodesPrefabs[0]);
        nodeInstance.transform.parent = nodesContainer;
        spawnPos = nodesSpawnPoints[1].position;
        nodeInstance.transform.position = spawnPos;
        nodeInstance.depth = 0;
        nodeInstance.spawnPointId = 1;
        currentlySelectedNode = nodeInstance;
        allNodes.Add(nodeInstance);

        //create other nodes
        for (int i = 1; i < stepsToBoss; i++)
        {
            int spawnChance = 75;//%
            for (int j = 0; j < 3; j++)
            {
                bool shouldSpawn = Random.Range(0, 100) < spawnChance ? true : false;
                if (!shouldSpawn)
                { 
                    spawnChance = 100;//to ensure both left and right side will have at least 1 tile to go 
                }
                else 
                {
                    spawnChance = 40;//%

                    nodeInstance = Instantiate(nodesPrefabs[0]);
                    nodeInstance.transform.parent = nodesContainer;
                    if (i % 2 == 1)
                    {
                        spawnPos = new Vector3(nodesSpawnPoints[2 - j].position.x, nodesSpawnPoints[2 - j].position.y + i * yAxisSpawnShift, nodesSpawnPoints[2 - j].position.z);
                        nodeInstance.spawnPointId = 2-j;
                    }
                    else
                    { 
                        spawnPos = new Vector3(nodesSpawnPoints[j].position.x, nodesSpawnPoints[j].position.y + i*yAxisSpawnShift, nodesSpawnPoints[j].position.z);
                        nodeInstance.spawnPointId = j;
                    }
                    nodeInstance.transform.position = spawnPos;
                    nodeInstance.depth = i;                    
                    allNodes.Add(nodeInstance);
                }
            }
        }        

        //create boss node
        nodeInstance = Instantiate(nodesPrefabs[0]);
        nodeInstance.transform.parent = nodesContainer;
        spawnPos = new Vector3(nodesSpawnPoints[1].position.x, nodesSpawnPoints[1].position.y + stepsToBoss * yAxisSpawnShift, nodesSpawnPoints[1].position.z);
        nodeInstance.transform.position = spawnPos;
        nodeInstance.depth = stepsToBoss;
        nodeInstance.spawnPointId = 1;
        allNodes.Add(nodeInstance);

        //post creation stuff
        foreach (MapNode node in allNodes)
        {
            node.Initialize(allNodes);
        }
        RemoveIntersectionsInTree();
    }
    MapNode GetNodeAtPosition(int tmpDepth, int tmpSpwPtId)
    {
        MapNode desiredNode = allNodes.Find(i => i.depth == tmpDepth && i.spawnPointId == tmpSpwPtId);
        if (desiredNode != null) return desiredNode;
        else return null;
    }
    void RemoveIntersectionsInTree()
    {        
        for (int i = 1; i < stepsToBoss; i++)
        {
            MapNode[] nodesToCheck = new MapNode[6];
            nodesToCheck[0] = GetNodeAtPosition(i, 0);
            nodesToCheck[1] = GetNodeAtPosition(i, 1);
            nodesToCheck[2] = GetNodeAtPosition(i, 2);
            nodesToCheck[3] = GetNodeAtPosition(i + 1, 0);
            nodesToCheck[4] = GetNodeAtPosition(i + 1, 1);
            nodesToCheck[5] = GetNodeAtPosition(i + 1, 2);

            if (nodesToCheck[1] != null && nodesToCheck[0] != null && nodesToCheck[3] != null && nodesToCheck[4] != null)
            {
                nodesToCheck[1].childNodes.Remove(nodesToCheck[3]);
            }
            else if (nodesToCheck[1] != null && nodesToCheck[2] != null && nodesToCheck[5] != null && nodesToCheck[4] != null)
            {
                nodesToCheck[1].childNodes.Remove(nodesToCheck[5]);
            }
        }
        OnIntersectionsRemoved?.Invoke();
    }
    void UnlockNextRooms()
    {
        foreach (MapNode n in currentlySelectedNode.childNodes)
        {
            n.canBeSelected = true;
        }
    }
    public void SelectRoom(MapNode room)
    {
        foreach (MapNode n in currentlySelectedNode.childNodes)
        {
            if (n != room) n.LockRoom();
        }
        currentlySelectedNode = room;
        nodesSpawnPoints[1].transform.position += new Vector3(0, yAxisSpawnShift, 0);
        playerPawn.position = currentlySelectedNode.transform.position;
        UnlockNextRooms();//to trzeba bedzie wywolywac po rozpatrzeniu wnetrza pokoi
    }
}
