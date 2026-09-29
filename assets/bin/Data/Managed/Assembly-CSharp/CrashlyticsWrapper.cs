// Decompiled with JetBrains decompiler
// Type: CrashlyticsWrapper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CrashlyticsWrapper
{
  private static AndroidJavaClass cl;

  private static AndroidJavaClass Crashlytics
  {
    get
    {
      if (CrashlyticsWrapper.cl == null)
        CrashlyticsWrapper.cl = new AndroidJavaClass("com.crashlytics.android.Crashlytics");
      return CrashlyticsWrapper.cl;
    }
  }

  public static void SetBool(string key, bool value)
  {
    key = key ?? "";
    ((AndroidJavaObject) CrashlyticsWrapper.Crashlytics).CallStatic("setBool", new object[2]
    {
      (object) key,
      (object) value
    });
  }

  public static void SetString(string key, string value)
  {
    key = key ?? "";
    value = value ?? "";
    ((AndroidJavaObject) CrashlyticsWrapper.Crashlytics).CallStatic("setString", new object[2]
    {
      (object) key,
      (object) value
    });
  }

  public static void SetInt(string key, int value)
  {
    key = key ?? "";
    ((AndroidJavaObject) CrashlyticsWrapper.Crashlytics).CallStatic("setInt", new object[2]
    {
      (object) key,
      (object) value
    });
  }

  public static void SetUserId(int id)
  {
    ((AndroidJavaObject) CrashlyticsWrapper.Crashlytics).CallStatic("setUserEmail", new object[1]
    {
      (object) id.ToString()
    });
  }

  public static void SetUserName(string name)
  {
    name = name ?? "";
    ((AndroidJavaObject) CrashlyticsWrapper.Crashlytics).CallStatic("setUserName", new object[1]
    {
      (object) name
    });
  }

  public static void ReportException(string report)
  {
    report = report ?? "";
    ((AndroidJavaObject) CrashlyticsWrapper.Crashlytics).CallStatic("logException", new object[1]
    {
      (object) new AndroidJavaObject("java.lang.Exception", new object[1]
      {
        (object) report
      })
    });
  }
}
