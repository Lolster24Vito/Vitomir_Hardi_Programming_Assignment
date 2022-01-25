using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemConsumable", menuName = "Inventory/Consumable")]
public class ItemConsumables : Item
{
    [SerializeField] private int _healthRegeneration;
    public override void Use()
    {
        Debug.Log("Used " + Name + " and regained " + _healthRegeneration + "hp");
    }
}
