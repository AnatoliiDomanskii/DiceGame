using System;
using System.Collections.Generic;

namespace CodeBase.Infractrucure
{
    public class GameStateMachine
    {
        public readonly Game Game;

        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _activeState;

        public GameStateMachine(Game game)
        {
            Game = game;

            _states = new Dictionary<Type, IGameState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
                [typeof(GameLoopState)] = new GameLoopState(this)

            };
        }

        public void Enter<TState>() where TState : IGameState
        {
            _activeState?.Exit();

            IGameState state = _states[typeof(TState)];
            _activeState = state;

            state.Enter();
        }

        public void Update()
        {
            _activeState.Update();
        }
    }
}
