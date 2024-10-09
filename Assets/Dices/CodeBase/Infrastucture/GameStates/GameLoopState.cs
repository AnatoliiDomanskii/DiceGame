
using UnityEngine;

namespace CodeBase.Infractrucure
{
    public class GameLoopState : IGameState
    {
        private GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.Game.BattleService.OnBattleStarted += _gameStateMachine.Game.UI.SetUI;
            _gameStateMachine.Game.BattleService.StartBattle();
        }
        public void Update()
        {
            _gameStateMachine.Game.InputService.IsTouching();
        }

        public void Exit() { }
    }
}

