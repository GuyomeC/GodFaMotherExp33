using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip clockMusic;
    public AudioClip findCatSound;
    public AudioClip nobodyInCrosshairSound;
    public AudioClip FindCoinSound;
    public AudioClip loseSound;
    public AudioClip winSound;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolumeChange = 0.5f;
    public float musicVolumeBase;


    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
