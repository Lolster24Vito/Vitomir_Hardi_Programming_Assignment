using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUIManager : MonoBehaviour
{
    [SerializeField] Text text;
    // Start is called before the first frame update

    private void Awake()
    {
        PlayerAttributeManager.OnAttributesChanged += UpdateUI;
    }
    private void UpdateUI(ItemAttributes itemAttributes,SpendableAttributes spendableAttribute)
    {
        text.text = spendableAttribute.ToString();
        text.text += itemAttributes.ToString();
    }
}
