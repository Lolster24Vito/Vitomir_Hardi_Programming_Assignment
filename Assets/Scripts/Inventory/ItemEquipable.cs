using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipableItem", menuName = "Inventory/Equipable Item")]
public class ItemEquipable : Item
{

    [SerializeField]public ItemAttributes Attributes;
    [SerializeField]public EnumEquipmentType EquipmentType;
    

    
    public override void Use()
    {
        PlayerEquipmentManager.Instance.Equip(this);
    }
    public override EnumEquipmentType GetEquipmentType()
    {
        return EquipmentType;
    }
    public override string ToString()
    {
        char breakLine = '\n';
        string text= base.ToString()+"Equipable"+breakLine+"Equipment Type:"+EquipmentType+breakLine+Attributes.ToString();
        return text;
    }

}
