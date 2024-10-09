using UnityEngine;
using UnityEngine.UI;

public class Portrait : MonoBehaviour
{
    private Image _sprite;

    private void Awake()
    {
        _sprite = GetComponent<Image>();
    }

    public void SetUnit(Unit unit)
    {
        _sprite.sprite = unit.portraitSprite;
    }
}
