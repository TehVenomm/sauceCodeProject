using GooglePlayGames.OurUtils;
using System;

namespace GooglePlayGames.Native
{
	internal static class CallbackUtils
	{
		internal unsafe static Action<T> ToOnGameThread<T>(Action<T> toConvert)
		{
			if (toConvert == null)
			{
				return delegate
				{
				};
			}
			return delegate
			{
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Expected O, but got Unknown
				_003CToOnGameThread_003Ec__AnonStorey820<T>._003CToOnGameThread_003Ec__AnonStorey821 _003CToOnGameThread_003Ec__AnonStorey;
				PlayGamesHelperObject.RunOnGameThread(new Action((object)_003CToOnGameThread_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			};
		}

		internal unsafe static Action<T1, T2> ToOnGameThread<T1, T2>(Action<T1, T2> toConvert)
		{
			if (toConvert == null)
			{
				return new Action<_003F, _003F>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			_003CToOnGameThread_003Ec__AnonStorey822<T1, T2> _003CToOnGameThread_003Ec__AnonStorey;
			return new Action<_003F, _003F>((object)_003CToOnGameThread_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}

		internal unsafe static Action<T1, T2, T3> ToOnGameThread<T1, T2, T3>(Action<T1, T2, T3> toConvert)
		{
			if (toConvert == null)
			{
				return new Action<_003F, _003F, _003F>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			_003CToOnGameThread_003Ec__AnonStorey824<T1, T2, T3> _003CToOnGameThread_003Ec__AnonStorey;
			return new Action<_003F, _003F, _003F>((object)_003CToOnGameThread_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
	}
}
