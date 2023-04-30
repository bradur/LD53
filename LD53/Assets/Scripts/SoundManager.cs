using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager main;
    [SerializeField]
    private List<GameSound> gameSounds;
    private void Awake()
    {
        main = this;
    }

    public void PlaySound(GameSoundType soundType)
    {
        GameSound gameSound = gameSounds.Where(sound => sound.Type == soundType).FirstOrDefault();
        if (gameSound != null)
        {
            AudioSource audio = gameSound.Get();
            if (audio != null)
            {
                audio.Play();
            }
        }
    }


    public void PlaySoundLoop(GameSoundType soundType)
    {
        GameSound gameSound = gameSounds.Where(sound => sound.Type == soundType && sound.Loop).FirstOrDefault();
        if (gameSound != null)
        {
            AudioSource audio = gameSound.GetLoop();
            if (audio != null && !audio.isPlaying)
            {
                audio.Play();
            }
        }
    }

    public void PauseLoop(GameSoundType soundType)
    {
        GameSound gameSound = gameSounds.Where(sound => sound.Type == soundType && sound.Loop).FirstOrDefault();
        if (gameSound != null)
        {
            AudioSource audio = gameSound.GetLoop();
            if (audio != null && audio.isPlaying)
            {
                audio.Pause();
            }
        }
    }
}


public enum GameSoundType
{
    LockClick,
    LockHit,
    LockBouncy,
    LockPullOpen,
    DoorOpen
}

[System.Serializable]
public class GameSound
{
    [field: SerializeField]
    public GameSoundType Type { get; private set; }

    [field: SerializeField]
    private List<AudioSource> sounds;

    private List<GameSoundPool> soundPools = new List<GameSoundPool>();
    private bool initialized = false;

    [field: SerializeField]
    public bool Loop { get; private set; } = false;

    public AudioSource Get()
    {
        if (!initialized)
        {
            initialize();
        }

        if (sounds == null || sounds.Count == 0)
        {
            return null;
        }
        return soundPools[Random.Range(0, soundPools.Count)].getAvailable();
    }

    public AudioSource GetLoop()
    {
        if (!initialized)
        {
            initialize();
        }

        if (sounds == null || sounds.Count == 0)
        {
            return null;
        }
        return soundPools[Random.Range(0, soundPools.Count)].getAvailableLoop();
    }

    private void initialize()
    {
        soundPools = sounds.Select(it => new GameSoundPool(it)).ToList();
        initialized = true;
    }


    private class GameSoundPool
    {
        private AudioSource originalAudioSource;
        private List<AudioSource> audioSources = new List<AudioSource>();

        public GameSoundPool(AudioSource audioSource)
        {
            originalAudioSource = audioSource;
            addNewToPool();
        }

        public AudioSource getAvailableLoop()
        {
            var src = audioSources.First();
            if (src == null)
            {
                src = addNewToPool();
            }
            return src;
        }

        public AudioSource getAvailable()
        {
            var src = audioSources.Where(it => it.isPlaying == false).FirstOrDefault();
            if (src == null)
            {
                src = addNewToPool();
            }
            return src;
        }

        private AudioSource addNewToPool()
        {
            AudioSource newSource = GameObject.Instantiate(originalAudioSource, originalAudioSource.transform.parent);
            audioSources.Add(newSource);
            return newSource;
        }
    }
}