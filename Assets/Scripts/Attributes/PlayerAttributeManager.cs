using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributeManager : MonoBehaviour
{
    private PlayerEquipmentManager _equipmentManager;
    public ItemAttributes CharecterAttributes { get; set; }

    [SerializeField] Text _agilityTextNumber;
    [SerializeField] Text _dexterityTextNumber;
    [SerializeField] Text _inteligenceTextNumber;
    [SerializeField] Text _strengthTextNumber;
    // Start is called before the first frame update
    void Start()
    {
        _equipmentManager = GetComponent<PlayerEquipmentManager>();
        PlayerEquipmentManager.OnItemUnequipped += UpdateValues;
        PlayerEquipmentManager.itemEquipped += UpdateValues;
    }

    private void UpdateValues()
    {
        int agility=0;
        int dexterity=0;
        int inteligence=0;
        int strength=0;
        if (_equipmentManager.Head != null)
        {
        ItemAttributes attributesHead = _equipmentManager.Head.Attributes;
            agility += attributesHead.Agility;
            dexterity += attributesHead.Dexterity;
            inteligence += attributesHead.Inteligence;
            strength += attributesHead.Strength;
        }
        if (_equipmentManager.Torso != null)
        {
        ItemAttributes attributesTorso = _equipmentManager.Torso.Attributes;
            agility += attributesTorso.Agility;
            dexterity += attributesTorso.Dexterity;
            inteligence += attributesTorso.Inteligence;
            strength += attributesTorso.Strength;
        }
        if (_equipmentManager.Weapon != null)
        {
        ItemAttributes attributesWeapon = _equipmentManager.Weapon.Attributes;
            agility += attributesWeapon.Agility;
            dexterity += attributesWeapon.Dexterity;
            inteligence += attributesWeapon.Inteligence;
            strength += attributesWeapon.Strength;
        }
        if (_equipmentManager.Shield != null) {
            ItemAttributes attributesShield = _equipmentManager.Shield.Attributes;
            agility += attributesShield.Agility;
            dexterity += attributesShield.Dexterity;
            inteligence += attributesShield.Inteligence;
            strength += attributesShield.Strength;
        }
        CharecterAttributes = new ItemAttributes
        {
            Agility = agility,
            Dexterity = dexterity,
            Inteligence = inteligence,
            Strength = strength,
        };
        UpdatePlayerAttributesUI(CharecterAttributes);
    }
    public void UpdatePlayerAttributesUI(ItemAttributes itemAttributes)
    {
        _agilityTextNumber.text = itemAttributes.Agility.ToString();
        _dexterityTextNumber.text = itemAttributes.Dexterity.ToString();
        _inteligenceTextNumber.text = itemAttributes.Inteligence.ToString();
        _strengthTextNumber.text = itemAttributes.Strength.ToString();
    }

    // Update is called once per frame

}
