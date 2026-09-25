// Decompiled with JetBrains decompiler
// Type: FriendSearchID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class FriendSearchID : ConfigName
{
  public override void Initialize()
  {
    this.sectionType = ConfigName.SECTION_TYPE.SEARCH_ID;
    base.Initialize();
  }

  protected override void SetBeforeText()
  {
    this.before_text = string.Empty;
    this.inputMaxLength = 99;
  }

  private void OnQuery_OK()
  {
    GameSection.SetEventData((object) null);
    string input_text = this.GetInputValue((Enum) FriendSearchID.UI.IPT_TEXT);
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendSearchID(input_text, (Action<bool, FriendSearchResult>) ((is_success, recv_data) =>
    {
      if (is_success)
        GameSection.ChangeStayEvent("OK", (object) new object[4]
        {
          (object) true,
          (object) 0,
          (object) recv_data,
          (object) input_text
        });
      else
        GameSection.ChangeStayEvent("OK", (object) new object[4]
        {
          (object) true,
          (object) 0,
          (object) new FriendSearchResult(),
          (object) input_text
        });
      GameSection.ResumeEvent(true);
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
