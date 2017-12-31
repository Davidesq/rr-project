using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour {
    #region variables
    public Setup setup;
    public TrainMovement player;

    public GameObject[] prefabs;

    public Transform[] firstWave = new Transform[3];
    public Transform[] currentWave;
        
    float[] WaveSpeed = { -4f, -6f, -8f };    
    public int[] levels = {5, 15, 30, 50, 100, 150, 200, 300};

    public static int score = 0; 
    public static int levelCounter;

    public AudioClip point, levelUp;

    bool gameStarted = false;
    bool gameEnded = false;

    bool[] levelSpeedUpgrade = new bool[7];


    #endregion

    void Start()
    {
        currentWave = firstWave;

        levelCounter = 0;
        score = 0;

        for (int i = 0; i < currentWave.Length; i++)
        {
            SpawnMob(currentWave[i]);
        }              
    }

	void Update () {
        gameStarted = setup.CheckIfGameStarted();

        gameEnded = player.CheckIfGameEnded();

        if (gameStarted)
        {
            for (int i = 0; i < currentWave.Length; i++)
			{			           
                MoveMob(currentWave[i], WaveSpeed[i]);
                IfGetsOut(ref currentWave[i]);

                if (!gameEnded)
                {
                    CheckForSpeed();
                }
            }
        }
	}

    public void SpawnMob(Transform theMob)
    {
        theMob.position = Camera.main.ViewportToWorldPoint(new Vector3(Mathf.Clamp(Random.value, 0.1f, .9f), 1.50f, 10f));
    }

    public void MoveMob(Transform theMob, float speed)
    {
        theMob.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
    }

    public void IfGetsOut(ref Transform theMob)
    {
        if (theMob.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0f, -.35f, 0f)).y)
        {
            SpawnMob(theMob);
            CheckForVehicleChange(ref theMob);
            if (!gameEnded)
            {
                score++;
                AudioSource.PlayClipAtPoint(point, new Vector2(0, 0), 2f);
            }
        }
    }

    void ReplaceVehicle(int prefabNumber, ref Transform trans)
    {
        int x = Mathf.RoundToInt(Random.value); //saque un valor entre 1 y 0, si es 1, cambie alguno de los que esta en el arreglo de vehiculos caminando por un camion (prefabs[0]), u otros

        //int x = 1;

        switch (x)
        {
            case (1):

                GameObject toDestroy = trans.gameObject;
                trans = (Instantiate(prefabs[prefabNumber], toDestroy.transform.position, Quaternion.identity) as GameObject).transform;
                Destroy(toDestroy);
                break;
        }
    }

    void CheckForVehicleChange(ref Transform trans)
    {
        switch (score)
        {
            case (50):
                ReplaceVehicle(1, ref trans);
                break;

            case (75):
                ReplaceVehicle(0, ref trans);
                break;

            case (100):
                ReplaceVehicle(0, ref trans);
                break;

            case (150):
                ReplaceVehicle(2, ref trans);
                break;

            case (175):
                ReplaceVehicle(2, ref trans);
                break;
        }
    }

    public void CheckForSpeed()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (score == levels[i] && !levelSpeedUpgrade[i])
            {
                AudioSource.PlayClipAtPoint(levelUp, new Vector2(0, 0), 1f);
                levelSpeedUpgrade[i] = true;
                levelCounter++;
                //if (levels[i] <= 50)
                //{
                AugmentVelocity(WaveSpeed, 1.35f);
                //}
                //else
                ////{
                //    firstWaveSpeed[0] -= 1f;
                //    firstWaveSpeed[1] -= 1f;
                //    firstWaveSpeed[2] -= 1f;
                ////}
                break;
            }
        }
    }

    void AugmentVelocity(float[] velo, float toIncrease)
    {
        for (int i = 0; i < velo.Length; i++)
        {
            velo[i] -= toIncrease;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int ReturnLevel()
    {
        return levelCounter;
    }

    public int[] GetLevels()
    {
        return levels;
    }
}
