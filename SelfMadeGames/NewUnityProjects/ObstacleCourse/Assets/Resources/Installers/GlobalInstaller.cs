using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private Canvas _loadingSqreenPrefab;
     
    public override void InstallBindings()
    {

        Canvas loadingSqreeenPrefab = _loadingSqreenPrefab;

        Container.Bind<LoadingSqreen>().FromComponentInNewPrefab(loadingSqreeenPrefab)
            .AsSingle().NonLazy();

        Container.Bind<CoroutinePerformer>().FromNewComponentOnNewGameObject()
            .WithGameObjectName("CoroutinePerformer").AsSingle().NonLazy();
        
        Container.Bind<SceneLoader>().AsSingle().NonLazy();

        Container.Bind<SceneController>().AsSingle().NonLazy();
    }
}