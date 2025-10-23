using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Animator animator;
    private Vector3 scale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        scale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(0, 0);
        // Move right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.linearVelocityX = speed;
            scale.x = Mathf.Abs(scale.x);
        }

        // Move left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.linearVelocityX = -speed;
            scale.x = -Mathf.Abs(scale.x);
        }

        // Move up
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.linearVelocityY = speed;
        }

        // Move down
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.linearVelocityY = -speed;
        }

        this.transform.localScale = scale;

        if (rb.linearVelocityX == 0 && rb.linearVelocityY == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }
}
