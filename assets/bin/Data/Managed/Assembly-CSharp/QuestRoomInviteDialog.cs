// Decompiled with JetBrains decompiler
// Type: QuestRoomInviteDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;
using UnityEngine;

#nullable disable
public class QuestRoomInviteDialog : GameSection
{
  protected string inviteMessage = "";
  protected string helpLinkURL = "";

  public override void Initialize()
  {
    string inviteMessage = MonoBehaviourSingleton<PartyManager>.I.GetInviteMessage();
    this.helpLinkURL = MonoBehaviourSingleton<PartyManager>.I.GetInviteHelpURL();
    this.inviteMessage = inviteMessage.Replace("<BR>", "\n").Replace("<br>", "\n");
    this.InitializeBase();
  }

  protected void InitializeBase()
  {
    Debug.Log((object) $"<color=blue>InitializeBase  </color>{MonoBehaviourSingleton<AccountManager>.I.usageLimitMode.ToString()} ; {string.Format(this.sectionData.GetText("STR_SERVICE"))}");
    if (MonoBehaviourSingleton<AccountManager>.I.usageLimitMode)
    {
      this.SetActive((Enum) QuestRoomInviteDialog.UI.LBL_INVITE, false);
      this.SetActive((Enum) QuestRoomInviteDialog.UI.LBL_SNS, false);
      this.SetActive((Enum) QuestRoomInviteDialog.UI.OBJ_LINE_ROOT, false);
      this.SetActive((Enum) QuestRoomInviteDialog.UI.OBJ_TWITTER_ROOT, false);
      this.SetActive((Enum) QuestRoomInviteDialog.UI.LBL_SERVICE, true);
      this.SetLabelText((Enum) QuestRoomInviteDialog.UI.LBL_SERVICE, string.Format(this.sectionData.GetText("STR_SERVICE")));
    }
    else
    {
      this.SetActive((Enum) QuestRoomInviteDialog.UI.LBL_SNS, false);
      this.SetActive((Enum) QuestRoomInviteDialog.UI.LBL_SERVICE, false);
      this.SetActive((Enum) QuestRoomInviteDialog.UI.LBL_INVITE, true);
    }
    base.Initialize();
  }

  protected void OnQuery_LINE()
  {
    Native.OpenURL("https://line.naver.jp/R/msg/text/?" + WWW.EscapeURL(this.inviteMessage, Encoding.UTF8));
  }

  protected void OnQuery_TWITTER()
  {
    Native.OpenURL("https://twitter.com/intent/tweet?text=" + WWW.EscapeURL(this.inviteMessage + " #DragonProject"));
  }

  private void OnQuery_DETAIL() => GameSection.SetEventData((object) this.helpLinkURL);

  protected enum UI
  {
    LBL_SNS,
    OBJ_LINE_ROOT,
    OBJ_TWITTER_ROOT,
    LBL_SERVICE,
    LBL_INVITE,
  }
}
