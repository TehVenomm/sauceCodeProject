using Network;
using System;
using UnityEngine;

public class QuestAcceptSelect : QuestSelect
{
	private uint materialId;

	protected Transform root;

	public override void Initialize()
	{
		root = SetPrefab(base.collectUI, "QuestAcceptSelect", true);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(questInfo.questData.tableData.questID);
		SetActive((Enum)UI.OBJ_REWARD_ICON_ROOT, false);
		if (questItem != null && questItem.sellItems != null && questItem.sellItems.Count > 0)
		{
			int num = 0;
			int count = questItem.sellItems.Count;
			REWARD_TYPE type;
			uint num2;
			while (true)
			{
				if (num >= count)
				{
					return;
				}
				QuestItem.SellItem sellItem = questItem.sellItems[num];
				type = (REWARD_TYPE)sellItem.type;
				num2 = (materialId = (uint)sellItem.itemId);
				if (sellItem.num <= 0)
				{
					break;
				}
				int num3 = -1;
				SetActive((Enum)UI.OBJ_REWARD_ICON_ROOT, true);
				ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(type, num2, FindCtrl(root, UI.OBJ_MATERIAL_ICON_ROOT), num3, "EQUIP_LIST", 0, false, -1, false, null, false, true, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
				num++;
			}
			Log.Error(LOG.OUTGAME, "QuestItem sold get item num is zero. type={0},itemId={1}", type, num2);
		}
	}

	public void OnCloseDialog_QuestAcceptRoomSettings()
	{
		_OnCloseRoomSettings();
	}

	public void OnCloseDialog_QuestAcceptStartChangeEquipSet()
	{
		_OnCloseStartChangeEquipSet();
	}

	protected virtual void OnQuery_GUILD_REQUEST()
	{
		GameSection.SetEventData(questInfo);
	}

	private void OnQuery_EQUIP_LIST()
	{
		if (materialId == 0)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[1]
			{
				materialId
			});
		}
	}

	private void OnQuery_QuestAcceptOrderCreateRoomConfirm_YES()
	{
		_OnQueryOrderCreateRoomConfirm_YES();
	}

	private void OnCloseDialog_QuestAcceptOrderCreateRoomConfirm()
	{
		_OnCloseDialogOrderCreateRoomConfirm();
	}
}
