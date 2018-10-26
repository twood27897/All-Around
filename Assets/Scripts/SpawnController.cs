using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    // Prefabs to spawn for enemies and friends
    public GameObject enemy;
    public GameObject friend;

    // Spawnrate in seconds
    public float spawnTime;

    // The timer 
    private float _timer;

	// Use this for initialization
	void Start () {
        _timer = 0;
	}
	
	// Update is called once per frame
	void Update () {

        // Add to the timer
        _timer += Time.deltaTime;

        // Check whether its time to spawn, increase the spawn rate and spawn a new body
        if (_timer > spawnTime)
        {
            // Increase spawn rate
            IncreaseSpawnRate();

            // Spawn body
            SpawnRandom();

            // Reset timer to 0
            _timer = 0;
        }

	}

    // Increase the spawn rate
    void IncreaseSpawnRate()
    {
        // Increase the spawn rate (by decreasing the spawn cooldown) till it hits one new body every 0.2 seconds
        if (spawnTime > 0.2f)
        {
            spawnTime -= 0.025f;
        }
    }

    void SpawnRandom()
    {

        // Randomly decide what side to spawn on
        int spawnSide = Random.Range(0, 4);

        // To store position to spawn in
        Vector3 spawnPosition = Vector3.zero;

        // Set spawn position based on spawn side
        switch (spawnSide)
        {
            case 0:
                spawnPosition = new Vector3(-10, Random.Range(-6, 6), 0);
                break;
            case 1:
                spawnPosition = new Vector3(10, Random.Range(-6, 6), 0);
                break;
            case 2:
                spawnPosition = new Vector3(Random.Range(-10, 10), 6, 0);
                break;
            case 3:
                spawnPosition = new Vector3(Random.Range(-10, 10), -6, 0);
                break;
        }

        // Randomly decide what to spawn
        int spawnType = Random.Range(0, 4);

        // Based on spawn type spawn a friend or enemy (1 : 3 chance)
        if (spawnType == 0 || spawnType == 1 || spawnType == 2)
        {
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
        else if (spawnType == 3)
        {
            Instantiate(friend, spawnPosition, Quaternion.identity);
        }

    }
}
