using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPermanentUse : Item
{

    public  void PickUp()
    {
        Debug.Log("Picked up permenant use item"+this.Name);
    }

    public override void PickUpItem()
    {
        throw new System.NotImplementedException();
    }
}
