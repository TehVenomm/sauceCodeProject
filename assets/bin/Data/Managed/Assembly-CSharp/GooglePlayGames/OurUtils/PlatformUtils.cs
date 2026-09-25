// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.OurUtils.PlatformUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace GooglePlayGames.OurUtils;

public static class PlatformUtils
{
  public static bool Supported
  {
    get
    {
      AndroidJavaObject androidJavaObject1 = ((AndroidJavaObject) new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("getPackageManager", Array.Empty<object>());
      AndroidJavaObject androidJavaObject2;
      try
      {
        androidJavaObject2 = androidJavaObject1.Call<AndroidJavaObject>("getLaunchIntentForPackage", new object[1]
        {
          (object) "com.google.android.play.games"
        });
      }
      catch (Exception ex)
      {
        return false;
      }
      return androidJavaObject2 != null;
    }
  }
}
