using Zenject;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle();
    }
}