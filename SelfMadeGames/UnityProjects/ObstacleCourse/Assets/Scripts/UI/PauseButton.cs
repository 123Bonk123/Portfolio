using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PauseButton : MonoBehaviour, IPointerDownHandler
{
    //to Inject
    private IPauseUnpause _pauseController;

    [Inject]
    private void Construct(IPauseUnpause pauseController)
    {
        _pauseController = pauseController;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_pauseController.IsPaused)
            _pauseController.Pause();
    }


}
