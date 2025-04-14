using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class BonusCapsuleView : MonoBehaviour, IPointerClickHandler
{
    private IPlayerDetector _triggerCollider;
    private ICanUseBonus _player;
    private Canvas _bonusCanvas;

    private bool _isBonusInteractable;
    private bool _isBonusWindowOpened;

    private List<IBuff> _buffsContainer;

    [Inject]
    private void Construct(ICanUseBonus player)
    {
        _player = player;

        _triggerCollider = gameObject.GetComponentInParent<PlayerDetector>();

        _triggerCollider.PlayerEnterTrigger += MakeBonusInteractable;
        _triggerCollider.PlayerExitTrigger += MakeBonusNonInteractable;

        _bonusCanvas = gameObject.GetComponent<Canvas>();
        _bonusCanvas.gameObject.SetActive(false);
        
        _isBonusInteractable = false;
        _isBonusWindowOpened = false;

        _buffsContainer = new List<IBuff>
        {
            new HealthBuff(50),
            new DamageBuff(10),
            new SpeedBuff(10)
        };
        
    }

    private void LateUpdate()
    {
        Vector3 cameraSight = new Vector3(_player.FollowCamera.transform.position.x, _player.FollowCamera.transform.position.y, _player.FollowCamera.transform.position.z);
        _bonusCanvas.transform.LookAt(cameraSight);
        _bonusCanvas.transform.Rotate(0, 180, 0);
    }

    private void MakeBonusInteractable()
    {
        _isBonusInteractable = true;
        _player.PlayerObjectInteractionE += BonusWindowOpenClose;
    }

    private void MakeBonusNonInteractable()
    {
        _isBonusWindowOpened = false;
        _isBonusInteractable = false;

        _player.PlayerObjectInteractionE -= BonusWindowOpenClose;

        _bonusCanvas.gameObject.SetActive(false);
    }

    private void BonusWindowOpenClose()
    {
        if(_isBonusInteractable)
            if(!_isBonusWindowOpened)
            {
                _isBonusWindowOpened = true;
                _bonusCanvas.gameObject.SetActive(true);
            }
            else
            {
                _isBonusWindowOpened = false;
                _bonusCanvas.gameObject.SetActive(false);
            }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            IBuff selectedBuff = null;
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

            switch (clickedObject.name)
            {
                case "Buff1":
                    selectedBuff = _buffsContainer[0];
                    break;
                case "Buff2":
                    selectedBuff = _buffsContainer[1];
                    break;
                case "Buff3":
                    selectedBuff = _buffsContainer[2];
                    break;
                default:
                    Debug.Log("Unknown image clicked. Name: " + clickedObject.name);
                    break;
            }

            if(selectedBuff != null)
                _player.UseBonus(selectedBuff);
        }
    }

    void OnDestroy()
    {
        _triggerCollider.PlayerEnterTrigger -= MakeBonusInteractable;
        _triggerCollider.PlayerExitTrigger -= MakeBonusNonInteractable;

        _player.PlayerObjectInteractionE -= BonusWindowOpenClose;
    }

}
