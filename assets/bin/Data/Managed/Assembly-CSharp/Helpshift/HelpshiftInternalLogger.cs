// Decompiled with JetBrains decompiler
// Type: Helpshift.HelpshiftInternalLogger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace Helpshift;

public class HelpshiftInternalLogger
{
  private static string TAG = "HelpshiftUnityPlugin";
  private static AndroidJavaClass hsInternalLogger = new AndroidJavaClass("com.helpshift.util.HSLogger");

  public static void d(string message)
  {
    ((AndroidJavaObject) HelpshiftInternalLogger.hsInternalLogger).CallStatic(nameof (d), new object[2]
    {
      (object) HelpshiftInternalLogger.TAG,
      (object) message
    });
  }

  public static void e(string message)
  {
    ((AndroidJavaObject) HelpshiftInternalLogger.hsInternalLogger).CallStatic(nameof (e), new object[2]
    {
      (object) HelpshiftInternalLogger.TAG,
      (object) message
    });
  }

  public static void w(string message)
  {
    ((AndroidJavaObject) HelpshiftInternalLogger.hsInternalLogger).CallStatic(nameof (w), new object[2]
    {
      (object) HelpshiftInternalLogger.TAG,
      (object) message
    });
  }

  public static void f(string message)
  {
    ((AndroidJavaObject) HelpshiftInternalLogger.hsInternalLogger).CallStatic(nameof (f), new object[2]
    {
      (object) HelpshiftInternalLogger.TAG,
      (object) message
    });
  }
}
