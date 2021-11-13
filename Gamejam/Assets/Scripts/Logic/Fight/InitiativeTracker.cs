using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeTracker
{
  private List<IEntity> _initiativeQueue;
  private IEntity _currentEntity;

  public InitiativeTracker(IEntity[] entityQueue)
  {
    this._initiativeQueue = new List<IEntity>(entityQueue);
    _currentEntity = this._initiativeQueue[0];
  }

  public IEntity GetNextEntity()
  {
    int index = this._initiativeQueue.FindIndex(e => e == _currentEntity);
    int nextIndex = (index + 1) % this._initiativeQueue.Count;
    this._currentEntity = this._initiativeQueue[nextIndex];
    return this._currentEntity;
  }

  public void KillEntity(IEntity entity)
  {
    if (entity == this._currentEntity)
      this.GetNextEntity();

    this._initiativeQueue.Remove(entity);
  }
}
