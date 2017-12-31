using UnityEngine;
using System.Collections;
//using ChartboostSDK;

public class TrainMovement : MonoBehaviour {
    //Transform cashedTransform;
    Touch touch;
    public Transform railroad;
    public GameObject deathParticles;
    public GameObject smokeParticles;
    public AudioClip explosion;
    public GameObject gMenu, scoreM;
    public Rigidbody2D train; public float velocityConstant;
    bool gameStarted = false;
    bool gameEnded = false;

    public ScreenWhite white;
    public SimpleCameraShake shake;

    float leftLimit;
    float rightLimit;

    Vector2 screen;
    Vector2 touchPosition;

    public GameObject smokeL, smokeR;
    
    void Start () {
        //Chartboost.cacheInterstitial(CBLocation.GameOver);

        screen = new Vector2(Screen.width, Screen.height);

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 10, 10f));
        railroad.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 10, 10f));

        leftLimit = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x, 0f).x;
        rightLimit = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x, 0f).x;        

        gMenu.SetActive(false);
	}

    void Update()
    {
        if(!gameStarted)
            gameStarted = GameObject.Find("Manager").GetComponent<Setup>().CheckIfGameStarted();

        if (Input.touchCount > 0 && Time.timeScale == 1)
        {
            Touch touch = Input.GetTouch(0);
            #region other method
            if (touch.phase == TouchPhase.Moved && gameStarted)
            {
                //transform.position = new Vector3(transform.position.x + (touch.deltaPosition.x * velocityConstant * Time.deltaTime), transform.position.y, transform.position.z);
                //transform.position += new Vector3(touch.deltaPosition.x * velocityConstant * Time.deltaTime, 0, 0);
                
                #region controladorHumo

                if (GameObject.FindGameObjectWithTag("smoke"))
                    smokeParticles = GameObject.FindGameObjectWithTag("smoke");
                if (touch.deltaPosition.x > 0) //a la derecha
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;

                    //smokeR.SetActive(true); smokeL.SetActive(false);

                    smokeR.GetComponent<ParticleSystem>().enableEmission = true;
                    smokeL.GetComponent<ParticleSystem>().enableEmission = false;

                }
                else if (touch.deltaPosition.x < 0) //a la izquierda
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;

                    //smokeR.SetActive(false); smokeL.SetActive(true);

                    smokeR.GetComponent<ParticleSystem>().enableEmission = false;
                    smokeL.GetComponent<ParticleSystem>().enableEmission = true;
                }
                #endregion

                transform.Translate(new Vector3(((touch.deltaPosition.x/screen.x) * velocityConstant * Time.deltaTime), 0, 0));
                //Debug.Log("" + (touch.deltaPosition.x / screen.x));
                transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), transform.position.y);
            }
            #endregion
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "mob")
        {
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explosion, new Vector2(0f, 0f));

            white.ActivateFlash();
            shake.StartShake();

            gMenu.SetActive(true); scoreM.SetActive(false);
            gameEnded = true;

            PlayerPrefs.SetInt("timesPlayed", PlayerPrefs.GetInt("timesPlayed") + 1);

            int times = PlayerPrefs.GetInt("timesPlayed");
            Debug.Log("" + times);

            Debug.Log("true");

            //if(Chartboost.hasInterstitial(CBLocation.GameOver) && times % 3 == 0)
            //    Chartboost.showInterstitial(CBLocation.GameOver);
        }
    }

    public bool CheckIfGameEnded()
    {
        return gameEnded;
    }
}