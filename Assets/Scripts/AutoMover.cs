using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Simple script to move the camera automatically in desired speed.
 */

public class AutoMover : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0 ,0);
    }
}
