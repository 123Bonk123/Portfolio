using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GamePlayLevel1Installer : MonoInstaller
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Transform[] _disappearingPlatformsContainers;
    [SerializeField] private Canvas _hudCanvas;

    List<Transform> _respawnPointList;
    List<DisappearingPlatform> disappearingPlatformsList;

    public override void InstallBindings()
    {
        BindInputAndUI();

        BindMovementServices(_characterController);
        
        BindRespawnPoints();

        BindDisappearingPlatforms();

        BindPauseHandling();

        BindPlayerServices(_playerBody);

        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();

        BindFinishHandling();
    }

    private void BindInputAndUI()
    {
        Container.Bind<HudInput>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle();
    }

    private void BindMovementServices(CharacterController characterController)
    {
        Container.BindInterfacesAndSelfTo<MovementHandler>().AsSingle().WithArguments(characterController);
    }

    private void BindPlayerServices(Transform playerBody)
    {
        Container.Bind<PlayerAnimation>().AsSingle().WithArguments(playerBody);

        Container.BindInterfacesAndSelfTo<PlayerActionsController>().FromComponentInHierarchy().AsSingle();
    }

    private void BindRespawnPoints()
    {
        _respawnPointList = GameObject.Find("RespawnPoints").transform.Cast<Transform>().ToList();
        Container.Bind<List<Transform>>().FromInstance(_respawnPointList).AsSingle();
    }

    private void BindDisappearingPlatforms()
    {
        disappearingPlatformsList = new List<DisappearingPlatform>();
        
        foreach (var container in _disappearingPlatformsContainers)
        {
            disappearingPlatformsList.AddRange(container.GetComponentsInChildren<DisappearingPlatform>());
        }

        Container.Bind<List<DisappearingPlatform>>().FromInstance(disappearingPlatformsList).AsSingle();
    }

    private void BindPauseHandling()
    {
        Container.Bind<PauseMenu>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesAndSelfTo<PauseController>().AsSingle();
    }

    private void BindFinishHandling()
    {
        Container.Bind<FinishCanvas>().FromComponentInHierarchy().AsSingle();

        Container.Bind<FinishController>().AsSingle().WithArguments(_hudCanvas).NonLazy();
    }
}