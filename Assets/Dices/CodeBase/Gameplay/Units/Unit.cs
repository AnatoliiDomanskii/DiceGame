using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Unit
{
    public Sprite portraitSprite;

    private bool _isLife;

    private int _health;
    private int _maxHealth;
    private int _armor;

    public List<DiceData> diceList = new List<DiceData>();
    public List<Ability> effects = new List<Ability>();

    public Action OnKilled;
    public Action OnHealthChenged;

    public Unit(UnitData data) //Sprite portraitSprite, int maxHealth, List<Dice> diceList
    {
        portraitSprite = Resources.Load<Sprite>(data.portraitPath);
        _isLife = true;
        _maxHealth = data.maxHealth;
        Health = data.maxHealth;
        diceList = data.dices;
    }

    public bool IsLife
    {
        get { return _isLife; }
    }

    public int Health 
    { 
        get 
        { 
            return _health; 
        }

        private set
        {
            if (value <= 0)
            {
                _health = 0;
                _isLife = false;
                OnKilled?.Invoke();
            }
            else if (value > _maxHealth)
            {
                _health = _maxHealth;
            }
            else
            {
                _health = value;
            }

            OnHealthChenged?.Invoke();
        }
    }
    public int MaxHealth { get { return _maxHealth; } }

    public int Armor 
    { 
        get { return _armor; } 
        set 
        {
            if ( value < 0)
            {
                _armor = 0;
            }
            else
            {
                _armor = value;
            }

            OnHealthChenged?.Invoke();
        } 
    }

    public void GetDamage(int damage)
    {
        if (damage < 0)
        {
            damage = 0;
        }

        if (damage > Armor)
        {
            Armor = 0;

            damage = damage - Armor;

            Health -= damage;
        }
        else
        {
            Armor -= damage;
        }
    }

    public void GetHealing(int healing)
    {
        if (healing < 0)
        {
            healing = 0;
        }

        Health += healing;
    }

    public void GetArmor(int armor)
    {
        if (armor < 0)
        {
            armor = 0;
        }

        Armor += armor;
    }
}
