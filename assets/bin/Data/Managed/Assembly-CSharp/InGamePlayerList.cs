// Decompiled with JetBrains decompiler
// Type: InGamePlayerList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InGamePlayerList : GameSection
{
  private List<FriendCharaInfo> infoList = new List<FriendCharaInfo>();

  public override void Initialize()
  {
    base.Initialize();
    this.UpdateCharaList();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
  }

  public override void Exit()
  {
    base.Exit();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  private void OnScreenRotate(bool is_portrait)
  {
    UIPanel panel = ((Component) this.GetCtrl((Enum) InGamePlayerList.UI.SCR_LIST)).GetComponent<UIPanel>();
    Vector4 baseClipRegion = panel.baseClipRegion;
    if (is_portrait)
    {
      Vector3 localPosition = this.GetCtrl((Enum) InGamePlayerList.UI.PORTRAIT_FRAME).localPosition;
      int height = this.GetHeight((Enum) InGamePlayerList.UI.PORTRAIT_FRAME);
      this.GetCtrl((Enum) InGamePlayerList.UI.FRAME).localPosition = localPosition;
      this.SetHeight((Enum) InGamePlayerList.UI.FRAME, height);
      this.GetCtrl((Enum) InGamePlayerList.UI.SCR_LIST).parent = this.GetCtrl((Enum) InGamePlayerList.UI.PORTRAIT_LIST);
      baseClipRegion.w = (float) this.GetHeight((Enum) InGamePlayerList.UI.PORTRAIT_LIST);
    }
    else
    {
      Vector3 localPosition = this.GetCtrl((Enum) InGamePlayerList.UI.LANDSCAPE_FRAME).localPosition;
      int height = this.GetHeight((Enum) InGamePlayerList.UI.LANDSCAPE_FRAME);
      this.GetCtrl((Enum) InGamePlayerList.UI.FRAME).localPosition = localPosition;
      this.SetHeight((Enum) InGamePlayerList.UI.FRAME, height);
      this.GetCtrl((Enum) InGamePlayerList.UI.SCR_LIST).parent = this.GetCtrl((Enum) InGamePlayerList.UI.LANDSCAPE_LIST);
      baseClipRegion.w = (float) this.GetHeight((Enum) InGamePlayerList.UI.LANDSCAPE_LIST);
    }
    panel.baseClipRegion = baseClipRegion;
    panel.clipOffset = Vector2.zero;
    this.GetCtrl((Enum) InGamePlayerList.UI.SCR_LIST).localPosition = Vector3.zero;
    this.ScrollViewResetPosition((Enum) InGamePlayerList.UI.SCR_LIST);
    this.UpdateAnchors();
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() =>
    {
      this.RefreshUI();
      panel.Refresh();
    });
  }

  public override void UpdateUI()
  {
    int count = this.infoList.Count;
    if (count <= 0)
    {
      this.SetActive((Enum) InGamePlayerList.UI.GRD_LIST, false);
      this.SetActive((Enum) InGamePlayerList.UI.STR_NON_LIST, true);
    }
    else
    {
      this.SetGrid((Enum) InGamePlayerList.UI.GRD_LIST, "InGamePlayerListItem", count, false, (Action<int, Transform, bool>) ((i, t, b) =>
      {
        int _atk;
        int _def;
        int _hp;
        MonoBehaviourSingleton<StatusManager>.I.CalcUserStatusParam((CharaInfo) this.infoList[i], out _atk, out _def, out _hp);
        this.SetLabelText(t, (Enum) InGamePlayerList.UI.LBL_NAME, this.infoList[i].name);
        this.SetLabelText(t, (Enum) InGamePlayerList.UI.LBL_LEVEL, this.infoList[i].level.ToString());
        this.SetLabelText(t, (Enum) InGamePlayerList.UI.LBL_HP, _hp.ToString());
        this.SetLabelText(t, (Enum) InGamePlayerList.UI.LBL_ATK, _atk.ToString());
        this.SetLabelText(t, (Enum) InGamePlayerList.UI.LBL_DEF, _def.ToString());
        this.SetLabelText(t, (Enum) InGamePlayerList.UI.LBL_COMMENT, this.infoList[i].comment);
        if (this.infoList[i].following)
          this.SetEvent(t, (Enum) InGamePlayerList.UI.BTN_FOLLOW, "UN_FOLLOW", i);
        else
          this.SetEvent(t, (Enum) InGamePlayerList.UI.BTN_FOLLOW, "FOLLOW", i);
        this.SetButtonEnabled(t, (Enum) InGamePlayerList.UI.BTN_FOLLOW, !this.infoList[i].following);
        bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(this.infoList[i].userId);
        if (flag)
          this.SetEvent(t, (Enum) InGamePlayerList.UI.BTN_BLACK_LIST, "BLACK_LIST_OUT", i);
        else
          this.SetEvent(t, (Enum) InGamePlayerList.UI.BTN_BLACK_LIST, "BLACK_LIST_IN", i);
        this.SetButtonEnabled(t, (Enum) InGamePlayerList.UI.BTN_BLACK_LIST, !flag);
        string clanId = this.infoList[i].userClanData != null ? this.infoList[i].userClanData.cId : "0";
        this.SetFollowStatus(t, this.infoList[i].userId, this.infoList[i].following, this.infoList[i].follower, clanId);
      }));
      this.SetActive((Enum) InGamePlayerList.UI.STR_NON_LIST, false);
    }
  }

  protected void SetFollowStatus(
    Transform t,
    int user_id,
    bool following,
    bool follower,
    string clanId)
  {
    bool is_visible1 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
    bool is_visible2 = false;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
      is_visible2 = clanId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
    bool is_visible3 = !is_visible1 && following | follower;
    this.SetActive(t, (Enum) InGamePlayerList.UI.SPR_BLACKLIST_ICON, is_visible1);
    this.SetActive(t, (Enum) InGamePlayerList.UI.OBJ_FOLLOW, is_visible3);
    this.SetActive(t, (Enum) InGamePlayerList.UI.SPR_FOLLOW, is_visible3 & following);
    this.SetActive(t, (Enum) InGamePlayerList.UI.SPR_FOLLOWER, is_visible3 & follower);
    this.SetActive(t, (Enum) InGamePlayerList.UI.SPR_SAME_CLAN_ICON, is_visible2);
    UIGrid component = this.GetComponent<UIGrid>(t, (Enum) InGamePlayerList.UI.GRD_FOLLOW_ARROW);
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reposition();
  }

  private void UpdateCharaList()
  {
    this.infoList.Clear();
    if (!CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
      return;
    MonoBehaviourSingleton<FieldManager>.I.SendFieldCharaList((Action<bool, List<FriendCharaInfo>>) ((is_success, list) =>
    {
      if (!is_success)
        return;
      this.infoList = list;
      this.SetDirty((Enum) InGamePlayerList.UI.GRD_LIST);
      this.RefreshUI();
    }));
  }

  private void OnQuery_CHANGE_INFO() => this.UpdateCharaList();

  private void OnQuery_FOLLOW()
  {
    int index = (int) GameSection.GetEventData();
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.infoList[index].name
    });
    List<int> id_list = new List<int>();
    id_list.Add(this.infoList[index].userId);
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(id_list, (Action<Error, List<int>>) ((err, follow_list) =>
    {
      int num = err != Error.None ? 0 : (follow_list.Count > 0 ? 1 : 0);
      if (num != 0)
        this.infoList[index].following = !this.infoList[index].following;
      if (MonoBehaviourSingleton<CoopApp>.IsValid())
        CoopApp.UpdateField();
      GameSection.ResumeEvent(num != 0);
      this.RefreshUI();
    }));
  }

  private void OnQuery_UN_FOLLOW()
  {
    int index = (int) GameSection.GetEventData();
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.infoList[index].name
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendUnfollowUser(this.infoList[index].userId, (Action<bool>) (is_success =>
    {
      if (is_success)
        this.infoList[index].following = !this.infoList[index].following;
      GameSection.ResumeEvent(is_success);
      this.RefreshUI();
    }));
  }

  private void OnQuery_BLACK_LIST_IN()
  {
    int index = (int) GameSection.GetEventData();
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.infoList[index].name
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<BlackListManager>.I.SendAdd(this.infoList[index].userId, (Action<bool>) (is_success =>
    {
      if (is_success)
        this.infoList[index].following = false;
      GameSection.ResumeEvent(is_success);
      this.RefreshUI();
    }));
  }

  private void OnQuery_BLACK_LIST_OUT()
  {
    int eventData = (int) GameSection.GetEventData();
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.infoList[eventData].name
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<BlackListManager>.I.SendDelete(this.infoList[eventData].userId, (Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(is_success);
      this.RefreshUI();
    }));
  }

  private enum UI
  {
    FRAME,
    GRD_LIST,
    STR_NON_LIST,
    SCR_LIST,
    LBL_NAME,
    LBL_LEVEL,
    LBL_HP,
    LBL_ATK,
    LBL_DEF,
    LBL_COMMENT,
    GRD_FOLLOW_ARROW,
    OBJ_FOLLOW,
    SPR_FOLLOW,
    SPR_FOLLOWER,
    SPR_BLACKLIST_ICON,
    SPR_SAME_CLAN_ICON,
    BTN_FOLLOW,
    BTN_BLACK_LIST,
    PORTRAIT_FRAME,
    PORTRAIT_LIST,
    LANDSCAPE_FRAME,
    LANDSCAPE_LIST,
  }
}
