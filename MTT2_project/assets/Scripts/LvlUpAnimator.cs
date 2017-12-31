using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class LvlUpAnimator : MonoBehaviour {

    public Animator anim;

    public Text text;

    public Enemies enem;
    static int score;

    public bool[] levelColorChanged = { false, false, false };
    public int[] levels;

    public string[] strings;

    void Awake()
    {
        Uncheck();
        score = 0;
        levels = enem.GetLevels();
    }

	// Update is called once per frame
	void Update () {
        Check(); 
        score = enem.GetScore();

        if (anim.isInitialized)
        {
            anim.SetInteger("score", score);
            //Debug.Log("score = " + score);
            ChangeText();
        }
                  
    }

    private void ChangeText()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (score == levels[i])
            {
                text.text = strings[i];
            }
        }
    }

    private void Check()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (score == levels[i])
            {
                //Debug.Log("CONDITION PASSED: SCORE " + score + " == " + levels[i]);
                SetBool(i);
            }
        }
        
    }

    void SetBool(int id)
    {
        switch (id)
        {
            case 0:
                anim.SetBool("x", true);
                break;
            case 1:
                anim.SetBool("bool02", true);
                break;
            case 2:
                anim.SetBool("bool03", true);
                break;
            case 3:
                anim.SetBool("bool04", true);
                break;
            case 4:
                anim.SetBool("bool05", true);
                break;
            default:
                break;
        }

    }

    void Uncheck()
    {
        anim.SetBool("x", false);
        anim.SetBool("bool02", false);
        anim.SetBool("bool03", false);
        anim.SetBool("bool04", false);
        anim.SetBool("bool05", false);
    }
}
