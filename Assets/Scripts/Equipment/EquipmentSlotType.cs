using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlotType
{
    public ItemEquipableIndividual Item;
    readonly EnumEquipmentSlot _equipmentSlotType;
    public EnumEquipmentSlot EquipmentSlot { get => _equipmentSlotType; }

    public EquipmentSlotType(EnumEquipmentSlot equipmentSlotType)
    {
        this._equipmentSlotType = equipmentSlotType;
    }
}
