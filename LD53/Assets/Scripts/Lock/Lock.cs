using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private List<TumblerDetector> tumblerDetectors;

    [SerializeField]
    private SpriteRenderer tumblerIndicator;

    void Start()
    {
        tumblerDetectors = GetComponentsInChildren<TumblerDetector>().ToList();
        Debug.Log(tumblerDetectors.Count);
    }


    void Update()
    {
        bool unlocked = tumblerDetectors.All(tumblerDetector => !tumblerDetector.IsTumbled);
        if (!unlocked)
        {
            tumblerIndicator.color = Color.gray;
            return;
        }
        tumblerIndicator.color = Color.green;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Win!");
        }
    }

}
