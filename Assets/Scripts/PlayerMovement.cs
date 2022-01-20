using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _speed=4;
    private Vector2 _input;
    private Vector2 _lastInput;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _walking;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (_input == Vector2.zero)
        {
            _walking = false;
            _animator.SetBool("Walking", _walking);
        }
        else { 
            _walking = true;
            _animator.SetBool("Walking", _walking);
        }

        if (_lastInput != _input&&_walking)
        {
            _lastInput = _input;
            _animator.SetFloat("DirectionHorizontal", _lastInput.x);
            _animator.SetFloat("DirectionVertical", _lastInput.y);

        }
    }
    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition((Vector2)transform.position+ (_input* _speed*Time.fixedDeltaTime));
    }
}
