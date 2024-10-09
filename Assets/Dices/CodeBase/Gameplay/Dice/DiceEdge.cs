using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceEdge : MonoBehaviour
{
    private static Dictionary<Ability.AbilityType, Material> _materialsForAbilities = new Dictionary<Ability.AbilityType, Material>();

    public readonly List<Ability> abilities = new List<Ability>();
    private int _value;

    private MeshRenderer _meshRenderer;
    private TextMeshPro _textMeshPro;

    public int Value
    {
        get { return _value; }
        set
        {
            _textMeshPro.text = value.ToString();
            _value = value;
        }
    }

    public List<Ability> Abilities
    {
        get { return Abilities; }
    }

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _textMeshPro = GetComponentInChildren<TextMeshPro>();

        if (_materialsForAbilities.Count == 0)
        {
            _materialsForAbilities.Add(Ability.AbilityType.Attack, Resources.Load<Material>("Materials/Edges/AttackEdgeMaterial"));
            _materialsForAbilities.Add(Ability.AbilityType.Heal, Resources.Load<Material>("Materials/Edges/HealingEdgeMaterial"));
            _materialsForAbilities.Add(Ability.AbilityType.Poison, Resources.Load<Material>("Materials/Edges/PoisonEdgeMaterial"));
            _materialsForAbilities.Add(Ability.AbilityType.Defence, Resources.Load<Material>("Materials/Edges/DefenceEdgeMaterial"));
        }
    }

    public void SetAbilityView(EdgeData edgeData)
    {
        foreach (AbilityData abilityData in edgeData.abilities)
        {
            abilities.Add(new Ability(abilityData));
        }

        Value = abilities[0].Value;

        _meshRenderer.material = _materialsForAbilities[abilities[0].Type];
    }
}
