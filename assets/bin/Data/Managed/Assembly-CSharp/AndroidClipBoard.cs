using UnityEngine;

public class AndroidClipBoard : iClipBoard
{
	public void SetClipBoard(string s)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		activity.Call("runOnUiThread", (AndroidJavaRunnable)delegate
		{
			AndroidJavaObject androidJavaObject = activity.Call<AndroidJavaObject>("getSystemService", new object[1]
			{
				"clipboard"
			});
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.content.ClipData");
			AndroidJavaObject androidJavaObject2 = androidJavaClass2.CallStatic<AndroidJavaObject>("newPlainText", new object[2]
			{
				"simple text",
				s
			});
			androidJavaObject.Call("setPrimaryClip", androidJavaObject2);
		});
	}
}
