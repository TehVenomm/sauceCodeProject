// Decompiled with JetBrains decompiler
// Type: FriendSearchName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class FriendSearchName : ConfigName
{
  private int page;

  public override void Initialize()
  {
    this.page = (int) GameSection.GetEventData();
    this.sectionType = ConfigName.SECTION_TYPE.SEARCH_NAME;
    base.Initialize();
  }

  protected override void SetBeforeText()
  {
    this.before_text = string.Empty;
    this.inputMaxLength = 14;
  }

  private void OnQuery_OK()
  {
    GameSection.SetEventData((object) null);
    string input_text = this.GetInputValue((Enum) FriendSearchName.UI.IPT_TEXT);
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendSearchName(input_text, this.page, (Action<bool, FriendSearchResult>) ((is_success, recv_data) =>
    {
      GameSection.ChangeStayEvent("OK", (object) new object[4]
      {
        (object) is_success,
        (object) this.page,
        (object) recv_data,
        (object) input_text
      });
      GameSection.ResumeEvent(is_success);
    }));
  }

  private new enum UI
  {
    IPT_TEXT,
    BTN_OK,
    SPR_TITLE_CHANGE_NAME,
    SPR_TITLE_CHANGE_COMMENT,
    SPR_TITLE_SEARCH_NAME,
    SPR_TITLE_SEARCH_ID,
  }
}
