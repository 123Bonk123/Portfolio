using UnityEngine;
using ObstacleCourse.GlobalVars;
using Unity.Cinemachine;


public class PlayerInputController: IInput
{
    //to Inject
    private HudInput _input;
    
    
    //private bool _enabled;

    //Inject
    private PlayerInputController(HudInput input)
    {
        _input = input;
        //_enabled = true;
    }    


    // These methods can be written with the reference to DeviceType using Zenject, if needed 
    public Vector3 ReturnDirection()
    {
        Vector3 direction = _input.ReturnJoystickVector();
        if (!direction.Equals(Vector3.zero))
            return direction;

        return new Vector3(Input.GetAxis(GlobalVars.HORIZONTAL_AXIS), 0, Input.GetAxis(GlobalVars.VERTICAL_AXIS)); 
    }
    
    public bool ReturnJumpAction()
    {
        if(Input.GetAxis(GlobalVars.JUMP_BUTTON) > 0)
            return true;
        return _input.ReturnHudJumpButton();
    }

    public void DisableInput()
    {
        _input.OnInputDisable();
    
        _input.enabled = false;
    }

    public void EnableInput()
    {
        _input.enabled = true;
    }

    

}
