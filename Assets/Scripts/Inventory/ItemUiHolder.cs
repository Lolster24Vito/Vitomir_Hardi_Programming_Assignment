using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUiHolder : MonoBehaviour
{

     private Item _item = null;
     private Image _imageIcon;
    private int _amount = 0;
    [SerializeField] private Text _text;

    private void Start()
    {
        _text.text = "";
        _imageIcon = GetComponent<Image>();
    }
    public void  SetItem(Item item,int amount)
    {
        _amount = amount;
        _item = item;
        updateText();
        if (item != null && _imageIcon != null)
            _imageIcon.sprite = item.Icon;
        else _imageIcon.sprite = null;
    }
    public void SwapItems(ref ItemUiHolder itemHolder)
    {
        Item itemFromParameter=itemHolder.GetItem();

        if (itemFromParameter != null&&_item!=null&& GetItem().Equals(itemFromParameter)&&(_amount+itemHolder._amount<=GetItem().MaxStack||_item.InfiniteStack))
        {
            _amount += itemHolder._amount;
            updateText();
            itemHolder.RemoveItem();

        }
        else
        {
            int thisAmount = _amount;
            int parameterAmount = itemHolder._amount;
            itemHolder.SetItem(GetItem(), thisAmount);
            SetItem(itemFromParameter, parameterAmount);
        }
    }
    public void SwapItems(ref ItemEquipableUiHolder itemEquipableHolder)
    {
        Item itemFromParameter = itemEquipableHolder.GetItem();

        Item thisItem = GetItem();
        int thisItemAmount = GetAmount();
        bool hasItem = HasItem();
        //if empty_just simply put it there
        //if has something then remove item and put it into the inventory again
        RemoveItem();
        SetItem(itemFromParameter, 1);   
        itemEquipableHolder.RemoveItem();
        if (hasItem)
        {
            PlayerInventoryManager.Instance.AddItem(thisItem, GetAmount());
        }

    }

    public Item GetItem()
    {
        return _item;
    }
    public bool HasItem()
    {
        return _item != null;
    }
    public void RemoveItem()
    {
        _imageIcon.sprite = null;
        _item = null;
        _amount = 0;
        updateText();
    }
    private void updateText()
    {
        if (_amount == 0 || _amount == 1)
        {
            _text.text = "";
        }
        else
        {
            _text.text = _amount.ToString();
        }
    }
    public int GetAmount()
    {
        return _amount;
    }
   
}