// Decompiled with JetBrains decompiler
// Type: TheaterMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TheaterMode : GameSection
{
  public const int PAGING = 10;
  private List<TheaterModeTable.TheaterModeData> m_canViewStoryList;
  private List<TheaterModeChapterTable.TheaterModeChapterData> m_canViewChapterList;
  private List<TheaterModeChapterTable.TheaterModeChapterData> m_mainChapterList;
  private List<TheaterModeChapterTable.TheaterModeChapterData> m_eventChapterList;
  private Dictionary<int, LoadObject> bannerTable = new Dictionary<int, LoadObject>(10);
  private Dictionary<int, LoadObject> prevBannerTable = new Dictionary<int, LoadObject>(10);
  private static TheaterModeModel.Param s_connectCache;
  private bool m_isDownloadingBanner;
  private bool m_isSelectMain = true;
  private int m_nowPage = 1;
  private int m_pageMax = 1;
  private bool isRenewalFlag;
  private EventData[] requestEndEventArray;
  private int m_openChapterId = -1;

  public override string overrideBackKeyEvent => "BUTTONBACK";

  protected bool IsDownloadingBanner => this.m_isDownloadingBanner;

  public override void Initialize()
  {
    if (GameSection.GetEventData() is object[] eventData)
    {
      this.m_openChapterId = (int) eventData[0];
      this.requestEndEventArray = eventData[1] as EventData[];
    }
    this.isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
    if (this.isRenewalFlag)
      this.StartCoroutine(this.DoInitialize2());
    else
      this.StartCoroutine("DoInitialize");
  }

  private IEnumerator DoInitialize()
  {
    Utility.CreateGameObjectAndComponent("TheaterModeTable", ((Component) this).gameObject.transform);
    Utility.CreateGameObjectAndComponent("TheaterModeChapterTable", ((Component) this).gameObject.transform);
    while (MonoBehaviourSingleton<TheaterModeChapterTable>.I.isLoading || MonoBehaviourSingleton<TheaterModeTable>.I.isLoading)
      yield return (object) null;
    Dictionary<int, int> checkList = new Dictionary<int, int>();
    Dictionary<int, uint> idList = new Dictionary<int, uint>();
    Dictionary<uint, int> revIdList = new Dictionary<uint, int>();
    Dictionary<uint, int> questList = new Dictionary<uint, int>();
    Dictionary<uint, List<int>> deliveryList = new Dictionary<uint, List<int>>();
    MonoBehaviourSingleton<TheaterModeTable>.I.AllTheaterData((Action<TheaterModeTable.TheaterModeData>) (data =>
    {
      if (checkList.ContainsKey(data.script_id) || idList.ContainsKey(data.script_id))
        return;
      checkList.Add(data.script_id, 0);
      idList.Add(data.script_id, data.story_id);
      revIdList.Add(data.story_id, data.script_id);
    }));
    if (checkList.ContainsKey(11000001))
      checkList[11000001] = 1;
    if (checkList.ContainsKey(11000002))
      checkList[11000002] = 1;
    if (Singleton<QuestTable>.IsValid() && MonoBehaviourSingleton<QuestManager>.IsValid())
      Singleton<QuestTable>.I.AllQuestData((Action<QuestTable.QuestTableData>) (data =>
      {
        if (data.storyId == 0 || !checkList.ContainsKey(data.storyId))
          return;
        if (checkList[data.storyId] == 0)
          checkList[data.storyId] = -1;
        questList.Add(data.questID, data.storyId);
        ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(data.questID);
        if (clearStatusQuestData == null || clearStatusQuestData.questStatus != 3 && clearStatusQuestData.questStatus != 4)
          return;
        checkList[data.storyId] = 1;
      }));
    Singleton<DeliveryTable>.I.AllDeliveryData((Action<DeliveryTable.DeliveryData>) (data =>
    {
      if (data.clearEventID != 0U)
      {
        int clearEventId = (int) data.clearEventID;
        if (checkList.ContainsKey(clearEventId))
        {
          if (checkList[clearEventId] == 0)
            checkList[clearEventId] = -1;
          this.AddDeliveryList(deliveryList, data.id, clearEventId);
          switch (MonoBehaviourSingleton<DeliveryManager>.I.GetClearStatusDelivery(data.id))
          {
            case CLEAR_STATUS.CLEAR:
            case CLEAR_STATUS.ALL_CLEAR:
              checkList[clearEventId] = 1;
              break;
          }
        }
      }
      if (data.readScriptId == 0U)
        return;
      int readScriptId = (int) data.readScriptId;
      if (!checkList.ContainsKey(readScriptId))
        return;
      if (checkList[readScriptId] == 0)
        checkList[readScriptId] = -1;
      this.AddDeliveryList(deliveryList, data.id, readScriptId);
      switch (MonoBehaviourSingleton<DeliveryManager>.I.GetClearStatusDelivery(data.id))
      {
        case CLEAR_STATUS.CLEAR:
        case CLEAR_STATUS.ALL_CLEAR:
          checkList[readScriptId] = 1;
          break;
      }
    }));
    if (TheaterMode.s_connectCache == null)
    {
      TheaterModeModel.RequestSendForm postData = new TheaterModeModel.RequestSendForm();
      foreach (KeyValuePair<uint, List<int>> keyValuePair in deliveryList)
      {
        List<int> intList = keyValuePair.Value;
        if (intList != null && intList.Count >= 1)
        {
          int index = 0;
          for (int count = intList.Count; index < count; ++index)
          {
            int key = intList[index];
            if (checkList[key] < 1)
              postData.theaterList.Add(new TheaterModePostData((int) idList[key], (int) keyValuePair.Key, 0));
          }
        }
      }
      foreach (KeyValuePair<int, int> keyValuePair in checkList)
      {
        int key = keyValuePair.Key;
        if (checkList[key] == 0)
          postData.theaterList.Add(new TheaterModePostData((int) idList[key], 0, key));
      }
      bool isEndConnection = false;
      Protocol.Send<TheaterModeModel.RequestSendForm, TheaterModeModel>(TheaterModeModel.URL, postData, (Action<TheaterModeModel>) (ret =>
      {
        if (ret.Error == Error.None)
        {
          if (ret == null || ret.result == null || ret.result.theaterList == null)
            return;
          TheaterMode.s_connectCache = ret.result;
        }
        isEndConnection = true;
      }));
      while (!isEndConnection)
        yield return (object) null;
    }
    for (int index = 0; index < TheaterMode.s_connectCache.theaterList.Count; ++index)
    {
      if (TheaterMode.s_connectCache.theaterList[index].isOpen)
        checkList[revIdList[(uint) TheaterMode.s_connectCache.theaterList[index].theaterId]] = 1;
      else
        checkList[revIdList[(uint) TheaterMode.s_connectCache.theaterList[index].theaterId]] = -1;
    }
    this.m_canViewStoryList = MonoBehaviourSingleton<TheaterModeTable>.I.GetTableFromOKDic(checkList);
    List<uint> chapter_ids = new List<uint>();
    int index1 = 0;
    for (int count = this.m_canViewStoryList.Count; index1 < count; ++index1)
    {
      uint chapterId = (uint) this.m_canViewStoryList[index1].chapter_id;
      if (!chapter_ids.Contains(chapterId))
        chapter_ids.Add(chapterId);
    }
    this.m_canViewChapterList = MonoBehaviourSingleton<TheaterModeChapterTable>.I.GetPickedData(chapter_ids);
    this.m_canViewChapterList.Sort((Comparison<TheaterModeChapterTable.TheaterModeChapterData>) ((a, b) =>
    {
      if (a.order != 0 && b.order != 0)
        return b.order - a.order;
      if (b.order != 0)
        return -1;
      return a.order != 0 ? 1 : (int) b.chapter_id - (int) a.chapter_id;
    }));
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    this.bannerTable = new Dictionary<int, LoadObject>(this.m_canViewChapterList.Count);
    for (int index2 = 0; index2 < this.m_canViewChapterList.Count; ++index2)
    {
      TheaterModeChapterTable.TheaterModeChapterData canViewChapter = this.m_canViewChapterList[index2];
      if (!this.bannerTable.ContainsKey(canViewChapter.banner_id))
      {
        string eventBanner = ResourceName.GetEventBanner(canViewChapter.banner_id);
        LoadObject loadObject = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_ICON, eventBanner);
        this.bannerTable.Add(canViewChapter.banner_id, loadObject);
      }
    }
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.m_mainChapterList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
    this.m_eventChapterList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
    int index3 = 0;
    for (int count = this.m_canViewChapterList.Count; index3 < count; ++index3)
    {
      if (this.m_canViewChapterList[index3].is_main == 0)
        this.m_eventChapterList.Add(this.m_canViewChapterList[index3]);
      else
        this.m_mainChapterList.Add(this.m_canViewChapterList[index3]);
    }
    this.SetPaging();
    this.m_isSelectMain = false;
    this.OnQuery_SELECT_MAIN();
    base.Initialize();
    if (this.requestEndEventArray != null)
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(this.requestEndEventArray);
  }

  public override void UpdateUI()
  {
    List<TheaterModeChapterTable.TheaterModeChapterData> dispList = !this.m_isSelectMain ? this.m_eventChapterList : this.m_mainChapterList;
    if (this.m_pageMax > 1)
    {
      List<TheaterModeChapterTable.TheaterModeChapterData> theaterModeChapterDataList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
      int index = 0;
      for (int count = dispList.Count; index < count; ++index)
      {
        if (index >= (this.m_nowPage - 1) * 10 && index < this.m_nowPage * 10)
          theaterModeChapterDataList.Add(dispList[index]);
      }
      dispList = theaterModeChapterDataList;
    }
    if (dispList == null || dispList.Count == 0)
      this.SetActive((Enum) TheaterMode.UI.STR_EVENT_NON_LIST, true);
    else
      this.SetActive((Enum) TheaterMode.UI.STR_EVENT_NON_LIST, false);
    this.SetLabelText((Enum) TheaterMode.UI.LBL_MAX, this.m_pageMax.ToString());
    this.SetLabelText((Enum) TheaterMode.UI.LBL_NOW, this.m_nowPage.ToString());
    this.SetDynamicList((Enum) TheaterMode.UI.GRD_EVENT_QUEST, "TheaterModeListItem", dispList.Count, true, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      if (this.m_isSelectMain)
      {
        this.SetActive(t, (Enum) TheaterMode.UI.MAIN_BG, true);
        this.SetActive(t, (Enum) TheaterMode.UI.LBL_CHAPTER_NAME, true);
        this.SetActive(t, (Enum) TheaterMode.UI.TEX_EVENT_BANNER, false);
        this.SetLabelText(t, (Enum) TheaterMode.UI.LBL_CHAPTER_NAME, dispList[i].chapter_name);
      }
      else
      {
        bool flag = false;
        LoadObject loadObject;
        if ((!this.IsDownloadingBanner ? this.bannerTable : this.prevBannerTable).TryGetValue(dispList[i].banner_id, out loadObject))
        {
          Texture2D loadedObject = loadObject.loadedObject as Texture2D;
          if (Object.op_Inequality((Object) loadedObject, (Object) null))
          {
            Transform ctrl = this.FindCtrl(t, (Enum) TheaterMode.UI.TEX_EVENT_BANNER);
            this.SetActive(ctrl, true);
            this.SetTexture(ctrl, (Texture) loadedObject);
            this.SetActive(t, (Enum) TheaterMode.UI.LBL_CHAPTER_NAME, false);
            this.SetActive(t, (Enum) TheaterMode.UI.MAIN_BG, false);
            flag = true;
          }
        }
        if (!flag)
        {
          this.SetActive(t, (Enum) TheaterMode.UI.MAIN_BG, true);
          this.SetActive(t, (Enum) TheaterMode.UI.LBL_CHAPTER_NAME, true);
          this.SetActive(t, (Enum) TheaterMode.UI.TEX_EVENT_BANNER, false);
          this.SetLabelText(t, (Enum) TheaterMode.UI.LBL_CHAPTER_NAME, dispList[i].chapter_name);
        }
      }
      this.SetEvent(t, "STORY", (object) new object[2]
      {
        (object) this.GetCanViewChapterList((int) dispList[i].chapter_id),
        (object) dispList[i].chapter_name
      });
    }));
  }

  private void OnApplicationPause(bool pause)
  {
    if (pause)
      return;
    this.RefreshUI();
  }

  private List<TheaterModeTable.TheaterModeData> GetCanViewChapterList(int chapter_id)
  {
    List<TheaterModeTable.TheaterModeData> canViewChapterList = new List<TheaterModeTable.TheaterModeData>();
    int index = 0;
    for (int count = this.m_canViewStoryList.Count; index < count; ++index)
    {
      if (this.m_canViewStoryList[index].chapter_id == chapter_id)
        canViewChapterList.Add(this.m_canViewStoryList[index]);
    }
    return canViewChapterList;
  }

  private void OnQuery_SELECT_MAIN()
  {
    if (this.m_isSelectMain)
      return;
    this.m_isSelectMain = true;
    this.SetActive((Enum) TheaterMode.UI.BTN_TAB_MAIN_OFF, false);
    this.SetActive((Enum) TheaterMode.UI.BTN_TAB_MAIN_ON, true);
    this.SetActive((Enum) TheaterMode.UI.BTN_TAB_EVENT_OFF, true);
    this.SetActive((Enum) TheaterMode.UI.BTN_TAB_EVENT_ON, false);
    this.SetActive((Enum) TheaterMode.UI.SPR_TAB_EVENT, false);
    this.SetActive((Enum) TheaterMode.UI.SPR_TAB_MAIN, true);
    this.SetPaging();
    this.RefreshUI();
  }

  private void OnQuery_SELECT_EVENT()
  {
    if (!this.m_isSelectMain)
      return;
    this.m_isSelectMain = false;
    this.SetActive((Enum) TheaterMode.UI.BTN_TAB_MAIN_OFF, true);
    this.SetActive((Enum) TheaterMode.UI.BTN_TAB_MAIN_ON, false);
    this.SetActive((Enum) TheaterMode.UI.BTN_TAB_EVENT_OFF, false);
    this.SetActive((Enum) TheaterMode.UI.BTN_TAB_EVENT_ON, true);
    this.SetActive((Enum) TheaterMode.UI.SPR_TAB_EVENT, true);
    this.SetActive((Enum) TheaterMode.UI.SPR_TAB_MAIN, false);
    this.SetPaging();
    if (this.isRenewalFlag)
    {
      if (this.IsDownloadingBanner)
        return;
      this.StartCoroutine(this.LoadBannerImage(this.m_nowPage, (System.Action) (() =>
      {
        this.RefreshUI();
        this.ReleasePrevBannerImage();
      })));
    }
    else
      this.RefreshUI();
  }

  private void OnQuery_PAGE_PREV()
  {
    if (this.isRenewalFlag)
    {
      if (this.IsDownloadingBanner)
        return;
      this.m_nowPage = this.m_nowPage > 1 ? this.m_nowPage - 1 : this.m_pageMax;
      this.StartCoroutine(this.LoadBannerImage(this.m_nowPage, (System.Action) (() =>
      {
        this.RefreshUI();
        this.ReleasePrevBannerImage();
      })));
    }
    else
    {
      this.m_nowPage = this.m_nowPage > 1 ? this.m_nowPage - 1 : this.m_pageMax;
      this.RefreshUI();
    }
  }

  private void OnQuery_PAGE_NEXT()
  {
    if (this.isRenewalFlag)
    {
      if (this.IsDownloadingBanner)
        return;
      this.m_nowPage = this.m_nowPage < this.m_pageMax ? this.m_nowPage + 1 : 1;
      this.StartCoroutine(this.LoadBannerImage(this.m_nowPage, (System.Action) (() =>
      {
        this.RefreshUI();
        this.ReleasePrevBannerImage();
      })));
    }
    else
    {
      this.m_nowPage = this.m_nowPage < this.m_pageMax ? this.m_nowPage + 1 : 1;
      this.RefreshUI();
    }
  }

  private void SetPaging()
  {
    this.m_nowPage = 1;
    List<TheaterModeChapterTable.TheaterModeChapterData> theaterModeChapterDataList = !this.m_isSelectMain ? this.m_eventChapterList : this.m_mainChapterList;
    if (theaterModeChapterDataList.Count <= 10)
    {
      this.SetActive((Enum) TheaterMode.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) TheaterMode.UI.OBJ_INACTIVE_ROOT, true);
      this.m_pageMax = 1;
    }
    else
    {
      this.SetActive((Enum) TheaterMode.UI.OBJ_ACTIVE_ROOT, true);
      this.SetActive((Enum) TheaterMode.UI.OBJ_INACTIVE_ROOT, false);
      this.m_pageMax = Mathf.CeilToInt((float) theaterModeChapterDataList.Count / 10f);
    }
    this.SetLabelText((Enum) TheaterMode.UI.LBL_MAX, this.m_pageMax.ToString());
    this.SetLabelText((Enum) TheaterMode.UI.LBL_NOW, this.m_nowPage.ToString());
  }

  private void OnQuery_STORY()
  {
  }

  private void OnQuery_BUTTONBACK()
  {
    TheaterMode.s_connectCache = (TheaterModeModel.Param) null;
    GameSection.ChangeEvent("[BACK]");
  }

  private IEnumerator DoInitialize2()
  {
    yield return (object) this.InitTables();
    this.InitChapterTables();
    yield return (object) this.StartCoroutine(this.LoadBannerImage(1, (System.Action) null));
    this.SetPaging();
    this.m_isSelectMain = false;
    this.OnQuery_SELECT_MAIN();
    base.Initialize();
    if (this.requestEndEventArray != null)
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(this.requestEndEventArray);
  }

  private IEnumerator InitTables()
  {
    Utility.CreateGameObjectAndComponent("TheaterModeTable", ((Component) this).gameObject.transform);
    Utility.CreateGameObjectAndComponent("TheaterModeChapterTable", ((Component) this).gameObject.transform);
    while (MonoBehaviourSingleton<TheaterModeChapterTable>.I.isLoading || MonoBehaviourSingleton<TheaterModeTable>.I.isLoading)
      yield return (object) null;
    Dictionary<int, int> checkList = new Dictionary<int, int>();
    Dictionary<int, uint> idList = new Dictionary<int, uint>();
    Dictionary<uint, int> revIdList = new Dictionary<uint, int>();
    Dictionary<uint, int> _questList = new Dictionary<uint, int>();
    Dictionary<uint, List<int>> _deliveryList = new Dictionary<uint, List<int>>();
    Dictionary<int, int> stateList = new Dictionary<int, int>();
    MonoBehaviourSingleton<TheaterModeTable>.I.AllTheaterDataDesc((Action<TheaterModeTable.TheaterModeData>) (data =>
    {
      if (checkList.ContainsKey(data.script_id) || idList.ContainsKey(data.script_id))
        return;
      checkList.Add(data.script_id, 0);
      stateList.Add(data.script_id, data.state_id);
      idList.Add(data.script_id, data.story_id);
      revIdList.Add(data.story_id, data.script_id);
    }));
    this.SetTutorialStoryForceEnableView(checkList);
    this.SetQuestStorySetting(checkList, stateList, _questList);
    this.SetDeliveryStorySetting(checkList, stateList, _deliveryList);
    yield return (object) this.StartCoroutine(this.RequestPrologueStoryStatusAPI(checkList, _deliveryList, idList));
    this.CheckPrologueStorySettings(checkList, revIdList);
    this.m_canViewStoryList = MonoBehaviourSingleton<TheaterModeTable>.I.GetTableFromOKDic(checkList);
  }

  private void InitChapterTables()
  {
    List<uint> chapterList = new List<uint>();
    int index1 = 0;
    for (int count = this.m_canViewStoryList.Count; index1 < count; ++index1)
    {
      uint chapterId = (uint) this.m_canViewStoryList[index1].chapter_id;
      if (!chapterList.Contains(chapterId))
        chapterList.Add(chapterId);
    }
    this.m_canViewChapterList = this.GetSortedEnableViewList(chapterList);
    this.m_mainChapterList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
    this.m_eventChapterList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
    int index2 = 0;
    for (int count = this.m_canViewChapterList.Count; index2 < count; ++index2)
    {
      if (this.m_canViewChapterList[index2].is_main == 0)
        this.m_eventChapterList.Add(this.m_canViewChapterList[index2]);
      else
        this.m_mainChapterList.Add(this.m_canViewChapterList[index2]);
    }
  }

  private void SetTutorialStoryForceEnableView(Dictionary<int, int> _checkList)
  {
    if (_checkList == null)
      return;
    if (_checkList.ContainsKey(11000001))
      _checkList[11000001] = 1;
    if (!_checkList.ContainsKey(11000002))
      return;
    _checkList[11000002] = 1;
  }

  private void SetQuestStorySetting(
    Dictionary<int, int> _checkList,
    Dictionary<int, int> _stateList,
    Dictionary<uint, int> _questList)
  {
    if (_checkList == null || _questList == null || !Singleton<QuestTable>.IsValid() || !MonoBehaviourSingleton<QuestManager>.IsValid())
      return;
    QuestManager questMgr = MonoBehaviourSingleton<QuestManager>.I;
    int stateId = 0;
    bool isMainStory = false;
    Singleton<QuestTable>.I.AllQuestDataAsc((Action<QuestTable.QuestTableData>) (data =>
    {
      isMainStory = false;
      int storyId = data.storyId;
      if (storyId == 0 || !_checkList.ContainsKey(storyId))
        return;
      if (_stateList.TryGetValue(storyId, out stateId))
      {
        if (stateId == 0)
        {
          _checkList[storyId] = -1;
          return;
        }
        isMainStory = stateId == 1;
      }
      if (isMainStory && _checkList[storyId] == 1)
        return;
      if (_checkList[storyId] == 0)
        _checkList[storyId] = -1;
      if (questMgr.IsFutureEvent(data.eventId))
        _checkList[storyId] = -1;
      else if (questMgr.IsEventOpen(data.eventId) | isMainStory)
      {
        _questList.Add(data.questID, storyId);
        if (questMgr.IsClearQuest(data.questID))
          _checkList[storyId] = 1;
        else
          _checkList[storyId] = -1;
      }
      else
      {
        _questList.Add(data.questID, storyId);
        _checkList[storyId] = 1;
      }
    }));
  }

  private void SetDeliveryStorySetting(
    Dictionary<int, int> _checkList,
    Dictionary<int, int> _stateList,
    Dictionary<uint, List<int>> _deliveryList)
  {
    if (!Singleton<DeliveryTable>.IsValid())
      return;
    Singleton<DeliveryTable>.I.AllDeliveryDataAsc((Action<DeliveryTable.DeliveryData>) (data =>
    {
      this.SetIndividualDeliveryStorySetting(_checkList, _stateList, _deliveryList, data.id, data.eventID, (int) data.readScriptId);
      this.SetIndividualDeliveryStorySetting(_checkList, _stateList, _deliveryList, data.id, data.eventID, (int) data.clearEventID);
    }));
  }

  private bool SetIndividualDeliveryStorySetting(
    Dictionary<int, int> _checkList,
    Dictionary<int, int> _stateList,
    Dictionary<uint, List<int>> _deliveryList,
    uint _deliveryId,
    int _eventId,
    int storyId)
  {
    if (_checkList == null || _deliveryList == null || _stateList == null || !MonoBehaviourSingleton<DeliveryManager>.IsValid() || !MonoBehaviourSingleton<QuestManager>.IsValid())
      return false;
    QuestManager i1 = MonoBehaviourSingleton<QuestManager>.I;
    DeliveryManager i2 = MonoBehaviourSingleton<DeliveryManager>.I;
    int num = 0;
    bool flag = false;
    if (_stateList.TryGetValue(storyId, out num))
    {
      flag = num == 1;
      if (num == 0)
      {
        _checkList[storyId] = -1;
        return true;
      }
    }
    if (i1.IsFutureEvent(_eventId))
    {
      if (storyId != 0 && _checkList.ContainsKey(storyId))
      {
        _checkList[storyId] = -1;
        this.AddDeliveryList(_deliveryList, _deliveryId, storyId);
      }
      return true;
    }
    if (flag && _checkList[storyId] == 1)
      return true;
    if (i1.IsEventOpen(_eventId) | flag)
    {
      if (storyId != 0 && _checkList.ContainsKey(storyId))
      {
        if (_checkList[storyId] == 0)
          _checkList[storyId] = -1;
        this.AddDeliveryList(_deliveryList, _deliveryId, storyId);
        _checkList[storyId] = !i2.IsClearDelivery(_deliveryId) ? -1 : 1;
      }
    }
    else if (_checkList.ContainsKey(storyId))
    {
      _checkList[storyId] = 1;
      this.AddDeliveryList(_deliveryList, _deliveryId, storyId);
    }
    return true;
  }

  private IEnumerator RequestPrologueStoryStatusAPI(
    Dictionary<int, int> _checkList,
    Dictionary<uint, List<int>> _deliveryList,
    Dictionary<int, uint> _idList)
  {
    if (TheaterMode.s_connectCache == null)
    {
      TheaterModeModel.RequestSendForm postData = new TheaterModeModel.RequestSendForm();
      foreach (KeyValuePair<uint, List<int>> delivery in _deliveryList)
      {
        List<int> intList = delivery.Value;
        if (intList != null && intList.Count >= 1)
        {
          int index = 0;
          for (int count = intList.Count; index < count; ++index)
          {
            int key = intList[index];
            if (_checkList[key] == 0)
              postData.theaterList.Add(new TheaterModePostData((int) _idList[key], (int) delivery.Key, 0));
          }
        }
      }
      foreach (KeyValuePair<int, int> check in _checkList)
      {
        int key = check.Key;
        if (_checkList[key] == 0)
          postData.theaterList.Add(new TheaterModePostData((int) _idList[key], 0, key));
      }
      bool isEndConnection = false;
      Protocol.Send<TheaterModeModel.RequestSendForm, TheaterModeModel>(TheaterModeModel.URL, postData, (Action<TheaterModeModel>) (ret =>
      {
        if (ret.Error == Error.None)
        {
          if (ret == null || ret.result == null || ret.result.theaterList == null)
            return;
          TheaterMode.s_connectCache = ret.result;
        }
        isEndConnection = true;
      }));
      while (!isEndConnection)
        yield return (object) null;
    }
  }

  private void CheckPrologueStorySettings(
    Dictionary<int, int> _checkList,
    Dictionary<uint, int> _revIdList)
  {
    if (!MonoBehaviourSingleton<QuestManager>.IsValid())
      return;
    int index = 0;
    for (int count = TheaterMode.s_connectCache.theaterList.Count; index < count; ++index)
    {
      TheaterModeGetData theater = TheaterMode.s_connectCache.theaterList[index];
      if (theater.isOpen)
        _checkList[_revIdList[(uint) theater.theaterId]] = 1;
      else
        _checkList[_revIdList[(uint) theater.theaterId]] = -1;
    }
  }

  private List<TheaterModeChapterTable.TheaterModeChapterData> GetSortedEnableViewList(
    List<uint> chapterList)
  {
    List<TheaterModeChapterTable.TheaterModeChapterData> sortedEnableViewList = new List<TheaterModeChapterTable.TheaterModeChapterData>();
    if (!MonoBehaviourSingleton<TheaterModeChapterTable>.IsValid() || chapterList == null || chapterList.Count < 1)
      return sortedEnableViewList;
    List<TheaterModeChapterTable.TheaterModeChapterData> pickedData = MonoBehaviourSingleton<TheaterModeChapterTable>.I.GetPickedData(chapterList);
    pickedData.Sort((Comparison<TheaterModeChapterTable.TheaterModeChapterData>) ((a, b) =>
    {
      if (a.order != 0 && b.order != 0)
        return b.order - a.order;
      if (b.order != 0)
        return -1;
      return a.order != 0 ? 1 : (int) b.chapter_id - (int) a.chapter_id;
    }));
    return pickedData;
  }

  private IEnumerator LoadBannerImage(int _nextVisiblePageNum, System.Action _onCompleteCallback)
  {
    this.m_isDownloadingBanner = true;
    if (this.m_canViewChapterList == null || _nextVisiblePageNum < 1)
    {
      this.m_isDownloadingBanner = false;
      if (_onCompleteCallback != null)
        _onCompleteCallback();
    }
    else
    {
      int num1 = (_nextVisiblePageNum - 1) * 10;
      int num2 = num1 + 10 > this.m_canViewChapterList.Count ? this.m_canViewChapterList.Count : num1 + 10;
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      this.prevBannerTable = new Dictionary<int, LoadObject>((IDictionary<int, LoadObject>) this.bannerTable);
      this.bannerTable.Clear();
      for (int index = num1; index < num2; ++index)
      {
        TheaterModeChapterTable.TheaterModeChapterData canViewChapter = this.m_canViewChapterList[index];
        if (!this.bannerTable.ContainsKey(canViewChapter.banner_id))
        {
          string eventBanner = ResourceName.GetEventBanner(canViewChapter.banner_id);
          LoadObject loadObject = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_ICON, eventBanner);
          this.bannerTable.Add(canViewChapter.banner_id, loadObject);
        }
      }
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.m_isDownloadingBanner = false;
      if (_onCompleteCallback != null)
        _onCompleteCallback();
    }
  }

  private void ReleasePrevBannerImage()
  {
    if (this.prevBannerTable.Count < 1)
      return;
    foreach (KeyValuePair<int, LoadObject> keyValuePair in this.prevBannerTable)
      keyValuePair.Value.ReleaseAllResources();
    this.prevBannerTable.Clear();
  }

  private void AddDeliveryList(
    Dictionary<uint, List<int>> _deliveryList,
    uint _addKey,
    int _addValue)
  {
    List<int> intList = (List<int>) null;
    if (_deliveryList.TryGetValue(_addKey, out intList))
    {
      if (intList.Contains(_addValue))
        return;
      intList.Add(_addValue);
    }
    else
      _deliveryList.Add(_addKey, new List<int>()
      {
        _addValue
      });
  }

  private void OnQuery_SELECT_CHAPTER_FROM_OUTER()
  {
    if (this.m_openChapterId < 0)
    {
      GameSection.ChangeEvent("BACK");
    }
    else
    {
      TheaterModeChapterTable.TheaterModeChapterData chapterData = this.GetChapterData(this.m_openChapterId);
      if (chapterData == null)
      {
        GameSection.ChangeEvent("BACK");
      }
      else
      {
        GameSection.SetEventData((object) new object[2]
        {
          (object) this.GetCanViewChapterList((int) chapterData.chapter_id),
          (object) chapterData.chapter_name
        });
        GameSection.ChangeEvent("STORY");
      }
    }
  }

  private TheaterModeChapterTable.TheaterModeChapterData GetChapterData(int _chapterId)
  {
    if (_chapterId < 0)
      return (TheaterModeChapterTable.TheaterModeChapterData) null;
    TheaterModeChapterTable.TheaterModeChapterData chapterData = (TheaterModeChapterTable.TheaterModeChapterData) null;
    int index = 0;
    for (int count = this.m_canViewChapterList.Count; index < count; ++index)
    {
      if ((int) this.m_canViewChapterList[index].chapter_id == _chapterId)
      {
        chapterData = this.m_canViewChapterList[index];
        break;
      }
    }
    return chapterData;
  }

  protected enum UI
  {
    SCR_EVENT_QUEST,
    GRD_EVENT_QUEST,
    TEX_EVENT_BANNER,
    LBL_CHAPTER_NAME,
    MAIN_BG,
    STR_EVENT_NON_LIST,
    BTN_EVENT,
    OBJ_FRAME,
    SPR_BG_FRAME,
    SPR_CLEARED,
    SPR_NEW,
    BTN_TAB_MAIN_ON,
    BTN_TAB_MAIN_OFF,
    BTN_TAB_EVENT_ON,
    BTN_TAB_EVENT_OFF,
    SPR_TAB_MAIN,
    SPR_TAB_EVENT,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    LBL_NOW,
    LBL_MAX,
  }

  private enum CHECK_VALUE
  {
    FALSE = -1, // 0xFFFFFFFF
    UNKNOWN = 0,
    TRUE = 1,
    MAX = 2,
  }
}
