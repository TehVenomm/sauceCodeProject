// Decompiled with JetBrains decompiler
// Type: ClanSettingDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ClanSettingDialog : ClanSettings
{
  private void OnQuery_CHANGE()
  {
    ClanEditClanModel.RequestSendForm clanSetting = new ClanEditClanModel.RequestSendForm();
    clanSetting.name = this.createRequest.clanName;
    clanSetting.iId = this.createRequest.stampId;
    clanSetting.jt = this.createRequest.isLock ? 1 : 0;
    clanSetting.lbl = (int) this.createRequest.label;
    clanSetting.cmt = this.createRequest.comment;
    clanSetting.tag = this.createRequest.clanTag;
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestEdit(clanSetting, (Action<bool>) (is_success => GameSection.ResumeEvent(true)));
  }
}
