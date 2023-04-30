using System.Collections.Generic;
using UnityEngine;

public class Lockpick : MonoBehaviour
{
    [SerializeField]
    private float horizontalSpeed = 1f;
    [SerializeField]
    private float verticalSpeed = 1f;

    [SerializeField]
    private float gravity = 1f;

    [SerializeField]
    private Transform floorPosition;
    [SerializeField]
    private Transform ceilingPosition;

    private float velYBeforeCol = 0f;

    private Rigidbody2D rb2d;
    // Start is called before the first frame update

    private List<KeyCode> horizontalKeys = new List<KeyCode>() {
        KeyCode.A,
        KeyCode.D,
        KeyCode.LeftArrow,
        KeyCode.RightArrow
    };
    private List<KeyCode> verticalKeys = new List<KeyCode>() {
        KeyCode.W,
        KeyCode.UpArrow
    };

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.main.Paused)
        {
            return;
        }
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        Vector2 vel = CalculateVelocityFromInput(horizontalAxis, verticalAxis);
        vel.y = CalculateGravity(vel.y);
        vel = HandleSlowdown(vel, verticalAxis);
        rb2d.velocity = vel;
    }

    private float CalculateGravity(float currentYVel)
    {
        if (!InputHelper.GetAnyKey(verticalKeys) && transform.position.y > floorPosition.position.y)
        {
            return currentYVel -= gravity * Time.deltaTime;
        }
        return currentYVel;
    }

    private Vector2 HandleSlowdown(Vector2 vel, float verticalAxis)
    {
        Vector2 newVel = new Vector2(vel.x, vel.y);
        if (!InputHelper.GetAnyKey(horizontalKeys))
        {
            newVel.x = Mathf.Lerp(newVel.x, 0, 0.5f);
        }
        if (!InputHelper.GetAnyKey(verticalKeys) && verticalAxis > 0.001f)
        {
            newVel.y = Mathf.Lerp(newVel.y, 0, 0.5f);
        }
        return newVel;
    }
    private Vector2 CalculateVelocityFromInput(float x, float y)
    {
        Vector2 vel = rb2d.velocity;

        vel.x += x * horizontalSpeed * Time.deltaTime;
        if (y > 0.001f && transform.position.y < ceilingPosition.position.y)
        {
            vel.y += y * verticalSpeed * Time.deltaTime;
            velYBeforeCol = vel.y;
        }
        return vel;
    }

    private void OnCollisionEnter2DFromChild(Collision2D collision)
    {
        //
    }

    private void OnTriggerEnter2DFromChild(Collider2D collider)
    {
        Tumbler tumbler = collider.GetComponentInParent<Tumbler>();
        if (tumbler)
        {
            tumbler.Tumble(velYBeforeCol);
            //            Debug.Log($"Bef: {velYBeforeCol} velY: {rb2d.velocity.y}");
        }
    }
}
