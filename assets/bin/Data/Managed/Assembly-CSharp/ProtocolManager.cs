using System;

public class ProtocolManager : MonoBehaviourSingleton<ProtocolManager>
{
	private Action reserves;

	public bool isReserved => reserves != null;

	public void Reserve(Action send)
	{
		reserves = (Action)Delegate.Combine(reserves, send);
	}

	private void Update()
	{
		if (reserves != null && !AppMain.isReset)
		{
			Protocol.Resend(delegate
			{
				Action action = reserves;
				reserves = null;
				action();
			});
		}
	}

	public void OnDiff(BaseModelDiff diff)
	{
		if (Utility.IsExist(diff.user))
		{
			MonoBehaviourSingleton<UserInfoManager>.I.OnDiff(diff.user[0]);
		}
		if (Utility.IsExist(diff.unlockStamps))
		{
			MonoBehaviourSingleton<UserInfoManager>.I.UpdateUnlockStamps(diff.unlockStamps[0]);
		}
		if (Utility.IsExist(diff.unlockDegrees))
		{
			MonoBehaviourSingleton<UserInfoManager>.I.UpdateUnlockDegrees(diff.unlockDegrees[0]);
		}
		if (Utility.IsExist(diff.selectedDegree))
		{
			MonoBehaviourSingleton<UserInfoManager>.I.UpdateSelectedDegrees(diff.selectedDegree[0]);
		}
		if (Utility.IsExist(diff.status))
		{
			MonoBehaviourSingleton<UserInfoManager>.I.OnDiff(diff.status[0]);
			MonoBehaviourSingleton<PresentManager>.I.OnDiff(diff.status[0]);
			MonoBehaviourSingleton<GatherManager>.I.OnDiff(diff.status[0]);
		}
		if (Utility.IsExist(diff.equipSet))
		{
			MonoBehaviourSingleton<StatusManager>.I.OnDiff(diff.equipSet[0]);
		}
		if (Utility.IsExist(diff.item))
		{
			MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.item[0]);
		}
		if (Utility.IsExist(diff.expiredItem))
		{
			MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.expiredItem[0]);
		}
		if (Utility.IsExist(diff.equipItem))
		{
			MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.equipItem[0]);
		}
		if (Utility.IsExist(diff.skillItem))
		{
			MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.skillItem[0]);
		}
		if (Utility.IsExist(diff.abilityItem))
		{
			MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.abilityItem[0]);
		}
		if (Utility.IsExist(diff.skillItemEquipSlot))
		{
			MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.skillItemEquipSlot[0]);
		}
		if (Utility.IsExist(diff.questItem))
		{
			MonoBehaviourSingleton<InventoryManager>.I.OnDiff(diff.questItem[0]);
		}
		if (Utility.IsExist(diff.clearStatusQuest))
		{
			MonoBehaviourSingleton<QuestManager>.I.OnDiff(diff.clearStatusQuest[0]);
		}
		if (Utility.IsExist(diff.clearStatusDelivery))
		{
			MonoBehaviourSingleton<DeliveryManager>.I.OnDiff(diff.clearStatusDelivery[0]);
		}
		if (Utility.IsExist(diff.clearStatusQuestEnemySpecies))
		{
			MonoBehaviourSingleton<QuestManager>.I.OnDiff(diff.clearStatusQuestEnemySpecies[0]);
		}
		if (Utility.IsExist(diff.delivery))
		{
			MonoBehaviourSingleton<DeliveryManager>.I.OnDiff(diff.delivery[0]);
		}
		if (Utility.IsExist(diff.gatherPoint))
		{
			MonoBehaviourSingleton<GatherManager>.I.OnDiff(diff.gatherPoint[0]);
		}
		if (Utility.IsExist(diff.blacklist))
		{
			MonoBehaviourSingleton<BlackListManager>.I.OnDiff(diff.blacklist[0]);
		}
		if (Utility.IsExist(diff.traveled))
		{
			MonoBehaviourSingleton<WorldMapManager>.I.OnDiff(diff.traveled[0]);
		}
		if (Utility.IsExist(diff.portal))
		{
			MonoBehaviourSingleton<WorldMapManager>.I.OnDiff(diff.portal[0]);
			MonoBehaviourSingleton<FieldManager>.I.OnDiff(diff.portal[0]);
		}
		if (Utility.IsExist(diff.friend))
		{
			MonoBehaviourSingleton<FriendManager>.I.OnDiff(diff.friend[0]);
		}
		if (Utility.IsExist(diff.message))
		{
			MonoBehaviourSingleton<FriendManager>.I.OnDiff(diff.message[0]);
		}
		if (Utility.IsExist(diff.boost))
		{
			MonoBehaviourSingleton<StatusManager>.I.OnDiff(diff.boost[0]);
		}
		if (Utility.IsExist(diff.notice))
		{
			MonoBehaviourSingleton<UserInfoManager>.I.OnDiff(diff.notice[0]);
		}
		if (Utility.IsExist(diff.fieldGather))
		{
			MonoBehaviourSingleton<FieldManager>.I.OnDiff(diff.fieldGather[0]);
		}
		if (Utility.IsExist(diff.achievement))
		{
			MonoBehaviourSingleton<AchievementManager>.I.OnDiff(diff.achievement[0]);
		}
		if (Utility.IsExist(diff.task))
		{
			MonoBehaviourSingleton<AchievementManager>.I.OnDiff(diff.task[0]);
		}
		if (Utility.IsExist(diff.equipCollection))
		{
			MonoBehaviourSingleton<AchievementManager>.I.OnDiff(diff.equipCollection[0]);
		}
		if (Utility.IsExist(diff.constDefine))
		{
			MonoBehaviourSingleton<UserInfoManager>.I.OnDiff(diff.constDefine[0]);
		}
		if (Utility.IsExist(diff.visual))
		{
			MonoBehaviourSingleton<GlobalSettingsManager>.I.OnDiff(diff.visual[0]);
		}
		if (Utility.IsExist(diff.userGuildRequest))
		{
			MonoBehaviourSingleton<GuildRequestManager>.I.OnDiff(diff.userGuildRequest[0]);
		}
	}
}
