using System.Collections.Generic;
using UnityEngine;

public class Character : IBuffable
{
    public CharacterStats BaseStats { get;}
    public CharacterStats CurrentStats {get; private set;}

    private readonly List<IBuff> _buffs;

    public Character(CharacterStats baseStats)
    {
        BaseStats = baseStats;
        CurrentStats = baseStats;

        _buffs = new List<IBuff>();
    }

    public void AddBuff(IBuff buff)
    {
        _buffs.Add(buff);

        ApplyBuffs();

        Debug.Log($"Buff {buff} added");
    }

    public void RemoveBuff(IBuff buff)
    {
        _buffs.Remove(buff);

        ApplyBuffs();

        Debug.Log($"Buff {buff} removed");
    }

    private void ApplyBuffs()
    {
        CurrentStats = BaseStats;

        foreach (var buff in _buffs)
        {
            CurrentStats = buff.ApplyBuff(CurrentStats);
        }
    }
}
