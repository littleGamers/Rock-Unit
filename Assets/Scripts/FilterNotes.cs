using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is used to test the notes that went past the player,
 * and filter them by color to know if the player have missed a note.
 * Missed note = 1 life loss.
 */

public class FilterNotes : MonoBehaviour
{
    // The tag for the notes:
    [SerializeField] string triggeringTag;

    // The score manager to update if necessary:
    ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Scoreboard").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == triggeringTag)
        {
            // Get the renderer from the note object to check it's color:
            SpriteRenderer renderer = collision.GetComponent<SpriteRenderer>();

            // If note is white - it means that the player have missed it.
            if (renderer.color == Color.white)
            {
                // paint the note in red for visualization and then tell scoreManager we had a miss:
                renderer.color = Color.red;
                scoreManager.miss();
            }
        }
    }
}
