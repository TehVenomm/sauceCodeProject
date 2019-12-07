using System;
using UnityEngine;

namespace Network
{
	[Serializable]
	public class GachaGuaranteeCampaignInfo
	{
		public int gachaId;

		public int guaranteeCampaignId;

		public int campaignType;

		public bool completeStatus;

		public int count;

		public int remainCount;

		public int type;

		public int itemId;

		public int param_0;

		public int param_1;

		public string buttonImg;

		public string startAt;

		public string endAt;

		public string description = "";

		public string detailButtonImg = "";

		public string link = "";

		public int userCount;

		public int probabilityChange;

		public int probabilityRarity;

		public int freeGachaReward;

		public bool hasFreeGachaReward;

		public string campaignDetailImg;

		public int crystalNum;

		public bool IsValid()
		{
			if (gachaId > 0 && guaranteeCampaignId > 0)
			{
				return !IsGetInsentive();
			}
			return false;
		}

		public bool IsNextGuaranteed()
		{
			return remainCount == 1;
		}

		public bool IsGetInsentive()
		{
			if (IsSSConfirmed() || IsItemConfirmed())
			{
				return completeStatus;
			}
			return false;
		}

		public string GetButtonImageName()
		{
			if ((IsSSConfirmed() || IsItemConfirmed()) && IsNextGuaranteed() && buttonImg != "")
			{
				return buttonImg;
			}
			if (IsChangeableButtonAndOpenInfo())
			{
				return buttonImg;
			}
			return "";
		}

		public int GetStep()
		{
			return Mathf.Min(userCount + 1, count);
		}

		public int GetImageCount()
		{
			if (IsStepUp() || IsFever())
			{
				return GetStep();
			}
			return remainCount;
		}

		public string GetTitleImageName()
		{
			int imageCount = GetImageCount();
			if (detailButtonImg == "")
			{
				detailButtonImg = "GGC_000000000";
			}
			return detailButtonImg + "_" + imageCount;
		}

		public DateTime GetStartDateTime()
		{
			return DateTime.Parse(startAt);
		}

		public bool IsSSConfirmed()
		{
			return campaignType == 0;
		}

		public bool IsItemConfirmed()
		{
			return campaignType == 1;
		}

		public bool IsStepUp()
		{
			return campaignType == 2;
		}

		public bool IsFever()
		{
			return campaignType == 3;
		}

		public bool IsStepUpWithPresent()
		{
			return campaignType == 4;
		}

		public bool IsChangeableButtonAndOpenInfo()
		{
			if (!IsStepUp() && !IsFever())
			{
				return IsStepUpWithPresent();
			}
			return true;
		}
	}
}
