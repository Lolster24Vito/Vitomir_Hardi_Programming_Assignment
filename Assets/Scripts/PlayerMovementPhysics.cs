using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPhysics : MonoBehaviour
{
    [SerializeField] private float _speed=4;
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Move(Vector2 input)
    {
        _rigidbody2D.MovePosition((Vector2)transform.position + (input * _speed * Time.fixedDeltaTime));
    }
}
