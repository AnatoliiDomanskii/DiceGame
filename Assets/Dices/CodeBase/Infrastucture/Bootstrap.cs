using UnityEngine;


namespace CodeBase.Infractrucure
{
    public class Bootstrap : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();

            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            _game.Update();
        }
    }
}
