// Decompiled with JetBrains decompiler
// Type: StatusEnemyDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
[Obsolete]
public class StatusEnemyDetail : GameSection
{
  private StatusEnemyDetail.UI[] rarityTable = new StatusEnemyDetail.UI[4]
  {
    StatusEnemyDetail.UI.OBJ_COMMON_FRAME,
    StatusEnemyDetail.UI.OBJ_RARE_FRAME,
    StatusEnemyDetail.UI.OBJ_BIGRARE_FRAME,
    StatusEnemyDetail.UI.OBJ_FIELD_FRAME
  };
  private readonly string[] RARITY_FUTTER = new string[4]
  {
    "G",
    "B",
    "Y",
    "R"
  };
  private const string UI_BASE_SPRITE_FORMAT = "MonsterWindowBase_{0}";
  private const string UI_TOTAL_SPRITE_FORMAT = "ItemWindowPartsPlate02_{0}";
  private const string UI_FIELD_SPRITE_FORMAT = "ItemWindowPartsPlate01_{0}";
  private const string UI_LINE_FORMAT = "MonsterWindowLine_{0}";
  private const string UI_ATTRIBUTE_BT_FORMAT = "MonsterInfoWindow_attribute_{0}";
  private readonly Dictionary<StatusEnemyDetail.UI, string> UI_COLOR_CHANGE_TARGETS = new Dictionary<StatusEnemyDetail.UI, string>()
  {
    {
      StatusEnemyDetail.UI.TEX_BREAK_REWARD_LINE,
      "MonsterWindowLine_{0}"
    },
    {
      StatusEnemyDetail.UI.TEX_REWARD_LINE,
      "MonsterWindowLine_{0}"
    },
    {
      StatusEnemyDetail.UI.TEX_FLAVOR_LINE,
      "MonsterWindowLine_{0}"
    },
    {
      StatusEnemyDetail.UI.TEX_TOTAL_BG,
      "ItemWindowPartsPlate02_{0}"
    },
    {
      StatusEnemyDetail.UI.TEX_TOTAL_LINE,
      "MonsterWindowLine_{0}"
    },
    {
      StatusEnemyDetail.UI.TEX_FIELD_BG,
      "ItemWindowPartsPlate01_{0}"
    },
    {
      StatusEnemyDetail.UI.TEX_FIELD_LINE,
      "MonsterWindowLine_{0}"
    },
    {
      StatusEnemyDetail.UI.TEX_BG,
      "MonsterWindowBase_{0}"
    },
    {
      StatusEnemyDetail.UI.TEX_ATTRIBUTE_BG,
      "MonsterInfoWindow_attribute_{0}"
    }
  };
  protected uint enemyCollectionId;
  protected List<EnemyCollectionTable.EnemyCollectionData> currentRegionCollectionData;
  protected EnemyTable.EnemyData enemyData;
  protected EnemyCollectionTable.EnemyCollectionData enemyCollectionData;
  protected RegionTable.Data regionData;
  protected List<AchievementCounter> achievementCounter;
  protected List<AchievementCounter> sameMonsterCounter;
  protected UIModelRenderTexture enemyModelRenderTexture;
  protected bool isUnknown;
  protected bool reInitialize;
  protected List<uint> popMapIds;
  protected UIEventListener eventListener;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  public IEnumerator DoInitialize()
  {
    LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
    if (this.reInitialize)
      yield return (object) 0;
    if (loadQueue.IsLoading())
      yield return (object) loadQueue.Wait();
    MonoBehaviourSingleton<UIManager>.I.enableShadow = true;
    if (GameSection.GetEventData() is object[])
    {
      object[] eventData = GameSection.GetEventData() as object[];
      if (eventData.Length == 2)
      {
        this.enemyCollectionId = (uint) eventData[0];
        this.currentRegionCollectionData = eventData[1] as List<EnemyCollectionTable.EnemyCollectionData>;
      }
    }
    if (this.currentRegionCollectionData == null)
    {
      Log.Warning("図鑑データが正しくありません");
    }
    else
    {
      this.enemyData = Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(this.enemyCollectionId).FirstOrDefault<EnemyTable.EnemyData>();
      this.enemyCollectionData = Singleton<EnemyCollectionTable>.I.GetEnemyCollectionData(this.enemyCollectionId);
      this.regionData = Singleton<RegionTable>.I.GetData(this.enemyCollectionData.regionId);
      this.achievementCounter = MonoBehaviourSingleton<AchievementManager>.I.monsterCollectionList;
      this.SetText((Enum) StatusEnemyDetail.UI.STR_TOTAL_DEFEAT_TITLE, "TOTAL_DEFEAT");
      this.SetLabelText((Enum) StatusEnemyDetail.UI.STR_FIELD_DEFEAT_TITLE, string.Format(this.sectionData.GetText("AREA_DEFEAT"), (object) this.regionData.regionName));
      this.SetText((Enum) StatusEnemyDetail.UI.STR_FLAVOR_TITLE, "FLAVOR_TITLE");
      this.SetText((Enum) StatusEnemyDetail.UI.STR_BREAK_REWARD_TITLE, "BREAK_REWARD_TITLE");
      this.SetText((Enum) StatusEnemyDetail.UI.STR_REWARD_TITLE, "DEFEAT_REWARD_TITLE");
      this.OnQuery_TO_FRONT();
      string foundation_name = "";
      this.popMapIds = new List<uint>();
      if (this.enemyCollectionData.collectionType == COLLECTION_TYPE.NORMAL)
      {
        foreach (EnemyTable.EnemyData enemyData in Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(this.enemyCollectionId))
        {
          uint targetEnemyPopMapId = Singleton<FieldMapTable>.I.GetTargetEnemyPopMapID(enemyData.id);
          FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(targetEnemyPopMapId);
          if (fieldMapData != null)
          {
            this.popMapIds.AddRange((IEnumerable<uint>) Singleton<FieldMapTable>.I.GetTargetEnemyPopMapIDs(enemyData.id));
            if (string.IsNullOrEmpty(foundation_name))
              foundation_name = ResourceName.GetFoundationName(fieldMapData.stageName);
          }
        }
      }
      else
      {
        foreach (EnemyTable.EnemyData enemyData in Singleton<EnemyTable>.I.GetEnemyDataByEnemyCollectionId(this.enemyCollectionId))
        {
          IEnumerable<QuestTable.QuestTableData> enemyAppearQuestData = Singleton<QuestTable>.I.GetEnemyAppearQuestData(enemyData.id);
          if (enemyAppearQuestData != null)
          {
            foreach (QuestTable.QuestTableData questTableData in enemyAppearQuestData)
            {
              FieldMapTable.FieldMapTableData[] tableFromQuestId = Singleton<QuestToFieldTable>.I.GetFieldMapTableFromQuestId(questTableData.questID);
              if (tableFromQuestId != null)
              {
                foreach (FieldMapTable.FieldMapTableData fieldMapTableData in tableFromQuestId)
                {
                  this.popMapIds.Add(fieldMapTableData.mapID);
                  if (string.IsNullOrEmpty(foundation_name))
                    foundation_name = enemyAppearQuestData.FirstOrDefault<QuestTable.QuestTableData>().GetFoundationName();
                }
              }
            }
          }
        }
      }
      if (this.popMapIds != null)
        this.popMapIds = this.popMapIds.Where<uint>((Func<uint, bool>) (x => MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) x))).ToList<uint>();
      if (string.IsNullOrEmpty(foundation_name))
        foundation_name = this.sectionData.GetText("DEFAULT_STGE_NAME");
      this.sameMonsterCounter = this.achievementCounter.Where<AchievementCounter>((Func<AchievementCounter, bool>) (x => x.Count != 0L)).Where<AchievementCounter>((Func<AchievementCounter, bool>) (x => (int) Singleton<EnemyCollectionTable>.I.GetEnemyCollectionData((uint) x.subType).enemySpeciesId == (int) this.enemyCollectionData.enemySpeciesId)).ToList<AchievementCounter>();
      this.SetLabelText((Enum) StatusEnemyDetail.UI.LBL_ENEMY_NAME, this.enemyData.name);
      this.SetFrame(this.GetCtrl((Enum) StatusEnemyDetail.UI.OBJ_FRAME), (int) this.enemyCollectionData.collectionType);
      this.isUnknown = !this.sameMonsterCounter.Any<AchievementCounter>((Func<AchievementCounter, bool>) (x => (long) x.subType == (long) this.enemyCollectionId));
      this.SetActive((Enum) StatusEnemyDetail.UI.OBJ_UNKNOWN, this.isUnknown);
      this.SetActive((Enum) StatusEnemyDetail.UI.LBL_UNKNOWN_WEAKPOINT, this.isUnknown || this.enemyData.weakElement == ELEMENT_TYPE.MAX);
      this.SetActive((Enum) StatusEnemyDetail.UI.LBL_ELEMENT, this.isUnknown || this.enemyData.element == ELEMENT_TYPE.MAX);
      this.SetActive((Enum) StatusEnemyDetail.UI.SPR_WEAK_ELEMENT, !this.isUnknown);
      this.SetActive((Enum) StatusEnemyDetail.UI.SPR_ELEMENT, !this.isUnknown);
      this.InitRenderTexture((Enum) StatusEnemyDetail.UI.TEX_ENEMY_MODEL);
      bool is_visible = !this.isUnknown && this.popMapIds != null && this.popMapIds.Any<uint>();
      this.SetActive((Enum) StatusEnemyDetail.UI.OBJ_APPEAR_MAP_ON, is_visible);
      this.SetActive((Enum) StatusEnemyDetail.UI.OBJ_APPEAR_MAP_OFF, !is_visible);
      if (this.isUnknown)
      {
        this.SetRenderEnemyModel((Enum) StatusEnemyDetail.UI.TEX_ENEMY_MODEL, this.enemyData.id, foundation_name, OutGameSettingsManager.EnemyDisplayInfo.SCENE.GACHA, moveType: UIModelRenderTexture.ENEMY_MOVE_TYPE.STOP);
        this.enemyModelRenderTexture = this.GetComponent<UIModelRenderTexture>((Enum) StatusEnemyDetail.UI.TEX_ENEMY_MODEL);
        this.GetComponent<UITexture>((Enum) StatusEnemyDetail.UI.TEX_ENEMY_MODEL).color = Color.black;
        this.SetActive((Enum) StatusEnemyDetail.UI.TEX_ENEMYICON, false);
        this.SetText((Enum) StatusEnemyDetail.UI.LBL_ENEMY_NAME, "???");
        this.SetText((Enum) StatusEnemyDetail.UI.LBL_REWARD_LIST, "???");
        this.SetText((Enum) StatusEnemyDetail.UI.LBL_BREAK_REWARD_LIST, "???");
        this.SetText((Enum) StatusEnemyDetail.UI.LBL_FLAVOR_TEXT, "???");
        this.SetText((Enum) StatusEnemyDetail.UI.LBL_UNKNOWN_WEAKPOINT, "?");
        this.SetText((Enum) StatusEnemyDetail.UI.LBL_ELEMENT, "?");
        this.SetLabelText((Enum) StatusEnemyDetail.UI.LBL_FIELD_DEFEAT, "0");
        this.SetLabelText((Enum) StatusEnemyDetail.UI.LBL_TOTAL_DEFEAT, "0");
      }
      else
      {
        this.SetRenderEnemyModel((Enum) StatusEnemyDetail.UI.TEX_ENEMY_MODEL, this.enemyData.id, foundation_name, OutGameSettingsManager.EnemyDisplayInfo.SCENE.GACHA, moveType: UIModelRenderTexture.ENEMY_MOVE_TYPE.DONT_MOVE);
        this.enemyModelRenderTexture = this.GetComponent<UIModelRenderTexture>((Enum) StatusEnemyDetail.UI.TEX_ENEMY_MODEL);
        this.GetComponent<UITexture>((Enum) StatusEnemyDetail.UI.TEX_ENEMY_MODEL).color = Color.white;
        this.SetActive((Enum) StatusEnemyDetail.UI.TEX_ENEMYICON, true);
        this.SetEnemyIcon((Enum) StatusEnemyDetail.UI.TEX_ENEMYICON, this.enemyData.iconId);
        this.SetElementSprite((Enum) StatusEnemyDetail.UI.SPR_WEAK_ELEMENT, (int) this.enemyData.weakElement);
        this.SetElementSprite((Enum) StatusEnemyDetail.UI.SPR_ELEMENT, (int) this.enemyData.element);
        if (this.enemyData.weakElement == ELEMENT_TYPE.MAX)
          this.SetText((Enum) StatusEnemyDetail.UI.LBL_UNKNOWN_WEAKPOINT, "NONE_WEAK_POINT");
        if (this.enemyData.element == ELEMENT_TYPE.MAX)
          this.SetText((Enum) StatusEnemyDetail.UI.LBL_ELEMENT, "NONE_WEAK_POINT");
        this.SetLabelText((Enum) StatusEnemyDetail.UI.LBL_ENEMY_NAME, this.enemyData.name);
        this.SetLabelText((Enum) StatusEnemyDetail.UI.LBL_REWARD_LIST, this.GetDefeatRewardText());
        this.SetLabelText((Enum) StatusEnemyDetail.UI.LBL_BREAK_REWARD_LIST, this.GetBreakRewardText());
        this.SetLabelText((Enum) StatusEnemyDetail.UI.LBL_FLAVOR_TEXT, this.enemyCollectionData.flavorText);
        this.SetLabelText((Enum) StatusEnemyDetail.UI.LBL_FIELD_DEFEAT, this.sameMonsterCounter.First<AchievementCounter>((Func<AchievementCounter, bool>) (x => (long) x.subType == (long) this.enemyCollectionData.id)).count);
        this.SetLabelText((Enum) StatusEnemyDetail.UI.LBL_TOTAL_DEFEAT, this.sameMonsterCounter.Sum<AchievementCounter>((Func<AchievementCounter, long>) (x => x.Count)).ToString());
      }
      this.eventListener = this.GetComponent<UIEventListener>((Enum) StatusEnemyDetail.UI.TEX_ENEMY_MODEL);
      if (Object.op_Inequality((Object) this.eventListener, (Object) null))
      {
        this.eventListener.onClick += new UIEventListener.VoidDelegate(this.OnTap);
        this.eventListener.onDrag += new UIEventListener.VectorDelegate(this.OnDrag);
      }
      this.reInitialize = false;
      base.Initialize();
    }
  }

  protected override void OnClose()
  {
    if (Object.op_Inequality((Object) this.eventListener, (Object) null))
    {
      this.eventListener.onClick -= new UIEventListener.VoidDelegate(this.OnTap);
      this.eventListener.onDrag -= new UIEventListener.VectorDelegate(this.OnDrag);
    }
    MonoBehaviourSingleton<UIManager>.I.enableShadow = false;
    base.OnClose();
  }

  private void SetFrame(Transform iconRoot, int rarity)
  {
    rarity = Mathf.Clamp(rarity, 0, this.rarityTable.Length - 1);
    for (int index = 0; index < this.rarityTable.Length; ++index)
      this.SetActive(iconRoot, (Enum) this.rarityTable[index], rarity == index);
    foreach (KeyValuePair<StatusEnemyDetail.UI, string> keyValuePair in this.UI_COLOR_CHANGE_TARGETS)
      this.SetSprite((Enum) keyValuePair.Key, string.Format(keyValuePair.Value, (object) this.RARITY_FUTTER[rarity]));
  }

  private void OnTap(GameObject obj)
  {
    if (this.isUnknown || !Object.op_Inequality((Object) this.enemyModelRenderTexture, (Object) null))
      return;
    this.enemyModelRenderTexture.SetApplyEnemyRootMotion(false);
    this.enemyModelRenderTexture.PlayRandomEnemyAnimation();
  }

  private void OnDrag(GameObject obj, Vector2 delta)
  {
    if (Object.op_Equality((Object) this.enemyModelRenderTexture, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable())
      return;
    foreach (Transform transform in this.enemyModelRenderTexture.GetModelTransform().parent)
      transform.Rotate(GameDefine.GetCharaRotateVector(delta));
  }

  private void OnQuery_TO_FLAVOR()
  {
    if (this.reInitialize)
      return;
    this.SetActive((Enum) StatusEnemyDetail.UI.OBJ_FLAVOR, true);
    this.SetActive((Enum) StatusEnemyDetail.UI.OBJ_FRONT, false);
  }

  private void OnQuery_TO_FRONT()
  {
    if (this.reInitialize)
      return;
    this.SetActive((Enum) StatusEnemyDetail.UI.OBJ_FLAVOR, false);
    this.SetActive((Enum) StatusEnemyDetail.UI.OBJ_FRONT, true);
  }

  private void OnQuery_GOTO_APPEAR_FIELD()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("OPEN_REGION_CHANGE", (object) (int) this.regionData.regionId),
      new EventData("VIEW_POP_ENEMY_MAP", (object) this.popMapIds)
    });
  }

  private void OnQuery_RIGHT()
  {
    if (this.reInitialize)
      return;
    this.enemyModelRenderTexture.Clear();
    this.enemyModelRenderTexture = (UIModelRenderTexture) null;
    int index;
    if ((index = this.currentRegionCollectionData.IndexOf(this.enemyCollectionData) + 1) >= this.currentRegionCollectionData.Count)
      index = 0;
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.currentRegionCollectionData[index].id,
      (object) this.currentRegionCollectionData
    });
    this.reInitialize = true;
    this.Initialize();
  }

  private void OnQuery_LEFT()
  {
    if (this.reInitialize)
      return;
    this.enemyModelRenderTexture.Clear();
    this.enemyModelRenderTexture = (UIModelRenderTexture) null;
    int index;
    if ((index = this.currentRegionCollectionData.IndexOf(this.enemyCollectionData) - 1) < 0)
      index = this.currentRegionCollectionData.Count - 1;
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.currentRegionCollectionData[index].id,
      (object) this.currentRegionCollectionData
    });
    this.reInitialize = true;
    this.Initialize();
  }

  private string GetDefeatRewardText()
  {
    string defeatRewardText = "";
    List<uint> uintList = new List<uint>();
    foreach (EnemyFieldDropItemTable.EnemyFieldDropItemData fieldDropItemData in Singleton<EnemyFieldDropItemTable>.I.GetEnemyData(this.enemyData.id))
    {
      if (fieldDropItemData.partIds.Any<int>((Func<int, bool>) (x => x == 0)) && !uintList.Contains(fieldDropItemData.itemId))
      {
        ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(fieldDropItemData.itemId);
        if (!string.IsNullOrEmpty(defeatRewardText))
          defeatRewardText += Environment.NewLine;
        defeatRewardText += itemData.name;
        uintList.Add(fieldDropItemData.itemId);
      }
    }
    if (string.IsNullOrEmpty(defeatRewardText))
      defeatRewardText = this.sectionData.GetText("NO_REWARD");
    return defeatRewardText;
  }

  private string GetBreakRewardText()
  {
    string breakRewardText = "";
    List<uint> uintList = new List<uint>();
    foreach (EnemyFieldDropItemTable.EnemyFieldDropItemData fieldDropItemData in Singleton<EnemyFieldDropItemTable>.I.GetEnemyData(this.enemyData.id))
    {
      if (fieldDropItemData.partIds.Any<int>((Func<int, bool>) (x => x > 0)) && !uintList.Contains(fieldDropItemData.itemId))
      {
        ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(fieldDropItemData.itemId);
        if (!string.IsNullOrEmpty(breakRewardText))
          breakRewardText += Environment.NewLine;
        breakRewardText += itemData.name;
        uintList.Add(fieldDropItemData.itemId);
      }
    }
    if (string.IsNullOrEmpty(breakRewardText))
      breakRewardText = this.sectionData.GetText("NO_REWARD");
    return breakRewardText;
  }

  public enum UI
  {
    TEX_ENEMY_MODEL,
    TEX_ENEMYICON,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_BREAK_REWARD_TITLE,
    STR_REWARD_TITLE,
    STR_TOTAL_DEFEAT_TITLE,
    STR_FIELD_DEFEAT_TITLE,
    STR_FLAVOR_TITLE,
    LBL_ENEMY_NAME,
    LBL_FIELD_DEFEAT,
    LBL_TOTAL_DEFEAT,
    LBL_BREAK_REWARD_LIST,
    LBL_REWARD_LIST,
    LBL_ELEMENT,
    LBL_UNKNOWN_WEAKPOINT,
    LBL_FLAVOR_TEXT,
    OBJ_FRAME,
    OBJ_COMMON_FRAME,
    OBJ_RARE_FRAME,
    OBJ_BIGRARE_FRAME,
    OBJ_FIELD_FRAME,
    OBJ_UNKNOWN,
    OBJ_FRONT,
    OBJ_FLAVOR,
    OBJ_APPEAR_MAP_ON,
    OBJ_APPEAR_MAP_OFF,
    TEX_BREAK_REWARD_LINE,
    TEX_REWARD_LINE,
    TEX_FLAVOR_LINE,
    TEX_TOTAL_BG,
    TEX_TOTAL_LINE,
    TEX_FIELD_BG,
    TEX_FIELD_LINE,
    TEX_BG,
    TEX_ATTRIBUTE_BG,
  }
}
