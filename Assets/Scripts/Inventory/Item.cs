using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
[System.Serializable]
public  class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public virtual void PickUpItem() { }
}

