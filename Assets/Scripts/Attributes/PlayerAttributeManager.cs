using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributeManager : MonoBehaviour
{
    private PlayerEquipmentManager _equipmentManager;

    private SpendableAttributes _spendableAttributes = new SpendableAttributes();
    public SpendableAttributes spendableAttributes { get => _spendableAttributes; set => _spendableAttributes = value; }

    public ItemAttributes CharecterAttributes { get; set; }

    public static event Action<ItemAttributes,SpendableAttributes> OnAttributesChanged;

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
        PlayerBuffManager.OnBuffChangeAttributes += UpdateValues;
        UpdateValues();
    }

    private void UpdateValues()
    {
        ItemAttributes attribute=new ItemAttributes();

        for(int i = 0; i < _equipmentManager.EquipmentSlots.Length;i++)
        {
            if (_equipmentManager.EquipmentSlots[i].Item != null)
            {
                attribute += _equipmentManager.EquipmentSlots[i].Item.Item.Attributes;
            }
        }
        /*
        if (_equipmentManager.Head != null)
        {
            attribute += _equipmentManager.Head.Item.Attributes;
        }
        if (_equipmentManager.Torso != null)
        {
            attribute += _equipmentManager.Torso.Item.Attributes;

        }
        if (_equipmentManager.Weapon != null)
        {
            attribute += _equipmentManager.Weapon.Item.Attributes;

        }
        if (_equipmentManager.Shield != null) {
            attribute += _equipmentManager.Shield.Item.Attributes;
        }
        if (_equipmentManager.LeftRing != null)
        {
            attribute += _equipmentManager.LeftRing.Item.Attributes;
        }
        if (_equipmentManager.RightRing != null)
        {
            attribute += _equipmentManager.RightRing.Item.Attributes;
        }
        */
        attribute += PlayerBuffManager.Instance.PlayerBuffAttributes;
        CharecterAttributes = attribute;

        OnAttributesChanged?.Invoke(CharecterAttributes, spendableAttributes);
    }


    // Update is called once per frame

}
