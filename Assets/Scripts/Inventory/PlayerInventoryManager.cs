using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    private static PlayerInventoryManager _instance;

    public static PlayerInventoryManager Instance { get { return _instance; } }

    const int MAX_ITEMS  = 32;
    private int _currentAmountOfItems;
    public static event Action<ItemWorld> OnItemAdded;
    private List<Item> _items = new List<Item>();
    [SerializeField]private GameObject _droppedItemPrefab;
    [SerializeField]private Vector2 _droppedItemOffset;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (collision.TryGetComponent<ItemWorld>(out ItemWorld itemWorld))
            {
                if(itemWorld.GetItem() is ItemPermanentUse)
                {
                    itemWorld.GetItem().Use();
                    Destroy(collision.gameObject);
                    return;
                }
                AddItem(itemWorld.GetItem());
                OnItemAdded?.Invoke(itemWorld);
                //_inventoryUI.AddItem(itemHolder.GetItem());
                Destroy(collision.gameObject);
            }
        }
    }
    void AddItem(Item item)
    {
        if (_items.Count > MAX_ITEMS)
        {
        _items.Add(item);
        }
    }
    public void RemoveItem(ItemUiHolder itemUiHolder)
    {
        Item item = itemUiHolder.GetItem();
        Vector3 currentPosition = transform.position;
        Vector3 dropPosition = new Vector3(currentPosition.x + _droppedItemOffset.x, currentPosition.y + _droppedItemOffset.y, currentPosition.z);
        GameObject droppedItem=Instantiate(_droppedItemPrefab, dropPosition,Quaternion.identity);
        droppedItem.GetComponent<ItemWorld>().SetItem(item);
        droppedItem.GetComponent<ItemWorld>().SetAmount(itemUiHolder.GetAmount());
        _items.Remove(item);

    }
    
}

