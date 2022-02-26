using UnityEngine;
using UnityEngine.Events;

public struct HpValueChangedEventArgs
{
    public Entity Sender { get; set; }
    public int Current { get; set; }
    public int Max { get; set; }
    public int DamageTaken { get; set; }

    public HpValueChangedEventArgs(Entity sender, int current, int max, int damageTaken)
    {
        Sender = sender;
        Current = current;
        Max = max;
        DamageTaken = damageTaken;
    }
}

public abstract class Entity
{
    protected EntityStats _stats;
    public EntityStats Stats => _stats;

    public UnityEvent OnDeath { get; private set; } = new UnityEvent();

    public UnityEvent<HpValueChangedEventArgs> OnHpValueChanged { get; private set; } = new UnityEvent<HpValueChangedEventArgs>();
    public bool IsAlive => _stats.CurrentHp > 0;

    public Entity(EntityStats initialStats)
    {
        _stats = initialStats.GetClone();
    }

    public void TakeDamage(int damage)
    {
        if (!IsAlive)
        {
            return;
        }

        _stats.CurrentHp -= damage;
        var args = new HpValueChangedEventArgs(this, _stats.CurrentHp, _stats.MaxHp, damage);
        OnHpValueChanged.Invoke(args);
        
        if (_stats.CurrentHp <= 0)
        {
            OnDeath.Invoke();
            Debug.Log(_stats.Identifier + " died");
        }
    }
}
