using System;
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
	public static void StopWatch(string log_str)
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
}
