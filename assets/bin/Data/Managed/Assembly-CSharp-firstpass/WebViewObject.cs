using System;
using UnityEngine;

public class WebViewObject
{
	private string lastURL;

	private AndroidJavaObject webView;

	private Vector2 offset;

	public Action onDestroy;

	public WebViewObject()
		: this()
	{
	}

	public void Init(string domain = "", string cookieName = "", string cookieValue = "")
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		offset = new Vector2(0f, 0f);
		webView = new AndroidJavaObject("net.gree.unitywebview.WebViewPlugin", new object[0]);
		webView.Call("Init", new object[1]
		{
			"NativeReceiver"
		});
	}

	private void OnDestroy()
	{
		if (webView != null)
		{
			webView.Call("Destroy", new object[0]);
			if (onDestroy != null)
			{
				onDestroy.Invoke();
				onDestroy = null;
			}
		}
	}

	public void SetMargins(int left, int top, int right, int bottom)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (webView != null)
		{
			offset = new Vector2((float)left, (float)top);
			webView.Call("SetMargins", new object[4]
			{
				left,
				top,
				right,
				bottom
			});
		}
	}

	public void SetVisibility(bool v)
	{
		if (webView != null)
		{
			webView.Call("SetVisibility", new object[1]
			{
				v
			});
		}
	}

	public void SetBackgroundColor(Color color)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (webView != null)
		{
			Color32 val = Color32.op_Implicit(color);
			int num = (val.r << 16) & 0xFF0000;
			int num2 = (val.g << 8) & 0xFF00;
			int num3 = val.b & 0xFF;
			int num4 = (int)((val.a << 24) & 4278190080u);
			webView.Call("SetBackgroundColor", new object[1]
			{
				num4 | num | num2 | num3
			});
		}
	}

	public void LoadURL(string url)
	{
		lastURL = url;
		if (webView != null)
		{
			webView.Call("LoadURL", new object[1]
			{
				url
			});
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
			webView.Call("SetCookie", new object[2]
			{
				url,
				cookieName + "=" + cookieValue
			});
		}
	}

	public void EvaluateJS(string js)
	{
		if (webView != null)
		{
			webView.Call("LoadURL", new object[1]
			{
				"javascript:" + js
			});
		}
	}

	public bool canGoBack()
	{
		return webView.Call<bool>("CanGoBack", new object[0]);
	}
}
