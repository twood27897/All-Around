using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour {
    
    // Player object
    public GameObject player;

    // Score counter and controller
    private ScoreController _scoreController;

	// Use this for initialization
	void Start () {

        _scoreController = FindObjectOfType<ScoreController>();

	}

    // Update is called once per frame
    void Update()
    {
        // If the game is not stopped
        if (Time.timeScale != 0.0f)
        {
            // On mouse click
            if (Input.GetMouseButtonDown(0))
            {
                // Get the mouse position in world space
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);

                // Vector between mouse position and player, normalized
                Vector2 playerToMouseDirection =  mousePosition - playerPosition;
                playerToMouseDirection.Normalize();

                // Multiply the vector and add the origin (player position) to get a point far enough out that when raycast from it will hit everything along the line
                Vector2 rayOrigin = playerPosition + (playerToMouseDirection * 20);

                // Find the direction from the new origin point to the player
                Vector2 rayDirection = playerPosition - rayOrigin;

                // Raycast from new point back in the direction of the player
                RaycastHit2D[] raycastHits = Physics2D.RaycastAll(rayOrigin, rayDirection, 100);


                // For every hit object
                for (int j = 0; j < raycastHits.Length; j++)
                {
                    // Get the collider of the object and check if it is in bounds
                    Collider2D col = raycastHits[j].collider;

                    if (col.gameObject.transform.position.x < 9 && col.gameObject.transform.position.x > -9 && col.gameObject.transform.position.y < 5 && col.gameObject.transform.position.y > -3.5f)
                    {
                        // Check tag of object and resolve
                        if (col.tag == "friend")
                        {
                            Destroy(col.gameObject);
                            _scoreController.lives -= 1;
                            _scoreController.UpdateLives();
                        }
                        else if (col.tag == "enemy")
                        {
                            Destroy(col.gameObject);
                            _scoreController.score += 100;
                        }
                    }
                }

                // Reset timer on player for losing life
                player.GetComponent<PlayerController>().ResetTimer();
            }
        }
    }
}