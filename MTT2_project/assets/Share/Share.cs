using UnityEngine;
using System.Collections;

public class Share : MonoBehaviour
{
		public string title;
		public string content;
		static AndroidJavaClass sharePluginClass;

		void Start ()
		{
				sharePluginClass = new AndroidJavaClass ("com.ari.tool.UnityAndroidTool");
//				if (sharePluginClass == null) {
//						Debug.Log ("sharePluginClass es nula");
//				} else {
//						Debug.Log ("sharePluginClass no es nula");
//				}
		}

		void OnMouseDown ()
		{
			CallShare (title, "", content);
		}
		public static void CallShare (string handline, string subject, string text)
		{
//				Debug.Log ("iniciamos la llamada a compartir");
				sharePluginClass.CallStatic ("share", new object[] {
						handline,
						subject,
						text
				});
//				Debug.Log ("terminamos la llamada a compartir");
		}
}
