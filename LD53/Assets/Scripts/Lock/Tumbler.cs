using UnityEngine;

public class Tumbler : MonoBehaviour
{

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Tumble(float speed)
    {
        rb2d.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        SoundManager.main.PlaySound(GameSoundType.LockClick);
    }

    public void OnCollisionEnter2DFromChild(Collision2D collision)
    {
    }
    public void OnTriggerEnter2DFromChild(Collider2D collider)
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "tumblerDetector")
        {
            SoundManager.main.PlaySound(GameSoundType.LockBouncy);
        }
    }

}
