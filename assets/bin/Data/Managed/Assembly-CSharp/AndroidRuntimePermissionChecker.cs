// Decompiled with JetBrains decompiler
// Type: AndroidRuntimePermissionChecker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AndroidRuntimePermissionChecker
{
  private const string ANDROID_CONTEXT_CLASS_NAME = "com.unity3d.player.UnityPlayer";

  private static AndroidJavaObject GetActivity()
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
      return ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity");
  }

  private static bool IsAndroidMOrGreater()
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Build$VERSION"))
      return ((AndroidJavaObject) androidJavaClass).GetStatic<int>("SDK_INT") >= 23;
  }

  public static bool CheckPermissions(string[] permissions)
  {
    bool flag = true;
    if (AndroidRuntimePermissionChecker.IsAndroidMOrGreater() && permissions != null)
    {
      for (int index = 0; index < permissions.Length; ++index)
        flag = AndroidRuntimePermissionChecker.CheckPermission(permissions[index]);
    }
    return flag;
  }

  private static bool CheckPermission(string permission)
  {
    using (AndroidJavaObject activity = AndroidRuntimePermissionChecker.GetActivity())
      return activity.Call<int>("checkSelfPermission", new object[1]
      {
        (object) permission
      }) == 0;
  }

  public static void RequestPermission(string[] permissiions)
  {
    if (!AndroidRuntimePermissionChecker.IsAndroidMOrGreater())
      return;
    using (AndroidJavaObject activity = AndroidRuntimePermissionChecker.GetActivity())
      activity.Call("requestPermissions", new object[2]
      {
        (object) permissiions,
        (object) 0
      });
  }
}
