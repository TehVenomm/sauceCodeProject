using System.Collections;
using System.IO;
using UnityEngine;

public class NativeShare : MonoBehaviourSingleton<NativeShare>
{
	public string ScreenshotName = "screenshot.png";

	public void ShareScreenshotWithText()
	{
		string text = Application.persistentDataPath + "/" + ScreenshotName;
		if (File.Exists(text))
		{
			File.Delete(text);
		}
		Application.CaptureScreenshot(ScreenshotName);
		StartCoroutine(delayedShare(text));
	}

	private IEnumerator delayedShare(string screenShotPath)
	{
		while (!File.Exists(screenShotPath))
		{
			yield return (object)new WaitForSeconds(0.05f);
		}
		Share(screenShotPath, string.Empty, string.Empty);
	}

	public void Share(string imagePath, string url, string subject = "")
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent");
		androidJavaObject.Call<AndroidJavaObject>("setAction", new object[1]
		{
			androidJavaClass.GetStatic<string>("ACTION_SEND")
		});
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject androidJavaObject2 = androidJavaClass2.CallStatic<AndroidJavaObject>("parse", new object[1]
		{
			"file://" + imagePath
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_STREAM"),
			androidJavaObject2
		});
		androidJavaObject.Call<AndroidJavaObject>("setType", new object[1]
		{
			"image/png"
		});
		AndroidJavaClass androidJavaClass3 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass3.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject3 = androidJavaClass.CallStatic<AndroidJavaObject>("createChooser", new object[2]
		{
			androidJavaObject,
			subject
		});
		@static.Call("startActivity", androidJavaObject3);
	}
}
