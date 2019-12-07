using Network;
using System;
using System.Collections;
using UnityEngine;

public class QuestSelect : GameSection
{
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
		OBJ_LEVEL_INACTIVE_L
	}

	private UI[] difficult = new UI[10]
	{
		UI.OBJ_DIFFICULT_STAR_1,
		UI.OBJ_DIFFICULT_STAR_2,
		UI.OBJ_DIFFICULT_STAR_3,
		UI.OBJ_DIFFICULT_STAR_4,
		UI.OBJ_DIFFICULT_STAR_5,
		UI.OBJ_DIFFICULT_STAR_6,
		UI.OBJ_DIFFICULT_STAR_7,
		UI.OBJ_DIFFICULT_STAR_8,
		UI.OBJ_DIFFICULT_STAR_9,
		UI.OBJ_DIFFICULT_STAR_10
	};

	protected QuestInfoData questInfo;

	private bool autoMatchEventIssue;

	private bool createOrderRoomEventIssue;

	private bool isShowDropInfo = true;

	private UI[] btnInvisibleTween = new UI[2]
	{
		UI.OBJ_BACK_BTN_ROOT,
		UI.OBJ_PARTY_BTN_ROOT
	};

	private bool loadModeRequest;

	private Transform model;

	private PlayerLoader loader;

	private bool loadComplete;

	private bool isCreateOrderRoom;

	public bool isSuccessChangeEquipSet
	{
		get;
		private set;
	}

	public void SuccessChangeEquipSet()
	{
		isSuccessChangeEquipSet = true;
	}

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		yield return _Initialize();
		InitializeBase();
	}

	protected void InitializeBase()
	{
		base.Initialize();
	}

	protected IEnumerator _Initialize()
	{
		questInfo = (GameSection.GetEventData() as QuestInfoData);
		if (questInfo.questData.tableData.questType == QUEST_TYPE.ORDER)
		{
			QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(questInfo.questData.tableData.questID);
			if (questItem != null)
			{
				GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.QUEST_ITEM, questItem.uniqueID);
			}
		}
		LoadingQueue loadingQueue = new LoadingQueue(this);
		if (questInfo != null)
		{
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questInfo.questData.tableData.GetMainEnemyID());
			if (enemyData != null)
			{
				EnemyLoader.CacheUIElementEffect(loadingQueue, enemyData.element);
			}
		}
		loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_new");
		loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_complete");
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
	}

	protected override void OnOpen()
	{
		loadComplete = false;
		InitEnemyModel();
		MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(null);
		MonoBehaviourSingleton<UIManager>.I.enableShadow = true;
	}

	protected override void OnClose()
	{
		MonoBehaviourSingleton<UIManager>.I.enableShadow = false;
	}

	public override void Close(UITransition.TYPE type)
	{
		base.Close(type);
		DeleteModel();
	}

	public override void UpdateUI()
	{
		UI[] array = new UI[3]
		{
			UI.OBJ_MISSION_INFO_1,
			UI.OBJ_MISSION_INFO_2,
			UI.OBJ_MISSION_INFO_3
		};
		UI[] array2 = new UI[3]
		{
			UI.OBJ_TOP_CROWN_1,
			UI.OBJ_TOP_CROWN_2,
			UI.OBJ_TOP_CROWN_3
		};
		UI[] array3 = new UI[3]
		{
			UI.LBL_MISSION_INFO_1,
			UI.LBL_MISSION_INFO_2,
			UI.LBL_MISSION_INFO_3
		};
		UI[] array4 = new UI[3]
		{
			UI.SPR_MISSION_INFO_CROWN_1,
			UI.SPR_MISSION_INFO_CROWN_2,
			UI.SPR_MISSION_INFO_CROWN_3
		};
		UI[] array5 = new UI[3]
		{
			UI.SPR_CROWN_1,
			UI.SPR_CROWN_2,
			UI.SPR_CROWN_3
		};
		QuestInfoData info = questInfo;
		QUEST_TYPE questType = info.questData.tableData.questType;
		int num = 0;
		SetFontStyle(UI.STR_MISSION, FontStyle.Italic);
		SetFontStyle(UI.STR_TREASURE, FontStyle.Italic);
		SetFontStyle(UI.STR_SELL, FontStyle.Italic);
		string text = null;
		switch (questType)
		{
		default:
			text = "STR_QUEST_TYPE_NORMAL";
			break;
		case QUEST_TYPE.ORDER:
			text = "STR_QUEST_TYPE_ORDER";
			break;
		case QUEST_TYPE.EVENT:
			text = "STR_QUEST_TYPE_EVENT";
			break;
		case QUEST_TYPE.STORY:
			text = "STR_QUEST_TYPE_STORY";
			break;
		}
		SetText(UI.LBL_QUEST_TYPE, text);
		SetLabelText(UI.LBL_QUEST_NUM, string.Format(base.sectionData.GetText("QUEST_NUMBER"), info.questData.tableData.locationNumber, info.questData.tableData.questNumber));
		SetLabelText(UI.LBL_QUEST_NAME, info.questData.tableData.questText);
		int num2 = (int)info.questData.tableData.limitTime;
		SetLabelText(UI.LBL_LIMIT_TIME, $"{num2 / 60:D2}:{num2 % 60:D2}");
		SetActive(UI.LBL_GUILD_REQUEST_NEED_POINT, is_visible: false);
		SetActive(UI.STR_MISSION_EMPTY, is_visible: false);
		if (!info.isExistMission)
		{
			SetActive(UI.OBJ_MISSION_INFO_ROOT, is_visible: false);
		}
		else
		{
			SetActive(UI.OBJ_MISSION_INFO_ROOT, is_visible: true);
			int i = 0;
			for (int num3 = info.missionData.Length; i < num3; i++)
			{
				SetActive(array[i], info.missionData[i] != null);
				SetActive(array2[i], info.missionData[i] != null);
				if (info.missionData[i] != null)
				{
					SetActive(array4[i], info.missionData[i].state >= CLEAR_STATUS.CLEAR);
					SetActive(array5[i], info.missionData[i].state >= CLEAR_STATUS.CLEAR);
					SetLabelText(array3[i], info.missionData[i].tableData.missionText);
				}
			}
		}
		if (questType == QUEST_TYPE.ORDER)
		{
			SetActive(UI.OBJ_SELL_ITEM, is_visible: true);
			QuestItemInfo quest_item = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(info.questData.tableData.questID);
			if (quest_item != null && quest_item.sellItems != null && quest_item.sellItems.Count > 0)
			{
				SetGrid(UI.GRD_REWARD_SELL, "", quest_item.sellItems.Count, reset: false, delegate(int i_2, Transform t_2, bool is_recycle_2)
				{
					QuestItem.SellItem sellItem = quest_item.sellItems[i_2];
					REWARD_TYPE type2 = (REWARD_TYPE)sellItem.type;
					uint itemId = (uint)sellItem.itemId;
					if (sellItem.num <= 0)
					{
						Log.Error(LOG.OUTGAME, "QuestItem sold get item num is zero. type={0},itemId={1}", type2, itemId);
					}
					else
					{
						int num4 = -1;
						ItemIcon itemIcon2 = ItemIcon.CreateRewardItemIcon(type2, itemId, t_2, num4);
						SetMaterialInfo(itemIcon2.transform, type2, itemId);
					}
				});
			}
			SetActive(UI.OBJ_TOP_CROWN_ROOT, is_visible: false);
		}
		SetActive(UI.OBJ_TREASURE, is_visible: true);
		SetGrid(UI.GRD_REWARD_QUEST, "", 5, reset: false, delegate(int i_2, Transform t_2, bool is_recycle_2)
		{
			if (info.questData.reward != null && info.questData.reward.Length > i_2)
			{
				REWARD_TYPE type = (REWARD_TYPE)info.questData.reward[i_2].type;
				uint id = (uint)info.questData.reward[i_2].id;
				ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(type, id, t_2);
				SetMaterialInfo(itemIcon.transform, type, id);
			}
		});
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)info.questData.tableData.GetMainEnemyID());
		if (enemyData != null)
		{
			int iconId = enemyData.iconId;
			RARITY_TYPE? rarity = (info.questData.tableData.questType == QUEST_TYPE.ORDER) ? new RARITY_TYPE?(info.questData.tableData.rarity) : null;
			ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, rarity, GetCtrl(UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(is_enable: false);
			ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, rarity, GetCtrl(UI.OBJ_ENEMY), enemyData.element).SetEnableCollider(is_enable: false);
		}
		SetActive(UI.SPR_ELEMENT_ROOT, is_visible: false);
		if (enemyData != null)
		{
			SetActive(UI.SPR_ELEMENT_ROOT_2, is_visible: true);
			SetElementSprite(UI.SPR_ELEMENT_2, (int)enemyData.element);
			SetActive(UI.STR_NON_ELEMENT_2, enemyData.element == ELEMENT_TYPE.MAX);
			SetElementSprite(UI.SPR_WEAK_ELEMENT_2, (int)enemyData.weakElement);
			SetActive(UI.STR_NON_WEAK_ELEMENT_2, enemyData.weakElement == ELEMENT_TYPE.MAX);
		}
		else
		{
			SetActive(UI.SPR_ELEMENT_ROOT_2, is_visible: false);
			SetActive(UI.STR_NON_WEAK_ELEMENT_2, is_visible: false);
		}
		ShowInfo(questType, isShowDropInfo);
		SetActive(UI.TWN_DIFFICULT_STAR, is_visible: false);
		num = (MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestEnemySpecies(info.questData.tableData.questID)?.questStatus ?? 1);
		SetClearStatus((CLEAR_STATUS)num);
		if (!MonoBehaviourSingleton<UserInfoManager>.I.isGuildRequestOpen)
		{
			SetActive(UI.BTN_GUILD_REQUEST, is_visible: false);
		}
	}

	protected virtual void SetClearStatus(CLEAR_STATUS clear_status)
	{
		int value = 11;
		SetToggleGroup(UI.OBJ_ICON_NEW, value);
		SetToggleGroup(UI.OBJ_ICON_CLEARED, value);
		SetToggleGroup(UI.OBJ_ICON_COMPLETE, value);
		if (clear_status != CLEAR_STATUS.NEW)
		{
			SetToggle(UI.OBJ_ICON_NEW, value: false);
			SetToggle(UI.OBJ_ICON_CLEARED, value: false);
			SetToggle(UI.OBJ_ICON_COMPLETE, value: false);
		}
		else
		{
			SetToggle(UI.OBJ_ICON_NEW, value: true);
			SetVisibleWidgetEffect(UI.SPR_ICON_NEW, "ef_ui_questselect_new");
		}
	}

	private void ShowInfo(QUEST_TYPE quest_type, bool is_show_drop_info)
	{
		if (quest_type != QUEST_TYPE.ORDER)
		{
			SetActive(UI.OBJ_TREASURE, is_show_drop_info);
			SetActive(UI.OBJ_MISSION_INFO, !is_show_drop_info);
			SetActive(UI.OBJ_SELL_ITEM, is_visible: false);
			SetActive(UI.OBJ_CHANGE_INFO_TREASURE_ROOT, isShowDropInfo);
			SetActive(UI.OBJ_CHANGE_INFO_MISSION_ROOT, !isShowDropInfo);
			SetActive(UI.OBJ_CHANGE_INFO_SELL_ROOT, is_visible: false);
		}
		else
		{
			SetActive(UI.OBJ_MISSION_INFO, is_visible: false);
			SetActive(UI.OBJ_TREASURE, is_show_drop_info);
			SetActive(UI.OBJ_SELL_ITEM, !is_show_drop_info);
			SetActive(UI.OBJ_CHANGE_INFO_TREASURE_ROOT, isShowDropInfo);
			SetActive(UI.OBJ_CHANGE_INFO_SELL_ROOT, !isShowDropInfo);
			SetActive(UI.OBJ_CHANGE_INFO_MISSION_ROOT, is_visible: false);
		}
	}

	private void Update()
	{
		if (loadModeRequest)
		{
			InitEnemyModel();
		}
		if (autoMatchEventIssue && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			autoMatchEventIssue = false;
			DispatchEvent("AUTO_MATCH");
		}
		if (createOrderRoomEventIssue && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			createOrderRoomEventIssue = false;
			DispatchEvent("CREATE_ROOM");
		}
	}

	public void InitEnemyModel()
	{
		if (!loadComplete)
		{
			if (base.state != STATE.OPEN)
			{
				loadModeRequest = true;
				return;
			}
			LoadModel();
			loadModeRequest = false;
		}
	}

	private void DeleteModel()
	{
		DeleteRenderTexture(UI.TEX_ENEMY);
		SetVisibleWidgetEffect(UI.TEX_ENEMY, null);
		if (model != null)
		{
			UnityEngine.Object.DestroyImmediate(model.gameObject);
			model = null;
			loader = null;
		}
	}

	private void LoadModel()
	{
		DeleteModel();
		QUEST_TYPE questType = questInfo.questData.tableData.questType;
		InitLoading();
		if (questType != QUEST_TYPE.STORY)
		{
			SetRenderEnemyModel(UI.TEX_ENEMY, (uint)questInfo.questData.tableData.GetMainEnemyID(), questInfo.questData.tableData.GetFoundationName(), OutGameSettingsManager.EnemyDisplayInfo.SCENE.QUEST, delegate
			{
				CompleteEnemyLoading();
			});
		}
	}

	private void InitLoading()
	{
		loadComplete = false;
		SetActive(UI.OBJ_LOADING, is_visible: true);
	}

	private void CompleteEnemyLoading()
	{
		SetActive(UI.OBJ_LOADING, is_visible: false);
		Transform renderTextureModelTransform = GetRenderTextureModelTransform(UI.TEX_ENEMY);
		if (renderTextureModelTransform != null && MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
		{
			MonoBehaviourSingleton<OutGameEffectManager>.I.ShowSilhoutteffect(renderTextureModelTransform.parent, GetRenderTextureLayer(UI.TEX_ENEMY));
		}
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questInfo.questData.tableData.GetMainEnemyID());
		if (enemyData != null && enemyData.element < ELEMENT_TYPE.MAX)
		{
			SetVisibleWidgetEffect(UI.TEX_ENEMY, EnemyLoader.GetElementEffectName(enemyData.element));
		}
		if (renderTextureModelTransform != null && MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
		{
			StartCoroutine(HideSilhoutteEffect());
		}
		GetCtrl(UI.SPR_LOAD_ROTATE_CIRCLE).localRotation = Quaternion.identity;
		loadComplete = true;
	}

	private void CompleteStoryNPCLoading(NPCTable.NPCData npc_data)
	{
		PLCA default_anim = PlayerAnimCtrl.StringToEnum(npc_data.anim);
		model.localPosition = new Vector3(0f, -1.5f, 1.5f);
		model.localEulerAngles = new Vector3(0f, 180f, 0f);
		PlayerAnimCtrl.Get(loader.animator, default_anim);
		EnableRenderTexture(UI.TEX_ENEMY);
		SetActive(UI.OBJ_LOADING, is_visible: false);
		GetCtrl(UI.SPR_LOAD_ROTATE_CIRCLE).localRotation = Quaternion.identity;
		loadComplete = true;
	}

	private IEnumerator HideSilhoutteEffect()
	{
		yield return new WaitForSeconds(0.2f);
		MonoBehaviourSingleton<OutGameEffectManager>.I.HideSilhoutteEffect();
	}

	private void OnQuery_CHANGE_INFO()
	{
		ResetTween(UI.TWN_CHANGE_BTN);
		PlayTween(UI.TWN_CHANGE_BTN, forward: true, null, is_input_block: false);
		isShowDropInfo = !isShowDropInfo;
		ShowInfo(questInfo.questData.tableData.questType, isShowDropInfo);
	}

	protected virtual void OnQuery_CREATE_ROOM()
	{
		AnimBtnInvisible(isforward: true);
		GameSection.SetEventData(new object[2]
		{
			questInfo.questData.tableData.questType,
			questInfo
		});
	}

	public void OnCloseDialog_QuestRoomSettings()
	{
		_OnCloseRoomSettings();
	}

	protected void _OnCloseRoomSettings()
	{
		AnimBtnInvisible(isforward: false);
	}

	public void OnCloseDialog_QuestStartChangeEquipSet()
	{
		_OnCloseStartChangeEquipSet();
	}

	protected void _OnCloseStartChangeEquipSet()
	{
		if (!isSuccessChangeEquipSet)
		{
			AnimBtnInvisible(isforward: false);
			return;
		}
		isSuccessChangeEquipSet = false;
		autoMatchEventIssue = true;
	}

	private void OnQuery_AUTO_MATCH()
	{
		GameSection.StayEvent();
		Action<bool, bool, bool, bool> matching_end_action = delegate(bool is_m, bool is_c, bool is_r, bool is_s)
		{
			if (!is_m)
			{
				QuestResume(is_success: false);
			}
			else if (is_s)
			{
				UIModelRenderTexture component = GetComponent<UIModelRenderTexture>(UI.TEX_ENEMY);
				if (component != null && component.enemyAnimCtrl != null)
				{
					component.enemyAnimCtrl.PlayQuestStartAnim(delegate
					{
						StartCoroutine(GoToQuest(delegate
						{
							QuestResume(is_success: true);
						}));
					});
				}
				else
				{
					StartCoroutine(GoToQuest(delegate
					{
						QuestResume(is_success: true);
					}));
				}
			}
			else if (!is_c)
			{
				GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
				QuestResume(is_success: true);
			}
			else
			{
				QuestResume(is_success: false);
			}
		};
		if (questInfo.questData.tableData.questType == QUEST_TYPE.ORDER)
		{
			int questID = (int)questInfo.questData.tableData.questID;
			PartyManager.PartySetting party_setting = new PartyManager.PartySetting(is_lock: false, 0, 0);
			MonoBehaviourSingleton<PartyManager>.I.SendCreate(questID, party_setting, delegate(bool is_success)
			{
				if (is_success)
				{
					CoopApp.EnterPartyQuest(matching_end_action);
				}
				else
				{
					QuestResume(is_success: false);
				}
			});
		}
		else
		{
			CoopApp.EnterQuest(matching_end_action);
		}
	}

	private void QuestResume(bool is_success)
	{
		if (!is_success)
		{
			AnimBtnInvisible(isforward: false);
		}
		GameSection.ResumeEvent(is_success);
	}

	private IEnumerator GoToQuest(Action onComplete)
	{
		yield return null;
		yield return new WaitForSeconds(1f);
		onComplete();
	}

	protected void OnQuery_CoopServerInvalidConfirm_YES()
	{
		GameSection.StayEvent();
		CoopApp.EnterQuestOffline(delegate(bool is_m, bool is_c, bool is_r, bool is_s)
		{
			GameSection.ResumeEvent(is_s);
		});
	}

	protected void OnQuery_CoopServerInvalidConfirm_NO()
	{
		AnimBtnInvisible(isforward: false);
	}

	private void OnQuery_QuestOrderCreateRoomConfirm_YES()
	{
		_OnQueryOrderCreateRoomConfirm_YES();
	}

	protected void _OnQueryOrderCreateRoomConfirm_YES()
	{
		isCreateOrderRoom = true;
	}

	private void OnCloseDialog_QuestOrderCreateRoomConfirm()
	{
		_OnCloseDialogOrderCreateRoomConfirm();
	}

	protected void _OnCloseDialogOrderCreateRoomConfirm()
	{
		if (isCreateOrderRoom)
		{
			createOrderRoomEventIssue = true;
		}
		else
		{
			AnimBtnInvisible(isforward: false);
		}
		isCreateOrderRoom = false;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags();
	}

	private void AnimBtnInvisible(bool isforward)
	{
		int i = 0;
		for (int num = btnInvisibleTween.Length; i < num; i++)
		{
			GetComponent<UITweener>(btnInvisibleTween[i]).Play(isforward);
		}
	}
}
