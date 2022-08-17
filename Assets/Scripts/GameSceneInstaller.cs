using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameScreen gameScreen;
    [SerializeField] private StartGameScreen startGameScreen;
    [SerializeField] private PauseScreen pauseScreen;
    [SerializeField] private GameOverScreen gameOverScreen;
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
        Container.Bind<GameScreen>().FromInstance(gameScreen).AsSingle();
        Container.Bind<StartGameScreen>().FromInstance(startGameScreen).AsSingle();
        Container.Bind<PauseScreen>().FromInstance(pauseScreen).AsSingle();
        Container.Bind<GameOverScreen>().FromInstance(gameOverScreen).AsSingle();
    }
}