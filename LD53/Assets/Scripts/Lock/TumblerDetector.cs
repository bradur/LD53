using UnityEngine;

public class TumblerDetector : MonoBehaviour
{
    private bool isTumbled = true;
    public bool IsTumbled { get { return isTumbled; } }
    void OnTriggerExit2D(Collider2D collider)
    {
        isTumbled = false;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        isTumbled = true;
    }
}
