using System.Collections.Generic;
using UnityEngine;

public class InitiativeController : MonoBehaviour
{
    private List<Entity> _initiativeQueue;
    private List<InitiativeAvatarController> _initiativeControllersQueue = new List<InitiativeAvatarController>();

    [SerializeField] private GameObject InitiativeAvatarPrefab;

    public void Initialize(InitiativeTracker initiativeTracker)
    {
        _initiativeQueue = initiativeTracker.GetInitiativeQueue();

        foreach (Entity entity in _initiativeQueue)
        {
            this.SpawnAvatar(entity);
        }
    }

    public void SpawnAvatar(Entity entity)
    {
        GameObject childObject = Instantiate(InitiativeAvatarPrefab) as GameObject;
        childObject.transform.parent = gameObject.transform;

        InitiativeAvatarController initiativeAvatarController = childObject.GetComponentInChildren<InitiativeAvatarController>();
        initiativeAvatarController.Initialize(entity);
        _initiativeControllersQueue.Add(initiativeAvatarController);
    }
}
