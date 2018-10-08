using Network;
using System;
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

	public unsafe override void Gather()
	{
		if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
		{
			MonoBehaviourSingleton<FieldManager>.I.SendFieldGather((int)base.pointData.pointID, new Action<bool, FieldGatherRewardList>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void Update()
	{
		if (base.pointData != null && ObjectMode != 0)
		{
			localTimer += Time.get_deltaTime();
			if (ObjectMode == OBJECT_MODE.Growth)
			{
				localElapsedTime += Time.get_deltaTime();
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
						localElapsedTime = (float)currentFieldGatherGrowthList[j].elapsedTime;
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		if (gatherEffect != null)
		{
			gatherEffect.get_gameObject().SetActive(!base.isGathered);
		}
		if (modelView != null && !string.IsNullOrEmpty(base.viewData.modelHideNodeName))
		{
			Transform val = Utility.Find(modelView, base.viewData.modelHideNodeName);
			if (val != null)
			{
				if (ObjectMode != 0)
				{
					val.get_gameObject().SetActive(true);
				}
				else
				{
					val.get_gameObject().SetActive(false);
				}
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
