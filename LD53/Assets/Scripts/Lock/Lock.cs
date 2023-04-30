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
    private List<Tumbler> tumblers;

    [SerializeField]
    private SpriteRenderer tumblerIndicator;

    private Animator animator;

    private List<KeyCode> openLockKeys = new List<KeyCode>() {
        KeyCode.Space,
        KeyCode.E
    };

    private List<KeyCode> tumblerLockKeys = new List<KeyCode>() {
        KeyCode.LeftControl,
        KeyCode.RightControl,
        KeyCode.C
    };

    [SerializeField]
    private Color indicatorColor;
    private Color originalColor;

    void Start()
    {
        originalColor = tumblerIndicator.color;
        animator = GetComponent<Animator>();
        tumblerDetectors = GetComponentsInChildren<TumblerDetector>().ToList();
        tumblers = GetComponentsInChildren<Tumbler>().ToList();
    }
    public void PlayUnlockAnimation()
    {
        animator.Play("lockUnlock");
    }

    public void UnlockAnimationFinished()
    {
        SoundManager.main.PlaySound(GameSoundType.DoorOpen);
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
        if (InputHelper.GetAnyKeyDown(tumblerLockKeys))
        {
            foreach (Tumbler tumbler in tumblers)
            {
                tumbler.ReleaseTop();
            }
            Tumbler topTumbler = tumblers.FirstOrDefault(tumbler => tumbler.IsTopTumbled);
            if (topTumbler != null)
            {
                topTumbler.LockTop();
            }
        }
        bool unlocked = tumblerDetectors.All(tumblerDetector => !tumblerDetector.IsTumbled);
        if (!unlocked)
        {
            tumblerIndicator.color = originalColor;
            return;
        }
        tumblerIndicator.color = indicatorColor;
        if (InputHelper.GetAnyKeyDown(openLockKeys))
        {
            Debug.Log("Win!");
            SoundManager.main.PlaySound(GameSoundType.LockPullOpen);
            GameManager.main.FinishLock();
        }
    }

}
