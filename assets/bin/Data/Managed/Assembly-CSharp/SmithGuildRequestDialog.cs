using Network;
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
		m_Input.SendMessage("OnSelect", true);
	}

	public override void UpdateUI()
	{
		SetInputSubmitEvent(UI.IPT_POST, new EventDelegate(delegate
		{
			OnTouchPost();
		}));
		SetActive(UI.OBJ_TARGET, is_visible: true);
		SetActive(UI.OBJ_OWNER, is_visible: false);
		SetInputValue(UI.IPT_POST, base.sectionData.GetText("TEXT_HELP"));
		SetInputLabel(UI.IPT_POST, base.sectionData.GetText("TEXT_HELP"));
		SetLabelText(UI.LBL_USER_NAME, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
		SetLabelText(UI.LBL_MATERIAL_NAME, data.GetName());
		SetLabelText(UI.LBL_QUATITY, data.GetNum());
		SetLabelText(UI.LBL_DONATE_NUM, data.GetNum());
		SetLabelText(UI.LBL_DONATE_MAX, needNum);
		SetButtonEnabled(UI.BTN_GIFT, is_enabled: false);
		SetSliderValue(UI.SLD_PROGRESS, (float)data.GetNum() / (float)needNum);
		Transform ctrl = GetCtrl(UI.OBJ_MATERIAL_ICON);
		ItemInfo item = ItemInfo.CreateItemInfo(new Item
		{
			uniqId = "0",
			itemId = (int)data.GetTableID(),
			num = data.GetNum()
		});
		ItemSortData itemSortData = new ItemSortData();
		itemSortData.SetItem(item);
		SetItemIcon(ctrl, itemSortData, ctrl);
	}

	private void OnTouchPost()
	{
		SetLabelText(UI.LBL_CHAT_MESSAGE, m_Input.value);
	}

	private void OnQuery_SUBMIT()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendDonateRequest((int)data.GetTableID(), data.GetName(), GetLabelText(UI.LBL_CHAT_MESSAGE), needNum - data.GetNum(), delegate(bool success)
		{
			if (success)
			{
				RequestEvent("CHAT", GuildMessage.VIEW_TYPE.DONATE);
			}
			GameSection.ResumeEvent(success);
		});
	}

	private void SetItemIcon(Transform holder, ItemSortData data, Transform parent_scroll)
	{
		ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
		RARITY_TYPE? rarity = null;
		ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
		EQUIPMENT_TYPE? magi_enable_icon_type = null;
		int icon_id = -1;
		if (data != null)
		{
			iTEM_ICON_TYPE = data.GetIconType();
			icon_id = data.GetIconID();
			rarity = data.GetRarity();
			element = data.GetIconElement();
			magi_enable_icon_type = data.GetIconMagiEnableType();
			data.GetNum();
			_ = 1;
		}
		bool is_new = false;
		switch (iTEM_ICON_TYPE)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.QUEST_ITEM:
			if (data.GetUniqID() != 0L)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, data.GetUniqID());
			}
			break;
		default:
			is_new = true;
			break;
		case ITEM_ICON_TYPE.NONE:
			break;
		}
		int enemy_icon_id = 0;
		if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
		{
			enemy_icon_id = Singleton<ItemTable>.I.GetItemData(data.GetTableID()).enemyIconID;
		}
		ItemIcon itemIcon = null;
		itemIcon = ((data.GetIconType() != ITEM_ICON_TYPE.QUEST_ITEM) ? ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, -1, "DROP", 0, is_new, -1, is_select: false, null, is_equipping: false, enemy_icon_id) : ItemIcon.Create(new ItemIcon.ItemIconCreateParam
		{
			icon_type = data.GetIconType(),
			icon_id = data.GetIconID(),
			rarity = data.GetRarity(),
			parent = holder,
			element = data.GetIconElement(),
			magi_enable_equip_type = data.GetIconMagiEnableType(),
			num = data.GetNum(),
			enemy_icon_id = enemy_icon_id,
			questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
		}));
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), parent_scroll);
	}
}
