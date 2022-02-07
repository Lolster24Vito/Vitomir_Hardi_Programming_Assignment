using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipableIndividual
{
    public ItemEquipable Item { get; set; }
    private float _currentDurability;

    public ItemEquipableIndividual(ItemEquipable item, float currentDurability)
    {
        Item = item;
        _currentDurability = currentDurability;
    }

    public float CurrentDurability {
        get => _currentDurability; 
        set {
             _currentDurability = Mathf.Clamp(value, 0, Item.MaxDurability);
        }
    }
    // Start is called before the first frame update
    
}
