// Decompiled with JetBrains decompiler
// Type: Helpshift.HelpshiftLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace Helpshift;

public class HelpshiftLog
{
  public static int v(string tag, string log) => HelpshiftAndroidLog.v(tag, log);

  public static int d(string tag, string log) => HelpshiftAndroidLog.d(tag, log);

  public static int i(string tag, string log) => HelpshiftAndroidLog.i(tag, log);

  public static int w(string tag, string log) => HelpshiftAndroidLog.w(tag, log);

  public static int e(string tag, string log) => HelpshiftAndroidLog.e(tag, log);
}
