using System;

namespace Network
{
	public class GuildRequestItem
	{
		public int slotNo;

		public int crystalNum;

		public int questId;

		public int num;

		public EndDate endAt;

		public EndDate expiredAt;

		public TimeSpan GetQuestRemainTime()
		{
			if (endAt == null)
			{
				return TimeSpan.FromTicks(-1L);
			}
			if (!IsExpired())
			{
				return endAt.CalcRemainTime();
			}
			TimeSpan result = endAt.ConvToDateTime() - expiredAt.ConvToDateTime();
			if (result.TotalSeconds < 0.0)
			{
				return TimeSpan.FromTicks(-1L);
			}
			return result;
		}

		public string GetQuestRemainTimeWithFormat()
		{
			TimeSpan value = GetQuestRemainTime();
			if (value.TotalSeconds < 0.0)
			{
				value = TimeSpan.FromTicks(0L);
			}
			return new DateTime(0L).Add(value).ToString("H:mm:ss");
		}

		public int GetQuestRemainPoint()
		{
			TimeSpan questRemainTime = GetQuestRemainTime();
			if (questRemainTime.TotalSeconds < 0.0)
			{
				return -1;
			}
			return MonoBehaviourSingleton<GuildRequestManager>.I.CalcPointFromTimeSpan(questRemainTime);
		}

		public TimeSpan GetHoundRemainTime()
		{
			if (expiredAt == null)
			{
				return TimeSpan.FromTicks(-1L);
			}
			return expiredAt.CalcRemainTime();
		}

		public string GetHoundRemainTimeWithFormat()
		{
			TimeSpan value = GetHoundRemainTime();
			if (value.TotalSeconds < 0.0)
			{
				value = TimeSpan.FromTicks(0L);
			}
			return new DateTime(0L).Add(value).ToString("H:mm:ss");
		}

		public TimeSpan GetBonusRemainTime()
		{
			TimeSpan t = TimeSpan.FromMinutes(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.GUILD_REQUEST_EARLY_RECEIVE_MIN);
			if (endAt == null)
			{
				return TimeSpan.FromTicks(0L);
			}
			TimeSpan t2 = TimeManager.GetNow() - endAt.ConvToDateTime();
			TimeSpan result = t - t2;
			if (result.TotalSeconds < 0.0)
			{
				result = TimeSpan.FromTicks(0L);
			}
			return result;
		}

		public string GetBonusRemainTimeWithFormat()
		{
			TimeSpan value = GetBonusRemainTime();
			if (value.TotalSeconds < 0.0)
			{
				value = TimeSpan.FromTicks(0L);
			}
			return new DateTime(0L).Add(value).ToString("H:mm:ss");
		}

		public bool IsExpired()
		{
			if (crystalNum == 0)
			{
				return false;
			}
			bool flag = false;
			flag = (GetHoundRemainTime().TotalSeconds > 0.0);
			return !flag;
		}

		public bool IsSortieing()
		{
			return endAt != null;
		}

		public bool IsComplete()
		{
			double totalSeconds = GetQuestRemainTime().TotalSeconds;
			int num = (int)totalSeconds;
			return num <= 0;
		}
	}
}
