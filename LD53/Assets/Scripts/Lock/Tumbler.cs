using UnityEngine;

public class Tumbler : MonoBehaviour
{

    private Rigidbody2D rb2d;
    [SerializeField]
    private SpriteRenderer tumblerSr;
    [SerializeField]
    private Color topColor;
    private Color originalColor;

    private bool isTopTumbled = false;
    public bool IsTopTumbled { get { return isTopTumbled; } }
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalColor = tumblerSr.color;
    }

    public void Tumble(float speed)
    {
        rb2d.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        SoundManager.main.PlaySound(GameSoundType.LockClick);
    }

    public void LockTop()
    {
        if (rb2d.simulated)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.simulated = false;
            tumblerSr.color = topColor;
        }
    }

    public void ReleaseTop()
    {
        tumblerSr.color = originalColor;
        rb2d.simulated = true;
    }

    public void OnCollisionEnter2DFromChild(Collision2D collision)
    {

    }
    public void OnTriggerEnter2DFromChild(Collider2D collider)
    {

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "tumblerStopperTop")
        {
            isTopTumbled = false;
            tumblerSr.color = originalColor;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "tumblerStopperTop")
        {
            Debug.Log("Top tumbler");
            isTopTumbled = true;
            tumblerSr.color = topColor;
        }
        if (collider.gameObject.tag == "tumblerDetector")
        {
            SoundManager.main.PlaySound(GameSoundType.LockBouncy);
        }
    }

}
