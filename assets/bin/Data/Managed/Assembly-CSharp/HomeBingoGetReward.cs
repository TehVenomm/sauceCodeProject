// Decompiled with JetBrains decompiler
// Type: HomeBingoGetReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class HomeBingoGetReward : GameSection
{
  private Transform texModel_;
  private UIModelRenderTexture texModelRenderTexture_;
  private UITexture texModelTexture_;
  private Transform texInnerModel_;
  private UIModelRenderTexture texInnerModelRenderTexture_;
  private UITexture texInnerModelTexture_;
  private Transform glowModel_;
  private DeliveryTable.DeliveryData deliveryData;
  private Network.EventData eventData;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.deliveryData = eventData[0] as DeliveryTable.DeliveryData;
    this.eventData = eventData[1] as Network.EventData;
    base.Initialize();
    this.texModel_ = Utility.Find(this._transform, "TEX_MODEL");
    this.texModelRenderTexture_ = UIModelRenderTexture.Get(this.texModel_);
    this.texModelTexture_ = ((Component) this.texModel_).GetComponent<UITexture>();
    this.texInnerModel_ = Utility.Find(this._transform, "TEX_INNER_MODEL");
    this.texInnerModelRenderTexture_ = UIModelRenderTexture.Get(this.texInnerModel_);
    this.texInnerModelTexture_ = ((Component) this.texInnerModel_).GetComponent<UITexture>();
    this.glowModel_ = Utility.Find(this._transform, "LIB_00000003");
    this.SetLabelText((Enum) HomeBingoGetReward.UI.LBL_TITLE, this.eventData.name);
  }

  public override void UpdateUI()
  {
    this.UpdateRewardIcon(Singleton<DeliveryRewardTable>.I.GetDeliveryRewardTableData(this.deliveryData.id));
    base.UpdateUI();
  }

  private void UpdateRewardIcon(DeliveryRewardTable.DeliveryRewardData[] rewards)
  {
    if (rewards == null || rewards.Length == 0)
      return;
    int exp = 0;
    this.SetGrid((Enum) HomeBingoGetReward.UI.GRD_REWARD, "", rewards.Length, false, (Action<int, Transform, bool>) ((index, t, is_recycle) =>
    {
      DeliveryRewardTable.DeliveryRewardData.Reward reward = rewards[index].reward;
      bool is_visible = false;
      if (reward.type == REWARD_TYPE.EXP)
      {
        exp += reward.num;
      }
      else
      {
        is_visible = true;
        ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(reward.type, reward.item_id, t, reward.num, questIconSizeType: ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_DETAIL);
        this.SetMaterialInfo(rewardItemIcon.transform, reward.type, reward.item_id);
        rewardItemIcon.SetRewardBG(true);
      }
      this.SetActive(t, is_visible);
    }));
  }

  private void OnQuery_CLOSE()
  {
    if (Object.op_Inequality((Object) null, (Object) this.glowModel_))
      ((Component) this.glowModel_).gameObject.SetActive(false);
    GameSection.BackSection();
  }

  private enum UI
  {
    LBL_TITLE,
    GRD_GET_ITEM,
    LBL_GET_ITEM,
    GRD_REWARD,
  }

  private struct Reward
  {
    public int itemId;
    public int type;
    public string name;
  }
}
