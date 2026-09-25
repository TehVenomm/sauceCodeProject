// Decompiled with JetBrains decompiler
// Type: TimeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class TimeManager : MonoBehaviourSingleton<TimeManager>
{
  private DateTime? currentTime;
  private float elapsedTime;
  private float _timeScale;

  public TimeManager.STOP_FLAG stopFlags { get; private set; }

  public static float timeScale
  {
    get
    {
      return MonoBehaviourSingleton<TimeManager>.IsValid() ? MonoBehaviourSingleton<TimeManager>.I._timeScale : Time.timeScale;
    }
    set
    {
      if (!MonoBehaviourSingleton<TimeManager>.IsValid())
        return;
      MonoBehaviourSingleton<TimeManager>.I._timeScale = value;
      if (MonoBehaviourSingleton<TimeManager>.I.IsStop())
        return;
      Time.timeScale = value;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    Time.timeScale = 1f;
    this._timeScale = Time.timeScale;
  }

  public void SetStop(TimeManager.STOP_FLAG flag, bool is_stop)
  {
    if (is_stop)
      this.stopFlags |= flag;
    else
      this.stopFlags &= ~flag;
    Time.timeScale = this.IsStop() ? 0.0f : this._timeScale;
  }

  public bool IsStop() => this.stopFlags != 0;

  public static DateTime GetNow()
  {
    return !MonoBehaviourSingleton<TimeManager>.IsValid() || !MonoBehaviourSingleton<TimeManager>.I.currentTime.HasValue ? DateTime.Now : MonoBehaviourSingleton<TimeManager>.I.currentTime.Value.AddSeconds((double) MonoBehaviourSingleton<TimeManager>.I.elapsedTime);
  }

  public static void SetServerTime(string time)
  {
    DateTime result;
    if (!DateTime.TryParse(time, out result))
      return;
    MonoBehaviourSingleton<TimeManager>.I.currentTime = new DateTime?(result);
    MonoBehaviourSingleton<TimeManager>.I.elapsedTime = 0.0f;
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
    return TimeManager.GetRemainTimeToText(TimeManager.GetRemainTime(targetDateTime), digitNum);
  }

  public static TimeSpan GetRemainTime(string targetDateTime)
  {
    DateTime result;
    return DateTime.TryParse(targetDateTime, out result) ? TimeManager.GetRemainTime(result) : TimeSpan.FromDays(99.0);
  }

  private static TimeSpan GetRemainTime(DateTime targetDateTime)
  {
    return targetDateTime - TimeManager.GetNow();
  }

  private void Update() => this.elapsedTime += Time.unscaledDeltaTime;

  public static DateTime CombineDateAndTime(DateTime date, DateTime time)
  {
    return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
  }

  [Flags]
  public enum STOP_FLAG
  {
    DEBUG_MANAGER = 1,
    DEBUG_FUNC = 2,
  }
}
