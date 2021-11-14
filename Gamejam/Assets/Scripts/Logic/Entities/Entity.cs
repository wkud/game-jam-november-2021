public abstract class Entity
{
    protected EntityStats _stats;
    public EntityStats Stats => _stats;

    public void TakeDamage(int damage)
    {
        this._stats.CurrentHp -= damage;
    }
}
