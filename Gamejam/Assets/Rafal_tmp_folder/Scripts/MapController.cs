using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapController : MonoBehaviour
{
    public static MapController Instance = null;
    [SerializeField] int stepsToBoss = 20;
    [SerializeField] MapNode[] nodesPrefabs = new MapNode[0];
    [SerializeField] Transform[] nodesSpawnPoints = new Transform[3];//used on start to create node tree, then used to move camera
    List<MapNode> allNodes = new List<MapNode>();
    MapNode currentlySelectedNode;
    [SerializeField] Transform nodesContainer;
    [SerializeField] Transform playerMapPos;
    float yAxisSpawnShift = 2.5f;


    public void Initialize()
    {
        MapController.Instance = this;
        SpawnNodes();
        UnlockNextRooms();
        nodesSpawnPoints[1].transform.position += new Vector3(0, yAxisSpawnShift , 0);
        playerMapPos.position = currentlySelectedNode.transform.position;
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
            int spawnChance = 50;//%
            for (int j = 0; j < 3; j++)
            {
                bool shouldSpawn = Random.Range(0, 100) < spawnChance ? true : false;
                if (!shouldSpawn)
                { 
                    spawnChance = 100;//to ensure both left and right side will have at least 1 tile to go 
                }
                else 
                {
                    spawnChance = 50;//%

                    nodeInstance = Instantiate(nodesPrefabs[0]);
                    nodeInstance.transform.parent = nodesContainer;
                    spawnPos = new Vector3(nodesSpawnPoints[j].position.x, nodesSpawnPoints[j].position.y + i*yAxisSpawnShift, nodesSpawnPoints[j].position.z);                    
                    nodeInstance.transform.position = spawnPos;
                    nodeInstance.depth = i;
                    nodeInstance.spawnPointId = j;
                    allNodes.Add(nodeInstance);
                }
            }
        }
        foreach (MapNode node in allNodes)
        {
            node.Initialize(allNodes);
        }
        //create boss node
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
        playerMapPos.position = currentlySelectedNode.transform.position;
        UnlockNextRooms();//to trzeba bedzie wywolywac po rozpatrzeniu pokoi
    }
}
