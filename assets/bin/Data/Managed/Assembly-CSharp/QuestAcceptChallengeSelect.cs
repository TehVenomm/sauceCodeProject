// Decompiled with JetBrains decompiler
// Type: QuestAcceptChallengeSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestAcceptChallengeSelect : QuestAcceptSelect
{
  private List<QuestAcceptChallengeSelect.QuestDataSet> enableQuestList;
  private int selectedQuestIndex;

  public void OnCloseDialog_QuestAcceptChallengeRoomSettings() => this._OnCloseRoomSettings();

  public override void Initialize()
  {
    this.root = this.SetPrefab(this.collectUI, nameof (QuestAcceptChallengeSelect));
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    yield return (object) this._Initialize();
    bool sended = false;
    MonoBehaviourSingleton<QuestManager>.I.SendGetChallengeEnmey(this.questInfo.questData.tableData.enemyID[0], (Action<bool, QuestChallengeEnemyModel.Param>) ((isSuccess, result) =>
    {
      this.OnSendFinished(isSuccess, result);
      sended = true;
    }));
    if (!sended)
      yield return (object) null;
    this.InitializeBase();
  }

  private void OnSendFinished(bool isSuccess, QuestChallengeEnemyModel.Param result)
  {
    this.SetupQuestList(result.shadow);
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.UpdateEnemyLevelLabel();
    this.UpdateEnemyLevelButton();
    this.UpdateButtons();
  }

  private void UpdateEnemyLevelLabel()
  {
    this.SetLabelText((Enum) QuestSelect.UI.LBL_ENEMY_LEVEL, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1U, (object) this.GetSelectedQuestDataSet().tableData.enemyLv[0]));
  }

  private void UpdateEnemyLevelButton()
  {
    if (this.selectedQuestIndex >= this.enableQuestList.Count - 1)
    {
      this.SetColor((Enum) QuestSelect.UI.OBJ_LEVEL_R, Color.clear);
      this.SetActive((Enum) QuestSelect.UI.OBJ_LEVEL_INACTIVE_R, true);
    }
    else
    {
      this.SetColor((Enum) QuestSelect.UI.OBJ_LEVEL_R, Color.white);
      this.SetActive((Enum) QuestSelect.UI.OBJ_LEVEL_INACTIVE_R, false);
    }
    if (this.selectedQuestIndex <= 0)
    {
      this.SetColor((Enum) QuestSelect.UI.OBJ_LEVEL_L, Color.clear);
      this.SetActive((Enum) QuestSelect.UI.OBJ_LEVEL_INACTIVE_L, true);
    }
    else
    {
      this.SetColor((Enum) QuestSelect.UI.OBJ_LEVEL_L, Color.white);
      this.SetActive((Enum) QuestSelect.UI.OBJ_LEVEL_INACTIVE_L, false);
    }
  }

  private void UpdateButtons()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
      return;
    this.GetCtrl((Enum) QuestSelect.UI.BTN_GUILD_REQUEST).localPosition = new Vector3(136f, 10f, 0.0f);
    this.GetCtrl((Enum) QuestSelect.UI.BTN_GUILD_REQUEST).localScale = new Vector3(0.462f, 0.462f, 0.0f);
    this.GetCtrl((Enum) QuestSelect.UI.BTN_PARTY).localPosition = new Vector3(-34f, 10f, 0.0f);
    this.GetCtrl((Enum) QuestSelect.UI.BTN_PARTY).localScale = new Vector3(0.462f, 0.462f, 0.0f);
  }

  private void OnQuery_LEVEL_R()
  {
    ++this.selectedQuestIndex;
    this.OnLevelLRButton();
  }

  private void OnQuery_LEVEL_L()
  {
    --this.selectedQuestIndex;
    this.OnLevelLRButton();
  }

  private void OnLevelLRButton()
  {
    this.questInfo = this.GetSelectedQuestDataSet().questInfoData;
    this.RefreshUI();
  }

  private QuestAcceptChallengeSelect.QuestDataSet GetSelectedQuestDataSet()
  {
    return this.enableQuestList[this.selectedQuestIndex];
  }

  private void SetupQuestList(List<QuestData> allQuest)
  {
    int levelFromUserLevel = MonoBehaviourSingleton<UserInfoManager>.I.GetEnemyLevelFromUserLevel();
    this.enableQuestList = new List<QuestAcceptChallengeSelect.QuestDataSet>();
    int index = 0;
    for (int count = allQuest.Count; index < count; ++index)
    {
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) allQuest[index].questId);
      if (questData.enemyLv[0] == Singleton<QuestTable>.I.GetQuestData(this.questInfo.questData.tableData.questID).enemyLv[0])
        this.selectedQuestIndex = index;
      if (questData.enemyLv[0] <= levelFromUserLevel)
        this.enableQuestList.Add(new QuestAcceptChallengeSelect.QuestDataSet(allQuest[index], questData));
    }
  }

  protected override void OnQuery_CREATE_ROOM()
  {
    MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.questInfo.questData.tableData.questID);
    base.OnQuery_CREATE_ROOM();
  }

  protected override void OnQuery_GUILD_REQUEST()
  {
    GameSection.SetEventData((object) this.questInfo);
  }

  private class QuestDataSet
  {
    public QuestInfoData questInfoData;
    public QuestTable.QuestTableData tableData;

    public QuestDataSet(QuestData questData, QuestTable.QuestTableData tableData)
    {
      this.questInfoData = MonoBehaviourSingleton<QuestManager>.I.CreateQuestChallengeInfoData(questData, tableData);
      this.tableData = tableData;
    }
  }
}
