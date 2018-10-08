using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal static class Check
	{
		public static class Argument
		{
			public static void IsNotNull(object argument, string argumentName, string message = null)
			{
				if (message == null)
				{
					message = $"{argumentName} must not be null";
				}
				if (argument == null)
				{
					Debug.LogWarning((object)message);
				}
			}

			public static void IsStrNotNullOrEmpty(string argument, string argumentName, string message = null)
			{
				if (message == null)
				{
					message = $"{argumentName} must not be null or empty";
				}
				if (string.IsNullOrEmpty(argument))
				{
					Debug.LogWarning((object)message);
				}
			}

			public static void IsNotNegative(int argument, string argumentName)
			{
				if (argument < 0)
				{
					Debug.LogWarning((object)(argumentName + " must not be negative."));
				}
			}
		}

		public delegate bool Condition();

		public static void IfTrue(Condition condition, string message = "")
		{
			if (!condition())
			{
				throw new ArgumentException(message);
			}
		}

		public static void IfTrue(bool condition, string message = "")
		{
			if (!condition)
			{
				Debug.LogWarning((object)message);
			}
		}
	}
}
