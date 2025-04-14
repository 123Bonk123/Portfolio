using System;
using UnityEngine;

public interface IPlayerDetector
{
    [HideInInspector] event Action PlayerEnterTrigger;

    [HideInInspector] event Action PlayerExitTrigger;
}
