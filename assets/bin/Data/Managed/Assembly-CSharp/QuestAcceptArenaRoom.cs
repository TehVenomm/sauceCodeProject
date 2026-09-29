// Decompiled with JetBrains decompiler
// Type: QuestAcceptArenaRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestAcceptArenaRoom : OffLineQuestRoomBase
{
  private ArenaTable.ArenaData arenaData;
  private DeliveryTable.DeliveryData deliveryData;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "ArenaTable";
    }
  }

  public override void Initialize()
  {
    base.Initialize();
    this.arenaData = Singleton<ArenaTable>.I.GetArenaData(MonoBehaviourSingleton<QuestManager>.I.currentArenaId);
    this.deliveryData = GameSection.GetEventData() as DeliveryTable.DeliveryData;
    this.StartCoroutine(this.StartPredownload());
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.UpdateTopBar();
    this.UpdateEnemyList();
    this.UpdateLimitText();
    this.UpdateConditionText();
    this.UpdateStartButton();
    this.SetDifficultySprite();
  }

  private void UpdateTopBar()
  {
    int secByMilliSec = QuestUtility.ToSecByMilliSec(this.arenaData.timeLimit);
    this.SetLabelText((Enum) QuestAcceptArenaRoom.UI.LBL_LIMIT_TIME, $"{secByMilliSec / 60}:{secByMilliSec % 60:D2}");
    string text;
    if (this.deliveryData != null)
      text = QuestUtility.GetArenaTitle(this.arenaData.group, this.deliveryData.name);
    else
      text = $"{StringTable.Format(STRING_CATEGORY.ARENA, 0U, (object) this.arenaData.group)}　{StringTable.Format(STRING_CATEGORY.ARENA, 1U, (object) this.arenaData.rank)}";
    this.SetLabelText((Enum) QuestAcceptArenaRoom.UI.LBL_ARENA_NAME, text);
    ResourceLoad.LoadWithSetUITexture(((Component) this.GetCtrl((Enum) QuestAcceptArenaRoom.UI.TEX_ICON)).GetComponent<UITexture>(), RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(this.arenaData.rank));
  }

  private void UpdateEnemyList()
  {
    if (this.arenaData == null)
      return;
    List<QuestTable.QuestTableData> questDataArray = this.arenaData.GetQuestDataArray();
    this.SetTable((Enum) QuestAcceptArenaRoom.UI.TBL_LIST, "QuestArenaRoomEnemyListItem", questDataArray.Count, false, (Action<int, Transform, bool>) ((i, t, b) => this.InitEnemyItem(i, t, b, questDataArray[i])));
  }

  private void InitEnemyItem(
    int i,
    Transform t,
    bool isRecycle,
    QuestTable.QuestTableData questData)
  {
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questData.GetMainEnemyID());
    if (enemyData == null)
      return;
    this.SetLabelText(t, (Enum) QuestAcceptArenaRoom.UI.LBL_ENEMY_LEVEL, StringTable.Format(STRING_CATEGORY.MAIN_STATUS, 1U, (object) this.arenaData.level));
    this.SetLabelText(t, (Enum) QuestAcceptArenaRoom.UI.LBL_ENEMY_NAME, enemyData.name);
    ItemIcon.Create(ItemIcon.GetItemIconType(questData.questType), enemyData.iconId, new RARITY_TYPE?(questData.rarity), this.FindCtrl(t, (Enum) QuestAcceptArenaRoom.UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(false);
    this.SetActive(t, (Enum) QuestAcceptArenaRoom.UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
    this.SetElementSprite(t, (Enum) QuestAcceptArenaRoom.UI.SPR_ELEMENT, (int) enemyData.element);
    this.SetElementSprite(t, (Enum) QuestAcceptArenaRoom.UI.SPR_WEAK_ELEMENT, (int) enemyData.weakElement);
    this.SetActive(t, (Enum) QuestAcceptArenaRoom.UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
  }

  private void UpdateLimitText()
  {
    this.SetLabelText((Enum) QuestAcceptArenaRoom.UI.LBL_LIMIT, QuestUtility.GetLimitText(this.arenaData));
  }

  private void UpdateConditionText()
  {
    this.SetLabelText((Enum) QuestAcceptArenaRoom.UI.LBL_CONDITION, QuestUtility.GetConditionText(this.arenaData));
  }

  private void UpdateStartButton()
  {
    bool is_visible = QuestUtility.JudgeLimit(this.arenaData, this.userInfo.equipSet);
    this.SetActive((Enum) QuestAcceptArenaRoom.UI.BTN_START, is_visible);
    this.SetActive((Enum) QuestAcceptArenaRoom.UI.BTN_NG, !is_visible);
  }

  protected void OnQuery_START() => this.StartQuest();

  private void StartQuest()
  {
    GameSection.StayEvent();
    CoopApp.EnterArenaQuestOffline((Action<bool, bool, bool, bool>) ((isMatching, isConnect, isRegist, isStart) => GameSection.ResumeEvent(isStart)));
  }

  private void SetDifficultySprite()
  {
    this.SetActive((Enum) QuestAcceptArenaRoom.UI.SPR_TYPE_DIFFICULTY, this.deliveryData != null && this.deliveryData.difficulty >= DIFFICULTY_MODE.HARD);
  }

  private IEnumerator StartPredownload()
  {
    yield return (object) null;
    List<OffLineQuestRoomBase.ResourceInfo> list = new List<OffLineQuestRoomBase.ResourceInfo>();
    List<QuestTable.QuestTableData> questDataArray = this.arenaData.GetQuestDataArray();
    if (!questDataArray.IsNullOrEmpty<QuestTable.QuestTableData>())
    {
      for (int index1 = 0; index1 < questDataArray.Count; ++index1)
      {
        uint mapId = questDataArray[index1].mapId;
        FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
        if (fieldMapData == null)
          yield break;
        string name = fieldMapData.stageName;
        if (string.IsNullOrEmpty(name))
          name = "ST011D_01";
        StageTable.StageData data = Singleton<StageTable>.I.GetData(name);
        if (data == null)
          yield break;
        list.Add(new OffLineQuestRoomBase.ResourceInfo(RESOURCE_CATEGORY.STAGE_SCENE, data.scene));
        list.Add(new OffLineQuestRoomBase.ResourceInfo(RESOURCE_CATEGORY.STAGE_SKY, data.sky));
        if (!string.IsNullOrEmpty(data.cameraLinkEffect))
          list.Add(new OffLineQuestRoomBase.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffect));
        if (!string.IsNullOrEmpty(data.cameraLinkEffectY0))
          list.Add(new OffLineQuestRoomBase.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffectY0));
        if (!string.IsNullOrEmpty(data.rootEffect))
          list.Add(new OffLineQuestRoomBase.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.rootEffect));
        for (int index2 = 0; index2 < 8; ++index2)
        {
          if (!string.IsNullOrEmpty(data.useEffects[index2]))
            list.Add(new OffLineQuestRoomBase.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, data.useEffects[index2]));
        }
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) questDataArray[index1].enemyID[0]);
        int modelId = enemyData.modelId;
        string enemyBody = ResourceName.GetEnemyBody(modelId);
        string enemyMaterial = ResourceName.GetEnemyMaterial(modelId);
        string enemyAnim = ResourceName.GetEnemyAnim(enemyData.animId);
        if (!string.IsNullOrEmpty(enemyBody))
          list.Add(new OffLineQuestRoomBase.ResourceInfo(RESOURCE_CATEGORY.ENEMY_MODEL, enemyBody));
        if (!string.IsNullOrEmpty(enemyMaterial))
          list.Add(new OffLineQuestRoomBase.ResourceInfo(RESOURCE_CATEGORY.ENEMY_MATERIAL, enemyBody));
        if (!string.IsNullOrEmpty(enemyAnim))
          list.Add(new OffLineQuestRoomBase.ResourceInfo(RESOURCE_CATEGORY.ENEMY_ANIM, enemyAnim));
        if (!string.IsNullOrEmpty(enemyData.baseEffectName))
          list.Add(new OffLineQuestRoomBase.ResourceInfo(RESOURCE_CATEGORY.EFFECT_ACTION, enemyData.baseEffectName));
      }
      if (list.Find((Predicate<OffLineQuestRoomBase.ResourceInfo>) (x => !MonoBehaviourSingleton<ResourceManager>.I.IsCached(x.category, x.packageName))) != null)
      {
        List<string> assetNames = new List<string>();
        foreach (OffLineQuestRoomBase.ResourceInfo resourceInfo in list)
        {
          if (!string.IsNullOrEmpty(resourceInfo.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(resourceInfo.category, resourceInfo.packageName))
            assetNames.Add(resourceInfo.category.ToAssetBundleName(resourceInfo.packageName));
        }
        this.SetButtonEnabled((Enum) QuestAcceptArenaRoom.UI.BTN_START, false);
        yield return (object) ResourceSizeInfo.Init();
        string act = (string) null;
        yield return (object) ResourceSizeInfo.OpenConfirmDialog(ResourceSizeInfo.GetAssetsSizeMB(assetNames.ToArray()), 3002U, CommonDialog.TYPE.YES_NO, (Action<string>) (str => act = str));
        if (act == "NO")
        {
          while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
            yield return (object) null;
          this.DispatchEvent("[BACK]");
        }
        else
        {
          LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
          foreach (OffLineQuestRoomBase.ResourceInfo resourceInfo in list)
          {
            if (!string.IsNullOrEmpty(resourceInfo.packageName) && !MonoBehaviourSingleton<ResourceManager>.I.IsCached(resourceInfo.category, resourceInfo.packageName))
            {
              ResourceManager.downloadOnly = true;
              loadQueue.Load(resourceInfo.category, resourceInfo.packageName, (string[]) null);
              ResourceManager.downloadOnly = false;
              yield return (object) loadQueue.Wait();
            }
          }
          this.SetActive((Enum) QuestAcceptArenaRoom.UI.BTN_START_DISABLE, false);
          this.SetActive((Enum) QuestAcceptArenaRoom.UI.BTN_START, true);
          this.SetButtonEnabled((Enum) QuestAcceptArenaRoom.UI.BTN_START, true);
          assetNames = (List<string>) null;
          loadQueue = (LoadingQueue) null;
        }
      }
    }
  }

  private new enum UI
  {
    GRD_PLAYER_INFO,
    LBL_NAME,
    LBL_LV,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    SPR_USER_READY,
    SPR_USER_READY_WAIT,
    SPR_USER_EMPTY,
    SPR_USER_BATTLE,
    BTN_EMO_0,
    BTN_EMO_1,
    BTN_EMO_2,
    SPR_WEAPON_1,
    SPR_WEAPON_2,
    SPR_WEAPON_3,
    BTN_NAME_BG,
    BTN_FRAME,
    OBJ_CHAT,
    LBL_ARENA_NAME,
    LBL_LIMIT_TIME,
    TBL_LIST,
    LBL_ENEMY_NAME,
    LBL_ENEMY_LEVEL,
    SPR_ELEMENT_ROOT,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    OBJ_ENEMY,
    TEX_ICON,
    BTN_START,
    BTN_NG,
    LBL_LIMIT,
    LBL_CONDITION,
    SPR_TYPE_DIFFICULTY,
    BTN_START_DISABLE,
  }
}
