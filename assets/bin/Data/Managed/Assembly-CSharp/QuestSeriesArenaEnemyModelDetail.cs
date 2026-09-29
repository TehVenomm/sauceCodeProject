// Decompiled with JetBrains decompiler
// Type: QuestSeriesArenaEnemyModelDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestSeriesArenaEnemyModelDetail : GameSection
{
  protected UIModelRenderTexture enemyModelRenderTexture;
  private QuestTable.QuestTableData questData;
  private DeliveryTable.DeliveryData deliveryData;
  protected int enemyIndex;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.deliveryData = eventData[0] as DeliveryTable.DeliveryData;
    this.enemyIndex = (int) eventData[1];
    this.questData = this.deliveryData.GetQuestData();
    this.LoadEnemyModel();
    this.UpdateTopBar();
    base.Initialize();
  }

  private void UpdateTopBar()
  {
    int limitTime = (int) this.questData.limitTime;
    this.SetLabelText((Enum) QuestSeriesArenaEnemyModelDetail.UI.LBL_LIMIT_TIME, $"{limitTime / 60}:{limitTime % 60:D2}");
    this.SetLabelText((Enum) QuestSeriesArenaEnemyModelDetail.UI.LBL_SERIES_ARENA_NAME, this.deliveryData.name);
    ResourceLoad.LoadWithSetUITexture(((Component) this.GetCtrl((Enum) QuestSeriesArenaEnemyModelDetail.UI.TEX_ICON)).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(this.questData.rarity));
  }

  private void LoadEnemyModel()
  {
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.questData.enemyID[this.enemyIndex]);
    this.SetRenderEnemyModel((Enum) QuestSeriesArenaEnemyModelDetail.UI.TEX_ENEMY_MODEL, enemyData.id, enemyData.name, OutGameSettingsManager.EnemyDisplayInfo.SCENE.QUEST, moveType: UIModelRenderTexture.ENEMY_MOVE_TYPE.DONT_MOVE);
    this.enemyModelRenderTexture = this.GetComponent<UIModelRenderTexture>((Enum) QuestSeriesArenaEnemyModelDetail.UI.TEX_ENEMY_MODEL);
    this.GetComponent<UITexture>((Enum) QuestSeriesArenaEnemyModelDetail.UI.TEX_ENEMY_MODEL).color = Color.white;
    ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, new RARITY_TYPE?(), this.GetCtrl((Enum) QuestSeriesArenaEnemyModelDetail.UI.OBJ_ENEMY), enemyData.element);
    itemIcon.SetEnableCollider(false);
    itemIcon.rarityFrame.spriteName = "MonsterFrame_CD";
    UIBehaviour.SetRarityColorType(1, (UIWidget) itemIcon.rarityFrame);
    this.SetLabelText((Enum) QuestSeriesArenaEnemyModelDetail.UI.LBL_QUEST_NAME, $"Lv{(object) this.questData.enemyLv[this.enemyIndex]}{enemyData.name}");
    this.SetElementSprite((Enum) QuestSeriesArenaEnemyModelDetail.UI.SPR_ELEMENT_2, (int) enemyData.element);
    this.SetActive((Enum) QuestSeriesArenaEnemyModelDetail.UI.STR_NON_ELEMENT_2, enemyData.element == ELEMENT_TYPE.MAX);
    this.SetElementSprite((Enum) QuestSeriesArenaEnemyModelDetail.UI.SPR_WEAK_ELEMENT_2, (int) enemyData.weakElement);
    this.SetActive((Enum) QuestSeriesArenaEnemyModelDetail.UI.STR_NON_WEAK_ELEMENT_2, enemyData.weakElement == ELEMENT_TYPE.MAX);
  }

  private enum UI
  {
    WGT_ENEMY_MODEL,
    TEX_ENEMY_MODEL,
    LBL_ENEMY,
    OBJ_ENEMY,
    LBL_QUEST_NAME,
    SPR_ELEMENT_2,
    STR_NON_ELEMENT_2,
    SPR_WEAK_ELEMENT_2,
    STR_NON_WEAK_ELEMENT_2,
    LBL_LIMIT_TIME,
    LBL_SERIES_ARENA_NAME,
    TEX_ICON,
  }
}
