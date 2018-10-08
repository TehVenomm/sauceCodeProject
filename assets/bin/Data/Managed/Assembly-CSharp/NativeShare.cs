using System.Collections;
using System.IO;
using UnityEngine;

public class NativeShare : MonoBehaviourSingleton<NativeShare>
{
	public string ScreenshotName = "screenshot.png";

	public void ShareScreenshotWithText()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		string text = Application.get_persistentDataPath() + "/" + ScreenshotName;
		if (File.Exists(text))
		{
			File.Delete(text);
		}
		Application.CaptureScreenshot(ScreenshotName);
		this.StartCoroutine(delayedShare(text));
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
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Expected O, but got Unknown
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		AndroidJavaClass val = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject val2 = new AndroidJavaObject("android.content.Intent", new object[0]);
		val2.Call<AndroidJavaObject>("setAction", new object[1]
		{
			val.GetStatic<string>("ACTION_SEND")
		});
		AndroidJavaClass val3 = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject val4 = val3.CallStatic<AndroidJavaObject>("parse", new object[1]
		{
			"file://" + imagePath
		});
		val2.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			val.GetStatic<string>("EXTRA_STREAM"),
			val4
		});
		val2.Call<AndroidJavaObject>("setType", new object[1]
		{
			"image/png"
		});
		AndroidJavaClass val5 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = val5.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject val6 = val.CallStatic<AndroidJavaObject>("createChooser", new object[2]
		{
			val2,
			subject
		});
		@static.Call("startActivity", new object[1]
		{
			val6
		});
	}
}
