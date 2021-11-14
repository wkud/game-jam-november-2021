public interface IEnemyAi
{
    /// <summary>
    /// Enemy decides what to do in this method and the a specific skill is invoked. This method takes entities as parameters (enemy can decide what to do based on entities' state, such as hp)
    /// </summary>
    /// <param name="players"></param>
    /// <param name="monsters"></param>
    /// <param name="availableSkills">Collection of available spells that enemy can use</param>
    void MakeMove(Entity[] players, Entity[] monsters, Skill[] availableSkills);

}
