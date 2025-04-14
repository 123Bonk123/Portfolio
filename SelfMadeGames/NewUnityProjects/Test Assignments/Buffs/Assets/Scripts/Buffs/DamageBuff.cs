using UnityEngine;

public class DamageBuff : IBuff
{
    private int _damageBonus;

    public DamageBuff(int damageBonus)
    {
        _damageBonus = damageBonus;
    }

    public CharacterStats ApplyBuff(CharacterStats currentStats)
    {
        var newStats = currentStats;
        newStats.Damage = Mathf.Max(newStats.Damage + _damageBonus, 0);

        return newStats;
    }
}
