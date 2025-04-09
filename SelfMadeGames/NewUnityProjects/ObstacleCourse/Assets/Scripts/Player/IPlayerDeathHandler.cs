using UnityEngine;
using UnityEngine.Events;

public interface IPlayerDeathHandler
{
    Transform PlayerTransform { get; }
    
    //UnityEvents are used to avoid unsubscribe actions on destroy.
    //if optimization is suddenly needed, they will be replaced with c# events
    [HideInInspector] UnityEvent PlayerDied { get; }
    [HideInInspector] UnityEvent PlayerRespawned { get; }

    void TriggerPlayerRespawned();
    
}
