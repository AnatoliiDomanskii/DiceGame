using System;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

namespace CodeBase.Infractrucure
{
    public class BattleService
    {
        public InputService _inputService;
        private GameFactory _gameFactory;
        private int _levelNum;

        private Unit _turnUnit;
        private Player _player;
        private Enemy _enemy;

        private List<Dice> _spawnedDices = new List<Dice>();

        public Action<Player, Enemy, int> OnBattleStarted;

        public Player Player
        {
            get
            {
                if (_player == null)
                {
                    _player = _gameFactory.CreatePlayer();
                }

                return _player;
            }
        }

        public Enemy Enemy { get { return _enemy; } }

        public BattleService(InputService inputService, GameFactory gameFactory)
        {
            _inputService = inputService;
            _gameFactory = gameFactory;
            _levelNum = 0;
        }

        public void StartBattle()
        {
            _levelNum++;

            Player.Armor = 0;

            Enemy enemy = _gameFactory.CreateEnemy(_levelNum);

            _enemy = enemy;

            OnBattleStarted?.Invoke(_player, enemy, _levelNum);

            _turnUnit = _player;

            StartRound();
        }

        public void RollDices()
        {
            foreach(Dice dice in _spawnedDices)
            {
                dice.Roll(ApplyDiceAbility);
            }

            _inputService.OnTouched -= RollDices; 
        }

        private void StartRound()
        {
            SpawnDices(_turnUnit.diceList);

            if (_turnUnit is Player)
            {
                _inputService.OnTouched += RollDices;
            }
            else
            {
                RollDices();
            }
        }

        private void SpawnDices(List<DiceData> dicesData)
        {
            foreach (DiceData dice in dicesData)
            {
                _spawnedDices.Add(_gameFactory.CreateDice(dice));
            }
        }

        private void ApplyDiceAbility(Dice dice, DiceEdge edge)
        {
            Unit unitForAttack = _turnUnit is Player ? _enemy : _player;

            Unit unitForHeal = _turnUnit is Player ? _player : _enemy;

            UnityEngine.Vector3 moveVector = UnityEngine.Vector3.zero;

            moveVector.z = _turnUnit is Player ? 7f : -7f;

            int coef = (edge.abilities[0].Type == Ability.AbilityType.Attack || edge.abilities[0].Type == Ability.AbilityType.Poison) ? 1 : -1;

            DOTween.Sequence()
                .Append(dice.transform.DOScale(1.1f, 0.3f))
                .Append(dice.transform.DOScale(1, 0.3f))
                .AppendInterval(0.7f)
                .Append(dice.transform.DOMove(moveVector * coef, 0.5f).SetEase(Ease.InQuad))
                .OnKill(() =>
                {
                    dice.ApplyAbility(edge, unitForAttack, unitForHeal);
                    WaitForApplingEnd(dice);
                });
        }

        private void WaitForApplingEnd(Dice dice)
        {
            _spawnedDices.Remove(dice);
            UnityEngine.Object.Destroy(dice.gameObject);

            if (_spawnedDices.Count() == 0) // Where(dice => dice.Applied == false).Count() == 0)
            {
                _turnUnit = _turnUnit is Player ? _enemy : _player;

                if (_enemy.IsLife && _player.IsLife)
                {
                    StartRound();
                }
                else if (!_enemy.IsLife)
                {
                    StartBattle();
                }
            }
        }
    }
}
