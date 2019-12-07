using Network;
using System.Collections.Generic;
using UnityEngine;

public class GrowthGatherPointObject : GatherPointObject
{
	public class GrowthInfo
	{
		public int current;

		public int max;
	}

	public enum OBJECT_MODE
	{
		None,
		Rest,
		Growth
	}

	public OBJECT_MODE ObjectMode;

	protected float localElapsedTime;

	protected float localTimer;

	private GrowthInfo tmpGrowth = new GrowthInfo();

	public override void Initialize(FieldMapTable.GatherPointTableData point_data)
	{
		localElapsedTime = 0f;
		localTimer = 1f;
		ObjectMode = OBJECT_MODE.None;
		if (base.pointData != null)
		{
			tmpGrowth.current = 0;
			tmpGrowth.max = (int)point_data.maxNumRate;
		}
		base.Initialize(point_data);
	}

	public override void Gather()
	{
		if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
		{
			MonoBehaviourSingleton<FieldManager>.I.SendFieldGather((int)base.pointData.pointID, delegate(bool b, FieldGatherRewardList list)
			{
				if (b)
				{
					ObjectMode = OBJECT_MODE.Rest;
				}
				localElapsedTime = 0f;
				calcGrowth();
				UpdateView();
				if (MonoBehaviourSingleton<UIDropAnnounce>.IsValid())
				{
					int i = 0;
					for (int count = list.fieldGather.accessoryItem.Count; i < count; i++)
					{
						QuestCompleteReward.AccessoryItem accessoryItem = list.fieldGather.accessoryItem[i];
						bool is_rare = false;
						MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateAccessoryItemInfo((uint)accessoryItem.accessoryId, accessoryItem.num, out is_rare));
						SoundManager.PlayOneShotUISE(40000154);
					}
					int j = 0;
					for (int count2 = list.fieldGather.skillItem.Count; j < count2; j++)
					{
						QuestCompleteReward.SkillItem skillItem = list.fieldGather.skillItem[j];
						bool is_rare2 = false;
						MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateSkillItemInfo((uint)skillItem.skillItemId, skillItem.num, out is_rare2));
						SoundManager.PlayOneShotUISE(40000154);
					}
					int k = 0;
					for (int count3 = list.fieldGather.equipItem.Count; k < count3; k++)
					{
						QuestCompleteReward.EquipItem equipItem = list.fieldGather.equipItem[k];
						bool is_rare3 = false;
						MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateEquipItemInfo((uint)equipItem.equipItemId, equipItem.num, out is_rare3));
						SoundManager.PlayOneShotUISE(40000154);
					}
					int l = 0;
					for (int count4 = list.fieldGather.item.Count; l < count4; l++)
					{
						QuestCompleteReward.Item item = list.fieldGather.item[l];
						bool is_rare4 = false;
						MonoBehaviourSingleton<UIDropAnnounce>.I.Announce(UIDropAnnounce.DropAnnounceInfo.CreateItemInfo((uint)item.itemId, item.num, out is_rare4));
						SoundManager.PlayOneShotUISE(is_rare4 ? 40000154 : 40000153);
					}
				}
			});
		}
	}

	private void Update()
	{
		if (base.pointData == null || ObjectMode == OBJECT_MODE.None)
		{
			return;
		}
		localTimer += Time.deltaTime;
		if (ObjectMode == OBJECT_MODE.Growth)
		{
			localElapsedTime += Time.deltaTime;
		}
		if (localTimer > 1f)
		{
			if (calcGrowth())
			{
				UpdateView();
			}
			localTimer = 0f;
		}
	}

	public override void CheckGather()
	{
		base.isGathered = true;
		ObjectMode = OBJECT_MODE.None;
		if (base.pointData != null)
		{
			bool flag = false;
			bool flag2 = false;
			List<int> currentFieldPointIdList = MonoBehaviourSingleton<FieldManager>.I.currentFieldPointIdList;
			if (currentFieldPointIdList != null)
			{
				int i = 0;
				for (int count = currentFieldPointIdList.Count; i < count; i++)
				{
					if (base.pointData.pointID == currentFieldPointIdList[i])
					{
						flag = true;
						break;
					}
				}
			}
			List<GatherGrowthInfo> currentFieldGatherGrowthList = MonoBehaviourSingleton<FieldManager>.I.currentFieldGatherGrowthList;
			if (currentFieldGatherGrowthList != null)
			{
				for (int j = 0; j < currentFieldGatherGrowthList.Count; j++)
				{
					if (base.pointData.pointID == currentFieldGatherGrowthList[j].pId)
					{
						flag2 = true;
						localElapsedTime = currentFieldGatherGrowthList[j].elapsedTime;
						break;
					}
				}
			}
			if (flag && flag2)
			{
				ObjectMode = OBJECT_MODE.Growth;
			}
			else if (!flag && flag2)
			{
				ObjectMode = OBJECT_MODE.Rest;
			}
		}
		calcGrowth();
		base.CheckGather();
	}

	public override void UpdateView()
	{
		if (gatherEffect != null)
		{
			gatherEffect.gameObject.SetActive(!base.isGathered);
		}
		if (!(modelView != null) || string.IsNullOrEmpty(base.viewData.modelHideNodeName))
		{
			return;
		}
		Transform transform = Utility.Find(modelView, base.viewData.modelHideNodeName);
		if (transform != null)
		{
			if (ObjectMode != 0)
			{
				transform.gameObject.SetActive(value: true);
			}
			else
			{
				transform.gameObject.SetActive(value: false);
			}
		}
	}

	private bool calcGrowth()
	{
		bool result = false;
		tmpGrowth.current = 0;
		if (ObjectMode == OBJECT_MODE.Growth)
		{
			if (base.pointData.growthInterval != 0)
			{
				tmpGrowth.current = Mathf.Min((int)base.pointData.maxNumRate, Mathf.FloorToInt(localElapsedTime / (float)(double)base.pointData.growthInterval * (float)(double)base.pointData.addNumRate));
			}
			if (tmpGrowth.current < 0)
			{
				tmpGrowth.current = 0;
			}
		}
		if (tmpGrowth.current > 0)
		{
			if (base.isGathered)
			{
				result = true;
			}
			base.isGathered = false;
		}
		else
		{
			if (!base.isGathered)
			{
				result = true;
			}
			base.isGathered = true;
		}
		tmpGrowth.max = (int)base.pointData.maxNumRate;
		if (gimmick != null)
		{
			gimmick.OnNotify(tmpGrowth);
		}
		return result;
	}
}
