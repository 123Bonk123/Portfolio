using UnityEngine;

public class SpeedBuff: IBuff
{
    private int _speedBonus;

    public SpeedBuff(int speedBonus)
    {
        _speedBonus = speedBonus;
    }

    public CharacterStats ApplyBuff(CharacterStats currentStats)
    {
        var newStats = currentStats;
        newStats.Speed = Mathf.Max(newStats.Speed + _speedBonus, 0);

        return newStats;
    }
}
