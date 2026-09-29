// Decompiled with JetBrains decompiler
// Type: HomeSelfCharacter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class HomeSelfCharacter : HomePlayerCharacterBase
{
  public static bool CTRL = true;
  public bool InitedAnimation;
  private InputManager.TouchInfo dragTouchInfo;
  private Action<HomeStageAreaEvent> noticeCallback;
  private HomeStageAreaEvent lastEvent;
  private Vector3 sentPosition;

  public HomeCharacterBase targetChara { get; private set; }

  public int lastTargetNPCID { get; private set; }

  public HomeStageAreaEvent targetEvent { get; private set; }

  public override int GetUserId()
  {
    return !UserInfoManager.IsValidUser() ? 0 : MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
  }

  public bool IsEnableControl()
  {
    return !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing && !GameSceneEvent.IsStay() && !this.isPlayingSitAnimation && !this.isPlayingStandAnimation && (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "HomeTop") || !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "LoungeTop") || !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() != "ClanTop"));
  }

  public void SetNoticeCallback(Action<HomeStageAreaEvent> callback)
  {
    this.noticeCallback = callback;
  }

  public void Sit()
  {
    this.CurrentActionType = LOUNGE_ACTION_TYPE.SIT;
    this.isSitting = true;
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      this.chairPoint = MonoBehaviourSingleton<LoungeManager>.I.TableSet.GetNearSitPoint(this._transform.position);
    if (MonoBehaviourSingleton<ClanManager>.IsValid())
      this.chairPoint = MonoBehaviourSingleton<ClanManager>.I.TableSet.GetNearSitPoint(this._transform.position);
    this.SendMoveToSitPosition(((Component) this.chairPoint).transform.position);
    this.SendSit();
    this.StartCoroutine(this.DoSit());
  }

  protected override IEnumerator StandUp()
  {
    this.CurrentActionType = LOUNGE_ACTION_TYPE.STAND_UP;
    this.SendStandUp();
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

  protected override ModelLoaderBase LoadModel()
  {
    this.lastTargetNPCID = -1;
    this.sexType = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
    PlayerLoader playerLoader = ((Component) this).gameObject.AddComponent<PlayerLoader>();
    PlayerLoadInfo player_load_info = PlayerLoadInfo.FromUserStatus(false, true);
    player_load_info.isNeedToCache = true;
    playerLoader.StartLoad(player_load_info, 8, 99, false, false, true, true, false, false, false, false, SHADER_TYPE.NORMAL, (PlayerLoader.OnCompleteLoad) null);
    return (ModelLoaderBase) playerLoader;
  }

  protected override void InitCollider()
  {
    base.InitCollider();
    ((Component) this).gameObject.GetComponent<Rigidbody>().isKinematic = false;
  }

  protected override void InitAnim()
  {
    base.InitAnim();
    this.animCtrl.moveAnim = this.sexType == 0 ? PLCA.RUN : PLCA.RUN_F;
    this.animCtrl.transitionDuration = 0.15f;
    this.animCtrl.animator.speed = 1f;
    this.InitedAnimation = true;
  }

  private void OnEnable()
  {
    if (!HomeSelfCharacter.CTRL)
      return;
    InputManager.OnDrag += new InputManager.OnTouchDelegate(this.OnDrag);
    InputManager.OnTap += new InputManager.OnTouchDelegate(this.OnTap);
    this.dragTouchInfo = (InputManager.TouchInfo) null;
  }

  private void OnDisable()
  {
    if (!HomeSelfCharacter.CTRL)
      return;
    InputManager.OnDrag -= new InputManager.OnTouchDelegate(this.OnDrag);
    InputManager.OnTap -= new InputManager.OnTouchDelegate(this.OnTap);
  }

  private void OnDrag(InputManager.TouchInfo info)
  {
    if (MonoBehaviourSingleton<UIManager>.I.IsDisable() || !this.IsEnableControl() || this.dragTouchInfo != null && this.dragTouchInfo.enable)
      return;
    this.dragTouchInfo = info;
  }

  private void OnTap(InputManager.TouchInfo info)
  {
    if (!this.IsEnableControl())
      return;
    HomeCamera homeCamera = (HomeCamera) null;
    IHomeManager currentIhomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
    if (currentIhomeManager != null)
      homeCamera = currentIhomeManager.HomeCamera;
    if (homeCamera.viewMode != HomeCamera.VIEW_MODE.NORMAL)
      return;
    HomeCharacterBase homeCharacterBase = (HomeCharacterBase) null;
    HomeStageTouchEvent homeStageTouchEvent = (HomeStageTouchEvent) null;
    if (Object.op_Inequality((Object) this.targetEvent, (Object) null))
      this.targetEvent.DispatchEvent();
    else if (Object.op_Inequality((Object) this.targetChara, (Object) null))
    {
      homeCharacterBase = this.targetChara;
    }
    else
    {
      Ray ray = new Ray();
      if (currentIhomeManager != null)
        ray = homeCamera.targetCamera.ScreenPointToRay(Vector2.op_Implicit(info.position));
      RaycastHit raycastHit;
      if (Physics.Raycast(ray, ref raycastHit, 100f, 259))
      {
        homeCharacterBase = ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<HomeCharacterBase>();
        homeStageTouchEvent = ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<HomeStageTouchEvent>();
      }
    }
    if (Object.op_Inequality((Object) homeCharacterBase, (Object) null))
    {
      if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
        return;
      this.lastTargetNPCID = !(homeCharacterBase is HomeNPCCharacter) ? 0 : ((HomeNPCCharacter) homeCharacterBase).npcInfo.npcID;
      if (!homeCharacterBase.DispatchEvent())
        return;
      homeCharacterBase.StopMoving();
    }
    else
    {
      if (!Object.op_Inequality((Object) homeStageTouchEvent, (Object) null))
        return;
      homeStageTouchEvent.DispatchEvent();
    }
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.animCtrl, (Object) null) || !((Behaviour) this.animCtrl.animator).enabled)
      return;
    Vector3 zero = Vector3.zero;
    Vector3 vector3;
    if (this.dragTouchInfo != null && this.dragTouchInfo.enable && MonoBehaviourSingleton<InputManager>.I.GetActiveInfoCount() == 1 && this.IsEnableControl())
    {
      if (this.isSitting)
      {
        this.StartCoroutine("StandUp");
        vector3 = Vector3.zero;
      }
      else
      {
        vector3 = Quaternion.op_Multiply(MonoBehaviourSingleton<AppMain>.I.mainCameraTransform.rotation, Vector2.op_Subtraction(this.dragTouchInfo.position, this.dragTouchInfo.beginPosition).ToVector3XZ());
        vector3.y = 0.0f;
        ((Vector3) ref vector3).Normalize();
      }
    }
    else
      vector3 = Vector3.zero;
    if ((double) ((Vector3) ref vector3).sqrMagnitude > 0.0099999997764825821)
    {
      this._transform.rotation = Quaternion.Slerp(this._transform.rotation, Quaternion.LookRotation(vector3), 0.5f);
      this.animCtrl.PlayMove();
      this.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
      if (MonoBehaviourSingleton<LoungeManager>.IsValid() || MonoBehaviourSingleton<ClanManager>.IsValid())
        this.SendMove(true);
    }
    else if (!this.isSitting && !this.isStanding)
    {
      this.CurrentActionType = LOUNGE_ACTION_TYPE.NONE;
      if (Vector3.op_Inequality(this.sentPosition, this._transform.position) && (MonoBehaviourSingleton<LoungeManager>.IsValid() || MonoBehaviourSingleton<ClanManager>.IsValid()))
        this.SendMove(false);
      this.animCtrl.PlayDefault();
    }
    if (!this.IsEnableControl())
      return;
    RaycastHit raycastHit;
    if (Physics.Raycast(Vector3.op_Addition(this._transform.localPosition, new Vector3(0.0f, 50f, 0.0f)), Vector3.down, ref raycastHit, 50f, 4))
    {
      HomeStageAreaEvent component = ((Component) ((RaycastHit) ref raycastHit).collider).GetComponent<HomeStageAreaEvent>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      if (this.noticeCallback != null)
        this.noticeCallback(component);
      if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
        return;
      float num = component.defaultRadius * component.defaultRadius;
      Vector2 vector2 = Vector2.op_Subtraction(component._transform.TransformPoint(component._collider.center).ToVector2XZ(), this._transform.localPosition.ToVector2XZ());
      if ((double) ((Vector2) ref vector2).sqrMagnitude <= (double) num)
      {
        if (!Object.op_Inequality((Object) this.lastEvent, (Object) component))
          return;
        component.DispatchEvent();
        this.lastEvent = component;
      }
      else
        this.lastEvent = (HomeStageAreaEvent) null;
    }
    else
    {
      if (this.noticeCallback != null)
        this.noticeCallback((HomeStageAreaEvent) null);
      this.lastEvent = (HomeStageAreaEvent) null;
    }
  }

  private void SendMove(bool isMoving)
  {
    float num = isMoving ? 5f : 0.3f;
    if ((double) Vector3.Distance(this.sentPosition, this._transform.position) <= (double) num)
      return;
    Lounge_Model_RoomMove model = new Lounge_Model_RoomMove();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.pos = this._transform.position;
    if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
      MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomMove>(model);
    if (ClanMatchingManager.IsValidInClan() && MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
      MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_RoomMove>(model);
    this.sentPosition = model.pos;
  }

  private void SendMoveToSitPosition(Vector3 pos)
  {
    Lounge_Model_RoomMove model = new Lounge_Model_RoomMove();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.pos = pos;
    if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
      MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomMove>(model);
    if (MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
      MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_RoomMove>(model);
    this.sentPosition = model.pos;
  }

  private void SendSit()
  {
    Lounge_Model_RoomAction model = new Lounge_Model_RoomAction();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.aid = 1;
    if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
      MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomAction>(model);
    if (!MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
      return;
    MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_RoomAction>(model);
  }

  private void SendStandUp()
  {
    Lounge_Model_RoomAction model = new Lounge_Model_RoomAction();
    model.id = 1005;
    model.cid = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    model.aid = 2;
    if (MonoBehaviourSingleton<LoungeNetworkManager>.IsValid())
      MonoBehaviourSingleton<LoungeNetworkManager>.I.SendBroadcast<Lounge_Model_RoomAction>(model);
    if (!MonoBehaviourSingleton<ClanNetworkManager>.IsValid())
      return;
    MonoBehaviourSingleton<ClanNetworkManager>.I.SendBroadcast<Lounge_Model_RoomAction>(model);
  }

  public LOUNGE_ACTION_TYPE GetActionType() => this.CurrentActionType;
}
