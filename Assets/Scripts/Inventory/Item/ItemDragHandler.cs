using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TouchScript.Gestures;

public class ItemDragHandler : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{

    private Transform _originalParent = null;
    private Vector3 _localPosition;
    private CanvasGroup _canvasGroup;


    private ItemUiHolder _itemUiHolder;
    private ItemEquipableUiHolder _itemEquipableUiHolder;
    private RectTransform _rectTransform;

    private static ItemInAirEventArgs _itemInAirEventArgs;
    private static ItemInAirEventArgs _itemHovered;
    private bool _itemInAir = false;

    private bool _firstClick = false;
    private bool _itemsSwitched = false;

    private LongPressGesture _longPressGesture;
    private ReleaseGesture _releaseGesture;
    private TapGesture _doubleTapGesture;

    // private static Transform _currentHoverItemTransform;
    // Start is called before the first frame update
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _itemUiHolder = GetComponent<ItemUiHolder>();
        _itemEquipableUiHolder = GetComponent<ItemEquipableUiHolder>();
        _rectTransform = GetComponent<RectTransform>();

        _longPressGesture = GetComponent<LongPressGesture>();
        _releaseGesture = GetComponent<ReleaseGesture>();
        _doubleTapGesture = GetComponent<TapGesture>();


        _longPressGesture.LongPressed += LongPressAddItemToAir;
        _releaseGesture.Released += ItemRelease;
        _doubleTapGesture.Tapped += DoubleTap;

    }

    private void DoubleTap(object sender, System.EventArgs e)
    {
        Debug.Log("Doubletap");
        if (IsUsingMobile.IsMobile)
        UseItem();

    }
    
    private void ItemRelease(object sender, System.EventArgs e)
    {
        Debug.Log("ItemRelease");
        if (IsUsingMobile.IsMobile)
        {
        Debug.Log("ItemReleased");

        if (_itemInAirEventArgs != null&&_itemHovered!=null)
        {

            if (_itemHovered._itemUIHolder != null)
            {
                if (_itemInAirEventArgs._itemUIHolder != null)
                {
                    _itemHovered._itemUIHolder.SwapItems(ref _itemInAirEventArgs._itemUIHolder);
                }
                if (_itemInAirEventArgs._itemEquipableUIHolder != null)
                {
                    _itemHovered._itemUIHolder.SwapItems(ref _itemInAirEventArgs._itemEquipableUIHolder);
                }

            }

            if (_itemHovered._itemEquipableUIHolder != null)
            {
                if (_itemInAirEventArgs._itemUIHolder != null && _itemInAirEventArgs._itemUIHolder.GetItem() != null &&
                    _itemHovered._itemEquipableUIHolder.IsValid(_itemInAirEventArgs._itemUIHolder.GetItem().GetEquipmentType()))
                {
                    PlayerEquipmentManager.Instance.ItemSlotToChange = _itemHovered._itemEquipableUIHolder.EquipmentTypeSlot;
                    _itemInAirEventArgs._itemUIHolder.GetItem().Use();
                    PlayerInventoryManager.Instance.RemoveItem(_itemInAirEventArgs._itemUIHolder);
                    _itemInAirEventArgs._itemUIHolder.RemoveItem();

                }

            }
        }
        if (_itemInAirEventArgs != null)
        {
        _itemInAirEventArgs.itemDragHandler.DisableInAir();
        _itemInAirEventArgs = null;

        }

        }
    }

    private void LongPressAddItemToAir(object sender, System.EventArgs e)
    {
        Debug.Log("LongPress");
        if (IsUsingMobile.IsMobile)
            if (_itemInAirEventArgs==null)
            CheckAndAddItemToAir();
        
    }

   
    private void CheckAndAddItemToAir()
    {
        Debug.Log("THISSS");
        if ((_itemEquipableUiHolder != null && _itemEquipableUiHolder.HasItem()) || (_itemUiHolder != null && _itemUiHolder.HasItem())&& !_itemsSwitched)
        {
            _originalParent = transform.parent;
            _localPosition = transform.localPosition;

            _rectTransform.SetParent(transform.parent.parent.parent.parent);
            _rectTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;

            _itemInAirEventArgs = new ItemInAirEventArgs(this, ref _itemUiHolder, ref _itemEquipableUiHolder);
            _itemInAir = true;

            _firstClick = true;

            StackableItemsSplitUI.Instance.HideUI();

        }

        _itemsSwitched = false;

    }
    private void SwapItems()
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
            if (_itemInAirEventArgs._itemUIHolder != null && _itemInAirEventArgs._itemUIHolder.GetItem() != null &&
                _itemEquipableUiHolder.IsValid(_itemInAirEventArgs._itemUIHolder.GetItem().GetEquipmentType()))
            {
                PlayerEquipmentManager.Instance.ItemSlotToChange = _itemEquipableUiHolder.EquipmentTypeSlot;
                _itemInAirEventArgs._itemUIHolder.GetItem().Use();
                PlayerInventoryManager.Instance.RemoveItem(_itemInAirEventArgs._itemUIHolder);
                _itemInAirEventArgs._itemUIHolder.RemoveItem();

            }

        }
        _itemsSwitched = true;


        _itemInAirEventArgs.itemDragHandler.DisableInAir();
        _itemInAirEventArgs = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clici");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                DropItem();
            }
            if (!IsUsingMobile.IsMobile)
            {

            //if theres no item in air
            
                    Debug.Log("kkk");
                if (_itemInAirEventArgs == null)
                {
                    Debug.Log("IOK");
                    CheckAndAddItemToAir();

                }
                else
                {
                    Debug.Log("ppppppppppe");
                    if (_itemInAirEventArgs != null)
                    {

                        SwapItems();

                    }
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Middle)
        {


            UseItem();

        }


    }
    private void UseItem()
    {
        Debug.Log("Use");
        if (_itemUiHolder != null)
        {
            if (_itemUiHolder.GetItem() != null)
            {
                if (_itemUiHolder.GetItem() is ItemEquipable)
                {
                    PlayerEquipmentManager.Instance.DurabilitySetHelper = _itemUiHolder.GetDurability();
                }
                this._itemUiHolder.GetItem().Use();
                if (_itemUiHolder.GetItem() is ItemConsumables)
                {
                  
                    _itemUiHolder.ReduceAmount();
                    if (_itemUiHolder.GetAmount() <= 0)
                    {
                        PlayerInventoryManager.Instance.RemoveItem(this._itemUiHolder);
                        this._itemUiHolder.RemoveItem();
                    }
                }
                if (_itemUiHolder.GetItem() is ItemEquipable)
                {
                    PlayerInventoryManager.Instance.RemoveItem(this._itemUiHolder);
                    this._itemUiHolder.RemoveItem();
                }

            }
        }

        if (_itemEquipableUiHolder != null)
        {
            if (_itemEquipableUiHolder.GetItem() != null)
            {
                PlayerInventoryManager.Instance.AddItem(_itemEquipableUiHolder.GetItem(), 1, _itemEquipableUiHolder.Durability);
                _itemEquipableUiHolder.RemoveItem();

            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowToolTip();
        if (IsUsingMobile.IsMobile)
        {
            _itemHovered=new ItemInAirEventArgs(this, ref _itemUiHolder, ref _itemEquipableUiHolder);
            if(_itemUiHolder!=null)
            _itemUiHolder.ShowSplitUI();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsUsingMobile.IsMobile)
        {
        ToolTipManager.Instance.HideToolTip();
        }
        if (IsUsingMobile.IsMobile)
        {
            ToolTipManager.Instance.HideToolTip();
        }
    }
    public void DisableInAir()
    {
        Debug.Log("Disable in air");
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
        float durability=0f;
        if (_itemUiHolder != null&&_itemUiHolder.GetItem()!=null)
        {
            item = _itemUiHolder.GetItem();
            durability = _itemUiHolder.GetDurability();
        }
        if (_itemEquipableUiHolder != null&&_itemEquipableUiHolder.GetItem()!=null)
        {
            item = _itemEquipableUiHolder.GetItem();
            durability = _itemEquipableUiHolder.Durability;
        }

        if (item != null&&durability!=0)
        {
            ToolTipManager.Instance.ShowToolTip(item,durability);
        }
        else
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
            PlayerInventoryManager.Instance.DropItem(_itemEquipableUiHolder);
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
                    if (!_firstClick)
                    {
                        if (_itemInAirEventArgs._itemUIHolder != null)
                        {
                            _itemInAirEventArgs._itemUIHolder.ShowSplitUI();
                        }


                    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                    pointerEventData.position = Input.mousePosition;
                    List<RaycastResult> results = new List<RaycastResult>();
                    IsUsingMobile.Instance.NormalEventSystem.RaycastAll(pointerEventData, results);
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
                    _firstClick = false;


                }
            }

            if (Input.touchCount > 0)
            {
                Touch touch=Input.GetTouch(0);
                if (touch.phase==TouchPhase.Ended)
                {
                    if (_itemInAir)
                    {

                        if (!IsUsingMobile.Instance.NormalEventSystem.IsPointerOverGameObject(touch.fingerId))
                        {

                        DropItem();

                            ToolTipManager.Instance.HideToolTip();

                            DisableInAir();
                            //_itemInAir = false;
                           // _itemInAirEventArgs = null;

                        }
                        else
                        if (_itemInAirEventArgs != null)
                        {
                            if (_itemUiHolder != null)
                            {
                                Debug.Log("item called:" + _itemUiHolder.GetItem().ToString());
                            }

                            if (_itemUiHolder != null)
                            {
                                Debug.Log("item iteminair:" + _itemInAirEventArgs._itemUIHolder.GetItem().ToString());
                            }

                            SwapItems();
                        }
                    }
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
