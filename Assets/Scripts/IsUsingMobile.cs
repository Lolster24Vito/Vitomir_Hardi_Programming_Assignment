using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsUsingMobile : MonoBehaviour
{
    [SerializeField] private bool _isMobile;
    private static IsUsingMobile _instance;

    public static IsUsingMobile Instance { get { return _instance; } }
    [SerializeField] public EventSystem NormalEventSystem;
    public static bool IsMobile;
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
        IsMobile = _isMobile;
    }

}
