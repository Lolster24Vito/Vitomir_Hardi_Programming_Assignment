﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    private Transform _originalParent = null;
    private Vector3 _localPosition;
    private CanvasGroup _canvasGroup;


    private ItemUiHolder _itemUiHolder;
    private ItemEquipableUiHolder _itemEquipableUiHolder;
    private RectTransform _rectTransform;

    private static ItemInAirEventArgs _itemInAirEventArgs;
    private bool _itemInAir = false;

    private bool firstClick = false;
    // private static Transform _currentHoverItemTransform;
    // Start is called before the first frame update
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _itemUiHolder = GetComponent<ItemUiHolder>();
        _itemEquipableUiHolder = GetComponent<ItemEquipableUiHolder>();
        _rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                DropItem();
            }
            if (_itemInAirEventArgs == null)
            {
               if((_itemEquipableUiHolder != null && _itemEquipableUiHolder.HasItem()) || (_itemUiHolder != null && _itemUiHolder.HasItem()))
                {
                _originalParent = transform.parent;
                _localPosition = transform.localPosition;
                
                _rectTransform.SetParent(transform.parent.parent.parent.parent);
                _rectTransform.SetAsLastSibling();
                _canvasGroup.blocksRaycasts = false;

                _itemInAirEventArgs = new ItemInAirEventArgs(this, ref _itemUiHolder, ref _itemEquipableUiHolder);
                _itemInAir = true;
                 firstClick = true;
                }
                //pack things into the eventArgs


            }
            else
            {

                if (_itemUiHolder != null)
                {
                    if (_itemInAirEventArgs._itemUIHolder != null)
                    {
                        _itemUiHolder.SwapItems(ref _itemInAirEventArgs._itemUIHolder);
                    }
                    if (_itemInAirEventArgs._itemEquipableUIHolder != null)
                    {
                        _itemUiHolder.SwapItems(ref _itemInAirEventArgs._itemEquipableUIHolder);
                    }

                }

                if (_itemEquipableUiHolder != null)
                {
                    if(_itemInAirEventArgs._itemUIHolder!=null&& _itemInAirEventArgs._itemUIHolder.GetItem()!=null&&
                        _itemEquipableUiHolder.IsValid(_itemInAirEventArgs._itemUIHolder.GetItem().GetEquipmentType()))
                        //_itemInAirEventArgs._itemUIHolder.GetItem().GetEquipmentType() == _itemEquipableUiHolder.EquipmentType)
                    {
                        PlayerEquipmentManager.Instance.ItemSlotToChange=_itemEquipableUiHolder.EquipmentTypeSlot;
                            _itemInAirEventArgs._itemUIHolder.GetItem().Use();
                             PlayerInventoryManager.Instance.RemoveItem(_itemInAirEventArgs._itemUIHolder);
                            _itemInAirEventArgs._itemUIHolder.RemoveItem();
                        
                    }
                    
                }
                

                _itemInAirEventArgs.itemDragHandler.DisableInAir();
                _itemInAirEventArgs = null;

            }
        }
        if (eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Middle)
        {

            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<ItemUiHolder>(out ItemUiHolder mousePositionItemUiHolder))
            {
                if (mousePositionItemUiHolder.GetItem() != null)
                {
                    _itemUiHolder.GetItem().Use();
                    if (mousePositionItemUiHolder.GetItem() is ItemConsumables)
                    {
                        mousePositionItemUiHolder.ReduceAmount();
                        if (mousePositionItemUiHolder.GetAmount() <= 0)
                        {
                            PlayerInventoryManager.Instance.RemoveItem(_itemUiHolder);
                            _itemUiHolder.RemoveItem();
                        }
                    }
                    if (mousePositionItemUiHolder.GetItem() is ItemEquipable)
                    {
                        PlayerInventoryManager.Instance.RemoveItem(_itemUiHolder);
                        _itemUiHolder.RemoveItem();
                    }

                }
            }
            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<ItemEquipableUiHolder>(out ItemEquipableUiHolder mousePositionEquipableUI))
            {
                if (mousePositionEquipableUI.GetItem() != null)
                {
                    PlayerInventoryManager.Instance.AddItem(mousePositionEquipableUI.GetItem(), 1);
                    mousePositionEquipableUI.RemoveItem();

                }
            }


        }


    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowToolTip();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipManager.Instance.HideToolTip();
    }
    public void DisableInAir()
    {
        if (_itemInAir)
        {

            _itemInAir = false;
        transform.SetParent(_originalParent);
        transform.localPosition = _localPosition;
        _canvasGroup.blocksRaycasts = true;
        _itemInAirEventArgs = null;
        }

    }
    private void ShowToolTip()
    {
        Item item=null;
        if (_itemUiHolder != null&&_itemUiHolder.GetItem()!=null)
        {
            item = _itemUiHolder.GetItem();
        }
        if (_itemEquipableUiHolder != null&&_itemEquipableUiHolder.GetItem()!=null)
        {
            item = _itemEquipableUiHolder.GetItem();
        }
        if (item != null)
        {
            ToolTipManager.Instance.ShowToolTip(item);
        }
    }
    void OnEnable()
    {
        DisableInAir();
    }
    private void DropItem()
    {
        if (_itemUiHolder != null)
        {
            PlayerInventoryManager.Instance.DropItem(_itemUiHolder);
            _itemUiHolder.RemoveItem();
        }
        if (_itemEquipableUiHolder != null)
        {
            PlayerInventoryManager.Instance.DropItem(_itemEquipableUiHolder.GetItem(), 1);
            _itemEquipableUiHolder.RemoveItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_itemInAir)
        {
            transform.position = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {

                //DROP ITEM
                if (!EventSystem.current.IsPointerOverGameObject())
                {

                    DropItem();


                    DisableInAir();
                    /*_itemInAir = false;
                    _itemInAirEventArgs = null;*/

                }
                else
                {
                    if (!firstClick)
                    {


                    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                    pointerEventData.position = Input.mousePosition;
                    List<RaycastResult> results = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(pointerEventData, results);
                    bool returnObject=true;
                    for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i].gameObject.GetComponent<ItemDragHandler>())
                        {
                            returnObject = false;
                            break;
                        }
                    }
                    if (returnObject)
                    {
                            DisableInAir();
                    }
                    }
                    firstClick = false;


                }
            }
        }
    }


}


public class ItemInAirEventArgs
{
   public ItemDragHandler itemDragHandler;
    public  ItemUiHolder _itemUIHolder;
    public ItemEquipableUiHolder _itemEquipableUIHolder;

    public ItemInAirEventArgs(ItemDragHandler itemDragHandler, ref ItemUiHolder itemUIHolder, ref ItemEquipableUiHolder itemEquipableUIHolder)
    {
        this.itemDragHandler = itemDragHandler;
        _itemUIHolder = itemUIHolder;
        _itemEquipableUIHolder = itemEquipableUIHolder;
    }
}
