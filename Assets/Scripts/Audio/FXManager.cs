using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SoundEffectEntry
{
    public string key;
    public AudioClip clip;
}

public class FXManager : MonoBehaviour
{
    public static FXManager Instance;

    // List to show in the Inspector
    public List<SoundEffectEntry> soundEffectList = new List<SoundEffectEntry>();

    // Actual dictionary used in the game
    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    private AudioSource fxAudioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        fxAudioSource = gameObject.AddComponent<AudioSource>();

        // Populate the dictionary from the list
        foreach (var entry in soundEffectList)
        {
            if (!soundEffects.ContainsKey(entry.key))
            {
                soundEffects.Add(entry.key, entry.clip);
            }
        }
    }

    public void PlayFX(string key)
    {
        if (soundEffects.ContainsKey(key))
        {
            fxAudioSource.PlayOneShot(soundEffects[key]);
        }
        else
        {
            Debug.LogWarning("Sound effect " + key + " not found!");
        }
    }
}
