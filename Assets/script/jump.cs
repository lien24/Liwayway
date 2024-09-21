using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float jumpSpeed = 4.0f;
    private bool onGround = false;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    float movementSpeed;

    void Update()
    {
        float amountToMove = movementSpeed * Time.deltaTime;
        Vector3 movement = (Input.GetAxis("Horizontal") * -Vector3.left * amountToMove) + (Input.GetAxis("Vertical") * Vector3.forward * amountToMove);
        rb.AddForce(movement, ForceMode.Force);

        // Only jump if the player is on the ground
        if (Input.GetKeyDown("space") && onGround)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            onGround = false; // After jumping, set onGround to false
        }
    }

    // Check if the player is on the ground using collision detection
    void OnCollisionStay(Collision collision)
    {
        // You may want to check the tag or layer of the object you're colliding with to ensure it's the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true; // Reset to true when on the ground
        }
    }

    // Optionally, use OnCollisionExit to ensure onGround is false when leaving the ground
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false; // Player is not on the ground anymore
        }
    }

}
