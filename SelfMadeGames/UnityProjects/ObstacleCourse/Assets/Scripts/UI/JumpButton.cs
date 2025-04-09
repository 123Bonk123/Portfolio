using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    
    [HideInInspector] public UnityEvent JumpButtonPointerDown;
    public void OnPointerDown(PointerEventData eventData)
    {
        JumpButtonPointerDown.Invoke();
    }
}

