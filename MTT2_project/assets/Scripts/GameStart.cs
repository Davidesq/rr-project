using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public AudioClip sound;

    public void ChangeToScene(int sceneToChangeTo)
    {
        AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 1f);
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
