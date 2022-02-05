using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _parentToHide;

    private static ToolTipManager _instance;

    public static ToolTipManager Instance { get { return _instance; } }

    private void Awake()
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
    public void ShowToolTip(Item item)
    {
        _parentToHide.SetActive(true);

        _text.text = item.ToString();
        _icon.sprite = item.Icon;
        transform.position = Input.mousePosition;
    }
    public void HideToolTip()
    {
        _parentToHide.SetActive(false);
    }

}
