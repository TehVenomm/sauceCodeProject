using System;

namespace Network
{
	[Serializable]
	public class AutoModeStatus
	{
		private double remainTime;

		public void Init(double remainTime_)
		{
			remainTime = remainTime_;
		}

		public void SubTime(double subTime_)
		{
			remainTime -= subTime_;
		}

		public bool IsRemain()
		{
			return remainTime > 0.0;
		}

		public string GetRemainTime()
		{
			if (remainTime < 0.0)
			{
				return "00:00:00";
			}
			int num = (int)remainTime % 60;
			int num2 = (int)(remainTime / 60.0) % 60;
			int num3 = (int)(remainTime / 3600.0);
			return $"{num3:D2}:{num2:D2}:{num:D2}";
		}
	}
}
