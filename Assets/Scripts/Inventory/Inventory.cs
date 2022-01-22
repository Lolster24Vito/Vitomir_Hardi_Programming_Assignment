using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] List<Item> _items = new List<Item>();

    internal void AddItem(Item item)
    {
        _items.Add(item);
    }
}
