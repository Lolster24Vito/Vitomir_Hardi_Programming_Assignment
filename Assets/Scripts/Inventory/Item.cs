using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
[System.Serializable]
public  class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public virtual void PickUpItem() { }
}

=======
[CreateAssetMenu(fileName ="Item",menuName ="Inventory/Item")]
public class Item :ScriptableObject,IEqualityComparer<Item>
{
    public int ID = 1;
    public string Name;
    public string Description;
    public Sprite Icon;
    public int MaxStack;

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


}
>>>>>>> Stashed changes
