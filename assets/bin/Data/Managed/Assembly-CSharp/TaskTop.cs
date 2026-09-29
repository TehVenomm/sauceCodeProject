// Decompiled with JetBrains decompiler
// Type: TaskTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TaskTop : GameSection
{
  private readonly string[] STRING_KEY = new string[2]
  {
    "STR_NOTACHIEVED",
    "STR_ACHIEVED"
  };
  private const int ONE_PAGE_ITEM_NUM = 10;
  private readonly string LIST_ITEM_PREFAB_NAME = "TaskListItem";
  private int currentPageIndex;
  private int pageMaxNum;
  private TaskTop.SHOW_TYPE showType;
  private Transform gridTransform;
  private UIScrollView scrollView;
  private List<TaskTop.TaskData>[] taskDataLists = new List<TaskTop.TaskData>[2];

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "TaskTable";
    }
  }

  public override void Initialize()
  {
    this.gridTransform = this.GetCtrl((Enum) TaskTop.UI.GRD_INVENTORY);
    this.scrollView = this.GetComponent<UIScrollView>((Enum) TaskTop.UI.SCR_INVENTORY);
    this.InitTaskDataLists();
    this.SendTaskList();
    base.Initialize();
  }

  private void InitTaskDataLists()
  {
    List<TaskInfo> taskInfos = MonoBehaviourSingleton<AchievementManager>.I.GetTaskInfos();
    int count = taskInfos.Count;
    this.taskDataLists[0] = new List<TaskTop.TaskData>(count);
    this.taskDataLists[1] = new List<TaskTop.TaskData>(count);
    for (int index = 0; index < count; ++index)
    {
      TaskTop.TaskData taskData = this.CreateTaskData(taskInfos[index]);
      if (taskData.info.status == 3)
        this.taskDataLists[1].Add(taskData);
      else
        this.taskDataLists[0].Add(taskData);
    }
    for (int index = 0; index < 2; ++index)
    {
      if (this.taskDataLists[index] != null)
        this.taskDataLists[index].Sort(new Comparison<TaskTop.TaskData>(TaskTop.CompareByStatusAndId));
    }
  }

  private static int CompareByStatusAndId(TaskTop.TaskData a, TaskTop.TaskData b)
  {
    if (b.info.status < a.info.status)
      return -1;
    if (a.info.status != b.info.status)
      return 1;
    if (a.tableData != null && b.tableData != null)
      return a.tableData.orderNo - b.tableData.orderNo;
    Debug.LogError((object) $"<color=red>{(object) (a.tableData == null ? a.info.taskId : b.info.taskId)} not found</color>");
    return -1;
  }

  private TaskTop.TaskData CreateTaskData(TaskInfo info)
  {
    return new TaskTop.TaskData()
    {
      info = info,
      tableData = Singleton<TaskTable>.I.Get((uint) info.taskId)
    };
  }

  private void InsertTaskData(TaskTop.SHOW_TYPE type, TaskTop.TaskData data)
  {
    this.taskDataLists[(int) type].Add(data);
    this.taskDataLists[(int) type].Sort(new Comparison<TaskTop.TaskData>(TaskTop.CompareByStatusAndId));
  }

  private void InsertTaskData(TaskTop.SHOW_TYPE type, TaskInfo info)
  {
    this.InsertTaskData(type, this.CreateTaskData(info));
  }

  public override void UpdateUI()
  {
    this.pageMaxNum = this.taskDataLists[(int) this.showType].Count / 10 + 1;
    this.SetLabelText((Enum) TaskTop.UI.LBL_PAGE_MAX, this.pageMaxNum.ToString());
    this.SetLabelText((Enum) TaskTop.UI.LBL_CURRENT_NUM, this.taskDataLists[1].Count.ToString());
    bool is_visible1 = this.currentPageIndex != 0;
    this.SetActive((Enum) TaskTop.UI.BTN_ACHIEVE_LIST_L, is_visible1);
    this.SetActive((Enum) TaskTop.UI.BTN_ACHIEVE_LIST_L_ADD, is_visible1);
    this.SetActive((Enum) TaskTop.UI.BTN_INACTIVE_ACHIEVE_LIST_L, !is_visible1);
    bool is_visible2 = this.currentPageIndex + 1 < this.pageMaxNum;
    this.SetActive((Enum) TaskTop.UI.BTN_ACHIEVE_LIST_R, is_visible2);
    this.SetActive((Enum) TaskTop.UI.BTN_ACHIEVE_LIST_R_ADD, is_visible2);
    this.SetActive((Enum) TaskTop.UI.BTN_INACTIVE_ACHIEVE_LIST_R, !is_visible2);
    this.SetLabelText((Enum) TaskTop.UI.LBL_SHOW_TYPE, this.sectionData.GetText(this.STRING_KEY[(int) this.showType]));
    this.SetLabelText((Enum) TaskTop.UI.LBL_PAGE_NOW, (this.currentPageIndex + 1).ToString());
    this.UpdateInventory();
    if (Object.op_Inequality((Object) this.scrollView, (Object) null))
      this.scrollView.ResetPosition();
    base.UpdateUI();
  }

  private void UpdateInventory()
  {
    if (Object.op_Equality((Object) this.gridTransform, (Object) null))
      this.gridTransform = this.GetCtrl((Enum) TaskTop.UI.GRD_INVENTORY);
    int start = this.currentPageIndex * 10;
    int item_num = 10;
    if (this.taskDataLists[(int) this.showType].Count - start < 10)
      item_num = this.taskDataLists[(int) this.showType].Count - start;
    this.SetDynamicList(this.gridTransform, this.LIST_ITEM_PREFAB_NAME, item_num, true, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, isRecycle) => this.InitListItem(this.taskDataLists[(int) this.showType][start + i], t)));
  }

  private void InitListItem(TaskTop.TaskData data, Transform root)
  {
    this.SetActive(root, (Enum) TaskTop.UI.OBJ_CLEARED_ITEM, data.info.status == 2 || data.info.status == 3);
    this.SetActive(root, (Enum) TaskTop.UI.OBJ_NOT_CLEARED_ITEM, data.info.status == 1);
    Transform ctrl = this.FindCtrl(root, (Enum) TaskTop.UI.SPR_GAUGE);
    int num = data.tableData != null ? data.tableData.goalNum : int.MaxValue;
    int rewardNum = data.tableData != null ? data.tableData.rewardNum : 0;
    int itemId = data.tableData != null ? data.tableData.itemId : 0;
    string title = data.tableData != null ? data.tableData.title : "";
    string detail = data.tableData != null ? data.tableData.detail : "";
    REWARD_TYPE rewardType = data.tableData != null ? data.tableData.rewardType : REWARD_TYPE.NONE;
    if (Object.op_Inequality((Object) ctrl, (Object) null))
      ctrl.localScale = new Vector3(Mathf.Clamp((float) data.info.progress / (float) num, 0.0f, 1f), 1f, 1f);
    this.SetLabelText(root, (Enum) TaskTop.UI.LBL_GAUGE, $"{data.info.progress.ToString()}/{num.ToString()}");
    this.SetLabelText(root, (Enum) TaskTop.UI.LBL_CONDITION, title);
    this.SetLabelText(root, (Enum) TaskTop.UI.LBL_REWARD_NAME, detail);
    if (rewardNum <= 1)
    {
      this.SetActive(root, (Enum) TaskTop.UI.LBL_ITEM, false);
    }
    else
    {
      this.SetActive(root, (Enum) TaskTop.UI.LBL_ITEM, true);
      this.SetLabelText(root, (Enum) TaskTop.UI.LBL_ITEM, "x" + rewardNum.ToString());
    }
    ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(rewardType, (uint) itemId, this.FindCtrl(root, (Enum) TaskTop.UI.OBJ_ICON_ROOT));
    this.SetMaterialInfo(rewardItemIcon._transform, rewardType, (uint) itemId, ((Component) this.scrollView).transform);
    UIButton component = ((Component) root).GetComponent<UIButton>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.tweenTarget = ((Component) rewardItemIcon).gameObject;
    GameObject gameObject1 = ((Component) this.FindCtrl(root, (Enum) TaskTop.UI.SPR_NOT_RECIEVED)).gameObject;
    GameObject gameObject2 = ((Component) this.FindCtrl(root, (Enum) TaskTop.UI.SPR_RECIEVED)).gameObject;
    if (data.info.status == 2)
    {
      this.SetButtonEnabled(root, true);
      this.SetEvent(root, "RECEIVE_REWARD", (object) data);
      gameObject1.SetActive(true);
      gameObject2.SetActive(false);
    }
    else if (data.info.status == 3)
    {
      this.SetButtonEnabled(root, false);
      gameObject1.SetActive(false);
      gameObject2.SetActive(true);
    }
    else
      this.SetButtonEnabled(root, false);
  }

  private void OnQuery_NEXT_PAGE()
  {
    if (this.pageMaxNum <= this.currentPageIndex + 1)
      return;
    ++this.currentPageIndex;
    this.UpdateUI();
  }

  private void OnQuery_PREV_PAGE()
  {
    if (this.currentPageIndex <= 0)
      return;
    --this.currentPageIndex;
    this.UpdateUI();
  }

  private void OnQuery_CHANGE_SHOW_TYPE()
  {
    this.currentPageIndex = 0;
    this.showType = this.showType != TaskTop.SHOW_TYPE.NOT_ACHIEVED ? TaskTop.SHOW_TYPE.NOT_ACHIEVED : TaskTop.SHOW_TYPE.ACHIVED;
    this.UpdateUI();
  }

  private void OnQuery_RECEIVE_REWARD()
  {
    TaskTop.TaskData data = GameSection.GetEventData() as TaskTop.TaskData;
    if (data == null)
      return;
    GameSection.StayEvent();
    Protocol.Send<TaskCompleteModel.RequestSendForm, TaskCompleteModel>(TaskCompleteModel.URL, new TaskCompleteModel.RequestSendForm()
    {
      uId = data.info.taskId
    }, (Action<TaskCompleteModel>) (model =>
    {
      this.taskDataLists[0].Remove(data);
      this.UpdateUI();
      GameSection.ResumeEvent(true);
    }));
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.UPDATE_TASK_LIST;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG notify_flags)
  {
    if ((notify_flags & GameSection.NOTIFY_FLAG.UPDATE_TASK_LIST) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.InitTaskDataLists();
    base.OnNotify(notify_flags);
  }

  public void SendTaskList()
  {
    Protocol.Send<TaskListModel>(TaskListModel.URL, (Action<TaskListModel>) (ret => { }));
  }

  private enum UI
  {
    BTN_INACTIVE_ACHIEVE_LIST_L,
    BTN_INACTIVE_ACHIEVE_LIST_R,
    BTN_ACHIEVE_LIST_L,
    BTN_ACHIEVE_LIST_R,
    BTN_ACHIEVE_LIST_L_ADD,
    BTN_ACHIEVE_LIST_R_ADD,
    SCR_INVENTORY,
    GRD_INVENTORY,
    LBL_CURRENT_NUM,
    LBL_PAGE_NOW,
    LBL_PAGE_MAX,
    LBL_SHOW_TYPE,
    OBJ_ICON_ROOT,
    OBJ_CLEARED_ITEM,
    OBJ_NOT_CLEARED_ITEM,
    SPR_NOT_RECIEVED,
    SPR_RECIEVED,
    SPR_GAUGE,
    LBL_REWARD_NAME,
    LBL_CONDITION,
    LBL_GAUGE,
    LBL_ITEM,
  }

  private enum SHOW_TYPE
  {
    NOT_ACHIEVED,
    ACHIVED,
    MAX_NUM,
  }

  public class TaskData
  {
    public TaskTable.TaskData tableData;
    public TaskInfo info;
  }
}
