using System;

namespace GooglePlayGames.OurUtils
{
	public class Logger
	{
		private static bool debugLogEnabled;

		private static bool warningLogEnabled = true;

		public static bool DebugLogEnabled
		{
			get
			{
				return debugLogEnabled;
			}
			set
			{
				debugLogEnabled = value;
			}
		}

		public static bool WarningLogEnabled
		{
			get
			{
				return warningLogEnabled;
			}
			set
			{
				warningLogEnabled = value;
			}
		}

		public unsafe static void d(string msg)
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			if (debugLogEnabled)
			{
				_003Cd_003Ec__AnonStorey805 _003Cd_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003Cd_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		public unsafe static void w(string msg)
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			if (warningLogEnabled)
			{
				_003Cw_003Ec__AnonStorey806 _003Cw_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003Cw_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		public unsafe static void e(string msg)
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			if (warningLogEnabled)
			{
				_003Ce_003Ec__AnonStorey807 _003Ce_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003Ce_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}

		public static string describe(byte[] b)
		{
			return (b != null) ? ("byte[" + b.Length + "]") : "(null)";
		}

		private static string ToLogMessage(string prefix, string logType, string msg)
		{
			return string.Format("{0} [Play Games Plugin DLL] {1} {2}: {3}", prefix, DateTime.Now.ToString("MM/dd/yy H:mm:ss zzz"), logType, msg);
		}
	}
}
