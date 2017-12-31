using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class ScoreAchievements : MonoBehaviour {
    int score = 0;
    public string[] achieveCodes = { "CgkIuornqoQQEAIQAg", "CgkIuornqoQQEAIQAw", "CgkIuornqoQQEAIQBA", " CgkIuornqoQQEAIQBQ", 
                                     "CgkIuornqoQQEAIQBg", "CgkIuornqoQQEAIQBw", "CgkIuornqoQQEAIQCA", "CgkIuornqoQQEAIQCQ"};

    int[] achievGoalNumbers;
	
    void Start()
    {
        achievGoalNumbers = GameObject.Find("Manager").GetComponent<Enemies>().GetLevels();
    }

	// Update is called once per frame
	void Update () {
        achievGoalNumbers = GameObject.Find("Manager").GetComponent<Enemies>().GetLevels();
        Check();
	}

    public void Check()
    {        
        for (int i = 0; i < achievGoalNumbers.Length; i++)
        {
            if (score == achievGoalNumbers[i])
            {
                Social.ReportProgress(
                achieveCodes[i], 100.0f,
                (bool success) =>
                {
                });
            }
        }
    }
    
}
