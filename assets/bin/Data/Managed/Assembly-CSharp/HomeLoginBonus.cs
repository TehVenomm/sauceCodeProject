// Decompiled with JetBrains decompiler
// Type: HomeLoginBonus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class HomeLoginBonus : GameSection
{
  private Transform texModel_;
  private UIModelRenderTexture texModelRenderTexture_;
  private UITexture texModelTexture_;
  private Transform texInnerModel_;
  private UIModelRenderTexture texInnerModelRenderTexture_;
  private UITexture texInnerModelTexture_;
  private Transform glowModel_;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    base.Initialize();
    MonoBehaviourSingleton<AccountManager>.I.DisplayLogInBonusSection();
    this.texModel_ = Utility.Find(this._transform, "TEX_MODEL");
    this.texModelRenderTexture_ = UIModelRenderTexture.Get(this.texModel_);
    this.texModelTexture_ = ((Component) this.texModel_).GetComponent<UITexture>();
    this.texInnerModel_ = Utility.Find(this._transform, "TEX_INNER_MODEL");
    this.texInnerModelRenderTexture_ = UIModelRenderTexture.Get(this.texInnerModel_);
    this.texInnerModelTexture_ = ((Component) this.texInnerModel_).GetComponent<UITexture>();
    this.glowModel_ = Utility.Find(this._transform, "LIB_00000003");
    LoginBonus loginBonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus.Find((Predicate<LoginBonus>) (obj => obj.type == 0));
    if (loginBonus == null)
      return;
    MonoBehaviourSingleton<AccountManager>.I.logInBonus.Remove(loginBonus);
    this.SetLabelText((Enum) HomeLoginBonus.UI.LBL_LOGIN_DAYS, loginBonus.total.ToString());
    if (loginBonus.reward.Count <= 0)
      return;
    LoginBonus.LoginBonusReward loginBonusReward = loginBonus.reward[0];
    this.SetLabelText((Enum) HomeLoginBonus.UI.LBL_GET_ITEM, loginBonusReward.name);
    float val = 35f;
    if (5 == loginBonusReward.type)
    {
      uint itemId = (uint) loginBonusReward.itemId;
      this.texModelRenderTexture_.InitSkillItem(this.texModelTexture_, itemId, fov: 45f);
      this.texInnerModelRenderTexture_.InitSkillItemSymbol(this.texInnerModelTexture_, itemId, fov: 17f);
    }
    else
      this.texModelRenderTexture_.InitItem(this.texModelTexture_, this.GetItemModelID((REWARD_TYPE) loginBonusReward.type, loginBonusReward.itemId));
    this.texModelRenderTexture_.SetRotateSpeed(val);
    this.texInnerModelRenderTexture_.SetRotateSpeed(val);
  }

  protected override void OnOpen()
  {
    HomeLoginBonusTheater.PlayRandomVoice(HomeLoginBonusTheater.voiceCheers);
    base.OnOpen();
  }

  public override void UpdateUI() => base.UpdateUI();

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
    if (MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count > 0)
      GameSection.ChangeEvent("LIMITED_LOGIN_BONUS");
    else
      GameSection.BackSection();
  }

  private enum UI
  {
    LBL_LOGIN_DAYS,
    GRD_GET_ITEM,
    LBL_GET_ITEM,
  }
}
