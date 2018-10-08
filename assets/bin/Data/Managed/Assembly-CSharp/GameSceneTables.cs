using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneTables
{
	public class EventData
	{
		public string appVer;

		public string eventName;

		public string toSectionName;

		public UITransition.TYPE closeType;

		public UITransition.TYPE openType;

		public EventData()
		{
		}

		public EventData(string eventName, string toSectionName)
		{
			this.eventName = eventName;
			this.toSectionName = toSectionName;
			closeType = UITransition.TYPE.CLOSE;
			openType = UITransition.TYPE.OPEN;
		}
	}

	public class TextData
	{
		public string key;

		public string text;
	}

	public class SectionData
	{
		public string sectionName;

		public GAME_SECTION_TYPE type;

		public string[] typeParams;

		public List<string> useResourceList = new List<string>();

		public List<string> preloadResourceList;

		public List<EventData> eventDataList;

		public int backButtonIndex;

		public bool isTop;

		public List<TextData> textList;

		public EventData GetEventData(string event_name)
		{
			if (eventDataList != null)
			{
				int i = 0;
				for (int count = eventDataList.Count; i < count; i++)
				{
					if (eventDataList[i].eventName == event_name)
					{
						return eventDataList[i];
					}
				}
			}
			return null;
		}

		public string GetText(string key)
		{
			if (textList != null)
			{
				int i = 0;
				for (int count = textList.Count; i < count; i++)
				{
					if (textList[i].key == key)
					{
						return textList[i].text;
					}
				}
			}
			return string.Empty;
		}

		public LoadObject[] LoadUseResources(LoadingQueue load_queue)
		{
			LoadObject[] array = new LoadObject[useResourceList.Count];
			int i = 0;
			for (int count = useResourceList.Count; i < count; i++)
			{
				array[i] = load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.UI, useResourceList[i]);
			}
			return array;
		}

		public void LoadPreloadResources(LoadingQueue load_queue)
		{
			if (preloadResourceList != null)
			{
				int i = 0;
				for (int count = preloadResourceList.Count; i < count; i++)
				{
					load_queue.Load(RESOURCE_CATEGORY.UI, preloadResourceList[i], false);
				}
			}
		}

		public static bool operator ==(SectionData a, SectionData b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.sectionName == b.sectionName;
		}

		public static bool operator !=(SectionData a, SectionData b)
		{
			return !(a == b);
		}
	}

	public class SceneData
	{
		public string sceneName;

		public List<SectionData> sectionList = new List<SectionData>();

		public SectionData GetSectionData(string section_name)
		{
			return sectionList.Find((SectionData o) => o.sectionName == section_name);
		}

		public static bool operator ==(SceneData a, SceneData b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.sceneName == b.sceneName;
		}

		public static bool operator !=(SceneData a, SceneData b)
		{
			return !(a == b);
		}
	}

	private StringKeyTable<SceneData> sceneDataTable = new StringKeyTable<SceneData>();

	private StringKeyTable<string> commonResourceTable = new StringKeyTable<string>();

	public SceneData GetSceneData(string scene_name, string section_name)
	{
		SceneData sceneData = sceneDataTable.Get(scene_name);
		if (sceneData != (SceneData)null && !string.IsNullOrEmpty(section_name) && sceneData.GetSectionData(section_name) == (SectionData)null)
		{
			SceneData sceneDataFromSectionName = GetSceneDataFromSectionName(section_name);
			if (sceneDataFromSectionName != (SceneData)null)
			{
				sceneData = sceneDataFromSectionName;
			}
		}
		return sceneData;
	}

	public SceneData GetSceneDataFromSectionName(string section_name)
	{
		SceneData find_data = null;
		if (!string.IsNullOrEmpty(section_name))
		{
			sceneDataTable.ForEach(delegate(SceneData o)
			{
				if (find_data == (SceneData)null && o.GetSectionData(section_name) != (SectionData)null)
				{
					find_data = o;
					if (LoungeMatchingManager.IsValidInLounge() && o.sceneName == "Home")
					{
						find_data = null;
					}
					if (!LoungeMatchingManager.IsValidInLounge() && o.sceneName == "Lounge")
					{
						find_data = null;
					}
				}
			});
		}
		return find_data;
	}

	public SceneData CreateSceneData(string scene_name, TextAsset text_asset)
	{
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			CSVReader cSVReader = new CSVReader(text_asset.get_text(), "name,type,useRes,loadRes,appVer,evName,evTo,retBtn,strKey,strJP", true);
			SceneData sceneData = new SceneData();
			sceneData.sceneName = scene_name;
			SectionData sectionData = null;
			string text = null;
			while (cSVReader.NextLine())
			{
				string value = string.Empty;
				string value2 = string.Empty;
				string value3 = string.Empty;
				string value4 = string.Empty;
				string value5 = string.Empty;
				string value6 = string.Empty;
				string value7 = string.Empty;
				int value8 = 0;
				string value9 = string.Empty;
				string value10 = string.Empty;
				cSVReader.Pop(ref value);
				cSVReader.Pop(ref value2);
				cSVReader.Pop(ref value3);
				cSVReader.Pop(ref value4);
				cSVReader.Pop(ref value5);
				cSVReader.Pop(ref value6);
				cSVReader.Pop(ref value7);
				cSVReader.Pop(ref value8);
				cSVReader.Pop(ref value9);
				cSVReader.Pop(ref value10);
				if (value.Length > 0)
				{
					sectionData = new SectionData();
					if (value == "SCENE")
					{
						value = scene_name + "Scene";
					}
					sectionData.sectionName = value;
					if (text != null && value == text)
					{
						sectionData.isTop = true;
						text = null;
					}
					if (value2.Length == 0)
					{
						throw new UnityException("scene table parse error");
					}
					sectionData.typeParams = value2.Split(new char[1]
					{
						':'
					}, StringSplitOptions.RemoveEmptyEntries);
					try
					{
						sectionData.type = (GAME_SECTION_TYPE)(int)Enum.Parse(typeof(GAME_SECTION_TYPE), sectionData.typeParams[0]);
					}
					catch (Exception)
					{
						sectionData.type = GAME_SECTION_TYPE.COMMON_DIALOG;
					}
					sectionData.backButtonIndex = value8;
					sceneData.sectionList.Add(sectionData);
				}
				if (value3.Length > 0)
				{
					sectionData.useResourceList.Add(value3);
				}
				if (value4.Length > 0)
				{
					if (sectionData.preloadResourceList == null)
					{
						sectionData.preloadResourceList = new List<string>();
					}
					sectionData.preloadResourceList.Add(value4);
				}
				if (value6.Length > 0 || value7.Length > 0)
				{
					EventData eventData = new EventData();
					eventData.appVer = value5;
					eventData.eventName = value6;
					if (sectionData.type == GAME_SECTION_TYPE.SCENE && value6.Length == 0 && value7.Length > 0)
					{
						text = value7;
					}
					string[] array = value7.Split(':');
					eventData.toSectionName = array[0];
					eventData.closeType = UITransition.TYPE.CLOSE;
					eventData.openType = UITransition.TYPE.OPEN;
					int i = 1;
					for (int num = array.Length; i < num; i++)
					{
						string text2 = array[i];
						if (text2.Length == 3 && text2[1] == '>')
						{
							if (text2[0] == 'c')
							{
								eventData.closeType = UITransition.GetType(text2[2]);
							}
							else if (text2[0] == 'o')
							{
								eventData.openType = UITransition.GetType(text2[2]);
							}
						}
					}
					if (sectionData.eventDataList == null)
					{
						sectionData.eventDataList = new List<EventData>();
					}
					sectionData.eventDataList.Add(eventData);
				}
				if (value9.Length > 0 || value10.Length > 0)
				{
					TextData textData = new TextData();
					textData.key = value9;
					textData.text = value10;
					if (sectionData.textList == null)
					{
						sectionData.textList = new List<TextData>();
					}
					sectionData.textList.Add(textData);
				}
			}
			sceneDataTable.Add(scene_name, sceneData);
			return sceneData;
			IL_038e:
			SceneData result;
			return result;
		}
		catch (Exception exc)
		{
			Log.Exception(exc);
			return null;
			IL_03a4:
			SceneData result;
			return result;
		}
	}

	public string GetCommonResourceName(string common_name)
	{
		return commonResourceTable.Get(common_name);
	}

	public void CreateCommonResourceTable(TextAsset text_asset)
	{
		CSVReader cSVReader = new CSVReader(text_asset.get_text(), "name,useRes", true);
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			string value2 = string.Empty;
			cSVReader.Pop(ref value);
			cSVReader.Pop(ref value2);
			if (value.Length > 0 && value2.Length > 0)
			{
				commonResourceTable.Add(value, value2);
			}
		}
	}
}
