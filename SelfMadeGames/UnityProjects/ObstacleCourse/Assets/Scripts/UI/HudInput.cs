using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HudInput : HudJoystick
{
    [SerializeField] private JumpButton _jumpButton;

    private bool _isJumped;

    [Inject]
    private void Construct()
    {
        StartControllerConditions();
        _isJumped = false;
        // this jump handling (event) aims to make jump on JumpButtonPoinerDown (not on Click)
        _jumpButton.JumpButtonPointerDown.AddListener(OnJump);
    }    
 
    public Vector3 ReturnJoystickVector() => new Vector3(_inputVector.x, 0, _inputVector.y);
    
    private void OnJump() => _isJumped = true;

    public bool ReturnHudJumpButton()
    {
        if(_isJumped)
        {
            _isJumped = false;
            return true;
        }
        return false;
    }

    public void OnInputDisable()
    {
        StartControllerConditions();
    }
}
