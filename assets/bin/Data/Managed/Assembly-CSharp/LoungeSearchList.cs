// Decompiled with JetBrains decompiler
// Type: LoungeSearchList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

#nullable disable
public class LoungeSearchList : GameSection
{
  private LoungeModel.Lounge[] lounges;
  private LoungeSearchList.UI[] loungeMembers = new LoungeSearchList.UI[7]
  {
    LoungeSearchList.UI.TGL_L_MEMBER_1,
    LoungeSearchList.UI.TGL_L_MEMBER_2,
    LoungeSearchList.UI.TGL_L_MEMBER_3,
    LoungeSearchList.UI.TGL_L_MEMBER_4,
    LoungeSearchList.UI.TGL_L_MEMBER_5,
    LoungeSearchList.UI.TGL_L_MEMBER_6,
    LoungeSearchList.UI.TGL_L_MEMBER_7
  };

  public override void UpdateUI()
  {
    if (!LoungeMatchingManager.IsValidNotEmptyList())
    {
      this.SetActive((Enum) LoungeSearchList.UI.GRD_LOUNGE, false);
      this.SetActive((Enum) LoungeSearchList.UI.STR_NON_LIST, true);
    }
    else
    {
      this.lounges = MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges.ToArray();
      this.SetActive((Enum) LoungeSearchList.UI.GRD_LOUNGE, true);
      this.SetActive((Enum) LoungeSearchList.UI.STR_NON_LIST, false);
      this.SetGrid((Enum) LoungeSearchList.UI.GRD_LOUNGE, "LoungeSearchListItem", this.lounges.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        this.SetEvent(t, "SELECT_LOUNGE", i);
        this.SetLoungeData(this.lounges[i], t);
      }));
      base.UpdateUI();
    }
  }

  public override void Initialize()
  {
    MonoBehaviourSingleton<LoungeMatchingManager>.I.ResetLoungeSearchRequest();
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    yield return (object) this.StartCoroutine(this.Reload());
    base.Initialize();
  }

  private IEnumerator Reload(Action<bool> cb = null)
  {
    bool is_recv = false;
    this.SendRequest((System.Action) (() => is_recv = true), cb);
    while (!is_recv)
      yield return (object) null;
    this.SetDirty((Enum) LoungeSearchList.UI.GRD_LOUNGE);
    this.RefreshUI();
  }

  private void SendRequest(System.Action onFinish, Action<bool> cb)
  {
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendSearch((Action<bool, Error>) ((isSuccess, error) =>
    {
      onFinish();
      if (cb == null)
        return;
      cb(isSuccess);
    }), false);
  }

  private void SetLoungeData(LoungeModel.Lounge lounge, Transform t)
  {
    CharaInfo charaInfo = (CharaInfo) null;
    for (int index = 0; index < lounge.slotInfos.Count; ++index)
    {
      if (lounge.slotInfos[index].userInfo.userId == lounge.ownerUserId)
      {
        charaInfo = lounge.slotInfos[index].userInfo;
        break;
      }
    }
    this.SetLabelText(t, (Enum) LoungeSearchList.UI.LBL_HOST_NAME, charaInfo.name);
    this.SetLabelText(t, (Enum) LoungeSearchList.UI.LBL_HOST_LV, charaInfo.level.ToString());
    this.SetLabelText(t, (Enum) LoungeSearchList.UI.LBL_LOUNGE_NAME, lounge.name);
    string text = StringTable.Get(STRING_CATEGORY.LOUNGE_LABEL, (uint) lounge.label);
    this.SetLabelText(t, (Enum) LoungeSearchList.UI.LBL_LABEL, text);
    this.SetStamp(t, lounge.stampId);
    int num1 = lounge.num + 1;
    int num2 = lounge.slotInfos.Count<PartyModel.SlotInfo>((Func<PartyModel.SlotInfo, bool>) (slotInfo => slotInfo != null && slotInfo.userInfo != null && slotInfo.userInfo.userId != lounge.ownerUserId));
    for (int index = 0; index < 7; ++index)
    {
      bool is_visible = index < num1 - 1;
      this.SetActive(t, (Enum) this.loungeMembers[index], is_visible);
      this.SetToggle(t, (Enum) this.loungeMembers[index], index < num2);
    }
  }

  private void OnQuery_RELOAD()
  {
    GameSection.StayEvent();
    this.StartCoroutine(this.Reload((Action<bool>) (b => GameSection.ResumeEvent(b))));
  }

  private void OnQuery_SELECT_LOUNGE()
  {
    int eventData = (int) GameSection.GetEventData();
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEntry(this.lounges[eventData].id, (Action<bool>) (isSuccess => GameSection.ResumeEvent(isSuccess)));
  }

  private void OnCloseDialog_LoungeSearchSettings() => this.RefreshUI();

  private void SetStamp(Transform root, int stampId)
  {
    if (Singleton<StampTable>.I.GetData((uint) stampId) == null)
      return;
    this.StartCoroutine(this.LoadStamp(root, stampId));
  }

  private IEnumerator LoadStamp(Transform root, int stampId)
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_stamp = load_queue.LoadChatStamp(stampId);
    while (load_queue.IsLoading())
      yield return (object) null;
    if (Object.op_Inequality(lo_stamp.loadedObject, (Object) null))
    {
      Texture2D loadedObject = lo_stamp.loadedObject as Texture2D;
      this.SetActive((Enum) LoungeSearchList.UI.OBJ_SYMBOL, true);
      this.SetTexture(root, (Enum) LoungeSearchList.UI.TEX_STAMP, (Texture) loadedObject);
    }
  }

  protected enum UI
  {
    GRD_LOUNGE,
    STR_NON_LIST,
    LBL_HOST_NAME,
    LBL_HOST_LV,
    TGL_L_MEMBER_1,
    TGL_L_MEMBER_2,
    TGL_L_MEMBER_3,
    TGL_L_MEMBER_4,
    TGL_L_MEMBER_5,
    TGL_L_MEMBER_6,
    TGL_L_MEMBER_7,
    LBL_LOUNGE_NAME,
    LBL_LABEL,
    LBL_STYLE,
    OBJ_SYMBOL,
    TEX_STAMP,
  }
}
