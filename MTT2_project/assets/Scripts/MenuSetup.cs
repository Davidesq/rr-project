using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using ChartboostSDK;

public class MenuSetup : MonoBehaviour {

    bool IsConnectedToGoogleServices = false;
    bool scoresUpdated = false;

    void Awake()
    {
        PlayGamesPlatform.Activate();
        Chartboost.cacheInterstitial(CBLocation.HomeScreen);
    }

    void Start () {
        IsConnectedToGoogleServices = false;

        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                IsConnectedToGoogleServices = true;
            }
        });
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Time.realtimeSinceStartup % 60 == 0 && SceneManager.GetActiveScene().buildIndex == 0)
        {
            Chartboost.showInterstitial(CBLocation.HomeScreen);
        }

        if (!IsConnectedToGoogleServices)
            StartCoroutine("TryGooglePlayServicesConnection");
    }

    //public void MoveCloud(float speed, Transform cloud)
    //{
    //    cloud.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
    //}

    //public void ReplaceCloud(Transform cloud)
    //{
    //    if (cloud.position.x > Camera.main.ViewportToWorldPoint(new Vector3(1.15f, Random.value, 10f)).x)
    //    {
    //        cloud.position = Camera.main.ViewportToWorldPoint(new Vector3(-.15f, Random.value, 10f));
    //    }
    //}

    public bool ConnectToGoogleServices()
    {
        PlayGamesPlatform.Activate();
        if (!IsConnectedToGoogleServices)
        {
            Social.localUser.Authenticate((bool success) => {

            });
        }

        return IsConnectedToGoogleServices;
    }

    private IEnumerator CheckConnectionToMasterServer()
    {
        Ping pingMasterServer = new Ping("8.8.8.8");
        Debug.Log(pingMasterServer.ip);
        float startTime = Time.time;
        while (!pingMasterServer.isDone && Time.time < startTime + 5.0f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (pingMasterServer.isDone)
        {
            ConnectToGoogleServices();
        }
    }

    private IEnumerator TryGooglePlayServicesConnection()
    {
        IsConnectedToGoogleServices = true;

        yield return new WaitForSeconds(30f);

        Social.localUser.Authenticate((bool success) => {
            if (!success)
            {
                IsConnectedToGoogleServices = false;
            }
        });
    }
}
