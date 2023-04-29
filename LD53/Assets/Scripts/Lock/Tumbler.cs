using UnityEngine;

public class Tumbler : MonoBehaviour
{
    [SerializeField]
    private float tumbleSpeed = 1f;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Tumble(float speed)
    {
        rb2d.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2DFromChild(Collision2D collision)
    {
    }
    public void OnTriggerEnter2DFromChild(Collider2D collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer("LockpickHead"))
        {
            Debug.Log("Not lockpickhead");
            return;
        }
        Rigidbody2D lockPickRb2d = collider.GetComponentInParent<Rigidbody2D>();
        if (lockPickRb2d)
        {
            Tumble(tumbleSpeed + lockPickRb2d.velocity.y);
        }
        else
        {
            Debug.LogWarning("No rb2d in lockpick????");
        }
    }
}
