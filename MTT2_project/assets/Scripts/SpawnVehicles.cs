using UnityEngine;
using System.Collections;

public class SpawnVehicles : MonoBehaviour {
    public Transform[] mobs = new Transform[4];
    public float[] speed = { -6f, -4f, -6.5f, -10f };
    bool vehicleIsPicked;
    bool canMove;
    int index;

    void Awake()
    {
        canMove = false;
        vehicleIsPicked = false;
        for (int i = 0; i < mobs.Length; i++)
        {
            SpawnMob(mobs[i]);
        }
    }
    void Update()
    {
        if (!vehicleIsPicked)
        {
            StartCoroutine("DoTheThing");
        }

        if (mobs[index] != null) {
            if (canMove)
                moveMob(mobs[index], speed[index]);


            IfGetsOut(mobs[index]);
        }
    }

    public void moveMob(Transform themob, float speed)
    {        
        themob.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
    }

    public void IfGetsOut(Transform theMob)
    {
        if (theMob.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0f, -.25f, 0f)).y)
        {
            SpawnMob(mobs[index]); canMove = false;
        }
    }

    public void SpawnMob(Transform theMob)
    {
        if(theMob != null)
            theMob.position = Camera.main.ViewportToWorldPoint(new Vector3(Mathf.Clamp(Random.value,0.1f,.9f), 1.50f, 10f));
    }

    public IEnumerator DoTheThing()
    {
        vehicleIsPicked = true;
        yield return new WaitForSeconds(6f);
        index = Random.Range(0, 4);
        SpawnMob(mobs[index]);
        canMove = true;
        vehicleIsPicked = false;
    }
}
