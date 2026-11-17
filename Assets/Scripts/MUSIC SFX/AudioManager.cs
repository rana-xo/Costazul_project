using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] private AudioSource audio_source;
    public static AudioManager Instance;

    [Header("AudioClip")]
    public AudioClip _GameMusic;
    public AudioClip _MenuMusic;
    public AudioClip _GameOverMusic;
    public AudioClip _AttackSFX;
    public AudioClip _ShootSFX;
    public AudioClip _SelectSFX;

    public void Awake()
    {
        audio_source = GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ShootSFX()
    {
        audio_source.PlayOneShot(_ShootSFX);
    }
}
