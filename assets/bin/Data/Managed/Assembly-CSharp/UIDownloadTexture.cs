using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UITexture))]
public class UIDownloadTexture : MonoBehaviour
{
	public string url = "http://www.yourwebsite.com/logo.png";

	public bool pixelPerfect = true;

	private Texture2D mTex;

	private IEnumerator Start()
	{
		WWW www = new WWW(url);
		yield return (object)www;
		mTex = www.texture;
		if ((Object)mTex != (Object)null)
		{
			UITexture ut = GetComponent<UITexture>();
			ut.mainTexture = mTex;
			if (pixelPerfect)
			{
				ut.MakePixelPerfect();
			}
		}
		www.Dispose();
	}

	private void OnDestroy()
	{
		if ((Object)mTex != (Object)null)
		{
			Object.Destroy(mTex);
		}
	}
}
