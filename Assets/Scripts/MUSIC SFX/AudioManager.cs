using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public static AudioManager Instance;

    [Header("AudioClip")]
    public AudioClip _GameMusic;
    public AudioClip _MenuMusic;
    public AudioClip _GameOverMusic;
    public AudioClip _AttackSFX;
    public AudioClip _ShootSFX;
    public AudioClip _NPCShootSFX;
    public AudioClip _NPCDamageSFX;
    public AudioClip _PlayerDamageSFX;
    public AudioClip _SelectSFX;

    public void Awake()
    {
        Instance = this;
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void NPCShootSFX()
    {
        PlaySFX(_NPCShootSFX);
    }
    public void ShootSFX()
    {
        PlaySFX(_ShootSFX);
    }
    public void AttackSFX()
    {
        PlaySFX(_AttackSFX);
    }

    public void PlayerDMG()
    {
        PlaySFX(_PlayerDamageSFX);
    }

    public void NpcDMG()
    {
        PlaySFX(_NPCDamageSFX);
    }

    public void SelectSFX()
    {
        PlaySFX(_SelectSFX);
    }

}
