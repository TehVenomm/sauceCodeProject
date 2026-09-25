// Decompiled with JetBrains decompiler
// Type: Brain
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Brain : MonoBehaviour
{
  public BrainParam param = new BrainParam();
  private Transform _frontTransform;
  private Transform _backTransform;
  public bool canCheckAvoidAttack;
  public bool canAvoidAttack;

  public Character owner { get; private set; }

  public bool isInitialized { get; private set; }

  public OpponentMemory opponentMem { get; private set; }

  public TargetController targetCtrl { get; private set; }

  public MoveController moveCtrl { get; private set; }

  public WeaponController weaponCtrl { get; private set; }

  public StateMachine fsm { get; protected set; }

  public Goal_Think think { get; protected set; }

  public DangerRader dangerRader { get; protected set; }

  protected SpanTimer opponentMemSpanTimer { get; set; }

  protected SpanTimer targetUpdateSpanTimer { get; set; }

  public float rootInternalRedius { get; private set; }

  public float rootFrontDistance { get; private set; }

  public float rootBackDistance { get; private set; }

  protected virtual void Awake()
  {
    this.owner = ((Component) this).GetComponentInParent<Character>();
    this.isInitialized = false;
  }

  protected virtual void OnEnable() => this.Initialize();

  protected virtual void Start()
  {
  }

  protected virtual void Update()
  {
    if (Object.op_Equality((Object) this.owner, (Object) null) || this.owner.isDead || this.owner.IsStone() || !this.isInitialized)
      return;
    if (this.opponentMemSpanTimer != null && this.opponentMemSpanTimer.IsReady())
      this.opponentMem.Update();
    if (this.targetUpdateSpanTimer != null && this.targetUpdateSpanTimer.IsReady())
      this.targetCtrl.UpdateTarget();
    if (this.fsm != null)
      this.fsm.Update();
    if (this.think == null)
      return;
    int num = (int) this.think.Update(this);
  }

  protected virtual void OnDestroy()
  {
    if (this.think == null)
      return;
    Goal.Free((Goal) this.think);
  }

  public void Initialize()
  {
    if (Object.op_Equality((Object) this.owner, (Object) null) || !this.owner.isInitialized || this.isInitialized)
      return;
    this.OnInitialize();
    this.isInitialized = true;
  }

  protected virtual void OnInitialize()
  {
    this.opponentMem = new OpponentMemory(this);
    this.targetCtrl = new TargetController(this);
    this.moveCtrl = new MoveController(this);
    this.weaponCtrl = new WeaponController(this);
    this.opponentMemSpanTimer = new SpanTimer(this.param.thinkParam.opponentMemorySpan);
    this.targetUpdateSpanTimer = new SpanTimer(this.param.thinkParam.targetUpdateSpan);
    this._frontTransform = this.GetFront();
    this._backTransform = this.GetBack();
    this.rootInternalRedius = this.param.sensorParam.internalRadius * this.GetScale();
    Vector2 vector2_1 = Vector2.op_Subtraction(this.frontPositionXZ, this.owner.positionXZ);
    this.rootFrontDistance = ((Vector2) ref vector2_1).magnitude - this.rootInternalRedius;
    Vector2 vector2_2 = Vector2.op_Subtraction(this.backPositionXZ, this.owner.positionXZ);
    this.rootBackDistance = ((Vector2) ref vector2_2).magnitude - this.rootInternalRedius;
  }

  public void ResetInitialized() => this.OnInitialize();

  public virtual float GetScale() => 1f;

  public virtual Transform GetFront() => this.owner._transform;

  public virtual Transform GetBack() => this.owner._transform;

  public Vector2 frontPositionXZ
  {
    get
    {
      Vector3 position = this._frontTransform.position;
      return new Vector2(position.x, position.z);
    }
  }

  public Vector2 frontForwardXZ
  {
    get
    {
      Vector3 forward = this._frontTransform.forward;
      return new Vector2(forward.x, forward.z);
    }
  }

  public Vector2 backPositionXZ
  {
    get
    {
      Vector3 position = this._backTransform.position;
      return new Vector2(position.x, position.z);
    }
  }

  public Vector2 backForwardXZ
  {
    get
    {
      Vector3 forward = this._backTransform.forward;
      return new Vector2(forward.x, forward.z);
    }
  }

  public bool isNonActive => this.fsm != null && this.fsm.currentType == STATE_TYPE.NONACTIVE;

  public virtual List<StageObject> GetTargetObjectList()
  {
    return !MonoBehaviourSingleton<StageObjectManager>.IsValid() ? new List<StageObject>() : MonoBehaviourSingleton<StageObjectManager>.I.objectList;
  }

  public virtual List<StageObject> GetAllyObjectList()
  {
    return !MonoBehaviourSingleton<StageObjectManager>.IsValid() ? new List<StageObject>() : MonoBehaviourSingleton<StageObjectManager>.I.objectList;
  }

  public virtual void HandleEvent(BRAIN_EVENT ev, object param = null)
  {
    if (ev == BRAIN_EVENT.DESTROY_OBJECT)
    {
      StageObject stageObject = (StageObject) param;
      if (this.opponentMem != null)
        this.opponentMem.Remove(stageObject);
      if (this.targetCtrl != null && Object.op_Equality((Object) this.targetCtrl.GetCurrentTarget(), (Object) stageObject))
        this.targetCtrl.MissCurrentTarget();
      if (this.targetCtrl != null && Object.op_Equality((Object) this.targetCtrl.GetAllyTarget(), (Object) stageObject))
        this.targetCtrl.SetAllyTarget((StageObject) null);
      if (this.targetUpdateSpanTimer != null)
        this.targetUpdateSpanTimer.SetTempSpan(0.5f);
    }
    if (this.fsm != null)
      this.fsm.HandleEvent(ev, param);
    if (this.think == null)
      return;
    this.think.HandleEvent(this, ev, param);
  }
}
