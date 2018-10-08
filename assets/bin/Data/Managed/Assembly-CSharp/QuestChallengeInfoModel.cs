using Network;

public class QuestChallengeInfoModel : BaseModel
{
	public class Param
	{
		public int enable;

		public int satisfy;

		public string message;

		public int num;

		public int firstClear;

		public int isRankingEvent = -1;

		public ShadowCount oldShadowCount;

		public ShadowCount currentShadowCount;

		public bool IsEnable()
		{
			return enable == 1;
		}

		public bool IsSatisfy()
		{
			return satisfy == 1;
		}

		public bool NotClaer()
		{
			if (!IsEnable() || !IsSatisfy() || firstClear == 1)
			{
				return false;
			}
			return true;
		}

		public bool IsRankingEvent()
		{
			return isRankingEvent == 1;
		}
	}

	public class RequestSendForm
	{
	}

	public static string URL = "ajax/quest/challenge-info";

	public Param result = new Param();
}
