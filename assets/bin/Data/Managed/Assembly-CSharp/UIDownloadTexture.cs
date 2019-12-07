using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(UITexture))]
public class UIDownloadTexture : MonoBehaviour
{
	public string url = "http://www.yourwebsite.com/logo.png";

	public bool pixelPerfect = true;

	private Texture2D mTex;

	private IEnumerator Start()
	{
		UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
		yield return www.SendWebRequest();
		mTex = DownloadHandlerTexture.GetContent(www);
		if (mTex != null)
		{
			UITexture component = GetComponent<UITexture>();
			component.mainTexture = mTex;
			if (pixelPerfect)
			{
				component.MakePixelPerfect();
			}
		}
		www.Dispose();
	}

	private void OnDestroy()
	{
		if (mTex != null)
		{
			Object.Destroy(mTex);
		}
	}
}
