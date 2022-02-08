using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]private Transform _itemsParent;

    private ItemUiHolder[] _itemSlots;



    private void Awake()
    {
        PlayerInventoryManager.OnItemAdded += AddItem;
    }
    private void Start()
    {
        _itemSlots = _itemsParent.GetComponentsInChildren<ItemUiHolder>(true);

    }
    public void AddItem(Item item,int amount,float durability)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (!_itemSlots[i].HasItem())
            {
                _itemSlots[i].SetItem(item,amount, durability);
                return;
            }
        }
    }

   
}

