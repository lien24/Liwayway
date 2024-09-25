using UnityEngine;

public class MobMovement2D : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the mob
    public float moveDistance = 5.0f; // Distance to move left and right
    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Flip the sprite by inverting the X scale
        transform.localScale = localScale;
    }
}
