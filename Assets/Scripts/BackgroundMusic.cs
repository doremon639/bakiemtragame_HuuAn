using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour 
{
    private AudioSource audioSource; 

    void Start() 
    {
        audioSource = GetComponent<AudioSource>(); 
        if (audioSource == null) 
        {
            Debug.LogError("AudioSource component not found on this GameObject for BackgroundMusic script.");
            return;
        }
        audioSource.loop = true; 
        audioSource.Play(); 
    }

    public void ToggleMusic()
    {
        if (audioSource == null) return; 

        if (audioSource.isPlaying) 
        {
            audioSource.Pause(); 
        }
        else 
        {
            audioSource.Play(); // Phát nhạc
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource == null) return; 
        audioSource.volume = volume;
    }
}