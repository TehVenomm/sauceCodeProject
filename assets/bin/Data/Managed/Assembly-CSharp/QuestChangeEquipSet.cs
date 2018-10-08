using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChangeEquipSet : QuestRoomUserInfoDetail
{
	protected new enum UI
	{
		LBL_NAME,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		SPR_COMMENT,
		LBL_COMMENT,
		OBJ_LAST_LOGIN,
		LBL_LAST_LOGIN,
		LBL_LAST_LOGIN_TIME,
		LBL_LEVEL,
		OBJ_LEVEL_ROOT,
		LBL_USER_ID,
		OBJ_USER_ID_ROOT,
		TEX_MODEL,
		BTN_FOLLOW,
		BTN_UNFOLLOW,
		OBJ_BLACKLIST_ROOT,
		BTN_BLACKLIST_IN,
		BTN_BLACKLIST_OUT,
		OBJ_ICON_WEAPON_1,
		OBJ_ICON_WEAPON_2,
		OBJ_ICON_WEAPON_3,
		OBJ_ICON_ARMOR,
		OBJ_ICON_HELM,
		OBJ_ICON_ARM,
		OBJ_ICON_LEG,
		BTN_ICON_WEAPON_1,
		BTN_ICON_WEAPON_2,
		BTN_ICON_WEAPON_3,
		BTN_ICON_ARMOR,
		BTN_ICON_HELM,
		BTN_ICON_ARM,
		BTN_ICON_LEG,
		OBJ_EQUIP_ROOT,
		OBJ_EQUIP_SET_ROOT,
		OBJ_FRIEND_INFO_ROOT,
		OBJ_CHANGE_EQUIP_INFO_ROOT,
		LBL_MAX,
		LBL_NOW,
		OBJ_FOLLOW_ARROW_ROOT,
		SPR_FOLLOW_ARROW,
		SPR_FOLLOWER_ARROW,
		SPR_BLACKLIST_ICON,
		LBL_LEVEL_WEAPON_1,
		LBL_LEVEL_WEAPON_2,
		LBL_LEVEL_WEAPON_3,
		LBL_LEVEL_ARMOR,
		LBL_LEVEL_HELM,
		LBL_LEVEL_ARM,
		LBL_LEVEL_LEG,
		LBL_CHANGE_MODE,
		LBL_SET_NAME,
		OBJ_DEGREE_PLATE_ROOT
	}

	protected int equipSetMax;

	protected override bool IsFriendInfo => false;

	public override void Initialize()
	{
		isChangeEquip = true;
		isVisualMode = false;
		InGameRecorder.PlayerRecord playerRecord = InitializePlayerRecord();
		object[] array = GameSection.GetEventData() as object[];
		GameSection.SetEventData(new object[3]
		{
			playerRecord,
			array[0],
			array[1]
		});
		base.Initialize();
	}

	protected InGameRecorder.PlayerRecord InitializePlayerRecord()
	{
		UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		InGameRecorder.PlayerRecord playerRecord = new InGameRecorder.PlayerRecord();
		playerRecord.id = 0;
		playerRecord.isNPC = false;
		playerRecord.isSelf = true;
		MonoBehaviourSingleton<StatusManager>.I.CreateLocalEquipSetData();
		playerRecord.playerLoadInfo = CreatePlayerLoadInfo(userStatus.eSetNo);
		playerRecord.animID = 90;
		playerRecord.charaInfo = new CharaInfo();
		playerRecord.charaInfo.userId = userInfo.id;
		playerRecord.charaInfo.name = userInfo.name;
		playerRecord.charaInfo.comment = userInfo.comment;
		playerRecord.charaInfo.code = userInfo.code;
		playerRecord.charaInfo.level = userStatus.level;
		playerRecord.charaInfo.atk = userStatus.atk;
		playerRecord.charaInfo.def = userStatus.def;
		playerRecord.charaInfo.hp = userStatus.hp;
		playerRecord.charaInfo.faceId = userStatus.faceId;
		playerRecord.charaInfo.sex = userStatus.sex;
		playerRecord.charaInfo.equipSet = null;
		MonoBehaviourSingleton<StatusManager>.I.CreateLocalVisualEquipData();
		StatusManager.LocalVisual localVisualEquip = MonoBehaviourSingleton<StatusManager>.I.GetLocalVisualEquip();
		playerRecord.charaInfo.aId = (int)((localVisualEquip.visualItem[0] != null) ? localVisualEquip.visualItem[0].tableID : 0);
		playerRecord.charaInfo.hId = (int)((localVisualEquip.visualItem[1] != null) ? localVisualEquip.visualItem[1].tableID : 0);
		playerRecord.charaInfo.rId = (int)((localVisualEquip.visualItem[2] != null) ? localVisualEquip.visualItem[2].tableID : 0);
		playerRecord.charaInfo.lId = (int)((localVisualEquip.visualItem[3] != null) ? localVisualEquip.visualItem[3].tableID : 0);
		playerRecord.charaInfo.showHelm = (localVisualEquip.isVisibleHelm ? 1 : 0);
		equipSetMax = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
		return playerRecord;
	}

	public override void UpdateUI()
	{
		UpdateEquipSetUI();
		base.UpdateUI();
	}

	protected void UpdateEquipSetUI()
	{
		MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(selfCharaEquipSetNo);
		SetLabelText(transRoot, UI.LBL_NOW, (selfCharaEquipSetNo + 1).ToString());
		SetLabelText(transRoot, UI.LBL_MAX, equipSetMax.ToString());
		SetLabelText(transRoot, UI.LBL_SET_NAME, localEquipSet.name);
	}

	protected override void OnClose()
	{
	}

	protected override void UpdateUserIDLabel()
	{
	}

	public override void SetupCommentText()
	{
	}

	public override void SetupFollowButton()
	{
	}

	protected override void CreateDegree()
	{
	}

	protected override void UpdateEquipIcon(List<CharaInfo.EquipItem> equip_set_info)
	{
		SetActive(transRoot, UI.LBL_CHANGE_MODE, isVisualMode);
		int i = 0;
		for (int num = 7; i < num; i++)
		{
			SetEvent(FindCtrl(transRoot, icons[i]), "EMPTY", 0);
			SetEvent(FindCtrl(transRoot, icons_btn[i]), "EMPTY", 0);
			SetLabelText(FindCtrl(transRoot, icons_level[i]), string.Empty);
		}
		bool flag = isVisualMode;
		bool flag2 = isVisualMode;
		bool flag3 = isVisualMode;
		bool flag4 = isVisualMode;
		if (localEquipSet != null)
		{
			int j = 0;
			for (int num2 = localEquipSet.item.Length; j < num2; j++)
			{
				ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
				RARITY_TYPE? nullable = null;
				ELEMENT_TYPE eLEMENT_TYPE = ELEMENT_TYPE.MAX;
				int num3 = -1;
				EquipItemInfo equipItemInfo = localEquipSet.item[j];
				EquipItemTable.EquipItemData equipItemData = null;
				if (equipItemInfo != null)
				{
					switch (equipItemInfo.tableData.type)
					{
					case EQUIPMENT_TYPE.ARMOR:
						flag2 = false;
						break;
					case EQUIPMENT_TYPE.HELM:
						flag = false;
						break;
					case EQUIPMENT_TYPE.ARM:
						flag3 = false;
						break;
					case EQUIPMENT_TYPE.LEG:
						flag4 = false;
						break;
					}
					equipItemData = ((!isVisualMode) ? Singleton<EquipItemTable>.I.GetEquipItemData(equipItemInfo.tableID) : GetVisualModeTargetTable(equipItemInfo.tableData.id, equipItemInfo.tableData.type, record.charaInfo));
				}
				if (isVisualMode)
				{
					if (equipItemData != null)
					{
						iTEM_ICON_TYPE = ItemIcon.GetItemIconType(equipItemData.type);
						nullable = equipItemData.rarity;
						eLEMENT_TYPE = equipItemData.GetTargetElementPriorityToTable();
						num3 = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
						SetActive(FindCtrl(transRoot, icons_level[j]), false);
					}
				}
				else if (equipItemInfo != null && equipItemInfo.tableID != 0)
				{
					num3 = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
					SetActive(FindCtrl(transRoot, icons_level[j]), true);
					string text = string.Format(StringTable.Get(STRING_CATEGORY.MAIN_STATUS, 1u), equipItemInfo.level.ToString());
					SetLabelText(FindCtrl(transRoot, icons_level[j]), text);
				}
				Transform transform = FindCtrl(transRoot, icons[j]);
				transform.GetComponentsInChildren(true, Temporary.itemIconList);
				int k = 0;
				for (int count = Temporary.itemIconList.Count; k < count; k++)
				{
					Temporary.itemIconList[k].gameObject.SetActive(true);
				}
				Temporary.itemIconList.Clear();
				ItemIcon itemIcon = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, transform, null, -1, "EQUIP", j, false, -1, false, null, false, false);
				SetLongTouch(itemIcon.transform, "DETAIL", j);
				SetEvent(FindCtrl(transRoot, icons_btn[j]), "DETAIL", j);
				itemIcon.gameObject.SetActive(num3 != -1);
				if (num3 != -1)
				{
					itemIcon.SetEquipExtInvertedColor(equipItemInfo, GetComponent<UILabel>(icons_level[j]));
				}
				UpdateEquipSkillButton(equipItemInfo, j);
			}
			ResetTween(transRoot, UI.OBJ_EQUIP_ROOT, 0);
			PlayTween(transRoot, UI.OBJ_EQUIP_ROOT, true, null, false, 0);
		}
		if (flag && record.charaInfo.hId != 0)
		{
			int index = 4;
			int hId = record.charaInfo.hId;
			EQUIPMENT_TYPE e_type = EQUIPMENT_TYPE.HELM;
			CharaInfo charaInfo = record.charaInfo;
			SetVisualModeIcon(index, hId, e_type, charaInfo);
		}
		if (flag2 && record.charaInfo.aId != 0)
		{
			int index2 = 3;
			int aId = record.charaInfo.aId;
			EQUIPMENT_TYPE e_type2 = EQUIPMENT_TYPE.ARMOR;
			CharaInfo charaInfo2 = record.charaInfo;
			SetVisualModeIcon(index2, aId, e_type2, charaInfo2);
		}
		if (flag3 && record.charaInfo.rId != 0)
		{
			int index3 = 5;
			int rId = record.charaInfo.rId;
			EQUIPMENT_TYPE e_type3 = EQUIPMENT_TYPE.ARM;
			CharaInfo charaInfo3 = record.charaInfo;
			SetVisualModeIcon(index3, rId, e_type3, charaInfo3);
		}
		if (flag4 && record.charaInfo.lId != 0)
		{
			int index4 = 6;
			int lId = record.charaInfo.lId;
			EQUIPMENT_TYPE e_type4 = EQUIPMENT_TYPE.LEG;
			CharaInfo charaInfo4 = record.charaInfo;
			SetVisualModeIcon(index4, lId, e_type4, charaInfo4);
		}
	}

	protected virtual void UpdateEquipSkillButton(EquipItemInfo item, int i)
	{
	}

	protected virtual void OnQuery_DECISION()
	{
		GameSection.ChangeEvent("[BACK]", null);
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.CheckChangeEquipSet(selfCharaEquipSetNo, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	protected override void OnQuery_SECTION_BACK()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.SetLocalEquipSetNo(-1);
		base.OnQuery_SECTION_BACK();
		MonoBehaviourSingleton<PartyManager>.I.SendIsEquip(false, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	protected void OnQuery_EQUIP_SET_L()
	{
		selfCharaEquipSetNo--;
		if (selfCharaEquipSetNo < 0)
		{
			selfCharaEquipSetNo = equipSetMax - 1;
		}
		RefreshUI();
		StartCoroutine(ReloadModelCoroutine());
	}

	protected void OnQuery_EQUIP_SET_R()
	{
		selfCharaEquipSetNo++;
		if (selfCharaEquipSetNo >= equipSetMax)
		{
			selfCharaEquipSetNo = 0;
		}
		RefreshUI();
		StartCoroutine(ReloadModelCoroutine());
	}

	protected override void OnQuery_CHANGE_MODE()
	{
		RefreshUI();
		ReloadModel();
	}

	protected IEnumerator ReloadModelCoroutine()
	{
		while (UIModelRenderTexture.Get(FindCtrl(transRoot, UI.TEX_MODEL)).IsLoadingPlayer())
		{
			yield return (object)null;
		}
		ReloadModel();
	}

	protected virtual void ReloadModel()
	{
		reloadModel = true;
		record.playerLoadInfo = CreatePlayerLoadInfo(selfCharaEquipSetNo);
		record.charaInfo.showHelm = MonoBehaviourSingleton<StatusManager>.I.GetEquippingShowHelm(selfCharaEquipSetNo);
		SetLabelText(transRoot, UI.LBL_SET_NAME, localEquipSet.name);
		LoadModel();
		reloadModel = false;
	}

	private PlayerLoadInfo CreatePlayerLoadInfo(int set_no)
	{
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetEquipSet(set_no);
		StatusManager.LocalVisual localVisual = new StatusManager.LocalVisual();
		localVisual.visualItem[0] = ((!isVisualMode) ? null : MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.armorUniqId)));
		localVisual.visualItem[1] = ((!isVisualMode) ? null : MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.helmUniqId)));
		localVisual.visualItem[2] = ((!isVisualMode) ? null : MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.armUniqId)));
		localVisual.visualItem[3] = ((!isVisualMode) ? null : MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(ulong.Parse(userStatus.legUniqId)));
		localVisual.isVisibleHelm = (MonoBehaviourSingleton<StatusManager>.I.GetEquippingShowHelm(set_no) > 0);
		playerLoadInfo.SetupLoadInfo(localEquipSet, 0uL, localVisual.VisialID(0), localVisual.VisialID(1), localVisual.VisialID(2), localVisual.VisialID(3), localVisual.isVisibleHelm);
		return playerLoadInfo;
	}
}
