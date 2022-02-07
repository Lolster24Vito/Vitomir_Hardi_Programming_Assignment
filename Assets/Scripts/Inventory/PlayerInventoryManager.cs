using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    private static PlayerInventoryManager _instance;

    public static PlayerInventoryManager Instance { get { return _instance; } }

    private List<Item> _items = new List<Item>();

    [SerializeField] private GameObject _droppedItemPrefab;
    [SerializeField] private Vector2 _droppedItemOffset;
    public static event Action<Item,int,float> OnItemAdded;
    [System.Serializable]
    enum PickUpType{Trigger, OverlapCircle, CircleCasting}
    [SerializeField]private PickUpType pickUpType;
    [SerializeField]private float _overlapCastingRadius=3f;
    const int MAX_ITEMS  = 32;
    private int _currentAmountOfItems;
    bool inputPressed;




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
    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            inputPressed = true;
            if (pickUpType == PickUpType.OverlapCircle)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _overlapCastingRadius);
                for (int i = 0; i < colliders.Length; i++)
                {
                    PickUpItem(colliders[i]);
                }
            }
            if (pickUpType == PickUpType.CircleCasting)
            {
                RaycastHit2D[] raycastHits = Physics2D.CircleCastAll(transform.position, _overlapCastingRadius, Vector2.up, 2f);
                for (int i = 0; i < raycastHits.Length; i++)
                {

                    PickUpItem(raycastHits[i].collider);
                }
            }

        }
        else
        {
            inputPressed = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (pickUpType==PickUpType.Trigger&& inputPressed)
        {
            PickUpItem(collision);
        }
    }
    private void PickUpItem(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (collision.TryGetComponent<ItemWorld>(out ItemWorld itemWorld))
            {
                if (itemWorld.GetItem() is ItemPermanentUse)
                {
                    itemWorld.GetItem().Use();
                    Destroy(collision.gameObject);
                    return;
                }
                if (_items.Count <= MAX_ITEMS)
                {
                    AddItem(itemWorld.GetItem(), itemWorld.GetAmount(),itemWorld.Durability);

                    //_inventoryUI.AddItem(itemHolder.GetItem());
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    public void AddItem(Item item,int amount,float durability)
    {

        if (_items.Count < MAX_ITEMS)
        {
        _items.Add(item);
         OnItemAdded?.Invoke(item,amount, durability);
        }
    }
    public void DropItem(ItemUiHolder itemUiHolder)
    {
        Item item = itemUiHolder.GetItem();
        Vector3 currentPosition = transform.position;
        Vector3 dropPosition = new Vector3(currentPosition.x + _droppedItemOffset.x, currentPosition.y + _droppedItemOffset.y, currentPosition.z);
        GameObject droppedItem=Instantiate(_droppedItemPrefab, dropPosition,Quaternion.identity);
        ItemWorld droppedItemWorld = droppedItem.GetComponent<ItemWorld>();
        droppedItemWorld.SetItem(item);
        droppedItemWorld.SetAmount(itemUiHolder.GetAmount());
        droppedItemWorld.Durability=itemUiHolder.GetDurability();
        _items.Remove(item);
    }
    public void DropItem(ItemEquipableUiHolder itemEquipableUi)
    {

        Item item = itemEquipableUi.GetItem();
        Vector3 currentPosition = transform.position;
        Vector3 dropPosition = new Vector3(currentPosition.x + _droppedItemOffset.x, currentPosition.y + _droppedItemOffset.y, currentPosition.z);
        GameObject droppedItem = Instantiate(_droppedItemPrefab, dropPosition, Quaternion.identity);
        ItemWorld droppedItemWorld = droppedItem.GetComponent<ItemWorld>();
        droppedItemWorld.SetItem(item);
        droppedItemWorld.SetAmount(1);
        droppedItemWorld.Durability=itemEquipableUi.Durability;
        _items.Remove(item);
    }

    public void RemoveItem(ItemUiHolder itemUiHolder)
    {
        _items.Remove(itemUiHolder.GetItem());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 1f, 0.3f);
        Gizmos.DrawSphere(transform.position, _overlapCastingRadius);
    }

}


