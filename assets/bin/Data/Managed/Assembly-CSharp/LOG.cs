using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum LOG
{
	UNITY,
	COMMON,
	SYSTEM,
	RESOURCE,
	SAVEDATA,
	NETWORK,
	UI,
	EFFECT,
	DATA_TABLE,
	INGAME,
	COOP,
	WEBSOCK,
	LOUNGE,
	OUTGAME,
	GAMESCENE,
	NATIVE,
	DAMAGE_DETAIL,
	EXCEPTION,
	VALUE_CHECK,
	SOCIAL,
	_,
	ALL,
	ERROR
}
public static class Log
{
	public static readonly Color COLOR_ERROR = new Color(1f, 0.5f, 0.2f);

	public static readonly string NON_DATA_NAME = "デ\u30fcタなし";

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
		Warning(LOG.COMMON, str, objs);
	}

	public static void Warning(LOG category, string str, params object[] objs)
	{
	}

	public static void Error(string str, params object[] objs)
	{
		Error(LOG.COMMON, str, objs);
	}

	public static void TableError(object table_class, uint id)
	{
		Error(LOG.COMMON, $"{table_class.GetType().FullName} : 存在しないIDです。\nID = {id}");
	}

	public static void Error(LOG category, string str, params object[] objs)
	{
	}

	public static void Exception(Exception exc)
	{
		Debug.LogException(exc);
	}

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
