public class Ability
{
    private AbilityType _type;
    private int _value;

    public AbilityType Type
    {
        get { return _type; }
    }

    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
        }
    }

    public enum AbilityType
    {
        Attack = 0,
        Heal = 1,
        Poison = 2,
        Defence = 3
    }

    public Ability(AbilityData abilityData)
    {
        _type = abilityData.type;
        _value = abilityData.value;
    }
}
