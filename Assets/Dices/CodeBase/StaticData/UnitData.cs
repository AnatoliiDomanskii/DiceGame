using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "StaticData/Unit")]
public class UnitData : ScriptableObject
{
    public string portraitPath;
    public int maxHealth;
    public List<DiceData> dices;

}

[Serializable]
public class DiceData
{
    public List<EdgeData> Edges = new List<EdgeData>(6);
}

[Serializable]
public class EdgeData
{
    public List<AbilityData> abilities = new List<AbilityData>();
}

[Serializable]
public class AbilityData
{
    public Ability.AbilityType type;
    public int value;
}
