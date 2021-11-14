using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;

    private MapController _mapController;
    private FightController _fightController;

    [SerializeField] private ResourceContainer _resources = new ResourceContainer();

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
    }

    public void Initialize()
    {
        GameState = new GameState(_resources);

        _mapController = FindObjectOfType<MapController>();
        _mapController.Initialize();

        SceneManager.sceneLoaded += OnSceneLoaded;

        OpenScene(SceneId.Fight);
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

}
