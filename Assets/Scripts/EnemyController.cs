using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    // Speed enemy starts at
    public float speed;

	// Use this for initialization
	void Start ()
    {
        // Find the players position and set it as the target for this enemy
        GameObject _player = GameObject.FindGameObjectWithTag("player");
        Vector2 _target = _player.transform.position;

        // Set the velocity of this enemy to be in the direction of the target(player) at the set speed
        Rigidbody2D _rigidbody = GetComponent<Rigidbody2D>();
        Vector2 vel = _target - new Vector2(transform.position.x, transform.position.y);
        vel.Normalize();
        _rigidbody.velocity = vel * speed;
    }

    void Update()
    {
        // Check if enemy should be destroyed for being out of bounds
        if (CheckIfOutOfBounds(11, 9))
        {
            Destroy(gameObject);
        }
    }

    // Check bounds
    bool CheckIfOutOfBounds(float xBounds, float yBounds)
    {
        if (transform.position.x < -xBounds)
        {
            return true;
        }
        else if (transform.position.x > xBounds)
        {
            return true;
        }
        else if (transform.position.y > yBounds)
        {
            return true;
        }
        else if (transform.position.y < -yBounds)
        {
            return true;
        }

        return false;
    }
}
