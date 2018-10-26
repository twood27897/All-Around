using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    
    // How much the camera will move during a shake
    public float startingShakingPower;

    // How long the camera will shake for
    public float shakeDuration;
    
    private float _currentShakeAmount;
    private float _currentShakeTimer;
    private Vector3 _defaultpos;

    // Use this for initialization
    void Start () {
        _currentShakeTimer = -1;
        _defaultpos = transform.position;
	}

	// Update is called once per frame
	void Update () {

        // If the game is running
        if (Time.timeScale != 0.0f)
        {
            ShakeCameraOnMouseClick();
        }
        else if (transform.position != _defaultpos)
        {
            StopAllCoroutines();
            transform.position = _defaultpos;
        }
    }

    // Shake camera on mouse click
    void ShakeCameraOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ShakeCamera(startingShakingPower, shakeDuration));
        }
    }

    // Shake camera
    IEnumerator ShakeCamera(float shakePwr, float shakeDur)
    {
        // Set the initial values which will be slowly brought to zero over the duration of the camera shake
        _currentShakeAmount = shakePwr;
        _currentShakeTimer = shakeDur;

        // Check if there is still time left on the current shake
        while (_currentShakeTimer >= 0)
        {
            // Randomly generate offset for camera position by shake amount
            Vector2 shakePosOffset = Random.insideUnitCircle * _currentShakeAmount;

            // Reset the camera position from the previous shake
            transform.position = _defaultpos;

            // Offset the camera by the random 
            transform.position = new Vector3(transform.position.x + shakePosOffset.x, transform.position.y + shakePosOffset.y, transform.position.z);

            // Take away time from the current shake
            _currentShakeTimer -= Time.deltaTime;

            // Wait till next frame
            yield return null;
        }

        // Reset camera 
        transform.position = _defaultpos;
    }
}
