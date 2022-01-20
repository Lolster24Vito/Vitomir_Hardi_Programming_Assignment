using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.01f,1.5f)][SerializeField] private float _SmoothTime=0.2f;
    private Transform _player;
    private Vector2 _currentVelocity;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lerpedPosition = Vector2.SmoothDamp(transform.position, _player.position,ref _currentVelocity, _SmoothTime);
        transform.position = new Vector3(lerpedPosition.x, lerpedPosition.y, transform.position.z);
    }
}
