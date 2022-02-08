using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class WindowPopUp : MonoBehaviour
{
    
    [SerializeField]private KeyCode _keyShowHide;
    [SerializeField] private GameObject _screen;
    [SerializeField] private GameObject _button;
    // Start is called before the first frame update

    private bool _showingScreen = false;

    public event Action onScreenHide;

    // Update is called once per frame
    private void Start()
    {
        _screen.SetActive(_showingScreen);
        _button.SetActive(!_showingScreen);
    }
    void Update()
    {

        if (Input.GetKeyDown(_keyShowHide))
        {
            ToggleShowHide();
        }
    }

    public void ToggleShowHide()
    {
        if (!_showingScreen)
        {
            onScreenHide?.Invoke();
        }
        else
        {
            Analytics.CustomEvent("Window Opened", new Dictionary<string, object>
                {
                    {"Window Type",gameObject.name},
                });
        }
        _screen.SetActive(_showingScreen);
        _button.SetActive(!_showingScreen);
        _showingScreen = !_showingScreen;
    }
}
