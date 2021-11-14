public abstract class Entity
{
    protected EntityStats _stats;

    public Entity(EntityStats initialStats)
    {
        this._stats = initialStats.GetClone();
    }

    public EntityStats Stats => _stats;

    public void TakeDamage(int damage)
    {
        this._stats.CurrentHp -= damage;
    }
}
