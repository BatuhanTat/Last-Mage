using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance { get; private set; }

    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        //audioSource = GetComponent<AudioSource>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = mainMenuMusic;
        audioSource.Play();
    }

    public void MainMenuMusic()
    {
        StopAllCoroutines();   
        StartCoroutine(FadeOutAndChangeTrack(mainMenuMusic));
    }

    public void GameplayMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutAndChangeTrack(gameplayMusic));
    }

    private IEnumerator FadeOutAndChangeTrack(AudioClip newClip)
    {
        float timeToFade = 0.5f;
        float timeElapsed = 0;
        float startVolume = audioSource.volume;

        while (timeElapsed < timeToFade)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        timeElapsed = 0;
        while (timeElapsed < timeToFade)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}