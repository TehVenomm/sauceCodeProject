// Decompiled with JetBrains decompiler
// Type: ClanCreateDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class ClanCreateDialog : ClanSettings
{
  private void OnQuery_CREATE()
  {
    GameSection.SetEventData((object) new string[1]
    {
      MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_CREATE_COST.ToString()
    });
  }

  private void OnQuery_ClanCreateConfirmDialog_YES()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal < MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_CREATE_COST)
    {
      GameSection.ChangeEvent("COST_ERROR");
    }
    else
    {
      if (this.createRequest.label == CLAN_LABEL.NONE)
        this.createRequest.SetLabel(CLAN_LABEL.ANYONE);
      MonoBehaviourSingleton<ClanMatchingManager>.I.SetClanCreateRequest(this.createRequest);
      GameSection.StayEvent();
      MonoBehaviourSingleton<ClanMatchingManager>.I.SendCreate((Action<bool, Error>) ((is_success, err) => GameSection.ResumeEvent(is_success)));
    }
  }
}
