using UnityEngine;
using System.Collections;

public class TimesPlayedAchievements : MonoBehaviour {

    int timesPlayed = 0;
    int[] goals = {5,20};
    string[] achieveCodes = { "CgkIuornqoQQEAIQCg", "CgkIuornqoQQEAIQCw"};
	
    void Update () {
        timesPlayed = PlayerPrefs.GetInt("timesPlayed");
        Check();
	}

    public void Check()
    {
        for (int i = 0; i < goals.Length; i++)
        {
            if (timesPlayed > goals[i])
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
