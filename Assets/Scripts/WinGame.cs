using UnityEngine;

/*
    This script is being used with an Edge Collider to stop the game from running.
    When the noteSpawner reaches the collider the game stops and the player wins.
 */
public class WinGame : MonoBehaviour
{
    // The tag used for the noteSpawner which is location right after the camera:
    [SerializeField] string triggeringTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == triggeringTag)
        {
            // If reached - end game:
            ScoreManager scoreManager = GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<ScoreManager>();
            scoreManager.gameOver();
        }
    }
}
