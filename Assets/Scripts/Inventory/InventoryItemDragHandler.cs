using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler
{
    private Transform _originalParent = null;
    private CanvasGroup _canvasGroup;


    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _originalParent = transform.parent;
            transform.SetParent(transform.parent.parent);
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
            transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;

            //IF THERE IS SWAP ITEMS
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                //DROP ITEM
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryUI.Instance.setHoveredItem(this.GetComponent<ItemUiHolder>());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryUI.Instance.setHoveredItem(null);
    }

    // Start is called before the first frame update



}
