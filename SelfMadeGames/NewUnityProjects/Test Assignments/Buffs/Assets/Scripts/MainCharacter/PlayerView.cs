using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour, ICanUseBonus
{
    private Character _playerCharacter;
    private IDisplayStats _hud;
    private CharacterController _characterController;

    private Camera _camera;
    public Transform FollowCamera {get; set;}

    public event Action PlayerObjectInteractionE;

    [Inject]
    private void Construct(Character character, IDisplayStats hud)
    {
        
        _playerCharacter = character;
        _hud = hud;

        _hud.UpdateStats(_playerCharacter.CurrentStats);
        _characterController = GetComponent<CharacterController>();

        _camera = Camera.main;
        FollowCamera = _camera.transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerObjectInteractionE != null)
                PlayerObjectInteractionE.Invoke();
            Debug.Log("E Key Pressed");
        }

        var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _characterController.Move(direction * _playerCharacter.CurrentStats.Speed / 5 * Time.deltaTime);
    }

    public void UseBonus(IBuff buff)
    {
        _playerCharacter.AddBuff(buff);

        _hud.UpdateStats(_playerCharacter.CurrentStats);

        Debug.Log($"Health: {_playerCharacter.CurrentStats.Health}, Damage: {_playerCharacter.CurrentStats.Damage}, Speed: {_playerCharacter.CurrentStats.Speed}");
    }
}
