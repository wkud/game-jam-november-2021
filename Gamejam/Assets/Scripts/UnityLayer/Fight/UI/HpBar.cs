
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private Slider _slider;

    public void Initialize(Entity entity)
    {
        _slider = GetComponent<Slider>();
    
        entity.OnHpValueChanged.AddListener(UpdateHp);

        UpdateHp(entity.Stats.CurrentHp, entity.Stats.MaxHp); // set hp bar on initial value
    }

    public void UpdateHp(HpValueChangedEventArgs hpData) => UpdateHp(hpData.Current, hpData.Max);

    public void UpdateHp(int currentHp, int maxHp)
    {
        _slider.value = (currentHp * 1.0f) / maxHp;
    }
}
