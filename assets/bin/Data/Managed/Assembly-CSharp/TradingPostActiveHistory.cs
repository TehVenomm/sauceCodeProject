using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingPostActiveHistory : GameSection
{
	private enum VIEW_TYPE
	{
		ACTIVE,
		HISTORY
	}

	private enum UI
	{
		OBJ_ON_TAB_ACTIVE,
		OBJ_ON_TAB_HISTORY,
		SCR_POST,
		GRD_POST,
		PNL_MATERIAL_INFO,
		LBL_TRANSACTION,
		LBL_NAME,
		LBL_STATUS_TEXT,
		LBL_DAY,
		LBL_PRICE,
		LBL_QUATITY,
		OBJ_ICON
	}

	private VIEW_TYPE _viewType;

	private TradingPostTransactionLog loginfo;

	private TradingPostTransactionLog.ActiveLog currentLog;

	public override void Initialize()
	{
		object eventData = GameSection.GetEventData();
		if (eventData is int && (int)eventData == 1)
		{
			_viewType = VIEW_TYPE.HISTORY;
		}
		this.StartCoroutine(DoInitialize());
		base.Initialize();
	}

	private IEnumerator DoInitialize()
	{
		bool isRequestDone = false;
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestLogInfo(delegate(bool isSuccess, TradingPostTransactionLog ret)
		{
			if (isSuccess)
			{
				isRequestDone = true;
				if (loginfo != null)
				{
					loginfo = null;
				}
				loginfo = ret;
				currentLog = null;
			}
		});
		while (isRequestDone)
		{
			yield return null;
		}
	}

	public override void UpdateUI()
	{
		List<TradingPostTransactionLog.ActiveLog> showLogs = (_viewType != 0) ? loginfo.historyList : loginfo.activeList;
		SetActive((Enum)UI.OBJ_ON_TAB_ACTIVE, _viewType == VIEW_TYPE.ACTIVE);
		SetActive((Enum)UI.OBJ_ON_TAB_HISTORY, _viewType == VIEW_TYPE.HISTORY);
		SetGrid(UI.GRD_POST, "TradingPostListLogItem", showLogs.Count, reset: true, delegate(int i, Transform t, bool b)
		{
			TradingPostTransactionLog.ActiveLog activeLog = showLogs[i];
			SetLabelText(t, UI.LBL_NAME, Singleton<ItemTable>.I.GetItemData((uint)activeLog.itemId).name);
			SetLabelText(t, UI.LBL_QUATITY, activeLog.quantity);
			SetLabelText(t, UI.LBL_PRICE, activeLog.price);
			SetLabelText(t, UI.LBL_TRANSACTION, string.Format(base.sectionData.GetText("STR_TRANSACTION_ID"), activeLog.transactionId));
			string empty = string.Empty;
			if (_viewType == VIEW_TYPE.ACTIVE)
			{
				empty = TimeManager.GetRemainTimeToText(activeLog.expiredTime, 2);
				SetLabelText(t, UI.LBL_STATUS_TEXT, base.sectionData.GetText("STR_EXPIRED"));
				SetEvent(t, "REMOVE", i);
			}
			else
			{
				SetEvent(t, string.Empty, i);
				if (DateTime.TryParse(activeLog.createdAt, out DateTime result))
				{
					empty = TimeManager.GetRemainTimeToText(TimeManager.GetNow() - result, 2);
				}
				else
				{
					empty = "Can not parse time";
					Debug.LogError((object)empty);
				}
				SetLabelText(t, UI.LBL_STATUS_TEXT, base.sectionData.GetText("STR_STATUS_" + activeLog.status));
			}
			SetLabelText(t, UI.LBL_DAY, empty);
			ItemInfo item = ItemInfo.CreateItemInfo(activeLog.itemId);
			ItemSortData itemSortData = new ItemSortData();
			itemSortData.SetItem(item);
			SetItemIcon(FindCtrl(t, UI.OBJ_ICON), itemSortData);
		});
	}

	private void OnQuery_REMOVE()
	{
		int index = (int)GameSection.GetEventData();
		List<TradingPostTransactionLog.ActiveLog> list = (_viewType != 0) ? loginfo.historyList : loginfo.activeList;
		currentLog = list[index];
	}

	private void OnQuery_TradingPostRemoveLogConfirm_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestRemoveTransaction(currentLog.transactionId, delegate(bool isSuccess, Error ret)
		{
			if (isSuccess)
			{
				loginfo.activeList.Remove(currentLog);
				currentLog = null;
				RefreshUI();
			}
			GameSection.ResumeEvent(isSuccess);
		});
	}

	private void OnQuery_ACTIVE()
	{
		if (_viewType != 0)
		{
			_viewType = VIEW_TYPE.ACTIVE;
			RefreshUI();
		}
	}

	private void OnQuery_HISTORY()
	{
		if (_viewType != VIEW_TYPE.HISTORY)
		{
			_viewType = VIEW_TYPE.HISTORY;
			RefreshUI();
		}
	}

	private void SetItemIcon(Transform holder, ItemSortData data, int event_data = 0)
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
		}
		bool is_new = false;
		switch (iTEM_ICON_TYPE)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.QUEST_ITEM:
		{
			ulong uniqID = data.GetUniqID();
			if (uniqID != 0)
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
			itemIcon = ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, num, "DROP", event_data, is_new, -1, is_select: false, null, is_equipping: false, enemy_icon_id);
		}
		itemIcon.SetRewardBG(is_visible: false);
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), GetCtrl(UI.PNL_MATERIAL_INFO));
	}
}
