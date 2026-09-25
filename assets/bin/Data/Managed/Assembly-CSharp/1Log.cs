// Decompiled with JetBrains decompiler
// Type: Log
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
public static class Log
{
  public static readonly Color COLOR_ERROR = new Color(1f, 0.5f, 0.2f);
  public static readonly string NON_DATA_NAME = "データなし";
  private static Dictionary<string, Stopwatch> watchLists = new Dictionary<string, Stopwatch>();

  public static bool enabled => false;

  [Conditional("ENABLE_LOG")]
  public static void d(string str, params object[] objs)
  {
  }

  [Conditional("ENABLE_LOG")]
  public static void d(LOG category, string str, params object[] objs)
  {
  }

  public static void Warning(string str, params object[] objs)
  {
    Log.Warning(LOG.COMMON, str, objs);
  }

  public static void Warning(LOG category, string str, params object[] objs)
  {
  }

  public static void Error(string str, params object[] objs) => Log.Error(LOG.COMMON, str, objs);

  public static void TableError(object table_class, uint id)
  {
    Log.Error(LOG.COMMON, $"{table_class.GetType().FullName} : 存在しないIDです。\nID = {id}");
  }

  public static void Error(LOG category, string str, params object[] objs)
  {
  }

  public static void Exception(System.Exception exc) => Debug.LogException(exc);

  [Conditional("ENABLE_LOG")]
  public static void StartWatch()
  {
  }

  [Conditional("ENABLE_LOG")]
  public static void StopWatch(string log_str, bool show_log = true)
  {
  }

  [Conditional("ENABLE_LOG")]
  public static void StopAndStartWatch(string log_str)
  {
  }

  [Conditional("ENABLE_LOG")]
  public static void ClearWatch()
  {
  }

  [Conditional("ENABLE_LOG")]
  public static void StartWatch(string watch_name, bool reset = true, bool show_log = true)
  {
  }

  [Conditional("ENABLE_LOG")]
  public static void StopMyWatch(string watch_name, bool show_log = true)
  {
  }

  [Conditional("ENABLE_LOG")]
  public static void StopWatch(string watch_name, string log_str, bool show_log = true)
  {
  }

  [Conditional("ENABLE_LOG")]
  public static void StopAndStartWatch(string watch_name, string log_str)
  {
  }

  [Conditional("ENABLE_LOG")]
  public static void StopAndStartWatchWithoutReset(string watch_name, string log_str)
  {
  }

  [Conditional("ENABLE_LOG")]
  public static void ClearAllWatch()
  {
  }
}
