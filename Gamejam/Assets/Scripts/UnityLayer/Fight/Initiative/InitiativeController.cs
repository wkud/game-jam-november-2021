using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeController : MonoBehaviour
{
    private List<Entity> _initiativeQueue;
    private List<GameObject> _initiativeGameObjectsQueue = new List<GameObject>();
    private List<InitiativeAvatarController> _initiativeControllersQueue = new List<InitiativeAvatarController>();

    [SerializeField] private GameObject InitiativeAvatarPrefab;


    public void SpawnAvatar(Entity entity, bool isSelected)
    {
        GameObject childObject = Instantiate(InitiativeAvatarPrefab) as GameObject;
        childObject.transform.parent = gameObject.transform;

        InitiativeAvatarController initiativeAvatarController = childObject.GetComponentInChildren<InitiativeAvatarController>();
        initiativeAvatarController.Initialize(entity, isSelected);
        _initiativeControllersQueue.Add(initiativeAvatarController);
        _initiativeGameObjectsQueue.Add(childObject);
    }

    internal void Initialize(InitiativeTracker initiativeTracker)
    {
        _initiativeQueue = initiativeTracker.GetInitiativeQueue();
        Entity selectedEntity = initiativeTracker.GetStartEntity();

        foreach (Entity entity in _initiativeQueue)
        {
            this.SpawnAvatar(entity, selectedEntity == entity);
        }
    }

    internal void OnFinishedTurn(InitiativeTracker initiativeTracker)
    {
        foreach (GameObject gameObject in _initiativeGameObjectsQueue)
        {
            Destroy(gameObject);
        }

        _initiativeQueue = initiativeTracker.GetInitiativeQueue();
        Entity selectedEntity = initiativeTracker.GetStartEntity();
        foreach (Entity entity in _initiativeQueue)
        {
            this.SpawnAvatar(entity, selectedEntity == entity);
        }
    }
}
