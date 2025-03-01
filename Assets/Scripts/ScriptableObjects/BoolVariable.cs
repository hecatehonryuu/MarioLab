using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "ScriptableObjects/BoolVariable", order = 2)]
public class BoolVariable : Variable<bool>
{

    public override void SetValue(bool value)
    {
        _value = value;
    }

    // overload
    public void SetValue(BoolVariable value)
    {
        SetValue(value.Value);
    }

    public void Toggle()
    {
        this.Value = !this.Value;
    }

    public void And(bool value)
    {
        this.Value = this.Value && value;
    }

    public void And(BoolVariable value)
    {
        And(value.Value);
    }

    public void Or(bool value)
    {
        this.Value = this.Value || value;
    }

    public void Or(BoolVariable value)
    {
        Or(value.Value);
    }
}