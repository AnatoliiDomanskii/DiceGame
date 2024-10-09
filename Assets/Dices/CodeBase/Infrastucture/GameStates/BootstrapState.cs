namespace CodeBase.Infractrucure
{
    public class BootstrapState : IGameState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        public void Enter()
        {
            RegisterServices();
            _stateMachine.Game.SceneLoader.LoadSceneAsync(Initial, onLoaded: EnterLoadLevel);
        }

        private void RegisterServices()
        {
            InputService inputService = new InputService();
            _stateMachine.Game.SetInputService(inputService);
            _stateMachine.Game.SetSceneLoader(new SceneLoader());

            GameFactory gameFactory = new GameFactory();
            _stateMachine.Game.SetGameFactory(gameFactory);
            _stateMachine.Game.SetBattleService(new BattleService(inputService, gameFactory));
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Game.GameFactory.CreateObj("Prefabs/Arena");
            _stateMachine.Game.GameFactory.CreateObj("Prefabs/Light");
            _stateMachine.Game.SetUI(
                _stateMachine.Game.GameFactory.CreateUI("Prefabs/UI"));

            _stateMachine.Enter<GameLoopState>();
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }
    }
}
