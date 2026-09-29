// Decompiled with JetBrains decompiler
// Type: QuestEntryPassRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class QuestEntryPassRoom : GameSection
{
  private const int PASS_CODE_MAX_DIGIT = 5;
  protected const string PASS_CODE_RESET_NUM = "-";
  protected int digit = 5;
  protected string[] passCode = new string[5]
  {
    "-",
    "-",
    "-",
    "-",
    "-"
  };
  protected int passCodeIndex;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    this.SetActive((Enum) QuestEntryPassRoom.UI.STR_NON_SETTINGS, false);
    this.SetLabelText((Enum) QuestEntryPassRoom.UI.LBL_INPUT_PASS_1, this.passCode[0]);
    this.SetLabelText((Enum) QuestEntryPassRoom.UI.LBL_INPUT_PASS_2, this.passCode[1]);
    this.SetLabelText((Enum) QuestEntryPassRoom.UI.LBL_INPUT_PASS_3, this.passCode[2]);
    this.SetLabelText((Enum) QuestEntryPassRoom.UI.LBL_INPUT_PASS_4, this.passCode[3]);
    this.SetLabelText((Enum) QuestEntryPassRoom.UI.LBL_INPUT_PASS_5, this.passCode[4]);
  }

  protected override void OnOpen()
  {
    if (!MonoBehaviourSingleton<CoopManager>.IsValid())
      return;
    MonoBehaviourSingleton<CoopManager>.I.Clear();
  }

  protected virtual void OnQuery_0() => this.InputNumber(0);

  private void OnQuery_1() => this.InputNumber(1);

  private void OnQuery_2() => this.InputNumber(2);

  private void OnQuery_3() => this.InputNumber(3);

  private void OnQuery_4() => this.InputNumber(4);

  private void OnQuery_5() => this.InputNumber(5);

  private void OnQuery_6() => this.InputNumber(6);

  private void OnQuery_7() => this.InputNumber(7);

  private void OnQuery_8() => this.InputNumber(8);

  private void OnQuery_9() => this.InputNumber(9);

  private void OnQuery_CLEAR()
  {
    this.passCodeIndex = 0;
    for (int index = 0; index < this.digit; ++index)
      this.passCode[index] = this.GetResetString();
    this.RefreshUI();
  }

  protected virtual string GetResetString() => "-";

  private void InputNumber(int num)
  {
    if (this.passCodeIndex == this.digit)
      return;
    this.passCode[this.passCodeIndex++] = num.ToString();
    this.RefreshUI();
  }

  protected virtual void OnQuery_ROOM() => this.SendApply();

  protected void SendApply(int questId = 0)
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) true
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendApply(string.Join("", this.passCode), (Action<bool, Error>) ((is_apply, ret_code) =>
    {
      if (is_apply && !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(MonoBehaviourSingleton<PartyManager>.I.GetQuestId()))
      {
        Protocol.Force((System.Action) (() => MonoBehaviourSingleton<PartyManager>.I.SendLeave((Action<bool>) (b => { }))));
      }
      else
      {
        switch (ret_code)
        {
          case Error.WRN_PARTY_SEARCH_NOT_FOUND_PARTY:
          case Error.WRN_PARTY_OWNER_REJOIN:
            GameSection.ChangeStayEvent("NOT_FOUND_PARTY");
            GameSection.ResumeEvent(true);
            break;
          default:
            GameSection.ResumeEvent(is_apply);
            break;
        }
      }
    }), questId);
  }

  protected enum UI
  {
    LBL_INPUT_PASS_1,
    LBL_INPUT_PASS_2,
    LBL_INPUT_PASS_3,
    LBL_INPUT_PASS_4,
    LBL_INPUT_PASS_5,
    STR_NON_SETTINGS,
  }
}
