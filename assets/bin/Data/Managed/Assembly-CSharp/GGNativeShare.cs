using System.Collections;
using System.IO;
using UnityEngine;

public class GGNativeShare : MonoBehaviourSingleton<GGNativeShare>
{
	public string ScreenshotName = "screenshot.png";

	public void ShareScreenshotWithText()
	{
		string text = Application.persistentDataPath + "/" + ScreenshotName;
		if (File.Exists(text))
		{
			File.Delete(text);
		}
		ScreenCapture.CaptureScreenshot(ScreenshotName);
		StartCoroutine(delayedShare(text));
	}

	private IEnumerator delayedShare(string screenShotPath)
	{
		while (!File.Exists(screenShotPath))
		{
			yield return new WaitForSeconds(0.05f);
		}
		new NativeShare().AddFile(screenShotPath).Share();
	}
}
