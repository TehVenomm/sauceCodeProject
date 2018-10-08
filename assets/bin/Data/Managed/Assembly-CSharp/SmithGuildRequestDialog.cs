using Network;
using System;
using UnityEngine;

public class SmithGuildRequestDialog : GameSection
{
	private enum UI
	{
		IPT_POST,
		LBL_USER_NAME,
		LBL_CHAT_MESSAGE,
		OBJ_TARGET,
		OBJ_OWNER,
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

	private int needNum;

	private UIInput m_Input;

	private SortCompareData data;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		data = (array[0] as SortCompareData);
		needNum = (int)array[1];
		m_Input = GetCtrl(UI.IPT_POST).GetComponent<UIInput>();
		base.Initialize();
	}

	public override void StartSection()
	{
		m_Input.SendMessage("OnSelect", (object)true);
	}

	public override void UpdateUI()
	{
		SetInputSubmitEvent((Enum)UI.IPT_POST, new EventDelegate(delegate
		{
			OnTouchPost();
		}));
		SetActive((Enum)UI.OBJ_TARGET, true);
		SetActive((Enum)UI.OBJ_OWNER, false);
		SetInputValue((Enum)UI.IPT_POST, base.sectionData.GetText("TEXT_HELP"));
		SetInputLabel((Enum)UI.IPT_POST, base.sectionData.GetText("TEXT_HELP"));
		SetLabelText((Enum)UI.LBL_USER_NAME, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
		SetLabelText((Enum)UI.LBL_MATERIAL_NAME, data.GetName());
		SetLabelText(UI.LBL_QUATITY, data.GetNum());
		SetLabelText(UI.LBL_DONATE_NUM, data.GetNum());
		SetLabelText(UI.LBL_DONATE_MAX, needNum);
		SetButtonEnabled((Enum)UI.BTN_GIFT, false);
		SetSliderValue((Enum)UI.SLD_PROGRESS, (float)data.GetNum() / (float)needNum);
		Transform ctrl = GetCtrl(UI.OBJ_MATERIAL_ICON);
		Item item = new Item();
		item.uniqId = "0";
		item.itemId = (int)data.GetTableID();
		item.num = data.GetNum();
		ItemInfo item2 = ItemInfo.CreateItemInfo(item);
		ItemSortData itemSortData = new ItemSortData();
		itemSortData.SetItem(item2);
		SetItemIcon(ctrl, itemSortData, ctrl);
	}

	private void OnTouchPost()
	{
		SetLabelText((Enum)UI.LBL_CHAT_MESSAGE, m_Input.value);
	}

	private void OnQuery_SUBMIT()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendDonateRequest((int)data.GetTableID(), data.GetName(), GetLabelText((Enum)UI.LBL_CHAT_MESSAGE), needNum - data.GetNum(), delegate(bool success)
		{
			if (success)
			{
				RequestEvent("CHAT", GuildMessage.VIEW_TYPE.DONATE);
			}
			GameSection.ResumeEvent(success, null, false);
		});
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
