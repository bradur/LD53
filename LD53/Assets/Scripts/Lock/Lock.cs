using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public static Lock main;
    void Awake()
    {
        main = this;
    }
    private List<TumblerDetector> tumblerDetectors;

    [SerializeField]
    private SpriteRenderer tumblerIndicator;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        tumblerDetectors = GetComponentsInChildren<TumblerDetector>().ToList();
    }
    public void PlayUnlockAnimation()
    {
        animator.Play("lockUnlock");
    }

    public void UnlockAnimationFinished()
    {
        UIManager.main.ShowNextLevelPopup();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Win!");
            GameManager.main.FinishLock();
        }
        if (GameManager.main.Paused)
        {
            return;
        }
        bool unlocked = tumblerDetectors.All(tumblerDetector => !tumblerDetector.IsTumbled);
        if (!unlocked)
        {
            tumblerIndicator.color = Color.gray;
            return;
        }
        tumblerIndicator.color = Color.green;

    }

}
