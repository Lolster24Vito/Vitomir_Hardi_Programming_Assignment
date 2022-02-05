using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _input;
    // Start is called before the first frame update
    private PlayerMovementPhysics _playerMovementPhysics;
    private PlayerMovementAnimationManager _playerMovementAnimationManager;    // Update is called once per frame
    private void Start()
    {
        _playerMovementPhysics=GetComponent<PlayerMovementPhysics>();
        _playerMovementAnimationManager=GetComponent<PlayerMovementAnimationManager>();
    }
    void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _playerMovementAnimationManager.Move(_input);
    }
    private void FixedUpdate()
    {
        _playerMovementPhysics.Move(_input);
    }

}
