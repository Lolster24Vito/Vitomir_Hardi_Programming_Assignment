using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    private static PlayerEquipmentManager _instance;

    public static PlayerEquipmentManager Instance { get { return _instance; } }

    public static event Action<ItemEquipable,EnumEquipmentSlot> OnItemEquipped;
    public static event Action itemEquipped;
    public static event Action OnItemUnequipped;

 ///   [SerializeField] private PlayerEquipmentUIManager _playerEquipmentUIManager;

    private ItemEquipable _head;
    public ItemEquipable Head { get => _head; }
    private ItemEquipable _torso;
    public ItemEquipable Torso { get => _torso; }
    private ItemEquipable _weapon;
    public ItemEquipable Weapon { get => _weapon; }
    private ItemEquipable _shield;
    public ItemEquipable Shield { get => _shield; } 
    private ItemEquipable _boots;
    public ItemEquipable Boots { get => _boots; }
    private ItemEquipable _leftRing;
    public ItemEquipable LeftRing { get => _leftRing; }

    private ItemEquipable _rightRing;
    public ItemEquipable RightRing { get => _rightRing; }

    bool _changeSpecificItemSlot = false;
    private EnumEquipmentSlot _itemSlotToChange;
    public EnumEquipmentSlot ItemSlotToChange {
        get => _itemSlotToChange;
        set {
            _changeSpecificItemSlot = true;
            _itemSlotToChange = value;
        } }
    

    void Awake()
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

    // Update is called once per frame
    public void Equip(ItemEquipable item)
    {
        //Update attributes in attribute manager
        //equip item here
        Item swappedItem=null;

        if(_changeSpecificItemSlot)
        switch (ItemSlotToChange)
        {
            case EnumEquipmentSlot.Torso:
                if(_torso!=null)
                swappedItem = _torso;
                _torso = item;
                break;
            case EnumEquipmentSlot.Head:
                if(_head!=null)
                swappedItem = _head;
                _head = item;
                break;
            case EnumEquipmentSlot.Weapon:
                if(_weapon!=null)
                swappedItem = _weapon;
                _weapon = item;
                break;
            case EnumEquipmentSlot.Shield:
                if(_shield!=null)
                swappedItem = _shield;
                _shield = item;
                break;
            case EnumEquipmentSlot.Boots:
                if (_boots != null)
                    swappedItem = _boots;
                _boots = item;
                break;
            case EnumEquipmentSlot.LeftRing:
                if (_leftRing != null)
                    swappedItem = _leftRing;
                _leftRing = item;

                break;
            case EnumEquipmentSlot.RightRing:
                if (_rightRing != null)
                    swappedItem = _rightRing;
                _rightRing = item;

                break;

            default:
                break;
        }
        else
        {
            switch (item.EquipmentType)
            {
                case EnumEquipmentType.Torso:
                    if (_torso != null)
                        swappedItem = _torso;
                    _torso = item;
                    ItemSlotToChange = EnumEquipmentSlot.Torso;
                    break;
                case EnumEquipmentType.Head:
                    if (_head != null)
                        swappedItem = _head;
                    _head = item;
                    ItemSlotToChange = EnumEquipmentSlot.Head;
                    break;
                case EnumEquipmentType.Weapon:
                    if (_weapon != null)
                        swappedItem = _weapon;
                    _weapon = item;
                    ItemSlotToChange = EnumEquipmentSlot.Weapon;
                    break;
                case EnumEquipmentType.Shield:
                    if (_shield != null)
                        swappedItem = _shield;
                    _shield = item;
                    ItemSlotToChange = EnumEquipmentSlot.Shield;
                    break;
                case EnumEquipmentType.Boots:
                    if (_boots != null)
                        swappedItem = _boots;
                    _boots = item;
                    ItemSlotToChange = EnumEquipmentSlot.Boots;
                    break;
                case EnumEquipmentType.Ring:
                    //cases where i pick left ring
                    if (_leftRing == null || (_leftRing != null && _rightRing != null))
                    {
                        if(_leftRing!=null)
                        swappedItem = _leftRing;
                    _leftRing = item;
                    ItemSlotToChange = EnumEquipmentSlot.LeftRing;

                    }
                    else
                    {
                        if (_rightRing == null)
                        {
                            ItemSlotToChange = EnumEquipmentSlot.RightRing;
                            _rightRing = item;
                        }
                    }
                    
                    break;
                default:
                    break;
            }
        }
        _changeSpecificItemSlot = false;
        OnItemEquipped?.Invoke(item, ItemSlotToChange);
        itemEquipped?.Invoke();
        if (swappedItem != null)
        {
            PlayerInventoryManager.Instance.AddItem(swappedItem, 1);//send item to inventory
        }
        //send item to equipmentUI---if already taken swap item and add to inventory


    }
    public void RemoveItem(EnumEquipmentSlot enumEquipmentType) 
    {


     switch (enumEquipmentType)
        {
            case EnumEquipmentSlot.Torso:
                _torso = null;
                break;
            case EnumEquipmentSlot.Head:
                _head = null;

                break;
            case EnumEquipmentSlot.Weapon:
                _weapon = null;

                break;
            case EnumEquipmentSlot.Shield:
                _shield = null;
                break;
            case EnumEquipmentSlot.Boots:
                _boots= null;
                break;
            case EnumEquipmentSlot.LeftRing:
                _leftRing = null;
                break;
            case EnumEquipmentSlot.RightRing:
                _rightRing = null;
                break;
            default:
                break;
        }

        OnItemUnequipped?.Invoke();
       // _playerEquipmentUIManager.RemoveItem(enumEquipmentType);

    }
}
