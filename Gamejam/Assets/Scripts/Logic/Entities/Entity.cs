using UnityEngine.Events;

public abstract class Entity
{
    protected EntityStats _stats;
    public EntityStats Stats => _stats;

    public UnityEvent OnDeath { get; private set; } = new UnityEvent();

    public UnityEvent<int, int> OnHpValueChanged { get; private set; } = new UnityEvent<int, int>();

    public Entity(EntityStats initialStats)
    {
        _stats = initialStats.GetClone();
    }

    public void TakeDamage(int damage)
    {
        _stats.CurrentHp -= damage;
        OnHpValueChanged.Invoke(_stats.CurrentHp, _stats.MaxHp);
        
        if (_stats.CurrentHp <= 0)
        {
            OnDeath.Invoke();
        }
    }
}
