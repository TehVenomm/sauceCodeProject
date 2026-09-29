// Decompiled with JetBrains decompiler
// Type: UniqueStatusSetNameEdit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class UniqueStatusSetNameEdit : ConfigName
{
  private int setNo;
  private EquipSetInfo info;

  public override void Initialize()
  {
    this.setNo = (int) (GameSection.GetEventData() as object[])[0];
    this.info = (GameSection.GetEventData() as object[])[1] as EquipSetInfo;
    base.Initialize();
  }

  protected override void SetBeforeText()
  {
    this.before_text = this.info.name;
    this.inputMaxLength = 13;
  }

  private void OnQuery_OK()
  {
    GameSection.SetEventData((object) null);
    string input_text = this.GetInputValue((Enum) UniqueStatusSetNameEdit.UI.IPT_TEXT);
    GameSection.StayEvent();
    MonoBehaviourSingleton<StatusManager>.I.SendUniqueEquipSetName(input_text, this.setNo, (Action<bool>) (is_success =>
    {
      if (is_success)
        this.info.ChangeName(input_text);
      GameSection.ChangeStayEvent("OK", (object) new object[3]
      {
        (object) is_success,
        (object) this.setNo,
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
