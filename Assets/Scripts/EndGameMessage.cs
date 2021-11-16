using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This script is used to control the text displayed in the end of the game.
 */

public class EndGameMessage : MonoBehaviour
{
    // We check the score with 'scoreManager' and update it on 'messageBox'
    private ScoreManager scoreManager;
    private TextMeshPro messageBox;

    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<ScoreManager>();
        messageBox = GetComponent<TextMeshPro>();
    }

    // endGame is called whenever the game has to end (Win or Loss):
    public void endGame()
    {
        // If player ran out of lives he lost:
        if (scoreManager.getLives() == 0)
        {
            messageBox.text = "you lost!\n" +
                "score:" + scoreManager.getScore() + "\n" +
                "press space to start over";
        }
        // Else - means that player wins!
        else
        {
            messageBox.text = "you win!\n" +
                "score:" + scoreManager.getScore() + "\n" +
                "press space to start over";
        }

        // Display the message:
        messageBox.enabled = true;
    }
}
