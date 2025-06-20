using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip jumpSound; // Chỉ giữ lại âm thanh nhảy nếu bạn sử dụng nó

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject for SoundManager.");
        }
    }

    public void PlayJump()
    {
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        else
        {
            if (jumpSound == null) Debug.LogWarning("Jump Sound is not assigned in SoundManager.");
            if (audioSource == null) Debug.LogWarning("AudioSource is missing on SoundManager GameObject.");
        }
    }

    public void SetSFXVolume (float volume)
    {
        audioSource.volume = volume;
    }
}