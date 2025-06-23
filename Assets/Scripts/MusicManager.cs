using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioClip mainMenuMusic;
    public AudioClip levelMusic;
    public AudioClip bossMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Automatically choose music based on scene
        if (scene.name == "MainMenu")
        {
            PlayMusic(mainMenuMusic);
        }
        else if (scene.name == "Level1" || scene.name == "Level2")
        {
            PlayMusic(levelMusic);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayBossMusic()
    {
        PlayMusic(bossMusic);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
