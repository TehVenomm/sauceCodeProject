// Decompiled with JetBrains decompiler
// Type: QuestSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class QuestSelect : GameSection
{
  private QuestSelect.UI[] difficult = new QuestSelect.UI[10]
  {
    QuestSelect.UI.OBJ_DIFFICULT_STAR_1,
    QuestSelect.UI.OBJ_DIFFICULT_STAR_2,
    QuestSelect.UI.OBJ_DIFFICULT_STAR_3,
    QuestSelect.UI.OBJ_DIFFICULT_STAR_4,
    QuestSelect.UI.OBJ_DIFFICULT_STAR_5,
    QuestSelect.UI.OBJ_DIFFICULT_STAR_6,
    QuestSelect.UI.OBJ_DIFFICULT_STAR_7,
    QuestSelect.UI.OBJ_DIFFICULT_STAR_8,
    QuestSelect.UI.OBJ_DIFFICULT_STAR_9,
    QuestSelect.UI.OBJ_DIFFICULT_STAR_10
  };
  protected QuestInfoData questInfo;
  private bool autoMatchEventIssue;
  private bool createOrderRoomEventIssue;
  private bool isShowDropInfo = true;
  private QuestSelect.UI[] btnInvisibleTween = new QuestSelect.UI[2]
  {
    QuestSelect.UI.OBJ_BACK_BTN_ROOT,
    QuestSelect.UI.OBJ_PARTY_BTN_ROOT
  };
  private bool loadModeRequest;
  private Transform model;
  private PlayerLoader loader;
  private bool loadComplete;
  private bool isCreateOrderRoom;

  public void SuccessChangeEquipSet() => this.isSuccessChangeEquipSet = true;

  public bool isSuccessChangeEquipSet { get; private set; }

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    yield return (object) this._Initialize();
    this.InitializeBase();
  }

  protected void InitializeBase() => base.Initialize();

  protected IEnumerator _Initialize()
  {
    this.questInfo = GameSection.GetEventData() as QuestInfoData;
    if (this.questInfo.questData.tableData.questType == QUEST_TYPE.ORDER)
    {
      QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(this.questInfo.questData.tableData.questID);
      if (questItem != null)
        GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.QUEST_ITEM, questItem.uniqueID);
    }
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    if (this.questInfo != null)
    {
      EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.questInfo.questData.tableData.GetMainEnemyID());
      if (enemyData != null)
        EnemyLoader.CacheUIElementEffect(load_queue, enemyData.element);
    }
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_new");
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_complete");
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
  }

  protected override void OnOpen()
  {
    this.loadComplete = false;
    this.InitEnemyModel();
    MonoBehaviourSingleton<PartyManager>.I.SetPartySetting((PartyManager.PartySetting) null);
    MonoBehaviourSingleton<UIManager>.I.enableShadow = true;
  }

  protected override void OnClose() => MonoBehaviourSingleton<UIManager>.I.enableShadow = false;

  public override void Close(UITransition.TYPE type)
  {
    base.Close(type);
    this.DeleteModel();
  }

  public override void UpdateUI()
  {
    QuestSelect.UI[] uiArray1 = new QuestSelect.UI[3]
    {
      QuestSelect.UI.OBJ_MISSION_INFO_1,
      QuestSelect.UI.OBJ_MISSION_INFO_2,
      QuestSelect.UI.OBJ_MISSION_INFO_3
    };
    QuestSelect.UI[] uiArray2 = new QuestSelect.UI[3]
    {
      QuestSelect.UI.OBJ_TOP_CROWN_1,
      QuestSelect.UI.OBJ_TOP_CROWN_2,
      QuestSelect.UI.OBJ_TOP_CROWN_3
    };
    QuestSelect.UI[] uiArray3 = new QuestSelect.UI[3]
    {
      QuestSelect.UI.LBL_MISSION_INFO_1,
      QuestSelect.UI.LBL_MISSION_INFO_2,
      QuestSelect.UI.LBL_MISSION_INFO_3
    };
    QuestSelect.UI[] uiArray4 = new QuestSelect.UI[3]
    {
      QuestSelect.UI.SPR_MISSION_INFO_CROWN_1,
      QuestSelect.UI.SPR_MISSION_INFO_CROWN_2,
      QuestSelect.UI.SPR_MISSION_INFO_CROWN_3
    };
    QuestSelect.UI[] uiArray5 = new QuestSelect.UI[3]
    {
      QuestSelect.UI.SPR_CROWN_1,
      QuestSelect.UI.SPR_CROWN_2,
      QuestSelect.UI.SPR_CROWN_3
    };
    QuestInfoData info = this.questInfo;
    QUEST_TYPE questType = info.questData.tableData.questType;
    this.SetFontStyle((Enum) QuestSelect.UI.STR_MISSION, (FontStyle) 2);
    this.SetFontStyle((Enum) QuestSelect.UI.STR_TREASURE, (FontStyle) 2);
    this.SetFontStyle((Enum) QuestSelect.UI.STR_SELL, (FontStyle) 2);
    string key;
    switch (questType)
    {
      case QUEST_TYPE.EVENT:
        key = "STR_QUEST_TYPE_EVENT";
        break;
      case QUEST_TYPE.ORDER:
        key = "STR_QUEST_TYPE_ORDER";
        break;
      case QUEST_TYPE.STORY:
        key = "STR_QUEST_TYPE_STORY";
        break;
      default:
        key = "STR_QUEST_TYPE_NORMAL";
        break;
    }
    this.SetText((Enum) QuestSelect.UI.LBL_QUEST_TYPE, key);
    this.SetLabelText((Enum) QuestSelect.UI.LBL_QUEST_NUM, string.Format(this.sectionData.GetText("QUEST_NUMBER"), (object) info.questData.tableData.locationNumber, (object) info.questData.tableData.questNumber));
    this.SetLabelText((Enum) QuestSelect.UI.LBL_QUEST_NAME, info.questData.tableData.questText);
    int limitTime = (int) info.questData.tableData.limitTime;
    this.SetLabelText((Enum) QuestSelect.UI.LBL_LIMIT_TIME, $"{limitTime / 60:D2}:{limitTime % 60:D2}");
    this.SetActive((Enum) QuestSelect.UI.LBL_GUILD_REQUEST_NEED_POINT, false);
    this.SetActive((Enum) QuestSelect.UI.STR_MISSION_EMPTY, false);
    if (!info.isExistMission)
    {
      this.SetActive((Enum) QuestSelect.UI.OBJ_MISSION_INFO_ROOT, false);
    }
    else
    {
      this.SetActive((Enum) QuestSelect.UI.OBJ_MISSION_INFO_ROOT, true);
      int index = 0;
      for (int length = info.missionData.Length; index < length; ++index)
      {
        this.SetActive((Enum) uiArray1[index], info.missionData[index] != null);
        this.SetActive((Enum) uiArray2[index], info.missionData[index] != null);
        if (info.missionData[index] != null)
        {
          this.SetActive((Enum) uiArray4[index], info.missionData[index].state >= CLEAR_STATUS.CLEAR);
          this.SetActive((Enum) uiArray5[index], info.missionData[index].state >= CLEAR_STATUS.CLEAR);
          this.SetLabelText((Enum) uiArray3[index], info.missionData[index].tableData.missionText);
        }
      }
    }
    if (questType == QUEST_TYPE.ORDER)
    {
      this.SetActive((Enum) QuestSelect.UI.OBJ_SELL_ITEM, true);
      QuestItemInfo quest_item = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(info.questData.tableData.questID);
      if (quest_item != null && quest_item.sellItems != null && quest_item.sellItems.Count > 0)
        this.SetGrid((Enum) QuestSelect.UI.GRD_REWARD_SELL, "", quest_item.sellItems.Count, false, (Action<int, Transform, bool>) ((i_2, t_2, is_recycle_2) =>
        {
          QuestItem.SellItem sellItem = quest_item.sellItems[i_2];
          REWARD_TYPE type = (REWARD_TYPE) sellItem.type;
          uint itemId = (uint) sellItem.itemId;
          if (sellItem.num <= 0)
          {
            Log.Error(LOG.OUTGAME, "QuestItem sold get item num is zero. type={0},itemId={1}", (object) type, (object) itemId);
          }
          else
          {
            int num = -1;
            this.SetMaterialInfo(ItemIcon.CreateRewardItemIcon(type, itemId, t_2, num).transform, type, itemId);
          }
        }));
      this.SetActive((Enum) QuestSelect.UI.OBJ_TOP_CROWN_ROOT, false);
    }
    this.SetActive((Enum) QuestSelect.UI.OBJ_TREASURE, true);
    this.SetGrid((Enum) QuestSelect.UI.GRD_REWARD_QUEST, "", 5, false, (Action<int, Transform, bool>) ((i_2, t_2, is_recycle_2) =>
    {
      if (info.questData.reward == null || info.questData.reward.Length <= i_2)
        return;
      REWARD_TYPE type = (REWARD_TYPE) info.questData.reward[i_2].type;
      uint id = (uint) info.questData.reward[i_2].id;
      this.SetMaterialInfo(ItemIcon.CreateRewardItemIcon(type, id, t_2).transform, type, id);
    }));
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) info.questData.tableData.GetMainEnemyID());
    if (enemyData != null)
    {
      int iconId = enemyData.iconId;
      RARITY_TYPE? rarity = info.questData.tableData.questType == QUEST_TYPE.ORDER ? new RARITY_TYPE?(info.questData.tableData.rarity) : new RARITY_TYPE?();
      ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, rarity, this.GetCtrl((Enum) QuestSelect.UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(false);
      ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, rarity, this.GetCtrl((Enum) QuestSelect.UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(false);
    }
    this.SetActive((Enum) QuestSelect.UI.SPR_ELEMENT_ROOT, false);
    if (enemyData != null)
    {
      this.SetActive((Enum) QuestSelect.UI.SPR_ELEMENT_ROOT_2, true);
      this.SetElementSprite((Enum) QuestSelect.UI.SPR_ELEMENT_2, (int) enemyData.element);
      this.SetActive((Enum) QuestSelect.UI.STR_NON_ELEMENT_2, enemyData.element == ELEMENT_TYPE.MAX);
      this.SetElementSprite((Enum) QuestSelect.UI.SPR_WEAK_ELEMENT_2, (int) enemyData.weakElement);
      this.SetActive((Enum) QuestSelect.UI.STR_NON_WEAK_ELEMENT_2, enemyData.weakElement == ELEMENT_TYPE.MAX);
    }
    else
    {
      this.SetActive((Enum) QuestSelect.UI.SPR_ELEMENT_ROOT_2, false);
      this.SetActive((Enum) QuestSelect.UI.STR_NON_WEAK_ELEMENT_2, false);
    }
    this.ShowInfo(questType, this.isShowDropInfo);
    this.SetActive((Enum) QuestSelect.UI.TWN_DIFFICULT_STAR, false);
    ClearStatusQuestEnemySpecies questEnemySpecies = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestEnemySpecies(info.questData.tableData.questID);
    this.SetClearStatus(questEnemySpecies == null ? CLEAR_STATUS.NEW : (CLEAR_STATUS) questEnemySpecies.questStatus);
    if (MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
      return;
    this.SetActive((Enum) QuestSelect.UI.BTN_GUILD_REQUEST, false);
  }

  protected virtual void SetClearStatus(CLEAR_STATUS clear_status)
  {
    int num = 11;
    this.SetToggleGroup((Enum) QuestSelect.UI.OBJ_ICON_NEW, num);
    this.SetToggleGroup((Enum) QuestSelect.UI.OBJ_ICON_CLEARED, num);
    this.SetToggleGroup((Enum) QuestSelect.UI.OBJ_ICON_COMPLETE, num);
    if (clear_status != CLEAR_STATUS.NEW)
    {
      this.SetToggle((Enum) QuestSelect.UI.OBJ_ICON_NEW, false);
      this.SetToggle((Enum) QuestSelect.UI.OBJ_ICON_CLEARED, false);
      this.SetToggle((Enum) QuestSelect.UI.OBJ_ICON_COMPLETE, false);
    }
    else
    {
      this.SetToggle((Enum) QuestSelect.UI.OBJ_ICON_NEW, true);
      this.SetVisibleWidgetEffect((Enum) QuestSelect.UI.SPR_ICON_NEW, "ef_ui_questselect_new");
    }
  }

  private void ShowInfo(QUEST_TYPE quest_type, bool is_show_drop_info)
  {
    if (quest_type != QUEST_TYPE.ORDER)
    {
      this.SetActive((Enum) QuestSelect.UI.OBJ_TREASURE, is_show_drop_info);
      this.SetActive((Enum) QuestSelect.UI.OBJ_MISSION_INFO, !is_show_drop_info);
      this.SetActive((Enum) QuestSelect.UI.OBJ_SELL_ITEM, false);
      this.SetActive((Enum) QuestSelect.UI.OBJ_CHANGE_INFO_TREASURE_ROOT, this.isShowDropInfo);
      this.SetActive((Enum) QuestSelect.UI.OBJ_CHANGE_INFO_MISSION_ROOT, !this.isShowDropInfo);
      this.SetActive((Enum) QuestSelect.UI.OBJ_CHANGE_INFO_SELL_ROOT, false);
    }
    else
    {
      this.SetActive((Enum) QuestSelect.UI.OBJ_MISSION_INFO, false);
      this.SetActive((Enum) QuestSelect.UI.OBJ_TREASURE, is_show_drop_info);
      this.SetActive((Enum) QuestSelect.UI.OBJ_SELL_ITEM, !is_show_drop_info);
      this.SetActive((Enum) QuestSelect.UI.OBJ_CHANGE_INFO_TREASURE_ROOT, this.isShowDropInfo);
      this.SetActive((Enum) QuestSelect.UI.OBJ_CHANGE_INFO_SELL_ROOT, !this.isShowDropInfo);
      this.SetActive((Enum) QuestSelect.UI.OBJ_CHANGE_INFO_MISSION_ROOT, false);
    }
  }

  private void Update()
  {
    if (this.loadModeRequest)
      this.InitEnemyModel();
    if (this.autoMatchEventIssue && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
    {
      this.autoMatchEventIssue = false;
      this.DispatchEvent("AUTO_MATCH");
    }
    if (!this.createOrderRoomEventIssue || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      return;
    this.createOrderRoomEventIssue = false;
    this.DispatchEvent("CREATE_ROOM");
  }

  public void InitEnemyModel()
  {
    if (this.loadComplete)
      return;
    if (this.state != UIBehaviour.STATE.OPEN)
    {
      this.loadModeRequest = true;
    }
    else
    {
      this.LoadModel();
      this.loadModeRequest = false;
    }
  }

  private void DeleteModel()
  {
    this.DeleteRenderTexture((Enum) QuestSelect.UI.TEX_ENEMY);
    this.SetVisibleWidgetEffect((Enum) QuestSelect.UI.TEX_ENEMY, (string) null);
    if (!Object.op_Inequality((Object) this.model, (Object) null))
      return;
    Object.DestroyImmediate((Object) ((Component) this.model).gameObject);
    this.model = (Transform) null;
    this.loader = (PlayerLoader) null;
  }

  private void LoadModel()
  {
    this.DeleteModel();
    int questType = (int) this.questInfo.questData.tableData.questType;
    this.InitLoading();
    if (questType == 3)
      return;
    this.SetRenderEnemyModel((Enum) QuestSelect.UI.TEX_ENEMY, (uint) this.questInfo.questData.tableData.GetMainEnemyID(), this.questInfo.questData.tableData.GetFoundationName(), OutGameSettingsManager.EnemyDisplayInfo.SCENE.QUEST, (Action<bool, EnemyLoader>) ((is_success, enemyLoader) => this.CompleteEnemyLoading()));
  }

  private void InitLoading()
  {
    this.loadComplete = false;
    this.SetActive((Enum) QuestSelect.UI.OBJ_LOADING, true);
  }

  private void CompleteEnemyLoading()
  {
    this.SetActive((Enum) QuestSelect.UI.OBJ_LOADING, false);
    Transform textureModelTransform = this.GetRenderTextureModelTransform((Enum) QuestSelect.UI.TEX_ENEMY);
    if (Object.op_Inequality((Object) textureModelTransform, (Object) null) && MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
      MonoBehaviourSingleton<OutGameEffectManager>.I.ShowSilhoutteffect(textureModelTransform.parent, this.GetRenderTextureLayer((Enum) QuestSelect.UI.TEX_ENEMY));
    EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) this.questInfo.questData.tableData.GetMainEnemyID());
    if (enemyData != null && enemyData.element < ELEMENT_TYPE.MAX)
      this.SetVisibleWidgetEffect((Enum) QuestSelect.UI.TEX_ENEMY, EnemyLoader.GetElementEffectName(enemyData.element));
    if (Object.op_Inequality((Object) textureModelTransform, (Object) null) && MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
      this.StartCoroutine(this.HideSilhoutteEffect());
    this.GetCtrl((Enum) QuestSelect.UI.SPR_LOAD_ROTATE_CIRCLE).localRotation = Quaternion.identity;
    this.loadComplete = true;
  }

  private void CompleteStoryNPCLoading(NPCTable.NPCData npc_data)
  {
    PLCA default_anim = PlayerAnimCtrl.StringToEnum(npc_data.anim);
    this.model.localPosition = new Vector3(0.0f, -1.5f, 1.5f);
    this.model.localEulerAngles = new Vector3(0.0f, 180f, 0.0f);
    PlayerAnimCtrl.Get(this.loader.animator, default_anim);
    this.EnableRenderTexture((Enum) QuestSelect.UI.TEX_ENEMY);
    this.SetActive((Enum) QuestSelect.UI.OBJ_LOADING, false);
    this.GetCtrl((Enum) QuestSelect.UI.SPR_LOAD_ROTATE_CIRCLE).localRotation = Quaternion.identity;
    this.loadComplete = true;
  }

  private IEnumerator HideSilhoutteEffect()
  {
    yield return (object) new WaitForSeconds(0.2f);
    MonoBehaviourSingleton<OutGameEffectManager>.I.HideSilhoutteEffect();
  }

  private void OnQuery_CHANGE_INFO()
  {
    this.ResetTween((Enum) QuestSelect.UI.TWN_CHANGE_BTN);
    this.PlayTween((Enum) QuestSelect.UI.TWN_CHANGE_BTN, is_input_block: false);
    this.isShowDropInfo = !this.isShowDropInfo;
    this.ShowInfo(this.questInfo.questData.tableData.questType, this.isShowDropInfo);
  }

  protected virtual void OnQuery_CREATE_ROOM()
  {
    this.AnimBtnInvisible(true);
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.questInfo.questData.tableData.questType,
      (object) this.questInfo
    });
  }

  public void OnCloseDialog_QuestRoomSettings() => this._OnCloseRoomSettings();

  protected void _OnCloseRoomSettings() => this.AnimBtnInvisible(false);

  public void OnCloseDialog_QuestStartChangeEquipSet() => this._OnCloseStartChangeEquipSet();

  protected void _OnCloseStartChangeEquipSet()
  {
    if (!this.isSuccessChangeEquipSet)
    {
      this.AnimBtnInvisible(false);
    }
    else
    {
      this.isSuccessChangeEquipSet = false;
      this.autoMatchEventIssue = true;
    }
  }

  private void OnQuery_AUTO_MATCH()
  {
    GameSection.StayEvent();
    Action<bool, bool, bool, bool> matching_end_action = (Action<bool, bool, bool, bool>) ((is_m, is_c, is_r, is_s) =>
    {
      if (!is_m)
        this.QuestResume(false);
      else if (is_s)
      {
        UIModelRenderTexture component = this.GetComponent<UIModelRenderTexture>((Enum) QuestSelect.UI.TEX_ENEMY);
        if (Object.op_Inequality((Object) component, (Object) null) && Object.op_Inequality((Object) component.enemyAnimCtrl, (Object) null))
          component.enemyAnimCtrl.PlayQuestStartAnim((System.Action) (() => this.StartCoroutine(this.GoToQuest((System.Action) (() => this.QuestResume(true))))));
        else
          this.StartCoroutine(this.GoToQuest((System.Action) (() => this.QuestResume(true))));
      }
      else if (!is_c)
      {
        GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
        this.QuestResume(true);
      }
      else
        this.QuestResume(false);
    });
    if (this.questInfo.questData.tableData.questType == QUEST_TYPE.ORDER)
      MonoBehaviourSingleton<PartyManager>.I.SendCreate((int) this.questInfo.questData.tableData.questID, new PartyManager.PartySetting(false, 0, 0), (Action<bool>) (is_success =>
      {
        if (is_success)
          CoopApp.EnterPartyQuest(matching_end_action);
        else
          this.QuestResume(false);
      }));
    else
      CoopApp.EnterQuest(matching_end_action);
  }

  private void QuestResume(bool is_success)
  {
    if (!is_success)
      this.AnimBtnInvisible(false);
    GameSection.ResumeEvent(is_success);
  }

  private IEnumerator GoToQuest(System.Action onComplete)
  {
    yield return (object) null;
    yield return (object) new WaitForSeconds(1f);
    onComplete();
  }

  protected void OnQuery_CoopServerInvalidConfirm_YES()
  {
    GameSection.StayEvent();
    CoopApp.EnterQuestOffline((Action<bool, bool, bool, bool>) ((is_m, is_c, is_r, is_s) => GameSection.ResumeEvent(is_s)));
  }

  protected void OnQuery_CoopServerInvalidConfirm_NO() => this.AnimBtnInvisible(false);

  private void OnQuery_QuestOrderCreateRoomConfirm_YES()
  {
    this._OnQueryOrderCreateRoomConfirm_YES();
  }

  protected void _OnQueryOrderCreateRoomConfirm_YES() => this.isCreateOrderRoom = true;

  private void OnCloseDialog_QuestOrderCreateRoomConfirm()
  {
    this._OnCloseDialogOrderCreateRoomConfirm();
  }

  protected void _OnCloseDialogOrderCreateRoomConfirm()
  {
    if (this.isCreateOrderRoom)
      this.createOrderRoomEventIssue = true;
    else
      this.AnimBtnInvisible(false);
    this.isCreateOrderRoom = false;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags) => base.OnNotify(flags);

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags();
  }

  private void AnimBtnInvisible(bool isforward)
  {
    int index = 0;
    for (int length = this.btnInvisibleTween.Length; index < length; ++index)
      this.GetComponent<UITweener>((Enum) this.btnInvisibleTween[index]).Play(isforward);
  }

  protected enum UI
  {
    OBJ_FRAME,
    TEX_ENEMY,
    SPR_LOAD_ROTATE_CIRCLE,
    OBJ_LOADING,
    OBJ_QUEST_NORMAL_ROOT,
    LBL_QUEST_TYPE,
    LBL_QUEST_NAME,
    LBL_QUEST_NUM,
    LBL_LIMIT_TIME,
    LBL_GUILD_REQUEST_NEED_POINT,
    OBJ_TOP_CROWN_ROOT,
    OBJ_TOP_CROWN_1,
    OBJ_TOP_CROWN_2,
    OBJ_TOP_CROWN_3,
    STR_MISSION_EMPTY,
    SPR_CROWN_1,
    SPR_CROWN_2,
    SPR_CROWN_3,
    OBJ_MISSION_INFO_ROOT,
    OBJ_MISSION_INFO,
    OBJ_MISSION_INFO_1,
    OBJ_MISSION_INFO_2,
    OBJ_MISSION_INFO_3,
    LBL_MISSION_INFO_1,
    LBL_MISSION_INFO_2,
    LBL_MISSION_INFO_3,
    SPR_MISSION_INFO_CROWN_1,
    SPR_MISSION_INFO_CROWN_2,
    SPR_MISSION_INFO_CROWN_3,
    STR_MISSION,
    TWN_CHANGE_BTN,
    OBJ_CHANGE_INFO_TREASURE_ROOT,
    OBJ_CHANGE_INFO_MISSION_ROOT,
    OBJ_CHANGE_INFO_SELL_ROOT,
    OBJ_TREASURE,
    STR_TREASURE,
    GRD_REWARD_QUEST,
    OBJ_SELL_ITEM,
    STR_SELL,
    GRD_REWARD_SELL,
    OBJ_ENEMY,
    SPR_MONSTER_ICON,
    SPR_MONSTER_ICON_GRADE_FRAME,
    SPR_ELEMENT_ROOT,
    SPR_ELEMENT,
    SPR_WEAK_ELEMENT,
    STR_NON_ELEMENT,
    STR_NON_WEAK_ELEMENT,
    SPR_ELEMENT_ROOT_2,
    SPR_ELEMENT_2,
    SPR_WEAK_ELEMENT_2,
    STR_NON_ELEMENT_2,
    STR_NON_WEAK_ELEMENT_2,
    BTN_PARTY,
    BTN_BACK,
    OBJ_PARTY_OPT,
    BTN_GUILD_REQUEST,
    TWN_DIFFICULT_STAR,
    OBJ_DIFFICULT_STAR_1,
    OBJ_DIFFICULT_STAR_2,
    OBJ_DIFFICULT_STAR_3,
    OBJ_DIFFICULT_STAR_4,
    OBJ_DIFFICULT_STAR_5,
    OBJ_DIFFICULT_STAR_6,
    OBJ_DIFFICULT_STAR_7,
    OBJ_DIFFICULT_STAR_8,
    OBJ_DIFFICULT_STAR_9,
    OBJ_DIFFICULT_STAR_10,
    OBJ_ICON,
    OBJ_ICON_NEW,
    OBJ_ICON_CLEARED,
    OBJ_ICON_COMPLETE,
    SPR_ICON_NEW,
    SPR_ICON_CLEARED,
    SPR_ICON_COMPLETE,
    OBJ_BACK_BTN_ROOT,
    OBJ_PARTY_BTN_ROOT,
    BTN_NEXT,
    OBJ_NEXT_OPT,
    OBJ_NEXT_BTN_ROOT,
    BTN_SELL,
    STR_BTN_SELL,
    STR_BTN_SELL_D,
    BTN_BATTLE,
    OBJ_REWARD_ICON_ROOT,
    OBJ_MATERIAL_ICON_ROOT,
    LBL_ENEMY_LEVEL,
    OBJ_LEVEL_R,
    OBJ_LEVEL_L,
    OBJ_LEVEL_INACTIVE_R,
    OBJ_LEVEL_INACTIVE_L,
  }
}
