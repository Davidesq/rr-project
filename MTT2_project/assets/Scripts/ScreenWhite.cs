using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenWhite : MonoBehaviour {

    public CanvasGroup CG;
    private bool flash = false;

    public float timeConstant;

    void Update()
    {
        if (flash)
        {
            CG.alpha = CG.alpha - timeConstant;
            if (CG.alpha <= 0)
            {
                CG.alpha = 0;
                flash = false;
            }
        }
    }

    public void ActivateFlash()
    {
        flash = true;
        CG.alpha = 1;
    }
}
