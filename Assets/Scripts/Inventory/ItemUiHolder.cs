using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUiHolder : MonoBehaviour
{

   [SerializeField] private Item _item=null;
     private Image _imageIcon;


    private void Start()
    {
        _imageIcon = GetComponent<Image>();
    }
    public void SetItem(Item item)
    {
        _item = item;
        _imageIcon.sprite = item.Icon;
    }
    public Item GetItem()
    {
        return _item;
    }
    public bool HasItem()
    {
        return _item != null;
    }
    public void DropItem()
    {

    }



}
