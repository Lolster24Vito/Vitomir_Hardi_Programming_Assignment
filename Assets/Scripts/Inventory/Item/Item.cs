using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Item",menuName ="Inventory/Item")]
public class Item :ScriptableObject,IEqualityComparer<Item>
{
    public int ID = 1;
    public string Name;
    public string Description;
    public Sprite Icon;
    public int MaxStack;
    public bool InfiniteStack;

    protected EnumEquipmentType _equipmentType=EnumEquipmentType.None;

    public virtual void Use()
    {

    }
    public new bool Equals(Item x, Item y)
    {
        return x.ID == y.ID;
    }

    public int GetHashCode(Item obj)
    {
        return obj.ID.GetHashCode();
    }

    public virtual EnumEquipmentType GetEquipmentType()
    {
        return _equipmentType;
    }
    public override string ToString()
    {
        char breakLine = '\n';
        string text = Name + breakLine+ Description+breakLine;
        return text;
    }

}
