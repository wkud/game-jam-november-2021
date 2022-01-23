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

    [SerializeField] public ResourceContainer ResourceContainer = new ResourceContainer(); // TODO consider dependency injection (Unit needs ResourceContainer.DeadCharacterPortrait)
    public event UnityAction<int, Player> OnStatChanged;
    public event UnityAction<int, int, Sprite> OnSkillChanged;
    public GameState GameState { get; private set; }

    private void Awake()
    {
        if (GameController.Instance == null)
        {
            GameController.Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        Initialize();
        TryInitializeFightController(); // for debug in fight scene
    }

    public void Initialize()
    {
        GameState = new GameState(ResourceContainer);

        _mapController = FindObjectOfType<MapController>();
        _mapController?.Initialize();

        TryInitializeMapController();

        SceneManager.sceneLoaded += (Scene scena, LoadSceneMode mode) => TryInitializeFightController();
        SceneManager.sceneLoaded += (Scene scena, LoadSceneMode mode) => TryInitializeMapController();
    }

    public void OpenScene(SceneId scene)
    {
        _mapController.gameObject.SetActive(scene == SceneId.Map);

        SceneManager.LoadScene((int)scene);
    }

    private void TryInitializeFightController()
    {
        if (SceneManagerExtension.GetCurrentScene() == SceneId.Fight)
        {
            _fightController = FindObjectOfType<FightController>();
            _fightController.Initialize(GameState);

            _mapController?.gameObject.SetActive(false);
        }
    }

    private void TryInitializeMapController()
    {
        if (SceneManagerExtension.GetCurrentScene() == SceneId.Map)
        {
            foreach (CharacterPanel panel in FindObjectsOfType<CharacterPanel>())
            {
                panel.Initialize();
            }
            for (int i = 0; i < GameState.Allies.Count; i++)
            {
                OnStatChanged?.Invoke(i, GameState.Allies[i]);
            }
        }
    }

    public void ChangeCharacterStat(StatName statType, int value, int unitId)
    {
        var ally = GameState.Allies[unitId];
        switch (statType)
        {
            case StatName.Hp:
                ally.Stats.MaxHp += value;
                if (ally.Stats.MaxHp < 0) ally.Stats.MaxHp = 0;
                break;
            case StatName.CurrentHp:
                ally.Stats.CurrentHp += value;
                if (ally.Stats.CurrentHp < 0) ally.Stats.CurrentHp = 0;
                break;
            case StatName.Initiative:
                ally.Stats.Initiative += value;
                if (ally.Stats.Initiative < 0) ally.Stats.Initiative = 0;
                break;
            case StatName.Defence:
                ally.Stats.Defence += value;
                if (ally.Stats.Defence < 0) ally.Stats.Defence = 0;
                break;
            case StatName.CritChance:
                ally.Stats.CritChance += value;
                if (ally.Stats.CritChance < 0) ally.Stats.CritChance = 0;
                break;
            case StatName.AttackModifier:
                ally.Stats.AttackModifier += value;
                if (ally.Stats.AttackModifier < 0) ally.Stats.AttackModifier = 0;
                break;
            case StatName.Threat:
                ally.Stats.Threat += value;
                if (ally.Stats.Threat < 0) ally.Stats.Threat = 0;
                break;
        }
        OnStatChanged?.Invoke(unitId, ally);
    }

    public void ChangeCharacterSkill(int playerId, int skillSlotId, Skill skill)
    {
        GameController.Instance.GameState.Allies[playerId].SetSkill(skillSlotId, skill);
        OnSkillChanged?.Invoke(playerId, skillSlotId, skill.Data.Sprite);
    }
}
