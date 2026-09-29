// Decompiled with JetBrains decompiler
// Type: GoGameTimeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class GoGameTimeManager : MonoBehaviourSingleton<GoGameTimeManager>
{
  private DateTime? currentTime;
  private float elapsedTime;

  public GoGameTimeManager.STOP_FLAG stopFlags { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    Time.timeScale = 1f;
  }

  public void SetStop(GoGameTimeManager.STOP_FLAG flag, bool is_stop)
  {
    if (is_stop)
      this.stopFlags |= flag;
    else
      this.stopFlags &= ~flag;
    Time.timeScale = this.IsStop() ? 0.0f : 1f;
  }

  public bool IsStop() => this.stopFlags != 0;

  public static DateTime GetNow()
  {
    return !MonoBehaviourSingleton<GoGameTimeManager>.IsValid() || !MonoBehaviourSingleton<GoGameTimeManager>.I.currentTime.HasValue ? DateTime.Now : MonoBehaviourSingleton<GoGameTimeManager>.I.currentTime.Value.AddSeconds((double) MonoBehaviourSingleton<GoGameTimeManager>.I.elapsedTime);
  }

  public static void SetServerTime(string time)
  {
    DateTime result;
    if (!DateTime.TryParse(time, out result))
      return;
    MonoBehaviourSingleton<GoGameTimeManager>.I.currentTime = new DateTime?(result);
    MonoBehaviourSingleton<GoGameTimeManager>.I.elapsedTime = 0.0f;
  }

  public static string GetRemainTimeToText(TimeSpan span, int digitNum = 3)
  {
    string str = "";
    if (span.Seconds > 0)
      span = span.Add(TimeSpan.FromMinutes(1.0));
    int num1 = 0;
    if (span.Days > 0 && num1 < digitNum)
    {
      str += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 0U), (object) span.Days);
      ++num1;
    }
    if (span.Hours > 0 && num1 < digitNum)
    {
      str += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 1U), (object) span.Hours);
      ++num1;
    }
    if (span.Minutes > 0 && num1 < digitNum)
    {
      str += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2U), (object) span.Minutes);
      int num2 = num1 + 1;
    }
    return str == "" ? string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2U), (object) 0) : str;
  }

  public static string GetRemainTimeToText(string targetDateTime, int digitNum = 3)
  {
    return GoGameTimeManager.GetRemainTimeToText(GoGameTimeManager.GetRemainTime(targetDateTime), digitNum);
  }

  public static TimeSpan GetRemainTime(string targetDateTime)
  {
    DateTime result;
    return DateTime.TryParse(targetDateTime, out result) ? GoGameTimeManager.GetRemainTime(result) : TimeSpan.FromDays(99.0);
  }

  private static TimeSpan GetRemainTime(DateTime targetDateTime)
  {
    return targetDateTime - GoGameTimeManager.GetNow();
  }

  private void Update() => this.elapsedTime += Time.unscaledDeltaTime;

  public static DateTime CombineDateAndTime(DateTime date, DateTime time)
  {
    return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
  }

  public static bool HasValue()
  {
    return MonoBehaviourSingleton<GoGameTimeManager>.IsValid() && MonoBehaviourSingleton<GoGameTimeManager>.I.currentTime.HasValue;
  }

  [Flags]
  public enum STOP_FLAG
  {
    DEBUG_MANAGER = 1,
    DEBUG_FUNC = 2,
  }
}
