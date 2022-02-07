using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.01f,1.5f)][SerializeField] private float _SmoothTime=0.2f;
    private Transform _player;
    private Transform _focusTransform;
    private Vector2 _currentVelocity;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _focusTransform = _player;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lerpedPosition = Vector2.SmoothDamp(transform.position, _focusTransform.position,ref _currentVelocity, _SmoothTime);
        transform.position = new Vector3(lerpedPosition.x, lerpedPosition.y, transform.position.z);
    }
    
    public void FocusOnObject(Transform focusTransform)
    {
        _focusTransform = focusTransform;
    }
    public void ReturnFocusToPlayerInTime(float seconds)
    {
        StartCoroutine(ReturnFocusToPlayer(seconds));
    }
    IEnumerator ReturnFocusToPlayer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _focusTransform = _player;
    }
}
