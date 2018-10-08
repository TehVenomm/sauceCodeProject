using System.Collections;
using System.IO;
using UnityEngine;

public class GGNativeShare : MonoBehaviourSingleton<GGNativeShare>
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
		new NativeShare().AddFile(screenShotPath, null).Share();
	}
}
