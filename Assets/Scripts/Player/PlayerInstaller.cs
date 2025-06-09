using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerLookDirection lookDirection;
    [SerializeField] private PlayerComponents playerComponents;
    [SerializeField] private PlayerSettings playerSettings;
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerLookDirection>().FromInstance(lookDirection).AsCached();
        Container.Bind<PlayerComponents>().FromInstance(playerComponents).AsCached();
        Container.Bind<PlayerSettings>().FromInstance(playerSettings).AsCached();
        Container.Bind<PlayerMover>().FromNew().AsCached();
        Container.Bind<PlayerInput>().FromNew().AsCached();
        Container.BindInterfacesAndSelfTo<PlayerLogicLoop>().FromNew().AsCached();
    }
}