using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class ScoreAtEndGame : MonoBehaviour {
    public TrainMovement player;
    public Enemies enemyScript;

    public static int score;
    public static int bestScore;

    public Text currentScore;
    public Text bestText;

    public AudioClip newBest;
    public AudioClip counterSound;

    public Animator anim;

    public Score scoreScript;
    Color[] colors;
    public int[] levels;

    bool gameEnded; bool alreadyDisplayed = false;

    // Use this for initialization
    void Start()
    {
        colors = scoreScript.GetColors();
        levels = enemyScript.GetLevels();
        alreadyDisplayed = false;
        bestText.text = "" + PlayerPrefs.GetInt("score");

        if (Social.localUser.authenticated)
        {
            scoreChecker();
        }
    }

    // Update is called once per frame
    void Update () {
        anim.SetBool("new", false);

        #region game ended
        gameEnded = player.CheckIfGameEnded();

        if (gameEnded && !alreadyDisplayed)
        {
            SetVariables();
            StartCoroutine("UpdateScore");            
        }
        #endregion       
    }

    void SetVariables()
    {
        score = enemyScript.GetScore();
        currentScore.text = "" + 0;
        bestText.text = "" + PlayerPrefs.GetInt("score");
        bestScore = PlayerPrefs.GetInt("score");
    }

    void scoreChecker()
    {

        PlayGamesPlatform.Instance.LoadScores(
             "CgkIuornqoQQEAIQAA",
             LeaderboardStart.PlayerCentered,
             1,
             LeaderboardCollection.Public,
             LeaderboardTimeSpan.AllTime,
         (LeaderboardScoreData data) =>
         {
             int registeredScore = (int)data.PlayerScore.value;
             int savedScore = PlayerPrefs.GetInt("score");

             //update score
             if (savedScore > registeredScore)
             {
                 Social.ReportScore(savedScore, "CgkIuornqoQQEAIQAA", (bool success) =>
                 {
                     // handle success or failure
                 });

                 bestScore = savedScore;
             }

             else if (registeredScore > savedScore)
             {
                 PlayerPrefs.SetInt("score", registeredScore);
                 bestScore = registeredScore;
             }

             if (!data.Valid)
                 bestText.text = "" + PlayerPrefs.GetInt("score");

         });

    }

    IEnumerator UpdateScore()
    {
        alreadyDisplayed = true;
        yield return new WaitForSeconds(.5f);
        for (int i = 1; i <= score; i++)
        {
            AudioSource.PlayClipAtPoint(counterSound, new Vector2(0f, 0f), 1f);
            currentScore.text = "" + i;
            ChangeColor();
            yield return new WaitForSeconds(0.03f);
        }

        #region otra cosa
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("score", score);
            Social.ReportScore(score, "CgkIuornqoQQEAIQAA", (bool success) =>
            {
                // handle success or failure
            });

            yield return new WaitForSeconds(0.05f);
            yield return StartCoroutine("CheckForBest");
        }
        #endregion


        
        
    }

    IEnumerator CheckForBest()
    {
        anim.SetBool("new", true);
        bestText.text = score + " NEW!";
        AudioSource.PlayClipAtPoint(newBest, new Vector2(0f, 0f), 1f);
        yield return null;
    }

    void ChangeColor()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (int.Parse(currentScore.text) == levels[i])
            {
                currentScore.color = colors[i];
            }
        }
    }
}
