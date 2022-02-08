using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

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
                    Analytics.CustomEvent("Item Equipped", new Dictionary<string, object>
                {
                    {"Item name",item.Name},
                    {"Item slot",EquipmentSlots[i].EquipmentSlot }
                });
                }
            }
        
        else
        {
            string name = item.EquipmentType.ToString();
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
            Analytics.CustomEvent("Item Equipped", new Dictionary<string, object>
                {
                    {"Item name",item.Name},
                    {"Item slot",ItemSlotToChange }
                });

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
