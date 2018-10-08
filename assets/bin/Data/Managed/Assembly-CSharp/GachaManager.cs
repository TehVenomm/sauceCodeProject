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

	public void ResetGachaIndex()
	{
		currentGachaIndex = 0;
	}

	public void IncrementGachaIndex()
	{
		currentGachaIndex++;
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
				if (gachaGroup.gachas.Count > gachaIndex)
				{
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
		if ((selectGacha.crystalNum != 0 || selectGacha.requiredItemId != 0) && result != null && result.reward != null && result.reward.Count != 0)
		{
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
				dictionary.Add("quest_id", array.ToJoinString(",", null));
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
				dictionary2.Add("skill_id", array2.ToJoinString(",", null));
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
	}

	private void SortGachaResult(List<GachaResult.GachaReward> rewardList)
	{
		int num = -1;
		int index = -1;
		if (rewardList[0].rewardType == 6)
		{
			int i = 0;
			for (int count = rewardList.Count; i < count; i++)
			{
				QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)rewardList[i].itemId);
				if (questData != null && (int)questData.rarity > num)
				{
					num = (int)questData.rarity;
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
				if (skillItemData != null && (int)skillItemData.rarity > num)
				{
					num = (int)skillItemData.rarity;
					index = j;
				}
			}
		}
		GachaResult.GachaReward value = rewardList[index];
		rewardList[index] = rewardList[rewardList.Count - 1];
		rewardList[rewardList.Count - 1] = value;
	}

	public unsafe void SendGetGacha(Action<bool> call_back)
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
						List<GachaList.Gacha> gachas = gr.gachas;
						if (_003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache6 == null)
						{
							_003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache6 = new Func<GachaList.Gacha, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
						}
						List<GachaList.Gacha> oncePurchaseGachaList = gachas.Where(_003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache6).ToList();
						int num = oncePurchaseGachaList.Count();
						for (int i = 0; i < num; i++)
						{
							GachaList.Gacha gacha = oncePurchaseGachaList.ElementAt(i);
							int gachaId = gacha.gachaId;
							_003CSendGetGacha_003Ec__AnonStorey5CC._003CSendGetGacha_003Ec__AnonStorey5CA _003CSendGetGacha_003Ec__AnonStorey5CA;
							List<GachaList.Gacha> list = gr.gachas.Where(new Func<GachaList.Gacha, bool>((object)_003CSendGetGacha_003Ec__AnonStorey5CA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).Where(new Func<GachaList.Gacha, bool>((object)_003CSendGetGacha_003Ec__AnonStorey5CA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList();
							if (list.Count > 0)
							{
								gr.gachas.Remove(gacha);
								IEnumerable<GachaList.Gacha> source = gr.gachas.Where(new Func<GachaList.Gacha, bool>((object)_003CSendGetGacha_003Ec__AnonStorey5CA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
								if (_003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache7 == null)
								{
									_003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache7 = new Func<GachaList.Gacha, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
								}
								int targetSubGroup = source.Select<GachaList.Gacha, int>(_003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache7).First();
								List<GachaList.Gacha> gachas2 = gr.gachas;
								if (_003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache8 == null)
								{
									_003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache8 = new Func<GachaList.Gacha, int, _003C_003E__AnonType0<GachaList.Gacha, int>>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
								}
								var source2 = Enumerable.Select(gachas2, _003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache8).Where(new Func<_003C_003E__AnonType0<GachaList.Gacha, int>, bool>((object)_003CSendGetGacha_003Ec__AnonStorey5CA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
								if (_003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache9 == null)
								{
									_003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache9 = new Func<_003C_003E__AnonType0<GachaList.Gacha, int>, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
								}
								int index = Enumerable.Select(source2, _003CSendGetGacha_003Ec__AnonStorey5CC._003C_003Ef__am_0024cache9).First();
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
		requestSendForm.ticketCL = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == requiredItemId, 1, true);
		requestSendForm.productId = productId;
		requestSendForm.guaranteeCampaignId = campaignId;
		requestSendForm.guaranteeCampaignType = campaignType;
		requestSendForm.guaranteeRemainCount = remainCount;
		requestSendForm.guaranteeUserCount = userCount;
		requestSendForm.useStepUpTicket = (isStepUpTicket ? 1 : 0);
		requestSendForm.seriesId = seriesId;
		gachaResultList = new List<GachaResult>();
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
				ResetGachaIndex();
				GachaResult currentGachaResult = GetCurrentGachaResult();
				if (currentGachaResult == null || currentGachaResult.oncePurchaseItemToShop == null || string.IsNullOrEmpty(currentGachaResult.oncePurchaseItemToShop.productId))
				{
					for (int i = 0; i < gachaResultList.Count; i++)
					{
						SortGachaResult(gachaResultList[i].reward);
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
			if (guarantee.IsStepUp())
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
