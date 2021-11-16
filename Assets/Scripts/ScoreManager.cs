using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/*
    This script handle all score changes and wins/losses.
 */
public class ScoreManager : MonoBehaviour
{
    // The player's score, remaining lives and how many point will a correct hit grants:
    [SerializeField] private int playerScore;
    [SerializeField] private int playerLives;
    [SerializeField] private int scorePerHit;

    // The flag to check if a game reset is needed after game over.
    private bool resetGameListener = false; 

    // Our displayed text score field:
    private TextMeshPro scoreField;

    private void Start()
    {
        // Initialize displayed score to the starting scored entered in the Unity editor:
        scoreField = GetComponent<TextMeshPro>();
        updateScore();
    }

    public void hit()
    {
        // Successfull note hit:
        playerScore += scorePerHit;
        updateScore();
    }
    public void miss()
    {
        // Unsuccessfull note play.
        // If the player ran out of lives we should end the game.
        if (playerLives == 0)
            gameOver();
        else
        {
            playerLives--;
            updateScore();
        }
    }

    private void updateScore()
    {
        // Update the displayed score and lives.
        if (scoreField)
            scoreField.text = "score:" + playerScore + "\t\tlives:" + playerLives ;
    }

    public void gameOver()
    {
        // This function is used to end a game with a win/loss.

        // First we stop the camera and player controllers:
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AutoMover>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<KeyboardController>().enabled = false;

        // Then we display the 'End Game' messages.
        GameObject.FindGameObjectWithTag("EndGameMessage").GetComponent<EndGameMessage>().endGame();

        // Finally we change the resetGameListener to true and wait for user input.
        resetGameListener = true;
    }
    private void Update()
    {
        // if the game has ended and player have pressed SPACE we start over the game.
        if (resetGameListener && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public int getScore()
    {
        return playerScore;
    }
    public int getLives()
    {
        return playerLives;
    }
}
