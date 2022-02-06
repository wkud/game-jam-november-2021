using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;

    [SerializeField] private MapController _mapController;
    private FightController _fightController;

    [SerializeField] public ResourceContainer ResourceContainer = new ResourceContainer();
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

        _mapController?.Initialize();

        TryInitializeMapController();

        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => TryInitializeFightController();
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => TryInitializeMapController();
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
        ally.AddStat(statType, value);
        OnStatChanged?.Invoke(unitId, ally);
    }

    public void ChangeCharacterSkill(int playerId, int skillSlotId, Skill skill)
    {
        GameController.Instance.GameState.Allies[playerId].SetSkill(skillSlotId, skill);
        OnSkillChanged?.Invoke(playerId, skillSlotId, skill.Data.Sprite);
    }
}
