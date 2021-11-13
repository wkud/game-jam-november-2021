using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateFactory
{

  public static StateController CreateStateController(StateData stateData, int turns)
  {
    IState state = StateFactory.CreateState(stateData);
    StateController stateController = new StateController(state, turns);
    return stateController;
  }

  public static IState CreateState(StateData stateData)
  {
    switch (stateData.StateType)
    {
      case StateTypes.Poisoned:
        return new PoisonedState(stateData);
        break;

      default:
        throw new NotImplementedException("SkillType not implemented");
    }
  }

}
