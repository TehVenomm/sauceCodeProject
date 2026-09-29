// Decompiled with JetBrains decompiler
// Type: WebViewObject
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using System;
using UnityEngine;

#nullable disable
public class WebViewObject : MonoBehaviour
{
  private string lastURL;
  private AndroidJavaObject webView;
  private Vector2 offset;
  public Action onDestroy;

  public void Init(string domain = "", string cookieName = "", string cookieValue = "")
  {
    this.offset = new Vector2(0.0f, 0.0f);
    this.webView = new AndroidJavaObject("net.gree.unitywebview.WebViewPlugin", Array.Empty<object>());
    this.webView.Call(nameof (Init), new object[1]
    {
      (object) "NativeReceiver"
    });
  }

  private void OnDestroy()
  {
    if (this.webView == null)
      return;
    this.webView.Call("Destroy", Array.Empty<object>());
    if (this.onDestroy == null)
      return;
    this.onDestroy();
    this.onDestroy = (Action) null;
  }

  public void SetMargins(int left, int top, int right, int bottom)
  {
    if (this.webView == null)
      return;
    this.offset = new Vector2((float) left, (float) top);
    this.webView.Call(nameof (SetMargins), new object[4]
    {
      (object) left,
      (object) top,
      (object) right,
      (object) bottom
    });
  }

  public void SetVisibility(bool v)
  {
    if (this.webView == null)
      return;
    this.webView.Call(nameof (SetVisibility), new object[1]
    {
      (object) v
    });
  }

  public void SetBackgroundColor(Color color)
  {
    if (this.webView == null)
      return;
    Color32 color32 = Color32.op_Implicit(color);
    this.webView.Call(nameof (SetBackgroundColor), new object[1]
    {
      (object) ((int) ((long) ((int) color32.a << 24) & 4278190080L /*0xFF000000*/) | (int) color32.r << 16 /*0x10*/ & 16711680 /*0xFF0000*/ | (int) color32.g << 8 & 65280 | (int) color32.b & (int) byte.MaxValue)
    });
  }

  public void LoadURL(string url)
  {
    this.lastURL = url;
    if (this.webView == null)
      return;
    this.webView.Call(nameof (LoadURL), new object[1]
    {
      (object) url
    });
  }

  public void Refresh()
  {
    string lastUrl = this.lastURL;
  }

  public void SetCookie(string url, string cookieName, string cookieValue)
  {
    if (this.webView == null)
      return;
    this.webView.Call(nameof (SetCookie), new object[2]
    {
      (object) url,
      (object) $"{cookieName}={cookieValue}"
    });
  }

  public void EvaluateJS(string js)
  {
    if (this.webView == null)
      return;
    this.webView.Call("LoadURL", new object[1]
    {
      (object) ("javascript:" + js)
    });
  }

  public bool canGoBack() => this.webView.Call<bool>("CanGoBack", Array.Empty<object>());
}
