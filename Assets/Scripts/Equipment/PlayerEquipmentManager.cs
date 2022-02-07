using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    private static PlayerEquipmentManager _instance;

    public static PlayerEquipmentManager Instance { get { return _instance; } }

    public static event Action<ItemEquipableIndividual,EnumEquipmentSlot> OnItemEquipped;
    public static event Action itemEquipped;
    public static event Action OnItemUnequipped;
    public static event Action<EnumEquipmentSlot, float> OnItemDurabilityChange;
    public static event Action<EnumEquipmentSlot> OnItemDestroyed;


    ///   [SerializeField] private PlayerEquipmentUIManager _playerEquipmentUIManager;
    public EquipmentSlotType[] EquipmentSlots = {
        new EquipmentSlotType(EnumEquipmentSlot.Boots),
        new EquipmentSlotType(EnumEquipmentSlot.Head),
        new EquipmentSlotType(EnumEquipmentSlot.LeftRing),
        new EquipmentSlotType(EnumEquipmentSlot.RightRing),
        new EquipmentSlotType(EnumEquipmentSlot.Shield),
        new EquipmentSlotType(EnumEquipmentSlot.Torso),
        new EquipmentSlotType(EnumEquipmentSlot.Weapon)
    };

    //new class that has ItemEquipableIndividual ali i slot type koji je readonly

    bool _changeSpecificItemSlot = false;
    private EnumEquipmentSlot _itemSlotToChange;
    public EnumEquipmentSlot ItemSlotToChange {
        get => _itemSlotToChange;
        set {
            _changeSpecificItemSlot = true;
            _itemSlotToChange = value;
        } }
    [NonSerialized]public float DurabilitySetHelper = 0;

    

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
    private void Start()
    {
        PlayerMovementPhysics.OnMoved += DecreseDurability;
    }

  
    private void DecreseDurability(float moveAmount)
    {
        /*
         for(int i=0;i<arraySize;i++){
            if(itemType.item!=null)
        }
         */
        for(int i=0;i< EquipmentSlots.Length; i++)
        {
            if (EquipmentSlots[i].Item != null)
            {
                EquipmentSlots[i].Item.CurrentDurability -= moveAmount;
                OnItemDurabilityChange?.Invoke(EquipmentSlots[i].EquipmentSlot, EquipmentSlots[i].Item.CurrentDurability);
                if (EquipmentSlots[i].Item.CurrentDurability <= 0)
                {
                    OnItemDestroyed?.Invoke(EquipmentSlots[i].EquipmentSlot);
                    //RemoveItem(EquipmentSlots[i].EquipmentSlot);
                }
            }
        }
        /*
                if (_torso != null)
        {
            _torso.CurrentDurability -= moveAmount;
            OnItemDurabilityChange?.Invoke(EnumEquipmentSlot.Torso, _torso.CurrentDurability);
            if (_torso.CurrentDurability == 0)
            {
                RemoveItem(EnumEquipmentSlot.Torso);
            }
        }
                if (_head != null)
        {
            _head.CurrentDurability -= moveAmount;
            if (_head.CurrentDurability == 0)
            {
                RemoveItem(EnumEquipmentSlot.Head);
            }
        }

                if (_weapon != null)
        {
            _weapon.CurrentDurability -= moveAmount;
            if (_weapon.CurrentDurability == 0)
            {
                RemoveItem(EnumEquipmentSlot.Weapon);
            }
        }

        
                if (_shield != null)
        {
            _shield.CurrentDurability -= moveAmount;
            if (_shield.CurrentDurability == 0)
            {
                RemoveItem(EnumEquipmentSlot.Shield);
            }
        }

        if (_boots != null)
        {
            _boots.CurrentDurability -= moveAmount;
            if (_boots.CurrentDurability == 0)
            {
                RemoveItem(EnumEquipmentSlot.Boots);
            }
        }
        if (_leftRing != null)
        {
            _leftRing.CurrentDurability -= moveAmount;
            if (_leftRing.CurrentDurability == 0)
            {
                RemoveItem(EnumEquipmentSlot.LeftRing);
            }
        }
        if (_rightRing != null)
        {
            _rightRing.CurrentDurability -= moveAmount;
            if (_rightRing.CurrentDurability == 0)
            {
                RemoveItem(EnumEquipmentSlot.RightRing);
            }
        }
           */

    }

    // Update is called once per frame
    public void Equip( ItemEquipable item)
    {
        //Update attributes in attribute manager
        //equip item here
        ItemEquipableIndividual swappedItem=null;
        ItemEquipableIndividual itemIndividual = new ItemEquipableIndividual(item, DurabilitySetHelper);


        if (_changeSpecificItemSlot)
            for(int i = 0; i < EquipmentSlots.Length; i++)
            {
                if(ItemSlotToChange== EquipmentSlots[i].EquipmentSlot)
                {
                    swappedItem = EquipmentSlots[i].Item;
                    EquipmentSlots[i].Item = itemIndividual;
                }
            }
        /*
        switch (ItemSlotToChange)
        {
            case EnumEquipmentSlot.Torso:
                if(_torso!=null)
                swappedItem = _torso;
                _torso = itemIndividual;
                break;
            case EnumEquipmentSlot.Head:
                if(_head!=null)
                swappedItem = _head;
                _head = itemIndividual;
                break;
            case EnumEquipmentSlot.Weapon:
                if(_weapon!=null)
                swappedItem = _weapon;
                _weapon = itemIndividual;
                break;
            case EnumEquipmentSlot.Shield:
                if(_shield!=null)
                swappedItem = _shield;
                _shield = itemIndividual;
                break;
            case EnumEquipmentSlot.Boots:
                if (_boots != null)
                    swappedItem = _boots;
                _boots = itemIndividual;
                break;
            case EnumEquipmentSlot.LeftRing:
                if (_leftRing != null)
                    swappedItem = _leftRing;
                _leftRing = itemIndividual;

                break;
            case EnumEquipmentSlot.RightRing:
                if (_rightRing != null)
                    swappedItem = _rightRing;
                _rightRing = itemIndividual;

                break;

            default:
                break;
        }
        */
        else
        {
            string name = item.EquipmentType.ToString();
            Debug.Log(name + "nameee");
            if (item.EquipmentType == EnumEquipmentType.Ring)
            {
              EquipmentSlotType _leftRing=GetEquipmentSlot(EnumEquipmentSlot.LeftRing);
                EquipmentSlotType _rightRing= GetEquipmentSlot(EnumEquipmentSlot.RightRing);
                //cases where i pick left ring
                if (_leftRing.Item == null || (_leftRing.Item != null && _rightRing.Item != null))
            {
                if (_leftRing.Item != null)
                    swappedItem = _leftRing.Item;
                _leftRing.Item = itemIndividual;
                ItemSlotToChange = EnumEquipmentSlot.LeftRing;

            }
            else
            {
                if (_rightRing.Item == null)
                {
                    ItemSlotToChange = EnumEquipmentSlot.RightRing;
                    _rightRing.Item = itemIndividual;
                }
            }

            }
            for (int i = 0; i < EquipmentSlots.Length; i++)
            {
                if (name == EquipmentSlots[i].EquipmentSlot.ToString())
                {
                    if (EquipmentSlots[i].Item != null)
                    swappedItem = EquipmentSlots[i].Item;

                    EquipmentSlots[i].Item = itemIndividual;
                    ItemSlotToChange = EquipmentSlots[i].EquipmentSlot;
                }
            }


           /* switch (item.EquipmentType)
            {
                case EnumEquipmentType.Torso:
                    if (_torso != null)
                        swappedItem = _torso;
                    _torso = itemIndividual;
                    ItemSlotToChange = EnumEquipmentSlot.Torso;
                    break;
                case EnumEquipmentType.Head:
                    if (_head != null)
                        swappedItem = _head;
                    _head = itemIndividual;
                    ItemSlotToChange = EnumEquipmentSlot.Head;
                    break;
                case EnumEquipmentType.Weapon:
                    if (_weapon != null)
                        swappedItem = _weapon;
                    _weapon = itemIndividual;
                    ItemSlotToChange = EnumEquipmentSlot.Weapon;
                    break;
                case EnumEquipmentType.Shield:
                    if (_shield != null)
                        swappedItem = _shield;
                    _shield = itemIndividual;
                    ItemSlotToChange = EnumEquipmentSlot.Shield;
                    break;
                case EnumEquipmentType.Boots:
                    if (_boots != null)
                        swappedItem = _boots;
                    _boots = itemIndividual;
                    ItemSlotToChange = EnumEquipmentSlot.Boots;
                    break;
                case EnumEquipmentType.Ring:
                    //cases where i pick left ring
                    if (_leftRing == null || (_leftRing != null && _rightRing != null))
                    {
                        if(_leftRing!=null)
                        swappedItem = _leftRing;
                    _leftRing = itemIndividual;
                    ItemSlotToChange = EnumEquipmentSlot.LeftRing;

                    }
                    else
                    {
                        if (_rightRing == null)
                        {
                            ItemSlotToChange = EnumEquipmentSlot.RightRing;
                            _rightRing = itemIndividual;
                        }
                    }
                    
                    break;
                default:
                    break;
            }*/
        }
        _changeSpecificItemSlot = false;
        OnItemEquipped?.Invoke(itemIndividual, ItemSlotToChange);
        itemEquipped?.Invoke();
        if (swappedItem != null)
        {
            PlayerInventoryManager.Instance.AddItem(swappedItem.Item, 1, swappedItem.CurrentDurability);//send item to inventory
        }
        //send item to equipmentUI---if already taken swap item and add to inventory


    }
    public void RemoveItem(EnumEquipmentSlot enumEquipmentType) 
    {
        for(int i = 0; i < EquipmentSlots.Length; i++)
        {
            if (enumEquipmentType == EquipmentSlots[i].EquipmentSlot)
            {
                EquipmentSlots[i].Item = null;
            }
        }
        /*
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
        */
        OnItemUnequipped?.Invoke();
       // _playerEquipmentUIManager.RemoveItem(enumEquipmentType);

    }
    private EquipmentSlotType GetEquipmentSlot(EnumEquipmentSlot enumEquipmentSlot)
    {
        for(int i = 0; i < EquipmentSlots.Length; i++)
        {
            if(EquipmentSlots[i].EquipmentSlot== enumEquipmentSlot)
            {
                return EquipmentSlots[i];
            }
        }
        return null;
    }
    
}
