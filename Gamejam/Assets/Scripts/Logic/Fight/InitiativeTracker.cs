using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeTracker
{
    private List<Entity> _initiativeQueue;
    private Entity _currentEntity;

    public InitiativeTracker(Entity[] entityQueue)
    {
        this._initiativeQueue = new List<Entity>(entityQueue);
        this._initiativeQueue.Sort((e1, e2) => e2?.Stats.Initiative.CompareTo(e1?.Stats.Initiative) ?? -1);
        _currentEntity = this._initiativeQueue[0];
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
        int index = this._initiativeQueue.FindIndex(e => e == _currentEntity);
        int nextIndex = (index + 1) % this._initiativeQueue.Count;
        this._currentEntity = this._initiativeQueue[nextIndex];
    }

    public void RemoveFromQueue(Entity entity)
    {
        if (entity == this._currentEntity)
            this.SetNextEntity();

        this._initiativeQueue.Remove(entity);
    }
}
