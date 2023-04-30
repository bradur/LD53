using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    [SerializeField]
    private List<Transform> groundCheckPositions;
    private bool grounded = false;
    private bool allowJumping = false;
    private bool facingRight = true;
    [SerializeField]
    private float moveForce = 365f;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float jumpForce = 1000f;
    [SerializeField]
    private float maxFallSpeed = 20f;

    [SerializeField]
    private float startingSpeed = 0.4f;


    [SerializeField]
    [Range(0f, 0.8f)]
    private float groundedLingerTime = 0.1f;
    private float lingerTimer = 0f;

    private List<KeyCode> jumpKeys = new List<KeyCode>(){
        KeyCode.UpArrow,
        KeyCode.W
    };
    private List<KeyCode> moveKeys = new List<KeyCode>(){
        KeyCode.A,
        KeyCode.D,
        KeyCode.LeftArrow,
        KeyCode.RightArrow
    };

    [SerializeField]
    private Animator animator;
    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {

            grounded = lingerTimer > 0f;
            if (!grounded)
            {
                foreach (Transform groundCheck in groundCheckPositions)
                {
                    if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
                    {

                        lingerTimer = groundedLingerTime;
                        grounded = true;
                        break;
                    }
                }
            }
            else
            {
                lingerTimer -= Time.deltaTime;
            }


            if (grounded && InputHelper.GetAnyKey(jumpKeys))
            {
                allowJumping = true;
                lingerTimer = -1f;
                grounded = false;
                //animator.Play("jump");
            }
        }
    }

    private bool stopped = false;
    public void Stop()
    {
        stopped = true;
    }

    void FixedUpdate()
    {
        if (!stopped)
        {
            float horizontalAxis = Input.GetAxis("Horizontal");

            bool moveKeysPressed = InputHelper.GetAnyKey(moveKeys);
            if (moveKeysPressed)
            {
                animator.Play("walk");
            }
            else
            {
                animator.Play("idle");
            }
            Vector2 newVelocity = rb2d.velocity;

            if (!moveKeysPressed)
            {
                newVelocity.x = 0;
            }
            else
            {
                float factor = horizontalAxis > 0 ? 1 : -1;
                newVelocity.x += moveForce * Time.deltaTime * factor;
                newVelocity.x = Mathf.Clamp(Mathf.Abs(newVelocity.x), startingSpeed, maxSpeed) * factor;
            }

            if ((horizontalAxis > 0 && !facingRight) || (horizontalAxis < 0 && facingRight))
            {
                Flip();
            }

            if (allowJumping)
            {
                //anim.SetTrigger("Jump");
                //rb2d.AddForce(new Vector2(0f, jumpForce));
                newVelocity.y = jumpForce;
                allowJumping = false;
            }
            newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed, maxSpeed);
            newVelocity.y = Mathf.Clamp(newVelocity.y, -maxFallSpeed, jumpForce);
            rb2d.velocity = newVelocity;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
