using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentUIManager : MonoBehaviour
{
    [SerializeField] private ItemEquipableUiHolder[] _equipmentSlots;

    private IEnumerator Start()
    {
        _equipmentSlots =GetComponentsInChildren<ItemEquipableUiHolder>();
        PlayerEquipmentManager.OnItemEquipped += SetItem;
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
    public void SetItem(ItemEquipable itemEquipable)
    {
       
        for(int i=0;i< _equipmentSlots.Length; i++)
        {

            if (itemEquipable.EquipmentType == _equipmentSlots[i].EquipmentType)
            {
                _equipmentSlots[i].SetItem(itemEquipable);
                return;
            }
        }
    }
}
