using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusBoostAnimator : MonoBehaviour
{
	private List<BoostStatus> enableBoostList = new List<BoostStatus>();

	private List<BoostStatus> deleteBoostList = new List<BoostStatus>();

	private USE_ITEM_EFFECT_TYPE boostDispType;

	private int time;

	private int updataCnt;

	private int[] animTable;

	private int animIndex;

	private Action<BoostStatus> updateCallback;

	private Action<BoostStatus> changeCallback;

	private const int ANIM_TIME = 3;

	private static readonly Color[] TEXT_RATE_COLOR = new Color[5]
	{
		Color.white,
		new Color32(0, byte.MaxValue, 244, byte.MaxValue),
		new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue),
		new Color32(251, 210, 0, byte.MaxValue),
		new Color32(225, 23, 23, byte.MaxValue)
	};

	public void SetupUI(Action<BoostStatus> update_callback, Action<BoostStatus> change_callback)
	{
		enableBoostList.Clear();
		BoostStatus boostStatus = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.EXP_UP);
		if (boostStatus != null)
		{
			enableBoostList.Add(boostStatus);
		}
		BoostStatus boostStatus2 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.MONEY_UP);
		if (boostStatus2 != null)
		{
			enableBoostList.Add(boostStatus2);
		}
		BoostStatus boostStatus3 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.DROP_UP);
		if (boostStatus3 != null)
		{
			enableBoostList.Add(boostStatus3);
		}
		BoostStatus boostStatus4 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.EVENT_POINT_UP);
		if (boostStatus4 != null)
		{
			if (!MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && !MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
			{
				enableBoostList.Add(boostStatus4);
			}
			else if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && MonoBehaviourSingleton<UIPlayerStatus>.I.PermitHGPBoostUpdate)
			{
				enableBoostList.Add(boostStatus4);
			}
			else if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid() && MonoBehaviourSingleton<UIEnduranceStatus>.I.PermitHGPBoostUpdate)
			{
				enableBoostList.Add(boostStatus4);
			}
		}
		BoostStatus boostStatus5 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.NOVICE_DROP_UP);
		if (boostStatus5 != null)
		{
			enableBoostList.Add(boostStatus5);
		}
		BoostStatus boostStatus6 = MonoBehaviourSingleton<StatusManager>.I.GetBoostStatus(USE_ITEM_EFFECT_TYPE.HAPPEN_QUEST_UP);
		if (boostStatus6 != null)
		{
			enableBoostList.Add(boostStatus6);
		}
		USE_ITEM_EFFECT_TYPE num = boostDispType;
		CreateAnimTable();
		updateCallback = update_callback;
		changeCallback = change_callback;
		if (num != boostDispType || boostDispType == USE_ITEM_EFFECT_TYPE.NONE)
		{
			changeCallback(GetShowBoostStatus());
		}
	}

	private void CreateAnimTable()
	{
		int count = enableBoostList.Count;
		if (count == 0)
		{
			boostDispType = USE_ITEM_EFFECT_TYPE.NONE;
			return;
		}
		animTable = new int[count];
		int index = 0;
		enableBoostList.ForEach(delegate(BoostStatus boost)
		{
			animTable[index] = boost.type;
			int num2 = ++index;
		});
		if (boostDispType == USE_ITEM_EFFECT_TYPE.NONE)
		{
			animIndex = 0;
		}
		else
		{
			int num = enableBoostList.FindIndex((BoostStatus data) => data.type == (int)boostDispType);
			animIndex = ((num != -1) ? num : 0);
		}
		boostDispType = (USE_ITEM_EFFECT_TYPE)animTable[animIndex];
	}

	private void Update()
	{
		if (enableBoostList.Count <= 0)
		{
			return;
		}
		DateTime now = TimeManager.GetNow();
		bool flag = false;
		if (time == now.Second)
		{
			return;
		}
		time = now.Second;
		int i = 0;
		for (int count = enableBoostList.Count; i < count; i++)
		{
			BoostStatus boostStatus = enableBoostList[i];
			if (!boostStatus.IsRemain())
			{
				deleteBoostList.Add(boostStatus);
				if (boostDispType == (USE_ITEM_EFFECT_TYPE)boostStatus.type)
				{
					flag = true;
				}
			}
		}
		bool flag2 = false;
		if (deleteBoostList.Count > 0)
		{
			flag2 = true;
			int j = 0;
			for (int count2 = deleteBoostList.Count; j < count2; j++)
			{
				BoostStatus delete_boost = deleteBoostList[j];
				enableBoostList.RemoveAll((BoostStatus data) => data.type == delete_boost.type);
			}
			deleteBoostList.Clear();
		}
		if (flag2)
		{
			CreateAnimTable();
		}
		if (enableBoostList.Count > 0)
		{
			updataCnt++;
			if (flag || updataCnt >= 3)
			{
				ShowNextBoost(flag);
				updataCnt = 0;
			}
			else
			{
				updateCallback(GetShowBoostStatus());
			}
		}
		else
		{
			updataCnt = 0;
			EndShowBoost();
		}
	}

	private void ShowNextBoost(bool is_recommend_change)
	{
		animIndex++;
		if (animIndex >= animTable.Length)
		{
			animIndex = 0;
		}
		USE_ITEM_EFFECT_TYPE num = boostDispType;
		boostDispType = (USE_ITEM_EFFECT_TYPE)animTable[animIndex];
		if ((num != boostDispType) | is_recommend_change)
		{
			changeCallback(GetShowBoostStatus());
		}
		else
		{
			updateCallback(GetShowBoostStatus());
		}
	}

	private void EndShowBoost()
	{
		boostDispType = USE_ITEM_EFFECT_TYPE.NONE;
		changeCallback(GetShowBoostStatus());
	}

	public USE_ITEM_EFFECT_TYPE GetShowBoostType()
	{
		return boostDispType;
	}

	public BoostStatus GetShowBoostStatus()
	{
		if (boostDispType == USE_ITEM_EFFECT_TYPE.NONE)
		{
			return null;
		}
		if (enableBoostList.Count == 0)
		{
			return null;
		}
		return enableBoostList.Find((BoostStatus data) => data.type == (int)boostDispType);
	}

	public Color GetRateColor(int boost_rate)
	{
		int num = 100 + boost_rate;
		int num2 = (num >= 500) ? 4 : ((num >= 300) ? 3 : ((num >= 200) ? 2 : ((num > 100) ? 1 : 0)));
		return TEXT_RATE_COLOR[num2];
	}
}
