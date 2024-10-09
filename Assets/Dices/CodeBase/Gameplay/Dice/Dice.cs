using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Dice : MonoBehaviour
{
    public bool Applied = false;

    private Rigidbody _rigidbody;
    private DiceEdge[] _diceEdges;

    public enum EdgeType
    {
        Attack = 0,
        Heal = 1,
        Poison = 2,
        Defence = 3
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _diceEdges = GetComponentsInChildren<DiceEdge>();
    }

    public void Roll(Action<Dice, DiceEdge> OnRollingEnded = null)
    {
        _rigidbody.AddForce(Vector3.up * 7f, ForceMode.Impulse);
        _rigidbody.AddTorque(new Vector3(5f, UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5)), ForceMode.Impulse);

        StartCoroutine(WaitForStopRolling(OnRollingEnded));
    }

    private IEnumerator WaitForStopRolling(Action<Dice, DiceEdge> OnRollingEnded = null)
    {
        while (!_rigidbody.IsSleeping())
        {
            yield return new WaitForEndOfFrame();
        }

        OnRollingEnded?.Invoke(this, GetDroppedEdge());
    }

    private DiceEdge GetDroppedEdge()
    {
        return _diceEdges
                .OrderByDescending(obj => obj.transform.position.y)
                .FirstOrDefault();
    }

    public void SetEdges(List<EdgeData> edges)
    {
        for(int i = 0; i < _diceEdges.Length; i++)
        {
            _diceEdges[i].SetAbilityView(edges[i]);
        }
    }

    public void ApplyAbility(DiceEdge edge, Unit unitForAttack, Unit unitForHeal)
    {
        foreach(Ability ability in edge.abilities)
        {
            switch (ability.Type)
            {
                case Ability.AbilityType.Attack:
                    unitForAttack.GetDamage(ability.Value);
                    break;
                case Ability.AbilityType.Poison:
                    unitForAttack.GetDamage(ability.Value);
                    break;
                case Ability.AbilityType.Heal:
                    unitForHeal.GetHealing(ability.Value);
                    break;
                case Ability.AbilityType.Defence:
                    unitForHeal.GetArmor(ability.Value);
                    break;
            }
        }

        Applied = true;
    }
}
