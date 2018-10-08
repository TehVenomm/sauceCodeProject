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
		SetLabelText((Enum)UI.LBL_NUMBER_REQUEST, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 34u), MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateCap, MonoBehaviourSingleton<GuildManager>.I.guildInfos.donateMaxCap));
		SetSupportEncoding(UI.LBL_NUMBER_REQUEST, true);
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		object grid_ctrl_enum = UI.GRD_INVENTORY;
		int count = itemList.Count;
		if (_003C_003Ef__am_0024cache5 == null)
		{
			_003C_003Ef__am_0024cache5 = new Func<int, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		SetDynamicList((Enum)grid_ctrl_enum, "GuildDonateMaterialItem", count, false, _003C_003Ef__am_0024cache5, null, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		string s = GameSection.GetEventData() as string;
		try
		{
			int num = int.Parse(s);
			if (num > 0 && chooseIndex >= 0)
			{
				this.StartCoroutine(CRSendDonateRequest((int)itemList[chooseIndex].tableData.id, itemList[chooseIndex].tableData.name, string.Empty, num));
				chooseIndex = -1;
			}
		}
		catch
		{
		}
		chooseIndex = -1;
	}

	private unsafe IEnumerator CRSendDonateRequest(int itemID, string itemName, string request, int numRequest)
	{
		if (_003CCRSendDonateRequest_003Ec__Iterator5D._003C_003Ef__am_0024cacheB == null)
		{
			_003CCRSendDonateRequest_003Ec__Iterator5D._003C_003Ef__am_0024cacheB = new Func<bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		yield return (object)new WaitUntil(_003CCRSendDonateRequest_003Ec__Iterator5D._003C_003Ef__am_0024cacheB);
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendDonateRequest(itemID, itemName, request, numRequest, delegate(bool success)
		{
			GameSection.ResumeEvent(success, null, false);
			if (success)
			{
				((_003CCRSendDonateRequest_003Ec__Iterator5D)/*Error near IL_0077: stateMachine*/)._003C_003Ef__this.backSection = true;
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
