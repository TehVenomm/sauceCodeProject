// Decompiled with JetBrains decompiler
// Type: Helpshift.HelpshiftAndroidLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace Helpshift;

public class HelpshiftAndroidLog
{
  private static AndroidJavaClass logger = new AndroidJavaClass("com.helpshift.support.Log");

  private HelpshiftAndroidLog()
  {
  }

  public static int v(string tag, string log)
  {
    return ((AndroidJavaObject) HelpshiftAndroidLog.logger).CallStatic<int>(nameof (v), new object[2]
    {
      (object) tag,
      (object) log
    });
  }

  public static int d(string tag, string log)
  {
    return ((AndroidJavaObject) HelpshiftAndroidLog.logger).CallStatic<int>(nameof (d), new object[2]
    {
      (object) tag,
      (object) log
    });
  }

  public static int i(string tag, string log)
  {
    return ((AndroidJavaObject) HelpshiftAndroidLog.logger).CallStatic<int>(nameof (i), new object[2]
    {
      (object) tag,
      (object) log
    });
  }

  public static int w(string tag, string log)
  {
    return ((AndroidJavaObject) HelpshiftAndroidLog.logger).CallStatic<int>(nameof (w), new object[2]
    {
      (object) tag,
      (object) log
    });
  }

  public static int e(string tag, string log)
  {
    return ((AndroidJavaObject) HelpshiftAndroidLog.logger).CallStatic<int>(nameof (e), new object[2]
    {
      (object) tag,
      (object) log
    });
  }
}
