using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UITexture))]
public class UIDownloadTexture : MonoBehaviour
{
	public string url = "http://www.yourwebsite.com/logo.png";

	public bool pixelPerfect = true;

	private Texture2D mTex;

	public UIDownloadTexture()
		: this()
	{
	}

	private IEnumerator Start()
	{
		WWW www = new WWW(url);
		yield return www;
		mTex = www.get_texture();
		if (mTex != null)
		{
			UITexture component = this.GetComponent<UITexture>();
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
