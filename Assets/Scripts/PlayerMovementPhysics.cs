using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPhysics : MonoBehaviour
{
    [SerializeField] private float _speed=4;
    private Rigidbody2D _rigidbody2D;

    public static event Action<float> OnMoved;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Move(Vector2 input)
    {
        if (input != Vector2.zero)
        {
            OnMoved?.Invoke(input.magnitude*_speed*Time.fixedDeltaTime);
        }

        //Debug.Log(input.magnitude* _speed*Time.fixedDeltaTime);
        _rigidbody2D.MovePosition((Vector2)transform.position + (input * _speed * Time.fixedDeltaTime));
    }
}
