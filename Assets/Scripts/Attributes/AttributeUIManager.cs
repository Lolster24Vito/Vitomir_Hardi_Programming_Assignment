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
    private void UpdateUI(ItemAttributes itemAttributes)
    {
        text.text = itemAttributes.ToString();
    }
}
