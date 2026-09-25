// Decompiled with JetBrains decompiler
// Type: RegionMapDescriptionHappenItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class RegionMapDescriptionHappenItem : UIBehaviour
{
  public void SetUp(QuestTable.QuestTableData quest)
  {
    Transform transform = this.GetTransform();
    if (quest == null)
      return;
    this.SetUpEnemy(transform, quest);
    this.SetUpSubMissions(transform, quest);
  }

  private void SetUpEnemy(Transform t, QuestTable.QuestTableData quest)
  {
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) quest.GetMainEnemyID());
    if (enemyData == null)
      return;
    bool is_visible = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(quest.questID) != null;
    int icon_id = 10999;
    string text = "？？？？？";
    string str = "？？";
    if (is_visible)
    {
      icon_id = enemyData.iconId;
      text = enemyData.name;
      str = quest.GetMainEnemyLv().ToString();
    }
    ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, icon_id, new RARITY_TYPE?(), this.FindCtrl(t, (Enum) RegionMapDescriptionHappenItem.UI.OBJ_ENEMY)).SetDepth(7);
    this.SetElementSprite(t, (Enum) RegionMapDescriptionHappenItem.UI.SPR_ENM_ELEMENT, (int) enemyData.element);
    this.SetActive(t, (Enum) RegionMapDescriptionHappenItem.UI.SPR_ENM_ELEMENT, is_visible);
    this.SetElementSprite(t, (Enum) RegionMapDescriptionHappenItem.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
    this.SetActive(t, (Enum) RegionMapDescriptionHappenItem.UI.SPR_WEAK_ELEMENT, is_visible);
    bool flag = enemyData.weakElement == ELEMENT_TYPE.MAX;
    this.SetActive(t, (Enum) RegionMapDescriptionHappenItem.UI.STR_NON_WEAK_ELEMENT, flag & is_visible);
    this.SetActive(t, (Enum) RegionMapDescriptionHappenItem.UI.STR_UNKNOWN_WEAK_ELEMENT, !is_visible);
    this.SetLabelText(t, (Enum) RegionMapDescriptionHappenItem.UI.LBL_MONSTER_NAME, text);
    this.SetLabelText(t, (Enum) RegionMapDescriptionHappenItem.UI.LBL_MONSTER_LEVEL, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1U, (object) str));
  }

  private void SetUpSubMissions(Transform t, QuestTable.QuestTableData quest)
  {
    RegionMapDescriptionHappenItem.UI[] uiArray = new RegionMapDescriptionHappenItem.UI[3]
    {
      RegionMapDescriptionHappenItem.UI.OBJ_MISSION_INFO_1,
      RegionMapDescriptionHappenItem.UI.OBJ_MISSION_INFO_2,
      RegionMapDescriptionHappenItem.UI.OBJ_MISSION_INFO_3
    };
    if (!quest.IsMissionExist())
    {
      this.SetActive(t, (Enum) RegionMapDescriptionHappenItem.UI.OBJ_SUBMISSION_ROOT, false);
    }
    else
    {
      this.SetActive(t, (Enum) RegionMapDescriptionHappenItem.UI.OBJ_SUBMISSION_ROOT, true);
      ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(quest.questID);
      if (clearStatusQuestData == null)
      {
        int index = 0;
        for (int length = quest.missionID.Length; index < length; ++index)
        {
          uint missionID = quest.missionID[index];
          this.SetActive(t, (Enum) uiArray[index], missionID > 0U);
          this.SetSubMissionNotCleared(this.FindCtrl(t, (Enum) uiArray[index]), missionID);
        }
      }
      else
      {
        int index = 0;
        for (int count = clearStatusQuestData.missionStatus.Count; index < count; ++index)
        {
          uint missionID = quest.missionID[index];
          this.SetActive(t, (Enum) uiArray[index], missionID > 0U);
          CLEAR_STATUS missionStatu = (CLEAR_STATUS) clearStatusQuestData.missionStatus[index];
          this.SetSubMissionCleared(this.FindCtrl(t, (Enum) uiArray[index]), missionID, missionStatu);
        }
      }
    }
  }

  private void SetSubMission(Transform parent, uint missionID, bool isCleared)
  {
    this.SetActive(parent, (Enum) RegionMapDescriptionHappenItem.UI.SPR_MISSION_INFO_CROWN, isCleared);
    this.SetActive(parent, (Enum) RegionMapDescriptionHappenItem.UI.LBL_MISSION_INFO, true);
    QuestTable.MissionTableData missionData = Singleton<QuestTable>.I.GetMissionData(missionID);
    this.SetLabelText(parent, (Enum) RegionMapDescriptionHappenItem.UI.LBL_MISSION_INFO, missionData.missionText);
  }

  private void SetSubMissionNotCleared(Transform parent, uint missionID)
  {
    this.SetSubMission(parent, missionID, false);
  }

  private void SetSubMissionCleared(Transform parent, uint missionID, CLEAR_STATUS clearStatus)
  {
    this.SetSubMission(parent, missionID, clearStatus >= CLEAR_STATUS.CLEAR);
  }

  private Transform GetTransform()
  {
    Transform transform = this._transform;
    if (Object.op_Equality((Object) transform, (Object) null))
      transform = ((Component) this).transform;
    return transform;
  }

  private enum UI
  {
    OBJ_ENEMY,
    SPR_ENM_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    OBJ_SUBMISSION_ROOT,
    OBJ_MISSION_INFO_1,
    OBJ_MISSION_INFO_2,
    OBJ_MISSION_INFO_3,
    SPR_MISSION_INFO_CROWN,
    LBL_MISSION_INFO,
    LBL_MONSTER_NAME,
    LBL_MONSTER_LEVEL,
    STR_UNKNOWN_WEAK_ELEMENT,
  }
}
