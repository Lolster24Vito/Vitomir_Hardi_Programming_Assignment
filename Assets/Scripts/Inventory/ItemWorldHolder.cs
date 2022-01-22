using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldHolder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Item _item;

    public Item GetItem()
    {
        return _item;
    }
   
}
