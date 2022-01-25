using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipableItem", menuName = "Inventory/Equipable Item")]
public class ItemEquipable : Item
{

    [SerializeField]public ItemAttributes Attributes;
    [SerializeField]public EnumEquipmentType EquipmentType;

    /*
    create ui 

    ATTR STRENGTH;DEXTERITY.....

     */
    public override void Use()
    {
        PlayerEquipmentManager.Instance.Equip(this);
    }
}
