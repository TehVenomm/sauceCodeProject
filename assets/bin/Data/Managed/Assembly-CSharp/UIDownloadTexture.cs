// Decompiled with JetBrains decompiler
// Type: UIDownloadTexture
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
[RequireComponent(typeof (UITexture))]
public class UIDownloadTexture : MonoBehaviour
{
  public string url = "http://www.yourwebsite.com/logo.png";
  public bool pixelPerfect = true;
  private Texture2D mTex;

  private IEnumerator Start()
  {
    UnityWebRequest www = UnityWebRequestTexture.GetTexture(this.url);
    yield return (object) www.SendWebRequest();
    this.mTex = DownloadHandlerTexture.GetContent(www);
    if (Object.op_Inequality((Object) this.mTex, (Object) null))
    {
      UITexture component = ((Component) this).GetComponent<UITexture>();
      component.mainTexture = (Texture) this.mTex;
      if (this.pixelPerfect)
        component.MakePixelPerfect();
    }
    www.Dispose();
  }

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.mTex, (Object) null))
      return;
    Object.Destroy((Object) this.mTex);
  }
}
