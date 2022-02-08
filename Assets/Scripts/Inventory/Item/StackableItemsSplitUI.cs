using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StackableItemsSplitUI : MonoBehaviour
{
    private static StackableItemsSplitUI _instance;

    public static StackableItemsSplitUI Instance { get { return _instance; } }

    [SerializeField]private GameObject _screen;
    [SerializeField]private Slider _slider;
    [SerializeField] private InputField _inputField;
    private ItemUiHolder _itemUiHolder;

    private int _maxValue;
    private int _currentNumber;
    private RectTransform _rectTransform;

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
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {

                HideUI();

            }
        }
        
    }
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _slider.onValueChanged.AddListener(OnSliderChange);
        _inputField.onEndEdit.AddListener(OnEndEdit);
    }

    public void ShowUI(ItemUiHolder itemUiHolder)
    {
        _itemUiHolder = itemUiHolder;

        transform.position = itemUiHolder.transform.position;
        //set up my values to the inputs.
        _screen.SetActive(true);
         _maxValue = itemUiHolder.GetAmount();
        _slider.maxValue = _maxValue;
        _slider.value = _maxValue;
        _inputField.text = _maxValue.ToString();
    }
    public void HideUI()
    {
        _screen.SetActive(false);
    }
    private void OnSliderChange(float number)
    {
        _currentNumber = (int)number;
        _inputField.text = _currentNumber.ToString();
    }
    private void OnEndEdit(string text)
    {
        if(int.TryParse(text,out int number))
        {
            _currentNumber = Mathf.Clamp(number, 1, _maxValue);
        }
        else
        {
            _currentNumber = 1;
        }
            _inputField.text = _currentNumber.ToString();
            _slider.value = _currentNumber;
        
    }
    public void SplitItem()
    {
        
        int diference = _maxValue - _currentNumber;

        if (diference > 0)
        {
            _itemUiHolder.SetAmount(_currentNumber);
            PlayerInventoryManager.Instance.AddItem(_itemUiHolder.GetItem(), diference);
        }
    }
    public void DecreaseAmount()
    {
        _currentNumber = Mathf.Clamp(_currentNumber - 1, 1, _maxValue);
        UpdateUI();
    }
    public void IncreaseAmount()
    {
        _currentNumber = Mathf.Clamp(_currentNumber+1, 1, _maxValue);
        UpdateUI();
    }
    private void UpdateUI()
    {
        _slider.value = _currentNumber;
        _inputField.text = _currentNumber.ToString();
    }



}
