using System.Collections.Generic;
using UnityEngine.Events;

public class InitiativeTracker
{
    public UnityEvent<Entity, Entity> OnCurrentEntityChange { get; private set; } = new UnityEvent<Entity, Entity>();
    public UnityEvent<Entity> OnEntityRemoved { get; private set; } = new UnityEvent<Entity>();

    private List<Entity> _initiativeQueue;
    private Entity _currentEntity;

    public InitiativeTracker(Entity[] entityQueue)
    {
        _initiativeQueue = new List<Entity>(entityQueue);
        _initiativeQueue.Sort((e1, e2) => e2?.Stats.Initiative.CompareTo(e1?.Stats.Initiative) ?? -1);
        _currentEntity = _initiativeQueue[0];
    }

    public Entity GetCurrentEntity()
    {
        return _currentEntity;
    }

    public List<Entity> GetInitiativeQueue()
    {
        return _initiativeQueue;
    }

    public void SetNextEntity()
    {
        var previousEntity = _currentEntity;

        int index = _initiativeQueue.FindIndex(e => e == _currentEntity);
        int nextIndex = (index + 1) % _initiativeQueue.Count;
        _currentEntity = _initiativeQueue[nextIndex];

        OnCurrentEntityChange.Invoke(previousEntity, _currentEntity);
    }

    public void RemoveFromQueue(Entity entity)
    {
        if (entity == _currentEntity)
        {
            SetNextEntity();
        }

        _initiativeQueue.Remove(entity);
        OnEntityRemoved.Invoke(entity);
    }
}
