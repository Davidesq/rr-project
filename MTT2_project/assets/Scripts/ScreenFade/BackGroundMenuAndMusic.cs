using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// This Class controls the Back Ground Music, whether or not its Muted, And Saves that state in playerPrefs.
/// </summary>
public class BackGroundMenuAndMusic : MonoBehaviour
{
    public static BackGroundMenuAndMusic BGMusic;       //StatocReference setup - this object will persist from Scene1->Forward.(DoNotDestroyOnLoad)
    public AudioSource aSource;                         //reference to the audio source that background music plays from    
    public float localSoundVolume;                      //localSoundVolume var (is a fraction of the Globale TemporaryGameVars.soundVol)
    public bool muteAudio;                              //Boolean muteAudio controls BG Music State(Stored in PlayerPrefs as int 1-Yes / 0-NO)
    public Canvas menuCanvas;                           //Reference to the menuCanvas( the canvas that pops up when M or Menu is pressed(has a mute button))

    public Image musicImageRenderer;
    public Sprite[] musicButtonSprites;

    public Enemies enemyScript;
    int[] levels;
    int score;
    public bool[] pitchChanged;

    public AudioClip sound;

    // Use this for initialization
    void Start () 
	{
       //Setup a Singleton Like Object
        if(BGMusic == null)
        {
            DontDestroyOnLoad(GameObject.Find("BGMusic"));
            BGMusic = this;
        }
        //and make sure it persists
        //if another is found.. destroy so only one exists.
        else if (BGMusic != this)
        {
            Destroy(GameObject.Find("BGMusic"));
        }

        //set localSoundVol to that of half of the Static soundVolume
        localSoundVolume = TemporaryGameVars.soundVolume * 0.4f;
        //get audio source from this object
        aSource = GameObject.Find("BGMusic").GetComponent<AudioSource>();
        //set audio source volume to the new localSoundVolume
        aSource.volume = localSoundVolume;

        //if we retrieve the PlayerPrefs int and its 1(yes), we set our muted boolean to true, and our mute
        //property on the audio source to true.
        //if (TemporaryGameVars.mutedVolume == 1)
        //{
        //    muteAudio = true;
        //    aSource.mute = true;
        //}
        //else if its 0, we set both to false... this is how we used playerprefs with and int value/
        //else if (TemporaryGameVars.mutedVolume == 0)
        //{
        //    muteAudio = false;
        //    aSource.mute = false;

        //}
        //on game start the menuCanvas should NOT be showing
        //menuCanvas.enabled = false;
    }

    void Update()
    {
        //if (SceneManager.GetActiveScene().buildIndex == 1)
        //{
        //    if (!GameObject.Find("Manager").GetComponent<Setup>().CheckIfGameStarted())
        //    {
        //        for (int i = 0; i < pitchChanged.Length; i++) pitchChanged[i] = false;
        //    }

        //    enemyScript = GameObject.Find("Manager").GetComponent<Enemies>();
        //    levels = enemyScript.GetLevels();
        //    score = enemyScript.GetScore();

        //    ChangeSongSpeed();                
        //}
        
            ChangeButtonSprite();
    }

    /// <summary>
    /// This Method Mutes the Audio and Remembers the State, because it stores the state in playerPrefs as an Integer.
    /// </summary>
    public void MuteAudio()
    {
        //    if (SceneManager.GetActiveScene().buildIndex == 1)
        //    {
        //        score = enemyScript.GetScore();
        //        ChangeSongSpeed();
        //    }

        //flip boolean state
        muteAudio = !muteAudio;
        //if its true, we set audioSources mute property to true, and PlayerPrefs.SetInt Key-mutedAudio Value-1("true")
        if (muteAudio)
        {
            AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 200f);
            musicImageRenderer.sprite = musicButtonSprites[1];
            aSource.mute = true;
            PlayerPrefs.SetInt("mutedAudio", 1);
        }
        // else if its false we do the opposite
        else
        {
            AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 200f);
            musicImageRenderer.sprite = musicButtonSprites[0];
            aSource.mute = false;
            PlayerPrefs.SetInt("mutedAudio", 0);

        }
    }

    void ChangeSongSpeed()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (score == levels[i] && !pitchChanged[i] && i!=0 && i!=1)
            {
                pitchChanged[i] = true;
                aSource.pitch += .015f;
            }
        }
    }

    void ChangeButtonSprite()
    {
        if (aSource.mute)
        {
            muteAudio = true;
            musicImageRenderer.sprite = musicButtonSprites[1];
        }
    }
}
