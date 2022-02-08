using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.01f,1.5f)][SerializeField] private float _SmoothTime=0.2f;
    private Transform _player;
    private Vector2 _currentVelocity;
    private Transform _focusTransform;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
<<<<<<< HEAD:Assets/Scripts/Camera/CameraFollow.cs
        _focusTransform = _player;

=======
>>>>>>> parent of 5f47e54 (Merge branch 'UpdatePlayerCamera' into production):Assets/Scripts/CameraFollow.cs
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD:Assets/Scripts/Camera/CameraFollow.cs
        Vector2 lerpedPosition = Vector2.SmoothDamp(transform.position, _focusTransform.position, ref _currentVelocity, _SmoothTime);
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
=======
        Vector2 lerpedPosition = Vector2.SmoothDamp(transform.position, _player.position,ref _currentVelocity, _SmoothTime);
        transform.position = new Vector3(lerpedPosition.x, lerpedPosition.y, transform.position.z);
    }
>>>>>>> parent of 5f47e54 (Merge branch 'UpdatePlayerCamera' into production):Assets/Scripts/CameraFollow.cs
}
