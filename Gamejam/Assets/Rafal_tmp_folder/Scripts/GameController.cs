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
    public event UnityAction<int, Player> OnStatChanged;
    public GameState GameState { get; private set; }

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
        Initialize();
        TryInitializeFightController(); // for debug in fight scene
    }

    public void Initialize()
    {
        GameState = new GameState(_resources);

        _mapController = FindObjectOfType<MapController>();
        _mapController.Initialize();

        

        for (int i = 0; i < 4; i++)
        {
            OnStatChanged?.Invoke(i, GameState.Allies[i]);
        }

        SceneManager.sceneLoaded += (Scene scena, LoadSceneMode mode) => TryInitializeFightController();
    }

    public void OpenScene(SceneId scene)
    {
        _mapController.gameObject.SetActive(scene == SceneId.Map);

        SceneManager.LoadScene((int)scene);
    }

    private void TryInitializeFightController()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneId.Fight)
        {
            _fightController = FindObjectOfType<FightController>();
            _fightController.Initialize(this);

            _mapController.gameObject.SetActive(false);
        }
    }

    public void ChangeCharacterStat(StatName statType, int value, int unitId)
    {
        switch (statType)
        {
            /*case StatType.MaxHp:
                GameState.Allies[unitId].Stats.MaxHp += value;
                if (GameState.Allies[unitId].MaxHp < 0) GameState.Allies[unitId].MaxHp = 0;
                break;*/
            case StatName.Hp:
                GameState.Allies[unitId].Stats.CurrentHp += value;
                if (GameState.Allies[unitId].Stats.CurrentHp < 0) GameState.Allies[unitId].Stats.CurrentHp = 0;
                break;
            case StatName.Initiative:
                GameState.Allies[unitId].Stats.Initiative += value;
                if (GameState.Allies[unitId].Stats.Initiative < 0) GameState.Allies[unitId].Stats.Initiative = 0;
                break;
            case StatName.Defence:
                GameState.Allies[unitId].Stats.Defence += value;
                if (GameState.Allies[unitId].Stats.Defence < 0) GameState.Allies[unitId].Stats.Defence = 0;
                break;
            case StatName.CritChance:
                GameState.Allies[unitId].Stats.CritChance += value;
                if (GameState.Allies[unitId].Stats.CritChance < 0) GameState.Allies[unitId].Stats.CritChance = 0;
                break;
            case StatName.AttackModifier:
                GameState.Allies[unitId].Stats.AttackModifier += value;
                if (GameState.Allies[unitId].Stats.AttackModifier < 0) GameState.Allies[unitId].Stats.AttackModifier = 0;
                break;
            case StatName.Threat:
                GameState.Allies[unitId].Stats.Threat += value;
                if (GameState.Allies[unitId].Stats.Threat < 0) GameState.Allies[unitId].Stats.Threat = 0;
                break;
        }
        OnStatChanged?.Invoke(unitId, GameState.Allies[unitId]);
    }
}
