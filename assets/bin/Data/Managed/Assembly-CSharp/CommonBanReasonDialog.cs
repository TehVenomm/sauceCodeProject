// Decompiled with JetBrains decompiler
// Type: CommonBanReasonDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class CommonBanReasonDialog : CommonDialog
{
  private string banReason = string.Empty;

  public override void Initialize()
  {
    base.Initialize();
    this.SetInput(this._transform, (Enum) CommonBanReasonDialog.UI.IPT_REASON, string.Empty, 50, new EventDelegate.Callback(this.OnChangeGuildName));
  }

  protected override void InitDialog(object data_object)
  {
    base.InitDialog((object) new CommonDialog.Desc(CommonDialog.TYPE.DEFAULT, string.Empty));
    this.SetLabelText(this._transform, (Enum) CommonBanReasonDialog.UI.LBL_USER_KICK, $"{(data_object as FriendCharaInfo).name} will be kicked from your clan!");
  }

  protected override SoundID.UISE openingSound => SoundID.UISE.DIALOG_IMPTNT;

  protected override string GetTransferUIName() => "UI_BanReasonDialog";

  protected void OnChangeGuildName()
  {
    this.banReason = this.GetInputValue(this._transform, (Enum) CommonBanReasonDialog.UI.IPT_REASON);
    MonoBehaviourSingleton<GuildManager>.I.BanReason = this.banReason;
  }

  protected new enum UI
  {
    MESSAGE,
    SPR_BTN_0,
    LBL_BTN_0,
    LBL_BTN_0_R,
    SPR_BTN_1,
    LBL_BTN_1,
    LBL_BTN_1_R,
    SPR_BTN_2,
    LBL_BTN_2,
    LBL_BTN_2_R,
    OBJ_SPACE,
    OBJ_FRAME,
    TBL_BTN,
    BG,
    HEADER,
    CLOSE_BTN,
    LBL_TITLE,
    LBL_TITLE_U,
    LBL_TITLE_D,
    FOOTER,
    IPT_REASON,
    LBL_DEFAULT_INPUT_REASON,
    LBL_USER_KICK,
    LBL_REASON,
  }
}
