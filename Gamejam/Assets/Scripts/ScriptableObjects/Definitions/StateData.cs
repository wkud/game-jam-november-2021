using UnityEngine;

[CreateAssetMenu(fileName = "StateData", menuName = "ScriptableObjects/StateData", order = 3)]
public class StateData : ScriptableObject
{
  [SerializeField] string _description;
  [SerializeField] string _name;
  [SerializeField] StateTypes _stateType;
  [SerializeField] int _power;

  public string Name { get => _name; set => _name = value; }
  public string Description { get => _description; set => _description = value; }
  public int Power { get => _power; set => _power = value; }
  public StateTypes StateType { get => _stateType; }

}
