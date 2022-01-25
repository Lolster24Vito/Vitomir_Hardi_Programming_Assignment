using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipableUiHolder : MonoBehaviour
{
    [SerializeField] private EnumEquipmentType _equipmentType;
    public EnumEquipmentType EquipmentType { get => _equipmentType; }
    [SerializeField] private ItemEquipable _item;
    Image _image;
    private void Start()
    {
        _image = GetComponent<Image>();
    }
    public void SetItem(ItemEquipable item)
    {
        _item = item;
        _image.sprite = item.Icon;
    }
    public void RemoveItem()
    {
        PlayerEquipmentManager.Instance.RemoveItem(EquipmentType);
        _item = null;
        _image.sprite = null;
    }
    public Item GetItem()
    {
        return (Item)_item;
    }

}
