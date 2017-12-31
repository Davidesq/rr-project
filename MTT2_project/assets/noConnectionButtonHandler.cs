using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System;

public class noConnectionButtonHandler : MonoBehaviour {

    public Button[] buttons;
    public Sprite[] sprites;
    Sprite[] lastSprites;
    bool done = false;

    // Use this for initialization
    void Start () {
        lastSprites = new Sprite[sprites.Length];
        Check();
	}
	
	// Update is called once per frame
	void Update () {
        Check();
	}

    void Check()
    {
        if (!Social.localUser.authenticated && !done)
            ChangeSpritesAndDisable();
        else if (Social.localUser.authenticated)
            ChangeSpritesAndDisable(false);
    }

    private void ChangeSpritesAndDisable(bool doIt = true)
    {
        if (doIt)
        {
            done = true;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
                lastSprites[i] = buttons[i].GetComponent<Image>().sprite;
                buttons[i].GetComponent<Image>().sprite = sprites[i];
                Debug.Log("yeah");
            }
        }
        else
        {
            done = false;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = true;
                buttons[i].GetComponent<Image>().sprite = lastSprites[i];
            }
        }
    }
}
