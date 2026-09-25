// Decompiled with JetBrains decompiler
// Type: GuildEntryPassRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;

#nullable disable
public class GuildEntryPassRoom : QuestEntryPassRoom
{
  private GuildStatisticInfo _info;

  public override string overrideBackKeyEvent => "[BACK]";

  private IEnumerator GetClanStatistic(int clanID)
  {
    bool finish_get_statistic = false;
    MonoBehaviourSingleton<GuildManager>.I.SendRequestStatistic(clanID, (Action<bool, GuildStatisticInfo>) ((success, info) =>
    {
      finish_get_statistic = true;
      this._info = info;
    }));
    while (!finish_get_statistic)
      yield return (object) null;
    this.HandleEvent(clanID);
  }

  private void OnQuery_FIND()
  {
    GameSection.StayEvent();
    try
    {
      MonoBehaviourSingleton<GuildManager>.I.SendSearchWithID(int.Parse(string.Join("", this.passCode)), (Action<bool, Error>) ((is_success, err) => GameSection.ResumeEvent(is_success)));
    }
    catch
    {
      GameSection.ResumeEvent(true);
    }
  }

  private void HandleEvent(int clanId)
  {
    if (this._info == null)
      return;
    if (this._info.privacy == 0)
      MonoBehaviourSingleton<GuildManager>.I.SendRequestJoin(clanId, -1, (Action<bool, Error>) ((isSuccess, error) => MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Guild")));
    else if (this._info.privacy == 1)
      MonoBehaviourSingleton<GuildManager>.I.SendRequestRequest(clanId, -1, (Action<bool, Error>) ((isSuccess, error) =>
      {
        if (!isSuccess)
          return;
        this.OpenDialog();
      }));
    GameSection.ResumeEvent(true);
  }

  private void OpenDialog()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, this.sectionData.GetText("TEXT_NOITICE")), (Action<string>) (ret => { }));
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
