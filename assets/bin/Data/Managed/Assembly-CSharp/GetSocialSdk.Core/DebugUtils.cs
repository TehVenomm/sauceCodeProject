using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace GetSocialSdk.Core
{
	[DebuggerStepThrough]
	public static class DebugUtils
	{
		public static void LogMethodCall(MethodBase method, params object[] values)
		{
			ParameterInfo[] parameters = method.GetParameters();
			StringBuilder stringBuilder = new StringBuilder().AppendFormat("Method call: {0}(", method.Name);
			for (int i = 0; i < parameters.Length; i++)
			{
				stringBuilder.AppendFormat("{0}: {1}", parameters[i].Name, values[i] ?? "null");
				if (i < parameters.Length - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append(")");
			Debug.Log((object)stringBuilder);
		}

		public static void TraceMethodCall()
		{
			try
			{
				throw new Exception("THIS EXCEPTION IS HARMLESS. TRACING METHOD CALL.");
				IL_000b:;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				return "null";
			}
			if (((ICollection<KeyValuePair<TKey, TValue>>)dictionary).Count == 0)
			{
				return "{}";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			foreach (TKey item in (IEnumerable<TKey>)dictionary.Keys)
			{
				stringBuilder.Append("{");
				stringBuilder.Append(item);
				stringBuilder.Append("=");
				stringBuilder.Append(dictionary[item]);
				stringBuilder.Append("}\n");
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		public static string ToDebugString<T>(this IList<T> list)
		{
			if (list == null)
			{
				return "null";
			}
			if (((ICollection<T>)list).Count == 0)
			{
				return "[]";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.Append("\n");
			foreach (T item in (IEnumerable<T>)list)
			{
				stringBuilder.Append(item);
				stringBuilder.Append("\n");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
