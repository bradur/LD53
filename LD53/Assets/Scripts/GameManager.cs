using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    void Awake()
    {
        main = this;
    }
    private bool paused = false;
    public bool Paused { get { return paused; } }
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1f;
    }

    public void FinishLock()
    {
        Pause();
        PlayUnlockAnimation();
    }

    private void PlayUnlockAnimation()
    {
        Lock.main.PlayUnlockAnimation();

    }

    public void PerformUIAction(UIAction action)
    {
        if (action == UIAction.NextLevel)
        {
            Debug.Log("Next level!");

        }
    }
}
