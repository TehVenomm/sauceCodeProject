// Decompiled with JetBrains decompiler
// Type: HomeCharacterBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public abstract class HomeCharacterBase : MonoBehaviour
{
  public Vector3 defaultPosition = Vector3.zero;
  public Quaternion defaultRotation = Quaternion.identity;
  private static readonly PLCA[] talkAnims = new PLCA[3]
  {
    PLCA.IDLE_01,
    PLCA.IDLE_02,
    PLCA.IDLE_03
  };
  protected int sexType;
  protected IHomePeople iHomePeople;
  protected Transform namePlate;
  protected Animator animator;
  protected PlayerAnimCtrl animCtrl;
  protected CapsuleCollider moveCollider;
  protected TransformInterpolator interpolator;
  protected WayPoint wayPoint;
  protected List<WayPoint> wayHistory = new List<WayPoint>();
  protected float waitTime;
  protected float discussionTimer;
  protected HomeCharacterBase.STATE state;
  protected ManualCoroutineList coroutines = new ManualCoroutineList();

  public bool isLoading { get; protected set; }

  public ModelLoaderBase loader { get; protected set; }

  public Transform _transform { get; private set; }

  public Vector3 moveTargetPos { get; protected set; }

  public Vector3 emotionTargetPos { get; set; }

  public void SetHomePeople(IHomePeople homePeople) => this.iHomePeople = homePeople;

  public void SetWaitTime(float time) => this.waitTime = time;

  public void SetWayPoint(WayPoint wayPoint) => this.wayPoint = wayPoint;

  public bool isStop
  {
    get
    {
      return Object.op_Equality((Object) this.animator, (Object) null) || !this.animator.applyRootMotion;
    }
  }

  public virtual FriendCharaInfo GetFriendCharaInfo() => (FriendCharaInfo) null;

  public Transform GetNamePlate() => this.namePlate;

  public void StopDiscussion() => this.discussionTimer = -1f;

  public virtual bool DispatchEvent() => false;

  public void PushOutControll()
  {
    this.state = HomeCharacterBase.STATE.OUT_CONTROLL;
    this.coroutines.Push(1);
  }

  public void PushBackPosition()
  {
    this.state = HomeCharacterBase.STATE.BACK_POSITION;
    this.coroutines.Push(2);
  }

  public void PushLeave()
  {
    this.state = HomeCharacterBase.STATE.LEAVE;
    this.coroutines.Push(3);
  }

  public void PopState()
  {
    this.coroutines.Pop();
    this.state = (HomeCharacterBase.STATE) this.coroutines.Peek();
  }

  public void StopMoving()
  {
    if (this.state == HomeCharacterBase.STATE.STOP)
      return;
    this.state = HomeCharacterBase.STATE.STOP;
    if (this is HomePlayerCharacter)
      this.animCtrl.PlayIdleAnims(this.sexType);
    if (this is LoungeMoveNPC)
      this.animCtrl.PlayDefault();
    this.coroutines.Push(4);
  }

  public void SetVisible(bool is_visible)
  {
    if (Object.op_Equality((Object) this.loader, (Object) null) || this.isLoading)
      return;
    this.loader.SetEnabled(is_visible);
    ((Collider) this.moveCollider).enabled = is_visible;
    if (Object.op_Inequality((Object) this.namePlate, (Object) null))
      ((Component) this.namePlate).gameObject.SetActive(is_visible);
    ((Behaviour) this).enabled = is_visible;
  }

  protected abstract ModelLoaderBase LoadModel();

  protected virtual void InitAnim()
  {
    this.animCtrl = PlayerAnimCtrl.Get(this.animator, this.sexType == 0 ? PLCA.IDLE_01 : PLCA.IDLE_01_F, new Action<PlayerAnimCtrl, PLCA>(this.OnAnimPlay), on_end: new Action<PlayerAnimCtrl, PLCA>(this.OnAnimEnd));
    this.animCtrl.moveAnim = PLCA.WALK;
  }

  protected virtual void InitCollider() => this.SetCollider(1.8f, 0.5f);

  protected void SetCollider(float height, float radius)
  {
    Rigidbody rigidbody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    rigidbody.isKinematic = true;
    rigidbody.drag = 0.0f;
    rigidbody.angularDrag = 100f;
    rigidbody.useGravity = false;
    rigidbody.constraints = (RigidbodyConstraints) 84;
    CapsuleCollider capsuleCollider = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
    capsuleCollider.direction = 1;
    capsuleCollider.height = height;
    capsuleCollider.radius = radius;
    capsuleCollider.center = new Vector3(0.0f, height * 0.5f, 0.0f);
    this.moveCollider = capsuleCollider;
  }

  protected virtual bool IsVisibleNamePlate() => true;

  protected virtual void OnAnimPlay(PlayerAnimCtrl anim_ctrl, PLCA anim)
  {
    this.animator.applyRootMotion = anim == anim_ctrl.moveAnim;
  }

  protected void OnAnimEnd(PlayerAnimCtrl anim_ctrl, PLCA anim)
  {
    if (this is LoungeMoveNPC)
      this.animCtrl.PlayDefault();
    else
      this.animCtrl.PlayIdleAnims(this.sexType);
  }

  private void Awake()
  {
    this.isLoading = true;
    this._transform = ((Component) this).transform;
  }

  private IEnumerator Start()
  {
    this.loader = this.LoadModel();
    if (!(this is HomePlayerCharacter))
    {
      while (this.loader.IsLoading())
        yield return (object) null;
      this.CreateNamePlate();
      this.UpdateNamePlatePos();
      this.InitCollider();
      this.ChangeScale();
      this.interpolator = ((Component) this).gameObject.AddComponent<TransformInterpolator>();
      this.animator = this.loader.GetAnimator();
      if (!Object.op_Equality((Object) this.animator, (Object) null))
      {
        ((Component) this.animator).gameObject.AddComponent<RootMotionProxy>();
        this.InitAnim();
        this.coroutines.Add(new ManualCoroutine(0, (MonoBehaviour) this, this.DoFreeMove(), false));
        this.coroutines.Add(new ManualCoroutine(1, (MonoBehaviour) this, this.DoOutControll(), false));
        this.coroutines.Add(new ManualCoroutine(2, (MonoBehaviour) this, this.DoBackPosition(), false));
        this.coroutines.Add(new ManualCoroutine(3, (MonoBehaviour) this, this.DoLeave(), false));
        this.coroutines.Add(new ManualCoroutine(4, (MonoBehaviour) this, this.DoStop(), false));
        this.coroutines.Push(0);
        this.isLoading = false;
      }
    }
    else
    {
      this.isLoading = false;
      while (this.loader.IsLoading())
        yield return (object) null;
      this.CreateNamePlate();
      this.UpdateNamePlatePos();
      this.InitCollider();
      this.ChangeScale();
      this.interpolator = ((Component) this).gameObject.AddComponent<TransformInterpolator>();
      this.animator = this.loader.GetAnimator();
      if (!Object.op_Equality((Object) this.animator, (Object) null))
      {
        ((Component) this.animator).gameObject.AddComponent<RootMotionProxy>();
        this.InitAnim();
        this.coroutines.Add(new ManualCoroutine(0, (MonoBehaviour) this, this.DoFreeMove(), false));
        this.coroutines.Add(new ManualCoroutine(1, (MonoBehaviour) this, this.DoOutControll(), false));
        this.coroutines.Add(new ManualCoroutine(2, (MonoBehaviour) this, this.DoBackPosition(), false));
        this.coroutines.Add(new ManualCoroutine(3, (MonoBehaviour) this, this.DoLeave(), false));
        this.coroutines.Add(new ManualCoroutine(4, (MonoBehaviour) this, this.DoStop(), false));
        this.coroutines.Push(0);
      }
    }
  }

  protected virtual void CreateNamePlate()
  {
    if (this.GetFriendCharaInfo() == null)
      return;
    this.namePlate = MonoBehaviourSingleton<UIManager>.I.common.CreateNamePlate(this.GetFriendCharaInfo().name);
  }

  protected virtual void ChangeScale()
  {
  }

  private void LateUpdate() => this.UpdateNamePlatePos();

  protected virtual void UpdateNamePlatePos()
  {
    if (Object.op_Equality((Object) this.namePlate, (Object) null))
      return;
    if (!GameSaveData.instance.headName)
    {
      ((Component) this.namePlate).gameObject.SetActive(false);
    }
    else
    {
      Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(Vector3.op_Addition(this._transform.position, new Vector3(0.0f, 1.9f, 0.0f))));
      if ((double) worldPoint.z >= 0.0 && this.IsVisibleNamePlate())
      {
        worldPoint.z = 0.0f;
        ((Component) this.namePlate).gameObject.SetActive(true);
        this.namePlate.position = worldPoint;
      }
      else
        ((Component) this.namePlate).gameObject.SetActive(false);
    }
  }

  private bool CheckBackPosition(Vector3 startDir)
  {
    Vector3 vector3 = Vector3.op_Subtraction(this.defaultPosition, this._transform.position);
    ((Vector3) ref vector3).Normalize();
    Vector3 position = this._transform.position;
    if ((double) position.x < (double) this.defaultPosition.x || (double) position.z > (double) this.defaultPosition.z)
      return true;
    this._transform.rotation = Quaternion.AngleAxis(-Vector3.Angle(vector3, Vector3.forward), Vector3.up);
    return false;
  }

  protected virtual void OnDestroy()
  {
    if (AppMain.isApplicationQuit)
      return;
    if (this.iHomePeople != null)
      this.iHomePeople.OnDestroyHomeCharacter(this);
    if (!Object.op_Inequality((Object) this.namePlate, (Object) null))
      return;
    Object.DestroyImmediate((Object) ((Component) this.namePlate).gameObject);
    this.namePlate = (Transform) null;
  }

  private IEnumerator DoFreeMove()
  {
    while (true)
    {
      Vector2 vector2;
      while ((double) this.waitTime <= 0.0)
      {
        if ((double) this.discussionTimer != 0.0)
        {
          if ((double) this.discussionTimer < 0.0)
          {
            if (this is HomeNPCCharacter && Object.op_Inequality((Object) this.iHomePeople.selfChara, (Object) null))
            {
              HomeNPCCharacter npc = (HomeNPCCharacter) this;
              if (Object.op_Inequality((Object) npc, (Object) null) && npc.nearAnim != PLCA.IDLE_01)
              {
                vector2 = Vector2.op_Subtraction(this.iHomePeople.selfChara._transform.position.ToVector2XZ(), this._transform.position.ToVector2XZ());
                if ((double) ((Vector2) ref vector2).sqrMagnitude < 9.0)
                  this.PlayNearAnim(npc);
                else if (this.animCtrl.playingAnim != this.animCtrl.defaultAnim)
                  this.animCtrl.PlayDefault();
                yield return (object) null;
                continue;
              }
            }
            if (this is HomePlayerCharacter)
            {
              if (Random.Range(0, 10) == 0)
              {
                this.animCtrl.Play(PlayerAnimCtrl.emotionAnims);
                this.discussionTimer = Random.Range(2f, 4f);
              }
              else if (Random.Range(0, 2) == 0)
              {
                this.animCtrl.Play(PlayerAnimCtrl.talkAnims);
                this.discussionTimer = Random.Range(5f, 10f);
              }
              else
              {
                this.animCtrl.PlayIdleAnims(this.sexType);
                this.discussionTimer = Random.Range(3f, 6f);
              }
            }
          }
          else
            this.discussionTimer -= Time.deltaTime;
          yield return (object) null;
        }
        else
        {
          this.SetupNextWayPoint();
          yield return (object) null;
          this.moveTargetPos = GameSceneGlobalSettings.GetCurrentIHomeManager().IHomePeople.GetTargetPos(this, this.wayPoint);
          while (true)
          {
            this.animCtrl.PlayMove();
            Vector3 vector3 = Vector3.op_Subtraction(this.moveTargetPos, this._transform.position);
            vector2 = vector3.ToVector2XZ();
            Quaternion quaternion = Quaternion.LookRotation(((Vector2) ref vector2).normalized.ToVector3XZ());
            float y = ((Quaternion) ref quaternion).eulerAngles.y;
            float num = 0.0f;
            this._transform.eulerAngles = new Vector3(0.0f, Mathf.SmoothDampAngle(this._transform.eulerAngles.y, y, ref num, 0.1f), 0.0f);
            if ((double) ((Vector3) ref vector3).magnitude >= 0.75)
              yield return (object) null;
            else
              break;
          }
          if (((Object) this.wayPoint).name.StartsWith("LEAF"))
          {
            Object.Destroy((Object) ((Component) this).gameObject);
            yield break;
          }
          if (((Object) this.wayPoint).name.StartsWith("WAIT"))
          {
            while (true)
            {
              float y = ((Component) this.wayPoint).transform.eulerAngles.y;
              float num1 = 0.0f;
              this._transform.eulerAngles = new Vector3(0.0f, Mathf.SmoothDampAngle(this._transform.eulerAngles.y, y, ref num1, 0.1f), 0.0f);
              float num2 = Mathf.Abs(num1);
              if ((double) num2 > 15.0)
                this.animCtrl.Play(PLCA.WALK);
              else if (!string.IsNullOrEmpty(this.wayPoint.waitAnimStateName))
                this.animCtrl.Play(PlayerAnimCtrl.StringToEnum(this.wayPoint.waitAnimStateName));
              else if (this is LoungeMoveNPC)
                this.animCtrl.PlayDefault();
              else
                this.animCtrl.PlayIdleAnims(this.sexType);
              this.animator.applyRootMotion = false;
              if ((double) num2 >= 0.0099999997764825821)
                yield return (object) null;
              else
                break;
            }
            this.animCtrl.PlayIdleAnims(this.sexType);
            this.waitTime = Random.Range(3f, 8f);
          }
          else if (((Object) this.wayPoint).name == "CENTER")
            this.waitTime = Random.Range(-3f, 8f);
        }
      }
      this.WaitInFreeMove();
      yield return (object) null;
    }
  }

  protected virtual void PlayNearAnim(HomeNPCCharacter npc) => this.animCtrl.Play(npc.nearAnim);

  private void WaitInFreeMove()
  {
    this.waitTime -= Time.deltaTime;
    if (!string.IsNullOrEmpty(this.wayPoint.waitAnimStateName))
      this.animCtrl.Play(PlayerAnimCtrl.StringToEnum(this.wayPoint.waitAnimStateName));
    else if (this is LoungeMoveNPC)
      this.animCtrl.PlayDefault();
    else
      this.animCtrl.PlayIdleAnims(this.sexType);
  }

  private void SetupNextWayPoint()
  {
    if (!this.wayHistory.Contains(this.wayPoint))
      this.wayHistory.Add(this.wayPoint);
    List<WayPoint> wayPointList = new List<WayPoint>();
    int index = 0;
    for (int length = this.wayPoint.links.Length; index < length; ++index)
    {
      WayPoint link = this.wayPoint.links[index];
      if (!Object.op_Equality((Object) link, (Object) null) && !this.wayHistory.Contains(link))
        wayPointList.Add(link);
    }
    if (wayPointList.Count > 0)
      this.wayPoint = wayPointList[Random.Range(0, wayPointList.Count)];
    else
      this.wayPoint = this.wayPoint.links[Random.Range(0, this.wayPoint.links.Length)];
  }

  private IEnumerator DoOutControll()
  {
    while (true)
      yield return (object) null;
  }

  private IEnumerator DoBackPosition()
  {
    while (true)
    {
      yield return (object) null;
      this._transform.rotation = Quaternion.AngleAxis(320f, Vector3.up);
      this.animCtrl.SetMoveRunAnim(1);
      this.animCtrl.Play(PLCA.RUN_F);
      Vector3 savePos;
      if ((double) Singleton<HomeThemeTable>.I.GetHomeThemeData(TimeManager.GetNow()).stumblePercent > (double) Random.Range(0, 100))
      {
        yield return (object) new WaitForSeconds(0.5f);
        this.animCtrl.moveAnim = PLCA.STUN;
        this.animCtrl.Play(PLCA.STUN);
        yield return (object) new WaitForSeconds(0.5f);
        savePos = this._transform.position;
        while (!this.animCtrl.IsPlayingIdleAnims(0))
        {
          this._transform.position = savePos;
          yield return (object) null;
        }
        this.animCtrl.moveAnim = PLCA.RUN_F;
        this.animCtrl.Play(PLCA.RUN_F);
      }
      Vector3 startDir = Vector3.op_Subtraction(this.defaultPosition, this._transform.position);
      ((Vector3) ref startDir).Normalize();
      while (!this.CheckBackPosition(startDir))
        yield return (object) null;
      this.animCtrl.PlayDefault();
      yield return (object) new WaitForSeconds(0.3f);
      savePos = this._transform.position;
      this.animCtrl.moveAnim = PLCA.TURN_L;
      this.animCtrl.Play(PLCA.TURN_L);
      yield return (object) null;
      while (!this.animCtrl.IsPlayingIdleAnims(0))
      {
        this._transform.position = savePos;
        yield return (object) null;
      }
      this.animCtrl.SetMoveRunAnim(1);
      this.coroutines.Pop();
      this.state = (HomeCharacterBase.STATE) this.coroutines.Peek();
      savePos = new Vector3();
      startDir = new Vector3();
    }
  }

  private IEnumerator DoLeave()
  {
    while (true)
    {
      yield return (object) null;
      while (!this.animCtrl.IsPlayingIdleAnims(0))
        yield return (object) null;
      ((Component) this).gameObject.SetActive(false);
      this.coroutines.Pop();
      this.state = (HomeCharacterBase.STATE) this.coroutines.Peek();
    }
  }

  private IEnumerator DoStop()
  {
    while (true)
    {
      yield return (object) null;
      if (this is HomePlayerCharacter)
        this.animCtrl.Play(HomeCharacterBase.talkAnims);
      while (!this.GetSelfCharacter().IsEnableControl())
        yield return (object) null;
      if (this is HomePlayerCharacter)
        this.animCtrl.PlayIdleAnims(this.sexType);
      if (this is LoungeMoveNPC)
        this.animCtrl.PlayDefault();
      this.coroutines.Pop();
      this.state = (HomeCharacterBase.STATE) this.coroutines.Peek();
    }
  }

  private HomeSelfCharacter GetSelfCharacter()
  {
    return GameSceneGlobalSettings.GetCurrentIHomeManager().IHomePeople.selfChara;
  }

  public enum STATE
  {
    FREE,
    OUT_CONTROLL,
    BACK_POSITION,
    LEAVE,
    STOP,
    STOP_END,
  }
}
