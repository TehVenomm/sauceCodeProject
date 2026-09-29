// Decompiled with JetBrains decompiler
// Type: LimitedLoginBonusGet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class LimitedLoginBonusGet : GameSection
{
  private Transform texModel_;
  private UIModelRenderTexture texModelRenderTexture_;
  private UITexture texModelTexture_;
  private Transform texInnerModel_;
  private UIModelRenderTexture texInnerModelRenderTexture_;
  private UITexture texInnerModelTexture_;
  private Transform glowModel_;
  private bool isModel;
  private LoginBonus.LoginBonusReward reward;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    base.Initialize();
    this.texModel_ = Utility.Find(this._transform, "TEX_MODEL");
    this.texModelRenderTexture_ = UIModelRenderTexture.Get(this.texModel_);
    this.texModelTexture_ = ((Component) this.texModel_).GetComponent<UITexture>();
    this.texInnerModel_ = Utility.Find(this._transform, "TEX_INNER_MODEL");
    this.texInnerModelRenderTexture_ = UIModelRenderTexture.Get(this.texInnerModel_);
    this.texInnerModelTexture_ = ((Component) this.texInnerModel_).GetComponent<UITexture>();
    this.glowModel_ = Utility.Find(this._transform, "LIB_00000003");
    LoginBonus eventData = (LoginBonus) GameSection.GetEventData();
    if (eventData == null)
      return;
    this.SetLabelText((Enum) LimitedLoginBonusGet.UI.LBL_LOGIN_DAYS, eventData.name);
    if (eventData.reward.Count <= 0)
      return;
    this.reward = eventData.reward[0];
    this.SetLabelText((Enum) LimitedLoginBonusGet.UI.LBL_GET_ITEM, this.reward.name);
    if (14 == this.reward.type)
    {
      this.SetRenderAccessoryModel((Enum) LimitedLoginBonusGet.UI.TEX_MODEL, (uint) this.reward.itemId, this.reward.GetScale());
      this.texModelTexture_.width = 300;
      this.texModelTexture_.height = 300;
      this.isModel = true;
    }
    else if (5 == this.reward.type)
    {
      uint itemId = (uint) this.reward.itemId;
      this.texModelRenderTexture_.InitSkillItem(this.texModelTexture_, itemId, fov: 45f);
      this.texInnerModelRenderTexture_.InitSkillItemSymbol(this.texInnerModelTexture_, itemId, fov: 17f);
      this.isModel = true;
    }
    else if (4 == this.reward.type)
    {
      this.SetRenderEquipModel((Enum) LimitedLoginBonusGet.UI.TEX_MODEL, (uint) this.reward.itemId, scale: this.reward.GetScale());
      this.texModelTexture_.width = 300;
      this.texModelTexture_.height = 300;
      this.isModel = true;
    }
    else if (1 == this.reward.type || 2 == this.reward.type)
    {
      this.texModelRenderTexture_.InitItem(this.texModelTexture_, this.GetItemModelID((REWARD_TYPE) this.reward.type, this.reward.itemId));
      this.isModel = true;
    }
    else if (3 == this.reward.type && this.IsDispItem3D(this.reward.itemId))
    {
      this.texModelRenderTexture_.InitItem(this.texModelTexture_, this.GetItemModelID((REWARD_TYPE) this.reward.type, this.reward.itemId));
      this.isModel = true;
    }
    if (!this.isModel)
      this.StartCoroutine("LoadIcon");
    float val = 35f;
    this.texModelRenderTexture_.SetRotateSpeed(val);
    this.texInnerModelRenderTexture_.SetRotateSpeed(val);
  }

  private IEnumerator LoadIcon()
  {
    yield return (object) null;
    LoginBonus.LoginBonusReward reward = this.reward;
    ItemIcon.CreateRewardItemIcon((REWARD_TYPE) reward.type, (uint) reward.itemId, this.GetCtrl((Enum) LimitedLoginBonusGet.UI.OBJ_DETAIL_ROOT)).transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    this.GetCtrl((Enum) LimitedLoginBonusGet.UI.OBJ_DETAIL_ROOT).localPosition = new Vector3(0.0f, 50f, 0.0f);
  }

  private bool IsDispItem3D(int itemID)
  {
    switch (itemID)
    {
      case 1200000:
      case 7000100:
      case 7000101:
      case 7000200:
      case 7000201:
      case 7000300:
      case 7000301:
        return true;
      default:
        return false;
    }
  }

  private uint GetItemModelID(REWARD_TYPE type, int itemID)
  {
    uint itemModelId = uint.MaxValue;
    switch (type)
    {
      case REWARD_TYPE.CRYSTAL:
        itemModelId = 1U;
        break;
      case REWARD_TYPE.MONEY:
        itemModelId = 2U;
        break;
      case REWARD_TYPE.ITEM:
        itemModelId = (uint) itemID;
        break;
    }
    return itemModelId;
  }

  private void OnQuery_CLOSE()
  {
    if (Object.op_Inequality((Object) null, (Object) this.glowModel_))
      ((Component) this.glowModel_).gameObject.SetActive(false);
    GameSection.BackSection();
  }

  private enum UI
  {
    LBL_LOGIN_DAYS,
    GRD_GET_ITEM,
    LBL_GET_ITEM,
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
    TEX_INNER_MODEL,
  }
}
