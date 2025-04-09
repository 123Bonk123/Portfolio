using UnityEngine;
using DG.Tweening;
using System;

public class PlayerAnimation : IDisposable
{
    //to Inject
    private readonly Transform _playerBody;


    private Vector3 _direction;
    private Tween _movementAnimation;

    //Inject
    private PlayerAnimation(Transform playerBody)
    {
        
        _playerBody = playerBody;

    }

    public void MovementAnimation(Vector3 direction)
    {
        _movementAnimation.Kill();
        _direction = new Vector3(direction.z, 0, -direction.x);

        //animation of ball rotation as if it were rigidbody
        _movementAnimation = _playerBody.DORotate(_direction, Time.deltaTime, RotateMode.WorldAxisAdd)
        .SetEase(Ease.Linear).SetId("PlayerMovement").Play();
        
    }

    public void Dispose()
    {
        _movementAnimation.Kill();
    }
}
