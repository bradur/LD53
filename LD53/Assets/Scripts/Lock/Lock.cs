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

    [SerializeField]
    private Color indicatorColor;
    private Color originalColor;

    void Start()
    {
        originalColor = tumblerIndicator.color;
        animator = GetComponent<Animator>();
        tumblerDetectors = GetComponentsInChildren<TumblerDetector>().ToList();
    }
    public void PlayUnlockAnimation()
    {
        animator.Play("lockUnlock");
    }

    public void UnlockAnimationFinished()
    {
        UIManager.main.ShowNextLevelPopup(delegate
        {
            //
            Debug.Log("OPEN Next level!");
            GameManager.main.UnlockAnimationFinished(this);
        });
    }


    void Update()
    {

        if (GameManager.main.Paused)
        {
            return;
        }
        bool unlocked = tumblerDetectors.All(tumblerDetector => !tumblerDetector.IsTumbled);
        if (!unlocked)
        {
            tumblerIndicator.color = originalColor;
            return;
        }
        tumblerIndicator.color = indicatorColor;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Win!");
            GameManager.main.FinishLock();
        }
    }

}
