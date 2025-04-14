using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDetector : MonoBehaviour, IPlayerDetector
{
    public event Action PlayerEnterTrigger;
    public event Action PlayerExitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            PlayerEnterTrigger.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            PlayerExitTrigger.Invoke();
    }

}
