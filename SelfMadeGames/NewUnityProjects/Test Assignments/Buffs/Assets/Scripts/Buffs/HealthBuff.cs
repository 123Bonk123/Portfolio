using UnityEngine;

public class HealthBuff: IBuff
{
    private int _healthBonus;

    public HealthBuff(int healthBonus)
    {
        _healthBonus = healthBonus;
    }

    public CharacterStats ApplyBuff(CharacterStats currentStats)
    {
        var newStats = currentStats;
        newStats.Health = Mathf.Max(newStats.Health + _healthBonus, 0);

        return newStats;
    }
}
