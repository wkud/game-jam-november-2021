using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;
    [SerializeField] MapController mapController;


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
        mapController.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
