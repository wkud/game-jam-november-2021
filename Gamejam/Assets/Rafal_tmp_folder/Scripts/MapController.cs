using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] int stepsToBoss = 20;
    [SerializeField] MapNode[] nodesPrefabs = new MapNode[0];
    [SerializeField] Transform[] nodesSpawnPoints = new Transform[3];
    List<MapNode> allNodes = new List<MapNode>();
    MapNode currentlySelectedNode;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNodes();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnNodes()//trzeba dorobic potem losowanie typów nodeów
    {
        float yAxisSpawnShift = 2.5f;
        Vector3 spawnPos;
        //create first node
        MapNode nodeInstance = Instantiate(nodesPrefabs[0]);
        nodeInstance.transform.parent = transform;
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
                    nodeInstance.transform.parent = transform;
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
}
