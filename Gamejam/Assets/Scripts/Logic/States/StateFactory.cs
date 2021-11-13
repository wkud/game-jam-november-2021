using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateFactory
{

  public static StateController CreateStateController(IState state, int turns)
  {
    StateController stateController = new StateController(state, turns);
    return stateController;
  }

}
