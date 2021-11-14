using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MapController : MonoBehaviour
{
    public static MapController Instance = null;
    [SerializeField] int _stepsToBoss = 11;//-1 for starting node
    [SerializeField] MapNode[] _nodesPrefabs = new MapNode[0];//nodes/room prefabs //0-altar, 1-hiden altar, 2-enemy, 3-hidden enemy, 4-elite, 5-boss
    [SerializeField] EncounterData[] _normalEncounters;
    [SerializeField] EncounterData[] _eliteEncounters;
    [SerializeField] EncounterData[] _bossEncounters;
    [SerializeField] Transform[] _nodesSpawnPoints = new Transform[3];//used on start to create node tree, then used to move camera
    List<MapNode> _allNodes = new List<MapNode>();
    MapNode _currentlySelectedNode;
    [SerializeField] Transform _nodesContainer;//here i spawn nodes
    [SerializeField] Transform _playerPawn;//current position indicator
    float _yAxisSpawnShift = 2.5f;//space between 2 nodes in y axis
    
    public event UnityAction OnIntersectionsRemoved;

    public void Initialize()
    {
        MapController.Instance = this;
        SpawnNodes();
        UnlockNextRooms();
        _nodesSpawnPoints[1].transform.position += new Vector3(0, _yAxisSpawnShift , 0);
        _playerPawn.position = _currentlySelectedNode.transform.position;
    }
    void SpawnNodes()//trzeba dorobic potem losowanie typów nodeów
    {
        
        Vector3 spawnPos;
        //create first node
        MapNode nodeInstance = Instantiate(_nodesPrefabs[0]);
        nodeInstance.transform.parent = _nodesContainer;
        spawnPos = _nodesSpawnPoints[1].position;
        nodeInstance.transform.position = spawnPos;
        nodeInstance.depth = 0;
        nodeInstance.spawnPointId = 1;
        _currentlySelectedNode = nodeInstance;
        _allNodes.Add(nodeInstance);

        //create other nodes
        for (int i = 1; i < _stepsToBoss; i++)
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

                    int nodeRandomizer = Random.Range(0, 100);
                    if (nodeRandomizer < 25)//altar
                    {
                        nodeInstance = Instantiate(_nodesPrefabs[0]);
                    }
                    else if (nodeRandomizer >= 25 && nodeRandomizer<50)//encounter
                    {
                        nodeInstance = Instantiate(_nodesPrefabs[2]);
                        ((FightNode)nodeInstance).Enemies = _normalEncounters[Random.Range(0, _normalEncounters.Length)]._enemies;
                    }
                    else if (nodeRandomizer >= 50 && nodeRandomizer < 70)//hidenaltar
                    {
                        nodeInstance = Instantiate(_nodesPrefabs[1]);
                    }
                    else if (nodeRandomizer >= 70 && nodeRandomizer < 90)//hidden encounter
                    {
                        nodeInstance = Instantiate(_nodesPrefabs[3]);
                        ((FightNode)nodeInstance).Enemies = _normalEncounters[Random.Range(0, _normalEncounters.Length)]._enemies;
                    }
                    else if (nodeRandomizer >= 90 && nodeRandomizer < 100)//elite encounter
                    {
                        nodeInstance = Instantiate(_nodesPrefabs[4]);
                        ((FightNode)nodeInstance).Enemies = _normalEncounters[Random.Range(0, _eliteEncounters.Length)]._enemies;
                    }

                    nodeInstance.transform.parent = _nodesContainer;
                    if (i % 2 == 1)
                    {
                        spawnPos = new Vector3(_nodesSpawnPoints[2 - j].position.x, _nodesSpawnPoints[2 - j].position.y + i * _yAxisSpawnShift, _nodesSpawnPoints[2 - j].position.z);
                        nodeInstance.spawnPointId = 2-j;
                    }
                    else
                    { 
                        spawnPos = new Vector3(_nodesSpawnPoints[j].position.x, _nodesSpawnPoints[j].position.y + i*_yAxisSpawnShift, _nodesSpawnPoints[j].position.z);
                        nodeInstance.spawnPointId = j;
                    }
                    nodeInstance.transform.position = spawnPos;
                    nodeInstance.depth = i;                    
                    _allNodes.Add(nodeInstance);
                }
            }
        }        

        //create boss node
        nodeInstance = Instantiate(_nodesPrefabs[5]);
        ((FightNode)nodeInstance).Enemies = _normalEncounters[Random.Range(0, _bossEncounters.Length)]._enemies;
        nodeInstance.transform.parent = _nodesContainer;
        spawnPos = new Vector3(_nodesSpawnPoints[1].position.x, _nodesSpawnPoints[1].position.y + _stepsToBoss * _yAxisSpawnShift, _nodesSpawnPoints[1].position.z);
        nodeInstance.transform.position = spawnPos;
        nodeInstance.depth = _stepsToBoss;
        nodeInstance.spawnPointId = 1;
        _allNodes.Add(nodeInstance);

        //post creation stuff
        foreach (MapNode node in _allNodes)
        {
            node.Initialize(_allNodes);
        }
        RemoveIntersectionsInTree();
    }
    MapNode GetNodeAtPosition(int tmpDepth, int tmpSpwPtId)
    {
        MapNode desiredNode = _allNodes.Find(i => i.depth == tmpDepth && i.spawnPointId == tmpSpwPtId);
        if (desiredNode != null) return desiredNode;
        else return null;
    }
    void RemoveIntersectionsInTree()
    {        
        for (int i = 1; i < _stepsToBoss; i++)
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
        foreach (MapNode n in _currentlySelectedNode.childNodes)
        {
            n.canBeSelected = true;
        }
    }
    public void SelectRoom(MapNode room)
    {
        room.LockRoom();
        foreach (MapNode n in _currentlySelectedNode.childNodes)
        {
            if (n != room) n.LockRoom();
        }
        _currentlySelectedNode = room;
        _nodesSpawnPoints[1].transform.position += new Vector3(0, _yAxisSpawnShift, 0);
        _playerPawn.position = _currentlySelectedNode.transform.position;
        GameController.Instance.GameState.CurrentNode = room;

        UnlockNextRooms();//pamietac zeby nie dalo sie od razu kliknac w nastepny

        if (room is FightNode)
        {
            //GameController.Instance.GameState.CurrentNode = room;
            //GameController.Instance.OpenScene(SceneId.Fight);
            for (int i = 0; i < 4; i++)
            {
                GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, Random.Range(-4, 0), i);                
            }
        }
        else if (room is AltarNode)
        {
            AltarController.Instance.TurnChildrenOn();
        }
        
    }
}
