// Decompiled with JetBrains decompiler
// Type: PresentTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PresentTop : GameSection
{
  private object[] selectEventData;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) PresentTop.UI.STR_TITLE, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) PresentTop.UI.STR_TITLE_REFLECT, this.sectionData.GetText("STR_TITLE"));
    int count = MonoBehaviourSingleton<PresentManager>.I.presentData.presents.Count;
    bool is_visible = count > 0;
    this.SetActive((Enum) PresentTop.UI.BTN_ALL, is_visible);
    this.SetActive((Enum) PresentTop.UI.BTN_ALL_DISABLE, !is_visible);
    this.SetLabelText((Enum) PresentTop.UI.STR_ALL_DISABLE, this.sectionData.GetText("STR_ALL"));
    this.SetActive((Enum) PresentTop.UI.STR_NON_LIST, !is_visible);
    this.SetGrid((Enum) PresentTop.UI.GRD_LIST, "PresentListItem", count, false, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      Present present = MonoBehaviourSingleton<PresentManager>.I.presentData.presents[i];
      this.SetLabelText(t, (Enum) PresentTop.UI.LBL_NAME, present.name);
      this.SetLabelText(t, (Enum) PresentTop.UI.LBL_COMMENT, present.comment);
      this.SetLabelText(t, (Enum) PresentTop.UI.LBL_DESC, present.desc);
      string text = string.IsNullOrEmpty(present.expire) ? this.sectionData.GetText("NON_EXPIRE") : present.expire;
      this.SetLabelText(t, (Enum) PresentTop.UI.LBL_EXPIRE, text);
      this.SetLabelText(t, (Enum) PresentTop.UI.LBL_TIME, present.timeInfo);
      this.SetEvent(t, (Enum) PresentTop.UI.BTN_SELECT, "SELECT", i);
      ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE) present.type, (uint) present.itemId, this.FindCtrl(t, (Enum) PresentTop.UI.OBJ_ICON_ROOT));
      if (!Object.op_Inequality((Object) rewardItemIcon, (Object) null))
        return;
      rewardItemIcon.SetEnableCollider(false);
    }));
    int num = 1;
    int page_num1 = MonoBehaviourSingleton<PresentManager>.I.page + num;
    int page_num2 = Mathf.Max(MonoBehaviourSingleton<PresentManager>.I.pageMax, num);
    this.SetPageNumText((Enum) PresentTop.UI.LBL_NOW, page_num1);
    this.SetPageNumText((Enum) PresentTop.UI.LBL_MAX, page_num2);
    this.SetActive((Enum) PresentTop.UI.OBJ_ACTIVE_ROOT, num != page_num2);
    this.SetActive((Enum) PresentTop.UI.OBJ_INACTIVE_ROOT, num == page_num2);
  }

  public override void StartSection()
  {
  }

  private void MovePage(int page, bool is_on_query_event = true)
  {
    if (page < 0)
      page = MonoBehaviourSingleton<PresentManager>.I.pageMax - 1;
    else if (page >= MonoBehaviourSingleton<PresentManager>.I.pageMax)
      page = 0;
    if (is_on_query_event)
      GameSection.StayEvent();
    MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(page, (Action<bool>) (is_success =>
    {
      if (!is_on_query_event)
        return;
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void OnQuery_PAGE_PREV()
  {
    this.MovePage(MonoBehaviourSingleton<PresentManager>.I.page - 1);
  }

  private void OnQuery_PAGE_NEXT()
  {
    this.MovePage(MonoBehaviourSingleton<PresentManager>.I.page + 1);
  }

  private void OnQuery_SELECT()
  {
    Present present = MonoBehaviourSingleton<PresentManager>.I.presentData.presents[(int) GameSection.GetEventData()];
    this.selectEventData = new object[3]
    {
      (object) (int) GameSection.GetEventData(),
      (object) present.name,
      (object) 1
    };
    GameSection.SetEventData((object) this.selectEventData);
    this.SendReceivePresent(new List<string>()
    {
      present.uniqId
    });
  }

  private void OnQuery_ALL()
  {
    this.selectEventData = new object[3]
    {
      (object) -1,
      (object) string.Empty,
      (object) MonoBehaviourSingleton<PresentManager>.I.presentData.presents.Count
    };
    GameSection.SetEventData((object) this.selectEventData);
  }

  private void OnQuery_PresentAllConfirm_YES()
  {
    GameSection.SetEventData((object) this.selectEventData);
    List<string> uniqIds = new List<string>();
    MonoBehaviourSingleton<PresentManager>.I.presentData.presents.ForEach((Action<Present>) (o => uniqIds.Add(o.uniqId)));
    this.SendReceivePresent(uniqIds);
  }

  private void SendReceivePresent(List<string> ids)
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<PresentManager>.I.SendReceivePresent(ids, (Action<bool, Error, int>) ((is_success, network_err, num) =>
    {
      bool is_resume = is_success;
      if (is_success)
      {
        this.selectEventData[2] = (object) num;
        SoundManager.PlaySystemSE(SoundID.UISE.GET_PRIZE);
      }
      else
      {
        is_resume = true;
        switch (network_err)
        {
          case Error.WRN_PRESENT_OVER_MONEY:
            GameSection.ChangeStayEvent("WRN_PRESENT_OVER_MONEY");
            break;
          case Error.WRN_PRESENT_OVER_ITEM:
            GameSection.ChangeStayEvent("WRN_PRESENT_OVER_ITEM");
            break;
          case Error.WRN_PRESENT_OVER_EQUIP_ITEM:
            GameSection.ChangeStayEvent("WRN_PRESENT_OVER_EQUIP_ITEM");
            break;
          case Error.WRN_PRESENT_OVER_SKILL_ITEM:
            GameSection.ChangeStayEvent("WRN_PRESENT_OVER_SKILL_ITEM");
            break;
          case Error.WRN_PRESENT_OVER_QUEST_ITEM:
            GameSection.ChangeStayEvent("WRN_PRESENT_OVER_QUEST_ITEM");
            break;
          case Error.WRN_PRESENT_OVER_EQUIP_AND_SKILL:
            GameSection.ChangeStayEvent("WRN_PRESENT_OVER_EQUIP_AND_SKILL");
            break;
          case Error.WRN_PRESENT_OVER_ETC:
            GameSection.ChangeStayEvent("WRN_PRESENT_OVER_ETC");
            break;
          default:
            is_resume = false;
            break;
        }
      }
      GameSection.ResumeEvent(is_resume);
    }));
  }

  private void OnQuery_PresentOneMessage_OK()
  {
  }

  private void OnQuery_PresentAllMessage_OK()
  {
  }

  public void OnQuery_PresentRecvOverEquipItem_GO_ITEM_STORAGE()
  {
    this.GO_ITEM_STORAGE(ItemStorageTop.TAB_MODE.EQUIP);
  }

  public void OnQuery_PresentRecvOverSkillItem_GO_ITEM_STORAGE()
  {
    this.GO_ITEM_STORAGE(ItemStorageTop.TAB_MODE.SKILL);
  }

  public void OnQuery_PresentRecvOverEquipAndSkill_GO_ITEM_STORAGE()
  {
    this.GO_ITEM_STORAGE(ItemStorageTop.TAB_MODE.SKILL);
  }

  private void GO_ITEM_STORAGE(ItemStorageTop.TAB_MODE tab)
  {
    EventData[] event_datas = new EventData[5]
    {
      new EventData("SECTION_BACK", (object) null),
      new EventData("SECTION_BACK", (object) null),
      new EventData("MAIN_MENU_STUDIO", (object) null),
      new EventData("TO_STORAGE", (object) null),
      new EventData("TAB_" + (object) (int) tab, (object) null)
    };
    GameSection.StopEvent();
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
  }

  public void OnQuery_PresentRecvOverEquipItem_EXPAND_STORAGE() => this.EXPAND_STORAGE();

  public void OnQuery_PresentRecvOverSkillItem_EXPAND_STORAGE() => this.EXPAND_STORAGE();

  public void OnQuery_PresentRecvOverEquipAndSkill_EXPAND_STORAGE() => this.EXPAND_STORAGE();

  private void EXPAND_STORAGE() => this.DispatchEvent(nameof (EXPAND_STORAGE));

  private void OnQuery_CAUTION() => GameSection.SetEventData((object) WebViewManager.Present);

  public override void OnNotify(GameSection.NOTIFY_FLAG notify_flags)
  {
    if ((notify_flags & GameSection.NOTIFY_FLAG.UPDATE_PRESENT_NUM) != (GameSection.NOTIFY_FLAG) 0 && (notify_flags & GameSection.NOTIFY_FLAG.UPDATE_PRESENT_LIST) == (GameSection.NOTIFY_FLAG) 0)
    {
      int page = Mathf.Min(MonoBehaviourSingleton<PresentManager>.I.presentNum > 0 ? (MonoBehaviourSingleton<PresentManager>.I.presentNum - 1) / MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.LIST_NUM_PER_PAGE : 0, MonoBehaviourSingleton<PresentManager>.I.page);
      this.SetDirty((Enum) PresentTop.UI.GRD_LIST);
      this.MovePage(page, false);
    }
    else if ((notify_flags & GameSection.NOTIFY_FLAG.UPDATE_PRESENT_LIST) != (GameSection.NOTIFY_FLAG) 0)
      this.SetDirty((Enum) PresentTop.UI.GRD_LIST);
    base.OnNotify(notify_flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_PRESENT_LIST;
  }

  private enum UI
  {
    GRD_LIST,
    LBL_NOW,
    LBL_MAX,
    BTN_PAGE_PREV,
    BTN_PAGE_NEXT,
    BTN_ALL,
    BTN_ALL_DISABLE,
    STR_ALL_DISABLE,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    STR_TITLE,
    STR_TITLE_REFLECT,
    STR_NON_LIST,
    LBL_NAME,
    LBL_COMMENT,
    LBL_DESC,
    LBL_TIME,
    LBL_EXPIRE,
    BTN_SELECT,
    OBJ_ICON_ROOT,
  }
}
