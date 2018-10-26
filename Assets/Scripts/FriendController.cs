using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour {

    // Speed this friend will start at
    public float speed;

	// Use this for initialization
	void Start () {

        // Set the target of the friend to a random point on screen
        Vector2 _target = new Vector2(Random.Range(-7, 7), Random.Range(-3, 3));

        // Set the velocity towards the target at the set speed
        Rigidbody2D _rigidbody = GetComponent<Rigidbody2D>();
        Vector2 vel = _target - new Vector2(transform.position.x, transform.position.y);
        vel.Normalize();
        _rigidbody.velocity = vel * speed;
    }
	
	// Update is called once per frame
	void Update () {

        // Check if friend should be destroyed for being out of bounds
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
