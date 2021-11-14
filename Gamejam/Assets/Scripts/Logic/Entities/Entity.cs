public abstract class Entity
{
    protected EntityStats _stats;
    EntityStats Stats => _stats;

    public void TakeDamage(int damage)
    {
        this._stats.CurrentHp -= damage;
    }
}
