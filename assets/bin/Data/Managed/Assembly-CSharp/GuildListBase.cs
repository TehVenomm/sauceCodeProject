// Decompiled with JetBrains decompiler
// Type: GuildListBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GuildListBase : UserListBase<FriendCharaInfo>
{
  public override void Initialize()
  {
    this.SetActive((Enum) GuildListBase.UI.OBJ_GUILD_NUMBER_ROOT, false);
    base.Initialize();
  }

  public virtual void ListUI()
  {
    FriendCharaInfo[] friendCharaInfoArray = (FriendCharaInfo[]) null;
    if (this.recvList != null && this.recvList.Count > 0)
      friendCharaInfoArray = this.recvList.ToArray();
    if (friendCharaInfoArray == null || friendCharaInfoArray.Length == 0)
    {
      this.SetActive((Enum) GuildListBase.UI.STR_NON_LIST, true);
      this.SetActive((Enum) GuildListBase.UI.GRD_LIST, false);
      this.SetActive((Enum) GuildListBase.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) GuildListBase.UI.OBJ_INACTIVE_ROOT, true);
      this.SetLabelText((Enum) GuildListBase.UI.LBL_NOW, "0");
      this.SetLabelText((Enum) GuildListBase.UI.LBL_MAX, "0");
    }
    else
    {
      this.SetPageNumText((Enum) GuildListBase.UI.LBL_NOW, this.nowPage + 1);
      this.SetPageNumText((Enum) GuildListBase.UI.LBL_MAX, this.pageNumMax);
      this.SetActive((Enum) GuildListBase.UI.STR_NON_LIST, false);
      this.SetActive((Enum) GuildListBase.UI.GRD_LIST, true);
      this.SetActive((Enum) GuildListBase.UI.OBJ_ACTIVE_ROOT, this.pageNumMax != 1);
      this.SetActive((Enum) GuildListBase.UI.OBJ_INACTIVE_ROOT, this.pageNumMax == 1);
      this.UpdateDynamicList();
    }
  }

  protected virtual void UpdateDynamicList()
  {
    FriendCharaInfo[] info = (FriendCharaInfo[]) null;
    int item_num = 0;
    if (this.recvList != null && this.recvList.Count > 0)
    {
      info = this.recvList.ToArray();
      if (info != null)
        item_num = info.Length;
    }
    if (GameDefine.ACTIVE_DEGREE)
      ((Component) this.GetCtrl((Enum) GuildListBase.UI.GRD_LIST)).GetComponent<UIGrid>().cellHeight = (float) GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
    this.SetDynamicList((Enum) GuildListBase.UI.GRD_LIST, this.GetListItemName, item_num, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetListItem(i, t, is_recycle, info[i])));
  }

  protected virtual void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
  {
    this.SetFollowStatus(t, data.userId, data.following, data.follower);
    this.SetCharaInfo(data, i, t, is_recycle, data.userId == 0);
    if (!LoungeMatchingManager.IsValidInLounge())
      return;
    this.SetActive(t, (Enum) GuildListBase.UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(data.userId));
  }

  protected void SetCharaInfo(
    FriendCharaInfo data,
    int i,
    Transform t,
    bool is_recycle,
    bool isGM)
  {
    this.SetEvent(t, "GUILD_INFO", i);
    if (isGM)
      this.SetRenderNPCModel(t, (Enum) GuildListBase.UI.TEX_MODEL, 0, new Vector3(0.0f, -1.49f, 1.87f), new Vector3(0.0f, 154f, 0.0f), 10f);
    else
      this.SetRenderPlayerModel(t, (Enum) GuildListBase.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo((CharaInfo) data, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    this.SetLabelText(t, (Enum) GuildListBase.UI.LBL_NAME, data.name);
    this.SetLabelText(t, (Enum) GuildListBase.UI.LBL_LEVEL, data.level.ToString());
    this.SetLabelText(t, (Enum) GuildListBase.UI.LBL_COMMENT, data.comment);
    this.SetLabelText(t, (Enum) GuildListBase.UI.LBL_LAST_LOGIN, this.sectionData.GetText("LAST_LOGIN"));
    this.SetLabelText(t, (Enum) GuildListBase.UI.LBL_LAST_LOGIN_TIME, data.lastLogin);
    EquipSetCalculator equipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4);
    equipSetCalculator.SetEquipSet(data.equipSet);
    SimpleStatus finalStatus = equipSetCalculator.GetFinalStatus(0, (int) data.hp, (int) data.atk, (int) data.def);
    this.SetLabelText(t, (Enum) GuildListBase.UI.LBL_ATK, finalStatus.GetAttacksSum().ToString());
    this.SetLabelText(t, (Enum) GuildListBase.UI.LBL_DEF, finalStatus.defences[0].ToString());
    this.SetLabelText(t, (Enum) GuildListBase.UI.LBL_HP, finalStatus.hp.ToString());
    ((Component) this.FindCtrl(t, (Enum) GuildListBase.UI.OBJ_DEGREE_FRAME_ROOT)).GetComponent<DegreePlate>().Initialize(data.selectedDegrees, false, (Action<DegreePlate>) (x => ((Component) this.GetCtrl((Enum) GuildListBase.UI.GRD_LIST)).GetComponent<UIGrid>().Reposition()));
  }

  protected void SetFollowStatus(Transform t, int user_id, bool following, bool follower)
  {
    bool is_visible = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
    this.SetActive(t, (Enum) GuildListBase.UI.SPR_BLACKLIST_ICON, is_visible);
    this.SetActive(t, (Enum) GuildListBase.UI.SPR_FOLLOW, !is_visible & following);
    this.SetActive(t, (Enum) GuildListBase.UI.SPR_FOLLOWER, !is_visible & follower);
  }

  protected List<FriendCharaInfo> ChangeData(List<FriendCharaInfo> chara_list)
  {
    List<FriendCharaInfo> new_list = new List<FriendCharaInfo>();
    chara_list.ForEach((Action<FriendCharaInfo>) (chara_info =>
    {
      if (chara_info == null)
        return;
      new_list.Add(new FriendCharaInfo()
      {
        userId = chara_info.userId,
        name = chara_info.name,
        comment = chara_info.comment,
        lastLogin = chara_info.lastLogin,
        code = chara_info.code,
        hp = chara_info.hp,
        atk = chara_info.atk,
        def = chara_info.def,
        level = chara_info.level,
        sex = chara_info.sex,
        faceId = chara_info.faceId,
        hairId = chara_info.hairId,
        hairColorId = chara_info.hairColorId,
        skinId = chara_info.skinId,
        voiceId = chara_info.voiceId,
        aId = chara_info.aId,
        hId = chara_info.hId,
        rId = chara_info.rId,
        lId = chara_info.lId,
        showHelm = chara_info.showHelm,
        equipSet = chara_info.equipSet,
        following = chara_info.following,
        follower = chara_info.follower,
        selectedDegrees = chara_info.selectedDegrees
      });
    }));
    return new_list;
  }

  public virtual void OnQuery_GUILD_INFO()
  {
    GameSection.SetEventData((object) this.recvList[(int) GameSection.GetEventData()]);
  }

  protected void SetDirtyTable() => this.SetDirty((Enum) GuildListBase.UI.GRD_LIST);

  protected virtual string GetListItemName => "GuildListBaseItem";

  protected enum UI
  {
    OBJ_GUILD_NUMBER_ROOT,
    LBL_GUILD_NUMBER_NOW,
    LBL_GUILD_NUMBER_MAX,
    GRD_LIST,
    TEX_MODEL,
    STR_NON_LIST,
    SPR_FOLLOW,
    SPR_FOLLOWER,
    SPR_BLACKLIST_ICON,
    OBJ_COMMENT,
    LBL_COMMENT,
    LBL_LAST_LOGIN,
    LBL_LAST_LOGIN_TIME,
    LBL_NAME,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_LEVEL,
    LBL_NOW,
    LBL_MAX,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    BTN_PAGE_PREV,
    BTN_PAGE_NEXT,
    OBJ_DEGREE_FRAME_ROOT,
    SPR_ICON_FIRST_MET,
  }
}
