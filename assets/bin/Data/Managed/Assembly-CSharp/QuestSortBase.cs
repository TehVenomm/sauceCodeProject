// Decompiled with JetBrains decompiler
// Type: QuestSortBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class QuestSortBase : SortBase
{
  private static readonly QuestSortBase.UI[] enemyButton = new QuestSortBase.UI[18]
  {
    QuestSortBase.UI.BTN_1,
    QuestSortBase.UI.BTN_2,
    QuestSortBase.UI.BTN_3,
    QuestSortBase.UI.BTN_4,
    QuestSortBase.UI.BTN_5,
    QuestSortBase.UI.BTN_6,
    QuestSortBase.UI.BTN_7,
    QuestSortBase.UI.BTN_8,
    QuestSortBase.UI.BTN_9,
    QuestSortBase.UI.BTN_10,
    QuestSortBase.UI.BTN_11,
    QuestSortBase.UI.BTN_12,
    QuestSortBase.UI.BTN_13,
    QuestSortBase.UI.BTN_14,
    QuestSortBase.UI.BTN_15,
    QuestSortBase.UI.BTN_16,
    QuestSortBase.UI.BTN_17,
    QuestSortBase.UI.BTN_18
  };
  private static readonly SortBase.TYPE[] enemyValue = new SortBase.TYPE[18]
  {
    SortBase.TYPE.ONE_HAND_SWORD,
    SortBase.TYPE.TWO_HAND_SWORD,
    SortBase.TYPE.SPEAR,
    SortBase.TYPE.PAIR_SWORDS,
    SortBase.TYPE.ARROW,
    SortBase.TYPE.ARMOR,
    SortBase.TYPE.HELM,
    SortBase.TYPE.ARM,
    SortBase.TYPE.LEG,
    SortBase.TYPE.SKILL_LIMITED,
    SortBase.TYPE.SKILL_GROW,
    SortBase.TYPE.ENEMY_ELEMENTAL,
    SortBase.TYPE.ENEMY_CHICKEN,
    SortBase.TYPE.ENEMY_MUSHROOM,
    SortBase.TYPE.ENEMY_COW,
    SortBase.TYPE.ENEMY_FROG,
    SortBase.TYPE.ENEMY_BAT,
    SortBase.TYPE.ENEMY_SLIME
  };
  private static readonly QuestSortBase.UI[] requirementButton = new QuestSortBase.UI[4]
  {
    QuestSortBase.UI.BTN_ID,
    QuestSortBase.UI.BTN_NUM,
    QuestSortBase.UI.BTN_RARITY,
    QuestSortBase.UI.BTN_DIFFICULTY
  };
  private static readonly SortBase.SORT_REQUIREMENT[] requirementValue = new SortBase.SORT_REQUIREMENT[4]
  {
    SortBase.SORT_REQUIREMENT.ID,
    SortBase.SORT_REQUIREMENT.NUM,
    SortBase.SORT_REQUIREMENT.RARITY,
    SortBase.SORT_REQUIREMENT.DIFFICULTY
  };
  private static readonly QuestSortBase.UI[] ascButton = new QuestSortBase.UI[2]
  {
    QuestSortBase.UI.BTN_ASC,
    QuestSortBase.UI.BTN_DESC
  };

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    int index1 = 0;
    for (int length = QuestSortBase.enemyButton.Length; index1 < length; ++index1)
      this.SetActive(this.GetCtrl((Enum) QuestSortBase.enemyButton[index1]).parent, false);
    MonoBehaviourSingleton<QuestManager>.I.questCollection.GetEnemyTypeList(QUEST_TYPE.ORDER)?.ForEach((Action<ENEMY_TYPE>) (_enemy =>
    {
      int index2 = (int) (_enemy - 1);
      this.SetActive(this.GetCtrl((Enum) QuestSortBase.enemyButton[index2]).parent, true);
    }));
    this.GetComponent<UIGrid>((Enum) QuestSortBase.UI.GRD_ENEMY).Reposition();
    this.UpdateAnchors();
    int event_data1 = 0;
    for (int length = QuestSortBase.enemyButton.Length; event_data1 < length; ++event_data1)
    {
      bool flag = (this.sortOrder.type & 1 << event_data1) != 0;
      this.SetEvent((Enum) QuestSortBase.enemyButton[event_data1], "ENEMY", event_data1);
      this.SetToggle(this.GetCtrl((Enum) QuestSortBase.enemyButton[event_data1]).parent, flag);
    }
    int num = 1035;
    int index3 = 0;
    for (int length = QuestSortBase.requirementButton.Length; index3 < length; ++index3)
    {
      int event_data2 = (int) QuestSortBase.requirementValue[index3];
      if ((event_data2 & num) != 0)
      {
        bool flag = this.sortOrder.requirement == (SortBase.SORT_REQUIREMENT) event_data2;
        this.SetEvent((Enum) QuestSortBase.requirementButton[index3], "REQUIREMENT", event_data2);
        this.SetToggle((Enum) QuestSortBase.requirementButton[index3], flag);
      }
      else
        this.SetActive((Enum) QuestSortBase.requirementButton[index3], false);
    }
    int event_data3 = 0;
    for (int length = QuestSortBase.ascButton.Length; event_data3 < length; ++event_data3)
    {
      bool flag = false;
      if (event_data3 == 0 && this.sortOrder.orderTypeAsc || event_data3 == 1 && !this.sortOrder.orderTypeAsc)
        flag = true;
      this.SetEvent((Enum) QuestSortBase.ascButton[event_data3], "ORDER_TYPE", event_data3);
      this.SetToggle((Enum) QuestSortBase.ascButton[event_data3], flag);
    }
  }

  private void OnQuery_ENEMY()
  {
    int eventData = (int) GameSection.GetEventData();
    int num = (int) QuestSortBase.enemyValue[eventData];
    bool flag;
    if ((this.sortOrder.type & num) == 0)
    {
      flag = true;
      this.sortOrder.type += num;
    }
    else
    {
      flag = false;
      this.sortOrder.type -= num;
    }
    this.SetToggle(this.GetCtrl((Enum) QuestSortBase.enemyButton[eventData]).parent, flag);
  }

  private enum UI
  {
    SCR_VIEW,
    BTN_1,
    BTN_2,
    BTN_3,
    BTN_4,
    BTN_5,
    BTN_6,
    BTN_7,
    BTN_8,
    BTN_9,
    BTN_10,
    BTN_11,
    BTN_12,
    BTN_13,
    BTN_14,
    BTN_15,
    BTN_16,
    BTN_17,
    BTN_18,
    BTN_ID,
    BTN_NUM,
    BTN_RARITY,
    BTN_DIFFICULTY,
    BTN_ASC,
    BTN_DESC,
    GRD_ENEMY,
    OBJ_ANCHOR_BOTTOM,
  }
}
