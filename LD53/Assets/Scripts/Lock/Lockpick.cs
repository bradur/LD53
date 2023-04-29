using System.Collections.Generic;
using UnityEngine;

public class Lockpick : MonoBehaviour
{
    [SerializeField]
    private float horizontalSpeed = 1f;
    [SerializeField]
    private float verticalSpeed = 1f;
    [SerializeField]
    private float staticSpeed = 3f;

    [SerializeField]
    private float slowSpeed = 1f;

    [SerializeField]
    private bool speedIsStatic = false;

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
        KeyCode.S,
        KeyCode.UpArrow,
        KeyCode.DownArrow
    };

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        Vector2 vel = CalculateVelocity(horizontalAxis, verticalAxis);
        if (!InputHelper.GetAnyKey(horizontalKeys))
        {
            if (speedIsStatic)
            {
                vel.x = 0;
            }
            else
            {
                vel.x = Mathf.Lerp(vel.x, 0, 0.5f);
            }
        }
        if (!InputHelper.GetAnyKey(verticalKeys))
        {
            if (speedIsStatic)
            {
                vel.y = 0;
            }
            else
            {
                vel.y = Mathf.Lerp(vel.y, 0, 0.5f);
            }
        }
        rb2d.velocity = vel;
    }

    private void ApplyMovementAsPosition(float x, float y)
    {
        Vector3 pos = transform.position;
        pos.x += x * horizontalSpeed * Time.deltaTime;
        pos.y += y * verticalSpeed * Time.deltaTime;
        transform.position = pos;
    }
    private Vector2 CalculateVelocity(float x, float y)
    {
        Vector2 vel = rb2d.velocity;
        if (speedIsStatic)
        {
            float minInputForStatic = 0.001f;
            if (Mathf.Abs(x) > minInputForStatic)
            {
                float dir = x > 0 ? 1 : -1;
                vel.x = x * staticSpeed * horizontalSpeed * Time.deltaTime;
            }
            if (Mathf.Abs(y) > minInputForStatic)
            {
                float dir = y > 0 ? 1 : -1;
                vel.y = y * staticSpeed * verticalSpeed * Time.deltaTime;
            }
        }
        else
        {
            vel.x += x * horizontalSpeed * Time.deltaTime;
            vel.y += y * verticalSpeed * Time.deltaTime;
        }
        return vel;
    }
}
