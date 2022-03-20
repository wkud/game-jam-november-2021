using System;
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
        if (damage < 0)
        {
            Debug.LogError("Invalid operation: dealing negative damage. Use Entity.Heal method instead");
        }

        var damageAfterDefence = Math.Max(damage - _stats.Defence, 0);
        ModifyHealth(-damageAfterDefence);
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError("Invalid operation: healing negative amount. Use Entity.TakeDamage method instead");
        }

        ModifyHealth(amount);
    }

    private void ModifyHealth(int amount)
    {
        if (!IsAlive)
        {
            return;
        }

        _stats.CurrentHp += amount;

        var args = new HpValueChangedEventArgs(this, _stats.CurrentHp, _stats.MaxHp, amount);
        OnHpValueChanged.Invoke(args);

        if (_stats.CurrentHp <= 0)
        {
            OnDeath.Invoke();
            Debug.Log(_stats.Identifier + " died");
        }
    }
}
