using UnityEngine;
using UnityEngine.Events;

public interface IPlayerFinishHandler
{
    //UnityEvents are used to avoid unsubscribe actions on destroy.
    //if optimization is suddenly needed, they will be replaced with c# events
    [HideInInspector] UnityEvent PlayerFinished { get; }
}
