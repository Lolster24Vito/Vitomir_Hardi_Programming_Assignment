using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PermanentUseItem", menuName = "Inventory/Permanent Use Item")]
public class ItemPermanentUse : Item
{
    public override void Use()
    {
        Debug.Log("ITEM "+Name+ " HAS BEEN PERMANETLY USED");
    }



}
