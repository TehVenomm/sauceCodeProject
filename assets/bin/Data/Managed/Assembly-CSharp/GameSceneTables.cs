// Decompiled with JetBrains decompiler
// Type: GameSceneTables
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GameSceneTables
{
  private StringKeyTable<GameSceneTables.SceneData> sceneDataTable = new StringKeyTable<GameSceneTables.SceneData>();
  private StringKeyTable<string> commonResourceTable = new StringKeyTable<string>();

  public GameSceneTables.SceneData GetSceneData(string scene_name, string section_name)
  {
    GameSceneTables.SceneData sceneData = this.sceneDataTable.Get(scene_name);
    if (sceneData != (GameSceneTables.SceneData) null && !string.IsNullOrEmpty(section_name) && sceneData.GetSectionData(section_name) == (GameSceneTables.SectionData) null)
    {
      GameSceneTables.SceneData dataFromSectionName = this.GetSceneDataFromSectionName(section_name);
      if (dataFromSectionName != (GameSceneTables.SceneData) null)
        sceneData = dataFromSectionName;
    }
    return sceneData;
  }

  public GameSceneTables.SceneData GetSceneDataFromSectionName(string section_name)
  {
    GameSceneTables.SceneData find_data = (GameSceneTables.SceneData) null;
    if (!string.IsNullOrEmpty(section_name))
      this.sceneDataTable.ForEach((Action<GameSceneTables.SceneData>) (o =>
      {
        if (!(find_data == (GameSceneTables.SceneData) null) || !(o.GetSectionData(section_name) != (GameSceneTables.SectionData) null))
          return;
        find_data = o;
        int num = LoungeMatchingManager.IsValidInLounge() ? 1 : 0;
        bool flag1 = ClanMatchingManager.IsValidInClan() || MonoBehaviourSingleton<ClanManager>.IsValid();
        bool flag2 = num == 0 && !flag1;
        if (num != 0 && (o.sceneName == "Home" || o.sceneName == "Clan"))
          find_data = (GameSceneTables.SceneData) null;
        if (flag2 && (o.sceneName == "Lounge" || o.sceneName == "Clan"))
          find_data = (GameSceneTables.SceneData) null;
        if (!flag1 || !(o.sceneName == "Lounge") && !(o.sceneName == "Home"))
          return;
        find_data = (GameSceneTables.SceneData) null;
      }));
    return find_data;
  }

  public GameSceneTables.SceneData CreateSceneData(string scene_name, TextAsset text_asset)
  {
    try
    {
      CSVReader csvReader = new CSVReader(text_asset.text, "name,type,useRes,loadRes,appVer,evName,evTo,retBtn,strKey,strJP", true);
      GameSceneTables.SceneData sceneData = new GameSceneTables.SceneData();
      sceneData.sceneName = scene_name;
      GameSceneTables.SectionData sectionData = (GameSceneTables.SectionData) null;
      string str1 = (string) null;
      while (csvReader.NextLine())
      {
        string str2 = string.Empty;
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string empty4 = string.Empty;
        string empty5 = string.Empty;
        string empty6 = string.Empty;
        int num = 0;
        string empty7 = string.Empty;
        string empty8 = string.Empty;
        csvReader.Pop(ref str2);
        csvReader.Pop(ref empty1);
        csvReader.Pop(ref empty2);
        csvReader.Pop(ref empty3);
        csvReader.Pop(ref empty4);
        csvReader.Pop(ref empty5);
        csvReader.Pop(ref empty6);
        csvReader.Pop(ref num);
        csvReader.Pop(ref empty7);
        csvReader.Pop(ref empty8);
        if (str2.Length > 0)
        {
          sectionData = new GameSceneTables.SectionData();
          if (str2 == "SCENE")
            str2 = scene_name + "Scene";
          sectionData.sectionName = str2;
          if (str1 != null && str2 == str1)
          {
            sectionData.isTop = true;
            str1 = (string) null;
          }
          sectionData.typeParams = empty1.Length != 0 ? empty1.Split(new char[1]
          {
            ':'
          }, StringSplitOptions.RemoveEmptyEntries) : throw new UnityException("scene table parse error");
          try
          {
            sectionData.type = (GAME_SECTION_TYPE) Enum.Parse(typeof (GAME_SECTION_TYPE), sectionData.typeParams[0]);
          }
          catch (Exception ex)
          {
            sectionData.type = GAME_SECTION_TYPE.COMMON_DIALOG;
          }
          sectionData.backButtonIndex = num;
          sceneData.sectionList.Add(sectionData);
        }
        if (empty2.Length > 0)
          sectionData.useResourceList.Add(empty2);
        if (empty3.Length > 0)
        {
          if (sectionData.preloadResourceList == null)
            sectionData.preloadResourceList = new List<string>();
          sectionData.preloadResourceList.Add(empty3);
        }
        if (empty5.Length > 0 || empty6.Length > 0)
        {
          GameSceneTables.EventData eventData = new GameSceneTables.EventData();
          eventData.appVer = empty4;
          eventData.eventName = empty5;
          if (sectionData.type == GAME_SECTION_TYPE.SCENE && empty5.Length == 0 && empty6.Length > 0)
            str1 = empty6;
          string[] strArray = empty6.Split(':');
          eventData.toSectionName = strArray[0];
          eventData.closeType = UITransition.TYPE.CLOSE;
          eventData.openType = UITransition.TYPE.OPEN;
          int index = 1;
          for (int length = strArray.Length; index < length; ++index)
          {
            string str3 = strArray[index];
            if (str3.Length == 3 && str3[1] == '>')
            {
              if (str3[0] == 'c')
                eventData.closeType = UITransition.GetType(str3[2]);
              else if (str3[0] == 'o')
                eventData.openType = UITransition.GetType(str3[2]);
            }
          }
          if (sectionData.eventDataList == null)
            sectionData.eventDataList = new List<GameSceneTables.EventData>();
          sectionData.eventDataList.Add(eventData);
        }
        if (empty7.Length > 0 || empty8.Length > 0)
        {
          GameSceneTables.TextData textData = new GameSceneTables.TextData();
          textData.key = empty7;
          textData.text = empty8;
          if (sectionData.textList == null)
            sectionData.textList = new List<GameSceneTables.TextData>();
          sectionData.textList.Add(textData);
        }
      }
      this.sceneDataTable.Add(scene_name, sceneData);
      return sceneData;
    }
    catch (Exception ex)
    {
      Log.Exception(ex);
      return (GameSceneTables.SceneData) null;
    }
  }

  public string GetCommonResourceName(string common_name)
  {
    return this.commonResourceTable.Get(common_name);
  }

  public void CreateCommonResourceTable(TextAsset text_asset)
  {
    CSVReader csvReader = new CSVReader(text_asset.text, "name,useRes", true);
    while (csvReader.NextLine())
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      csvReader.Pop(ref empty1);
      csvReader.Pop(ref empty2);
      if (empty1.Length > 0 && empty2.Length > 0)
        this.commonResourceTable.Add(empty1, empty2);
    }
  }

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
      this.closeType = UITransition.TYPE.CLOSE;
      this.openType = UITransition.TYPE.OPEN;
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
    public List<GameSceneTables.EventData> eventDataList;
    public int backButtonIndex;
    public bool isTop;
    public List<GameSceneTables.TextData> textList;

    public GameSceneTables.EventData GetEventData(string event_name)
    {
      if (this.eventDataList != null)
      {
        int index = 0;
        for (int count = this.eventDataList.Count; index < count; ++index)
        {
          if (this.eventDataList[index].eventName == event_name)
            return this.eventDataList[index];
        }
      }
      return (GameSceneTables.EventData) null;
    }

    public string GetText(string key)
    {
      if (this.textList != null)
      {
        int index = 0;
        for (int count = this.textList.Count; index < count; ++index)
        {
          if (this.textList[index].key == key)
            return this.textList[index].text;
        }
      }
      return string.Empty;
    }

    public LoadObject[] LoadUseResources(LoadingQueue load_queue)
    {
      LoadObject[] loadObjectArray = new LoadObject[this.useResourceList.Count];
      int index = 0;
      for (int count = this.useResourceList.Count; index < count; ++index)
        loadObjectArray[index] = (LoadObject) load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.UI, this.useResourceList[index]);
      return loadObjectArray;
    }

    public void LoadPreloadResources(LoadingQueue load_queue)
    {
      if (this.preloadResourceList == null)
        return;
      int index = 0;
      for (int count = this.preloadResourceList.Count; index < count; ++index)
        load_queue.Load(RESOURCE_CATEGORY.UI, this.preloadResourceList[index]);
    }

    public static bool operator ==(GameSceneTables.SectionData a, GameSceneTables.SectionData b)
    {
      if ((object) a == (object) b)
        return true;
      return (object) a != null && (object) b != null && a.sectionName == b.sectionName;
    }

    public static bool operator !=(GameSceneTables.SectionData a, GameSceneTables.SectionData b)
    {
      return !(a == b);
    }
  }

  public class SceneData
  {
    public string sceneName;
    public List<GameSceneTables.SectionData> sectionList = new List<GameSceneTables.SectionData>();

    public GameSceneTables.SectionData GetSectionData(string section_name)
    {
      return this.sectionList.Find((Predicate<GameSceneTables.SectionData>) (o => o.sectionName == section_name));
    }

    public static bool operator ==(GameSceneTables.SceneData a, GameSceneTables.SceneData b)
    {
      if ((object) a == (object) b)
        return true;
      return (object) a != null && (object) b != null && a.sceneName == b.sceneName;
    }

    public static bool operator !=(GameSceneTables.SceneData a, GameSceneTables.SceneData b)
    {
      return !(a == b);
    }
  }
}
