using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributeManager : MonoBehaviour
{
    private PlayerEquipmentManager _equipmentManager;
    public ItemAttributes CharecterAttributes { get; set; }

    public static event Action<ItemAttributes> OnAttributesChanged;

    /*
        [SerializeField] Text _agilityTextNumber;
        [SerializeField] Text _dexterityTextNumber;
        [SerializeField] Text _inteligenceTextNumber;
        [SerializeField] Text _strengthTextNumber;
    */
    // Start is called before the first frame update
    void Start()
    {
        _equipmentManager = GetComponent<PlayerEquipmentManager>();
        PlayerEquipmentManager.OnItemUnequipped += UpdateValues;
        PlayerEquipmentManager.itemEquipped += UpdateValues;
        UpdateValues();
    }

    private void UpdateValues()
    {
        ItemAttributes attribute=new ItemAttributes();

        if (_equipmentManager.Head != null)
        {
            attribute += _equipmentManager.Head.Attributes;
        }
        if (_equipmentManager.Torso != null)
        {
            attribute += _equipmentManager.Torso.Attributes; 

        }
        if (_equipmentManager.Weapon != null)
        {
            attribute += _equipmentManager.Weapon.Attributes;

        }
        if (_equipmentManager.Shield != null) {
            attribute += _equipmentManager.Shield.Attributes;
        }
        if (_equipmentManager.LeftRing != null)
        {
            attribute += _equipmentManager.LeftRing.Attributes;
        }
        if (_equipmentManager.RightRing != null)
        {
            attribute += _equipmentManager.RightRing.Attributes;
        }
        CharecterAttributes = attribute;
        OnAttributesChanged?.Invoke(attribute);
    }


    // Update is called once per frame

}
