public abstract class Entity
{
    protected EntityStats _stats;

    protected Entity(EntityStats stats)
    {
        _stats = stats;
    }

    public EntityStats Stats => _stats;

    public void TakeDamage(int damage)
    {
        this._stats.CurrentHp -= damage;
    }
}
