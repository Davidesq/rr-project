using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class musicPitchChanger : MonoBehaviour {

    public AudioSource aSource;

    public Enemies enemyScript;
    int[] levels;
    int score;
    public bool[] pitchChanged;

    // Use this for initialization
    void Start () {
        aSource = GameObject.Find("BGMusic").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!GameObject.Find("Manager").GetComponent<Setup>().CheckIfGameStarted())
            {
                for (int i = 0; i < pitchChanged.Length; i++) pitchChanged[i] = false;
            }

            enemyScript = GameObject.Find("Manager").GetComponent<Enemies>();
            levels = enemyScript.GetLevels();
            score = enemyScript.GetScore();

            ChangeSongSpeed();
        }
    }

    void ChangeSongSpeed()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (score == levels[i] && !pitchChanged[i] && i != 0 && i != 1)
            {
                pitchChanged[i] = true;
                aSource.pitch += .015f;
            }
        }
    }
}
