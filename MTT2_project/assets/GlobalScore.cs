using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GlobalScore : MonoBehaviour {

    public int topOne;
    Text text;
    public GameObject trophy;
    bool done = false;

    // Use this for initialization
    void Start()
    {
        text = this.GetComponentInParent<Text>();
        trophy.SetActive(false);


    }

    //   // Update is called once per frame
    //   void Update () {
    //       if (Social.localUser.authenticated && !done)
    //       {
    //           done = true;
    //           scoreChecker();
    //       }
    //   }

    //   void scoreChecker()
    //   {
    //       PlayGamesPlatform.Instance.LoadScores(
    //            "CgkIuornqoQQEAIQAA",
    //            LeaderboardStart.TopScores,
    //            1,
    //            LeaderboardCollection.Public,
    //            LeaderboardTimeSpan.AllTime,
    //        (LeaderboardScoreData data) =>
    //        {
    //            topOne = (int)data.Scores.GetValue(data.Scores.GetUpperBound(1));

    //            //if(topOne > 0)
    //            //{
    //                trophy.SetActive(true);
    //                text.text = "" + topOne;
    //            //}
    //        });


    //   }
}
