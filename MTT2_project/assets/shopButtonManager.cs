using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class shopButtonManager : MonoBehaviour
{
    string sGameType = "game";
    string sScoreType = "score";
    string sBoolOpen = "isOpen";
    string sSelectedIndex = "selectedTrainSkin";
    string sHiScore = "score";
    string sGamesPlayed = "timesPlayed";

    public GameObject[] buttons;

    public ScreenWhite white;
    public GameObject particles;
    public AudioClip openingSound;
    
    public AudioClip errorSound;
    public AudioClip selectSound;

    // Use this for initialization
    void Start()
    {       
        SpriteChecker();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpriteChecker()
    {
        int hiScore = PlayerPrefs.GetInt("score");
        int games = PlayerPrefs.GetInt("timesPlayed");

        int selected = PlayerPrefs.GetInt(sSelectedIndex);
        Debug.Log("selected: " + selected);
        Debug.Log("length: " + buttons.Length);

        for (int i = 0; i < buttons.Length; i++)
        {
            Debug.Log("selected: " + selected + " i: " + i);
            shopButtonScript script = buttons[i].GetComponent<shopButtonScript>();

            #region check if open
            string selectedIndex = sBoolOpen + i; //make the string which determines the bool stored procedurally in playerprefs        
            bool boolOpen = PlayerPrefs.GetInt(selectedIndex) > 0 ? true : false; //look for that value and turn it into a bool  
            Debug.Log("Checking the following playerpref bool: " + selectedIndex);
            Debug.Log(boolOpen.ToString());
            if (boolOpen) //if it's open, set the button isOpen property to true and change its sprite
                script.isOpen = true;
            
            #endregion
            
            #region condition 1
            if (string.Equals(script.type, sScoreType, System.StringComparison.OrdinalIgnoreCase))
            {
                if (script.threshold <= hiScore) //Put the sprite to show its enabled
                {
                    if (!script.isOpen)
                        buttons[i].GetComponent<Image>().sprite = script.ableSprite;

                    else if (script.isOpen)
                        buttons[i].GetComponent<Image>().sprite = script.openSprite;
                }

                else if (script.threshold > hiScore) // else check to put the unable sprite
                {
                    buttons[i].GetComponent<Image>().sprite = script.unableSprite;
                }
            }
            #endregion

            #region condition 2
            else if (string.Equals(script.type, sGameType, System.StringComparison.OrdinalIgnoreCase))
            {
                if (script.threshold <= games) //Put the sprite to show its enabled
                {
                    if (!script.isOpen) //if not open, available sprite
                        buttons[i].GetComponent<Image>().sprite = script.ableSprite;

                    else if (script.isOpen) //if open, open sprite
                        buttons[i].GetComponent<Image>().sprite = script.openSprite;
                }

                else if (script.threshold > games) // else check to put the unable sprite
                {
                    buttons[i].GetComponent<Image>().sprite = script.unableSprite;
                }
            }
            #endregion                    

            #region check which one is selected
            if (selected == i)
            {
                Debug.Log("entered change selected sprite condition");
                buttons[i].GetComponent<Image>().sprite = script.selectedSprite;
            }
            #endregion

            
        }
    }

    public void OnButtonClick(GameObject button)
    {
        shopButtonScript script = button.GetComponent<shopButtonScript>();

        int hiScore = PlayerPrefs.GetInt("score");
        int games = PlayerPrefs.GetInt("timesPlayed");

        #region if its scoretype
        if (thresholdConditionMet(button))
        {
            if (script.threshold <= hiScore) //Put the sprite to show its enabled
            {
                if (!script.isOpen)
                {
                    script.isOpen = true;

                    #region animate
                    Instantiate(particles, button.transform.position, Quaternion.identity);
                    AudioSource.PlayClipAtPoint(openingSound, new Vector2(0f, 0f));
                    white.ActivateFlash();
                    button.GetComponent<Image>().sprite = script.openSprite;
                    #endregion

                    #region setBool
                    string boolIndex = sBoolOpen + lookForButtonIndex(button).ToString();
                    PlayerPrefs.SetInt(boolIndex, 1); //set bool as an int

                    Debug.Log("Set up open boolean for: " + boolIndex);
                    #endregion
                }
                else if (script.isOpen)
                {
                    AudioSource.PlayClipAtPoint(selectSound, new Vector2(0f, 0f));
                    
                    int selected = lookForButtonIndex(button);
                    PlayerPrefs.SetInt(sSelectedIndex, selected);

                    Debug.Log("Set up selected index for: " + selected);

                    button.GetComponent<Image>().sprite = script.selectedSprite;

                    // ScreenFaderSingleton.Instance.FadeAndGoToHomeScreen();
                }
            }

            else if (!thresholdConditionMet(button)) // else check to put the unable sprite
            {
                AudioSource.PlayClipAtPoint(errorSound, new Vector2(0f, 0f));
            }
        }
        #endregion
    }

    int lookForButtonIndex(GameObject button)
    {
        int index = 0;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == button)
                index = i;
        }

        return index;
    }

    bool thresholdConditionMet(GameObject button) //Simple method to check the button's condition for activation
    {
        int hiScore = PlayerPrefs.GetInt("score");
        int games = PlayerPrefs.GetInt("timesPlayed");

        shopButtonScript script = button.GetComponent<shopButtonScript>();

        if (string.Equals(script.type, sScoreType, System.StringComparison.OrdinalIgnoreCase))
        {
            if (script.threshold <= hiScore) return true;
            else return false;
        }

        else if (string.Equals(script.type, sGameType, System.StringComparison.OrdinalIgnoreCase))
        {
            if (script.threshold <= games) return true;
            else return false;
        }

        else return false;
    }
}

