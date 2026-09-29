// Decompiled with JetBrains decompiler
// Type: GachaManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class GachaManager : MonoBehaviourSingleton<GachaManager>
{
  private int currentGachaIndex;

  public GachaList gachaData { get; private set; }

  public List<GachaResult> gachaResultList { get; private set; }

  public GachaResult gachaResultBonus { get; private set; }

  public bool enableFeverDirector { get; private set; }

  public bool IsMultiResult() => this.gachaResultList.Count > 1;

  public bool IsExistNextGachaResult() => this.gachaResultList.Count > this.currentGachaIndex + 1;

  public bool IsResultBonus() => this.gachaResultBonus != null;

  public void ResetGachaIndex()
  {
    this.currentGachaIndex = 0;
    this.enableFeverDirector = false;
  }

  public void IncrementGachaIndex() => ++this.currentGachaIndex;

  public void SetNextFever() => this.enableFeverDirector = true;

  public GachaResult GetCurrentGachaResult()
  {
    return this.gachaResultList == null ? (GachaResult) null : this.gachaResultList[this.currentGachaIndex];
  }

  public GachaResult GetNextGachaResult()
  {
    if (this.gachaResultList == null)
      return (GachaResult) null;
    return this.gachaResultList.Count <= this.currentGachaIndex + 1 ? (GachaResult) null : this.gachaResultList[this.currentGachaIndex + 1];
  }

  public GachaList.Gacha selectGacha { get; private set; }

  public GACHA_TYPE selectGachaType { get; private set; }

  public GACHA_TYPE selectGachaRealType { get; private set; }

  public GachaGuaranteeCampaignInfo selectGachaGuarantee { get; private set; }

  public void ResetGachaType()
  {
    this.selectGachaType = GACHA_TYPE.QUEST;
    this.selectGachaRealType = GACHA_TYPE.QUEST;
  }

  public bool IsSelectTutorialGacha()
  {
    return this.selectGachaRealType == GACHA_TYPE.TUTORIAL1 || this.selectGachaRealType == GACHA_TYPE.TUTORIAL2;
  }

  public GachaManager() => this.gachaData = new GachaList();

  public void Dirty()
  {
  }

  public void SelectGacha(int gachaId, int gachaIndex)
  {
    GACHA_TYPE gacha_type;
    GACHA_TYPE real_type;
    GachaGuaranteeCampaignInfo guaranteeInfo;
    this.selectGacha = this.FindGacha(gachaId, gachaIndex, out gacha_type, out real_type, out guaranteeInfo);
    this.selectGachaType = gacha_type;
    this.selectGachaRealType = real_type;
    this.selectGachaGuarantee = guaranteeInfo;
  }

  private GachaList.Gacha FindGacha(
    int gachaId,
    int gachaIndex,
    out GACHA_TYPE gacha_type,
    out GACHA_TYPE real_type,
    out GachaGuaranteeCampaignInfo guaranteeInfo)
  {
    GachaList.Gacha gacha1 = (GachaList.Gacha) null;
    gacha_type = (GACHA_TYPE) 0;
    real_type = (GACHA_TYPE) 0;
    guaranteeInfo = new GachaGuaranteeCampaignInfo();
    for (int index1 = 0; index1 < this.gachaData.types.Count; ++index1)
    {
      GachaList.GachaType type = this.gachaData.types[index1];
      for (int index2 = 0; index2 < type.groups.Count; ++index2)
      {
        GachaList.GachaGroup group = type.groups[index2];
        if (group.gachas.Count > gachaIndex)
        {
          GachaList.Gacha gacha2 = group.gachas[gachaIndex];
          if (gacha2.gachaId == gachaId)
          {
            GachaList.Gacha gacha3 = gacha2;
            gacha_type = type.ViewType;
            real_type = type.Type;
            guaranteeInfo = group.gachaGuaranteeCampaignInfo.Find((Predicate<GachaGuaranteeCampaignInfo>) (info => info.gachaId == gachaId));
            if (guaranteeInfo == null)
              guaranteeInfo = new GachaGuaranteeCampaignInfo();
            return gacha3;
          }
        }
      }
    }
    return gacha1;
  }

  public void SetSelectGachaGuarantee(GachaGuaranteeCampaignInfo guaranteeInfo)
  {
    this.selectGachaGuarantee = guaranteeInfo;
  }

  public RARITY_TYPE GetMaxRarity() => this.GetMaxRarity(this.GetCurrentGachaResult().reward);

  public RARITY_TYPE GetMaxRarity(List<GachaResult.GachaReward> rewardList)
  {
    RARITY_TYPE maxRarity = RARITY_TYPE.D;
    if (rewardList != null)
    {
      int index = 0;
      for (int count = rewardList.Count; index < count; ++index)
      {
        GachaResult.GachaReward reward = rewardList[index];
        RARITY_TYPE rarityType = RARITY_TYPE.D;
        switch (this.selectGachaType)
        {
          case GACHA_TYPE.SKILL:
            SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) reward.itemId);
            if (skillItemData != null)
            {
              rarityType = skillItemData.rarity;
              break;
            }
            break;
          case GACHA_TYPE.QUEST:
            QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) reward.itemId);
            if (questData != null)
            {
              rarityType = questData.rarity;
              break;
            }
            break;
        }
        if (maxRarity < rarityType)
          maxRarity = rarityType;
      }
    }
    return maxRarity;
  }

  public int GetCountOverRarity(List<GachaResult.GachaReward> rewardList, RARITY_TYPE rarity)
  {
    int countOverRarity = 0;
    if (rewardList != null)
    {
      for (int index = 0; index < rewardList.Count; ++index)
      {
        GachaResult.GachaReward reward = rewardList[index];
        switch (this.selectGachaType)
        {
          case GACHA_TYPE.SKILL:
            SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) reward.itemId);
            if (skillItemData != null && skillItemData.rarity >= rarity)
            {
              ++countOverRarity;
              break;
            }
            break;
          case GACHA_TYPE.QUEST:
            QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) reward.itemId);
            if (questData != null && questData.rarity >= rarity)
            {
              ++countOverRarity;
              break;
            }
            break;
        }
      }
    }
    return countOverRarity;
  }

  public bool IsReam() => this.selectGacha != null && this.selectGacha.num > 1;

  private void CheckGachaShowBannerInvite()
  {
    if (this.selectGacha.crystalNum == 0 && this.selectGacha.requiredItemId == 0 || this.selectGacha.requiredItemId == 0)
      return;
    GameSaveData.instance.spentSummonTicket += this.selectGacha.needItemNum;
  }

  private void TrackGachaEvent(GachaResult result)
  {
    if (this.selectGacha.crystalNum == 0 && this.selectGacha.requiredItemId == 0 || result == null || result.reward == null || result.reward.Count == 0)
      return;
    if (this.selectGachaType == GACHA_TYPE.QUEST)
    {
      int[] array = new int[result.reward.Count];
      int index = 0;
      for (int count = result.reward.Count; index < count; ++index)
        array[index] = result.reward[index].itemId;
      Dictionary<string, object> values = new Dictionary<string, object>();
      values.Add("quest_id", (object) array.ToJoinString<int>());
      values.Add("amount", (object) array.Length);
      if (this.selectGacha.crystalNum > 0)
      {
        values.Add("currency_type", (object) "gem");
        values.Add("currency_value", (object) this.selectGacha.crystalNum);
      }
      else if (this.selectGacha.requiredItemId != 0)
      {
        values.Add("currency_type", (object) "ticket");
        values.Add("currency_value", (object) this.selectGacha.needItemNum);
      }
      MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Credit_Spend_gacha_monster", "Credit_Spend", values);
    }
    else
    {
      if (this.selectGachaType != GACHA_TYPE.SKILL)
        return;
      int[] array = new int[result.reward.Count];
      int index = 0;
      for (int count = result.reward.Count; index < count; ++index)
        array[index] = result.reward[index].itemId;
      Dictionary<string, object> values = new Dictionary<string, object>();
      values.Add("skill_id", (object) array.ToJoinString<int>());
      values.Add("amount", (object) array.Length);
      if (this.selectGacha.crystalNum > 0)
      {
        values.Add("currency_type", (object) "gem");
        values.Add("currency_value", (object) this.selectGacha.crystalNum);
      }
      else if (this.selectGacha.requiredItemId != 0)
      {
        values.Add("currency_type", (object) "ticket");
        values.Add("currency_value", (object) this.selectGacha.needItemNum);
      }
      MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Credit_Spend_gacha_magi", "Credit_Spend", values);
    }
  }

  private void SortGachaResult(List<GachaResult.GachaReward> rewardList)
  {
    int num = -1;
    bool flag = false;
    int index1 = -1;
    if (rewardList[0].rewardType == 6)
    {
      int index2 = 0;
      for (int count = rewardList.Count; index2 < count; ++index2)
      {
        QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) rewardList[index2].itemId);
        if (questData != null && (questData.rarity > (RARITY_TYPE) num || questData.rarity == (RARITY_TYPE) num && !flag && 0 < rewardList[index2].lotGroupNo))
        {
          num = (int) questData.rarity;
          flag = 0 < rewardList[index2].lotGroupNo;
          index1 = index2;
        }
      }
    }
    else
    {
      if (rewardList[0].rewardType != 5)
        return;
      int index3 = 0;
      for (int count = rewardList.Count; index3 < count; ++index3)
      {
        SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) rewardList[index3].itemId);
        if (skillItemData != null && (skillItemData.rarity > (RARITY_TYPE) num || skillItemData.rarity == (RARITY_TYPE) num && !flag && 0 < rewardList[index3].lotGroupNo))
        {
          num = (int) skillItemData.rarity;
          flag = 0 < rewardList[index3].lotGroupNo;
          index1 = index3;
        }
      }
    }
    GachaResult.GachaReward reward = rewardList[index1];
    rewardList[index1] = rewardList[rewardList.Count - 1];
    rewardList[rewardList.Count - 1] = reward;
  }

  public void SendGetGacha(Action<bool> call_back)
  {
    this.gachaData = (GachaList) null;
    Protocol.Send<GachaListModel>(GachaListModel.URL, (Action<GachaListModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        flag = true;
        this.gachaData = ret.result;
        this.gachaData.types.ForEach((Action<GachaList.GachaType>) (o => o.groups.Sort((Comparison<GachaList.GachaGroup>) ((x, y) => y.priority - x.priority))));
        this.gachaData.types.ForEach((Action<GachaList.GachaType>) (type => type.groups.ForEach((Action<GachaList.GachaGroup>) (gr =>
        {
          List<GachaList.Gacha> oncePurchaseGachaList = gr.gachas.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.IsOncePurchase())).ToList<GachaList.Gacha>();
          int num = oncePurchaseGachaList.Count<GachaList.Gacha>();
          for (int index3 = 0; index3 < num; ++index3)
          {
            GachaList.Gacha gacha = oncePurchaseGachaList.ElementAt<GachaList.Gacha>(index3);
            int gachaId = gacha.gachaId;
            if (gr.gachas.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.gachaId == gachaId)).Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => oncePurchaseGachaList.IndexOf(g) == -1)).ToList<GachaList.Gacha>().Count > 0)
            {
              gr.gachas.Remove(gacha);
              int targetSubGroup = gr.gachas.Where<GachaList.Gacha>((Func<GachaList.Gacha, bool>) (g => g.gachaId == gachaId)).Select<GachaList.Gacha, int>((Func<GachaList.Gacha, int>) (g => g.subGroup)).First<int>();
              int index4 = gr.gachas.Select((g, j) => new
              {
                Content = g,
                Index = j
              }).Where(ano => ano.Content.subGroup == targetSubGroup).Select(ano => ano.Index).First<int>();
              gr.gachas.Insert(index4, gacha);
            }
          }
        }))));
        this.Dirty();
      }
      call_back(flag);
    }));
  }

  public void SendGachaGacha(
    int gachaId,
    int requiredItemId,
    string productId,
    int campaignId,
    int campaignType,
    int remainCount,
    int userCount,
    bool isStepUpTicket,
    int seriesId,
    Action<Error> call_back)
  {
    GachaGachaModel.RequestSendForm postData = new GachaGachaModel.RequestSendForm();
    postData.id = gachaId;
    postData.crystalCL = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
    postData.ticketCL = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((Predicate<ItemInfo>) (x => (long) x.tableData.id == (long) requiredItemId), 1, true);
    postData.productId = productId;
    postData.guaranteeCampaignId = campaignId;
    postData.guaranteeCampaignType = campaignType;
    postData.guaranteeRemainCount = remainCount;
    postData.guaranteeUserCount = userCount;
    postData.useStepUpTicket = isStepUpTicket ? 1 : 0;
    postData.seriesId = seriesId;
    this.gachaResultList = new List<GachaResult>();
    this.gachaResultBonus = (GachaResult) null;
    int presentNum = MonoBehaviourSingleton<PresentManager>.I.presentNum;
    Protocol.Send<GachaGachaModel.RequestSendForm, GachaGachaModel>(GachaGachaModel.URL, postData, (Action<GachaGachaModel>) (ret =>
    {
      if (ret.Error == Error.None)
      {
        this.gachaResultList.Add(ret.result);
        if (ret.resultArray != null && ret.resultArray.Count > 0)
          this.gachaResultList.AddRange((IEnumerable<GachaResult>) ret.resultArray);
        if (ret.resultBonus.reward != null)
          this.gachaResultBonus = ret.resultBonus;
        this.ResetGachaIndex();
        GachaResult currentGachaResult = this.GetCurrentGachaResult();
        if (currentGachaResult == null || currentGachaResult.oncePurchaseItemToShop == null || string.IsNullOrEmpty(currentGachaResult.oncePurchaseItemToShop.productId))
        {
          for (int index = 0; index < this.gachaResultList.Count; ++index)
            this.SortGachaResult(this.gachaResultList[index].reward);
          if (this.IsResultBonus())
            this.SortGachaResult(this.gachaResultBonus.reward);
          if (this.selectGachaType == GACHA_TYPE.QUEST)
          {
            GameSaveData.instance.recommendedOrderCheck = 1;
            GameSaveData.Save();
          }
          this.Dirty();
          this.CheckGachaShowBannerInvite();
          this.TrackGachaEvent(ret.result);
        }
      }
      call_back(ret.Error);
    }));
  }

  public bool IsTutorial() => this.IsTutorialQuestGacha() || this.IsTutorialSkillGacha();

  public bool IsTutorialQuestGacha() => this.IsExistGachaType(GACHA_TYPE.TUTORIAL1);

  public bool IsTutorialSkillGacha() => this.IsExistGachaType(GACHA_TYPE.TUTORIAL2);

  private bool IsExistGachaType(GACHA_TYPE targetType)
  {
    if (this.gachaData == null || this.gachaData.types == null || this.gachaData.types.Count <= 0)
      return false;
    foreach (GachaList.GachaType type in this.gachaData.types)
    {
      if ((GACHA_TYPE) type.type == targetType)
        return true;
    }
    return false;
  }

  public bool HasBeenShowAdvertisement() => PlayerPrefs.HasKey("SHOP_TOP_ADVERTISEMENT");

  public void SetTimeShowShopAdvertisement(DateTime startAt)
  {
    PlayerPrefs.SetString("SHOP_TOP_ADVERTISEMENT", startAt.ToBinary().ToString());
  }

  public DateTime GetTimeShowShopAdvertisement()
  {
    return DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("SHOP_TOP_ADVERTISEMENT")));
  }

  public string CreateButtonBaseName(
    GachaList.Gacha gacha,
    GachaGuaranteeCampaignInfo guarantee,
    bool resultScene = false)
  {
    string buttonBaseName = string.Empty;
    if (!resultScene || gacha != null && gacha.requiredItemId > 0)
      buttonBaseName = gacha.buttonImg;
    if (guarantee != null && guarantee.IsValid())
    {
      string buttonImageName = guarantee.GetButtonImageName();
      if (buttonImageName != "")
        buttonBaseName = buttonImageName;
      if (gacha.IsOncePurchase() && guarantee.IsStepUp())
        buttonBaseName = "BTN_GACHA_STEP10_Pay";
      if (guarantee.IsStepUp() || guarantee.IsFever())
        buttonBaseName = !guarantee.hasFreeGachaReward ? $"{buttonBaseName}_{(object) guarantee.GetImageCount()}" : buttonBaseName + "_FREE";
    }
    if (resultScene && MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult() != null && !string.IsNullOrEmpty(MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().buttonImg) && string.IsNullOrEmpty(buttonBaseName))
      buttonBaseName = MonoBehaviourSingleton<GachaManager>.I.GetCurrentGachaResult().buttonImg;
    if (string.IsNullOrEmpty(buttonBaseName))
      buttonBaseName = "BTN_GACHA_NORMAL1" + (gacha.num == 1 ? "" : "0");
    return buttonBaseName;
  }
}
