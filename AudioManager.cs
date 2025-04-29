using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioSource defaultAudioSource;
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private AudioSource menuAudioSource;
    [SerializeField] private AudioSource gameOverAudioSource;


    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reLoadClip;
    [SerializeField] private AudioClip energyClip;
    [SerializeField] private AudioSource winAudioSource;
    [SerializeField] private AudioSource dieAudioSource;



    public void PlayShootSound()
    {
        effectAudioSource.PlayOneShot(shootClip);
    }

    public void PlayReloadSound()
    {
        effectAudioSource.PlayOneShot(reLoadClip);
    }

    public void PlayEnergySound()
    {
        effectAudioSource.PlayOneShot(energyClip);
    }
    public void PlayDefaultAudio()
    {
        bossAudioSource.Stop();
        defaultAudioSource.Play();
    }
    public void PlayBossAudio()
    {
        defaultAudioSource.Stop();
        bossAudioSource.Play();
    }
    public void StopAudioGame()
    {
    effectAudioSource.Stop();
    bossAudioSource.Stop();
    defaultAudioSource.Stop();
    menuAudioSource.Stop();
    gameOverAudioSource.Stop();
    winAudioSource.Stop();
    dieAudioSource.Stop();

    }
    public void PlayMenuAudio()
    {
    bossAudioSource.Stop();
    defaultAudioSource.Stop();
    menuAudioSource.Stop();
    dieAudioSource.Stop();  
    menuAudioSource.Play();
    }
    public void PlayGameOverAudio()
    {
        bossAudioSource.Stop();
        defaultAudioSource.Stop();
        menuAudioSource.Stop();
        gameOverAudioSource.Play();
    }
    public void PlayWinAudio()
    {
        bossAudioSource.Stop();
        defaultAudioSource.Stop();
        menuAudioSource.Stop();
        gameOverAudioSource.Stop();
        winAudioSource.Play();
    }
    public void PlayDieAudio()
{
    bossAudioSource.Stop();
    defaultAudioSource.Stop();
    menuAudioSource.Stop();
    gameOverAudioSource.Stop();
    winAudioSource.Stop();
    dieAudioSource.Play();
}

public float GetDieClipLength()
{
    return dieAudioSource.clip.length;
}

}
