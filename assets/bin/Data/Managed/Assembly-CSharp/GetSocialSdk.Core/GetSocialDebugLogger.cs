using System;
using System.Diagnostics;
using UnityEngine;

namespace GetSocialSdk.Core
{
	[DebuggerStepThrough]
	internal static class GetSocialDebugLogger
	{
		[Conditional("DEVELOPMENT_BUILD")]
		public static void D(string message, params object[] arguments)
		{
			if (arguments != null && arguments.Length > 0)
			{
				Debug.Log((object)string.Format(message, arguments));
			}
			else
			{
				Debug.Log((object)message);
			}
		}

		[Conditional("DEVELOPMENT_BUILD")]
		public static void I(string message, params object[] arguments)
		{
			if (arguments != null && arguments.Length > 0)
			{
				Debug.Log((object)string.Format(message, arguments));
			}
			else
			{
				Debug.Log((object)message);
			}
		}

		[Conditional("DEVELOPMENT_BUILD")]
		public static void W(string message, params object[] arguments)
		{
			if (arguments != null && arguments.Length > 0)
			{
				Debug.LogWarning((object)string.Format(message, arguments));
			}
			else
			{
				Debug.LogWarning((object)message);
			}
		}

		[Conditional("DEVELOPMENT_BUILD")]
		public static void E(string message, params object[] arguments)
		{
			if (arguments != null && arguments.Length > 0)
			{
				Debug.LogError((object)string.Format(message, arguments));
			}
			else
			{
				Debug.LogError((object)message);
			}
		}

		[Conditional("DEVELOPMENT_BUILD")]
		public static void Ex(Exception ex, string message, params object[] arguments)
		{
			Debug.LogException(ex);
		}
	}
}
