using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnimationManager : MonoBehaviour
{
    private Vector2 _lastInput;
    private Animator _animator;
    private bool _walking;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
   public void Move(Vector2 input)
    {
        if (input == Vector2.zero)
        {
            _walking = false;
            _animator.SetBool("Walking", _walking);
        }
        else
        {
            _walking = true;
            _animator.SetBool("Walking", _walking);
        }

        if (_lastInput != input && _walking)
        {
            _lastInput = input;
            _animator.SetFloat("DirectionHorizontal", _lastInput.x);
            _animator.SetFloat("DirectionVertical", _lastInput.y);

        }
    }
}
