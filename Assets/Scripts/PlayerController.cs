using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D _rigidbody;

    // Player speed variables
    public float speed;
    public float maxSpeed;

    // Timer to lose life if you dont click
    public float clickTime;
    private float _timer;

	// Use this for initialization
	void Start () {

        _rigidbody = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        // Game hasnt stopped
        if (Time.timeScale != 0.0f)
        {
            // Rotate the player based on mouse position
            RotatePlayer(transform);

            // Move the player based on speed and max speed
            MovePlayer(_rigidbody, speed, maxSpeed);

            // If the player is going out of bounds stop them from moving that direction
            if (transform.position.x > 8 || transform.position.x < -8)
            {
                _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
            }
            if (transform.position.y > 4 || transform.position.y < -3)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.0f);
            }

            // Add to the timer
            _timer += Time.deltaTime;
            
            if (_timer >= clickTime)
            {
                ResetTimer();
                Camera.main.GetComponent<ScoreController>().lives--;
                Camera.main.GetComponent<ScoreController>().UpdateLives();
            }

            float scaley = (((clickTime * 1.25f) - _timer) / 2.5f) * 3.0f;
            transform.localScale = new Vector3(transform.localScale.x, scaley, transform.localScale.z);
        }
    }

    // Rotate the player based on the mouse position
    void RotatePlayer(Transform playerTransform)
    {
        // Find a vector from the player to the mouse
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(playerTransform.position.x, playerTransform.position.y) - mousePosition;

        // Find rotation between up vector and player to mouse position vector
        float angle = Vector2.Angle(Vector2.up, mousePosition);

        // FIGURE OUT
        if (mousePosition.x < 0)
        {
            angle += 180;
        }
        else
        {
            angle *= -1;
        }

        playerTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    // Move the player based on keyboard input
    void MovePlayer(Rigidbody2D rigidbody, float playerSpeed, float playerMaxSpeed)
    {
        Vector2 newforce = new Vector2(0, 0);

        // Create vector in direction of input
        if (Input.GetKey(KeyCode.A))
        {
            newforce.x = -1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            newforce.y = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newforce.x = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newforce.y = -1;
        }

        newforce.Normalize();

        // Add force at player speed in direction 
        rigidbody.AddForce(newforce * playerSpeed);

        // Make sure the velocity is not greater than the set maximum
        rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, playerMaxSpeed);
    }

    // Reset life lost for no click timer to zero
    public void ResetTimer()
    {
        _timer = 0;
    }
}