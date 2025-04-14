using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Zenject;

public class HudView : MonoBehaviour, IDisplayStats
{
    [SerializeField] private List<TMP_Text> _statsTextList;

    [Inject]
    private void Construct()
    {
        Debug.Log("Hud constructed");
    }

    public void UpdateStats(CharacterStats currentStats)
    {
        _statsTextList[0].text = "Health: " + currentStats.Health;
        _statsTextList[1].text = "Damage: " + currentStats.Damage;
        _statsTextList[2].text = "Speed: " + currentStats.Speed;
    }
}
