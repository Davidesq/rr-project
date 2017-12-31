using UnityEngine;
using System.Collections;

public class LvlUpAnimation : MonoBehaviour {
    public Animator anim;
    public Enemies enem;
    static int score = 0;
	// Update is called once per frame
	void Update () {
        score = enem.GetScore();

        anim.SetInteger("score", score);
	}
}
