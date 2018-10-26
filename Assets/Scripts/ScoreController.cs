using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour {

    // Text display of score
    public TextMesh scoreText;

    // Score and lives trackers
    public int score;
    public int lives;

    // Game over message
    public GameObject gameOverBox;

    // Heart display
    public GameObject[] hearts;

    // Use this for initialization
    void Start () {

        Time.timeScale = 1.0f;
        score = 0;
        lives = 3;
        gameOverBox.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        // Update score text
        scoreText.text = score.ToString();

        // If the player has ran out of lives
        if (lives <= 0)
        {
            UpdateLives();

            // Stop the game if it hasnt been done already
            if (Time.timeScale != 0.0f)
            {
                Time.timeScale = 0.0f;
            }

            // Show the game over message if it is not shown yet
            if (gameOverBox.activeSelf == false)
            {
                gameOverBox.SetActive(true);
            }

            // If the player hits R restart the game
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("start");
            }
        }

	}

    // Update display of hearts to match number of lives
    public void UpdateLives()
    {
        int heartsdisplayed = 0;

        // Count how many hearts are being displayed
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i].activeSelf == false)
            {
                heartsdisplayed++;
            }
        }

        // Number of hearts does not match number of lives
        if (heartsdisplayed != lives)
        {
            // Number of hearts is less than lives
            if (heartsdisplayed < lives)
            { 
                // Find the difference and change required number of hearts
                int diff = lives - heartsdisplayed;

                for (int i = 0; i < hearts.Length; i++)
                {
                    if (diff > 0 && hearts[i].activeSelf == true)
                    {
                        diff--;
                        hearts[i].SetActive(false);
                    }
                }
            }
            // Number of hearts is greater than lives
            else if (heartsdisplayed > lives)
            {
                // Get the difference and change required number of hearts
                int diff = heartsdisplayed - lives;

                for (int i = (hearts.Length - 1); i >= 0; i--)
                {
                    if (diff > 0 && hearts[i].activeSelf == false)
                    {
                        diff--;
                        hearts[i].SetActive(true);
                    }
                }
            }
        }
    }
}