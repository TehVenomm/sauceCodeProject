using System;
using UnityEngine;

public class WebViewObject : MonoBehaviour
{
	private string lastURL;

	private AndroidJavaObject webView;

	private Vector2 offset;

	public Action onDestroy;

	public void Init(string domain = "", string cookieName = "", string cookieValue = "")
	{
		offset = new Vector2(0f, 0f);
		webView = new AndroidJavaObject("net.gree.unitywebview.WebViewPlugin");
		webView.Call("Init", "NativeReceiver");
	}

	private void OnDestroy()
	{
		if (webView != null)
		{
			webView.Call("Destroy");
			if (onDestroy != null)
			{
				onDestroy();
				onDestroy = null;
			}
		}
	}

	public void SetMargins(int left, int top, int right, int bottom)
	{
		if (webView != null)
		{
			offset = new Vector2((float)left, (float)top);
			webView.Call("SetMargins", left, top, right, bottom);
		}
	}

	public void SetVisibility(bool v)
	{
		if (webView != null)
		{
			webView.Call("SetVisibility", v);
		}
	}

	public void SetBackgroundColor(Color color)
	{
		if (webView != null)
		{
			Color32 color2 = color;
			int num = (color2.r << 16) & 0xFF0000;
			int num2 = (color2.g << 8) & 0xFF00;
			int num3 = color2.b & 0xFF;
			int num4 = (int)((color2.a << 24) & 4278190080u);
			webView.Call("SetBackgroundColor", num4 | num | num2 | num3);
		}
	}

	public void LoadURL(string url)
	{
		lastURL = url;
		if (webView != null)
		{
			webView.Call("LoadURL", url);
		}
	}

	public void Refresh()
	{
		if (lastURL == null)
		{
			return;
		}
	}

	public void SetCookie(string url, string cookieName, string cookieValue)
	{
		if (webView != null)
		{
			webView.Call("SetCookie", url, cookieName + "=" + cookieValue);
		}
	}

	public void EvaluateJS(string js)
	{
		if (webView != null)
		{
			webView.Call("LoadURL", "javascript:" + js);
		}
	}

	public bool canGoBack()
	{
		return webView.Call<bool>("CanGoBack", new object[0]);
	}
}
