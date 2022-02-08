using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

public class CameraZoomOnPinch : MonoBehaviour
{
     TransformGesture _pinchGesture;
    private Camera _camera;
    [SerializeField] private float _maxCameraSize;
    [SerializeField]private float _minimumCameraSize;
    [SerializeField] private float _zoomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _pinchGesture = GetComponent<TransformGesture>();
        _pinchGesture.Transformed += _pinchGesture_Transformed;
    }

    private void _pinchGesture_Transformed(object sender, System.EventArgs e)
    {
        float value = _camera.orthographicSize + (-(_pinchGesture.DeltaScale - 1) * _zoomSpeed);
        _camera.orthographicSize = Mathf.Clamp(value, _minimumCameraSize, _maxCameraSize);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
