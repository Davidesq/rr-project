using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using UnityEngine.SceneManagement;
//using GooglePlayGames.Android;

public class ButtonActions : MonoBehaviour {

    public AudioClip sound;

    public AudioSource music;

    public LoadGameLevel load;
    
    public GameObject pausedCanvas;
    public GameObject Panel;
    public GameObject pauseItems;
    public GameObject resumeAnim;
    public GameObject pauseButton;

    public AudioClip counterSound;

    bool paused;
    
    void Start()
    {
        paused = false;

        if(SceneManager.GetActiveScene().buildIndex == 1)
            pausedCanvas.SetActive(false);
    }

    public void Retry()
    {
        AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 200f);

        int score = GameObject.Find("Manager").GetComponent<Enemies>().GetScore();

        if (score >= 30)
        {
            load.PlayGameButton();
            music = GameObject.Find("BGMusic").GetComponent<AudioSource>();
            music.Stop();
            music.pitch = 1;
            music.Play();
        }

        else load.PlayGameButton();
    }

    public void Play()
    {
        AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 200f);
        load.PlayGameButton();
    }

    public void HighScoresButtonClicked()
    {
        AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 200f);
        //parameters: audioclip sound file, vector2 origin of the sound in space, float intensity

        if (Social.Active.localUser.authenticated)//check if the user is connected to Google Play games
            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkIuornqoQQEAIQAA"); //call the leaderboard
    }
    
    public void RateButtonClicked()
    {
        AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 200f);
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Davidesq.Game0");
    }

    public void HomeButtonClicked()
    {
        if (Time.timeScale != 1) Time.timeScale = 1;
        AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 200f);
        load.HomeButton();
    }

    public void Pause()
    {
        music = GameObject.Find("BGMusic").GetComponent<AudioSource>();

        AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 200f);

        paused = !paused;

        if (paused)
        {
            resumeAnim.SetActive(false);
            pauseItems.SetActive(true);
            Time.timeScale = float.Epsilon;
            pausedCanvas.SetActive(true);
            pauseButton.SetActive(false);
        }
        else if (!paused)
        {            
            StartCoroutine("PlayAnimation");            
        }
    }

    IEnumerator PlayAnimation()
    {
        resumeAnim.SetActive(true);
        pauseItems.SetActive(false);

        yield return StartCoroutine(CoroutineUtil1.WaitForRealSeconds(1f));
        
        for (int i = 0; i < 3; i++)
        {
            AudioSource.PlayClipAtPoint(counterSound, new Vector2(0f, 0f), 100);
            yield return StartCoroutine(CoroutineUtil1.WaitForRealSeconds(1.075f));
        }

        Time.timeScale = 1;
        pauseButton.SetActive(true);
        pausedCanvas.SetActive(false);
        yield return null;
    }
}
