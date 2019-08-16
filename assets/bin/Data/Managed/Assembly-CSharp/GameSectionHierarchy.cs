using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSectionHierarchy
{
	public class HierarchyData
	{
		public GameSection section;

		public GameSceneTables.SectionData data;
	}

	private List<HierarchyData> hierarchyList = new List<HierarchyData>();

	private HierarchyData[] typedDatas = new HierarchyData[7];

	private int GetPrefabUIDepth(GAME_SECTION_TYPE type)
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.isOpenImportantDialog)
		{
			return 9999;
		}
		if (type.IsDialog())
		{
			return 5000 + hierarchyList.Count * 10;
		}
		return 1000 + hierarchyList.Count * 10;
	}

	public void DestroyHierarchy(HierarchyData hierarchy_data)
	{
		int type = (int)hierarchy_data.data.type;
		if (typedDatas[type] == hierarchy_data)
		{
			typedDatas[type] = null;
		}
		Object.DestroyImmediate(hierarchy_data.section.get_gameObject());
		hierarchyList.Remove(hierarchy_data);
	}

	public void DestroyHierarchy(List<HierarchyData> list)
	{
		if (!AppMain.isApplicationQuit)
		{
			list.ForEach(delegate(HierarchyData o)
			{
				DestroyHierarchy(o);
			});
			list.Clear();
		}
	}

	public List<HierarchyData> GetExclusiveList(GAME_SECTION_TYPE type)
	{
		List<HierarchyData> list = new List<HierarchyData>();
		int num = (!type.IsSingle()) ? hierarchyList.FindLastIndex((HierarchyData o) => o.data.type == type) : hierarchyList.FindLastIndex((HierarchyData o) => o.data.type.IsSingle());
		if (num == -1)
		{
			if (type == GAME_SECTION_TYPE.SCREEN)
			{
				int num2 = hierarchyList.Count - 1;
				while (num2 >= 0 && hierarchyList[num2].data.type != 0)
				{
					list.Add(hierarchyList[num2]);
					num2--;
				}
			}
			else
			{
				int num3 = hierarchyList.Count - 1;
				while (num3 >= 0 && hierarchyList[num3].data.type.IsSingle())
				{
					list.Add(hierarchyList[num3]);
					num3--;
				}
			}
			return list;
		}
		if (type == GAME_SECTION_TYPE.DIALOG)
		{
			num++;
		}
		for (int num4 = hierarchyList.Count - 1; num4 >= num; num4--)
		{
			list.Add(hierarchyList[num4]);
		}
		return list;
	}

	public List<HierarchyData> GetCutList(HierarchyData hierarchy_data)
	{
		List<HierarchyData> list = new List<HierarchyData>();
		int num = hierarchyList.Count - 1;
		while (num >= 0 && hierarchyList[num] != hierarchy_data)
		{
			list.Add(hierarchyList[num]);
			num--;
		}
		return list;
	}

	public GameSection CreateSection(GameSceneTables.SectionData section_data, LoadObject[] use_objects)
	{
		GameSection gameSection = null;
		HierarchyData last = GetLast();
		Transform parent = (last != null) ? last.section._transform : MonoBehaviourSingleton<UIManager>.I.uiCamera.get_transform();
		if (section_data.type == GAME_SECTION_TYPE.COMMON_DIALOG)
		{
			gameSection = (Utility.CreateGameObjectAndComponent(section_data.typeParams[0], parent, 5) as GameSection);
			gameSection.baseDepth = GetPrefabUIDepth(section_data.type);
			gameSection.set_name(section_data.sectionName);
			parent = gameSection._transform;
		}
		int i = 0;
		for (int num = use_objects.Length; i < num; i++)
		{
			LoadObject loadObject = use_objects[i];
			if (loadObject == null)
			{
				continue;
			}
			GameObject val = loadObject.loadedObject as GameObject;
			if (!(val != null))
			{
				continue;
			}
			if (val.GetComponent<UIVirtualScreen>() != null)
			{
				Type type = null;
				if (gameSection == null)
				{
					type = Type.GetType(section_data.sectionName);
				}
				UIBehaviour uIBehaviour = UIManager.CreatePrefabUI(val, loadObject.PopInstantiatedGameObject(), type, initVisible: false, parent, GetPrefabUIDepth(section_data.type), section_data);
				if (gameSection == null && section_data.type == GAME_SECTION_TYPE.COMMON_DIALOG)
				{
					gameSection = (uIBehaviour.get_gameObject().AddComponent(Type.GetType(section_data.typeParams[0])) as GameSection);
					parent = gameSection._transform;
					continue;
				}
				if (gameSection == null && type != null)
				{
					gameSection = (uIBehaviour.GetComponent<UIBehaviour>() as GameSection);
					parent = gameSection._transform;
					continue;
				}
				if (gameSection == null)
				{
					gameSection = (uIBehaviour.GetComponent<UIBehaviour>() as GameSection);
				}
				if (gameSection == null)
				{
					gameSection = (Utility.CreateGameObjectAndComponent(section_data.sectionName, parent, 5) as GameSection);
					gameSection.baseDepth = GetPrefabUIDepth(section_data.type);
					parent = gameSection._transform;
				}
				if (section_data.type != GAME_SECTION_TYPE.COMMON_DIALOG)
				{
					uIBehaviour.Open();
				}
			}
			else if (gameSection != null)
			{
				gameSection.AddPrefab(val, loadObject.PopInstantiatedGameObject());
			}
			else
			{
				Log.Warning(LOG.GAMESCENE, "[{0}] is not used.", val.get_name());
			}
		}
		if (gameSection == null)
		{
			gameSection = (Utility.CreateGameObjectAndComponent(section_data.sectionName, parent, 5) as GameSection);
			gameSection.baseDepth = GetPrefabUIDepth(section_data.type);
		}
		HierarchyData hierarchyData = new HierarchyData();
		hierarchyData.section = gameSection;
		hierarchyData.data = section_data;
		hierarchyList.Add(hierarchyData);
		int type2 = (int)section_data.type;
		if (typedDatas[type2] == null)
		{
			typedDatas[type2] = hierarchyData;
		}
		return gameSection;
	}

	public HierarchyData GetLast()
	{
		int num = hierarchyList.Count - 1;
		if (num < 0)
		{
			return null;
		}
		return hierarchyList[num];
	}

	public HierarchyData GetOpendLast()
	{
		for (int num = hierarchyList.Count - 1; num >= 0; num--)
		{
			HierarchyData hierarchyData = hierarchyList[num];
			if (hierarchyData.section.state == UIBehaviour.STATE.OPEN || hierarchyData.section.state == UIBehaviour.STATE.TO_OPEN)
			{
				return hierarchyData;
			}
		}
		return null;
	}

	public int GetDialogDialogBlockerDepth(GameSceneTables.SectionData new_section_data)
	{
		HierarchyData hierarchyData = null;
		if (new_section_data != null && new_section_data.type.IsDialog())
		{
			hierarchyData = Find(new_section_data);
			if (hierarchyData != null)
			{
				return hierarchyData.section.baseDepth - 2;
			}
			hierarchyData = GetOpendLast();
			if (hierarchyData != null && hierarchyData.data.type.IsDialog())
			{
				return hierarchyData.section.baseDepth - 2;
			}
			return 3002;
		}
		hierarchyData = GetOpendLast();
		if (hierarchyData != null && hierarchyData.data.type.IsDialog())
		{
			return hierarchyData.section.baseDepth - 2;
		}
		return -1;
	}

	public HierarchyData GetLastExcludeDialog()
	{
		for (int num = hierarchyList.Count - 1; num >= 0; num--)
		{
			HierarchyData hierarchyData = hierarchyList[num];
			if (!hierarchyData.data.type.IsDialog())
			{
				return hierarchyData;
			}
		}
		return null;
	}

	public HierarchyData GetLastExcludeCommonDialog()
	{
		for (int num = hierarchyList.Count - 1; num >= 0; num--)
		{
			HierarchyData hierarchyData = hierarchyList[num];
			if (hierarchyData.data.type != GAME_SECTION_TYPE.COMMON_DIALOG)
			{
				return hierarchyData;
			}
		}
		return null;
	}

	public HierarchyData GetTyped(GAME_SECTION_TYPE type)
	{
		return typedDatas[(int)type];
	}

	public HierarchyData Find(string section_name)
	{
		int i = 0;
		for (int count = hierarchyList.Count; i < count; i++)
		{
			if (hierarchyList[i].data.sectionName == section_name)
			{
				return hierarchyList[i];
			}
		}
		return null;
	}

	public HierarchyData Find(GameSceneTables.SectionData section_data)
	{
		int i = 0;
		for (int count = hierarchyList.Count; i < count; i++)
		{
			if (hierarchyList[i].data == section_data)
			{
				return hierarchyList[i];
			}
		}
		return null;
	}

	public HierarchyData FindIgnoreSingle(GameSceneTables.SectionData section_data)
	{
		int i = 0;
		for (int count = hierarchyList.Count; i < count; i++)
		{
			HierarchyData hierarchyData = hierarchyList[i];
			if (hierarchyData.data == section_data && !hierarchyData.data.type.IsSingle())
			{
				return hierarchyData;
			}
		}
		return null;
	}

	public void DoNotify(GameSection.NOTIFY_FLAG flags)
	{
		int i = 0;
		for (int count = hierarchyList.Count; i < count; i++)
		{
			HierarchyData hierarchyData = hierarchyList[i];
			if (hierarchyData.section.isInitialized)
			{
				hierarchyData.section.OnNotify(flags);
			}
		}
	}

	public List<HierarchyData> GetHierarchyList()
	{
		return hierarchyList;
	}
}
