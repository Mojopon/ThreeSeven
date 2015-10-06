using UnityEngine;
using System.Collections;

public class SoundRepository : MonoBehaviour, ISoundRepository {

    public Sound[] sounds;
    [HideInInspector]
    public static SoundRepository Instance;

    public void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public AudioClip Get(SoundName soundName)
    {
        Sound target = null;
        foreach (Sound sound in sounds)
        {
            if (sound.soundName == soundName)
            {
                target = sound;
                break;
            }
        }

        if (target == null)
        {
            Debug.Log("Couldnt find the sound file!");
        }

        return target.audioClip;
    }
}

[System.Serializable]
public class Sound
{
    public SoundName soundName;
    public AudioClip audioClip;
}
