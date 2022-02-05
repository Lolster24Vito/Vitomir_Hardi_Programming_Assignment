using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentUIManager : MonoBehaviour
{
    [SerializeField] private ItemEquipableUiHolder[] _equipmentSlots;

    private WindowPopUp windowPopUp;
    private void Start()
    {
        _equipmentSlots = GetComponentsInChildren<ItemEquipableUiHolder>(true);
        PlayerEquipmentManager.OnItemEquipped += SetItem;
        windowPopUp = GetComponent<WindowPopUp>();
        windowPopUp.onScreenHide += WindowPopUp_onScreenHide;

    }

    private void WindowPopUp_onScreenHide()
    {
        for(int i = 0; i < _equipmentSlots.Length; i++)
        {
            _equipmentSlots[i].DisableInAir();
        }
    }

    public void SetItem(ItemEquipable itemEquipable,EnumEquipmentSlot enumEquipmentSlot)
    {
       
        for(int i=0;i< _equipmentSlots.Length; i++)
        {

            if (enumEquipmentSlot == _equipmentSlots[i].EquipmentTypeSlot)
            {
                _equipmentSlots[i].SetItem(itemEquipable);
                return;
            }
        }
    }
    public void RemoveItem(EnumEquipmentSlot enumEquipmentType)
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {

            if (enumEquipmentType == _equipmentSlots[i].EquipmentTypeSlot)
            {
                _equipmentSlots[i].RemoveItem();
                return;
            }
        }
    }
}

//napravit zasebnu klasu koja sadrži 
//EnumEquipable i list<> EnumEquipableSlot koji ga podržava sve static 