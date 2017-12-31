using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class Setup : MonoBehaviour {
    public GameObject instruction;
    public static bool gameStarted;

    public CanvasRenderer fadingCanvas;

    public GameObject panelThingy;

    public GameObject scoreCounter;

    public GameObject pauseButton;

    
    void Start()
    {
        fadingCanvas = GameObject.Find("FaderCanvas(DontDestroyOnLoad)").GetComponentInChildren<CanvasRenderer>();

        gameStarted = false;
    }
	
	void Update () {
        Screen.orientation = ScreenOrientation.Portrait;
        if (Input.touchCount>0 && !gameStarted && fadingCanvas.GetAlpha() == 0)
        {
            gameStarted = true;
            Destroy(instruction);
            Destroy(panelThingy);
            scoreCounter.SetActive(true);
            pauseButton.SetActive(true);
        }
    }

    public bool CheckIfGameStarted()
    {
        return gameStarted;
    }

    
}
