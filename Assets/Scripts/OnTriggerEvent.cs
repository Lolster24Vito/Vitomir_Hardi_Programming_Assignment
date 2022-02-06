using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _onTriggerEnterEvents;
    [SerializeField] UnityEvent _onTriggerStayEvents;
    [SerializeField] UnityEvent _onTriggerExitEvents;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
        _onTriggerEnterEvents.Invoke();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _onTriggerExitEvents.Invoke();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _onTriggerExitEvents.Invoke();
        }
    }



}
