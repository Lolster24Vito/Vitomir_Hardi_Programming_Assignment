using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpendableAttribute:ItemAttribute
{

   private int _maxValue;
    public int MaxValue { get => _maxValue; }

    public SpendableAttribute(string name, int maxValue) : base(name)
    {
        _maxValue = maxValue;
        Value = maxValue;
    }
    public override string ToString()
    {
        return Name + ": " + Value + "/" + MaxValue;
    }
}
