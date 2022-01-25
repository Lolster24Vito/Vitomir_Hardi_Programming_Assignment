using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _equipmentUI;
    [SerializeField] GameObject _inventoryUI;
    [SerializeField] GameObject _attributesUI;
    [SerializeField] GameObject _equipmentButton;
    [SerializeField] GameObject _inventoryButton;
    [SerializeField] GameObject _attributesButton;

    private IEnumerator Start()
    {
        _canvas.enabled = false;
        _attributesUI.SetActive(false);
        yield return new WaitForEndOfFrame();
        _canvas.enabled = true;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventoryUI.SetActive(!_inventoryUI.activeInHierarchy);
            _inventoryButton.SetActive(!_inventoryUI.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _equipmentUI.SetActive(!_equipmentUI.activeInHierarchy);
            _equipmentButton.SetActive(!_equipmentUI.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _attributesUI.SetActive(!_attributesUI.activeInHierarchy);
            _attributesButton.SetActive(!_attributesButton.activeInHierarchy);
        }
    }

}
