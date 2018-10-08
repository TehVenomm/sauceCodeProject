using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildDonateMaterialSelectDialog : GameSection
{
	private enum UI
	{
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		TGL_CHANGE_INVENTORY,
		TGL_ICON_ASC,
		LBL_SORT,
		BTN_SORT,
		SPR_INVALID_SORT,
		LBL_INVALID_SORT,
		OBJ_CAPTION_3,
		LBL_CAPTION,
		SPR_RARITY_TEXT_ICON,
		LBL_NAME,
		lbl_item_num,
		ICON,
		SPR_RARITY,
		icon_bg,
		LBL_NUMBER_REQUEST
	}

	private int[] materialList = new int[46]
	{
		1000007,
		1000008,
		1000009,
		1000010,
		1000011,
		1000012,
		1000013,
		1000014,
		1000015,
		1000004,
		2010002,
		2010003,
		2010004,
		2010005,
		2010006,
		2010102,
		2010103,
		2010104,
		2010105,
		2010106,
		2010202,
		2010203,
		2010204,
		2010205,
		2010206,
		2010302,
		2010303,
		2010304,
		2010305,
		2010306,
		2010402,
		2010403,
		2010404,
		2010405,
		2010406,
		2010502,
		2010503,
		2010504,
		2010505,
		2010506,
		2010000,
		2010100,
		2010200,
		2010300,
		2010400,
		2010500
	};

	private List<ItemInfo> itemList = new List<ItemInfo>();

	private ItemStorageTop.MaterialInventory inventory;

	private int chooseIndex = -1;

	private bool backSection;

	public override void Initialize()
	{
		inventory = new ItemStorageTop.MaterialInventory(true, false, false, null);
		itemList.Clear();
		Array.ForEach(materialList, delegate(int id)
		{
			GuildDonateMaterialSelectDialog guildDonateMaterialSelectDialog = this;
			ItemInfo itemInfo = new ItemInfo();
			itemInfo.tableID = (uint)id;
			itemInfo.tableData = Singleton<ItemTable>.I.GetItemData(itemInfo.tableID);
			itemInfo.num = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == id, 1, false);
			itemList.Add(itemInfo);
		});
		SetLabelText(UI.LBL_NUMBER_REQUEST, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 34u), MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateCap, MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap));
		SetSupportEncoding(UI.LBL_NUMBER_REQUEST, true);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetDynamicList(UI.GRD_INVENTORY, "GuildDonateMaterialItem", itemList.Count, false, (int i) => true, null, delegate(int i, Transform t, bool is_recycre)
		{
			GuildDonateMaterialSelectDialog guildDonateMaterialSelectDialog = this;
			SetSprite(t, UI.SPR_RARITY_TEXT_ICON, ItemIcon.ITEM_ICON_ITEM_RARITY_ICON_SPRITE[(int)itemList[i].tableData.rarity]);
			SetSprite(t, UI.SPR_RARITY, ItemIcon.ITEM_ICON_EQUIP_RARITY_FRAME_SPRITE[(int)itemList[i].tableData.rarity]);
			SetLabelText(t, UI.LBL_NAME, itemList[i].tableData.name);
			SetLabelText(t, UI.lbl_item_num, itemList[i].GetNum().ToString());
			ResourceLoad.LoadIconTexture(this, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(itemList[i].tableData.iconID), null, delegate(Texture tex)
			{
				guildDonateMaterialSelectDialog.SetTexture(t, UI.ICON, tex);
			});
			int iconBGID = ItemIcon.GetIconBGID(ITEM_ICON_TYPE.ITEM, itemList[i].tableData.iconID, itemList[i].tableData.rarity);
			ResourceLoad.LoadIconTexture(this, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(iconBGID), null, delegate(Texture tex)
			{
				guildDonateMaterialSelectDialog.SetTexture(t, UI.icon_bg, tex);
			});
			SetEvent(t, "CHOSE_MATERIAL", i);
		});
	}

	private void OnQuery_CHOSE_MATERIAL()
	{
		chooseIndex = (int)GameSection.GetEventData();
		GameSection.ChangeEvent("OPEN_SEND_DIALOG", null);
	}

	private void OnQuery_ARMORY()
	{
		GameSection.SetEventData(new object[2]
		{
			SmithEquipBase.SmithType.GROW,
			EQUIPMENT_TYPE.ONE_HAND_SWORD
		});
	}

	private void OnCloseDialog_GuildDonateSendDialog()
	{
		string s = GameSection.GetEventData() as string;
		try
		{
			int num = int.Parse(s);
			if (num > 0 && chooseIndex >= 0)
			{
				StartCoroutine(CRSendDonateRequest((int)itemList[chooseIndex].tableData.id, itemList[chooseIndex].tableData.name, string.Empty, num));
				chooseIndex = -1;
			}
		}
		catch
		{
		}
		chooseIndex = -1;
	}

	private IEnumerator CRSendDonateRequest(int itemID, string itemName, string request, int numRequest)
	{
		yield return (object)new WaitUntil(() => !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible());
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendDonateRequest(itemID, itemName, request, numRequest, delegate(bool success)
		{
			GameSection.ResumeEvent(success, null);
			if (success)
			{
				((_003CCRSendDonateRequest_003Ec__Iterator55)/*Error near IL_0077: stateMachine*/)._003C_003Ef__this.backSection = true;
			}
		});
	}

	private void Update()
	{
		if (backSection && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			backSection = false;
			GameSection.BackSection();
		}
	}
}
