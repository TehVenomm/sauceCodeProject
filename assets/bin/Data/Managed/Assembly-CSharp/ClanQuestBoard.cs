using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanQuestBoard : GameSection
{
	private enum UI
	{
		LBL_TITLE,
		LBL_TITLE_SHADOW,
		SCR_QUEST,
		GRD_QUEST,
		STR_NON_LIST,
		LBL_QUEST_NAME,
		LBL_QUEST_NUM,
		OBJ_ENEMY,
		SPR_ELEMENT_ROOT,
		SPR_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		SPR_CONDITION_DIFFICULTY,
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
		LBL_HOST_NAME,
		LBL_HOST_LV,
		TGL_MEMBER_1,
		TGL_MEMBER_2,
		TGL_MEMBER_3,
		STR_NO_CONDITION,
		SPR_WINDOW_BASE,
		SPR_ICON_DOUBLE,
		OBJ_SEARCH_INFO_ROOT,
		LBL_LV,
		TEX_NPCMODEL,
		LBL_MESSAGE,
		SPR_ICON_DEFENSE_BATTLE,
		LBL_RECRUTING_MEMBERS
	}

	private UI[] memberUI = new UI[3]
	{
		UI.TGL_MEMBER_1,
		UI.TGL_MEMBER_2,
		UI.TGL_MEMBER_3
	};

	private List<PartyModel.Party> parties;

	public override void Initialize()
	{
		SetLabelText((Enum)UI.LBL_TITLE, base.sectionData.GetText("TITLE"));
		SetLabelText((Enum)UI.LBL_TITLE_SHADOW, base.sectionData.GetText("TITLE"));
		SetLabelText((Enum)UI.STR_NON_LIST, base.sectionData.GetText("NON_QUEST"));
		SetActive((Enum)UI.SCR_QUEST, is_visible: true);
		this.StartCoroutine(DoInitialize());
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.SPR_CONDITION_DIFFICULTY, is_visible: false);
		SetActive((Enum)UI.STR_NO_CONDITION, is_visible: true);
		SetNpcInfo();
		if (parties == null)
		{
			SetActive((Enum)UI.GRD_QUEST, is_visible: false);
			SetActive((Enum)UI.STR_NON_LIST, is_visible: true);
		}
		else if (parties.Count <= 0)
		{
			SetActive((Enum)UI.GRD_QUEST, is_visible: false);
			SetActive((Enum)UI.STR_NON_LIST, is_visible: true);
		}
		else
		{
			SetActive((Enum)UI.GRD_QUEST, is_visible: true);
			SetActive((Enum)UI.STR_NON_LIST, is_visible: false);
			SetGrid(UI.GRD_QUEST, "QuestSearchListSelectItem", parties.Count, reset: false, delegate(int i, Transform t, bool is_recycle)
			{
				QuestTable.QuestTableData questTableData = null;
				questTableData = ((parties[i].quest.explore == null) ? Singleton<QuestTable>.I.GetQuestData((uint)parties[i].quest.questId) : Singleton<QuestTable>.I.GetQuestData((uint)parties[i].quest.explore.mainQuestId));
				if (questTableData == null)
				{
					SetActive(t, is_visible: false);
				}
				else
				{
					SetEvent(t, "SELECT_ROOM", i);
					SetQuestData(questTableData, t);
					SetPartyData(parties[i], t, questTableData.questType);
					SetStatusIconInfo(parties[i], t);
					SetMemberIcon(t, questTableData);
				}
			});
		}
	}

	protected void SetStatusIconInfo(PartyModel.Party _partyParam, Transform _targetObject)
	{
		if (!(_targetObject == null) && _partyParam != null)
		{
			QuestUserStatusIconController componentInChildren = _targetObject.GetComponentInChildren<QuestUserStatusIconController>();
			if (componentInChildren != null)
			{
				componentInChildren.Initialize(new QuestUserStatusIconController.InitParam
				{
					StatusBit = (uint)_partyParam.iconBit
				});
			}
		}
	}

	private IEnumerator DoInitialize()
	{
		yield return this.StartCoroutine(Reload());
	}

	private IEnumerator Reload(Action<bool> cb = null)
	{
		bool isRecv = false;
		MonoBehaviourSingleton<ClanMatchingManager>.I.SendRoomParty(delegate(bool isSuccess, List<PartyModel.Party> parties)
		{
			if (isSuccess)
			{
				this.parties = parties;
				isRecv = true;
			}
			if (cb != null)
			{
				cb(isSuccess);
			}
		});
		isRecv = true;
		while (!isRecv)
		{
			yield return null;
		}
		SetDirty(UI.GRD_QUEST);
		RefreshUI();
		base.Initialize();
	}

	private void SetPartyData(PartyModel.Party party, Transform t, QUEST_TYPE type)
	{
		int member_num = 0;
		party.slotInfos.ForEach(delegate(PartyModel.SlotInfo data)
		{
			if (data != null && data.userInfo != null)
			{
				if (data.userInfo.userId == party.ownerUserId)
				{
					SetLabelText(t, UI.LBL_HOST_NAME, data.userInfo.name);
					SetLabelText(t, UI.LBL_HOST_LV, data.userInfo.level.ToString());
				}
				else
				{
					member_num++;
				}
			}
		});
		for (int i = 0; i < 3; i++)
		{
			SetToggle(t, memberUI[i], i < member_num);
		}
		SetLabelText(t, UI.LBL_LV, base.sectionData.GetText("LV"));
		if (type == QUEST_TYPE.GATE || type == QUEST_TYPE.DEFENSE)
		{
			if (type == QUEST_TYPE.GATE)
			{
				SetLabelText(t, UI.LBL_HOST_NAME, base.sectionData.GetText("GATE_QUEST_MESSAGE"));
			}
			else
			{
				SetLabelText(t, UI.LBL_HOST_NAME, string.Empty);
			}
			SetLabelText(t, UI.LBL_HOST_LV, string.Empty);
			SetLabelText(t, UI.LBL_LV, string.Empty);
		}
	}

	private void SetQuestData(QuestTable.QuestTableData questData, Transform t)
	{
		UI[] array = new UI[10]
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
		int num = (int)(questData.difficulty + 1);
		int i = 0;
		for (int num2 = array.Length; i < num2; i++)
		{
			SetActive(t, array[i], i < num);
		}
		ResetTween(t, UI.TWN_DIFFICULT_STAR);
		PlayTween(t, UI.TWN_DIFFICULT_STAR, forward: true, null, is_input_block: false);
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
		if (enemyData != null)
		{
			SetActive(t, UI.OBJ_ENEMY, is_visible: true);
			int iconId = enemyData.iconId;
			RARITY_TYPE? rarity = (questData.questType != QUEST_TYPE.ORDER) ? null : new RARITY_TYPE?(questData.rarity);
			ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, rarity, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element);
			itemIcon.SetEnableCollider(is_enable: false);
			SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
			SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
		}
		else
		{
			SetActive(t, UI.OBJ_ENEMY, is_visible: false);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, 6);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, is_visible: true);
		}
		Transform val = FindCtrl(t, UI.SPR_ICON_DOUBLE);
		Transform val2 = FindCtrl(t, UI.SPR_ICON_DEFENSE_BATTLE);
		Transform val3 = FindCtrl(t, UI.LBL_RECRUTING_MEMBERS);
		Transform val4 = FindCtrl(t, UI.SPR_WINDOW_BASE);
		if (val4 != null)
		{
			UISprite component = val4.GetComponent<UISprite>();
			Transform val5 = FindCtrl(t, UI.OBJ_SEARCH_INFO_ROOT);
			UISprite component2 = val5.GetComponent<UISprite>();
			if (questData.questType == QUEST_TYPE.GATE || questData.questType == QUEST_TYPE.DEFENSE)
			{
				component.spriteName = "QuestListPlateO";
				component2.spriteName = "SearchAdWindowO";
				val.get_gameObject().SetActive(true);
				val2.get_gameObject().SetActive(questData.questType == QUEST_TYPE.DEFENSE);
				val3.get_gameObject().SetActive(questData.questType == QUEST_TYPE.DEFENSE);
				string format = StringTable.Get(STRING_CATEGORY.GATE_QUEST_NAME, 0u);
				string text = string.Empty;
				if (enemyData != null)
				{
					text = string.Format(format, questData.GetMainEnemyLv(), enemyData.name);
				}
				SetLabelText(t, UI.LBL_QUEST_NAME, text);
				SetLabelText(t, UI.LBL_QUEST_NUM, string.Empty);
			}
			else
			{
				component.spriteName = "QuestListPlateN";
				component2.spriteName = "SearchAdWindow";
				val.get_gameObject().SetActive(false);
				val2.get_gameObject().SetActive(false);
				val3.get_gameObject().SetActive(false);
				SetLabelText(t, UI.LBL_QUEST_NAME, questData.questText);
				SetLabelText(t, UI.LBL_QUEST_NUM, string.Format(base.sectionData.GetText("QUEST_NUMBER"), questData.locationNumber, questData.questNumber));
			}
		}
		else
		{
			SetLabelText(t, UI.LBL_QUEST_NAME, questData.questText);
			SetLabelText(t, UI.LBL_QUEST_NUM, string.Format(base.sectionData.GetText("QUEST_NUMBER"), questData.locationNumber, questData.questNumber));
		}
	}

	private void SetNpcInfo()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		string text = (parties.Count <= 0) ? base.sectionData.GetText("NON_LIST_MSG") : base.sectionData.GetText("EXIST_LIST_MSG");
		SetRenderNPCModel((Enum)UI.TEX_NPCMODEL, 7, MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.boardCenterNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.boardCenterNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.boardCenterNPCFOV, (Action<NPCLoader>)null);
		SetLabelText((Enum)UI.LBL_MESSAGE, text);
	}

	private void OnQuery_SELECT_ROOM()
	{
		int index = (int)GameSection.GetEventData();
		if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog((uint)MonoBehaviourSingleton<ClanMatchingManager>.I.clanRoomParties[index].quest.questId))
		{
			GameSection.StopEvent();
			return;
		}
		GameSection.SetEventData(new object[1]
		{
			false
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendEntry(MonoBehaviourSingleton<ClanMatchingManager>.I.clanRoomParties[index].id, isLoungeBoard: true, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	private void OnQuery_RELOAD()
	{
		GameSection.StayEvent();
		this.StartCoroutine(Reload(delegate(bool b)
		{
			GameSection.ResumeEvent(b);
		}));
	}

	private void OnQuery_MEMBER()
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.I.partyData == null)
		{
			GameSection.ChangeEvent("ERROR");
		}
	}

	protected void SetMemberIcon(Transform t, QuestTable.QuestTableData table)
	{
		if (table != null)
		{
			SetActive(t, UI.TGL_MEMBER_3, is_visible: true);
			SetActive(t, UI.TGL_MEMBER_2, is_visible: true);
			SetActive(t, UI.TGL_MEMBER_1, is_visible: true);
			if (table.userNumLimit < 4)
			{
				SetActive(t, UI.TGL_MEMBER_3, is_visible: false);
			}
			if (table.userNumLimit < 3)
			{
				SetActive(t, UI.TGL_MEMBER_2, is_visible: false);
			}
			if (table.userNumLimit < 2)
			{
				SetActive(t, UI.TGL_MEMBER_1, is_visible: false);
			}
		}
	}
}
