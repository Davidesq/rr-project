using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
    public Enemies enemies;
    public static int score;

    public Color[] colors;
    int[] levels;
    public bool[] colorChanged;

    public Text text;

	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
        score = 0;

        levels = enemies.GetLevels();
	}
	
	// Update is called once per frame
	void Update () {
        score = enemies.GetScore();
        text.text = "" + score;

        ChangeColor();        
	}

    public int GetScore()
    {
        return score;
    }

    void ChangeColor()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (score == levels[i] && !colorChanged[i])
            {
                text.color = colors[i];
            }
        }
    }

    public Color[] GetColors()
    {
        return colors;
    }
}
