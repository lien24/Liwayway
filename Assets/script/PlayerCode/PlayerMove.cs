using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerMove : MonoBehaviour
{
    // Variable used to set the movement speed of the player
    public float runSpeed = 4.0f;
    // Variable used to hold the horizontal value
    float horizontal;
    // Variable used to hold the vertical value
    float vertical;

    

    void Update()
    {
        // Check if the user is pressing Horizontal inputs
        horizontal = Input.GetAxis("Horizontal");
        // Check if the user is pressing Vertical inputs
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        // Set the value vector movement of the player depending on the user input
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        // Normalize the vector to maintain consistent speed
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Move the player to the set vector location according to the movement value
        transform.position = transform.position + movement * runSpeed * Time.deltaTime;
    }




}
