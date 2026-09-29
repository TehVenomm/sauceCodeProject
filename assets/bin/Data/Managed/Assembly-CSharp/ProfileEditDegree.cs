// Decompiled with JetBrains decompiler
// Type: ProfileEditDegree
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class ProfileEditDegree : GameSection
{
  public readonly string[] WORD_LIST_SPRITE_NAME = new string[3]
  {
    "Honor_SelectBtn_Normal",
    "Honor_SelectBtn_Select",
    "Honor_SelectBtn_Close"
  };
  public readonly string[] TAB_SPRITE_NAME = new string[3]
  {
    "Honor_TabBtn_Normal",
    "Honor_TabBtn_Select",
    "Honor_TabBtn_Unselect"
  };
  private List<int> currentDegrees;
  private DegreePlate currentPlate;
  private bool showAll;
  private Vector3 arrowLocalPos;
  private Vector3 arrowlocalScale;
  private Transform arrowTrans;
  private List<DegreeTable.DegreeData> allNounData;
  private List<DegreeTable.DegreeData> allConData;
  private List<DegreeTable.DegreeData> userHaveFrameData;
  private List<DegreeTable.DegreeData> userHaveNounData;
  private List<DegreeTable.DegreeData> userHaveConData;
  private List<DegreeTable.DegreeData> currentShowData;
  private DegreeTable.DegreeData currentSelectData;
  private int currentPage;
  private int maxPage;
  private ProfileEditDegree.WORD_TAB currentTab;
  private Color[] selectColors = new Color[2];
  private Color[] normalColors = new Color[2];

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "DegreeTable";
    }
  }

  public override void Initialize()
  {
    this.currentDegrees = MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds.ToList<int>();
    this.showAll = true;
    this.currentTab = ProfileEditDegree.WORD_TAB.PREFIX;
    this.SpoileColor();
    this.currentSelectData = Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[0]).type == DEGREE_TYPE.SPECIAL_FRAME ? (DegreeTable.DegreeData) null : Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[1]);
    List<DegreeTable.DegreeData> all = Singleton<DegreeTable>.I.GetAll();
    all.Sort((Comparison<DegreeTable.DegreeData>) ((a, b) => (int) a.id - (int) b.id));
    this.allNounData = all.Where<DegreeTable.DegreeData>((Func<DegreeTable.DegreeData, bool>) (x => x.type == DEGREE_TYPE.NOUN)).ToList<DegreeTable.DegreeData>();
    this.allConData = all.Where<DegreeTable.DegreeData>((Func<DegreeTable.DegreeData, bool>) (x => x.type == DEGREE_TYPE.CONJUNCTION)).ToList<DegreeTable.DegreeData>();
    this.userHaveNounData = this.allNounData.Where<DegreeTable.DegreeData>((Func<DegreeTable.DegreeData, bool>) (x => x.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))).ToList<DegreeTable.DegreeData>();
    this.userHaveConData = this.allConData.Where<DegreeTable.DegreeData>((Func<DegreeTable.DegreeData, bool>) (x => x.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))).ToList<DegreeTable.DegreeData>();
    this.userHaveFrameData = all.Where<DegreeTable.DegreeData>((Func<DegreeTable.DegreeData, bool>) (x => x.type == DEGREE_TYPE.FRAME)).Where<DegreeTable.DegreeData>((Func<DegreeTable.DegreeData, bool>) (x => x.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))).ToList<DegreeTable.DegreeData>();
    this.currentPage = 1;
    this.currentPlate = ((Component) this.GetCtrl((Enum) ProfileEditDegree.UI.OBJ_DEGREE_PLATE_ROOT)).GetComponent<DegreePlate>();
    this.arrowTrans = this.GetCtrl((Enum) ProfileEditDegree.UI.SPR_TAB_SELECTING);
    this.arrowLocalPos = this.arrowTrans.localPosition;
    this.arrowlocalScale = this.arrowTrans.localScale;
    base.Initialize();
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_DEGREE_FRAME;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if (flags == GameSection.NOTIFY_FLAG.UPDATE_DEGREE_FRAME && GameSection.GetEventData() is ProfileChangeDegreeFrame.ChangeFrame eventData)
    {
      this.currentDegrees[0] = (int) eventData.changeData.id;
      this.currentSelectData = eventData.changeData.type != DEGREE_TYPE.SPECIAL_FRAME ? Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[(int) this.currentTab]) : (DegreeTable.DegreeData) null;
    }
    base.OnNotify(flags);
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.currentShowData = this.currentTab != ProfileEditDegree.WORD_TAB.CONJUNCTION ? (this.showAll ? this.allNounData : this.userHaveNounData) : (this.showAll ? this.allConData : this.userHaveConData);
    this.maxPage = this.currentShowData.Count / GameDefine.DEGREE_WORD_CHANGE_LIST_COUNT;
    if (this.currentShowData.Count % GameDefine.DEGREE_FRAME_CHANGE_LIST_COUNT > 0)
      ++this.maxPage;
    DegreeTable.DegreeData data = Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[0]);
    if (data.type == DEGREE_TYPE.SPECIAL_FRAME)
    {
      this.maxPage = 1;
      this.currentPage = 1;
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_PREFIX, "");
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_CONJUNCTION, "");
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_SUFFIX, "");
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_PAGE_MAX, this.maxPage.ToString());
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_PAGE_NOW, this.currentPage.ToString());
      this.SetActive((Enum) ProfileEditDegree.UI.OBJ_ARROW_ACTIVE_ROOT, false);
      this.SetActive((Enum) ProfileEditDegree.UI.OBJ_ARROW_INACTIVE_ROOT, true);
      this.SetActive((Enum) ProfileEditDegree.UI.LBL_NO_SELECTABLE_FRAME, true);
      this.currentPlate.Initialize(this.currentDegrees, false, (Action<DegreePlate>) (x => { }));
      this.SetGrid((Enum) ProfileEditDegree.UI.GRD_WORD_LIST, "DegreeWordList", 0, false, (Action<int, Transform, bool>) ((i, t, b) => { }));
    }
    else
    {
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_PREFIX, Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[1]).name);
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_CONJUNCTION, Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[2]).name);
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_SUFFIX, Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[3]).name);
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_SORT, this.showAll ? StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 20U) : StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 21U));
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_PAGE_MAX, this.maxPage.ToString());
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_PAGE_NOW, this.currentPage.ToString());
      this.SetActive((Enum) ProfileEditDegree.UI.OBJ_ARROW_ACTIVE_ROOT, this.maxPage > 1);
      this.SetActive((Enum) ProfileEditDegree.UI.OBJ_ARROW_INACTIVE_ROOT, this.maxPage == 1);
      this.SetActive((Enum) ProfileEditDegree.UI.LBL_NO_SELECTABLE_FRAME, false);
      this.currentPlate.Initialize(this.currentDegrees, false, (Action<DegreePlate>) (x => { }));
      this.SetGrid((Enum) ProfileEditDegree.UI.GRD_WORD_LIST, "DegreeWordList", Mathf.Min(GameDefine.DEGREE_WORD_CHANGE_LIST_COUNT, this.currentShowData.Count - (this.currentPage - 1) * GameDefine.DEGREE_WORD_CHANGE_LIST_COUNT), false, (Action<int, Transform, bool>) ((i, t, b) =>
      {
        ((Component) t).gameObject.AddComponent<UIDragScrollView>();
        DegreeTable.DegreeData event_data = this.currentShowData[i + (this.currentPage - 1) * GameDefine.DEGREE_WORD_CHANGE_LIST_COUNT];
        this.SetEvent(t, "SELECT", (object) event_data);
        if (event_data.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))
          this.SetButtonSprite(t, (long) this.currentDegrees[(int) this.currentTab] == (long) event_data.id ? this.WORD_LIST_SPRITE_NAME[1] : this.WORD_LIST_SPRITE_NAME[0]);
        else
          this.SetButtonSprite(t, this.WORD_LIST_SPRITE_NAME[2]);
        if (!event_data.IsSecretName(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))
        {
          this.SetLabelText(t, (Enum) ProfileEditDegree.UI.LBL_WORD_NORMAL, event_data.name);
          this.SetLabelText(t, (Enum) ProfileEditDegree.UI.LBL_WORD_SELECTED, event_data.name);
          this.SetActive(t, (Enum) ProfileEditDegree.UI.LBL_WORD_NORMAL, this.currentDegrees[(int) this.currentTab] != (int) event_data.id);
          this.SetActive(t, (Enum) ProfileEditDegree.UI.LBL_WORD_SELECTED, this.currentDegrees[(int) this.currentTab] == (int) event_data.id);
          this.SetActive(t, (Enum) ProfileEditDegree.UI.LBL_WORD_UNKNOWN, false);
        }
        else
        {
          this.SetActive(t, (Enum) ProfileEditDegree.UI.LBL_WORD_NORMAL, false);
          this.SetActive(t, (Enum) ProfileEditDegree.UI.LBL_WORD_SELECTED, false);
          this.SetActive(t, (Enum) ProfileEditDegree.UI.LBL_WORD_UNKNOWN, true);
        }
        this.SetActive(t, (Enum) ProfileEditDegree.UI.SPR_WORD_SELECTED, event_data == this.currentSelectData);
      }));
    }
    this.SetTab(this.currentTab, data.type);
    this.SetDegreeDetail();
  }

  public void OnQuery_SELECT()
  {
    this.currentSelectData = GameSection.GetEventData() as DegreeTable.DegreeData;
    if (this.currentSelectData.IsUnlcok(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds))
      this.currentDegrees[(int) this.currentTab] = (int) this.currentSelectData.id;
    this.RefreshUI();
  }

  public void OnQuery_SORT()
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

  public void OnQuery_OMAKASE()
  {
    this.currentDegrees[0] = (int) this.userHaveFrameData[Random.Range(0, this.userHaveFrameData.Count)].id;
    this.currentDegrees[1] = (int) this.userHaveNounData[Random.Range(0, this.userHaveNounData.Count)].id;
    this.currentDegrees[2] = (int) this.userHaveConData[Random.Range(0, this.userHaveConData.Count)].id;
    this.currentDegrees[3] = (int) this.userHaveNounData[Random.Range(0, this.userHaveNounData.Count)].id;
    this.currentTab = ProfileEditDegree.WORD_TAB.PREFIX;
    this.currentPage = 1;
    this.currentSelectData = Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[1]);
    this.RefreshUI();
  }

  public void OnQuery_ON_PREFIX()
  {
    this.currentTab = ProfileEditDegree.WORD_TAB.PREFIX;
    this.currentPage = 1;
    this.currentSelectData = Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[1]);
    this.RefreshUI();
  }

  public void OnQuery_ON_CONJUNCTION()
  {
    this.currentTab = ProfileEditDegree.WORD_TAB.CONJUNCTION;
    this.currentPage = 1;
    this.currentSelectData = Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[2]);
    this.RefreshUI();
  }

  public void OnQuery_ON_SUFFIX()
  {
    this.currentTab = ProfileEditDegree.WORD_TAB.SUFFIX;
    this.currentPage = 1;
    this.currentSelectData = Singleton<DegreeTable>.I.GetData((uint) this.currentDegrees[3]);
    this.RefreshUI();
  }

  public void OnQuery_CONFIRM_CHANGE()
  {
    bool flag = false;
    for (int index = 0; index < this.currentDegrees.Count; ++index)
    {
      if (MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds[index] != this.currentDegrees[index])
      {
        flag = true;
        break;
      }
    }
    if (flag)
      return;
    GameSection.StopEvent();
    GameSection.BackSection();
  }

  public void OnQuery_ProfileChangeDegreeConfirmDialog_YES()
  {
    GameSection.StayEvent();
    Protocol.Send<DegreeEquipModel.RequestSendForm, DegreeEquipModel>(DegreeEquipModel.URL, new DegreeEquipModel.RequestSendForm()
    {
      degid0 = this.currentDegrees[0].ToString(),
      degid1 = this.currentDegrees[1].ToString(),
      degid2 = this.currentDegrees[2].ToString(),
      degid3 = this.currentDegrees[3].ToString()
    }, (Action<DegreeEquipModel>) (x =>
    {
      GameSection.ResumeEvent(x.Error == Error.None);
      if (x.Error != Error.None)
        return;
      this.RequestEvent("[BACK]");
    }));
  }

  private void SetDegreeDetail()
  {
    if (this.currentSelectData == null)
    {
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_SELECTED_DEGREE_NAME, "---");
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_SELECTED_DEGREE_REQUIREMENT, "---");
    }
    else
    {
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_SELECTED_DEGREE_NAME, this.currentSelectData.IsSecretName(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds) ? "???" : this.currentSelectData.name);
      this.SetLabelText((Enum) ProfileEditDegree.UI.LBL_SELECTED_DEGREE_REQUIREMENT, this.currentSelectData.IsSecretText(MonoBehaviourSingleton<UserInfoManager>.I.unlockedDegreeIds) ? "???" : this.currentSelectData.requirementText);
    }
  }

  private void SetTab(ProfileEditDegree.WORD_TAB selectTab, DEGREE_TYPE frameType)
  {
    if (frameType != DEGREE_TYPE.SPECIAL_FRAME)
    {
      this.SetButtonSprite((Enum) ProfileEditDegree.UI.BTN_PREFIX, this.TAB_SPRITE_NAME[selectTab == ProfileEditDegree.WORD_TAB.PREFIX ? 1 : 0]);
      this.SetButtonSprite((Enum) ProfileEditDegree.UI.BTN_CONJUNCTION, this.TAB_SPRITE_NAME[selectTab == ProfileEditDegree.WORD_TAB.CONJUNCTION ? 1 : 0]);
      this.SetButtonSprite((Enum) ProfileEditDegree.UI.BTN_SUFFIX, this.TAB_SPRITE_NAME[selectTab == ProfileEditDegree.WORD_TAB.SUFFIX ? 1 : 0]);
      this.SetButtonEnabled((Enum) ProfileEditDegree.UI.BTN_PREFIX, selectTab != ProfileEditDegree.WORD_TAB.PREFIX);
      this.SetButtonEnabled((Enum) ProfileEditDegree.UI.BTN_CONJUNCTION, selectTab != ProfileEditDegree.WORD_TAB.CONJUNCTION);
      this.SetButtonEnabled((Enum) ProfileEditDegree.UI.BTN_SUFFIX, selectTab != ProfileEditDegree.WORD_TAB.SUFFIX);
      ((Component) this.GetCtrl((Enum) ProfileEditDegree.UI.LBL_PREFIX)).GetComponent<UILabel>().color = selectTab != ProfileEditDegree.WORD_TAB.PREFIX ? this.normalColors[0] : this.selectColors[0];
      ((Component) this.GetCtrl((Enum) ProfileEditDegree.UI.LBL_PREFIX)).GetComponent<UILabel>().effectColor = selectTab != ProfileEditDegree.WORD_TAB.PREFIX ? this.normalColors[1] : this.selectColors[1];
      ((Component) this.GetCtrl((Enum) ProfileEditDegree.UI.LBL_CONJUNCTION)).GetComponent<UILabel>().color = selectTab != ProfileEditDegree.WORD_TAB.CONJUNCTION ? this.normalColors[0] : this.selectColors[0];
      ((Component) this.GetCtrl((Enum) ProfileEditDegree.UI.LBL_CONJUNCTION)).GetComponent<UILabel>().effectColor = selectTab != ProfileEditDegree.WORD_TAB.CONJUNCTION ? this.normalColors[1] : this.selectColors[1];
      ((Component) this.GetCtrl((Enum) ProfileEditDegree.UI.LBL_SUFFIX)).GetComponent<UILabel>().color = selectTab != ProfileEditDegree.WORD_TAB.SUFFIX ? this.normalColors[0] : this.selectColors[0];
      ((Component) this.GetCtrl((Enum) ProfileEditDegree.UI.LBL_SUFFIX)).GetComponent<UILabel>().effectColor = selectTab != ProfileEditDegree.WORD_TAB.SUFFIX ? this.normalColors[1] : this.selectColors[1];
      this.SetActive((Enum) ProfileEditDegree.UI.SPR_PREFIX_SELECT, selectTab == ProfileEditDegree.WORD_TAB.PREFIX);
      this.SetActive((Enum) ProfileEditDegree.UI.SPR_CONJUNCTION_SELECT, selectTab == ProfileEditDegree.WORD_TAB.CONJUNCTION);
      this.SetActive((Enum) ProfileEditDegree.UI.SPR_SUFFIX_SELECT, selectTab == ProfileEditDegree.WORD_TAB.SUFFIX);
      ((Component) this.arrowTrans).gameObject.SetActive(true);
      switch (selectTab)
      {
        case ProfileEditDegree.WORD_TAB.CONJUNCTION:
          this.arrowTrans.parent = this.GetCtrl((Enum) ProfileEditDegree.UI.BTN_CONJUNCTION);
          break;
        case ProfileEditDegree.WORD_TAB.SUFFIX:
          this.arrowTrans.parent = this.GetCtrl((Enum) ProfileEditDegree.UI.BTN_SUFFIX);
          break;
        default:
          this.arrowTrans.parent = this.GetCtrl((Enum) ProfileEditDegree.UI.BTN_PREFIX);
          break;
      }
      this.arrowTrans.localPosition = this.arrowLocalPos;
      this.arrowTrans.localScale = this.arrowlocalScale;
    }
    else
    {
      this.SetSprite((Enum) ProfileEditDegree.UI.BTN_PREFIX, this.TAB_SPRITE_NAME[2]);
      this.SetSprite((Enum) ProfileEditDegree.UI.BTN_CONJUNCTION, this.TAB_SPRITE_NAME[2]);
      this.SetSprite((Enum) ProfileEditDegree.UI.BTN_SUFFIX, this.TAB_SPRITE_NAME[2]);
      this.SetButtonEnabled((Enum) ProfileEditDegree.UI.BTN_PREFIX, false);
      this.SetButtonEnabled((Enum) ProfileEditDegree.UI.BTN_CONJUNCTION, false);
      this.SetButtonEnabled((Enum) ProfileEditDegree.UI.BTN_SUFFIX, false);
      this.SetActive((Enum) ProfileEditDegree.UI.SPR_PREFIX_SELECT, false);
      this.SetActive((Enum) ProfileEditDegree.UI.SPR_CONJUNCTION_SELECT, false);
      this.SetActive((Enum) ProfileEditDegree.UI.SPR_SUFFIX_SELECT, false);
      ((Component) this.arrowTrans).gameObject.SetActive(false);
    }
  }

  private void SpoileColor()
  {
    UILabel component1 = ((Component) this.GetCtrl((Enum) ProfileEditDegree.UI.LBL_PREFIX)).GetComponent<UILabel>();
    UILabel component2 = ((Component) this.GetCtrl((Enum) ProfileEditDegree.UI.LBL_CONJUNCTION)).GetComponent<UILabel>();
    this.selectColors[0] = component1.color;
    this.selectColors[1] = component1.effectColor;
    this.normalColors[0] = component2.color;
    this.normalColors[1] = component2.effectColor;
  }

  private enum UI
  {
    OBJ_DEGREE_PLATE_ROOT,
    SPR_TAB_SELECTING,
    BTN_PREFIX,
    LBL_PREFIX,
    SPR_PREFIX_SELECT,
    BTN_CONJUNCTION,
    LBL_CONJUNCTION,
    SPR_CONJUNCTION_SELECT,
    BTN_SUFFIX,
    LBL_SUFFIX,
    SPR_SUFFIX_SELECT,
    LBL_SORT,
    OBJ_ARROW_ACTIVE_ROOT,
    OBJ_ARROW_INACTIVE_ROOT,
    LBL_PAGE_NOW,
    LBL_PAGE_MAX,
    LBL_DEGREE_NAME,
    LBL_NO_SELECTABLE_FRAME,
    LBL_DEGREE_REQUIREMENT_TEXT,
    GRD_WORD_LIST,
    LBL_SELECTED_DEGREE_NAME,
    LBL_SELECTED_DEGREE_REQUIREMENT,
    LBL_WORD_NORMAL,
    LBL_WORD_SELECTED,
    LBL_WORD_UNKNOWN,
    SPR_WORD_SELECTED,
  }

  private enum WORD_LIST_SELECT
  {
    NO_SELECT,
    SELECT,
    CLOSE,
  }

  private enum WORD_ATTRIBUTE
  {
    NOUN,
    CONJUNCTION,
  }

  private enum WORD_TAB
  {
    PREFIX = 1,
    CONJUNCTION = 2,
    SUFFIX = 3,
  }
}
