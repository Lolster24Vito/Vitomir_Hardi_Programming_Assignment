using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipableUiHolder : MonoBehaviour
{
    //ovo zamijeniti s drugim enumom koja oznacava equipmentSlot


    [SerializeField] private EnumEquipmentSlot _equipmentTypeSlot;
    public EnumEquipmentSlot EquipmentTypeSlot { get => _equipmentTypeSlot; }
    [SerializeField] private ItemEquipable _item;
    private ItemDragHandler _itemDragHandler;
    public float Durability;
    Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
        _itemDragHandler = GetComponent<ItemDragHandler>();
    }
    public void SetItem(ItemEquipable item,float durability)
    {
        _item = item;
        _image.sprite = item.Icon;
        Durability = durability;
    }
    public void RemoveItem()
    {
        PlayerEquipmentManager.Instance.RemoveItem(_equipmentTypeSlot);
        _item = null;
        _image.sprite = null;
    }
    public void UpdateDurability()
    {

    }
    public Item GetItem()
    {
        return (Item)_item;
    }
    public ItemEquipable GetItemEquipable()
    {
        return _item;
    }
    public bool HasItem()
    {
        return _item != null;
    }
    public void DisableInAir()
    {
        if(_itemDragHandler!=null)
        {
            _itemDragHandler.DisableInAir();
        }
    }

    internal bool IsValid(EnumEquipmentType enumEquipmentType)
    {
        if (EquipmentTypeSlot.ToString() == enumEquipmentType.ToString())
        {
            return true;
        }
        if (enumEquipmentType == EnumEquipmentType.Ring&&
            (EquipmentTypeSlot == EnumEquipmentSlot.LeftRing || EquipmentTypeSlot == EnumEquipmentSlot.RightRing))
        {
            return true;
        }
        return false;
    }
}
