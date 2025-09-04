using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    [Space]
    [Header("References")]
    public static AudioManager Instance;
    [SerializeField] private GameObject musicSourceObj;
    [SerializeField] private GameObject sfxSourceObj;

    [Space]
    [Header("Music Audio Clips")]
    public AudioClip menuMusic;
    public AudioClip gameplayMusic;
    public AudioClip konamiMusic;

    [Space]
    [Header("Sfx Audio Clips")]
    public AudioClip konamiValidatedSfx;
    public AudioClip FireSfx;
    public AudioClip BombSfx;
    public AudioClip JerryCanSfx;
    public AudioClip GasSfx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        musicSourceObj = GameObject.FindGameObjectWithTag("MusicSource");
        sfxSourceObj = GameObject.FindGameObjectWithTag("SfxSource");
        musicAudioSource = musicSourceObj.GetComponent<AudioSource>();
        sfxAudioSource = sfxSourceObj.GetComponent<AudioSource>();
    }

    private void Start()
    {
        musicAudioSource.clip = menuMusic;
        musicAudioSource.Play();
    }

    public void PlaySFX(AudioClip inAudioClip)
    {
        sfxAudioSource.PlayOneShot(inAudioClip);
    }
}
