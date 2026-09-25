// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.OurUtils.Logger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace GooglePlayGames.OurUtils;

public class Logger
{
  private static bool debugLogEnabled = false;
  private static bool warningLogEnabled = true;

  public static bool DebugLogEnabled
  {
    get => Logger.debugLogEnabled;
    set => Logger.debugLogEnabled = value;
  }

  public static bool WarningLogEnabled
  {
    get => Logger.warningLogEnabled;
    set => Logger.warningLogEnabled = value;
  }

  public static void d(string msg)
  {
    if (!Logger.debugLogEnabled)
      return;
    PlayGamesHelperObject.RunOnGameThread((Action) (() => Debug.Log((object) Logger.ToLogMessage(string.Empty, "DEBUG", msg))));
  }

  public static void w(string msg)
  {
    if (!Logger.warningLogEnabled)
      return;
    PlayGamesHelperObject.RunOnGameThread((Action) (() => Debug.LogWarning((object) Logger.ToLogMessage("!!!", "WARNING", msg))));
  }

  public static void e(string msg)
  {
    if (!Logger.warningLogEnabled)
      return;
    PlayGamesHelperObject.RunOnGameThread((Action) (() => Debug.LogWarning((object) Logger.ToLogMessage("***", "ERROR", msg))));
  }

  public static string describe(byte[] b) => b != null ? $"byte[{(object) b.Length}]" : "(null)";

  private static string ToLogMessage(string prefix, string logType, string msg)
  {
    return $"{prefix} [Play Games Plugin DLL] {DateTime.Now.ToString("MM/dd/yy H:mm:ss zzz")} {logType}: {msg}";
  }
}
