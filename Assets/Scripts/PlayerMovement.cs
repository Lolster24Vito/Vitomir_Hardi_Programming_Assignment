using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _input;
    // Start is called before the first frame update
    private PlayerMovementPhysics _playerMovementPhysics;
    private PlayerMovementAnimationManager _playerMovementAnimationManager;    // Update is called once per frame
    private bool isMoving = true;
    private void Start()
    {
        _playerMovementPhysics=GetComponent<PlayerMovementPhysics>();
        _playerMovementAnimationManager=GetComponent<PlayerMovementAnimationManager>();
    }
    void Update()
    {
        if(isMoving)
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _playerMovementAnimationManager.Move(_input);
    }
    private void FixedUpdate()
    {
        _playerMovementPhysics.Move(_input);
    }
    private void StopMoving()
    {
        isMoving = false;
        _input = Vector2.zero;
    }
    private void StartMoving()
    {
        isMoving = true;
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


}
