using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Infractrucure
{
    public class GameFactory
    {
        private Player _player;

        public GameObject CreateObj(string path)
        {
            GameObject obj = Resources.Load<GameObject>(path);
            return Object.Instantiate(obj);
        }

        public UI CreateUI(string path)
        {
            return CreateObj(path).GetComponent<UI>();
        }

        public Player CreatePlayer()
        {
            if (_player == null )
            {
                string path = "StaticData/PlayerData";

                UnitData unitData = Resources.Load<UnitData>(path);

                _player = new Player(unitData);

                return _player;
            }
            else
            {
                return _player;
            }
            
        }

        public Enemy CreateEnemy(int levelNum)
        {
            levelNum = (levelNum % 3 == 0) ? 3 : levelNum % 3;

            string path = $"StaticData/EnemyData_{levelNum}";

            UnitData unitData = Resources.Load<UnitData>(path);

            return new Enemy(unitData);
        }

        public Dice CreateDice(DiceData diceData)
        {
            string path = "Prefabs/DiceView";

            Dice dicePrefab = Resources.Load<Dice>(path);

            Dice diceView = Object.Instantiate(dicePrefab, new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f)), Quaternion.identity);

            diceView.SetEdges(diceData.Edges);

            return diceView;
        }
    }
}