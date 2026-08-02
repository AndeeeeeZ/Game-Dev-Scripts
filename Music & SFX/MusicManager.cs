using UnityEngine;

/*
* Manages the music that plays continuously through scenes
* Singleton class that lives in DontDestroyOnLoad
*/

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip)
        {
            Debug.LogError("MusicManager did not receive any audio clip");
            return;
        }

        musicSource.clip = clip;
        musicSource.Play();
    }
}