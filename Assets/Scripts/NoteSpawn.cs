using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Simple script to spawn notes on the strings.
 * The spawner should move with the camera and spawn according to settings below.
 */
public class NoteSpawn : MonoBehaviour
{
    /*
        The settings are:
            prefabToSpawn - the note's prefab.
            minTargetString/maxTargerString - the currently played strings. should be from 1 to 6.
            distanceBetweenNotes - the X distance between spawned notes.
     */
    [SerializeField] Collider2D prefabToSpawn; 
    [Tooltip("Choose numbers between 1 to 6")]
    [SerializeField] int minTargetString, maxTargerString;
    [SerializeField] float distanceBetweenNotes;

    private GameObject guitarStrings; // Our strings' parent object.
    private float nextNotePointX; // The next point to spawn a note on.

    void Start()
    {
        guitarStrings = GameObject.FindGameObjectWithTag("Strings");
        nextNotePointX = transform.position.x;
    }

    void Update()
    {
        float currentSpawnerLocationX = transform.position.x;

        // Check if we reached the destination point to spawn a note:
        if (nextNotePointX <= currentSpawnerLocationX)
        {
            // Update a new destination point:
            nextNotePointX = currentSpawnerLocationX + distanceBetweenNotes;
            spawnNote();
        }
    }

    private void spawnNote()
    {
        // Random range from minString to maxString+1 because the upper bound of the range is exclusive.
        // When we add 1 to the upper bound we will get a value from minString(inclusive) to maxString(inclusive).
        int stringIndex = Random.Range(minTargetString, maxTargerString + 1);

        // The index will be stringToSpawn-1 because our string begin from 1 and indexes begin from 0.
        GameObject guitarStringToSpawnOn = guitarStrings.transform.GetChild(stringIndex - 1).gameObject;

        // Get the Y value from the randpm string's position:
        float nextNotePointY = guitarStringToSpawnOn.transform.position.y;

        // Create a new position vector and spawn a new note in that location with the correct prefab.
        Vector3 nextNotePosition = new Vector3(nextNotePointX, nextNotePointY, 0);

        Instantiate(prefabToSpawn.gameObject, nextNotePosition, Quaternion.identity);
    }
}
