using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;
    [SerializeField] private MapController mapController;

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
    // Start is called before the first frame update
    void Start()
    {
        GameState = new GameState(_resources);
        mapController.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
