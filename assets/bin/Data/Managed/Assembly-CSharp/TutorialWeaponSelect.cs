// Decompiled with JetBrains decompiler
// Type: TutorialWeaponSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class TutorialWeaponSelect : GameSection
{
  private EquipSetInfo[] equipSet;
  private EquipSetCalculator[] equipSetCalc;
  protected PlayerLoader loader;
  protected PlayerLoader preloader;
  private PlayerLoadInfo playerLoadInfo;
  private bool loadedModel;
  public EquipSetInfo[] localEquipSet;
  public int equipSetNo;
  private int SET_NO_MAX = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
  private TutorialWeaponSelect.EQUIP_SET_COPY_MODE equipSetCopyMode;
  private int equipSetCopyNo;
  private StatusEquipSetCopyModel.RequestSendForm equipSetCopyForm;
  private bool showEquipMode = true;
  private TutorialWeaponSelect.UI[] icons = new TutorialWeaponSelect.UI[5]
  {
    TutorialWeaponSelect.UI.OBJ_ICON_WEAPON_1,
    TutorialWeaponSelect.UI.OBJ_ICON_ARMOR,
    TutorialWeaponSelect.UI.OBJ_ICON_HELM,
    TutorialWeaponSelect.UI.OBJ_ICON_ARM,
    TutorialWeaponSelect.UI.OBJ_ICON_LEG
  };
  private EQUIPMENT_TYPE[] visualType = new EQUIPMENT_TYPE[4]
  {
    EQUIPMENT_TYPE.ARMOR,
    EQUIPMENT_TYPE.HELM,
    EQUIPMENT_TYPE.ARM,
    EQUIPMENT_TYPE.LEG
  };
  private TutorialWeaponSelect.UI[] iconsBtn = new TutorialWeaponSelect.UI[5]
  {
    TutorialWeaponSelect.UI.BTN_ICON_WEAPON_1,
    TutorialWeaponSelect.UI.BTN_ICON_ARMOR,
    TutorialWeaponSelect.UI.BTN_ICON_HELM,
    TutorialWeaponSelect.UI.BTN_ICON_ARM,
    TutorialWeaponSelect.UI.BTN_ICON_LEG
  };
  private TutorialWeaponSelect.UI[] iconsVisual = new TutorialWeaponSelect.UI[4]
  {
    TutorialWeaponSelect.UI.OBJ_ICON_VISUAL_ARMOR,
    TutorialWeaponSelect.UI.OBJ_ICON_VISUAL_HELM,
    TutorialWeaponSelect.UI.OBJ_ICON_VISUAL_ARM,
    TutorialWeaponSelect.UI.OBJ_ICON_VISUAL_LEG
  };
  private TutorialWeaponSelect.UI[] iconsVisualBtn = new TutorialWeaponSelect.UI[4]
  {
    TutorialWeaponSelect.UI.BTN_ICON_VISUAL_ARMOR_BASE,
    TutorialWeaponSelect.UI.BTN_ICON_VISUAL_HELM_BASE,
    TutorialWeaponSelect.UI.BTN_ICON_VISUAL_ARM_BASE,
    TutorialWeaponSelect.UI.BTN_ICON_VISUAL_LEG_BASE
  };
  private TutorialWeaponSelect.UI[] lblEquipLevel = new TutorialWeaponSelect.UI[5]
  {
    TutorialWeaponSelect.UI.LBL_LEVEL_WEAPON_1,
    TutorialWeaponSelect.UI.LBL_LEVEL_ARMOR,
    TutorialWeaponSelect.UI.LBL_LEVEL_HELM,
    TutorialWeaponSelect.UI.LBL_LEVEL_ARM,
    TutorialWeaponSelect.UI.LBL_LEVEL_LEG
  };
  private TutorialWeaponSelect.UI[] lblShadowEquipLevel = new TutorialWeaponSelect.UI[5]
  {
    TutorialWeaponSelect.UI.LBL_LEVEL_WEAPON_1_SHADOW,
    TutorialWeaponSelect.UI.LBL_LEVEL_ARMOR_SHADOW,
    TutorialWeaponSelect.UI.LBL_LEVEL_HELM_SHADOW,
    TutorialWeaponSelect.UI.LBL_LEVEL_ARM_SHADOW,
    TutorialWeaponSelect.UI.LBL_LEVEL_LEG_SHADOW
  };
  private StatusManager.LocalVisual visualEquip;
  private TutorialWeaponSelect.UI? tweenTarget;
  private UICenterOnChild uiCenterOnChild;
  private string[] maleVoice = new string[5]
  {
    "ACV_00300015",
    "ACV_00000001",
    "ACV_00200016",
    "ACV_00100090",
    "ACV_00200017"
  };
  private string[] femaleVoice = new string[5]
  {
    "ACV_00310014",
    "ACV_00010018",
    "ACV_00210014",
    "NPV_00300002",
    "NPV_00300001"
  };
  private int[] mvoices = new int[5]
  {
    300015,
    1,
    200016,
    100090,
    200017
  };
  private int[] fvoices = new int[5]
  {
    310014,
    10018,
    210014,
    300002,
    300001
  };
  private bool isInit;
  private bool isHideLoading;
  private int sexId;
  private int localEquipSetNo;

  public override void Initialize()
  {
    base.Initialize();
    this.preloader = ((Component) this).gameObject.AddComponent<PlayerLoader>();
    this.UpdateStr();
    this.SetActive((Enum) TutorialWeaponSelect.UI.OBJ_STATUS_UI_ROOT, false);
    MonoBehaviourSingleton<UIManager>.I.loading.ShowTutorialBg(true);
    this.StartCoroutine(this.Init());
  }

  private IEnumerator Init()
  {
    while (!MonoBehaviourSingleton<GameSceneManager>.I.isInitialized || Singleton<GrowEquipItemTable>.I.GrowTableData == null || Singleton<TutorialGearSetTable>.I.ItemTable == null)
      yield return (object) null;
    this.StartCoroutine(this.ShowInfo());
    UIntKeyTable<TutorialGearSetTable.ItemData> itemTable = Singleton<TutorialGearSetTable>.I.ItemTable;
    this.equipSet = new EquipSetInfo[itemTable.GetCount()];
    itemTable.ForEach((Action<TutorialGearSetTable.ItemData>) (o => this.equipSet[(int) o.id - 1] = new EquipSetInfo(o)));
    int length = this.equipSet.Length;
    this.equipSetCalc = new EquipSetCalculator[length];
    for (int setNo = 0; setNo < length; ++setNo)
    {
      this.equipSetCalc[setNo] = new EquipSetCalculator();
      this.equipSetCalc[setNo].SetEquipSet(this.equipSet[setNo], setNo);
    }
    this.CreateLocalEquipSetData();
    this.isInit = true;
    this.RefreshUI();
    this.PreLoadModel();
    MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_2_name_creation, "Tutorial");
    Debug.LogWarning((object) ("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_2_name_creation.ToString()));
    MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_2_name_creation, "Tutorial");
  }

  private void PreLoadModel() => this.StartCoroutine(this.IEPreLoadModel());

  private IEnumerator IEPreLoadModel()
  {
    while (Object.op_Equality((Object) this.preloader, (Object) null))
      yield return (object) null;
    for (int i = 0; i < this.equipSet.Length; ++i)
    {
      EquipSetInfo equip = this.equipSet[i];
      PlayerLoadInfo playerLoadInfoMale = PlayerLoadInfo.GenerateForTutorial(0, equip.item[0].tableID, equip.item[3].tableID, equip.item[4].tableID, equip.item[5].tableID, equip.item[6].tableID);
      PlayerLoadInfo playerLoadInfoFemale = PlayerLoadInfo.GenerateForTutorial(1, equip.item[0].tableID, equip.item[3].tableID, equip.item[4].tableID, equip.item[5].tableID, equip.item[6].tableID);
      while (this.preloader.isLoading)
        yield return (object) null;
      this.preloader.StartLoad(playerLoadInfoMale, ((Component) this).gameObject.layer, 99, false, false, true, true, false, false, true, true, SHADER_TYPE.NORMAL, (PlayerLoader.OnCompleteLoad) null);
      while (this.preloader.isLoading)
        yield return (object) null;
      this.preloader.StartLoad(playerLoadInfoFemale, ((Component) this).gameObject.layer, 99, false, false, true, true, false, false, true, true, SHADER_TYPE.NORMAL, (PlayerLoader.OnCompleteLoad) null);
      playerLoadInfoMale = (PlayerLoadInfo) null;
      playerLoadInfoFemale = (PlayerLoadInfo) null;
    }
    Object.Destroy((Object) this.preloader);
    this.StartCoroutine(this.IEPreloadVoice());
    this.HideLoading();
    this.RefreshUI();
  }

  private IEnumerator IEPreloadVoice()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    foreach (int mvoice in this.mvoices)
      loadingQueue.CacheActionVoice(mvoice);
    foreach (int fvoice in this.fvoices)
    {
      switch (fvoice)
      {
        case 300001:
        case 300002:
          loadingQueue.CacheVoice(fvoice);
          break;
        default:
          loadingQueue.CacheActionVoice(fvoice);
          break;
      }
    }
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
  }

  private void HideLoading()
  {
    MonoBehaviourSingleton<UIManager>.I.HideGGTutorialMessage();
    MonoBehaviourSingleton<UIManager>.I.loading.ShowTutorialBg(false);
    this.isHideLoading = true;
  }

  private IEnumerator ShowInfo()
  {
    yield return (object) new WaitForEndOfFrame();
    this.DispatchEvent("INFO", (object) this.sectionData.GetText("STR_INFO"));
  }

  private void UpdateStr()
  {
    this.SetLabelText(this.GetCtrl((Enum) TutorialWeaponSelect.UI.BTN_NEXT), (Enum) TutorialWeaponSelect.UI.LBL_NEXT, StringTable.Get(STRING_CATEGORY.COMMON, 19795U));
    this.SetLabelText(this.GetCtrl((Enum) TutorialWeaponSelect.UI.BTN_PRE), (Enum) TutorialWeaponSelect.UI.LBL_PRE, StringTable.Get(STRING_CATEGORY.COMMON, 19796U));
    this.SetLabelText(this.GetCtrl((Enum) TutorialWeaponSelect.UI.BTN_MALE), (Enum) TutorialWeaponSelect.UI.LBL_MALE, StringTable.Get(STRING_CATEGORY.COMMON, 19793U));
    this.SetLabelText(this.GetCtrl((Enum) TutorialWeaponSelect.UI.BTN_FEMALE), (Enum) TutorialWeaponSelect.UI.LBL_FEMALE, StringTable.Get(STRING_CATEGORY.COMMON, 19794U));
    this.SetLabelText(this.GetCtrl((Enum) TutorialWeaponSelect.UI.OBJ_STATUS_UI_ROOT), (Enum) TutorialWeaponSelect.UI.LBL_POPUP_NAME, StringTable.Get(STRING_CATEGORY.COMMON, 19797U));
    this.SetLabelText(this.GetCtrl((Enum) TutorialWeaponSelect.UI.BTN_SELECT), (Enum) TutorialWeaponSelect.UI.LBL_SELECT, StringTable.Get(STRING_CATEGORY.COMMON, 19801U));
  }

  public override void UpdateUI()
  {
    if (!this.isInit)
      return;
    this.SetActive((Enum) TutorialWeaponSelect.UI.OBJ_STATUS_UI_ROOT, true);
    EquipSetInfo localEquip = this.localEquipSet[this.localEquipSetNo];
    int index1 = 0;
    for (int index2 = 1; index1 < index2; ++index1)
    {
      EquipItemInfo equipItemInfo = localEquip.item[this.GetDataIndex(index1)];
      ItemIcon iconByEquipItemInfo = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, this.sexId, this.GetCtrl((Enum) this.icons[index1]), event_name: "DETAIL", event_data: index1);
      int num = -1;
      string text = string.Empty;
      if (equipItemInfo != null && equipItemInfo.tableID != 0U)
      {
        num = equipItemInfo.tableData.GetIconID(this.sexId);
        text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1U), (object) equipItemInfo.level);
      }
      ((Component) iconByEquipItemInfo).gameObject.SetActive(num != -1);
      this.SetEvent((Enum) this.iconsBtn[index1], num != -1 ? "DETAIL" : "EQUIP", index1);
      this.SetLabelText((Enum) this.lblEquipLevel[index1], text);
      this.SetLabelText((Enum) this.lblShadowEquipLevel[index1], text);
      if (num != -1)
        iconByEquipItemInfo.SetEquipExt(equipItemInfo, this.GetComponent<UILabel>((Enum) this.lblEquipLevel[index1]));
      Transform ctrl = this.GetCtrl((Enum) this.iconsBtn[index1]);
      bool flag = equipItemInfo != null && equipItemInfo.tableID > 0U;
      if (flag)
        this.SetSkillIconButton(ctrl, (Enum) TutorialWeaponSelect.UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButtonTOP", equipItemInfo.tableData, this.GetSkillSlotData(localEquip, equipItemInfo), button_event_data: index1);
      ((Component) this.FindCtrl(ctrl, (Enum) TutorialWeaponSelect.UI.OBJ_SKILL_BUTTON_ROOT)).gameObject.SetActive(flag);
    }
    if (this.tweenTarget.HasValue)
    {
      this.ResetTween((Enum) (ValueType) this.tweenTarget);
      this.PlayTween((Enum) (ValueType) this.tweenTarget, is_input_block: false);
    }
    this.SetActive((Enum) TutorialWeaponSelect.UI.OBJ_STUDIO_BUTTON_ROOT, this.showEquipMode);
    this.SetActive((Enum) TutorialWeaponSelect.UI.TGL_VISIBLE_UI_BUTTON, !this.showEquipMode);
    this.SetToggle((Enum) TutorialWeaponSelect.UI.TGL_SHOW_EQUIP_TYPE, this.showEquipMode);
    this.SetDynamicList((Enum) TutorialWeaponSelect.UI.GRD_DRUM, "equipno", this.SET_NO_MAX, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.SetLabelText(t, (Enum) TutorialWeaponSelect.UI.LBL_EQUIP_NO, (i + 1).ToString())));
    this.UpdateStatUI(this.sexId, localEquip);
    base.UpdateUI();
  }

  private void UpdateStatUI(int sex, EquipSetInfo equip_set)
  {
    EquipItemInfo equipItemInfo = equip_set.item[0];
    this.playerLoadInfo = PlayerLoadInfo.GenerateForTutorial(sex, equipItemInfo.tableID, equip_set.item[3].tableID, equip_set.item[4].tableID, equip_set.item[5].tableID, equip_set.item[6].tableID);
    this.SetRenderPlayerModel(this.playerLoadInfo);
    this.UpdateStat();
    this.SetSprite((Enum) TutorialWeaponSelect.UI.SPR_SP_ATTACK_TYPE, equipItemInfo.tableData.spAttackType.GetBigFrameSpriteName());
    Transform ctrl1 = this.GetCtrl((Enum) TutorialWeaponSelect.UI.SPR_TYPE_ICON_BG);
    Transform ctrl2 = this.FindCtrl(ctrl1, (Enum) TutorialWeaponSelect.UI.SPR_TYPE_ICON);
    Transform ctrl3 = this.FindCtrl(ctrl1, (Enum) TutorialWeaponSelect.UI.SPR_TYPE_ICON_RARITY);
    this.SetEquipmentTypeIcon(ctrl2, ctrl1, ctrl3, equipItemInfo.tableData);
    this.SetActive(ctrl3, false);
    this.SetLabelText(this.GetCtrl((Enum) TutorialWeaponSelect.UI.OBJ_STATUS_UI_ROOT), (Enum) TutorialWeaponSelect.UI.LBL_SET_TIER, string.Format(StringTable.Get(STRING_CATEGORY.COMMON, 19792U), (object) equip_set.tier));
    this.SetLabelText(this.GetCtrl((Enum) TutorialWeaponSelect.UI.OBJ_EQUIP_SET_NAME), (Enum) TutorialWeaponSelect.UI.LBL_SET_NAME, equip_set.name);
  }

  private int GetDataIndex(int i) => i > 0 ? i + 2 : 0;

  protected SkillSlotUIData[] GetSkillSlotData(EquipSetInfo setInfo, EquipItemInfo equip)
  {
    if (equip == null)
      return (SkillSlotUIData[]) null;
    int maxSlot = equip.GetMaxSlot();
    if (maxSlot == 0)
      return (SkillSlotUIData[]) null;
    SkillSlotUIData[] skillSlotData = new SkillSlotUIData[maxSlot];
    this.GetCurrentEquipSetNo();
    SkillItemInfo[] skillItemInfoArray = new SkillItemInfo[1]
    {
      new SkillItemInfo(0, (int) setInfo.skillId, 1, 4)
    };
    if (skillItemInfoArray != null && skillItemInfoArray.Length > maxSlot)
      Log.Error("Attach Skill Num is Over Skill Slot Num");
    SkillItemTable.SkillSlotData[] skillSlot = equip.tableData.GetSkillSlot(equip.exceed);
    if (equip.tableData.type == EQUIPMENT_TYPE.ONE_HAND_SWORD || equip.tableData.type == EQUIPMENT_TYPE.PAIR_SWORDS || equip.tableData.type == EQUIPMENT_TYPE.SPEAR || equip.tableData.type == EQUIPMENT_TYPE.TWO_HAND_SWORD || equip.tableData.type == EQUIPMENT_TYPE.ARROW)
    {
      skillSlotData[0] = new SkillSlotUIData();
      skillSlotData[0].slotData = new SkillItemTable.SkillSlotData(skillItemInfoArray[0].tableData.id, skillSlot[0].slotType);
      skillSlotData[0].itemData = skillItemInfoArray[0];
    }
    int index = 0;
    for (int length = skillSlotData.Length; index < length; ++index)
    {
      if (skillSlotData[index] == null)
      {
        skillSlotData[index] = new SkillSlotUIData();
        skillSlotData[index].slotData = new SkillItemTable.SkillSlotData(0U, equip.tableData.GetSkillSlot(equip.exceed)[index].slotType);
      }
    }
    return skillSlotData;
  }

  protected int GetCurrentEquipSetNo()
  {
    return MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo();
  }

  public void CreateLocalEquipSetData()
  {
    this.localEquipSet = new EquipSetInfo[this.equipSet.Length];
    this.localEquipSetNo = 0;
    int set_no = 0;
    for (int length = this.localEquipSet.Length; set_no < length; ++set_no)
    {
      EquipSetInfo equipSet = this.GetEquipSet(set_no);
      this.localEquipSet[set_no] = new EquipSetInfo(new EquipItemInfo[7]
      {
        equipSet.item[0],
        equipSet.item[1],
        equipSet.item[2],
        equipSet.item[3],
        equipSet.item[4],
        equipSet.item[5],
        equipSet.item[6]
      }, equipSet.name, equipSet.tier, equipSet.skillId);
    }
    int showHelm;
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || this.localEquipSet[this.localEquipSetNo].showHelm == (showHelm = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm))
      return;
    int index = 0;
    for (int length = this.localEquipSet.Length; index < length; ++index)
      this.localEquipSet[index].showHelm = showHelm;
  }

  protected void SetRenderPlayerModel(PlayerLoadInfo load_player_info)
  {
    this.StopCoroutine(this.IERender(load_player_info));
    this.StartCoroutine(this.IERender(load_player_info));
  }

  private IEnumerator IERender(PlayerLoadInfo load_player_info)
  {
    while (!this.isHideLoading)
      yield return (object) null;
    this.loadedModel = false;
    this.SetRenderPlayerModel(this.GetCtrl((Enum) TutorialWeaponSelect.UI.OBJ_MODEL_ROOT), (Enum) TutorialWeaponSelect.UI.TEX_MODEL, load_player_info, PLAYER_ANIM_TYPE.GetStatus(this.sexId), new Vector3(0.0f, -0.75f, 14f), new Vector3(0.0f, 180f, 0.0f), true, (Action<PlayerLoader>) (player_loader =>
    {
      this.loadedModel = true;
      if (Object.op_Inequality((Object) player_loader, (Object) null))
        this.loader = player_loader;
      this.PlayVoice();
    }));
  }

  private void PlayVoice()
  {
    int[] numArray = this.sexId != 0 ? this.fvoices : this.mvoices;
    int voice_id = this.localEquipSetNo < numArray.Length ? numArray[this.localEquipSetNo] : numArray[0];
    if (!this.IsActive((Enum) TutorialWeaponSelect.UI.OBJ_STATUS_UI_ROOT))
      return;
    if (voice_id == 300001 || voice_id == 300002)
      SoundManager.PlayVoice(voice_id);
    else
      SoundManager.PlayActionVoice(voice_id);
  }

  public EquipSetInfo GetEquipSet(int set_no)
  {
    return set_no >= this.equipSet.Length ? (EquipSetInfo) null : this.equipSet[set_no];
  }

  public void OnQuery_WEAPON_SELECT_NEXT()
  {
    if (!this.loadedModel)
      return;
    ++this.localEquipSetNo;
    if (this.localEquipSetNo >= this.equipSet.Length)
      this.localEquipSetNo = 0;
    this.RefreshUI();
  }

  public void OnQuery_WEAPON_SELECT_PRE()
  {
    if (!this.loadedModel)
      return;
    --this.localEquipSetNo;
    if (this.localEquipSetNo < 0)
      this.localEquipSetNo = this.equipSet.Length - 1;
    this.RefreshUI();
  }

  public void OnQuery_WEAPON_SELECT_SELECT()
  {
    GameSection.SetEventData((object) this.sexId);
    PlayerPrefs.SetInt("Tut_Armor", (int) this.localEquipSet[this.localEquipSetNo].item[3].tableID);
    PlayerPrefs.SetInt("Tut_Arm", (int) this.localEquipSet[this.localEquipSetNo].item[5].tableID);
    PlayerPrefs.SetInt("Tut_Head", (int) this.localEquipSet[this.localEquipSetNo].item[6].tableID);
    PlayerPrefs.SetInt("Tut_Leg", (int) this.localEquipSet[this.localEquipSetNo].item[4].tableID);
    PlayerPrefs.SetInt("Tut_Weapon", (int) this.localEquipSet[this.localEquipSetNo].item[0].tableID);
    PlayerPrefs.SetInt("Tut_Sex", this.sexId);
    PlayerPrefs.SetInt("Tut_Weapon_Type", this.playerLoadInfo.weaponModelID / 1000);
  }

  public void UpdateStat()
  {
    SimpleStatus finalStatus = this.equipSetCalc[this.localEquipSetNo].GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
    this.SetLabelText((Enum) TutorialWeaponSelect.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText((Enum) TutorialWeaponSelect.UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
    this.SetLabelText((Enum) TutorialWeaponSelect.UI.LBL_HP, finalStatus.hp.ToString());
  }

  public void OnQuery_WEAPON_SELECT_MALE()
  {
    if (this.sexId == 0)
      return;
    this.sexId = 0;
    this.RefreshUI();
  }

  public void OnQuery_WEAPON_SELECT_FEMALE()
  {
    if (this.sexId == 1)
      return;
    this.sexId = 1;
    this.RefreshUI();
  }

  public override void Exit()
  {
    base.Exit();
    if (MonoBehaviourSingleton<LoadingProcess>.IsValid())
      return;
    ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<InGameTutorialManager>();
  }

  protected void OnEnable()
  {
    InputManager.OnDragAlways += new InputManager.OnTouchDelegate(this.OnDrag);
  }

  protected void OnDisable()
  {
    InputManager.OnDragAlways -= new InputManager.OnTouchDelegate(this.OnDrag);
  }

  private void OnDrag(InputManager.TouchInfo touch_info)
  {
    if (Object.op_Equality((Object) this.loader, (Object) null))
      return;
    ((Component) this.loader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
  }

  private enum UI
  {
    OBJ_STATUS_UI_ROOT,
    OBJ_EQUIP_SET_NAME,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    BTN_EQUIP_SET_L,
    BTN_EQUIP_SET_R,
    LBL_NOW,
    LBL_MAX,
    OBJ_EQUIP_ROOT,
    OBJ_EQUIP_ROT_ROOT,
    OBJ_ICON_WEAPON_1,
    OBJ_ICON_ARMOR,
    OBJ_ICON_HELM,
    OBJ_ICON_ARM,
    OBJ_ICON_LEG,
    BTN_ICON_WEAPON_1,
    BTN_ICON_ARMOR,
    BTN_ICON_HELM,
    BTN_ICON_ARM,
    BTN_ICON_LEG,
    OBJ_VISUAL_ROOT,
    OBJ_ICON_VISUAL_ARMOR,
    OBJ_ICON_VISUAL_HELM,
    OBJ_ICON_VISUAL_ARM,
    OBJ_ICON_VISUAL_LEG,
    BTN_ICON_VISUAL_ARMOR_BASE,
    BTN_ICON_VISUAL_HELM_BASE,
    BTN_ICON_VISUAL_ARM_BASE,
    BTN_ICON_VISUAL_LEG_BASE,
    SPR_AVATAR_ACTIVE,
    SPR_PARAMETER_ACTIVE,
    BTN_AVATAR_INACTIVE,
    BTN_PARAMETER_INACTIVE,
    OBJ_EQUIP_SET_SELECT,
    OBJ_STUDIO_BUTTON_ROOT,
    OBJ_AVATAR_BUTTON_ROOT,
    OBJ_PARAMETER_BUTTON_ROOT,
    BTN_VISIBLE_UI,
    BTN_INVISIBLE_UI,
    BTN_VISIBLE_HELM,
    BTN_INVISIBLE_HELM,
    TGL_VISIBLE_HELM_BUTTON,
    TGL_VISIBLE_UI_BUTTON,
    LBL_LEVEL_WEAPON_1,
    LBL_LEVEL_ARMOR,
    LBL_LEVEL_HELM,
    LBL_LEVEL_ARM,
    LBL_LEVEL_LEG,
    LBL_LEVEL_WEAPON_1_SHADOW,
    LBL_LEVEL_ARMOR_SHADOW,
    LBL_LEVEL_HELM_SHADOW,
    LBL_LEVEL_ARM_SHADOW,
    LBL_LEVEL_LEG_SHADOW,
    TGL_SHOW_EQUIP_TYPE,
    BTN_STUDIO,
    BTN_EQUIPLIST,
    OBJ_SKILL_BUTTON_ROOT,
    OBJ_WITH_MONSTER_ROOT,
    OBJ_WITHOUT_MONSTER_ROOT,
    BTN_EQUIP_SET_COPY,
    BTN_EQUIP_SET_PASTE,
    BTN_EQUIP_SET_DELETE,
    LBL_SET_NAME,
    SCR_DRUM,
    GRD_DRUM,
    LBL_EQUIP_NO,
    TEX_MODEL,
    OBJ_MODEL_ROOT,
    LBL_SET_TIER,
    SPR_SP_ATTACK_TYPE,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    SPR_TYPE_ICON,
    LBL_POPUP_NAME,
    LBL_NEXT,
    LBL_PRE,
    LBL_MALE,
    LBL_FEMALE,
    BTN_MALE,
    BTN_FEMALE,
    BTN_NEXT,
    BTN_PRE,
    BTN_SELECT,
    LBL_SELECT,
  }

  private enum EQUIP_SET_COPY_MODE
  {
    NONE,
    COPY,
  }
}
