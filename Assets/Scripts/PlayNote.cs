using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    A script for the player to use.
    This script allows the player to play notes and update the score accordingly.
    It also handles the missed notes.
 */
public class PlayNote : MonoBehaviour
{
    // The tag of the note objects:
    [SerializeField] string noteTag;

    // isPlayable is enabled when player is touching a note.
    // playerPlayedNote is enabled if player has played a touched note (never mind if it was with the correct key).
    // noteCollided - the note touched by the player.
    // scoreManager - used to update the score.
    private bool isPlayable = false;
    private bool playerPlayedNote = false;
    private Collider2D noteCollided = null;
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If we touch a note we initialize all parameters for the current "playing session" and color the note in Cyan.
        if (noteTag == collider.tag)
        {
            isPlayable = true;
            playerPlayedNote = false;
            noteCollided = collider;
            noteCollided.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        // If we stopped touching the note we set the parameters according to player's action.
        // If the player missed - we color the  note in red and call miss() from the scoreBoard.
        if (noteTag == collider.tag)
        {
            isPlayable = false;
            if (!playerPlayedNote)
            {
                noteCollided.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                scoreManager.miss();
            }
            noteCollided = null;
        }
    }

    void Update()
    {
        // Check if a note has been played (a number key)
        int notePlayed = GetComponent<KeyboardController>().listenToNote();

        // If player got the right note: 
        if (notePlayed == 1)
        {
            // Check if he is touching the note:
            if (isPlayable)
            {
                // Green means successfull hit:
                noteCollided.gameObject.GetComponent<SpriteRenderer>().color = Color.green;

                isPlayable = false; // To avoid double hits.
                playerPlayedNote = true;
                scoreManager.hit();
            }
            // Else - the player pressed too early/late:
            else
            {
                scoreManager.miss();
            }
        }
        // If player pressed a num key which doesn't belong to the string he's on:
        else if (notePlayed == -1)
        {
            // If the player touches the note - paint it red:
            if (noteCollided)
                noteCollided.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            // call miss() to lose lives and update the fields:
            isPlayable = false;
            playerPlayedNote = true;
            scoreManager.miss();
        }
    }
    
}
