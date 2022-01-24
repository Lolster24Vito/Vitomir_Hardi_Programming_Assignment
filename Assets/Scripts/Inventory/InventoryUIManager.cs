using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]private Transform _itemsParent;

    private ItemUiHolder[] _itemSlots;

    private ItemUiHolder _hoveredItem;


    private void Awake()
    {
        PlayerInventoryManager.OnItemAdded += AddItem;
    }
    private void Start()
    {
        _itemSlots = _itemsParent.GetComponentsInChildren<ItemUiHolder>();
    }
    public void AddItem(ItemWorld item)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (!_itemSlots[i].HasItem())
            {
                _itemSlots[i].SetItem(item.GetItem(),item.GetAmount());
                return;
            }
        }
    }

   
 
    public void setHoveredItem(ItemUiHolder itemUiHolder)
    {
        _hoveredItem = itemUiHolder;
    }
}

