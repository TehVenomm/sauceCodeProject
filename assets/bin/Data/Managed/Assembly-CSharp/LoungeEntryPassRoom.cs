// Decompiled with JetBrains decompiler
// Type: LoungeEntryPassRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class LoungeEntryPassRoom : QuestEntryPassRoom
{
  private void OnQuery_LOUNGE()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendApply(string.Join("", this.passCode), (Action<bool, Error>) ((is_apply, ret_code) =>
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
    }));
  }

  protected new enum UI
  {
    LBL_INPUT_PASS_1,
    LBL_INPUT_PASS_2,
    LBL_INPUT_PASS_3,
    LBL_INPUT_PASS_4,
    LBL_INPUT_PASS_5,
    STR_NON_SETTINGS,
  }
}
