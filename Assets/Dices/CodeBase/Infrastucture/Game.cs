namespace CodeBase.Infractrucure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        private InputService _inputService;
        private SceneLoader _sceneLoader;
        private GameFactory _gameFactory;
        private BattleService _battleService;
        private UI _ui;
        private Player _player;
        private Enemy _enemy;

        public InputService InputService { get { return _inputService; } }
        public SceneLoader SceneLoader { get { return _sceneLoader; } }
        public GameFactory GameFactory { get { return _gameFactory; } }
        public BattleService BattleService {  get { return _battleService; } }
        public UI UI { get { return _ui; } }

        public Game()
        {
            StateMachine = new GameStateMachine(this);
        }

        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
        }

        public void SetSceneLoader(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void SetGameFactory(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void SetBattleService(BattleService battleService)
        {
            _battleService = battleService;
        }

        public void SetUI(UI ui)
        {
            _ui = ui;
        }

        public void Update()
        {
            StateMachine.Update();
        }
    }
}
