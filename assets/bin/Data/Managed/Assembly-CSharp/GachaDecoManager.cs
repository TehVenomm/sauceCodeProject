using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaDecoManager : MonoBehaviourSingleton<GachaDecoManager>
{
	private bool visible;

	private List<GachaDeco> list;

	private int index;

	public void SetVisible(bool is_visible)
	{
		visible = is_visible;
	}

	private IEnumerator Start()
	{
		double interval_time = (double)MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.gachaDecoIntervalTime;
		GachaDeco info = null;
		while (true)
		{
			yield return (object)null;
			if (list != MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoList)
			{
				list = MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoList;
				index = 0;
			}
			if (!IsVisible() || list == null || list.Count == 0)
			{
				if (info != null)
				{
					UpdateGachaDeco(null);
					info = null;
				}
			}
			else
			{
				if (list.Count <= index)
				{
					index = 0;
				}
				info = list[index];
				double num = (double)info.remainTime;
				TimeSpan timeSpan = new TimeSpan(TimeManager.GetNow().Ticks - MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoDateBase);
				double remain_time = num - timeSpan.TotalSeconds;
				double wait_time = interval_time;
				if (wait_time > remain_time)
				{
					wait_time = remain_time;
				}
				if (wait_time > 1.0)
				{
					UpdateGachaDeco(info);
					float timer = (float)wait_time;
					while (timer > 0f && IsVisible())
					{
						timer -= Time.deltaTime;
						yield return (object)null;
					}
				}
				if (wait_time != interval_time)
				{
					list.Remove(info);
				}
				else
				{
					index++;
				}
			}
		}
	}

	protected override void _OnDestroy()
	{
		UpdateGachaDeco(null);
	}

	private void UpdateGachaDeco(GachaDeco info)
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainMenu != (UnityEngine.Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainMenu.UpdateGachaDeco(info);
		}
	}

	private bool IsVisible()
	{
		if (!visible)
		{
			return false;
		}
		bool flag = false;
		List<GameSectionHierarchy.HierarchyData> hierarchyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHierarchyList();
		int i = 0;
		for (int count = hierarchyList.Count; i < count; i++)
		{
			GameSectionHierarchy.HierarchyData hierarchyData = hierarchyList[i];
			if (hierarchyData != null && hierarchyData.data != (GameSceneTables.SectionData)null)
			{
				if (hierarchyData.data.sectionName == "HomeTop" || hierarchyData.data.sectionName == "LoungeTop")
				{
					flag = true;
				}
				else if (flag && !hierarchyData.data.type.IsDialog())
				{
					flag = false;
					break;
				}
			}
		}
		return flag;
	}
}
