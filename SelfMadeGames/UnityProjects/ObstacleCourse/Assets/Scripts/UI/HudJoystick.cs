using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public abstract class HudJoystick: MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // This class handles Hud Joystick actions

    // The following HudJoystick parameters can be injected using Zenject
    // to remove inheritance from the MonoBehaviour if needed, but this is not done for convenience.  
    [SerializeField] private Image _controllerArea;
    [SerializeField] private Image _controllerBackground;
    [SerializeField] private Image _controller;

    [SerializeField] private Color _inactiveControllerColor;
    [SerializeField] private Color _activeControllerColor;


    private Vector2 _controllerStartPosition;
    protected Vector2 _inputVector;
    private bool _controllerIsActive;
    
    protected void StartControllerConditions()
    {
        _controller.color = _inactiveControllerColor;
        _controllerIsActive = false;
        _controllerStartPosition = _controllerBackground.rectTransform.anchoredPosition;
        _inputVector = Vector2.zero;
        _controller.rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 controllerPosition;
        
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_controllerBackground.rectTransform, eventData.position, null, out controllerPosition))
        {
            // next block of code normalizes input vector in right way 

            controllerPosition.x = (controllerPosition.x * 2 / _controllerBackground.rectTransform.sizeDelta.x);
            controllerPosition.y = (controllerPosition.y * 2 / _controllerBackground.rectTransform.sizeDelta.x);

            _inputVector = new Vector2(controllerPosition.x, controllerPosition.y);

            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;

            _controller.rectTransform.anchoredPosition = 
                    new Vector2(_inputVector.x * (_controllerBackground.rectTransform.sizeDelta.x / 2), _inputVector.y * (_controllerBackground.rectTransform.sizeDelta.y / 2));
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickEffect();

        Vector2 controllerBackgroundPosition;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_controllerArea.rectTransform, eventData.position, null, out controllerBackgroundPosition))
            _controllerBackground.rectTransform.anchoredPosition = new Vector2(controllerBackgroundPosition.x, controllerBackgroundPosition.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _controllerBackground.rectTransform.anchoredPosition = _controllerStartPosition;

        ClickEffect();

        _inputVector = Vector2.zero;
        _controller.rectTransform.anchoredPosition = Vector2.zero;
    }

    private void ClickEffect()
    {
        if(!_controllerIsActive)
        {
            _controller.color = _activeControllerColor;
            _controllerIsActive = true;
        }
        else
        {
            _controller.color = _inactiveControllerColor;
            _controllerIsActive = false;
        }
    }
}
