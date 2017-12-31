using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopTextScript : MonoBehaviour {

    public Text scoreText;
    public Text gamesText;

    // Use this for initialization
    void Start () {
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
        gamesText.text = PlayerPrefs.GetInt("timesPlayed").ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
