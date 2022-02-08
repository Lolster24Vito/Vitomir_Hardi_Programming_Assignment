using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPhysics : MonoBehaviour
{
    [SerializeField] private float _speed=4;
    private Rigidbody2D _rigidbody2D;

    public static event Action<float> OnMoved;
    private Vector3 _touchWorldPosition;
    private bool _isMovingToPoint = false;
    private PlayerMovementAnimationManager _playerMovementAnimation;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerMovementAnimation = GetComponent<PlayerMovementAnimationManager>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (_isMovingToPoint)
        {

            Vector2 direction = (_touchWorldPosition - transform.position).normalized;
           
            _playerMovementAnimation.Move(direction);
            Vector2 moveAmount = Vector2.MoveTowards((Vector2)transform.position, _touchWorldPosition, (_speed * Time.fixedDeltaTime));
            _rigidbody2D.MovePosition(moveAmount);

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, direction, 2f);
            if (raycastHit2D.collider != null&&!raycastHit2D.collider.isTrigger)
            {
                Stop();
            }
            if (Vector3.Distance(transform.position, _touchWorldPosition) < 0.5f)
            {
                Stop();
            }
        }
    }
    public void MoveToPoint(Vector3 point)
    {
        _touchWorldPosition = point;
        _isMovingToPoint = true;
    }
    public void Move(Vector2 input)
    {
        if (input != Vector2.zero)
        {
            OnMoved?.Invoke(input.magnitude*_speed*Time.fixedDeltaTime);
        }

        _rigidbody2D.MovePosition((Vector2)transform.position + (input * _speed * Time.fixedDeltaTime));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isMovingToPoint = false;
        _playerMovementAnimation.Move(Vector2.zero);
    }

    internal void Stop()
    {
        _isMovingToPoint = false;
        _playerMovementAnimation.Move(Vector2.zero);
    }
}
