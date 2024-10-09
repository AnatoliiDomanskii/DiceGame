using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public readonly Button screen;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private HealthBar _enemyHealthBar, _playerHealthBar;
    [SerializeField] private Portrait _enemyPortrate, _playerPortrate;
    [SerializeField] private TextMeshProUGUI levelNumber;
    
    private void Awake()
    {
        _canvas.worldCamera = Camera.main;
    }

    public void SetUI(Player player, Enemy enemy, int levelNum)
    {
        _playerHealthBar.SetUnit(player);
        _enemyHealthBar.SetUnit(enemy);

        _playerPortrate.SetUnit(player);
        _enemyPortrate.SetUnit(enemy);

        levelNumber.text = $"LEVEL {levelNum}";
    }
}
