using UnityEngine;

public class PlayerAnimControl : MonoBehaviour
{
    public float runSpeed = 1f;

    private float horizontal;
    private float vertical;
    private Animator animator;
    private bool facingRight = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        float movementValue = Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical));
        animator.SetFloat("Speed", movementValue);
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontal * runSpeed, 0.0f, vertical * runSpeed);
        transform.position += movement * Time.deltaTime;

        Flip(horizontal);
    }

    private void Flip(float horizontal)
    {
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
