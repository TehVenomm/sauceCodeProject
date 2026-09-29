// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.JavaUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;
using System.Reflection;
using UnityEngine;

#nullable disable
namespace GooglePlayGames.Native;

internal static class JavaUtils
{
  private static ConstructorInfo IntPtrConstructor = typeof (AndroidJavaObject).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new Type[1]
  {
    typeof (IntPtr)
  }, (ParameterModifier[]) null);

  internal static AndroidJavaObject JavaObjectFromPointer(IntPtr jobject)
  {
    if (jobject == IntPtr.Zero)
      return (AndroidJavaObject) null;
    return (AndroidJavaObject) JavaUtils.IntPtrConstructor.Invoke(new object[1]
    {
      (object) jobject
    });
  }

  internal static AndroidJavaObject NullSafeCall(
    this AndroidJavaObject target,
    string methodName,
    params object[] args)
  {
    try
    {
      return target.Call<AndroidJavaObject>(methodName, args);
    }
    catch (Exception ex)
    {
      if (ex.Message.Contains("null"))
        return (AndroidJavaObject) null;
      Logger.w("CallObjectMethod exception: " + (object) ex);
      return (AndroidJavaObject) null;
    }
  }
}
