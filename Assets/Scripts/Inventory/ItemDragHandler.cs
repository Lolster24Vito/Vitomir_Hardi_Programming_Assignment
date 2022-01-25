using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform _originalParent = null;
    private Vector3 _localPosition;
    private CanvasGroup _canvasGroup;

  // private static Transform _currentHoverItemTransform;

    private ItemUiHolder _itemUiHolder;
    private ItemEquipableUiHolder _itemEquipableUiHolder;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _itemUiHolder = GetComponent<ItemUiHolder>();
        _itemEquipableUiHolder = GetComponent<ItemEquipableUiHolder>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _originalParent = transform.parent;
            _localPosition = transform.localPosition;
            transform.SetParent(transform.parent.parent.parent);
            _canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent(_originalParent);
            transform.localPosition = _localPosition;
            _canvasGroup.blocksRaycasts = true;

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //DROP ITEM
                PlayerInventoryManager.Instance.DropItem(_itemUiHolder);
                _itemUiHolder.RemoveItem();

            }
            else {

                if(eventData.pointerCurrentRaycast.gameObject.TryGetComponent<ItemUiHolder>(out ItemUiHolder mousePositionItemUiHolder))
                {
                    if(_itemUiHolder!=null)
                    mousePositionItemUiHolder.SwapItems(ref _itemUiHolder);

                    if (_itemEquipableUiHolder != null)
                    {
                        mousePositionItemUiHolder.SwapItems(ref _itemEquipableUiHolder);
                    }
                }

                
                //if you click on any item slot automatically equip it.
                if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<ItemEquipableUiHolder>(out ItemEquipableUiHolder mousePositionEquipableUIHolder))
                { 
                    if(_itemUiHolder.GetItem()!=null&&_itemUiHolder.GetItem() is ItemEquipable)
                    {
                        EquipItem();
                    }
                }


            }
        }
        if (eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Middle)
        {


            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<ItemUiHolder>(out ItemUiHolder mousePositionItemUiHolder))
            {
                if (mousePositionItemUiHolder.GetItem() != null)
                {

                    EquipItem();
                }
            }
            

        }
        if (eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Middle)
        {
            

            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<ItemUiHolder>(out ItemUiHolder mousePositionItemUiHolder))
            {
                mousePositionItemUiHolder.GetItem().Use();
            }
            PlayerInventoryManager.Instance.RemoveItem(_itemUiHolder);
            _itemUiHolder.RemoveItem();

        }
    }
    private void EquipItem()
    {
        _itemUiHolder.GetItem().Use();
        PlayerInventoryManager.Instance.RemoveItem(_itemUiHolder);
        _itemUiHolder.RemoveItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //_currentHoverItemTransform = this.transform;
       // InventoryUI.Instance.setHoveredItem(this.GetComponent<ItemUiHolder>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // _currentHoverItemTransform = null;
      //  InventoryUI.Instance.setHoveredItem(null);
    }

    // Start is called before the first frame update



}