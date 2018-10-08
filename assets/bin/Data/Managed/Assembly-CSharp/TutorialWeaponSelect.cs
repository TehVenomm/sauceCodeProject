using System;
using System.Collections;
using UnityEngine;

public class TutorialWeaponSelect : GameSection
{
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
		LBL_SELECT
	}

	private enum EQUIP_SET_COPY_MODE
	{
		NONE,
		COPY
	}

	private EquipSetInfo[] equipSet;

	private EquipSetCalculator[] equipSetCalc;

	protected PlayerLoader loader;

	protected PlayerLoader preloader;

	private PlayerLoadInfo playerLoadInfo;

	private bool loadedModel;

	public EquipSetInfo[] localEquipSet;

	public int equipSetNo;

	private int SET_NO_MAX = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();

	private EQUIP_SET_COPY_MODE equipSetCopyMode;

	private int equipSetCopyNo;

	private StatusEquipSetCopyModel.RequestSendForm equipSetCopyForm;

	private bool showEquipMode = true;

	private UI[] icons = new UI[5]
	{
		UI.OBJ_ICON_WEAPON_1,
		UI.OBJ_ICON_ARMOR,
		UI.OBJ_ICON_HELM,
		UI.OBJ_ICON_ARM,
		UI.OBJ_ICON_LEG
	};

	private EQUIPMENT_TYPE[] visualType = new EQUIPMENT_TYPE[4]
	{
		EQUIPMENT_TYPE.ARMOR,
		EQUIPMENT_TYPE.HELM,
		EQUIPMENT_TYPE.ARM,
		EQUIPMENT_TYPE.LEG
	};

	private UI[] iconsBtn = new UI[5]
	{
		UI.BTN_ICON_WEAPON_1,
		UI.BTN_ICON_ARMOR,
		UI.BTN_ICON_HELM,
		UI.BTN_ICON_ARM,
		UI.BTN_ICON_LEG
	};

	private UI[] iconsVisual = new UI[4]
	{
		UI.OBJ_ICON_VISUAL_ARMOR,
		UI.OBJ_ICON_VISUAL_HELM,
		UI.OBJ_ICON_VISUAL_ARM,
		UI.OBJ_ICON_VISUAL_LEG
	};

	private UI[] iconsVisualBtn = new UI[4]
	{
		UI.BTN_ICON_VISUAL_ARMOR_BASE,
		UI.BTN_ICON_VISUAL_HELM_BASE,
		UI.BTN_ICON_VISUAL_ARM_BASE,
		UI.BTN_ICON_VISUAL_LEG_BASE
	};

	private UI[] lblEquipLevel = new UI[5]
	{
		UI.LBL_LEVEL_WEAPON_1,
		UI.LBL_LEVEL_ARMOR,
		UI.LBL_LEVEL_HELM,
		UI.LBL_LEVEL_ARM,
		UI.LBL_LEVEL_LEG
	};

	private UI[] lblShadowEquipLevel = new UI[5]
	{
		UI.LBL_LEVEL_WEAPON_1_SHADOW,
		UI.LBL_LEVEL_ARMOR_SHADOW,
		UI.LBL_LEVEL_HELM_SHADOW,
		UI.LBL_LEVEL_ARM_SHADOW,
		UI.LBL_LEVEL_LEG_SHADOW
	};

	private StatusManager.LocalVisual visualEquip;

	private UI? tweenTarget;

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
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize();
		preloader = this.get_gameObject().AddComponent<PlayerLoader>();
		UpdateStr();
		SetActive((Enum)UI.OBJ_STATUS_UI_ROOT, false);
		MonoBehaviourSingleton<UIManager>.I.loading.ShowTutorialBg(true);
		this.StartCoroutine(Init());
	}

	private IEnumerator Init()
	{
		while (!MonoBehaviourSingleton<GameSceneManager>.I.isInitialized || Singleton<GrowEquipItemTable>.I.GrowTableData == null || Singleton<TutorialGearSetTable>.I.ItemTable == null)
		{
			yield return (object)null;
		}
		this.StartCoroutine(ShowInfo());
		UIntKeyTable<TutorialGearSetTable.ItemData> result = Singleton<TutorialGearSetTable>.I.ItemTable;
		equipSet = new EquipSetInfo[result.GetCount()];
		result.ForEach(delegate(TutorialGearSetTable.ItemData o)
		{
			((_003CInit_003Ec__Iterator185)/*Error near IL_00ae: stateMachine*/)._003C_003Ef__this.equipSet[o.id - 1] = new EquipSetInfo(o);
		});
		int len = equipSet.Length;
		equipSetCalc = new EquipSetCalculator[len];
		for (int i = 0; i < len; i++)
		{
			equipSetCalc[i] = new EquipSetCalculator();
			equipSetCalc[i].SetEquipSet(equipSet[i], i);
		}
		CreateLocalEquipSetData();
		isInit = true;
		RefreshUI();
		PreLoadModel();
		MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_2_name_creation, "Tutorial");
		Debug.LogWarning((object)("trackTutorialStep " + TRACK_TUTORIAL_STEP_BIT.tutorial_2_name_creation.ToString()));
		MonoBehaviourSingleton<GoWrapManager>.I.SendStatusTracking(TRACK_TUTORIAL_STEP_BIT.tutorial_2_name_creation, "Tutorial", null, null);
	}

	private void PreLoadModel()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(IEPreLoadModel());
	}

	private IEnumerator IEPreLoadModel()
	{
		while (preloader == null)
		{
			yield return (object)null;
		}
		for (int i = 0; i < equipSet.Length; i++)
		{
			EquipSetInfo equip_set = equipSet[i];
			PlayerLoadInfo playerLoadInfoMale = PlayerLoadInfo.GenerateForTutorial(0, equip_set.item[0].tableID, equip_set.item[3].tableID, equip_set.item[4].tableID, equip_set.item[5].tableID, equip_set.item[6].tableID);
			PlayerLoadInfo playerLoadInfoFemale = PlayerLoadInfo.GenerateForTutorial(1, equip_set.item[0].tableID, equip_set.item[3].tableID, equip_set.item[4].tableID, equip_set.item[5].tableID, equip_set.item[6].tableID);
			while (preloader.isLoading)
			{
				yield return (object)null;
			}
			preloader.StartLoad(playerLoadInfoMale, this.get_gameObject().get_layer(), 99, false, false, true, true, false, false, true, true, SHADER_TYPE.NORMAL, null, true, -1);
			while (preloader.isLoading)
			{
				yield return (object)null;
			}
			preloader.StartLoad(playerLoadInfoFemale, this.get_gameObject().get_layer(), 99, false, false, true, true, false, false, true, true, SHADER_TYPE.NORMAL, null, true, -1);
		}
		Object.Destroy(preloader);
		this.StartCoroutine(IEPreloadVoice());
		HideLoading();
		RefreshUI();
	}

	private IEnumerator IEPreloadVoice()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		int[] array = mvoices;
		foreach (int id in array)
		{
			loadQueue.CacheActionVoice(id, null);
		}
		int[] array2 = fvoices;
		foreach (int id2 in array2)
		{
			if (id2 == 300001 || id2 == 300002)
			{
				loadQueue.CacheVoice(id2, null);
			}
			else
			{
				loadQueue.CacheActionVoice(id2, null);
			}
		}
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
	}

	private void HideLoading()
	{
		MonoBehaviourSingleton<UIManager>.I.HideGGTutorialMessage();
		MonoBehaviourSingleton<UIManager>.I.loading.ShowTutorialBg(false);
		isHideLoading = true;
	}

	private IEnumerator ShowInfo()
	{
		yield return (object)new WaitForEndOfFrame();
		DispatchEvent("INFO", base.sectionData.GetText("STR_INFO"));
	}

	private void UpdateStr()
	{
		SetLabelText(GetCtrl(UI.BTN_NEXT), UI.LBL_NEXT, StringTable.Get(STRING_CATEGORY.COMMON, 19795u));
		SetLabelText(GetCtrl(UI.BTN_PRE), UI.LBL_PRE, StringTable.Get(STRING_CATEGORY.COMMON, 19796u));
		SetLabelText(GetCtrl(UI.BTN_MALE), UI.LBL_MALE, StringTable.Get(STRING_CATEGORY.COMMON, 19793u));
		SetLabelText(GetCtrl(UI.BTN_FEMALE), UI.LBL_FEMALE, StringTable.Get(STRING_CATEGORY.COMMON, 19794u));
		SetLabelText(GetCtrl(UI.OBJ_STATUS_UI_ROOT), UI.LBL_POPUP_NAME, StringTable.Get(STRING_CATEGORY.COMMON, 19797u));
		SetLabelText(GetCtrl(UI.BTN_SELECT), UI.LBL_SELECT, StringTable.Get(STRING_CATEGORY.COMMON, 19801u));
	}

	public unsafe override void UpdateUI()
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		if (isInit)
		{
			SetActive((Enum)UI.OBJ_STATUS_UI_ROOT, true);
			EquipSetInfo equipSetInfo = localEquipSet[localEquipSetNo];
			int i = 0;
			for (int num = 1; i < num; i++)
			{
				EquipItemInfo equipItemInfo = equipSetInfo.item[GetDataIndex(i)];
				ItemIcon itemIcon = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, sexId, GetCtrl(icons[i]), null, -1, "DETAIL", i, false, -1, false, null, false, false);
				int num2 = -1;
				string text = string.Empty;
				if (equipItemInfo != null && equipItemInfo.tableID != 0)
				{
					EquipItemTable.EquipItemData tableData = equipItemInfo.tableData;
					num2 = tableData.GetIconID(sexId);
					text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1u), equipItemInfo.level);
				}
				itemIcon.get_gameObject().SetActive(num2 != -1);
				SetEvent((Enum)iconsBtn[i], (num2 == -1) ? "EQUIP" : "DETAIL", i);
				SetLabelText((Enum)lblEquipLevel[i], text);
				SetLabelText((Enum)lblShadowEquipLevel[i], text);
				if (num2 != -1)
				{
					itemIcon.SetEquipExt(equipItemInfo, base.GetComponent<UILabel>((Enum)lblEquipLevel[i]));
				}
				Transform ctrl = GetCtrl(iconsBtn[i]);
				bool flag = equipItemInfo != null && equipItemInfo.tableID != 0;
				if (flag)
				{
					int button_event_data = i;
					SetSkillIconButton(ctrl, UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButtonTOP", equipItemInfo.tableData, GetSkillSlotData(equipSetInfo, equipItemInfo), "SKILL_ICON_BUTTON", button_event_data);
				}
				FindCtrl(ctrl, UI.OBJ_SKILL_BUTTON_ROOT).get_gameObject().SetActive(flag);
			}
			UI? nullable = tweenTarget;
			if (nullable.HasValue)
			{
				ResetTween((Enum)tweenTarget, 0);
				PlayTween((Enum)tweenTarget, true, (EventDelegate.Callback)null, false, 0);
			}
			SetActive((Enum)UI.OBJ_STUDIO_BUTTON_ROOT, showEquipMode);
			SetActive((Enum)UI.TGL_VISIBLE_UI_BUTTON, !showEquipMode);
			SetToggle((Enum)UI.TGL_SHOW_EQUIP_TYPE, showEquipMode);
			SetDynamicList((Enum)UI.GRD_DRUM, "equipno", SET_NO_MAX, false, null, null, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			UpdateStatUI(sexId, equipSetInfo);
			base.UpdateUI();
		}
	}

	private void UpdateStatUI(int sex, EquipSetInfo equip_set)
	{
		EquipItemInfo equipItemInfo = equip_set.item[0];
		playerLoadInfo = PlayerLoadInfo.GenerateForTutorial(sex, equipItemInfo.tableID, equip_set.item[3].tableID, equip_set.item[4].tableID, equip_set.item[5].tableID, equip_set.item[6].tableID);
		SetRenderPlayerModel(playerLoadInfo);
		UpdateStat();
		SetSprite((Enum)UI.SPR_SP_ATTACK_TYPE, equipItemInfo.tableData.spAttackType.GetBigFrameSpriteName());
		Transform ctrl = GetCtrl(UI.SPR_TYPE_ICON_BG);
		Transform t_icon = FindCtrl(ctrl, UI.SPR_TYPE_ICON);
		Transform val = FindCtrl(ctrl, UI.SPR_TYPE_ICON_RARITY);
		SetEquipmentTypeIcon(t_icon, ctrl, val, equipItemInfo.tableData);
		SetActive(val, false);
		SetLabelText(GetCtrl(UI.OBJ_STATUS_UI_ROOT), UI.LBL_SET_TIER, string.Format(StringTable.Get(STRING_CATEGORY.COMMON, 19792u), equip_set.tier));
		SetLabelText(GetCtrl(UI.OBJ_EQUIP_SET_NAME), UI.LBL_SET_NAME, equip_set.name);
	}

	private int GetDataIndex(int i)
	{
		if (i > 0)
		{
			return i + 2;
		}
		return 0;
	}

	protected SkillSlotUIData[] GetSkillSlotData(EquipSetInfo setInfo, EquipItemInfo equip)
	{
		if (equip == null)
		{
			return null;
		}
		int maxSlot = equip.GetMaxSlot();
		if (maxSlot == 0)
		{
			return null;
		}
		SkillSlotUIData[] array = new SkillSlotUIData[maxSlot];
		int currentEquipSetNo = GetCurrentEquipSetNo();
		SkillItemInfo[] array2 = new SkillItemInfo[1]
		{
			new SkillItemInfo(0, (int)setInfo.skillId, 1, 4)
		};
		if (array2 != null && array2.Length > maxSlot)
		{
			Log.Error("Attach Skill Num is Over Skill Slot Num");
		}
		SkillItemTable.SkillSlotData[] skillSlot = equip.tableData.GetSkillSlot(equip.exceed);
		if (equip.tableData.type == EQUIPMENT_TYPE.ONE_HAND_SWORD || equip.tableData.type == EQUIPMENT_TYPE.PAIR_SWORDS || equip.tableData.type == EQUIPMENT_TYPE.SPEAR || equip.tableData.type == EQUIPMENT_TYPE.TWO_HAND_SWORD || equip.tableData.type == EQUIPMENT_TYPE.ARROW)
		{
			array[0] = new SkillSlotUIData();
			array[0].slotData = new SkillItemTable.SkillSlotData(array2[0].tableData.id, skillSlot[0].slotType);
			array[0].itemData = array2[0];
		}
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			if (array[i] == null)
			{
				array[i] = new SkillSlotUIData();
				array[i].slotData = new SkillItemTable.SkillSlotData(0u, equip.tableData.GetSkillSlot(equip.exceed)[i].slotType);
			}
		}
		return array;
	}

	protected int GetCurrentEquipSetNo()
	{
		return MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo();
	}

	public void CreateLocalEquipSetData()
	{
		localEquipSet = new EquipSetInfo[equipSet.Length];
		localEquipSetNo = 0;
		int i = 0;
		for (int num = localEquipSet.Length; i < num; i++)
		{
			EquipSetInfo equipSetInfo = GetEquipSet(i);
			localEquipSet[i] = new EquipSetInfo(new EquipItemInfo[7]
			{
				equipSetInfo.item[0],
				equipSetInfo.item[1],
				equipSetInfo.item[2],
				equipSetInfo.item[3],
				equipSetInfo.item[4],
				equipSetInfo.item[5],
				equipSetInfo.item[6]
			}, equipSetInfo.name, equipSetInfo.tier, equipSetInfo.skillId);
		}
		int showHelm;
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && localEquipSet[localEquipSetNo].showHelm != (showHelm = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.showHelm))
		{
			int j = 0;
			for (int num2 = localEquipSet.Length; j < num2; j++)
			{
				localEquipSet[j].showHelm = showHelm;
			}
		}
	}

	protected void SetRenderPlayerModel(PlayerLoadInfo load_player_info)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		this.StopCoroutine(IERender(load_player_info));
		this.StartCoroutine(IERender(load_player_info));
	}

	private IEnumerator IERender(PlayerLoadInfo load_player_info)
	{
		while (!isHideLoading)
		{
			yield return (object)null;
		}
		loadedModel = false;
		SetRenderPlayerModel(GetCtrl(UI.OBJ_MODEL_ROOT), UI.TEX_MODEL, load_player_info, PLAYER_ANIM_TYPE.GetStatus(sexId), new Vector3(0f, -0.75f, 14f), new Vector3(0f, 180f, 0f), true, delegate(PlayerLoader player_loader)
		{
			((_003CIERender_003Ec__Iterator189)/*Error near IL_00b3: stateMachine*/)._003C_003Ef__this.loadedModel = true;
			if (player_loader != null)
			{
				((_003CIERender_003Ec__Iterator189)/*Error near IL_00b3: stateMachine*/)._003C_003Ef__this.loader = player_loader;
			}
			((_003CIERender_003Ec__Iterator189)/*Error near IL_00b3: stateMachine*/)._003C_003Ef__this.PlayVoice();
		});
	}

	private void PlayVoice()
	{
		int[] array = (sexId != 0) ? fvoices : mvoices;
		int num = (localEquipSetNo >= array.Length) ? array[0] : array[localEquipSetNo];
		if (IsActive(UI.OBJ_STATUS_UI_ROOT))
		{
			if (num == 300001 || num == 300002)
			{
				SoundManager.PlayVoice(num, 1f, 0u, null, null);
			}
			else
			{
				SoundManager.PlayActionVoice(num, 1f, 0u, null, null);
			}
		}
	}

	public EquipSetInfo GetEquipSet(int set_no)
	{
		if (set_no >= equipSet.Length)
		{
			return null;
		}
		return equipSet[set_no];
	}

	public void OnQuery_WEAPON_SELECT_NEXT()
	{
		if (loadedModel)
		{
			localEquipSetNo++;
			if (localEquipSetNo >= equipSet.Length)
			{
				localEquipSetNo = 0;
			}
			RefreshUI();
		}
	}

	public void OnQuery_WEAPON_SELECT_PRE()
	{
		if (loadedModel)
		{
			localEquipSetNo--;
			if (localEquipSetNo < 0)
			{
				localEquipSetNo = equipSet.Length - 1;
			}
			RefreshUI();
		}
	}

	public void OnQuery_WEAPON_SELECT_SELECT()
	{
		GameSection.SetEventData(sexId);
		PlayerPrefs.SetInt("Tut_Armor", (int)localEquipSet[localEquipSetNo].item[3].tableID);
		PlayerPrefs.SetInt("Tut_Arm", (int)localEquipSet[localEquipSetNo].item[5].tableID);
		PlayerPrefs.SetInt("Tut_Head", (int)localEquipSet[localEquipSetNo].item[6].tableID);
		PlayerPrefs.SetInt("Tut_Leg", (int)localEquipSet[localEquipSetNo].item[4].tableID);
		PlayerPrefs.SetInt("Tut_Weapon", (int)localEquipSet[localEquipSetNo].item[0].tableID);
		PlayerPrefs.SetInt("Tut_Sex", sexId);
		int num = playerLoadInfo.weaponModelID / 1000;
		PlayerPrefs.SetInt("Tut_Weapon_Type", num);
	}

	public void UpdateStat()
	{
		EquipSetCalculator equipSetCalculator = equipSetCalc[localEquipSetNo];
		SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, MonoBehaviourSingleton<UserInfoManager>.I.userStatus);
		SetLabelText((Enum)UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
		SetLabelText((Enum)UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
		SetLabelText((Enum)UI.LBL_HP, finalStatus.hp.ToString());
	}

	public void OnQuery_WEAPON_SELECT_MALE()
	{
		if (sexId != 0)
		{
			sexId = 0;
			RefreshUI();
		}
	}

	public void OnQuery_WEAPON_SELECT_FEMALE()
	{
		if (sexId != 1)
		{
			sexId = 1;
			RefreshUI();
		}
	}

	public override void Exit()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		base.Exit();
		if (!MonoBehaviourSingleton<LoadingProcess>.IsValid())
		{
			MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<InGameTutorialManager>();
		}
	}

	protected void OnEnable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
	}

	protected void OnDisable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
	}

	private void OnDrag(InputManager.TouchInfo touch_info)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (!(loader == null))
		{
			loader.get_transform().Rotate(GameDefine.GetCharaRotateVector(touch_info));
		}
	}
}
