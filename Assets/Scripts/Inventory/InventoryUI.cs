using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    [SerializeField] Transform itemsParent;

    private ItemUiHolder[] _itemSlots;

    private ItemUiHolder _hoveredItem;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        _itemSlots = itemsParent.GetComponentsInChildren<ItemUiHolder>();
       // PlayerInventoryManager.instance.onItemChangedCallback += UpdateUI;
    }
    public void AddItem(Item item)
    {
        for(int i = 0; i < _itemSlots.Length; i++)
        {
            if (!_itemSlots[i].HasItem())
            {
                _itemSlots[i].SetItem(item);
               // itemsWithPosition.Add(new ItemPosition { Item = item, ChildPosition = i });
                return;
            }
        }
    }
    
    public void SetItemToIndex(int index)
    {
       // _itemSlots[index]=
    }
    public void SwapItems()
    {

    }
    public void setHoveredItem(ItemUiHolder itemUiHolder)
    {
        _hoveredItem = itemUiHolder;
    }
}


