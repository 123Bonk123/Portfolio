using UnityEngine;

public class MovementHandler : IMovement
{
    // Components to inject
    private readonly CharacterController _characterController;


    private Vector3 _velocityDirection;
    private float _gravityForce;
    private float _startJumpVelocity;

    private MovementHandler(CharacterController characterController)
    {
        _characterController = characterController;
    }

    public void Move(Vector3 direction, float speed)
    {
        _velocityDirection.x = direction.x * speed;
        _velocityDirection.z = direction.z * speed;

        _characterController.Move(_velocityDirection * Time.deltaTime);
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            _velocityDirection.y = _startJumpVelocity;
        }
    }

    public void GravityHandling()
    {
        if(!_characterController.isGrounded)
        {
            _velocityDirection.y -= _gravityForce * Time.deltaTime;
        }
    }

    public void CalculateGravityParams(float maxJumpTime, float maxJumpHeight)
    {
        // these are equations from simple kinematics
        float maxHeightTime = maxJumpTime / 2;
        _gravityForce = 2 * maxJumpHeight / (maxHeightTime * maxHeightTime);
        _startJumpVelocity = 2 * maxJumpHeight / maxHeightTime;
    }
    
}
