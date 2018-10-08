using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildDonateInvitationList : GameSection
{
	private enum UI
	{
		SCR_QUEST,
		TBL_QUEST,
		STR_NON_LIST,
		LBL_USER_NAME,
		LBL_CHAT_MESSAGE,
		LBL_MATERIAL_NAME,
		SLD_PROGRESS,
		OBJ_MATERIAL_ICON,
		LBL_QUATITY,
		OBJ_FULL,
		OBJ_NORMAL,
		LBL_DONATE_NUM,
		LBL_DONATE_MAX,
		BTN_GIFT
	}

	private List<DonateInvitationInfo> _donateList;

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool finish_donate_list = false;
		MonoBehaviourSingleton<GuildManager>.I.SendDonateInvitationList(delegate
		{
			((_003CDoInitialize_003Ec__Iterator36)/*Error near IL_002d: stateMachine*/)._003Cfinish_donate_list_003E__0 = true;
			((_003CDoInitialize_003Ec__Iterator36)/*Error near IL_002d: stateMachine*/)._003C_003Ef__this._donateList = MonoBehaviourSingleton<GuildManager>.I.donateInviteList;
		}, false);
		while (!finish_donate_list)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		SetActive((Enum)UI.STR_NON_LIST, _donateList.Count <= 0);
		object table_ctrl_enum = UI.TBL_QUEST;
		int count = _donateList.Count;
		if (_003C_003Ef__am_0024cache1 == null)
		{
			_003C_003Ef__am_0024cache1 = new Func<int, Transform, Transform>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		SetTable((Enum)table_ctrl_enum, "GuildDonateInvitationListItem", count, true, _003C_003Ef__am_0024cache1, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void OnQuery_RELOAD()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendDonateInvitationList(delegate
		{
			_donateList = MonoBehaviourSingleton<GuildManager>.I.donateInviteList;
			GameSection.ResumeEvent(false, null, false);
			RefreshUI();
		}, false);
	}

	private void SetItemIcon(Transform holder, ItemSortData data, Transform parent_scroll)
	{
		ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
		RARITY_TYPE? rarity = null;
		ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
		EQUIPMENT_TYPE? magi_enable_icon_type = null;
		int icon_id = -1;
		int num = -1;
		if (data != null)
		{
			iTEM_ICON_TYPE = data.GetIconType();
			icon_id = data.GetIconID();
			rarity = data.GetRarity();
			element = data.GetIconElement();
			magi_enable_icon_type = data.GetIconMagiEnableType();
			num = data.GetNum();
			if (num == 1)
			{
				num = -1;
			}
		}
		bool is_new = false;
		switch (iTEM_ICON_TYPE)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.QUEST_ITEM:
		{
			ulong uniqID = data.GetUniqID();
			if (uniqID != 0L)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, data.GetUniqID());
			}
			break;
		}
		default:
			is_new = true;
			break;
		case ITEM_ICON_TYPE.NONE:
			break;
		}
		int enemy_icon_id = 0;
		if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(data.GetTableID());
			enemy_icon_id = itemData.enemyIconID;
		}
		ItemIcon itemIcon = null;
		if (data.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
		{
			ItemIcon.ItemIconCreateParam itemIconCreateParam = new ItemIcon.ItemIconCreateParam();
			itemIconCreateParam.icon_type = data.GetIconType();
			itemIconCreateParam.icon_id = data.GetIconID();
			itemIconCreateParam.rarity = data.GetRarity();
			itemIconCreateParam.parent = holder;
			itemIconCreateParam.element = data.GetIconElement();
			itemIconCreateParam.magi_enable_equip_type = data.GetIconMagiEnableType();
			itemIconCreateParam.num = data.GetNum();
			itemIconCreateParam.enemy_icon_id = enemy_icon_id;
			itemIconCreateParam.questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST;
			itemIcon = ItemIcon.Create(itemIconCreateParam);
		}
		else
		{
			itemIcon = ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, -1, "DROP", 0, is_new, -1, false, null, false, enemy_icon_id, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		}
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), parent_scroll);
	}
}
