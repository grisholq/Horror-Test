using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerLook look;
    [SerializeField] private PlayerComponents playerComponents;
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private PlayerDragSystem.Settings dragSettings;
    [SerializeField] private PlayerHUD playerHUD;
    public override void InstallBindings()
    {
        Container.Bind<PlayerLook>().FromInstance(look).AsCached();
        Container.Bind<PlayerComponents>().FromInstance(playerComponents).AsCached();
        Container.Bind<PlayerSettings>().FromInstance(playerSettings).AsCached();
        Container.Bind<PlayerHUD>().FromInstance(playerHUD).AsCached();
        
        Container.Bind<PlayerDragSystem>().FromNew().AsCached();
        Container.Bind<PlayerDragSystem.Settings>().FromInstance(dragSettings).AsCached();
        Container.Bind<PlayerMover>().FromNew().AsCached();
        Container.Bind<PlayerInput>().FromNew().AsCached();
        Container.BindInterfacesAndSelfTo<PlayerLogicLoop>().FromNew().AsCached();
    }
}