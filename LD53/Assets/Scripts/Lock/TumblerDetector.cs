using UnityEngine;

public class TumblerDetector : MonoBehaviour
{

    [SerializeField]
    private GameObject detectorTriggerTop;

    private bool isTumbled = true;
    public bool IsTumbled { get { return isTumbled; } }
    [SerializeField]
    private SpriteRenderer mainSprite;
    void OnTriggerExit2D(Collider2D collider)
    {
        isTumbled = false;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        isTumbled = true;
    }

    private void Start()
    {
        float height = mainSprite.size.y;
        Vector3 oldPos = detectorTriggerTop.transform.localPosition;
        detectorTriggerTop.transform.localPosition = new Vector3(oldPos.x, height, oldPos.z);
    }
}
