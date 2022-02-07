using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemAttribute : System.IEquatable<ItemAttribute>
{
    private readonly string _name;
    public string Name { get => _name; }
    public int Value;

    public ItemAttribute(string name)
    {
        _name = name;
    }

    public bool Equals(ItemAttribute other)
    {
        return Name.Equals(other.Name);
    }
    public override string ToString()
    {
        return _name + ": " + Value;
    }
}