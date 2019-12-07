using System;
using UnityEngine;

public class TimeManager : MonoBehaviourSingleton<TimeManager>
{
	[Flags]
	public enum STOP_FLAG
	{
		DEBUG_MANAGER = 0x1,
		DEBUG_FUNC = 0x2
	}

	private DateTime? currentTime;

	private float elapsedTime;

	private float _timeScale;

	public STOP_FLAG stopFlags
	{
		get;
		private set;
	}

	public static float timeScale
	{
		get
		{
			if (MonoBehaviourSingleton<TimeManager>.IsValid())
			{
				return MonoBehaviourSingleton<TimeManager>.I._timeScale;
			}
			return Time.timeScale;
		}
		set
		{
			if (MonoBehaviourSingleton<TimeManager>.IsValid())
			{
				MonoBehaviourSingleton<TimeManager>.I._timeScale = value;
				if (!MonoBehaviourSingleton<TimeManager>.I.IsStop())
				{
					Time.timeScale = value;
				}
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();
		Time.timeScale = 1f;
		_timeScale = Time.timeScale;
	}

	public void SetStop(STOP_FLAG flag, bool is_stop)
	{
		if (is_stop)
		{
			stopFlags |= flag;
		}
		else
		{
			stopFlags &= ~flag;
		}
		Time.timeScale = (IsStop() ? 0f : _timeScale);
	}

	public bool IsStop()
	{
		return stopFlags != (STOP_FLAG)0;
	}

	public static DateTime GetNow()
	{
		if (!MonoBehaviourSingleton<TimeManager>.IsValid() || !MonoBehaviourSingleton<TimeManager>.I.currentTime.HasValue)
		{
			return DateTime.Now;
		}
		return MonoBehaviourSingleton<TimeManager>.I.currentTime.Value.AddSeconds(MonoBehaviourSingleton<TimeManager>.I.elapsedTime);
	}

	public static void SetServerTime(string time)
	{
		if (DateTime.TryParse(time, out DateTime result))
		{
			MonoBehaviourSingleton<TimeManager>.I.currentTime = result;
			MonoBehaviourSingleton<TimeManager>.I.elapsedTime = 0f;
		}
	}

	public static string GetRemainTimeToText(TimeSpan span, int digitNum = 3)
	{
		string text = "";
		if (span.Seconds > 0)
		{
			span = span.Add(TimeSpan.FromMinutes(1.0));
		}
		int num = 0;
		if (span.Days > 0 && num < digitNum)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 0u), span.Days);
			num++;
		}
		if (span.Hours > 0 && num < digitNum)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 1u), span.Hours);
			num++;
		}
		if (span.Minutes > 0 && num < digitNum)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2u), span.Minutes);
			num++;
		}
		if (text == "")
		{
			return string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2u), 0);
		}
		return text;
	}

	public static string GetRemainTimeToText(string targetDateTime, int digitNum = 3)
	{
		return GetRemainTimeToText(GetRemainTime(targetDateTime), digitNum);
	}

	public static TimeSpan GetRemainTime(string targetDateTime)
	{
		if (DateTime.TryParse(targetDateTime, out DateTime result))
		{
			return GetRemainTime(result);
		}
		return TimeSpan.FromDays(99.0);
	}

	private static TimeSpan GetRemainTime(DateTime targetDateTime)
	{
		return targetDateTime - GetNow();
	}

	private void Update()
	{
		elapsedTime += Time.unscaledDeltaTime;
	}

	public static DateTime CombineDateAndTime(DateTime date, DateTime time)
	{
		return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
	}
}
