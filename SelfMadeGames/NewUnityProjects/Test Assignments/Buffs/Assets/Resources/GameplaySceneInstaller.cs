using TMPro;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    
    Character _character;

    public override void InstallBindings()
    {
        var baseStats = new CharacterStats
        {
            Health = 50,
            Damage = 10,
            Speed = 10
        };

        

        Container.Bind<Character>().AsSingle().WithArguments(baseStats);

        Container.BindInterfacesAndSelfTo<HudView>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerView>().FromComponentInHierarchy().AsSingle().NonLazy();

        

        Container.Bind<BonusCapsuleView>().FromComponentInHierarchy().AsTransient();

    }
}