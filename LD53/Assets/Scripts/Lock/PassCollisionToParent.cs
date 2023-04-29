using UnityEngine;

public class PassCollisionToParent : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject parent;
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        parent.BroadcastMessage("OnCollisionEnter2DFromChild", collision);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        parent.BroadcastMessage("OnTriggerEnter2DFromChild", collider);
    }
}
