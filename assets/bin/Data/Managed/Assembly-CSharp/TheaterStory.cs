// Decompiled with JetBrains decompiler
// Type: TheaterStory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TheaterStory : GameSection
{
  public const int PAGING = 10;
  private List<TheaterModeTable.TheaterModeData> m_canViewStoryList;
  private string m_chapterName = "";
  private int m_nowPage = 1;
  private int m_pageMax = 1;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.m_canViewStoryList = eventData[0] as List<TheaterModeTable.TheaterModeData>;
    this.m_chapterName = eventData[1] as string;
    this.m_canViewStoryList.Sort((Comparison<TheaterModeTable.TheaterModeData>) ((a, b) =>
    {
      if (b.order != 0 && a.order != 0)
        return a.order - b.order;
      if (b.order != 0)
        return 1;
      return a.order != 0 ? -1 : (int) a.story_id - (int) b.story_id;
    }));
    this.SetPaging();
    this.StartCoroutine("DoInitialize");
  }

  private IEnumerator DoInitialize()
  {
    yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    EventData[] events = new EventData[4]
    {
      new EventData(GameSection.GetGoingHomeEvent(), (object) null),
      new EventData("MAIN_MENU_MENU", (object) null),
      new EventData("THEATERMODE", (object) null),
      new EventData("STORY", (object) new object[2]
      {
        (object) this.m_canViewStoryList,
        (object) this.m_chapterName
      })
    };
    if (this.m_canViewStoryList.Count > 0)
      this.SetActive(((Component) this).gameObject.transform, (Enum) TheaterStory.UI.STR_STORY_NON_LIST, false);
    else
      this.SetActive(((Component) this).gameObject.transform, (Enum) TheaterStory.UI.STR_STORY_NON_LIST, true);
    this.SetActive(((Component) this).gameObject.transform, (Enum) TheaterStory.UI.LBL_CHAPTER_NAME, true);
    this.SetLabelText(((Component) this).gameObject.transform, (Enum) TheaterStory.UI.LBL_CHAPTER_NAME, this.m_chapterName);
    this.SetLabelText((Enum) TheaterStory.UI.LBL_MAX, this.m_pageMax.ToString());
    this.SetLabelText((Enum) TheaterStory.UI.LBL_NOW, this.m_nowPage.ToString());
    List<TheaterModeTable.TheaterModeData> dispList = this.m_canViewStoryList;
    if (this.m_pageMax > 1)
    {
      List<TheaterModeTable.TheaterModeData> theaterModeDataList = new List<TheaterModeTable.TheaterModeData>();
      int index = 0;
      for (int count = dispList.Count; index < count; ++index)
      {
        if (index >= (this.m_nowPage - 1) * 10 && index < this.m_nowPage * 10)
          theaterModeDataList.Add(dispList[index]);
      }
      dispList = theaterModeDataList;
    }
    this.SetDynamicList((Enum) TheaterStory.UI.GRD_LIST, "TheaterStoryListItem", dispList.Count, true, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      this.SetActive(t, (Enum) TheaterStory.UI.LBL_STORY_TITLE, true);
      this.SetLabelText(t, (Enum) TheaterStory.UI.LBL_STORY_TITLE, dispList[i].title);
      this.SetEvent(t, "PLAY_STORY", (object) new object[4]
      {
        (object) dispList[i].script_id,
        (object) 0,
        (object) 0,
        (object) events
      });
    }));
  }

  private void OnApplicationPause(bool pause)
  {
    if (pause)
      return;
    this.RefreshUI();
  }

  private void OnQuery_PLAY_STORY()
  {
  }

  private void OnQuery_PAGE_PREV()
  {
    this.m_nowPage = this.m_nowPage > 1 ? this.m_nowPage - 1 : this.m_pageMax;
    this.RefreshUI();
  }

  private void OnQuery_PAGE_NEXT()
  {
    this.m_nowPage = this.m_nowPage < this.m_pageMax ? this.m_nowPage + 1 : 1;
    this.RefreshUI();
  }

  private void SetPaging()
  {
    this.m_nowPage = 1;
    List<TheaterModeTable.TheaterModeData> canViewStoryList = this.m_canViewStoryList;
    if (canViewStoryList.Count <= 10)
    {
      this.SetActive((Enum) TheaterStory.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) TheaterStory.UI.OBJ_INACTIVE_ROOT, true);
      this.m_pageMax = 1;
    }
    else
    {
      this.SetActive((Enum) TheaterStory.UI.OBJ_ACTIVE_ROOT, true);
      this.SetActive((Enum) TheaterStory.UI.OBJ_INACTIVE_ROOT, false);
      this.m_pageMax = canViewStoryList.Count / 10 + 1;
    }
    this.SetLabelText((Enum) TheaterStory.UI.LBL_MAX, this.m_pageMax.ToString());
    this.SetLabelText((Enum) TheaterStory.UI.LBL_NOW, this.m_nowPage.ToString());
  }

  protected enum UI
  {
    SCR_LIST,
    GRD_LIST,
    STR_STORY_NON_LIST,
    LBL_CHAPTER_NAME,
    LBL_STORY_TITLE,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    LBL_NOW,
    LBL_MAX,
  }
}
