// Decompiled with JetBrains decompiler
// Type: GameSectionHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class GameSectionHistory
{
  private List<GameSectionHistory.HistoryData> historyList = new List<GameSectionHistory.HistoryData>();

  public void Push(string scene_name, string section_name, GAME_SECTION_TYPE section_type)
  {
    int index1 = this.historyList.FindIndex((Predicate<GameSectionHistory.HistoryData>) (o => o.sceneName == scene_name && o.sectionName == section_name));
    if (index1 != -1)
    {
      int num = this.historyList.Count - 1;
      int index2 = index1 + 1;
      if (index2 > num)
        return;
      this.historyList.RemoveRange(index2, num - index2 + 1);
    }
    else
      this.historyList.Add(new GameSectionHistory.HistoryData()
      {
        sceneName = scene_name,
        sectionName = section_name,
        sectionType = section_type
      });
  }

  public void PopSection()
  {
    int count = this.historyList.Count;
    if (count <= 0)
      return;
    this.historyList.RemoveAt(count - 1);
  }

  public void RemoveSection(GameSectionHistory.HistoryData removeData)
  {
    int index = this.historyList.FindIndex((Predicate<GameSectionHistory.HistoryData>) (x => x.sceneName == removeData.sceneName && x.sectionName == removeData.sectionName));
    if (index < 0)
      return;
    this.historyList.RemoveAt(index);
  }

  public void RemoveSection(string removeSectionName)
  {
    int index = this.historyList.FindIndex((Predicate<GameSectionHistory.HistoryData>) (x => x.sectionName == removeSectionName));
    if (index < 0)
      return;
    this.historyList.RemoveAt(index);
  }

  public void RemoveSections(int num)
  {
    for (; num > 0 && this.historyList.Count > 0; --num)
    {
      int index = this.historyList.Count - 1;
      if (!this.historyList[index].sectionType.IsDialog())
        break;
      this.historyList.RemoveAt(index);
    }
  }

  public void CutScene()
  {
    int lastIndex = this.historyList.FindLastIndex((Predicate<GameSectionHistory.HistoryData>) (o => o.sectionType == GAME_SECTION_TYPE.SCENE));
    if (lastIndex == -1)
      return;
    int num = this.historyList.Count - 1;
    int index = lastIndex + 1;
    if (index > num)
      return;
    this.historyList.RemoveRange(index, num - index + 1);
  }

  public void CutDialog()
  {
    int lastIndex = this.historyList.FindLastIndex((Predicate<GameSectionHistory.HistoryData>) (o => !o.sectionType.IsDialog()));
    if (lastIndex == -1)
      return;
    int num = this.historyList.Count - 1;
    int index = lastIndex + 1;
    if (index > num)
      return;
    this.historyList.RemoveRange(index, num - index + 1);
  }

  public void CutSingleDialog()
  {
    int lastIndex = this.historyList.FindLastIndex((Predicate<GameSectionHistory.HistoryData>) (o => !o.sectionType.IsSingle()));
    if (lastIndex == -1)
      return;
    int num = this.historyList.Count - 1;
    int index = lastIndex + 1;
    if (index > num)
      return;
    this.historyList.RemoveRange(index, num - index + 1);
  }

  public GameSectionHistory.HistoryData GetLast()
  {
    int count = this.historyList.Count;
    return count == 0 ? (GameSectionHistory.HistoryData) null : this.historyList[count - 1];
  }

  public GameSectionHistory.HistoryData GetLast(int i, bool ignore_common_dialog = false)
  {
    int count = this.historyList.Count;
    GameSectionHistory.HistoryData last = (GameSectionHistory.HistoryData) null;
    while (last == null && i > 0 && i <= count)
    {
      last = this.historyList[count - i];
      if (ignore_common_dialog && last.sectionType == GAME_SECTION_TYPE.COMMON_DIALOG)
      {
        last = (GameSectionHistory.HistoryData) null;
        ++i;
      }
    }
    return last;
  }

  public bool Exist(string name)
  {
    int index = 0;
    for (int count = this.historyList.Count; index < count; ++index)
    {
      if (this.historyList[index].sectionName == name)
        return true;
    }
    return false;
  }

  public void Clear() => this.historyList.Clear();

  public List<GameSectionHistory.HistoryData> GetHistoryList() => this.historyList;

  public class HistoryData
  {
    public string sceneName;
    public string sectionName;
    public GAME_SECTION_TYPE sectionType;
  }
}
