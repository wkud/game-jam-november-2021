using UnityEngine.Events;

public abstract class Entity
{
    protected EntityStats _stats;

    public Entity(EntityStats initialStats)
    {
        this._stats = initialStats.GetClone();
    }

    public EntityStats Stats => _stats;

    public UnityEvent<int, int> OnHpValueChanged { get; private set; } = new UnityEvent<int, int>();

    public void TakeDamage(int damage)
    {
        this._stats.CurrentHp -= damage;
        OnHpValueChanged.Invoke(_stats.CurrentHp, _stats.MaxHp);
    }
}
