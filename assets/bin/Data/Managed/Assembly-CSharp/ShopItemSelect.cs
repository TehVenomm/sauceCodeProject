using Network;
using UnityEngine;

public class ShopItemSelect : GameSection
{
	protected enum UI
	{
		OBJ_FRAME,
		SCR_LIST,
		TBL_LIST,
		LBL_NAME,
		LBL_DESCRIPTION,
		LBL_CRYSTAL_NUM,
		OBJ_ICON_ROOT
	}

	protected object[] selectEventData;

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetTable(UI.TBL_LIST, "ShopItemListItem", MonoBehaviourSingleton<ShopManager>.I.shopData.lineups.Count, false, delegate(int i, Transform t, bool b)
		{
			ShopList.ShopLineup shopLineup = MonoBehaviourSingleton<ShopManager>.I.shopData.lineups[i];
			SetLabelText(t, UI.LBL_NAME, shopLineup.name);
			SetLabelText(t, UI.LBL_DESCRIPTION, shopLineup.description);
			SetLabelText(t, UI.LBL_CRYSTAL_NUM, shopLineup.crystalNum.ToString());
			SetEvent(t, "SELECT", shopLineup.shopLineupId);
			uint itemId = (uint)shopLineup.itemIds[0];
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.ITEM, itemId, FindCtrl(t, UI.OBJ_ICON_ROOT), -1, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
			if ((Object)itemIcon != (Object)null)
			{
				itemIcon.SetEnableCollider(false);
			}
		});
	}

	private void OnQuery_SELECT()
	{
		int num = (int)GameSection.GetEventData();
		ShopList.ShopLineup lineup = MonoBehaviourSingleton<ShopManager>.I.GetLineup(num);
		if (lineup == null)
		{
			Log.Error(LOG.OUTGAME, "lineup_id=" + num + " is not found.");
			GameSection.StopEvent();
		}
		else
		{
			selectEventData = new object[7]
			{
				num,
				lineup,
				lineup.name,
				lineup.description,
				lineup.crystalNum,
				MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal,
				MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal - lineup.crystalNum
			};
			GameSection.SetEventData(selectEventData);
		}
	}

	protected void OnQuery_ShopItemConfirm_YES()
	{
		GameSection.SetEventData(selectEventData);
		GameSection.StayEvent();
		MonoBehaviourSingleton<ShopManager>.I.SendBuy((int)selectEventData[0], delegate(Error error)
		{
			switch (error)
			{
			case Error.None:
				selectEventData[6] = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
				GameSection.ResumeEvent(true, null);
				break;
			case Error.ERR_CRYSTAL_NOT_ENOUGH:
				GameSection.ChangeStayEvent("NOT_ENOUGTH", null);
				GameSection.ResumeEvent(true, null);
				break;
			default:
				GameSection.ResumeEvent(false, null);
				break;
			}
		});
	}
}
