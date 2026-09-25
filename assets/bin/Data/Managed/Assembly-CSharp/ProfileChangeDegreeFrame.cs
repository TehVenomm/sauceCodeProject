// Decompiled with JetBrains decompiler
// Type: ProfileChangeDegreeFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class ProfileChangeDegreeFrame : GameSection
{
  public List<DegreeTable.DegreeData> allData;
  public List<DegreeTable.DegreeData> userHaveData;
  private bool showAll;
  private int currentPage;
  private int maxPage;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    this.allData = Singleton<DegreeTable>.I.GetAll().Where<DegreeTable.DegreeData>((Func<DegreeTable.DegreeData, bool>) (x => x.type == DEGREE_TYPE.FRAME || x.type == DEGREE_TYPE.SPECIAL_FRAME)).ToList<DegreeTable.DegreeData>();
    this.allData.Sort((Comparison<DegreeTable.DegreeData>) ((a, b) => (int) a.id - (int) b.id));
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || this.allData.Count == 0)
    {
      this.userHaveData = new List<DegreeTable.DegreeData>();
      base.Initialize();
    }
    else
    {
      this.userHaveData = this.allData.Where<DegreeTable.DegreeData>((Func<DegreeTable.DegreeData, bool>) (x => x.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))).ToList<DegreeTable.DegreeData>();
      this.showAll = true;
      this.currentPage = 1;
      yield return (object) 0;
      base.Initialize();
    }
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    List<DegreeTable.DegreeData> currentShow = this.showAll ? this.allData : this.userHaveData;
    this.maxPage = currentShow.Count / GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT;
    if (currentShow.Count % GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT > 0)
      ++this.maxPage;
    this.SetGrid((Enum) ProfileChangeDegreeFrame.UI.GRD_FRAME, "DegreePlate", Mathf.Min(GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT, currentShow.Count - (this.currentPage - 1) * GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT), true, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      int index = i + (this.currentPage - 1) * GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT;
      DegreePlate component = ((Component) t).GetComponent<DegreePlate>();
      DegreeTable.DegreeData event_data = currentShow[index];
      this.SetEvent(t, "FRAME_SELECT", (object) event_data);
      if (event_data.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))
      {
        component.SetFrame((int) event_data.id);
        ((Component) component).GetComponent<Collider>().enabled = true;
        ((Component) component).gameObject.AddComponent<UIDragScrollView>();
      }
      else if (event_data.IsSecretName(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))
      {
        component.SetUnknownFrame();
        ((Component) component).GetComponent<Collider>().enabled = false;
      }
      else
      {
        component.SetFrame((int) event_data.id);
        ((Component) component).GetComponent<Collider>().enabled = false;
      }
    }));
    this.SetLabelText((Enum) ProfileChangeDegreeFrame.UI.LBL_SORT, this.showAll ? StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 20U) : StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 21U));
    bool is_visible = this.maxPage > 1;
    this.SetActive((Enum) ProfileChangeDegreeFrame.UI.OBJ_ACTIVE_ARROW_ROOT, is_visible);
    this.SetActive((Enum) ProfileChangeDegreeFrame.UI.OBJ_INACTIVE_ARROW_ROOT, !is_visible);
    this.SetLabelText((Enum) ProfileChangeDegreeFrame.UI.LBL_ARROW_NOW, this.currentPage.ToString());
    this.SetLabelText((Enum) ProfileChangeDegreeFrame.UI.LBL_ARROW_MAX, this.maxPage.ToString());
  }

  private void OnQuery_SORT()
  {
    this.showAll = !this.showAll;
    this.currentPage = 1;
    this.RefreshUI();
  }

  private void OnQuery_PAGE_NEXT()
  {
    ++this.currentPage;
    if (this.currentPage > this.maxPage)
      this.currentPage = 1;
    this.RefreshUI();
  }

  private void OnQuery_PAGE_PREV()
  {
    --this.currentPage;
    if (this.currentPage < 1)
      this.currentPage = this.maxPage;
    this.RefreshUI();
  }

  private void OnQuery_FRAME_SELECT()
  {
    GameSection.SetEventData((object) new ProfileChangeDegreeFrame.ChangeFrame(GameSection.GetEventData() as DegreeTable.DegreeData));
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_DEGREE_FRAME);
    GameSection.BackSection();
  }

  private enum UI
  {
    OBJ_ACTIVE_ARROW_ROOT,
    OBJ_INACTIVE_ARROW_ROOT,
    LBL_ARROW_NOW,
    LBL_ARROW_MAX,
    LBL_SORT,
    GRD_FRAME,
  }

  public class ChangeFrame
  {
    public DegreeTable.DegreeData changeData;

    public ChangeFrame(DegreeTable.DegreeData data) => this.changeData = data;
  }
}
