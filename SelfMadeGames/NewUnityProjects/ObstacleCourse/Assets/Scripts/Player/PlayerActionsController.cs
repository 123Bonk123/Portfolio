using System;
using UnityEngine;
using ObstacleCourse.GlobalVars;
using Zenject;
using UnityEngine.Events;

public class PlayerActionsController : MonoBehaviour, IPlayerDeathHandler, IPlayerFinishHandler
{
    //to Inject
    private IInput _playerInput;
    private IMovement _movementHandler;
    private PlayerAnimation _playerAnimation;
    private IPauseUnpause _pauseController;
    //private Player _player;


    [Header("Player movement stats")]
    [SerializeField, Range(0, 15)] private float _moveSpeed = 8;
    [SerializeField] private float _maxJumpTime = 0.5f;
    [SerializeField] private float _maxJumpHeight = 0.7f;

    
    //UnityEvents are used to avoid unsubscribe actions on destroy.
    //if optimization is suddenly needed, they will be replaced with c# events
    [HideInInspector] public UnityEvent PlayerDied { get; } = new();
    [HideInInspector] public UnityEvent PlayerRespawned { get; } = new();
    [HideInInspector] public UnityEvent PlayerFinished { get; } = new();

    public Transform PlayerTransform => this.transform;

    private bool _isAlive;
    private bool _isFinished;


    [Inject]
    private void Construct(IInput playerInput, IMovement movementHandler, PlayerAnimation playerAnimation, IPauseUnpause pauseController)
    {
        //_player = player;
        _playerInput = playerInput;
        _movementHandler = movementHandler;
        _playerAnimation = playerAnimation;
        _pauseController = pauseController;

        _isAlive = true;
        _isFinished = false;

    }

    private void Start()
    {
        _movementHandler.CalculateGravityParams(_maxJumpTime, _maxJumpHeight);
           
    }

    private void Update()
    {
        if (_isAlive && !_isFinished)
        {
            _movementHandler.Move(_playerInput.ReturnDirection(), _moveSpeed);
            _playerAnimation.MovementAnimation(_playerInput.ReturnDirection());
        
            _movementHandler.GravityHandling();

            if (_playerInput.ReturnJumpAction())
                _movementHandler.Jump();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !_isFinished)
            if(!_pauseController.IsPaused)
                _pauseController.Pause();
            else
                _pauseController.Unpause();
    }

    private void TriggerPlayerDied()
    {
        PlayerDied.Invoke();

        _isAlive = false;
        _playerInput.DisableInput();
    }

    public void TriggerPlayerRespawned()
    {
        gameObject.transform.GetChild(0).transform.rotation = Quaternion.identity;

        PlayerRespawned.Invoke();

        _isAlive = true;
        _playerInput.EnableInput();
    }

    private void TriggerPlayerFinished()
    {
        _isFinished = true;
        _playerInput.DisableInput();
        PlayerFinished.Invoke();
        
    }

    //Death trigger is handled in this class to avoid adding code to every death object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone") && _isAlive)
        {
            TriggerPlayerDied();
        }

        if (other.CompareTag("Finish") && _isAlive && !_isFinished)
        {
            TriggerPlayerFinished();
        }
    }

    // to avoid DOTween errors
    private void OnDestroy()
    {
        _playerAnimation.Dispose();
    }
}
