using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
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
=======
[CreateAssetMenu(fileName = "PermanentUseItem", menuName = "Inventory/Permanent Use Item")]
public class ItemPermanentUse : Item
{
    public override void Use()
    {
        Debug.Log("ITEM "+Name+ " HAS BEEN PERMANETLY USED");
    }



>>>>>>> Stashed changes
}
