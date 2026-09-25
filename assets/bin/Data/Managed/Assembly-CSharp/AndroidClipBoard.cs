// Decompiled with JetBrains decompiler
// Type: AndroidClipBoard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AndroidClipBoard : iClipBoard
{
  public void SetClipBoard(string s)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AndroidClipBoard.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new AndroidClipBoard.\u003C\u003Ec__DisplayClass0_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.s = s;
    AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.activity = ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    cDisplayClass00.activity.Call("runOnUiThread", new object[1]
    {
      (object) new AndroidJavaRunnable((object) cDisplayClass00, __methodptr(\u003CSetClipBoard\u003Eb__0))
    });
  }
}
