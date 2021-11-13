using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public static GameController Instance = null;

    private MapController _mapController;
    private FightController _fightController;

    [SerializeField] private ResourceContainer _resources = new ResourceContainer();
    
    public event UnityAction<int,Player> OnStatChanged;

    public GameState GameState { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeCharacterStat(StatType.Hp, -1, 0);
        }
    }
    private void Awake()
    {
        if (GameController.Instance == null)
        {
            GameController.Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }

    void Start()
    {
        GameState = new GameState(_resources);

        _mapController = FindObjectOfType<MapController>();
        _mapController.Initialize();
        for (int i = 0; i < 4; i++)
        {
            OnStatChanged?.Invoke(i, GameState.Allies[i]);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        //OpenScene(SceneId.Fight);
    }

    public void OpenScene(SceneId scene) // map or altar
    {
        _mapController.gameObject.SetActive(scene == SceneId.Map);

        SceneManager.LoadScene((int)scene);
    }

    private void  OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == (int)SceneId.Fight)
        {
            _fightController = FindObjectOfType<FightController>();
            _fightController.Initialize(this);
        }
    }

    public void ChangeCharacterStat(StatType statType, int value, int unitId)
    {
        switch (statType)
        {
            case StatType.MaxHp:
                GameState.Allies[unitId].MaxHp += value;
                if (GameState.Allies[unitId].MaxHp < 0) GameState.Allies[unitId].MaxHp = 0;
                break;
            case StatType.Hp:
                GameState.Allies[unitId].Hp += value;
                if (GameState.Allies[unitId].Hp < 0) GameState.Allies[unitId].Hp = 0;
                break;
            case StatType.Initiative:
                GameState.Allies[unitId].Initiative += value;
                if (GameState.Allies[unitId].Initiative < 0) GameState.Allies[unitId].Initiative = 0;
                break;
            case StatType.Defence:
                GameState.Allies[unitId].Defence += value;
                if (GameState.Allies[unitId].Defence < 0) GameState.Allies[unitId].Defence = 0;
                break;
            case StatType.CritChance:
                GameState.Allies[unitId].CritChance += value;
                if (GameState.Allies[unitId].CritChance < 0) GameState.Allies[unitId].CritChance = 0;
                break;
            case StatType.AttackModifier:
                GameState.Allies[unitId].AttackModifier += value;
                if (GameState.Allies[unitId].AttackModifier < 0) GameState.Allies[unitId].AttackModifier = 0;
                break;
            case StatType.Threat:
                GameState.Allies[unitId].Threat += value;
                if (GameState.Allies[unitId].Threat < 0) GameState.Allies[unitId].Threat = 0;
                break;
        }
        OnStatChanged?.Invoke(unitId, GameState.Allies[unitId]);
    }
}
