// Decompiled with JetBrains decompiler
// Type: AnimEventCollider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AnimEventCollider
{
  protected GameObject gameObject;
  protected AnimEventCollider.AtkColliderHiter colliderHiter;

  public void SetFixedUpdateFlag(bool flag)
  {
    if (!Object.op_Inequality((Object) this.colliderHiter, (Object) null))
      return;
    this.colliderHiter.checkFixedUpdate = flag;
  }

  public void SetFixTransformUpdateFlag(bool flag)
  {
    if (!Object.op_Inequality((Object) this.colliderHiter, (Object) null))
      return;
    this.colliderHiter.isUpdateFixTrans = flag;
  }

  public void ValidTriggerStay()
  {
    if (!Object.op_Inequality((Object) this.colliderHiter, (Object) null))
      return;
    this.colliderHiter.ValidTriggerStay();
  }

  public AttackInfo attackInfo
  {
    get
    {
      return !Object.op_Inequality((Object) this.colliderHiter, (Object) null) ? (AttackInfo) null : this.colliderHiter.attackInfo;
    }
  }

  public bool isReleased
  {
    get
    {
      return Object.op_Inequality((Object) this.colliderHiter, (Object) null) && this.colliderHiter.isReleased;
    }
  }

  public bool Initialize(StageObject stgObj, AnimEventData.EventData data, AttackInfo atkInfo)
  {
    if (data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE && data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_START && data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE && data.id != AnimEventFormat.ID.CONTINUS_ATTACK && data.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI || stgObj == null)
      return false;
    Player player = stgObj as Player;
    int num1 = 13;
    float num2 = 1f;
    if (player != null)
    {
      num1 = 12;
      num2 = player.GetRadiusCustomRate();
    }
    if (Object.op_Equality((Object) this.gameObject, (Object) null))
    {
      this.gameObject = new GameObject();
      ((Object) this.gameObject).name = nameof (AnimEventCollider);
      this.gameObject.layer = num1;
      this.colliderHiter = this.gameObject.AddComponent<AnimEventCollider.AtkColliderHiter>();
    }
    Transform parent = ((Component) stgObj).gameObject.transform;
    if (!string.IsNullOrEmpty(data.stringArgs[1]))
    {
      Transform node = stgObj.FindNode(data.stringArgs[1]);
      if (Object.op_Inequality((Object) node, (Object) null))
        parent = node;
    }
    Vector3 pos;
    // ISSUE: explicit constructor call
    ((Vector3) ref pos).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
    Vector3 rot;
    // ISSUE: explicit constructor call
    ((Vector3) ref rot).\u002Ector(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
    float radius = data.floatArgs[6] * num2;
    float floatArg = data.floatArgs[7];
    this.colliderHiter.SetColliderInfo(stgObj, parent, atkInfo, pos, rot, radius, floatArg);
    return true;
  }

  public void ReserveRelease() => this.colliderHiter.ReserveRelease();

  public void Destroy()
  {
    this.colliderHiter = (AnimEventCollider.AtkColliderHiter) null;
    Object.Destroy((Object) this.gameObject);
    this.gameObject = (GameObject) null;
  }

  public void InitTransformSettings(StageObject stgObj, AnimEventData.EventData _eventData)
  {
    if (Object.op_Equality((Object) stgObj, (Object) null) || Object.op_Equality((Object) this.colliderHiter, (Object) null) || _eventData == null || _eventData.intArgs == null || _eventData.floatArgs == null || _eventData.id != AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI || _eventData.intArgs.Length < 2)
      return;
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(_eventData.floatArgs[8], _eventData.floatArgs[9], _eventData.floatArgs[10]);
    if (_eventData.intArgs[0] != 1)
      return;
    float num1 = Random.Range(-vector3.x, vector3.x);
    float num2 = Random.Range(-vector3.y, vector3.y);
    float num3 = Random.Range(0.0f, 360f) * ((float) Math.PI / 180f);
    Vector3 right = ((Component) stgObj).transform.right;
    Vector3 up = ((Component) stgObj).transform.up;
    Transform transform = ((Component) this.colliderHiter).transform;
    transform.position = Vector3.op_Addition(transform.position, Vector3.op_Addition(Vector3.op_Multiply(num1 * Mathf.Cos(num3), right), Vector3.op_Multiply(num2 * Mathf.Sin(num3), up)));
    ((Component) this.colliderHiter).transform.LookAt(((Component) stgObj).transform.position);
    this.colliderHiter.ForceUpdateCurrentTransformInfo();
  }

  public void OverwriteObjectLayer(int _layer)
  {
    if (Object.op_Equality((Object) this.colliderHiter, (Object) null))
      return;
    ((Component) this.colliderHiter).gameObject.layer = _layer;
  }

  public class AtkColliderHiter : MonoBehaviour, IAttackCollider
  {
    protected StageObject stageObject;
    protected CapsuleCollider capsule;
    protected Vector3 fixPos = Vector3.zero;
    protected Quaternion fixRot = Quaternion.identity;
    protected AttackColliderProcessor colliderProcessor;
    protected AttackHitChecker attackHitChecker;
    public bool checkFixedUpdate = true;
    public bool isUpdateFixTrans = true;

    protected AnimEventCollider.AtkColliderHiter.COLLIDER_INFO colliderInfo { get; set; }

    public StageObject fromObject => this.stageObject;

    public AttackInfo attackInfo { get; protected set; }

    public float timeCount { get; protected set; }

    public bool enabledCollider
    {
      protected set
      {
        if (!Object.op_Inequality((Object) this.capsule, (Object) null))
          return;
        ((Collider) this.capsule).enabled = value;
      }
      get
      {
        return Object.op_Inequality((Object) this.capsule, (Object) null) && ((Collider) this.capsule).enabled;
      }
    }

    public void ValidTriggerStay()
    {
      if (this.colliderProcessor == null)
        return;
      this.colliderProcessor.ValidTriggerStay();
    }

    public bool isReleased
    {
      get => (this.colliderInfo & AnimEventCollider.AtkColliderHiter.COLLIDER_INFO.RELEASED) != 0;
    }

    protected void Awake()
    {
      this.capsule = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
    }

    private void Update() => this.timeCount += Time.deltaTime;

    private void FixedUpdate()
    {
      if (this.isReleased)
        return;
      if (this.isUpdateFixTrans)
      {
        ((Component) this).transform.position = this.fixPos;
        ((Component) this).transform.rotation = this.fixRot;
      }
      if ((this.colliderInfo & AnimEventCollider.AtkColliderHiter.COLLIDER_INFO.RESERVE_RELEASE) != AnimEventCollider.AtkColliderHiter.COLLIDER_INFO.NONE && (this.colliderInfo & AnimEventCollider.AtkColliderHiter.COLLIDER_INFO.FIXED_UPDATE) != AnimEventCollider.AtkColliderHiter.COLLIDER_INFO.NONE)
        this.enabledCollider = false;
      if (!this.enabledCollider)
      {
        if (this.colliderProcessor != null && !this.colliderProcessor.IsBusy())
        {
          this.colliderProcessor.OnDestroy();
          this.colliderProcessor = (AttackColliderProcessor) null;
        }
        if (this.colliderProcessor == null)
          this.colliderInfo |= AnimEventCollider.AtkColliderHiter.COLLIDER_INFO.RELEASED;
      }
      if (!this.checkFixedUpdate)
        return;
      this.colliderInfo |= AnimEventCollider.AtkColliderHiter.COLLIDER_INFO.FIXED_UPDATE;
    }

    public void SetColliderInfo(
      StageObject _stageObject,
      Transform parent,
      AttackInfo info,
      Vector3 pos,
      Vector3 rot,
      float radius,
      float height)
    {
      this.stageObject = _stageObject;
      this.attackInfo = info;
      ((Component) this).transform.parent = parent;
      ((Component) this).transform.localPosition = pos;
      ((Component) this).transform.localEulerAngles = rot;
      ((Component) this).transform.localScale = Vector3.one;
      this.fixPos = ((Component) this).transform.position;
      this.fixRot = ((Component) this).transform.rotation;
      this.capsule.direction = 2;
      this.capsule.radius = radius;
      this.capsule.height = height;
      ((Collider) this.capsule).enabled = true;
      ((Collider) this.capsule).isTrigger = true;
      this.stageObject._rigidbody.WakeUp();
      this.timeCount = 0.0f;
      this.colliderInfo = AnimEventCollider.AtkColliderHiter.COLLIDER_INFO.NONE;
      if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
      {
        this.colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(this.attackInfo, this.stageObject, (Collider) this.capsule, (IAttackCollider) this);
        this.attackHitChecker = this.stageObject.ReferenceAttackHitChecker();
      }
      if (!(this.attackInfo is AttackContinuationInfo attackInfo) || !attackInfo.disableUpdateFixTrans)
        return;
      this.isUpdateFixTrans = false;
    }

    public void ForceUpdateCurrentTransformInfo()
    {
      this.fixPos = ((Component) this).transform.position;
      this.fixRot = ((Component) this).transform.rotation;
    }

    public void ReserveRelease()
    {
      this.colliderInfo |= AnimEventCollider.AtkColliderHiter.COLLIDER_INFO.RESERVE_RELEASE;
    }

    private void OnTriggerEnter(Collider collider)
    {
      if (this.colliderProcessor == null)
        return;
      this.colliderProcessor.OnTriggerEnter(collider);
    }

    private void OnTriggerStay(Collider collider)
    {
      if (this.colliderProcessor == null)
        return;
      this.colliderProcessor.OnTriggerStay(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
      if (this.colliderProcessor == null)
        return;
      this.colliderProcessor.OnTriggerExit(collider);
    }

    public virtual void OnHitTrigger(Collider to_collider, StageObject to_object)
    {
    }

    public virtual float GetTime() => this.timeCount;

    public virtual bool IsEnable() => true;

    public virtual void SortHitStackList(
      List<AttackHitColliderProcessor.HitResult> stack_list)
    {
    }

    public virtual Vector3 GetCrossCheckPoint(Collider from_collider)
    {
      Bounds bounds = from_collider.bounds;
      Vector3 crossCheckPoint = ((Bounds) ref bounds).center;
      Character stageObject = this.stageObject as Character;
      if (Object.op_Inequality((Object) stageObject, (Object) null) && Object.op_Inequality((Object) stageObject.rootNode, (Object) null))
        crossCheckPoint = stageObject.rootNode.position;
      return crossCheckPoint;
    }

    public bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
    {
      return this.attackHitChecker == null || this.attackHitChecker.CheckHitAttack(info, to_collider, to_object);
    }

    public void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
    {
      if (this.attackHitChecker == null)
        return;
      this.attackHitChecker.OnHitAttack(info, hit_param);
    }

    public AttackInfo GetAttackInfo() => this.colliderProcessor.attackInfo;

    public StageObject GetFromObject() => this.colliderProcessor.fromObject;

    protected enum COLLIDER_INFO
    {
      NONE = 0,
      FIXED_UPDATE = 1,
      RESERVE_RELEASE = 2,
      RELEASED = 4,
    }
  }
}
