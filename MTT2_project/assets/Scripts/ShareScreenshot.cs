using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ShareScreenshot : MonoBehaviour
{
    // public
    // private
    private bool isProcessing = false;
    public Button buttonShare;
    public string mensaje;

    public string gameName;
    public string gameLink;

    public AudioClip sound;

    //function called from a button
    public void ButtonShare()
    {
        AudioSource.PlayClipAtPoint(sound, new Vector2(0, 0), 2f);

        buttonShare.interactable = false;
        if (!isProcessing)
        {
            StartCoroutine(Share());
        }
    }
    public IEnumerator Share()
    {
        MakeMessage();

        isProcessing = true;
        // wait for graphics to render
        yield return new WaitForEndOfFrame();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        // create the texture
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        // put buffer into texture
        screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
        // apply
        screenTexture.Apply();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        byte[] dataToSave = screenTexture.EncodeToPNG();
        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        File.WriteAllBytes(destination, dataToSave);
        if (!Application.isEditor)
        {
            // block to open the file and share it ------------START
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "" + mensaje);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            currentActivity.Call("startActivity", intentObject);
        }
        isProcessing = false;
        buttonShare.interactable = true;
    }

    void MakeMessage()
    {
        int score = GameObject.Find("Manager").GetComponent<Enemies>().GetScore();

        mensaje = "I GOT A SCORE OF " + score + " ON #" + gameName + "! Check it out! " + gameLink;
    }
}