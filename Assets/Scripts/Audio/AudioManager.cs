using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance;

    // Reference for the AudioSources
    public AudioSource backgroundMusicSource;
    public AudioSource keyboardSoundSource;
    public AudioSource mouseSoundSource;
    public AudioSource ambientSoundSource;
    public AudioSource effectsSoundSource;


    // Global volume control
    [Range(0f, 1f)] public float globalVolume = 1f;

    void Awake()
    {
        // Singleton pattern to ensure only one AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make the AudioManager persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate AudioManager
        }
    }

    // Common method to play a sound
    public void PlaySound(AudioSource audioSource)
    {
        audioSource.volume = globalVolume;  // Apply global volume
        audioSource.Play();
    }

    // Method to trigger background music
    public void TriggerBackgroundMusic(AudioClip musicClip)
    {
        backgroundMusicSource.clip = musicClip;
        backgroundMusicSource.loop = true;
        PlaySound(backgroundMusicSource);
    }

    // Method to trigger keyboard sound
    public void TriggerKeyboardSound(AudioClip keyClip)
    {
        keyboardSoundSource.clip = keyClip;
        PlaySound(keyboardSoundSource);
    }

    // Method to trigger mouse sound
    public void TriggerMouseSound(AudioClip mouseClip)
    {
        mouseSoundSource.clip = mouseClip;
        PlaySound(mouseSoundSource);
    }

    // Method to change the global volume
    public void ChangeGlobalVolume(float volume)
    {
        globalVolume = Mathf.Clamp01(volume); // Ensure the volume is between 0 and 1
        // Adjust the volume of currently playing sounds
        backgroundMusicSource.volume = globalVolume;
        keyboardSoundSource.volume = globalVolume;
        mouseSoundSource.volume = globalVolume;
        ambientSoundSource.volume = globalVolume;
    }
}
