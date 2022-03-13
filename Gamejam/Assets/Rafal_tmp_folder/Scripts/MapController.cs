using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MapController : MonoBehaviour
{
    public static MapController Instance = null;
    [SerializeField] int _stepsToBoss = 11;//-1 for starting node
    [SerializeField] MapNode[] _nodesPrefabs = new MapNode[0];//nodes/room prefabs //0-altar, 1-hiden altar, 2-enemy////////////////////////////////, 3-hidden enemy, 4-elite, 5-boss
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
                    if (nodeRandomizer < 20)//altar
                    {
                        nodeInstance = Instantiate(_nodesPrefabs[0]);
                    }
                    else if (nodeRandomizer >= 20 && nodeRandomizer < 40)//hidden altar
                    {
                        nodeInstance = Instantiate(_nodesPrefabs[1]);
                    }
                    else//encounter
                    {
                        nodeInstance = Instantiate(_nodesPrefabs[2]);                        
                        int mapCompleteness = 100 * i / _stepsToBoss;
                        if (mapCompleteness > 50)
                        {
                            nodeRandomizer = Random.Range(0, 100);
                            if (nodeRandomizer < mapCompleteness * 3 / 5)//elite encounter
                            {
                                ((FightNode)nodeInstance).EncounterData = GetEncounterData(EncounterType.Elite, (float)mapCompleteness/100);//normal encounter
                            }
                            else//normal encounter
                            {
                                ((FightNode)nodeInstance).EncounterData = GetEncounterData(EncounterType.Normal, (float)mapCompleteness / 100);//normal encounter
                            }
                        }
                        else
                        {
                            ((FightNode)nodeInstance).EncounterData = GetEncounterData(EncounterType.Normal, (float)mapCompleteness / 100);//normal encounter
                        }
                        ((FightNode)nodeInstance).SetEncounterSprite(((FightNode)nodeInstance).EncounterData.EncounterType);
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
        nodeInstance = Instantiate(_nodesPrefabs[2]);
        ((FightNode)nodeInstance).EncounterData = GetEncounterData(EncounterType.Boss, 1);
        nodeInstance.transform.parent = _nodesContainer;
        spawnPos = new Vector3(_nodesSpawnPoints[1].position.x, _nodesSpawnPoints[1].position.y + _stepsToBoss * _yAxisSpawnShift, _nodesSpawnPoints[1].position.z);
        nodeInstance.transform.position = spawnPos;
        nodeInstance.depth = _stepsToBoss;
        nodeInstance.spawnPointId = 1;
        ((FightNode)nodeInstance).SetEncounterSprite(EncounterType.Boss);
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
            GameController.Instance.GameState.CurrentNode = room;
            GameController.Instance.OpenScene(SceneId.Fight);
            //for (int i = 0; i < 4; i++)
            //{
            //    GameController.Instance.ChangeCharacterStat(StatName.CurrentHp, Random.Range(-4, 0), i);                
            //}
        }
        else if (room is AltarNode)
        {
            AltarController.Instance.TurnChildrenOn();
        }
        
    }
    public EncounterData GetEncounterData(EncounterType encounterType, float mapProgressPercentage = 0)//randomly select 1 encounter from all similar encounters
    {
        if (encounterType == EncounterType.Normal)
        {
            if (mapProgressPercentage < 0.33f)
            {
                return GameController.Instance.Resources.NormalEasyEncounters[Random.Range(0, GameController.Instance.Resources.NormalEasyEncounters.Count)];

            }
            else if (mapProgressPercentage < 0.66f)
            {
                return GameController.Instance.Resources.NormalMediumEncounters[Random.Range(0, GameController.Instance.Resources.NormalMediumEncounters.Count)];
            }
            else
            {
                return GameController.Instance.Resources.NormalHardEncounters[Random.Range(0, GameController.Instance.Resources.NormalHardEncounters.Count)];
            }
        }
        else if (encounterType == EncounterType.Elite)
        {
            return GameController.Instance.Resources.EliteEncounters[Random.Range(0, GameController.Instance.Resources.EliteEncounters.Count)];
        }
        else if (encounterType == EncounterType.Boss)
        {
            return GameController.Instance.Resources.BossEncounters[Random.Range(0, GameController.Instance.Resources.BossEncounters.Count)];
        }
        else
        {
            Debug.LogError("TUTAJ COŒ POSZ£O NIE TAK I NULL PEWNIE BÊDZIE SYPA£ B£ÊDY");
            return null;
        }
    }
}
