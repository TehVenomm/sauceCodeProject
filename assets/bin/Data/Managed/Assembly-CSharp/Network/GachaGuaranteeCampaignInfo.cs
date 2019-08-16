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

		public string description = string.Empty;

		public string detailButtonImg = string.Empty;

		public string link = string.Empty;

		public int userCount;

		public int probabilityChange;

		public int probabilityRarity;

		public int freeGachaReward;

		public bool hasFreeGachaReward;

		public string campaignDetailImg;

		public int crystalNum;

		public bool IsValid()
		{
			return gachaId > 0 && guaranteeCampaignId > 0 && !IsGetInsentive();
		}

		public bool IsNextGuaranteed()
		{
			return remainCount == 1;
		}

		public bool IsGetInsentive()
		{
			return (IsSSConfirmed() || IsItemConfirmed()) && completeStatus;
		}

		public string GetButtonImageName()
		{
			if ((IsSSConfirmed() || IsItemConfirmed()) && IsNextGuaranteed() && buttonImg != string.Empty)
			{
				return buttonImg;
			}
			if (IsChangeableButtonAndOpenInfo())
			{
				return buttonImg;
			}
			return string.Empty;
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
			if (detailButtonImg == string.Empty)
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
			return IsStepUp() || IsFever() || IsStepUpWithPresent();
		}
	}
}
