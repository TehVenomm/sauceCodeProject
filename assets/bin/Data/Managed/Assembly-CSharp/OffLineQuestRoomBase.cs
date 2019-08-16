using Network;
using System;
using UnityEngine;

public class OffLineQuestRoomBase : GameSection
{
	private enum UI
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
		OBJ_CHAT
	}

	protected class ResourceInfo
	{
		public RESOURCE_CATEGORY category;

		public string packageName;

		public ResourceInfo(RESOURCE_CATEGORY category, string packageName)
		{
			this.category = category;
			this.packageName = packageName;
		}
	}

	private QuestTable.QuestTableData questData;

	private Coroutine preDownloadCoroutine;

	private bool goToInGame;

	private UI[] weaponIcon = new UI[3]
	{
		UI.SPR_WEAPON_1,
		UI.SPR_WEAPON_2,
		UI.SPR_WEAPON_3
	};

	protected CharaInfo userInfo;

	private static readonly string[] ITEM_TYPE_ICON_SPRITE_NAME = new string[5]
	{
		"Sword",
		"Brade",
		"Lance",
		"Edge",
		"Arrow"
	};

	private const int ROOM_MEMBER_MAX = 4;

	protected int equipSetNo;

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateUser();
	}

	protected void UpdateUser()
	{
		SetGrid(UI.GRD_PLAYER_INFO, string.Empty, 1, reset: false, delegate(int i, Transform t)
		{
			string prefab_name = "QuestRoomUserInfoSelf";
			return Realizes(prefab_name, t, check_panel: false);
		}, delegate(int i, Transform t, bool is_recycle)
		{
			UpdateRoomUserInfo(t, i);
			SetEvent(t, UI.BTN_NAME_BG, "CHANGE_EQUIP", i);
			SetEvent(t, UI.BTN_FRAME, "CHANGE_EQUIP", i);
		});
	}

	protected virtual void UpdateRoomUserInfo(Transform trans, int index)
	{
		SetActive(trans, UI.SPR_USER_EMPTY, is_visible: false);
		SetActive(trans, UI.SPR_USER_BATTLE, is_visible: false);
		SetActive(trans, UI.SPR_USER_READY, is_visible: false);
		SetActive(trans, UI.SPR_USER_READY_WAIT, is_visible: false);
		SetActive(trans, UI.OBJ_CHAT, is_visible: false);
		SetActive(trans, UI.SPR_WEAPON_1, is_visible: false);
		SetActive(trans, UI.SPR_WEAPON_2, is_visible: false);
		SetActive(trans, UI.SPR_WEAPON_3, is_visible: false);
		QuestRoomUserInfo component = trans.GetComponent<QuestRoomUserInfo>();
		if (!(component == null))
		{
			userInfo = GetUserCharaInfo(index);
			if (userInfo == null)
			{
				component.LoadModel(index, null);
				return;
			}
			int weapon_index = 0;
			userInfo.equipSet.ForEach(delegate(CharaInfo.EquipItem data)
			{
				if (data != null)
				{
					EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)data.eId);
					if (equipItemData != null && equipItemData.IsWeapon())
					{
						SetActive(trans, weaponIcon[weapon_index], is_visible: true);
						int equipmentTypeIndex = UIBehaviour.GetEquipmentTypeIndex(equipItemData.type);
						SetSprite(trans, weaponIcon[weapon_index], ITEM_TYPE_ICON_SPRITE_NAME[equipmentTypeIndex]);
						weapon_index++;
					}
				}
			});
			component.LoadModel(index, userInfo);
			EquipSetCalculator userEquipCalculator = GetUserEquipCalculator();
			SimpleStatus finalStatus = userEquipCalculator.GetFinalStatus(0, userInfo.hp, userInfo.atk, userInfo.def);
			SetLabelText(trans, UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
			SetLabelText(trans, UI.LBL_DEF, finalStatus.GetDefencesSum().ToString());
			SetLabelText(trans, UI.LBL_HP, finalStatus.hp.ToString());
			SetLabelText(trans, UI.LBL_NAME, userInfo.name);
			SetLabelText(trans, UI.LBL_LV, userInfo.level.ToString());
		}
	}

	protected virtual CharaInfo GetUserCharaInfo(int setNo)
	{
		equipSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
		return MonoBehaviourSingleton<StatusManager>.I.GetCreatePlayerInfo().charaInfo;
	}

	protected virtual EquipSetCalculator GetUserEquipCalculator()
	{
		return MonoBehaviourSingleton<StatusManager>.I.GetEquipSetCalculator(equipSetNo);
	}

	private void ActiveAndTween(Transform root, Enum _enum, bool is_active)
	{
		SetActive(root, _enum, is_active);
		if (is_active)
		{
			ResetTween(root, _enum);
			PlayTween(root, _enum, forward: true, null, is_input_block: false);
		}
	}
}
