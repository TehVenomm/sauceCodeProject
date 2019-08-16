using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GachaManager : MonoBehaviourSingleton<GachaManager>
{
	private int currentGachaIndex;

	public GachaList gachaData
	{
		get;
		private set;
	}

	public List<GachaResult> gachaResultList
	{
		get;
		private set;
	}

	public GachaResult gachaResultBonus
	{
		get;
		private set;
	}

	public bool enableFeverDirector
	{
		get;
		private set;
	}

	public GachaList.Gacha selectGacha
	{
		get;
		private set;
	}

	public GACHA_TYPE selectGachaType
	{
		get;
		private set;
	}

	public GACHA_TYPE selectGachaRealType
	{
		get;
		private set;
	}

	public GachaGuaranteeCampaignInfo selectGachaGuarantee
	{
		get;
		private set;
	}

	public GachaManager()
	{
		gachaData = new GachaList();
	}

	public bool IsMultiResult()
	{
		return gachaResultList.Count > 1;
	}

	public bool IsExistNextGachaResult()
	{
		return gachaResultList.Count > currentGachaIndex + 1;
	}

	public bool IsResultBonus()
	{
		return gachaResultBonus != null;
	}

	public void ResetGachaIndex()
	{
		currentGachaIndex = 0;
		enableFeverDirector = false;
	}

	public void IncrementGachaIndex()
	{
		currentGachaIndex++;
	}

	public void SetNextFever()
	{
		enableFeverDirector = true;
	}

	public GachaResult GetCurrentGachaResult()
	{
		if (gachaResultList == null)
		{
			return null;
		}
		return gachaResultList[currentGachaIndex];
	}

	public GachaResult GetNextGachaResult()
	{
		if (gachaResultList == null)
		{
			return null;
		}
		if (gachaResultList.Count <= currentGachaIndex + 1)
		{
			return null;
		}
		return gachaResultList[currentGachaIndex + 1];
	}

	public void ResetGachaType()
	{
		selectGachaType = GACHA_TYPE.QUEST;
		selectGachaRealType = GACHA_TYPE.QUEST;
	}

	public bool IsSelectTutorialGacha()
	{
		return selectGachaRealType == GACHA_TYPE.TUTORIAL1 || selectGachaRealType == GACHA_TYPE.TUTORIAL2;
	}

	public void Dirty()
	{
	}

	public void SelectGacha(int gachaId, int gachaIndex)
	{
		selectGacha = FindGacha(gachaId, gachaIndex, out GACHA_TYPE gacha_type, out GACHA_TYPE real_type, out GachaGuaranteeCampaignInfo guaranteeInfo);
		selectGachaType = gacha_type;
		selectGachaRealType = real_type;
		selectGachaGuarantee = guaranteeInfo;
	}

	private GachaList.Gacha FindGacha(int gachaId, int gachaIndex, out GACHA_TYPE gacha_type, out GACHA_TYPE real_type, out GachaGuaranteeCampaignInfo guaranteeInfo)
	{
		GachaList.Gacha result = null;
		gacha_type = (GACHA_TYPE)0;
		real_type = (GACHA_TYPE)0;
		guaranteeInfo = new GachaGuaranteeCampaignInfo();
		for (int i = 0; i < gachaData.types.Count; i++)
		{
			GachaList.GachaType gachaType = gachaData.types[i];
			for (int j = 0; j < gachaType.groups.Count; j++)
			{
				GachaList.GachaGroup gachaGroup = gachaType.groups[j];
				if (gachaGroup.gachas.Count <= gachaIndex)
				{
					continue;
				}
				GachaList.Gacha gacha = gachaGroup.gachas[gachaIndex];
				if (gacha.gachaId == gachaId)
				{
					result = gacha;
					gacha_type = gachaType.ViewType;
					real_type = gachaType.Type;
					guaranteeInfo = gachaGroup.gachaGuaranteeCampaignInfo.Find((GachaGuaranteeCampaignInfo info) => info.gachaId == gachaId);
					if (guaranteeInfo == null)
					{
						guaranteeInfo = new GachaGuaranteeCampaignInfo();
					}
					return result;
				}
			}
		}
		return result;
	}

	public void SetSelectGachaGuarantee(GachaGuaranteeCampaignInfo guaranteeInfo)
	{
		selectGachaGuarantee = guaranteeInfo;
	}

	public RARITY_TYPE GetMaxRarity()
	{
		List<GachaResult.GachaReward> reward = GetCurrentGachaResult().reward;
		return GetMaxRarity(reward);
	}

	public RARITY_TYPE GetMaxRarity(List<GachaResult.GachaReward> rewardList)
	{
		RARITY_TYPE rARITY_TYPE = RARITY_TYPE.D;
		if (rewardList != null)
		{
			int i = 0;
			for (int count = rewardList.Count; i < count; i++)
			{
				GachaResult.GachaReward gachaReward = rewardList[i];
				RARITY_TYPE rARITY_TYPE2 = RARITY_TYPE.D;
				switch (selectGachaType)
				{
				case GACHA_TYPE.QUEST:
				{
					QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)gachaReward.itemId);
					if (questData != null)
					{
						rARITY_TYPE2 = questData.rarity;
					}
					break;
				}
				case GACHA_TYPE.SKILL:
				{
					SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)gachaReward.itemId);
					if (skillItemData != null)
					{
						rARITY_TYPE2 = skillItemData.rarity;
					}
					break;
				}
				}
				if (rARITY_TYPE < rARITY_TYPE2)
				{
					rARITY_TYPE = rARITY_TYPE2;
				}
			}
		}
		return rARITY_TYPE;
	}

	public int GetCountOverRarity(List<GachaResult.GachaReward> rewardList, RARITY_TYPE rarity)
	{
		int num = 0;
		if (rewardList != null)
		{
			for (int i = 0; i < rewardList.Count; i++)
			{
				GachaResult.GachaReward gachaReward = rewardList[i];
				switch (selectGachaType)
				{
				case GACHA_TYPE.QUEST:
				{
					QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)gachaReward.itemId);
					if (questData != null && questData.rarity >= rarity)
					{
						num++;
					}
					break;
				}
				case GACHA_TYPE.SKILL:
				{
					SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)gachaReward.itemId);
					if (skillItemData != null && skillItemData.rarity >= rarity)
					{
						num++;
					}
					break;
				}
				}
			}
		}
		return num;
	}

	public bool IsReam()
	{
		if (selectGacha == null)
		{
			return false;
		}
		return selectGacha.num > 1;
	}

	private void CheckGachaShowBannerInvite()
	{
		if ((selectGacha.crystalNum != 0 || selectGacha.requiredItemId != 0) && selectGacha.requiredItemId != 0)
		{
			GameSaveData.instance.spentSummonTicket += selectGacha.needItemNum;
		}
	}

	private void TrackGachaEvent(GachaResult result)
	{
		if ((selectGacha.crystalNum == 0 && selectGacha.requiredItemId == 0) || result == null || result.reward == null || result.reward.Count == 0)
		{
			return;
		}
		if (selectGachaType == GACHA_TYPE.QUEST)
		{
			int count = result.reward.Count;
			int[] array = new int[count];
			int i = 0;
			for (int count2 = result.reward.Count; i < count2; i++)
			{
				array[i] = result.reward[i].itemId;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("quest_id", array.ToJoinString());
			dictionary.Add("amount", array.Length);
			if (selectGacha.crystalNum > 0)
			{
				dictionary.Add("currency_type", "gem");
				dictionary.Add("currency_value", selectGacha.crystalNum);
			}
			else if (selectGacha.requiredItemId != 0)
			{
				dictionary.Add("currency_type", "ticket");
				dictionary.Add("currency_value", selectGacha.needItemNum);
			}
			MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Credit_Spend_gacha_monster", "Credit_Spend", dictionary);
		}
		else if (selectGachaType == GACHA_TYPE.SKILL)
		{
			int[] array2 = new int[result.reward.Count];
			int j = 0;
			for (int count3 = result.reward.Count; j < count3; j++)
			{
				array2[j] = result.reward[j].itemId;
			}
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2.Add("skill_id", array2.ToJoinString());
			dictionary2.Add("amount", array2.Length);
			if (selectGacha.crystalNum > 0)
			{
				dictionary2.Add("currency_type", "gem");
				dictionary2.Add("currency_value", selectGacha.crystalNum);
			}
			else if (selectGacha.requiredItemId != 0)
			{
				dictionary2.Add("currency_type", "ticket");
				dictionary2.Add("currency_value", selectGacha.needItemNum);
			}
			MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Credit_Spend_gacha_magi", "Credit_Spend", dictionary2);
		}
	}

	private void SortGachaResult(List<GachaResult.GachaReward> rewardList)
	{
		int num = -1;
		bool flag = false;
		int index = -1;
		if (rewardList[0].rewardType == 6)
		{
			int i = 0;
			for (int count = rewardList.Count; i < count; i++)
			{
				QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)rewardList[i].itemId);
				if (questData != null && ((int)questData.rarity > num || (questData.rarity == (RARITY_TYPE)num && !flag && 0 < rewardList[i].lotGroupNo)))
				{
					num = (int)questData.rarity;
					flag = (0 < rewardList[i].lotGroupNo);
					index = i;
				}
			}
		}
		else
		{
			if (rewardList[0].rewardType != 5)
			{
				return;
			}
			int j = 0;
			for (int count2 = rewardList.Count; j < count2; j++)
			{
				SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)rewardList[j].itemId);
				if (skillItemData != null && ((int)skillItemData.rarity > num || (skillItemData.rarity == (RARITY_TYPE)num && !flag && 0 < rewardList[j].lotGroupNo)))
				{
					num = (int)skillItemData.rarity;
					flag = (0 < rewardList[j].lotGroupNo);
					index = j;
				}
			}
		}
		GachaResult.GachaReward value = rewardList[index];
		rewardList[index] = rewardList[rewardList.Count - 1];
		rewardList[rewardList.Count - 1] = value;
	}

	public void SendGetGacha(Action<bool> call_back)
	{
		gachaData = null;
		Protocol.Send(GachaListModel.URL, delegate(GachaListModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				gachaData = ret.result;
				gachaData.types.ForEach(delegate(GachaList.GachaType o)
				{
					o.groups.Sort((GachaList.GachaGroup x, GachaList.GachaGroup y) => y.priority - x.priority);
				});
				gachaData.types.ForEach(delegate(GachaList.GachaType type)
				{
					type.groups.ForEach(delegate(GachaList.GachaGroup gr)
					{
						List<GachaList.Gacha> oncePurchaseGachaList = (from g in gr.gachas
						where g.IsOncePurchase()
						select g).ToList();
						int num = oncePurchaseGachaList.Count();
						for (int i = 0; i < num; i++)
						{
							GachaList.Gacha gacha = oncePurchaseGachaList.ElementAt(i);
							int gachaId = gacha.gachaId;
							List<GachaList.Gacha> list = (from g in gr.gachas
							where g.gachaId == gachaId
							where oncePurchaseGachaList.IndexOf(g) == -1
							select g).ToList();
							if (list.Count > 0)
							{
								gr.gachas.Remove(gacha);
								int targetSubGroup = (from g in gr.gachas
								where g.gachaId == gachaId
								select g.subGroup).First();
								int index = (from ano in gr.gachas.Select((GachaList.Gacha g, int j) => new
								{
									Content = g,
									Index = j
								})
								where ano.Content.subGroup == targetSubGroup
								select ano.Index).First();
								gr.gachas.Insert(index, gacha);
							}
						}
					});
				});
				Dirty();
			}
			call_back(obj);
		}, string.Empty);
	}

	public void SendGachaGacha(int gachaId, int requiredItemId, string productId, int campaignId, int campaignType, int remainCount, int userCount, bool isStepUpTicket, int seriesId, Action<Error> call_back)
	{
		GachaGachaModel.RequestSendForm requestSendForm = new GachaGachaModel.RequestSendForm();
		requestSendForm.id = gachaId;
		requestSendForm.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		requestSendForm.ticketCL = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == requiredItemId, 1, isItemNum: true);
		requestSendForm.productId = productId;
		requestSendForm.guaranteeCampaignId = campaignId;
		requestSendForm.guaranteeCampaignType = campaignType;
		requestSendForm.guaranteeRemainCount = remainCount;
		requestSendForm.guaranteeUserCount = userCount;
		requestSendForm.useStepUpTicket = (isStepUpTicket ? 1 : 0);
		requestSendForm.seriesId = seriesId;
		gachaResultList = new List<GachaResult>();
		gachaResultBonus = null;
		int presentNum = MonoBehaviourSingleton<PresentManager>.I.presentNum;
		Protocol.Send(GachaGachaModel.URL, requestSendForm, delegate(GachaGachaModel ret)
		{
			if (ret.Error == Error.None)
			{
				gachaResultList.Add(ret.result);
				if (ret.resultArray != null && ret.resultArray.Count > 0)
				{
					gachaResultList.AddRange(ret.resultArray);
				}
				if (ret.resultBonus.reward != null)
				{
					gachaResultBonus = ret.resultBonus;
				}
				ResetGachaIndex();
				GachaResult currentGachaResult = GetCurrentGachaResult();
				if (currentGachaResult == null || currentGachaResult.oncePurchaseItemToShop == null || string.IsNullOrEmpty(currentGachaResult.oncePurchaseItemToShop.productId))
				{
					for (int i = 0; i < gachaResultList.Count; i++)
					{
						SortGachaResult(gachaResultList[i].reward);
					}
					if (IsResultBonus())
					{
						SortGachaResult(gachaResultBonus.reward);
					}
					if (selectGachaType == GACHA_TYPE.QUEST)
					{
						GameSaveData.instance.recommendedOrderCheck = 1;
						GameSaveData.Save();
					}
					Dirty();
					CheckGachaShowBannerInvite();
					TrackGachaEvent(ret.result);
				}
			}
			call_back(ret.Error);
		}, string.Empty);
	}

	public bool IsTutorial()
	{
		return IsTutorialQuestGacha() || IsTutorialSkillGacha();
	}

	public bool IsTutorialQuestGacha()
	{
		return IsExistGachaType(GACHA_TYPE.TUTORIAL1);
	}

	public bool IsTutorialSkillGacha()
	{
		return IsExistGachaType(GACHA_TYPE.TUTORIAL2);
	}

	private bool IsExistGachaType(GACHA_TYPE targetType)
	{
		if (gachaData == null)
		{
			return false;
		}
		if (gachaData.types == null || gachaData.types.Count <= 0)
		{
			return false;
		}
		foreach (GachaList.GachaType type in gachaData.types)
		{
			if (type.type == (int)targetType)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasBeenShowAdvertisement()
	{
		return PlayerPrefs.HasKey("SHOP_TOP_ADVERTISEMENT");
	}

	public void SetTimeShowShopAdvertisement(DateTime startAt)
	{
		PlayerPrefs.SetString("SHOP_TOP_ADVERTISEMENT", startAt.ToBinary().ToString());
	}

	public DateTime GetTimeShowShopAdvertisement()
	{
		string @string = PlayerPrefs.GetString("SHOP_TOP_ADVERTISEMENT");
		return DateTime.FromBinary(Convert.ToInt64(@string));
	}

	public string CreateButtonBaseName(GachaList.Gacha gacha, GachaGuaranteeCampaignInfo guarantee, bool resultScene = false)
	{
		string text = string.Empty;
		if (!resultScene || (gacha != null && gacha.requiredItemId > 0))
		{
			text = gacha.buttonImg;
		}
		if (guarantee != null && guarantee.IsValid())
		{
			string buttonImageName = guarantee.GetButtonImageName();
			if (buttonImageName != string.Empty)
			{
				text = buttonImageName;
			}
			if (gacha.IsOncePurchase() && guarantee.IsStepUp())
			{
				text = "BTN_GACHA_STEP10_Pay";
			}
			if (guarantee.IsStepUp() || guarantee.IsFever())
			{
				text = ((!guarantee.hasFreeGachaReward) ? (text + "_" + guarantee.GetImageCount()) : (text + "_FREE"));
			}
		}
		if (resultScene && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null && !string.IsNullOrEmpty(MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().buttonImg) && string.IsNullOrEmpty(text))
		{
			text = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().buttonImg;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = "BTN_GACHA_NORMAL1" + ((gacha.num != 1) ? "0" : string.Empty);
		}
		return text;
	}
}
