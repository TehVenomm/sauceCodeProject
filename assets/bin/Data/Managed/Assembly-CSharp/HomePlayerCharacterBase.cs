// Decompiled with JetBrains decompiler
// Type: HomePlayerCharacterBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public abstract class HomePlayerCharacterBase : HomeCharacterBase
{
  protected FriendCharaInfo charaInfo;
  protected bool isSitting;
  protected bool isSit;
  protected bool isStanding;
  protected ChairPoint chairPoint;
  protected bool isPlayingSitAnimation;
  protected bool isPlayingStandAnimation;
  protected ChatAppeal chatAppeal;
  protected StampAppeal stampAppeal;
  private bool isRegistChat;
  private bool isRegistClanChat;
  private Transform head;

  public LOUNGE_ACTION_TYPE CurrentActionType { get; protected set; }

  public virtual int GetUserId()
  {
    return this.GetFriendCharaInfo() == null ? 0 : this.GetFriendCharaInfo().userId;
  }

  public override FriendCharaInfo GetFriendCharaInfo() => this.charaInfo;

  public void SetFriendCharcterInfo(FriendCharaInfo charaInfo) => this.charaInfo = charaInfo;

  protected Transform Head
  {
    get
    {
      if (Object.op_Equality((Object) this.head, (Object) null) && Object.op_Inequality((Object) this.animator, (Object) null))
        this.head = ((Component) this.animator).transform.Find("PLC_Origin/Move/Root/Hip/Spine00/Spine01/Neck/Head");
      return this.head;
    }
  }

  protected override void OnAnimPlay(PlayerAnimCtrl anim_ctrl, PLCA anim)
  {
    if (anim == PLCA.WALK)
      this.animator.applyRootMotion = true;
    else
      this.animator.applyRootMotion = anim == anim_ctrl.moveAnim;
  }

  protected override void OnDestroy()
  {
    if (Object.op_Inequality((Object) this.chatAppeal, (Object) null) && MonoBehaviourSingleton<ChatManager>.IsValid())
    {
      Object.DestroyImmediate((Object) ((Component) this.chatAppeal).gameObject);
      this.chatAppeal = (ChatAppeal) null;
      Object.DestroyImmediate((Object) ((Component) this.stampAppeal).gameObject);
      this.stampAppeal = (StampAppeal) null;
      MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat -= new Action<ChatRoom>(this.RegistOnRecvChat);
      MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat -= new Action<ChatRoom>(this.UnRegistOnRecvChat);
      MonoBehaviourSingleton<ChatManager>.I.OnCreateClanChat -= new Action<ChatRoom>(this.RegistOnRecvChat);
      MonoBehaviourSingleton<ChatManager>.I.OnDestroyClanChat -= new Action<ChatRoom>(this.UnRegistOnRecvChat);
    }
    if (this.isRegistChat && MonoBehaviourSingleton<ChatManager>.IsValid())
      this.UnRegistOnRecvChat(MonoBehaviourSingleton<ChatManager>.I.loungeChat);
    if (this.isRegistClanChat && MonoBehaviourSingleton<ClanMatchingManager>.IsValid())
    {
      this.isRegistClanChat = false;
      MonoBehaviourSingleton<ClanMatchingManager>.I.OnReceiveCharacterMessage -= new Action<ClanChatMessageModel>(this.OnReceiveCharacterMessage);
    }
    base.OnDestroy();
  }

  protected virtual IEnumerator DoSit()
  {
    this.isPlayingSitAnimation = true;
    this.chairPoint.SetSittingCharacter(this);
    Vector3 sitPos = ((Component) this.chairPoint).transform.position;
    if (Object.op_Equality((Object) this.animCtrl, (Object) null))
      this.InitAnim();
    while (true)
    {
      this.animCtrl.Play(PLCA.WALK);
      Vector3 vector3 = Vector3.op_Subtraction(sitPos, this._transform.position);
      Vector2 vector2Xz = vector3.ToVector2XZ();
      Quaternion quaternion = Quaternion.LookRotation(((Vector2) ref vector2Xz).normalized.ToVector3XZ());
      float y = ((Quaternion) ref quaternion).eulerAngles.y;
      float num = 0.0f;
      this._transform.eulerAngles = new Vector3(0.0f, Mathf.SmoothDampAngle(this._transform.eulerAngles.y, y, ref num, 0.1f), 0.0f);
      if ((double) ((Vector3) ref vector3).magnitude >= 0.15000000596046448)
        yield return (object) null;
      else
        break;
    }
    this._transform.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(((Component) this.chairPoint.dir).transform.position, sitPos));
    this.isSit = true;
    PLCA anim1;
    switch (this.chairPoint.chairType)
    {
      case ChairPoint.CHAIR_TYPE.BENTCH:
        anim1 = this.sexType == 0 ? PLCA.SIT_BENCH : PLCA.SIT_BENCH_F;
        break;
      case ChairPoint.CHAIR_TYPE.SOFA:
        anim1 = this.sexType == 0 ? PLCA.SIT_SOFA : PLCA.SIT_SOFA_F;
        break;
      default:
        anim1 = this.sexType == 0 ? PLCA.SIT : PLCA.SIT_F;
        break;
    }
    this.animCtrl.Play(anim1);
    AnimatorStateInfo animatorStateInfo;
    while (true)
    {
      animatorStateInfo = this.animCtrl.animator.GetCurrentAnimatorStateInfo(0);
      if (1.0 < (double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime)
        yield return (object) null;
      else
        break;
    }
    while (true)
    {
      animatorStateInfo = this.animCtrl.animator.GetCurrentAnimatorStateInfo(0);
      if (1.0 > (double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime)
        yield return (object) null;
      else
        break;
    }
    this.isPlayingSitAnimation = false;
    PLCA anim2;
    switch (this.chairPoint.chairType)
    {
      case ChairPoint.CHAIR_TYPE.BENTCH:
        anim2 = this.sexType == 0 ? PLCA.SIT_BENCH_IDLE : PLCA.SIT_BENCH_IDLE_F;
        break;
      case ChairPoint.CHAIR_TYPE.SOFA:
        anim2 = this.sexType == 0 ? PLCA.SIT_SOFA_IDLE : PLCA.SIT_SOFA_IDLE_F;
        break;
      default:
        anim2 = this.sexType == 0 ? PLCA.SIT_IDLE : PLCA.SIT_IDLE_F;
        break;
    }
    this.animCtrl.Play(anim2);
  }

  protected virtual IEnumerator StandUp()
  {
    this.isSit = false;
    this.CurrentActionType = LOUNGE_ACTION_TYPE.STAND_UP;
    if (Object.op_Inequality((Object) this.chairPoint, (Object) null))
      this.chairPoint.ResetSittingCharacter();
    PLCA anim;
    switch (this.chairPoint.chairType)
    {
      case ChairPoint.CHAIR_TYPE.BENTCH:
        anim = this.sexType == 0 ? PLCA.STAND_BENCH_UP : PLCA.STAND_BENCH_UP_F;
        break;
      case ChairPoint.CHAIR_TYPE.SOFA:
        anim = this.sexType == 0 ? PLCA.STAND_SOFA_UP : PLCA.STAND_SOFA_UP_F;
        break;
      default:
        anim = this.sexType == 0 ? PLCA.STAND_UP : PLCA.STAND_UP_F;
        break;
    }
    this.animCtrl.Play(anim);
    GameSceneGlobalSettings.GetCurrentIHomeManager().HomeCamera.ChangeView(HomeCamera.VIEW_MODE.NORMAL);
    this.isSitting = false;
    this.isStanding = true;
    this.isPlayingStandAnimation = true;
    AnimatorStateInfo animatorStateInfo;
    while (true)
    {
      animatorStateInfo = this.animCtrl.animator.GetCurrentAnimatorStateInfo(0);
      if (1.0 < (double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime)
        yield return (object) null;
      else
        break;
    }
    while (true)
    {
      animatorStateInfo = this.animCtrl.animator.GetCurrentAnimatorStateInfo(0);
      if (1.0 > (double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime)
        yield return (object) null;
      else
        break;
    }
    this.isPlayingStandAnimation = false;
    this.isStanding = false;
  }

  public void SetChatEvent()
  {
    this.RegistOnRecvChat(MonoBehaviourSingleton<ChatManager>.I.loungeChat);
    MonoBehaviourSingleton<ChatManager>.I.OnCreateLoungeChat += new Action<ChatRoom>(this.RegistOnRecvChat);
    MonoBehaviourSingleton<ChatManager>.I.OnDestroyLoungeChat += new Action<ChatRoom>(this.UnRegistOnRecvChat);
    MonoBehaviourSingleton<ChatManager>.I.OnCreateClanChat += new Action<ChatRoom>(this.RegistOnRecvChat);
    MonoBehaviourSingleton<ChatManager>.I.OnDestroyClanChat += new Action<ChatRoom>(this.UnRegistOnRecvChat);
    Transform stampAppeal = MonoBehaviourSingleton<UIManager>.I.common.CreateStampAppeal();
    this.stampAppeal = ((Component) stampAppeal).gameObject.AddComponent<StampAppeal>();
    this.stampAppeal.collectUI = stampAppeal;
    this.stampAppeal.CreateCtrlsArray(typeof (StampAppeal.UI));
    Transform chatAppeal = MonoBehaviourSingleton<UIManager>.I.common.CreateChatAppeal();
    this.chatAppeal = ((Component) chatAppeal).gameObject.AddComponent<ChatAppeal>();
    this.chatAppeal.collectUI = chatAppeal;
    this.chatAppeal.CreateCtrlsArray(typeof (ChatAppeal.UI));
  }

  public void SetClanChatEvent()
  {
    if (!MonoBehaviourSingleton<ClanMatchingManager>.IsValid())
      return;
    this.isRegistClanChat = true;
    MonoBehaviourSingleton<ClanMatchingManager>.I.OnReceiveCharacterMessage += new Action<ClanChatMessageModel>(this.OnReceiveCharacterMessage);
  }

  public void OnReceiveCharacterMessage(ClanChatMessageModel model)
  {
    if (model.type != 1 || model.userId != this.GetUserId())
      return;
    int stampId = ClanMatchingManager.convertStringToStampId(model.body);
    if (stampId > 0)
      this.stampAppeal.View(stampId, this.Head ?? this._transform, Object.op_Equality((Object) this.head, (Object) null));
    else
      this.chatAppeal.View(model.body, this.Head ?? this._transform, Object.op_Equality((Object) this.head, (Object) null));
  }

  public void RegistOnRecvChat(ChatRoom room)
  {
    if (this.isRegistChat || room == null)
      return;
    this.isRegistChat = true;
    room.onReceiveStamp += new ChatRoom.OnReceiveStamp(this.ShowStamp);
    room.onReceiveText += new ChatRoom.OnReceiveText(this.ShowChatAppeal);
  }

  public void UnRegistOnRecvChat(ChatRoom room)
  {
    if (!this.isRegistChat || room == null)
      return;
    this.isRegistChat = false;
    room.onReceiveStamp -= new ChatRoom.OnReceiveStamp(this.ShowStamp);
    room.onReceiveText -= new ChatRoom.OnReceiveText(this.ShowChatAppeal);
  }

  public void ShowStamp(
    int userId,
    string userName,
    int stampId,
    string chatItemId,
    bool isOldMessage = false)
  {
    if (userId != this.GetUserId())
      return;
    this.stampAppeal.View(stampId, this.Head ?? this._transform, Object.op_Equality((Object) this.head, (Object) null));
  }

  public void ShowChatAppeal(
    int userId,
    string userName,
    string text,
    string chatItemId,
    bool isOldMessage = false)
  {
    if (userId != this.GetUserId())
      return;
    this.chatAppeal.View(text, this.Head ?? this._transform, Object.op_Equality((Object) this.head, (Object) null));
  }
}
