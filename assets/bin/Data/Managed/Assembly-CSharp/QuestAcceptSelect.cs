using Network;
using UnityEngine;

public class QuestAcceptSelect : QuestSelect
{
	private uint materialId;

	protected Transform root;

	public override void Initialize()
	{
		root = SetPrefab(base.collectUI, "QuestAcceptSelect");
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(questInfo.questData.tableData.questID);
		SetActive(UI.OBJ_REWARD_ICON_ROOT, is_visible: false);
		if (questItem == null || questItem.sellItems == null || questItem.sellItems.Count <= 0)
		{
			return;
		}
		int num = 0;
		int count = questItem.sellItems.Count;
		REWARD_TYPE type;
		uint num2;
		while (true)
		{
			if (num < count)
			{
				QuestItem.SellItem sellItem = questItem.sellItems[num];
				type = (REWARD_TYPE)sellItem.type;
				num2 = (materialId = (uint)sellItem.itemId);
				if (sellItem.num <= 0)
				{
					break;
				}
				int num3 = -1;
				SetActive(UI.OBJ_REWARD_ICON_ROOT, is_visible: true);
				ItemIcon.CreateRewardItemIcon(type, num2, FindCtrl(root, UI.OBJ_MATERIAL_ICON_ROOT), num3, "EQUIP_LIST", 0, is_new: false, -1, is_select: false, null, is_equipping: false, disable_rarity_text: true);
				num++;
				continue;
			}
			return;
		}
		Log.Error(LOG.OUTGAME, "QuestItem sold get item num is zero. type={0},itemId={1}", type, num2);
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
