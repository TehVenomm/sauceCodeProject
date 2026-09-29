// Decompiled with JetBrains decompiler
// Type: ProfileEditComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ProfileEditComment : ConfigName
{
  public override void Initialize()
  {
    this.sectionType = ConfigName.SECTION_TYPE.CHANGE_COMMENT;
    base.Initialize();
  }

  protected override void SetBeforeText()
  {
    this.before_text = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.comment;
    this.inputMaxLength = 30;
  }

  private void OnQuery_OK()
  {
    string comment = this.GetInputValue((Enum) ProfileEditComment.UI.IPT_TEXT);
    int num = comment.IndexOf("\n");
    if (num != -1)
    {
      int startIndex = comment.IndexOf("\n", num + 1);
      if (startIndex != -1)
      {
        comment = comment.Remove(startIndex, comment.Length - startIndex);
        this.SetInputValue((Enum) ProfileEditComment.UI.IPT_TEXT, comment);
      }
    }
    GameSection.StayEvent();
    MonoBehaviourSingleton<UserInfoManager>.I.SendEditComment(comment, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
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
