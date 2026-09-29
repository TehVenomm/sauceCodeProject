// Decompiled with JetBrains decompiler
// Type: GuildRequestAcceptSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Linq;
using UnityEngine;

#nullable disable
public class GuildRequestAcceptSelect : QuestAcceptSelect
{
  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetActive((Enum) QuestSelect.UI.BTN_PARTY, false);
    this.SetActive((Enum) QuestSelect.UI.TWN_DIFFICULT_STAR, false);
    this.GetCtrl((Enum) QuestSelect.UI.BTN_GUILD_REQUEST).localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    this.SetLabelText((Enum) QuestSelect.UI.LBL_LIMIT_TIME, "--:--");
    RARITY_TYPE rarity = this.questInfo.questData.tableData.rarity;
    this.SetActive((Enum) QuestSelect.UI.LBL_GUILD_REQUEST_NEED_POINT, true);
    this.SetLabelText((Enum) QuestSelect.UI.LBL_GUILD_REQUEST_NEED_POINT, string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 6U), (object) MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(rarity), (object) MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(rarity)));
  }

  protected override void OnQuery_GUILD_REQUEST()
  {
    GuildRequestItem selectedItem = MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
    string str = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedPoint(this.questInfo.questData.tableData.rarity).ToString();
    string needTimeWithFormat = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTimeWithFormat(this.questInfo.questData.tableData.rarity);
    string remainTimeWithFormat = selectedItem.GetHoundRemainTimeWithFormat();
    TimeSpan needTime = MonoBehaviourSingleton<GuildRequestManager>.I.GetNeedTime(this.questInfo.questData.tableData.rarity);
    TimeSpan houndRemainTime = selectedItem.GetHoundRemainTime();
    GameSection.SetEventData(0.0 >= houndRemainTime.TotalSeconds || !(houndRemainTime < needTime) ? (object) string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 0U), (object) str, (object) needTimeWithFormat) : (object) string.Format(StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 5U), (object) str, (object) needTimeWithFormat, (object) remainTimeWithFormat));
  }

  protected virtual void OnQuery_GuildRequestSortieMessage_YES()
  {
    MonoBehaviourSingleton<GuildRequestManager>.I.GetSelectedItem();
    bool flag = MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().Any<GameSectionHistory.HistoryData>((Func<GameSectionHistory.HistoryData, bool>) (h => h.sectionName == "QuestAcceptChallengeCounter" || h.sectionName == "GuildRequestChallengeCounter"));
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildRequestManager>.I.SendGuildRequestStart(this.questInfo, !flag, (Action<bool>) (isSuccess => this.SendGetChallengeInfo((System.Action) (() => GameSection.ResumeEvent(isSuccess)), (Action<bool>) null)));
  }

  private void SendGetChallengeInfo(System.Action onFinish, Action<bool> cb)
  {
    MonoBehaviourSingleton<PartyManager>.I.SendGetChallengeInfo((Action<bool, Error>) ((is_success, err) =>
    {
      if (onFinish != null)
        onFinish();
      if (cb == null)
        return;
      cb(is_success);
    }));
  }
}
