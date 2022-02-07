using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemConsumable", menuName = "Inventory/Consumable")]
public class ItemConsumables : Item
{
    [SerializeField]public  SpendableAttributes SpendableAttributes;
    [SerializeField]public ItemAttributes CharacterAttributes;
    [SerializeField]public float ConsumeEndTimeSeconds;
    [Tooltip("Time when value will ramp up to the maximum")]
    [SerializeField]public float RampValueMaxSeconds;
    [SerializeField]public float TickTimeToAddNewAttributes;
    [SerializeField]public EnumConsumeEffectType EnumConsumeType;
    public override void Use()
    {
        PlayerBuffManager.Instance.UseItem(this);
    }
}
public enum EnumConsumeEffectType
{
    HoldBonus,RampValue,TickValue
}
