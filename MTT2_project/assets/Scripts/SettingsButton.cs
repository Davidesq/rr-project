using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsButton : MonoBehaviour {

    public Image settingsImageRenderer;
    public Image soundImageRenderer;
    public Sprite[] settingsButtonSprites;
    public Sprite[] soundButtonSprites;

    public GameObject buttonCanvas, settingsCanvas;

    public AudioClip sound;

    bool wasPaused = false;

    // Use this for initialization
    void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
	    if(AudioListener.pause)
            soundImageRenderer.sprite = soundButtonSprites[1];

        if (wasPaused) { Time.timeScale = 0; wasPaused = false; }
    }

    public void Click()
    {
        if (PlayerPrefs.GetInt("SettingsCanvasActive") == 0)
        {
            AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 2f);

            settingsImageRenderer.sprite = settingsButtonSprites[1];

            PlayerPrefs.SetInt("SettingsCanvasActive", 1);

            //buttonCanvasFadeOut.Play();
            buttonCanvas.SetActive(false);

            settingsCanvas.SetActive(true);
        }
        else
        {
            AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 2f);

            settingsImageRenderer.sprite = settingsButtonSprites[0];

            PlayerPrefs.SetInt("SettingsCanvasActive", 0);

            buttonCanvas.SetActive(true);

            //settingsCanvasFadeOut.Play();
            settingsCanvas.SetActive(false);
        }

        //    soundBool = PlayerPrefs.GetInt("Sound");
        //    imageRenderer.sprite = sprites[soundBool];
        //    imageRenderer.SetNativeSize();       

    }

    public void StopAllSound()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
            wasPaused = true;
        }

        if (!AudioListener.pause)
        {
            AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 2f);
            soundImageRenderer.sprite = soundButtonSprites[1];
            //PlayerPrefs.SetInt("Sound", 0);

            AudioSource aSource = GameObject.Find("BGMusic").GetComponent<AudioSource>();
            aSource.ignoreListenerPause = true;

            AudioListener.pause = true;
        }

        else
        {
            AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 2f);
            soundImageRenderer.sprite = soundButtonSprites[0];
            //PlayerPrefs.SetInt("Sound", 1);
            AudioListener.pause = false;
        }
    }
}
