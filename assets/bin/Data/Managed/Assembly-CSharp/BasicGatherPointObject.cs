using Network;
using System.Collections.Generic;

public class BasicGatherPointObject : GatherPointObject
{
	public override void Gather()
	{
		if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
		{
			base.isGathered = true;
			UpdateView();
			MonoBehaviourSingleton<FieldManager>.I.SendFieldGather((int)base.pointData.pointID, delegate(bool b, FieldGatherRewardList list)
			{
				if (MonoBehaviourSingleton<UIDropAnnounce>.IsValid())
				{
					int i = 0;
					for (int count = list.fieldGather.accessoryItem.Count; i < count; i++)
					{
						QuestCompleteReward.AccessoryItem accessoryItem = list.fieldGather.accessoryItem[i];
						bool is_rare = false;
						MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateAccessoryItemInfo((uint)accessoryItem.accessoryId, accessoryItem.num, out is_rare));
						int se_id = 40000154;
						SoundManager.PlayOneShotUISE(se_id);
					}
					int j = 0;
					for (int count2 = list.fieldGather.skillItem.Count; j < count2; j++)
					{
						QuestCompleteReward.SkillItem skillItem = list.fieldGather.skillItem[j];
						bool is_rare2 = false;
						MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateSkillItemInfo((uint)skillItem.skillItemId, skillItem.num, out is_rare2));
						int se_id2 = 40000154;
						SoundManager.PlayOneShotUISE(se_id2);
					}
					int k = 0;
					for (int count3 = list.fieldGather.equipItem.Count; k < count3; k++)
					{
						QuestCompleteReward.EquipItem equipItem = list.fieldGather.equipItem[k];
						bool is_rare3 = false;
						MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateEquipItemInfo((uint)equipItem.equipItemId, equipItem.num, out is_rare3));
						int se_id3 = 40000154;
						SoundManager.PlayOneShotUISE(se_id3);
					}
					int l = 0;
					for (int count4 = list.fieldGather.item.Count; l < count4; l++)
					{
						QuestCompleteReward.Item item = list.fieldGather.item[l];
						bool is_rare4 = false;
						MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateItemInfo((uint)item.itemId, item.num, out is_rare4));
						int se_id4 = (!is_rare4) ? 40000153 : 40000154;
						SoundManager.PlayOneShotUISE(se_id4);
					}
				}
			});
		}
	}

	public override void CheckGather()
	{
		base.isGathered = true;
		List<int> currentFieldPointIdList = MonoBehaviourSingleton<FieldManager>.I.currentFieldPointIdList;
		if (currentFieldPointIdList != null)
		{
			int i = 0;
			for (int count = currentFieldPointIdList.Count; i < count; i++)
			{
				if (base.pointData.pointID == currentFieldPointIdList[i])
				{
					base.isGathered = false;
					break;
				}
			}
		}
		base.CheckGather();
	}

	public override void UpdateView()
	{
		base.UpdateView();
		if (gimmick != null)
		{
			gimmick.OnNotify(base.isGathered);
		}
	}
}
