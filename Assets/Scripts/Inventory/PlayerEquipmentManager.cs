using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    private static PlayerEquipmentManager _instance;

    public static PlayerEquipmentManager Instance { get { return _instance; } }

    public static event Action<ItemEquipable> OnItemEquipped;
    public static event Action itemEquipped;
    public static event Action OnItemUnequipped;

    [SerializeField] private PlayerEquipmentUIManager _playerEquipmentUIManager;

    private ItemEquipable _head;
    public ItemEquipable Head { get => _head; }
    private ItemEquipable _torso;
    public ItemEquipable Torso { get => _torso; }
    private ItemEquipable _weapon;
    public ItemEquipable Weapon { get => _weapon; }
    private ItemEquipable _shield;
    public ItemEquipable Shield { get => _shield; }

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

        switch (item.EquipmentType)
        {
            case EnumEquipmentType.Torso:
                if(_torso!=null)
                swappedItem = _torso;
                _torso = item;
                break;
            case EnumEquipmentType.Head:
                if(_head!=null)
                swappedItem = _head;
                _head = item;
                break;
            case EnumEquipmentType.Weapon:
                if(_weapon!=null)
                swappedItem = _weapon;
                _weapon = item;
                break;
            case EnumEquipmentType.Shield:
                if(_shield!=null)
                swappedItem = _shield;
                _shield = item;
                break;
            default:
                break;
        }
        OnItemEquipped?.Invoke(item);
        itemEquipped?.Invoke();
        if (swappedItem != null)
        {
            PlayerInventoryManager.Instance.AddItem(swappedItem, 1);//send item to inventory
        }
        //send item to equipmentUI---if already taken swap item and add to inventory


    }
    public void RemoveItem(EnumEquipmentType enumEquipmentType) 
    {


     switch (enumEquipmentType)
        {
            case EnumEquipmentType.Torso:
                _torso = null;
                break;
            case EnumEquipmentType.Head:
                _head = null;

                break;
            case EnumEquipmentType.Weapon:
                _weapon = null;

                break;
            case EnumEquipmentType.Shield:
                _shield = null;

                break;
            default:
                break;
        }

        OnItemUnequipped?.Invoke();

    }
}
