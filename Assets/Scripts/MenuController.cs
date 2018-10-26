using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Selection marker arrows
    public GameObject leftArrow;
    public GameObject rightArrow;

    [Tooltip("How fast the selection arrows move back and forth")]
    public float arrowBobSpeed;

    // Screen to change the palette of
    public GameObject screen;

    // Switch for arrow direction
    private bool _arrowMove;

    // Current menu choice
    private int _choice;


    // Use this for initialization
    void Start()
    {
        // Make sure game time is running
        Time.timeScale = 1.0f;

        // Initialise variables for menu
        _choice = 0;
        _arrowMove = false;

        // Reset palette
        PlayerPrefs.SetFloat("palette", 1.0f);
        PlayerPrefs.Save();
        float pal = PlayerPrefs.GetFloat("palette");
        screen.GetComponent<Renderer>().material.SetFloat("_PalettePicker", pal);
    }

    // Update is called once per frame
    void Update()
    {
        // Bob arrows in and out
        ArrowBob(arrowBobSpeed);

        // Move down in menu if player hits S
        if (Input.GetKeyDown(KeyCode.S))
        {
            MenuDown();
        }

        // Move up in menu if player hits W
        if (Input.GetKeyDown(KeyCode.W))
        {
            MenuUp();
        }

        // Select current menu item if player hits Space or Enter
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            MenuSelect();
        }
    }

    // Check where we are in menu and move down
    void MenuDown()
    {
        if (_choice == 0)
        {
            _choice = 1;
            MoveArrows(-1.85f);
        }
        else if (_choice == 1)
        {
            _choice = 2;
            MoveArrows(-1.9f);
        }
        else if (_choice == 2)
        {
            _choice = 0;
            MoveArrows(3.75f);
        }
    }

    // Check where we are in menu and move up
    void MenuUp()
    {
        if (_choice == 0)
        {
            _choice = 2;
            MoveArrows(-3.75f);
        }
        else if (_choice == 1)
        {
            _choice = 0;
            MoveArrows(1.85f);
        }
        else if (_choice == 2)
        {
            _choice = 1;
            MoveArrows(1.9f);
        }
    }

    // Check where we are in menu and execute appropriate action\
    void MenuSelect()
    {
        if (_choice == 0)
        {
            SceneManager.LoadScene("game");
        }
        else if (_choice == 1)
        {
            ChangePalette();
        }
        else if (_choice == 2)
        {
            Application.Quit();
        }
    }

    // Move arrows by move distance in the y axis
    void MoveArrows(float moveDistance)
    {
        leftArrow.transform.position = new Vector3(leftArrow.transform.position.x, leftArrow.transform.position.y + moveDistance, leftArrow.transform.position.z);
        rightArrow.transform.position = new Vector3(rightArrow.transform.position.x, rightArrow.transform.position.y + moveDistance, rightArrow.transform.position.z);
    }

    // Change to the next palette
    void ChangePalette()
    {
        // Get current palette choice and change to the next in the list
        float pal = PlayerPrefs.GetFloat("palette");
        if (pal == 1.0f)
        {
            pal = 0.5f;
        }
        else if (pal == 0.5f)
        {
            pal = 0.0f;
        }
        else if (pal == 0.0f)
        {
            pal = 1.0f;
        }

        // Save the new palette choice
        PlayerPrefs.SetFloat("palette", pal);
        PlayerPrefs.Save();

        // Set the palette material to know the palette choice
        screen.GetComponent<Renderer>().material.SetFloat("_PalettePicker", pal);
    }

    // Move arrows in and out
    void ArrowBob(float bobSpeed)
    {
        // Bob left arrow
        if (leftArrow)
        {
            if (leftArrow.transform.position.x < -4.0f && !_arrowMove)
            {
                leftArrow.transform.position = Vector3.Lerp(leftArrow.transform.position, new Vector3(-3.0f, leftArrow.transform.position.y, leftArrow.transform.position.z), bobSpeed * Time.deltaTime);
            }
            else if (leftArrow.transform.position.x > -5.0f && _arrowMove)
            {
                leftArrow.transform.position = Vector3.Lerp(leftArrow.transform.position, new Vector3(-6.0f, leftArrow.transform.position.y, leftArrow.transform.position.z), bobSpeed * Time.deltaTime);
            }
        }

        // Bob right arrow and switch direction when needed
        if (rightArrow)
        {
            if (rightArrow.transform.position.x > 4.0f && !_arrowMove)
            {
                rightArrow.transform.position = Vector3.Lerp(rightArrow.transform.position, new Vector3(3.0f, rightArrow.transform.position.y, rightArrow.transform.position.z), bobSpeed * Time.deltaTime);
            }
            else if (rightArrow.transform.position.x < 5.0f && _arrowMove)
            {
                rightArrow.transform.position = Vector3.Lerp(rightArrow.transform.position, new Vector3(6.0f, rightArrow.transform.position.y, rightArrow.transform.position.z), bobSpeed * Time.deltaTime);
            }
            else
            {
                _arrowMove = !_arrowMove;
            }
        }
    }
}
