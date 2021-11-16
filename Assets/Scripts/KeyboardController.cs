using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is used to control the player's keystrokes for movement and note hits.
 */

public class KeyboardController : MonoBehaviour
{
    // The guitar string the player is on:
    [SerializeField] int currentString;
    
    // Set the moving limit of the player for difficulty changes:
    [SerializeField] private int minString, maxString;
    
    // The parent object of every string:
    private GameObject strings;
    
    void Start()
    {
        strings = GameObject.FindGameObjectWithTag("Strings");
    }

    void Update()
    {
        // Check if player want to move up or down the strings.
        // Then update currentString accordingly and make sure we're in bounds:
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentString = Mathf.Max(minString, currentString-1);
            updatePosition();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentString = Mathf.Min(maxString, currentString + 1);
            updatePosition();
        } 
    }

    private void updatePosition()
    {
        // This functions moves the player on the screen to the correct position according to currentString:
        // stringIndex must be (currentString-1) since our strings are organized from 1 to 6,
        // but their indexes starts from 0.
        int stringIndex = currentString - 1;

        // Get the new position from the string's Y position (with the button on the string):
        float newY = strings.transform.GetChild(stringIndex).transform.position.y;

        // Set new position on Y axis:
        transform.position = new Vector3(transform.position.x,
                                         newY,
                                         transform.position.z);
    }

    // A simple function to check if a pressed key is a number from minString to maxString.
    // This helps to check if the player pressed a wrong string key and missed.
    private int numPressed()
    {
        for (int i = minString; i <= maxString; i++)
            if (Input.GetKeyDown(i.ToString()))
                return i;

        // If not key was pressed return 0 - no action needed.
        return 0;
    }

    public int listenToNote()
    {
        /*
         This function returns:
            1 if the player pressed the num key that matches the string he's on.
            0 if the player pressed any key other than the string numbers (from minString to maxString).
            -1 if the player pressed a valid string key but he's on a different string (from minString to maxString). 
         */
        
        int notePlayed = numPressed();

        if (notePlayed == currentString)
        {
            return 1;
        }
        else if (notePlayed == 0)
        {
            return 0;
        }
        return -1;
    }
}
