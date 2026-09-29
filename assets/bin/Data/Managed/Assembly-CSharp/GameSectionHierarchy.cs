// Decompiled with JetBrains decompiler
// Type: GameSectionHierarchy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GameSectionHierarchy
{
  private List<GameSectionHierarchy.HierarchyData> hierarchyList = new List<GameSectionHierarchy.HierarchyData>();
  private GameSectionHierarchy.HierarchyData[] typedDatas = new GameSectionHierarchy.HierarchyData[7];

  private int GetPrefabUIDepth(GAME_SECTION_TYPE type)
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.isOpenImportantDialog)
      return 9999;
    return type.IsDialog() ? 5000 + this.hierarchyList.Count * 10 : 1000 + this.hierarchyList.Count * 10;
  }

  public void DestroyHierarchy(GameSectionHierarchy.HierarchyData hierarchy_data)
  {
    int type = (int) hierarchy_data.data.type;
    if (this.typedDatas[type] == hierarchy_data)
      this.typedDatas[type] = (GameSectionHierarchy.HierarchyData) null;
    Object.DestroyImmediate((Object) ((Component) hierarchy_data.section).gameObject);
    this.hierarchyList.Remove(hierarchy_data);
  }

  public void DestroyHierarchy(List<GameSectionHierarchy.HierarchyData> list)
  {
    if (AppMain.isApplicationQuit)
      return;
    list.ForEach((Action<GameSectionHierarchy.HierarchyData>) (o => this.DestroyHierarchy(o)));
    list.Clear();
  }

  public List<GameSectionHierarchy.HierarchyData> GetExclusiveList(GAME_SECTION_TYPE type)
  {
    List<GameSectionHierarchy.HierarchyData> exclusiveList = new List<GameSectionHierarchy.HierarchyData>();
    int num = !type.IsSingle() ? this.hierarchyList.FindLastIndex((Predicate<GameSectionHierarchy.HierarchyData>) (o => o.data.type == type)) : this.hierarchyList.FindLastIndex((Predicate<GameSectionHierarchy.HierarchyData>) (o => o.data.type.IsSingle()));
    if (num == -1)
    {
      if (type == GAME_SECTION_TYPE.SCREEN)
      {
        for (int index = this.hierarchyList.Count - 1; index >= 0 && this.hierarchyList[index].data.type != GAME_SECTION_TYPE.SCENE; --index)
          exclusiveList.Add(this.hierarchyList[index]);
      }
      else
      {
        for (int index = this.hierarchyList.Count - 1; index >= 0 && this.hierarchyList[index].data.type.IsSingle(); --index)
          exclusiveList.Add(this.hierarchyList[index]);
      }
      return exclusiveList;
    }
    if (type == GAME_SECTION_TYPE.DIALOG)
      ++num;
    for (int index = this.hierarchyList.Count - 1; index >= num; --index)
      exclusiveList.Add(this.hierarchyList[index]);
    return exclusiveList;
  }

  public List<GameSectionHierarchy.HierarchyData> GetCutList(
    GameSectionHierarchy.HierarchyData hierarchy_data)
  {
    List<GameSectionHierarchy.HierarchyData> cutList = new List<GameSectionHierarchy.HierarchyData>();
    for (int index = this.hierarchyList.Count - 1; index >= 0 && this.hierarchyList[index] != hierarchy_data; --index)
      cutList.Add(this.hierarchyList[index]);
    return cutList;
  }

  public GameSection CreateSection(
    GameSceneTables.SectionData section_data,
    LoadObject[] use_objects)
  {
    GameSection section = (GameSection) null;
    GameSectionHierarchy.HierarchyData last = this.GetLast();
    Transform parent = last == null ? ((Component) MonoBehaviourSingleton<UIManager>.I.uiCamera).transform : last.section._transform;
    if (section_data.type == GAME_SECTION_TYPE.COMMON_DIALOG)
    {
      section = Utility.CreateGameObjectAndComponent(section_data.typeParams[0], parent, 5) as GameSection;
      section.baseDepth = this.GetPrefabUIDepth(section_data.type);
      ((Object) section).name = section_data.sectionName;
      parent = section._transform;
    }
    int index = 0;
    for (int length = use_objects.Length; index < length; ++index)
    {
      LoadObject useObject = use_objects[index];
      if (useObject != null)
      {
        GameObject loadedObject = useObject.loadedObject as GameObject;
        if (Object.op_Inequality((Object) loadedObject, (Object) null))
        {
          if (Object.op_Inequality((Object) loadedObject.GetComponent<UIVirtualScreen>(), (Object) null))
          {
            System.Type add_component_type = (System.Type) null;
            if (Object.op_Equality((Object) section, (Object) null))
              add_component_type = System.Type.GetType(section_data.sectionName);
            UIBehaviour prefabUi = UIManager.CreatePrefabUI((Object) loadedObject, useObject.PopInstantiatedGameObject(), add_component_type, false, parent, this.GetPrefabUIDepth(section_data.type), section_data);
            if (Object.op_Equality((Object) section, (Object) null) && section_data.type == GAME_SECTION_TYPE.COMMON_DIALOG)
            {
              section = ((Component) prefabUi).gameObject.AddComponent(System.Type.GetType(section_data.typeParams[0])) as GameSection;
              parent = section._transform;
            }
            else if (Object.op_Equality((Object) section, (Object) null) && add_component_type != (System.Type) null)
            {
              section = ((Component) prefabUi).GetComponent<UIBehaviour>() as GameSection;
              parent = section._transform;
            }
            else
            {
              if (Object.op_Equality((Object) section, (Object) null))
                section = ((Component) prefabUi).GetComponent<UIBehaviour>() as GameSection;
              if (Object.op_Equality((Object) section, (Object) null))
              {
                section = Utility.CreateGameObjectAndComponent(section_data.sectionName, parent, 5) as GameSection;
                section.baseDepth = this.GetPrefabUIDepth(section_data.type);
                parent = section._transform;
              }
              if (section_data.type != GAME_SECTION_TYPE.COMMON_DIALOG)
                prefabUi.Open();
            }
          }
          else if (Object.op_Inequality((Object) section, (Object) null))
            section.AddPrefab(loadedObject, useObject.PopInstantiatedGameObject());
          else
            Log.Warning(LOG.GAMESCENE, "[{0}] is not used.", (object) ((Object) loadedObject).name);
        }
      }
    }
    if (Object.op_Equality((Object) section, (Object) null))
    {
      section = Utility.CreateGameObjectAndComponent(section_data.sectionName, parent, 5) as GameSection;
      section.baseDepth = this.GetPrefabUIDepth(section_data.type);
    }
    GameSectionHierarchy.HierarchyData hierarchyData = new GameSectionHierarchy.HierarchyData();
    hierarchyData.section = section;
    hierarchyData.data = section_data;
    this.hierarchyList.Add(hierarchyData);
    int type = (int) section_data.type;
    if (this.typedDatas[type] == null)
      this.typedDatas[type] = hierarchyData;
    return section;
  }

  public GameSectionHierarchy.HierarchyData GetLast()
  {
    int index = this.hierarchyList.Count - 1;
    return index < 0 ? (GameSectionHierarchy.HierarchyData) null : this.hierarchyList[index];
  }

  public GameSectionHierarchy.HierarchyData GetOpendLast()
  {
    for (int index = this.hierarchyList.Count - 1; index >= 0; --index)
    {
      GameSectionHierarchy.HierarchyData hierarchy = this.hierarchyList[index];
      if (hierarchy.section.state == UIBehaviour.STATE.OPEN || hierarchy.section.state == UIBehaviour.STATE.TO_OPEN)
        return hierarchy;
    }
    return (GameSectionHierarchy.HierarchyData) null;
  }

  public int GetDialogDialogBlockerDepth(GameSceneTables.SectionData new_section_data)
  {
    if (new_section_data != (GameSceneTables.SectionData) null && new_section_data.type.IsDialog())
    {
      GameSectionHierarchy.HierarchyData hierarchyData = this.Find(new_section_data);
      if (hierarchyData != null)
        return hierarchyData.section.baseDepth - 2;
      GameSectionHierarchy.HierarchyData opendLast = this.GetOpendLast();
      return opendLast != null && opendLast.data.type.IsDialog() ? opendLast.section.baseDepth - 2 : 3002;
    }
    GameSectionHierarchy.HierarchyData opendLast1 = this.GetOpendLast();
    return opendLast1 != null && opendLast1.data.type.IsDialog() ? opendLast1.section.baseDepth - 2 : -1;
  }

  public GameSectionHierarchy.HierarchyData GetLastExcludeDialog()
  {
    for (int index = this.hierarchyList.Count - 1; index >= 0; --index)
    {
      GameSectionHierarchy.HierarchyData hierarchy = this.hierarchyList[index];
      if (!hierarchy.data.type.IsDialog())
        return hierarchy;
    }
    return (GameSectionHierarchy.HierarchyData) null;
  }

  public GameSectionHierarchy.HierarchyData GetLastExcludeCommonDialog()
  {
    for (int index = this.hierarchyList.Count - 1; index >= 0; --index)
    {
      GameSectionHierarchy.HierarchyData hierarchy = this.hierarchyList[index];
      if (hierarchy.data.type != GAME_SECTION_TYPE.COMMON_DIALOG)
        return hierarchy;
    }
    return (GameSectionHierarchy.HierarchyData) null;
  }

  public GameSectionHierarchy.HierarchyData GetTyped(GAME_SECTION_TYPE type)
  {
    return this.typedDatas[(int) type];
  }

  public GameSectionHierarchy.HierarchyData Find(string section_name)
  {
    int index = 0;
    for (int count = this.hierarchyList.Count; index < count; ++index)
    {
      if (this.hierarchyList[index].data.sectionName == section_name)
        return this.hierarchyList[index];
    }
    return (GameSectionHierarchy.HierarchyData) null;
  }

  public GameSectionHierarchy.HierarchyData Find(GameSceneTables.SectionData section_data)
  {
    int index = 0;
    for (int count = this.hierarchyList.Count; index < count; ++index)
    {
      if (this.hierarchyList[index].data == section_data)
        return this.hierarchyList[index];
    }
    return (GameSectionHierarchy.HierarchyData) null;
  }

  public GameSectionHierarchy.HierarchyData FindIgnoreSingle(
    GameSceneTables.SectionData section_data)
  {
    int index = 0;
    for (int count = this.hierarchyList.Count; index < count; ++index)
    {
      GameSectionHierarchy.HierarchyData hierarchy = this.hierarchyList[index];
      if (hierarchy.data == section_data && !hierarchy.data.type.IsSingle())
        return hierarchy;
    }
    return (GameSectionHierarchy.HierarchyData) null;
  }

  public void DoNotify(GameSection.NOTIFY_FLAG flags)
  {
    int index = 0;
    for (int count = this.hierarchyList.Count; index < count; ++index)
    {
      GameSectionHierarchy.HierarchyData hierarchy = this.hierarchyList[index];
      if (hierarchy.section.isInitialized)
        hierarchy.section.OnNotify(flags);
    }
  }

  public List<GameSectionHierarchy.HierarchyData> GetHierarchyList() => this.hierarchyList;

  public class HierarchyData
  {
    public GameSection section;
    public GameSceneTables.SectionData data;
  }
}
