using System.Collections.Generic;
using UnityEngine;

public class InitiativeController : MonoBehaviour
{
    private InitiativeTracker _initiativeTracker;
    private Dictionary<Entity, InitiativeAvatarController> _avatars = new Dictionary<Entity, InitiativeAvatarController>();

    [SerializeField] private GameObject InitiativeAvatarPrefab;

    public void Initialize(InitiativeTracker initiativeTracker)
    {
        _initiativeTracker = initiativeTracker;
        _initiativeTracker.OnCurrentEntityChange.AddListener(UpdateHighlightOnCurrentUnit);

        foreach (Entity entity in initiativeTracker.GetInitiativeQueue())
        {
            SpawnAvatar(entity);
        }

        _avatars[_initiativeTracker.GetCurrentEntity()].IsCurrentTurnMaker = true;
    }

    public void SpawnAvatar(Entity entity)
    {
        GameObject instance = Instantiate(InitiativeAvatarPrefab) as GameObject;
        instance.transform.parent = gameObject.transform;

        var initiativeAvatarController = instance.GetComponentInChildren<InitiativeAvatarController>();
        initiativeAvatarController.Initialize(entity);

        _avatars.Add(entity, initiativeAvatarController);
    }

    public void UpdateHighlightOnCurrentUnit(Entity previousEntity, Entity currentEntity)
    {
        _avatars[previousEntity].IsCurrentTurnMaker = false; // hide for previous
        _avatars[currentEntity].IsCurrentTurnMaker = true; // show for current
    }

}
