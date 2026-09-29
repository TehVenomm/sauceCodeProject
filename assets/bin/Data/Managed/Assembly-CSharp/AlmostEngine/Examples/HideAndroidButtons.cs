// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.HideAndroidButtons
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Examples;

public class HideAndroidButtons : MonoBehaviour
{
  private const int SYSTEM_UI_FLAG_IMMERSIVE_STICKY = 4096 /*0x1000*/;
  private const int SYSTEM_UI_FLAG_HIDE_NAVIGATION = 2;
  private const int SYSTEM_UI_FLAG_FULLSCREEN = 4;
  private AndroidJavaObject decorView;

  private void Awake()
  {
    this.decorView = ((AndroidJavaObject) new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("getWindow", Array.Empty<object>()).Call<AndroidJavaObject>("getDecorView", Array.Empty<object>());
    this.TurnImmersiveModeOn();
  }

  private void OnApplicationFocus(bool focusStatus)
  {
    if (!focusStatus)
      return;
    this.TurnImmersiveModeOn();
  }

  private void TurnImmersiveModeOn()
  {
    this.decorView.Call("setSystemUiVisibility", new object[1]
    {
      (object) 4102
    });
  }

  private void OnDestroy() => this.decorView.Dispose();
}
