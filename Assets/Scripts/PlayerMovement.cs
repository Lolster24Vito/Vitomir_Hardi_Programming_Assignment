using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //had to add _normalEventSystem because the extension for touch gestures for some reason bugged out this:
    //!EventSystem.current.IsPointerOverGameObject(fingerID)

    private Vector2 _input;
    // Start is called before the first frame update
    private PlayerMovementPhysics _playerMovementPhysics;
    private PlayerMovementAnimationManager _playerMovementAnimationManager;    // Update is called once per frame
    private bool _isMoving = true;
    private Camera camera;
    private void Start()
    {
        _playerMovementPhysics=GetComponent<PlayerMovementPhysics>();
        _playerMovementAnimationManager=GetComponent<PlayerMovementAnimationManager>();
        camera = Camera.main;
    }
    void Update()
    {

        if (_isMoving)
        {
            if(!IsUsingMobile.IsMobile)
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            else
            {
                if (Input.touchCount>0)
                {
                    Touch touch = Input.GetTouch(0);
                    int fingerID = touch.fingerId;
                    if(touch.phase == TouchPhase.Began)
                    {
                         if (!IsUsingMobile.Instance.NormalEventSystem.IsPointerOverGameObject(fingerID))
                        {
                            Vector3 touchWorldPosition = camera.ScreenToWorldPoint(touch.position);
                            _playerMovementPhysics.MoveToPoint(touchWorldPosition);
                        }

                    }
                    

                }
                

            }
        }
        if(!IsUsingMobile.IsMobile)
        _playerMovementAnimationManager.Move(_input);


    }
    private void FixedUpdate()
    {
        if(!IsUsingMobile.IsMobile)
        _playerMovementPhysics.Move(_input);
       
    }
    private void StopMoving()
    {
        _isMoving = false;
        _input = Vector2.zero;
        _playerMovementPhysics.Stop();
    }
    private void StartMoving()
    {
        _isMoving = true;
    }
    public void StopMovementForAmount(float seconds)
    {
        StartCoroutine(StopMovementForAmountIEnumerator(seconds));
    }

    IEnumerator StopMovementForAmountIEnumerator(float seconds)
    {
        StopMoving();
        yield return new WaitForSeconds(seconds);
        StartMoving();
    }

    private float _timeout = 0.0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Time.time> _timeout)
        {
            Analytics.CustomEvent("On Player Collided");
            _timeout = Time.time+5;

        }
    }



}
