// Decompiled with JetBrains decompiler
// Type: Character
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
public class Character : StageObject, IAnimEvent
{
  public static readonly string[] motionStateName = new string[16 /*0x10*/]
  {
    "",
    "end",
    "idle",
    "walk",
    "rotate_l",
    "rotate_r",
    "damage",
    "dead",
    "paralyze",
    "move_side_r",
    "move_side_l",
    "hide",
    "hide_end",
    "move_point",
    "move_lookat",
    "attack_{0:00}"
  };
  public static readonly string poseStateName = "pose";
  private static Dictionary<string, int> motionHashCaches = new Dictionary<string, int>();
  public const string ANIMATOR_DEF_LAYER_NAME = "Base Layer.";
  protected const string ANIMATOR_NEXT_TRIGGER_NAME = "next";
  private static readonly Character.MotionHashTable motionHash;
  protected string lastAnimTrigger;
  protected List<string> changeTriggerList = new List<string>();
  protected bool periodicSyncActionPositionFlag;
  protected float periodicSyncActionPositionLastTime;
  protected List<Character.PeriodicSyncActionPositionInfo> periodicSyncActionPositionList = new List<Character.PeriodicSyncActionPositionInfo>();
  protected StageObject periodicSyncTarget;
  protected bool enableRootMotion = true;
  protected bool enableEventMove;
  protected Vector3 eventMoveVelocity = Vector3.zero;
  protected float eventMoveTimeCount;
  public AnimEventData animEventData;
  protected CharacterStampCtrl stepCtrl;
  [Tooltip("最短移動回転時間（回転始動と終了のスムーズに関係")]
  public float moveRotateMinimumTime = 0.3f;
  [Tooltip("移動回転最大速度（角度/s")]
  public float moveRotateMaxSpeed = 60f;
  [Tooltip("移動回転最大速度変動掛け率")]
  public float actionMoveRotateMaxSpeedRate = 1f;
  [Tooltip("移動停止範囲")]
  public float moveStopRange = 5f;
  protected float moveSyncTime;
  protected float moveSyncDirection;
  protected float moveSyncDirectionTime;
  protected bool moveSyncEnd;
  protected float moveSyncEndDirection;
  public int moveSyncMotionID;
  protected Vector3 moveBeforePos = Vector3.zero;
  protected float moveNowDistance;
  protected float moveMaxDistance;
  [Tooltip("最短回転時間（回転始動と終了のスムーズに関係")]
  public float rotateMinimumTime = 0.1f;
  [Tooltip("回転最大速度（角度/s")]
  public float rotateMaxSpeed = 120f;
  protected int rotateSign;
  protected float rotateVelocity;
  protected float rootRotationRate = 1f;
  protected int rotateTargetCnt;
  protected bool rotateTargetEnd;
  protected GameObject damegeRemainEffect;
  protected float rotateEventSpeed;
  protected float rotateEventDirection;
  protected bool rotateEventKeep;
  protected bool rotateToTargetFlag;
  protected float rotateToTargetDiffAngle;
  protected bool rotateSafeMode;
  private Vector3 _velocity = Vector3.zero;
  public Character.VELOCITY_TYPE velocityType;
  protected float actionMoveRate = 1f;
  protected float rootMotionMoveRate = 1f;
  protected List<Character.DelayReactionInfo> m_reactionDelayList = new List<Character.DelayReactionInfo>();
  protected bool isReactionDelaySet;
  private XorInt _hpMax = (XorInt) 0;
  private XorInt _hp = (XorInt) 0;
  protected int localDamage;
  protected XorInt m_shieldHpMax = (XorInt) 0;
  public XorInt m_shieldHp = (XorInt) 0;
  protected AttackInfo[] attackInfos;
  private XorFloat _damageHealRate;
  private XorFloat _attackWeakRate;
  private XorFloat _attackDownRate;
  private XorFloat _downPowerWeak;
  private XorFloat _downPowerSimpleWeak;
  private XorFloat _elementWeakRate;
  private XorFloat _elementSkillWeakRate;
  private XorFloat _skillWeakRate;
  private XorFloat _healWeakRate;
  private XorFloat _elementSpAttackWeakRate;
  private List<AttackColliderObject> m_exAtkColliderObjectList = new List<AttackColliderObject>();
  protected bool[] objectTypeAutoDelete = new bool[4]
  {
    true,
    false,
    true,
    true
  };
  protected List<List<GameObject>> objectList;
  protected float hitStopTimer = float.MinValue;
  protected AnimEventProcessor animEventProcessor;
  protected bool animUpdatePhysics;
  protected List<string> animatorBoolList = new List<string>();
  protected List<AnimEventCollider> animEventColliderList = new List<AnimEventCollider>();
  protected AttackHitChecker attackHitChecker = new AttackHitChecker();
  protected bool referenceCheckerFlag;
  protected List<string> hideRendererList = new List<string>();
  protected bool isPlayingEndMotion;
  private bool isImmortal;
  public ContinusAttackParam continusAttackParam;
  public List<AnimEventData.EventData> continusAtkEventDataList = new List<AnimEventData.EventData>();
  public BuffParam buffParam;
  protected float buffSyncLastTime;
  public BadStatus badStatusMax = new BadStatus();
  public BadStatus badStatusBase = new BadStatus();
  protected float paralyzeTime;
  public float paralyzeEffectScale = 1f;
  public string paralyzeEffectName = "ef_btl_wyvern_paralyz_01";
  protected Transform paralyzeEffectTrans;
  protected string nowAnimCtrlName;
  protected string nextAnimCtrlName;
  protected int nextMotionHash;
  protected float nextMotionTransitionTime = -1f;
  protected GameObject actionRendererModel;
  protected string actionRendererNodeName;
  protected Transform actionRendererInstance;
  protected float actMotionStartTime = -1f;
  public bool onTheGround = true;
  public bool isUseInvincibleBuff;
  public float actionReceiveDamageRate = 1f;
  public bool isUseInvincibleBadStatusBuff;
  private List<GameObject> hittingIceFloor = new List<GameObject>(10);
  public const float FREEZE_START_NORMALIZED_TIME = 0.1f;
  public const float FREEZE_EFFECT_HEIGHT = 30f;
  public const float FREEZE_EFFECT_SPEED = 5f;
  private Renderer[] m_rendererList;
  private GameObject m_effectFreeze;
  private float m_freezeTimer;
  private float m_freezeHeight;
  private float m_emissionRadius;
  protected bool m_isStopMotionByDebuff;
  public float stopMotionByDebuffNormalizedTime = -1f;
  protected List<Character.ACTION_ID> shadowSealingStackDebuff = new List<Character.ACTION_ID>();
  protected GameObject m_effectElectricShock;
  private const float DEFAULT_MOVE_ANGLE = 45f;
  private const float DEFAULT_MOVE_ANGLE_SPEED_MAX = 20f;
  private float m_moveAngle_deg = 45f;
  private float m_moveAngleSpeed_deg = 20f;
  private float m_movedAngle_deg;
  private float m_diffAngle_deg;
  private int m_moveAngleSign;
  public static readonly Vector3 DEFAULT_MOVE_POINT = Vector3.zero;
  protected int m_rotateForActMotionId = 4;
  protected float m_rotateForActTime;
  protected float m_rotateForActFinishTime;
  protected Quaternion m_rotateForActStart_Quat = Quaternion.identity;
  protected Quaternion m_rotateForActEnd_Quat = Quaternion.identity;
  protected Vector3 m_moveLookAtInitTargetDir = Vector3.zero;
  protected static StringBuilder stateNameBuilder = new StringBuilder(1024 /*0x0400*/);
  private bool dbgTimeCountFlag;
  private float dbgTimeCount;

  public override Vector3 _position
  {
    get => !this.isInitialized ? base._position : this._rigidbody.position;
    set
    {
      if (!this.isInitialized)
      {
        base._position = value;
      }
      else
      {
        bool flag = false;
        if ((this._rigidbody.constraints & 2) != null && (double) this._rigidbody.position.x != (double) value.x)
          flag = true;
        if ((this._rigidbody.constraints & 4) != null && (double) this._rigidbody.position.y != (double) value.y)
          flag = true;
        if ((this._rigidbody.constraints & 8) != null && (double) this._rigidbody.position.z != (double) value.z)
          flag = true;
        if (!((Component) this._rigidbody).gameObject.activeInHierarchy)
          flag = true;
        this._rigidbody.position = value;
        if (!flag)
          return;
        this._transform.position = value;
      }
    }
  }

  public override Quaternion _rotation
  {
    get => !this.isInitialized ? base._rotation : this._rigidbody.rotation;
    set
    {
      if (!this.isInitialized)
      {
        base._rotation = value;
      }
      else
      {
        bool flag = false;
        if (!((Component) this._rigidbody).gameObject.activeInHierarchy)
          flag = true;
        this._rigidbody.rotation = value;
        if (!flag)
          return;
        this._transform.rotation = value;
      }
    }
  }

  public override Vector3 _forward
  {
    get
    {
      return !this.isInitialized ? base._forward : Quaternion.op_Multiply(this._rigidbody.rotation, Vector3.forward);
    }
    set
    {
      if (this.isInitialized)
        this._rigidbody.rotation = Quaternion.LookRotation(value);
      else
        base._forward = value;
    }
  }

  public override Vector3 _right
  {
    get
    {
      return !this.isInitialized ? base._right : Quaternion.op_Multiply(this._rigidbody.rotation, Vector3.right);
    }
    set
    {
      if (this.isInitialized)
        this._rigidbody.rotation = Quaternion.FromToRotation(Vector3.right, value);
      else
        base._right = value;
    }
  }

  public override Vector3 _up
  {
    get
    {
      return !this.isInitialized ? base._up : Quaternion.op_Multiply(this._rigidbody.rotation, Vector3.up);
    }
    set
    {
      if (this.isInitialized)
        this._rigidbody.rotation = Quaternion.FromToRotation(Vector3.up, value);
      else
        base._up = value;
    }
  }

  public override void _LookAt(Vector3 pos)
  {
    if (this.isInitialized)
      this._rigidbody.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(pos, this._position));
    else
      base._LookAt(pos);
  }

  public InGameSettingsManager.Character charaParameter { get; private set; }

  public string charaName { get; set; }

  public string fullName { get; set; }

  public Animator animator { get; protected set; }

  public Transform body { get; set; }

  public Transform rootNode { get; protected set; }

  public Character.ACTION_ID actionID { get; protected set; }

  public Character.ACTION_ID lastActionID { get; protected set; }

  public int attackID { get; protected set; }

  public bool isControllable { get; protected set; }

  public bool isDead { get; protected set; }

  public StageObject actionTarget { get; protected set; }

  public Vector3 actionPosition { get; protected set; }

  public bool actionPositionFlag { get; protected set; }

  public bool actionPositionThroughFlag { get; protected set; }

  public Vector3 targetPointPos { get; protected set; }

  public StageObject attackStartTarget { get; protected set; }

  public bool actionPositionWaitSync { get; protected set; }

  public string actionPositionWaitTrigger { get; protected set; }

  public bool directionWaitSync { get; protected set; }

  public string directionWaitTrigger { get; protected set; }

  public List<Character> periodicSyncOwnerList { get; protected set; }

  public Vector3 lerpRotateVec { get; protected set; }

  public Character.MOVE_TYPE moveType { get; protected set; }

  public Vector3 moveTargetPos { get; protected set; }

  public float moveSyncSpeed { get; protected set; }

  public Character.ROTATE_TYPE rotateType { get; protected set; }

  public float rotateDirection { get; protected set; }

  public bool rotateDisableMotion { get; set; }

  public void SetVelocity(Vector3 set_vec, Character.VELOCITY_TYPE type = Character.VELOCITY_TYPE.NONE)
  {
    this._velocity = set_vec;
    this.velocityType = type;
  }

  public Vector3 GetVelocity() => this._velocity;

  public Vector3 externalVelocity { get; protected set; }

  public Vector3 addForce { get; protected set; }

  public bool enableAddForce { get; protected set; }

  public Vector3 addForceBeforePos { get; protected set; }

  public bool waitAddForce { get; protected set; }

  public bool enableMotionCancel { get; protected set; }

  public bool enableMoveSuppress { get; protected set; }

  public bool enableReactionDelay { get; protected set; }

  public int hpMax
  {
    get => (int) this._hpMax;
    set => this._hpMax = (XorInt) value;
  }

  public int hp
  {
    get => (int) this._hp;
    set => this._hp = (XorInt) value;
  }

  public int hpShow
  {
    get
    {
      if (!this.isLocalDamageApply)
        return this.hp;
      int hpShow = this.hp - this.localDamage;
      if (this.isDead)
      {
        if (hpShow < 0)
          hpShow = 0;
      }
      else if (hpShow < 1)
        hpShow = 1;
      return hpShow;
    }
  }

  public bool isLocalDamageApply { get; protected set; }

  public AtkAttribute ShieldTolerance { get; set; }

  public XorInt ShieldHpMax
  {
    get => this.m_shieldHpMax;
    set => this.m_shieldHpMax = value;
  }

  public XorInt ShieldHp
  {
    get => this.m_shieldHp;
    set
    {
      if (this.IsValidShield() && (int) value <= 0)
        this.ActShieldBreak();
      this.m_shieldHp = value;
    }
  }

  public virtual void ActShieldBreak()
  {
  }

  public virtual float GetEffectScaleDependValue() => 1f;

  public AtkAttribute attack { get; protected set; }

  public AtkAttribute tolerance { get; protected set; }

  public AtkAttribute defense { get; protected set; }

  public float damageHealRate
  {
    get => (float) this._damageHealRate;
    protected set => this._damageHealRate = (XorFloat) value;
  }

  public float attackWeakRate
  {
    get => (float) this._attackWeakRate;
    protected set => this._attackWeakRate = (XorFloat) value;
  }

  public float attackDownRate
  {
    get => (float) this._attackDownRate;
    protected set => this._attackDownRate = (XorFloat) value;
  }

  public float downPowerWeak
  {
    get => (float) this._downPowerWeak;
    protected set => this._downPowerWeak = (XorFloat) value;
  }

  public float downPowerSimpleWeak
  {
    get => (float) this._downPowerSimpleWeak;
    protected set => this._downPowerSimpleWeak = (XorFloat) value;
  }

  public float elementWeakRate
  {
    get => (float) this._elementWeakRate;
    protected set => this._elementWeakRate = (XorFloat) value;
  }

  public float elementSkillWeakRate
  {
    get => (float) this._elementSkillWeakRate;
    protected set => this._elementSkillWeakRate = (XorFloat) value;
  }

  public float skillWeakRate
  {
    get => (float) this._skillWeakRate;
    protected set => this._skillWeakRate = (XorFloat) value;
  }

  public float healWeakRate
  {
    get => (float) this._healWeakRate;
    protected set => this._healWeakRate = (XorFloat) value;
  }

  public float elementSpAttackWeakRate
  {
    get => (float) this._elementSpAttackWeakRate;
    protected set => this._elementSpAttackWeakRate = (XorFloat) value;
  }

  public CharacterPacketReceiver characterReceiver => (CharacterPacketReceiver) this.packetReceiver;

  public CharacterPacketSender characterSender => (CharacterPacketSender) this.packetSender;

  public void SetImmortal() => this.isImmortal = true;

  public BadStatus atkBadStatus { get; protected set; }

  public BadStatus badStatusTotal { get; protected set; }

  public EffectPlayProcessor effectPlayProcessor { get; set; }

  public bool isSetAppearPos { get; protected set; }

  public Vector3 appearPos { get; protected set; }

  public AttackTrackingTarget TrackingTargetBullet { get; set; }

  public int SyncRandomSeed { get; set; }

  public StringKeyTable<BulletData> cachedBulletDataTable { get; set; }

  protected Renderer[] _rendererArray
  {
    get => this.m_rendererList;
    set => this.m_rendererList = value;
  }

  public bool IsAbleToInvincibleBuff()
  {
    return !this.isDead && !this.IsValidBuff(BuffParam.BUFFTYPE.AUTO_REVIVE) && !this.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS) && !this.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLECOUNT) && !this.isUseInvincibleBuff;
  }

  public bool IsAbleToInvincibleBadStatusBuff()
  {
    return !this.isDead && !this.IsValidBuff(BuffParam.BUFFTYPE.AUTO_REVIVE) && !this.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLECOUNT) && !this.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS) && !this.isUseInvincibleBadStatusBuff;
  }

  public override void LookAt(Vector3 pos, bool isBlindEnable = false)
  {
    if (isBlindEnable && this.IsValidBuffBlind())
      return;
    pos.y = this._position.y;
    this._LookAt(pos);
  }

  static Character() => Character.motionHash = new Character.MotionHashTable();

  protected override void Awake()
  {
    base.Awake();
    this.objectType = StageObject.OBJECT_TYPE.CHARACTER;
    this.animator = (Animator) null;
    this.body = (Transform) null;
    this.actionID = Character.ACTION_ID.IDLE;
    this.lastActionID = Character.ACTION_ID.NONE;
    this.attackID = 0;
    this.isControllable = true;
    this.periodicSyncOwnerList = new List<Character>();
    this.lerpRotateVec = Vector3.zero;
    this.moveType = Character.MOVE_TYPE.NONE;
    this.moveTargetPos = Vector3.zero;
    this.rotateType = Character.ROTATE_TYPE.NONE;
    this.rotateDirection = 0.0f;
    this.rootRotationRate = 1f;
    this.externalVelocity = Vector3.zero;
    this.addForce = Vector3.zero;
    this.enableAddForce = false;
    this.addForceBeforePos = Vector3.zero;
    this.waitAddForce = false;
    this.enableMotionCancel = false;
    this.enableMoveSuppress = false;
    this.actionPositionFlag = false;
    this.actionPositionWaitSync = false;
    this.actionPositionWaitTrigger = (string) null;
    this.directionWaitSync = false;
    this.directionWaitTrigger = (string) null;
    this.hpMax = 0;
    this.hp = this.hpMax;
    this.attack = new AtkAttribute();
    this.tolerance = new AtkAttribute();
    this.defense = new AtkAttribute();
    this.damageHealRate = 1f;
    this.downPowerWeak = 0.0f;
    this.downPowerSimpleWeak = 0.0f;
    this.actionReceiveDamageRate = 1f;
    this.actionMoveRotateMaxSpeedRate = 1f;
    this.objectList = new List<List<GameObject>>();
    for (int index = 0; index < 4; ++index)
      this.objectList.Add(new List<GameObject>());
    this.atkBadStatus = new BadStatus();
    this.badStatusTotal = new BadStatus();
    this.badStatusMax = new BadStatus(1f);
    this.buffParam = new BuffParam(this);
    this.continusAttackParam = new ContinusAttackParam(this);
    this.effectPlayProcessor = (EffectPlayProcessor) null;
    this.isSetAppearPos = false;
    this.isUseInvincibleBuff = false;
    this.isUseInvincibleBadStatusBuff = false;
    this.cachedBulletDataTable = new StringKeyTable<BulletData>();
    this.charaParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.character;
  }

  protected override void Clear()
  {
    base.Clear();
    this.animator = (Animator) null;
    this.animEventProcessor = (AnimEventProcessor) null;
    int index1 = 0;
    for (int count = this.objectList.Count; index1 < count; ++index1)
    {
      int index2 = 0;
      while (index2 < this.objectList[index1].Count)
      {
        if (Object.op_Equality((Object) this.objectList[index1][index2].transform.parent, (Object) this._transform))
          ++index2;
        else
          this.objectList[index1].RemoveAt(index2);
      }
    }
    this.animatorBoolList.Clear();
    this.changeTriggerList.Clear();
    this.animEventColliderList.ForEach((Action<AnimEventCollider>) (o => o.Destroy()));
    this.animEventColliderList.Clear();
  }

  public override void OnLoadComplete()
  {
    base.OnLoadComplete();
    this.rootNode = Utility.Find(this._transform, "Root");
    if (Object.op_Inequality((Object) this._collider, (Object) null) && Object.op_Equality((Object) this._rigidbody, (Object) null))
      this._rigidbody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    if (this.body != null)
      this.animator = ((Component) this.body).GetComponent<Animator>();
    if (this.animator == null)
      this.animator = ((Component) this).gameObject.GetComponentInChildren<Animator>();
    if (Object.op_Inequality((Object) this.animator, (Object) null))
    {
      this.animator.applyRootMotion = true;
      this.animator.cullingMode = (AnimatorCullingMode) 0;
      this.animator.Update(0.0f);
      this.animator.updateMode = !this.animUpdatePhysics ? (AnimatorUpdateMode) 0 : (AnimatorUpdateMode) 1;
      Transform transform = ((Component) this.animator).transform;
      if (Object.op_Inequality((Object) transform, (Object) this._transform))
      {
        this._transform.localScale = transform.localScale;
        transform.localScale = Vector3.one;
      }
    }
    this.nowAnimCtrlName = (string) null;
    this.nextAnimCtrlName = (string) null;
    this.nextMotionHash = 0;
    this.nextMotionTransitionTime = -1f;
    this.stepCtrl = ((Component) this).gameObject.GetComponentInChildren<CharacterStampCtrl>();
    if (Object.op_Inequality((Object) this.animEventData, (Object) null) && Object.op_Inequality((Object) this.animator, (Object) null))
    {
      this.animEventProcessor = new AnimEventProcessor(this.animEventData, this.animator, (IAnimEvent) this);
    }
    else
    {
      AnimEventComponent component = ((Component) this).gameObject.GetComponent<AnimEventComponent>();
      if (Object.op_Inequality((Object) component, (Object) null) && ((Behaviour) component).enabled && Object.op_Inequality((Object) component.animEventData, (Object) null))
      {
        this.animEventProcessor = new AnimEventProcessor(component.animEventData, this.animator, (IAnimEvent) this);
        Object.Destroy((Object) component);
      }
    }
    this.m_rendererList = ((Component) ((Component) this).transform).GetComponentsInChildren<Renderer>(true);
  }

  protected override void Initialize()
  {
    base.Initialize();
    if (!Object.op_Inequality((Object) this.controller, (Object) null))
      return;
    this.controller.OnCharacterInitialized();
  }

  protected override void Update()
  {
    base.Update();
    this.UpdateAction();
    if ((double) this.hitStopTimer >= 0.0)
    {
      this.hitStopTimer -= Time.deltaTime;
      if ((double) this.hitStopTimer <= 0.0)
        this.SetHitStop(-1f);
    }
    this.continusAttackParam.Update();
    this.buffParam.UpdateConditionsAbility();
    this.buffParam.Update();
    if (this.IsOriginal() && (double) this.buffSyncLastTime != 0.0 && (double) Time.time > (double) this.buffSyncLastTime + (double) this.charaParameter.buffSyncUpdateInterval)
      this.SendBuffSync();
    if (Vector3.op_Inequality(this.lerpRotateVec, Vector3.zero))
    {
      Quaternion quaternion = Quaternion.LookRotation(this.lerpRotateVec);
      float num = this.moveRotateMaxSpeed * this.actionMoveRotateMaxSpeedRate * Time.deltaTime / Mathf.Abs(Vector3.Angle(this._forward, this.lerpRotateVec));
      if ((double) num > 1.0)
      {
        this.lerpRotateVec = Vector3.zero;
        num = 1f;
      }
      this._rotation = Quaternion.Lerp(this._rotation, quaternion, num);
    }
    this.UpdateReactionDelay();
  }

  protected override void FixedUpdate()
  {
    if (this.rotateToTargetFlag)
    {
      if (this.actionPositionFlag)
      {
        Vector3 vector3 = Vector3.op_Subtraction(this.actionPosition, this._position);
        vector3.y = 0.0f;
        if ((double) ((Vector3) ref vector3).magnitude < 0.10000000149011612)
          vector3 = this._forward;
        else if (this.rotateSafeMode && (double) Vector3.Angle(Vector3.op_UnaryNegation(this._forward), vector3) < 1.0)
          vector3 = this._forward;
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        this.rotateEventDirection = ((Quaternion) ref quaternion).eulerAngles.y + this.rotateToTargetDiffAngle;
        if ((double) this.rotateEventSpeed <= 0.0)
          this._rotation = Quaternion.AngleAxis(this.rotateEventDirection, Vector3.up);
      }
      else
      {
        Quaternion rotation = this._rotation;
        this.rotateEventDirection = ((Quaternion) ref rotation).eulerAngles.y;
      }
      if (!this.periodicSyncActionPositionFlag)
        this.rotateToTargetFlag = false;
    }
    if (this.rotateEventKeep)
    {
      if (Object.op_Inequality((Object) this.attackStartTarget, (Object) null))
      {
        Vector3 forward = this._forward;
        forward.y = 0.0f;
        ((Vector3) ref forward).Normalize();
        Vector3 vector3 = Vector3.op_Subtraction(this.attackStartTarget._position, this._position);
        vector3.y = 0.0f;
        int num1 = (double) Vector3.Cross(forward, vector3).y >= 0.0 ? 1 : -1;
        float num2 = Vector3.Angle(forward, vector3);
        Quaternion rotation = this._rotation;
        Vector3 eulerAngles = ((Quaternion) ref rotation).eulerAngles;
        float num3 = num2;
        if ((double) this.rotateEventSpeed > 0.0)
        {
          num3 = this.rotateEventSpeed * Time.deltaTime;
          if ((double) num2 <= (double) num3)
            num3 = num2;
        }
        this._rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + (float) num1 * num3, eulerAngles.z);
      }
    }
    else if ((double) this.rotateEventSpeed != 0.0)
    {
      Vector3 forward = this._forward;
      forward.y = 0.0f;
      ((Vector3) ref forward).Normalize();
      Vector3 vector3 = Quaternion.op_Multiply(Quaternion.AngleAxis(this.rotateEventDirection, Vector3.up), Vector3.forward);
      int num4 = (double) Vector3.Cross(forward, vector3).y >= 0.0 ? 1 : -1;
      float num5 = Vector3.Angle(forward, vector3);
      Quaternion rotation = this._rotation;
      Vector3 eulerAngles = ((Quaternion) ref rotation).eulerAngles;
      float num6 = this.rotateEventSpeed * Time.deltaTime;
      if ((double) num5 <= (double) num6)
      {
        num6 = num5;
        if (!this.rotateToTargetFlag)
        {
          this.rotateEventSpeed = 0.0f;
          this.rotateEventDirection = 0.0f;
        }
      }
      this._rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + (float) num4 * num6, eulerAngles.z);
    }
    if ((this.IsCoopNone() || this.IsOriginal()) && this.periodicSyncActionPositionFlag)
    {
      if (Object.op_Inequality((Object) this.periodicSyncTarget, (Object) this.actionTarget))
        this.SetPeriodicSyncTarget(this.actionTarget);
      float actMotionTime = this.GetActMotionTime();
      if ((double) actMotionTime - (double) this.periodicSyncActionPositionLastTime >= (double) this.charaParameter.periodicSyncActionPositionCheckTime)
      {
        Vector3 targetPosition = this.GetTargetPosition(this.actionTarget);
        bool flag = Object.op_Inequality((Object) this.actionTarget, (Object) null);
        Vector3 actionPosition = this.actionPosition;
        if (Vector3.op_Inequality(targetPosition, actionPosition) || flag != this.actionPositionFlag)
        {
          Character.PeriodicSyncActionPositionInfo info = new Character.PeriodicSyncActionPositionInfo();
          info.applyTime = actMotionTime + this.charaParameter.periodicSyncActionPositionApplyTime;
          if (Object.op_Inequality((Object) this.actionTarget, (Object) null))
          {
            info.actionPosition = this.GetTargetPosition(this.actionTarget);
            info.actionPositionFlag = true;
            this.GetTargetPos(out info.targetPointPos);
          }
          this.AddPeriodicSyncActionPosition(info);
          this.periodicSyncActionPositionLastTime = actMotionTime + this.charaParameter.periodicSyncActionPositionApplyTime;
        }
      }
    }
    if (this.periodicSyncActionPositionList.Count > 0)
    {
      int index = 0;
      while (index < this.periodicSyncActionPositionList.Count)
      {
        double actMotionTime = (double) this.GetActMotionTime();
        Character.PeriodicSyncActionPositionInfo syncActionPosition = this.periodicSyncActionPositionList[index];
        double applyTime = (double) syncActionPosition.applyTime;
        if (actMotionTime >= applyTime)
        {
          this.SetActionPosition(syncActionPosition.actionPosition, syncActionPosition.actionPositionFlag);
          this.targetPointPos = syncActionPosition.targetPointPos;
          this.periodicSyncActionPositionList.RemoveAt(index);
        }
        else
          ++index;
      }
    }
    if ((double) this.eventMoveTimeCount > 0.0)
    {
      this.eventMoveTimeCount -= Time.deltaTime;
      if ((double) this.eventMoveTimeCount <= 0.0)
      {
        this.eventMoveTimeCount = 0.0f;
        this.enableEventMove = false;
        this.enableAddForce = false;
        this.SetVelocity(Vector3.zero);
        this.eventMoveVelocity = Vector3.zero;
      }
    }
    if (this.enableEventMove)
      this.SetVelocity(Quaternion.op_Multiply(Quaternion.LookRotation(this.GetTransformForward()), this.eventMoveVelocity), Character.VELOCITY_TYPE.EVENT_MOVE);
    base.FixedUpdate();
    if (this.animEventProcessor != null)
      this.animEventProcessor.Update();
    this.FixedUpdatePhysics();
    this.UpdateNextMotion();
  }

  protected virtual void FixedUpdatePhysics()
  {
    if (!this.isInitialized)
      return;
    if (this.enableAddForce)
    {
      Vector3 addForceBeforePos = this.addForceBeforePos;
      addForceBeforePos.y = 0.1f;
      Vector3 position1 = this._position;
      position1.y = 0.1f;
      if (Vector3.op_Inequality(addForceBeforePos, position1))
      {
        Vector3 vector3 = Vector3.op_Subtraction(position1, addForceBeforePos);
        if (Physics.Raycast(addForceBeforePos, vector3, ((Vector3) ref vector3).magnitude, 393728 /*0x060200*/))
        {
          addForceBeforePos.y = this._position.y;
          this._position = addForceBeforePos;
        }
        Vector3 position2 = this._position;
        position2.y = 0.0f;
        this.addForceBeforePos = position2;
      }
    }
    else if (!this.IsHitStop())
    {
      float num = 1f;
      if (this.enableMoveSuppress && this.actionPositionFlag && !this.actionPositionThroughFlag)
      {
        Vector3 vector3 = Vector3.op_Subtraction(this.actionPosition, this._position);
        vector3.y = 0.0f;
        if ((double) ((Vector3) ref vector3).magnitude <= (double) this.charaParameter.moveSuppressLength)
          num = this.charaParameter.moveSuppressRate;
      }
      Vector3 vector3_1 = Vector3.op_Multiply(Vector3.op_Multiply(this.GetVelocity(), num), this.actionMoveRate);
      vector3_1.y = this._rigidbody.velocity.y;
      this._rigidbody.velocity = vector3_1;
    }
    else
      this._rigidbody.velocity = Vector3.zero;
    Vector3 position3 = this._position;
    float height = StageManager.GetHeight(position3);
    if (!this.waitAddForce)
    {
      if (Vector3.op_Inequality(this.addForce, Vector3.zero))
      {
        this._rigidbody.velocity = Vector3.zero;
        this._rigidbody.AddForce(Vector3.op_Multiply(this.addForce, 0.02f / Time.fixedDeltaTime));
        if ((double) this.addForce.y > 0.0)
          this._rigidbody.constraints = (RigidbodyConstraints) (this._rigidbody.constraints & -5);
        this.addForce = Vector3.zero;
        this.enableAddForce = true;
        Vector3 position4 = this._position;
        position4.y = 0.0f;
        this.addForceBeforePos = position4;
      }
      else if ((this._rigidbody.constraints & 4) == null && (double) position3.y <= (double) height + 0.029999999329447746 && (double) this._rigidbody.velocity.y <= 0.0)
      {
        this._rigidbody.constraints = (RigidbodyConstraints) (this._rigidbody.constraints | 4);
        Vector3 velocity = this._rigidbody.velocity;
        velocity.y = 0.0f;
        this._rigidbody.velocity = velocity;
        this.enableAddForce = false;
      }
    }
    Vector3 externalVelocity = this.externalVelocity;
    externalVelocity.y = 0.0f;
    Rigidbody rigidbody = this._rigidbody;
    rigidbody.velocity = Vector3.op_Addition(rigidbody.velocity, externalVelocity);
    this.externalVelocity = Vector3.zero;
    if (!this.onTheGround)
      return;
    if ((this._rigidbody.constraints & 4) != null)
    {
      if ((double) Mathf.Abs(position3.y - height) <= 0.0099999997764825821)
        return;
      position3.y = height;
      this._position = position3;
    }
    else
    {
      if ((double) position3.y >= (double) height)
        return;
      position3.y = height;
      this._position = position3;
    }
  }

  protected void UpdateNextMotion()
  {
    if (this.animEventProcessor == null || this.nextMotionHash == 0 || !Object.op_Inequality((Object) this.animator, (Object) null))
      return;
    this.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.PLAY_MOTION;
    string nextAnimCtrlName = this.nextAnimCtrlName;
    int nextMotionHash = this.nextMotionHash;
    float motionTransitionTime = this.nextMotionTransitionTime;
    if ((double) motionTransitionTime < 0.0)
      motionTransitionTime = this.charaParameter.motionTransitionTime;
    this.nextAnimCtrlName = (string) null;
    this.nextMotionHash = 0;
    this.nextMotionTransitionTime = -1f;
    if (nextAnimCtrlName != this.nowAnimCtrlName)
    {
      RuntimeAnimatorController animCtrl = this.GetAnimCtrl(nextAnimCtrlName);
      AnimEventData animEvent = this.GetAnimEvent(nextAnimCtrlName);
      if (Object.op_Inequality((Object) animCtrl, (Object) null) && Object.op_Inequality((Object) animEvent, (Object) null))
      {
        this.animEventProcessor.ChangeAnimCtrl(animCtrl, animEvent);
        this.nowAnimCtrlName = nextAnimCtrlName;
      }
      else
        Log.Error(LOG.INGAME, "Character.UpdateNextMotion() anim_ctrl or anim_event is null. ctrlName = {0}", (object) nextAnimCtrlName);
    }
    if (this.charaName == "Hellish Zaark" && nextMotionHash == 423958061)
      Debug.Log((object) $"temp hash: {(object) nextMotionHash} {this.nowAnimCtrlName}");
    this.animEventProcessor.CrossFade(nextMotionHash, motionTransitionTime);
    this.UpdateAnimatorSpeed();
    if ((double) this.actMotionStartTime >= 0.0)
      return;
    this.actMotionStartTime = Time.time;
  }

  public override void OnDetachedObject(StageObject stage_object)
  {
    base.OnDetachedObject(stage_object);
    if (Object.op_Equality((Object) this.actionTarget, (Object) stage_object))
      this.SetActionTarget((StageObject) null, false);
    if (Object.op_Equality((Object) this.attackStartTarget, (Object) stage_object))
      this.attackStartTarget = (StageObject) null;
    if (Object.op_Equality((Object) this.periodicSyncTarget, (Object) stage_object))
      this.periodicSyncTarget = (StageObject) null;
    int index = this.periodicSyncOwnerList.IndexOf(stage_object as Character);
    if (index >= 0)
      this.periodicSyncOwnerList.RemoveAt(index);
    if (!Object.op_Inequality((Object) this.controller, (Object) null))
      return;
    this.controller.OnDetachedObject(stage_object);
  }

  public override void OnAnimatorMove()
  {
    if (!Object.op_Inequality((Object) this.animator, (Object) null) || !this.animator.applyRootMotion || !this.enableRootMotion || this.enableEventMove || this.enableAddForce)
      return;
    float num = (double) Time.deltaTime > 0.0 ? 1f / Time.deltaTime : 0.0f;
    Vector3 deltaPosition = this.animator.deltaPosition;
    deltaPosition.x *= num;
    deltaPosition.z *= num;
    Vector3 set_vec = Vector3.op_Multiply(deltaPosition, this.rootMotionMoveRate);
    if (Vector3.op_Inequality(this.lerpRotateVec, Vector3.zero))
      this.SetVelocity(Quaternion.op_Multiply(Quaternion.FromToRotation(this._forward, this.lerpRotateVec), set_vec), Character.VELOCITY_TYPE.ROOT_MOTION);
    else
      this.SetVelocity(set_vec, Character.VELOCITY_TYPE.ROOT_MOTION);
    if (!Quaternion.op_Inequality(this.animator.deltaRotation, Quaternion.identity))
      return;
    if ((double) this.rootRotationRate == 1.0)
      this._rotation = Quaternion.op_Multiply(this._rotation, this.animator.deltaRotation);
    else
      this._rotation = Quaternion.Lerp(this._rotation, Quaternion.op_Multiply(this._rotation, this.animator.deltaRotation), this.rootRotationRate);
  }

  public virtual void SetAnimUpdatePhysics(bool enable)
  {
    this.animUpdatePhysics = enable;
    if (!Object.op_Inequality((Object) this.animator, (Object) null))
      return;
    if (this.animUpdatePhysics)
      this.animator.updateMode = (AnimatorUpdateMode) 1;
    else
      this.animator.updateMode = (AnimatorUpdateMode) 0;
  }

  public virtual void SetActionTarget(StageObject target, bool send = true)
  {
    bool flag = false;
    if (Object.op_Inequality((Object) this.actionTarget, (Object) target))
      flag = true;
    this.actionTarget = target;
    if (!(send & flag) || !Object.op_Inequality((Object) this.characterSender, (Object) null))
      return;
    this.characterSender.OnSetActionTarget(target);
  }

  public void SetActionPosition(Vector3 position, bool flag)
  {
    if (!flag || this.IsValidBuffBlind())
    {
      position = Vector3.op_Multiply(this._forward, 3f);
      flag = false;
    }
    this.actionPosition = position;
    this.actionPositionFlag = flag;
  }

  public virtual void UpdateActionPosition(string trigger)
  {
    if (this.IsCoopNone() || this.IsOriginal())
      this.SetAttackActionPosition();
    this.SetChangeTrigger(trigger);
    this.EndWaitingPacket(StageObject.WAITING_PACKET.CHARACTER_UPDATE_ACTION_POSITION);
    this.actionPositionWaitSync = false;
    this.actionPositionWaitTrigger = (string) null;
    if (!Object.op_Inequality((Object) this.characterSender, (Object) null))
      return;
    this.characterSender.OnUpdateActionPosition(trigger);
  }

  public virtual void UpdateDirection(string trigger)
  {
    this.SetChangeTrigger(trigger);
    this.EndWaitingPacket(StageObject.WAITING_PACKET.CHARACTER_UPDATE_DIRECTION);
    this.directionWaitSync = false;
    this.directionWaitTrigger = (string) null;
    if (!Object.op_Inequality((Object) this.characterSender, (Object) null))
      return;
    this.characterSender.OnUpdateDirection(trigger);
  }

  public void AddPeriodicSyncActionPosition(Character.PeriodicSyncActionPositionInfo info)
  {
    if (info == null)
      return;
    int index1 = 0;
    int index2 = 0;
    for (int count = this.periodicSyncActionPositionList.Count; index2 < count && (double) info.applyTime >= (double) this.periodicSyncActionPositionList[index2].applyTime; ++index2)
      ++index1;
    this.periodicSyncActionPositionList.Insert(index1, info);
    if (!Object.op_Inequality((Object) this.characterSender, (Object) null))
      return;
    this.characterSender.OnPeriodicSyncActionPosition(info);
  }

  public void SetHitStop(float time)
  {
    if ((double) time < 0.0)
      time = float.MinValue;
    bool flag = (double) time != -3.4028234663852886E+38;
    if (flag != ((double) this.hitStopTimer != -3.4028234663852886E+38))
    {
      int index1 = 0;
      for (int count1 = this.objectList.Count; index1 < count1; ++index1)
      {
        List<GameObject> gameObjectList = this.objectList[index1];
        int index2 = 0;
        for (int count2 = gameObjectList.Count; index2 < count2; ++index2)
        {
          gameObjectList[index2].GetComponentsInChildren<Trail>(Temporary.trailList);
          int index3 = 0;
          for (int count3 = Temporary.trailList.Count; index3 < count3; ++index3)
            Temporary.trailList[index3].pause = flag;
          Temporary.trailList.Clear();
        }
      }
    }
    if ((double) this.actMotionStartTime >= 0.0 && (double) time > 0.0)
      this.actMotionStartTime += time;
    this.hitStopTimer = time;
    this.UpdateAnimatorSpeed();
  }

  public bool IsHitStop() => (double) this.hitStopTimer > 0.0;

  public bool isPause { private set; get; }

  public void setPause(bool pause)
  {
    this.isPause = pause;
    this.UpdateAnimatorSpeed();
  }

  public void setPauseWithAnim(bool pause)
  {
    if (!pause)
      return;
    this.ActIdle();
  }

  protected virtual float GetAnimatorSpeed()
  {
    if (this.IsHitStop() || this.isPause)
      return 0.0f;
    switch (this.actionID)
    {
      case Character.ACTION_ID.MOVE:
        return this.buffParam.GetMoveSpeed();
      case Character.ACTION_ID.ATTACK:
        return this.buffParam.GetAtkSpeed();
      default:
        return 1f;
    }
  }

  public void UpdateAnimatorSpeed()
  {
    if (!Object.op_Inequality((Object) this.animator, (Object) null))
      return;
    this.animator.speed = this.GetAnimatorSpeed();
  }

  public virtual float GetActMotionTime()
  {
    float actMotionTime = 0.0f;
    if ((double) this.actMotionStartTime >= 0.0)
      actMotionTime = Time.time - this.actMotionStartTime;
    return actMotionTime;
  }

  public virtual bool IsChangeableAction(Character.ACTION_ID action_id)
  {
    return !this.isLoading && (this.isControllable || this.enableMotionCancel);
  }

  public virtual void OnActReaction()
  {
    if (!Object.op_Inequality((Object) this.controller, (Object) null))
      return;
    this.controller.OnActReaction();
  }

  public virtual void ActIdle(bool is_sync = false, float transitionTime = -1f)
  {
    bool flag = false;
    Character.ACTION_ID lastActionId = this.lastActionID;
    if (this.actionID == Character.ACTION_ID.IDLE)
      flag = true;
    this.EndAction();
    if (flag)
      this.lastActionID = lastActionId;
    this.actionID = Character.ACTION_ID.IDLE;
    if (!this.IsPlayingMotion(2))
      this.PlayMotion(2, transitionTime);
    this.isControllable = true;
    if (!Object.op_Inequality((Object) this.characterSender, (Object) null))
      return;
    this.characterSender.OnActIdle(is_sync);
  }

  public virtual void SafeActIdle()
  {
    if (this.isLoading || this.isDead || !this.CanSafeActIdle())
      return;
    this.ActIdle();
  }

  protected virtual bool CanSafeActIdle() => true;

  public void SetLerpRotation(Vector3 velocity)
  {
    velocity.y = 0.0f;
    this.lerpRotateVec = velocity;
  }

  public bool IsArrivalPosition(Vector3 pos, float margin = 0.0f)
  {
    Vector3 vector3 = Vector3.op_Subtraction(pos, this._position);
    vector3.y = 0.0f;
    return (double) ((Vector3) ref vector3).magnitude < (double) this.moveStopRange + (double) margin;
  }

  public bool IsHittingIceFloor() => this.hittingIceFloor.Count > 0 && !this.isDead;

  public void OnHitEnterIceFloor(GameObject iceFloor)
  {
    if (this.hittingIceFloor.Contains(iceFloor))
      return;
    this.hittingIceFloor.Add(iceFloor);
  }

  public void OnHitExitIceFloor(GameObject iceFloor) => this.hittingIceFloor.Remove(iceFloor);

  public void ActMoveInertia(ref Vector3 slideVerocity)
  {
    this.SetVelocity(slideVerocity, Character.VELOCITY_TYPE.ACT_MOVE);
  }

  public virtual void ActMoveVelocity(
    Vector3 velocity_,
    float sync_speed,
    Character.MOTION_ID motion_id = Character.MOTION_ID.WALK)
  {
    if (this.actionID != Character.ACTION_ID.MOVE || !this.IsPlayingMotion((int) motion_id))
    {
      this.EndAction();
      this.actionID = Character.ACTION_ID.MOVE;
      if (!this.IsPlayingMotion((int) motion_id))
        this.PlayMotion((int) motion_id);
      if (Object.op_Inequality((Object) this.characterSender, (Object) null))
        this.characterSender.OnActMoveVelocity((int) motion_id);
    }
    this.moveType = Character.MOVE_TYPE.VELOCITY;
    this.moveSyncSpeed = sync_speed;
    this.isControllable = true;
    if (!Vector3.op_Inequality(this.GetVelocity(), velocity_) || !Vector3.op_Inequality(velocity_, Vector3.zero))
      return;
    this.SetVelocity(velocity_, Character.VELOCITY_TYPE.ACT_MOVE);
  }

  public virtual void ActMoveSyncVelocity(float time, Vector3 pos, int motion_id)
  {
    if (this.actionID != Character.ACTION_ID.MOVE || !this.IsPlayingMotion(motion_id))
    {
      this.EndAction();
      this.actionID = Character.ACTION_ID.MOVE;
      this.PlayMotion(motion_id);
    }
    this.moveType = Character.MOVE_TYPE.SYNC_VELOCITY;
    this.enableRootMotion = false;
    this.moveSyncTime = time;
    this.moveTargetPos = pos;
    this.moveSyncEnd = false;
    this.moveSyncMotionID = motion_id;
    Vector3 vector3 = Vector3.op_Subtraction(this.moveTargetPos, this._position);
    vector3.y = 0.0f;
    if ((double) this.moveSyncTime > 0.0)
      this.SetVelocity(Vector3.op_Multiply(((Vector3) ref vector3).normalized, ((Vector3) ref vector3).magnitude / this.moveSyncTime), Character.VELOCITY_TYPE.ACT_MOVE);
    if (Vector3.op_Equality(vector3, Vector3.zero))
    {
      this.moveSyncDirection = float.MinValue;
    }
    else
    {
      Quaternion quaternion = Quaternion.LookRotation(vector3);
      this.moveSyncDirection = ((Quaternion) ref quaternion).eulerAngles.y;
    }
    this.moveSyncDirectionTime = this.charaParameter.moveSyncRotateTime;
    this.StartWaitingPacket(StageObject.WAITING_PACKET.CHARACTER_MOVE_VELOCITY, false, this.charaParameter.moveSendInterval);
  }

  public virtual void SetMoveSyncVelocityEnd(
    float time,
    Vector3 pos,
    float direction,
    float sync_speed,
    int motion_id)
  {
    this.moveSyncTime += time;
    this.moveTargetPos = pos;
    this.moveSyncEnd = true;
    this.moveSyncEndDirection = direction;
    this.moveSyncSpeed = sync_speed;
    this.moveSyncMotionID = motion_id;
    Vector3 vector3 = Vector3.op_Subtraction(this.moveTargetPos, this._position);
    vector3.y = 0.0f;
    if ((double) this.moveSyncSpeed > 0.0)
    {
      float num = ((Vector3) ref vector3).magnitude / sync_speed;
      if ((double) num < (double) this.moveSyncTime)
        this.moveSyncTime = num;
    }
    if ((double) this.moveSyncTime > 0.0)
      this.SetVelocity(Vector3.op_Multiply(((Vector3) ref vector3).normalized, ((Vector3) ref vector3).magnitude / this.moveSyncTime), Character.VELOCITY_TYPE.ACT_MOVE);
    if (Vector3.op_Equality(vector3, Vector3.zero))
    {
      this.moveSyncDirection = float.MinValue;
    }
    else
    {
      Quaternion quaternion = Quaternion.LookRotation(vector3);
      this.moveSyncDirection = ((Quaternion) ref quaternion).eulerAngles.y;
    }
    this.moveSyncDirectionTime = this.charaParameter.moveSyncRotateTime;
    this.EndWaitingPacket(StageObject.WAITING_PACKET.CHARACTER_MOVE_VELOCITY);
  }

  public virtual bool ActMoveToTarget(float max_length = 0.0f, bool fix_rotate = false)
  {
    if (Object.op_Equality((Object) this.actionTarget, (Object) null))
      return false;
    Vector3 vector3 = Vector3.op_Subtraction(this.GetTargetPosition(this.actionTarget), this._position);
    vector3.y = 0.0f;
    if ((double) max_length > 0.0)
    {
      float magnitude = ((Vector3) ref vector3).magnitude;
      if ((double) magnitude > (double) max_length)
        vector3 = Vector3.op_Multiply(vector3, max_length / magnitude);
    }
    return this.ActMoveToPosition(Vector3.op_Addition(this._position, vector3), fix_rotate);
  }

  public virtual bool ActMoveToPosition(Vector3 target_pos, bool fix_rotate = false)
  {
    if (this.IsArrivalPosition(target_pos))
      return false;
    this.EndAction();
    this.actionID = Character.ACTION_ID.MOVE;
    this.PlayMotion(3);
    this.moveTargetPos = target_pos;
    this.moveType = Character.MOVE_TYPE.TO_POSITION;
    if (fix_rotate)
      this.LookAt(this.moveTargetPos, true);
    if (Object.op_Inequality((Object) this.characterSender, (Object) null))
      this.characterSender.OnActMoveToPosition(target_pos);
    return true;
  }

  public virtual bool ActMoveHoming(float max_length = 0.0f)
  {
    if ((this.IsCoopNone() || this.IsOriginal()) && Object.op_Equality((Object) this.actionTarget, (Object) null))
      return false;
    this.EndAction();
    if (this.IsCoopNone() || this.IsOriginal())
      this.SetAttackActionPosition();
    this.actionID = Character.ACTION_ID.MOVE;
    this.PlayMotion(3);
    this.moveType = Character.MOVE_TYPE.HOMING;
    this.periodicSyncActionPositionLastTime = this.GetActMotionTime();
    this.periodicSyncActionPositionFlag = true;
    this.SetPeriodicSyncTarget(this.actionTarget);
    this.moveBeforePos = this._position;
    this.moveMaxDistance = max_length;
    if (Object.op_Inequality((Object) this.characterSender, (Object) null))
      this.characterSender.OnActMoveHoming(max_length);
    return true;
  }

  public virtual bool ActRotateToTarget()
  {
    if (Object.op_Equality((Object) this.actionTarget, (Object) null))
      return false;
    Vector3 vector3 = Vector3.op_Subtraction(this.GetTargetPosition(this.actionTarget), this._position);
    vector3.y = 0.0f;
    if (Vector3.op_Equality(vector3, Vector3.zero))
      return false;
    Quaternion quaternion = Quaternion.LookRotation(vector3);
    return this.ActRotateToDirection(((Quaternion) ref quaternion).eulerAngles.y);
  }

  public virtual bool ActRotateToDirection(float direction)
  {
    float diff_angle = 0.0f;
    int num = this.CalcDiffAngle(direction, ref diff_angle);
    if ((double) diff_angle < 1.0)
      return false;
    this.EndAction();
    this.actionID = Character.ACTION_ID.ROTATE;
    if (!this.rotateDisableMotion)
    {
      if (num > 0)
        this.PlayMotion(5);
      else
        this.PlayMotion(4);
    }
    this.enableRootMotion = false;
    this.rotateType = Character.ROTATE_TYPE.TO_DIRECTION;
    this.rotateDirection = direction;
    this.rotateSign = num;
    this.rotateVelocity = 0.0f;
    if (Object.op_Inequality((Object) this.characterSender, (Object) null))
      this.characterSender.OnActRotate(this.rotateDirection);
    return true;
  }

  public virtual bool ActRotateMotionToTarget(bool keep_rotate = false)
  {
    if (Object.op_Equality((Object) this.actionTarget, (Object) null))
      return false;
    Vector3 vector3 = Vector3.op_Subtraction(this.GetTargetPosition(this.actionTarget), this._position);
    vector3.y = 0.0f;
    ((Vector3) ref vector3).Normalize();
    if (Vector3.op_Equality(vector3, Vector3.zero))
      return false;
    Quaternion quaternion = Quaternion.LookRotation(vector3);
    float y = ((Quaternion) ref quaternion).eulerAngles.y;
    float diff_angle = 0.0f;
    int num1 = this.CalcDiffAngle(y, ref diff_angle);
    double num2 = (double) this.CalcRotateMotionRate(diff_angle);
    int num3 = 0;
    if (keep_rotate)
      num3 = this.rotateTargetCnt + 1;
    bool flag = false;
    float direction = y;
    if (num2 == 1.0 && num3 < this.charaParameter.rotateTargetMaxNum - 1)
    {
      Quaternion rotation = this._rotation;
      direction = ((Quaternion) ref rotation).eulerAngles.y + (float) num1 * 90f;
    }
    else
      flag = true;
    int num4 = this.ActRotateMotionToDirection(direction) ? 1 : 0;
    if (num4 == 0)
      return num4 != 0;
    this.rotateType = Character.ROTATE_TYPE.MOTION_TO_TARGET;
    this.rotateTargetEnd = flag;
    this.rotateTargetCnt = num3;
    return num4 != 0;
  }

  public virtual bool ActRotateMotionToDirection(float direction)
  {
    float diff_angle = 0.0f;
    int num = this.CalcDiffAngle(direction, ref diff_angle);
    if ((double) diff_angle < 1.0)
      return false;
    this.EndAction();
    this.actionID = Character.ACTION_ID.ROTATE;
    if (num > 0)
      this.PlayMotion(5);
    else
      this.PlayMotion(4);
    this.rotateDirection = direction;
    this.rotateSign = num;
    this.rootRotationRate = this.CalcRotateMotionRate(diff_angle);
    this.rotateType = Character.ROTATE_TYPE.MOTION_TO_DIRECTION;
    if (Object.op_Inequality((Object) this.characterSender, (Object) null))
      this.characterSender.OnActRotateMotion(direction);
    return true;
  }

  protected float CalcRotateMotionRate(float diff_angle)
  {
    float num = Mathf.Abs(diff_angle) / 90f;
    if ((double) num > 1.2000000476837158)
      num = 1f;
    return num;
  }

  protected int CalcDiffAngle(float direction, ref float diff_angle)
  {
    Vector3 forward = this._forward;
    forward.y = 0.0f;
    ((Vector3) ref forward).Normalize();
    Vector3 vector3 = Quaternion.op_Multiply(Quaternion.AngleAxis(direction, Vector3.up), Vector3.forward);
    diff_angle = Vector3.Angle(forward, vector3);
    return (double) Vector3.Cross(forward, vector3).y < 0.0 ? -1 : 1;
  }

  public virtual void ActDamage()
  {
    this.EndAction();
    this.actionID = Character.ACTION_ID.DAMAGE;
    this.PlayMotion(6);
    this.OnActReaction();
  }

  public virtual void ActDead(bool force_sync = false, bool recieve = false)
  {
    this.EndAction();
    this.actionID = Character.ACTION_ID.DEAD;
    this.PlayMotion(7);
    this.Die();
    this.OnActReaction();
    if (!(Object.op_Inequality((Object) this.characterSender, (Object) null) & force_sync))
      return;
    this.characterSender.OnActDead();
  }

  public void Die()
  {
    this.hp = 0;
    this.isDead = true;
    this.hitOffFlag |= StageObject.HIT_OFF_FLAG.DEAD;
    this._collider.enabled = false;
    this.buffParam.AllBuffEnd(false);
    this.continusAttackParam.RemoveAll();
  }

  public virtual void VanishLocal() => this.PrepareVanishLocal();

  public virtual void PrepareVanishLocal()
  {
    this.EndAction();
    int hp = this.hp;
    this.Die();
    this.hp = hp;
  }

  public virtual void OnDeadEnd()
  {
  }

  public virtual void ActParalyze()
  {
    if (this.IsDebuffShadowSealing())
    {
      if (this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.PARALYZE))
        return;
      this.shadowSealingStackDebuff.Add(Character.ACTION_ID.PARALYZE);
      if (this.paralyzeEffectTrans == null)
        this.paralyzeEffectTrans = AnimEventFormat.EffectEventExec(AnimEventFormat.ID.EFFECT, new AnimEventData.EventData()
        {
          intArgs = new int[0],
          floatArgs = new float[1]
          {
            this.paralyzeEffectScale
          },
          stringArgs = new string[2]
          {
            this.paralyzeEffectName,
            ""
          }
        }, this._transform, true, new AnimEventFormat.EffectNameAnalyzer(this.EffectNameAnalyzer), new AnimEventFormat.NodeFinder(((StageObject) this).FindNode), this);
    }
    else
    {
      this.EndAction();
      this.actionID = Character.ACTION_ID.PARALYZE;
      this.PlayMotion(8);
    }
    this.OnActReaction();
  }

  protected bool UpdateParalyzeAction()
  {
    if ((double) this.paralyzeTime - (double) Time.time > 0.0)
      return false;
    this.ActParalyzeEnd();
    if (!this.IsDebuffShadowSealing())
      this.SetNextTrigger();
    return true;
  }

  protected virtual void ActParalyzeEnd()
  {
    this.badStatusTotal.paralyze = 0.0f;
    if (!this.IsDebuffShadowSealing())
      return;
    this._EndDebuffAction(Character.ACTION_ID.PARALYZE);
    if (this.paralyzeEffectTrans != null)
    {
      EffectManager.ReleaseEffect(((Component) this.paralyzeEffectTrans).gameObject);
      this.paralyzeEffectTrans = (Transform) null;
    }
    if (!this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.PARALYZE))
      return;
    this.shadowSealingStackDebuff.Remove(Character.ACTION_ID.PARALYZE);
  }

  public bool IsParalyze()
  {
    return this.actionID == Character.ACTION_ID.PARALYZE || this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.PARALYZE);
  }

  public virtual void ActFreezeStart()
  {
    if (this.IsFreeze())
      return;
    if (this.IsDebuffShadowSealing())
    {
      if (!this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.FREEZE))
        this.shadowSealingStackDebuff.Add(Character.ACTION_ID.FREEZE);
    }
    else
    {
      this.EndAction();
      this.actionID = Character.ACTION_ID.FREEZE;
      this.PlayMotion(6, (double) this.stopMotionByDebuffNormalizedTime < 0.0 ? -1f : 0.0f);
    }
    this.CreateFreezeEffect();
    this.SwitchFreezeShader();
    this.m_freezeTimer = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.freezeParam.duration;
    this.m_freezeHeight = 0.0f;
    if (Object.op_Inequality((Object) this._rigidbody, (Object) null))
      this._rigidbody.velocity = Vector3.zero;
    this.rotateEventKeep = false;
    this.rotateToTargetFlag = false;
    this.rotateEventSpeed = 0.0f;
    this.OnActReaction();
  }

  protected virtual void ActFreezeEnd()
  {
    if (!this.IsFreeze())
      return;
    if (Object.op_Inequality((Object) this.m_effectFreeze, (Object) null))
    {
      EffectManager.ReleaseEffect(this.m_effectFreeze);
      this.m_effectFreeze = (GameObject) null;
    }
    this.RestoreShader();
    this.badStatusTotal.freeze = 0.0f;
    if (this.IsDebuffShadowSealing())
    {
      this._EndDebuffAction(Character.ACTION_ID.FREEZE);
      if (!this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.FREEZE))
        return;
      this.shadowSealingStackDebuff.Remove(Character.ACTION_ID.FREEZE);
    }
    else
    {
      this.setPause(false);
      this.m_isStopMotionByDebuff = false;
    }
  }

  protected bool UpdateFreezeAction()
  {
    if (!this.IsFreeze())
      return false;
    this.UpdateFreezeShader();
    this.m_freezeTimer -= Time.deltaTime;
    if ((double) this.m_freezeTimer <= 0.0)
    {
      this.ActFreezeEnd();
      return true;
    }
    float num = 0.1f;
    if ((double) this.stopMotionByDebuffNormalizedTime >= 0.0)
      num = this.stopMotionByDebuffNormalizedTime;
    AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < (double) num || ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash != Animator.StringToHash("Base Layer.damage") || this.m_isStopMotionByDebuff)
      return false;
    this.setPause(true);
    this.m_isStopMotionByDebuff = true;
    return false;
  }

  private void SwitchFreezeShader()
  {
    if (this.m_rendererList == null)
      return;
    Utility.MaterialForEach(this.m_rendererList, (Action<Material>) (material =>
    {
      Shader shader = ResourceUtility.FindShader(((Object) material.shader).name.Replace("enemy_", "freeze_enemy_"));
      if (!Object.op_Inequality((Object) shader, (Object) null))
        return;
      material.shader = shader;
      if (!material.HasProperty("_Height"))
        return;
      material.SetFloat("_Height", 0.0f);
    }));
  }

  private void RestoreShader()
  {
    if (this.m_rendererList == null)
      return;
    Utility.MaterialForEach(this.m_rendererList, (Action<Material>) (material =>
    {
      Shader shader = ResourceUtility.FindShader(((Object) material.shader).name.Replace("freeze_", ""));
      if (!Object.op_Inequality((Object) shader, (Object) null))
        return;
      material.shader = shader;
    }));
  }

  private void UpdateFreezeShader()
  {
    if (this.m_rendererList == null)
      return;
    this.m_freezeHeight += 5f * Time.deltaTime;
    if ((double) this.m_freezeHeight > 30.0)
      this.m_freezeHeight = 30f;
    Utility.MaterialForEach(this.m_rendererList, (Action<Material>) (material =>
    {
      if (!material.HasProperty("_Height"))
        return;
      material.SetFloat("_Height", this.m_freezeHeight);
    }));
  }

  private void CreateFreezeEffect()
  {
    Transform effect = EffectManager.GetEffect("ef_btl_pl_frozen_01", this._transform);
    if (!Object.op_Inequality((Object) effect, (Object) null))
      return;
    ParticleSystem[] componentsInChildren = ((Component) effect).GetComponentsInChildren<ParticleSystem>(true);
    if (componentsInChildren != null)
    {
      this.CalcFreezeEffectEmissionRadius();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        ParticleSystem.ShapeModule shape = componentsInChildren[index].shape;
        ((ParticleSystem.ShapeModule) ref shape).radius = this.m_emissionRadius;
      }
    }
    Transform transform = effect;
    transform.localPosition = Vector3.op_Addition(transform.localPosition, Vector3.op_Multiply(Vector3.up, this.m_emissionRadius));
    this.m_effectFreeze = ((Component) effect).gameObject;
  }

  protected void CalcFreezeEffectEmissionRadius()
  {
    if ((double) this.m_emissionRadius > 0.0)
      return;
    this.m_emissionRadius = this._transform.localScale.x;
    if (Object.op_Equality((Object) this._collider, (Object) null))
      return;
    this.m_emissionRadius *= this.GetFreezeEffectRadiusRate();
  }

  protected virtual float GetFreezeEffectRadiusRate()
  {
    SphereCollider collider1 = this._collider as SphereCollider;
    if (Object.op_Inequality((Object) collider1, (Object) null))
      return collider1.radius;
    CapsuleCollider collider2 = this._collider as CapsuleCollider;
    return Object.op_Inequality((Object) collider2, (Object) null) ? collider2.radius : 1f;
  }

  public bool IsFreeze() => Object.op_Inequality((Object) this.m_effectFreeze, (Object) null);

  protected float GetEmittionRadius() => this.m_emissionRadius;

  public virtual bool IsDebuffShadowSealing() => false;

  public virtual bool IsConcussion() => false;

  public virtual bool IsLightRing() => false;

  protected void CreateElectricShockEffect()
  {
    Transform effect = EffectManager.GetEffect("ef_btl_enm_shock_01", this._transform);
    if (!Object.op_Inequality((Object) effect, (Object) null))
      return;
    ParticleSystem[] componentsInChildren = ((Component) effect).GetComponentsInChildren<ParticleSystem>(true);
    if (componentsInChildren != null)
    {
      this.CalcFreezeEffectEmissionRadius();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        ParticleSystem.ShapeModule shape = componentsInChildren[index].shape;
        ((ParticleSystem.ShapeModule) ref shape).radius = this.m_emissionRadius;
      }
    }
    Transform transform = effect;
    transform.localPosition = Vector3.op_Addition(transform.localPosition, Vector3.op_Multiply(Vector3.up, this.m_emissionRadius));
    this.m_effectElectricShock = ((Component) effect).gameObject;
  }

  public virtual bool IsInkSplash() => false;

  public virtual bool IsStone() => false;

  public virtual void ActAttack(
    int id,
    bool send_packet = true,
    bool sync_immediately = false,
    string _motionLayerName = "",
    string _motionStateName = "")
  {
    this.EndAction();
    this.isControllable = false;
    this.actionID = Character.ACTION_ID.ATTACK;
    this.attackID = id;
    Character.PlayMotionParam playMotionParam = new Character.PlayMotionParam();
    playMotionParam.MotionID = 15 + id;
    playMotionParam.MotionLayerName = string.IsNullOrEmpty(_motionLayerName) ? "Base Layer." : _motionLayerName;
    if (_motionStateName.IsNullOrWhiteSpace())
      this.PlayMotion(playMotionParam);
    else
      this.PlayMotionImmidate((string) null, _motionStateName, 0.0f);
    if (this.IsCoopNone() || this.IsOriginal())
    {
      this.SyncRandomSeed = Random.Range(int.MinValue, int.MaxValue);
      this.SetAttackActionPosition();
    }
    this.attackStartTarget = this.actionTarget;
    if (!send_packet || !Object.op_Inequality((Object) this.characterSender, (Object) null))
      return;
    this.characterSender.OnActAttack(id, sync_immediately, this.SyncRandomSeed, playMotionParam.MotionLayerName, _motionStateName);
  }

  public virtual void SetAttackActionPosition()
  {
    if (Object.op_Inequality((Object) this.actionTarget, (Object) null))
      this.SetActionPosition(this.GetTargetPosition(this.actionTarget), true);
    else
      this.SetActionPosition(Vector3.zero, false);
  }

  protected virtual void UpdateAction()
  {
    switch (this.actionID)
    {
      case Character.ACTION_ID.MOVE:
        if (this.moveType == Character.MOVE_TYPE.TO_POSITION || this.moveType == Character.MOVE_TYPE.HOMING)
        {
          if (this.moveType == Character.MOVE_TYPE.HOMING)
          {
            if (!this.actionPositionFlag)
            {
              this.ActIdle();
              break;
            }
            if ((double) this.moveMaxDistance > 0.0)
            {
              Vector3 vector3 = Vector3.op_Subtraction(this._position, this.moveBeforePos);
              vector3.y = 0.0f;
              this.moveNowDistance += ((Vector3) ref vector3).magnitude;
              this.moveBeforePos = this._position;
              if ((double) this.moveNowDistance >= (double) this.moveMaxDistance)
              {
                this.ActIdle();
                break;
              }
            }
            this.moveTargetPos = this.actionPosition;
          }
          if (this.IsWallStay())
          {
            this.ActIdle(true);
            break;
          }
          if (this.IsArrivalPosition(this.moveTargetPos))
          {
            this.ActIdle();
            break;
          }
          Vector3 vector3_1 = Vector3.op_Subtraction(this.moveTargetPos, this._position);
          vector3_1.y = 0.0f;
          ((Vector3) ref vector3_1).Normalize();
          Vector3 forward = this._forward;
          forward.y = 0.0f;
          ((Vector3) ref forward).Normalize();
          float num1 = Vector3.Angle(forward, vector3_1);
          if ((double) num1 > 90.0)
          {
            this.ActIdle();
            break;
          }
          int num2 = (double) Vector3.Cross(forward, vector3_1).y >= 0.0 ? 1 : -1;
          Quaternion rotation = this._rotation;
          Vector3 eulerAngles = ((Quaternion) ref rotation).eulerAngles;
          float num3 = Mathf.SmoothDampAngle(0.0f, num1 * (float) num2, ref this.rotateVelocity, this.moveRotateMinimumTime, this.moveRotateMaxSpeed, Time.deltaTime);
          this._rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + num3, eulerAngles.z);
          break;
        }
        if (this.moveType == Character.MOVE_TYPE.SYNC_VELOCITY)
        {
          if ((double) this.moveSyncDirection != -3.4028234663852886E+38)
          {
            if ((double) this.moveSyncDirectionTime > 0.0)
              this._rotation = Quaternion.Slerp(this._rotation, Quaternion.AngleAxis(this.moveSyncDirection, Vector3.up), Time.deltaTime / this.moveSyncDirectionTime);
            else
              this._rotation = Quaternion.AngleAxis(this.moveSyncDirection, Vector3.up);
          }
          this.moveSyncTime -= Time.deltaTime;
          this.moveSyncDirectionTime -= Time.deltaTime;
          if ((double) this.moveSyncTime <= 0.0)
          {
            this._position = this.moveTargetPos;
            if (this.moveSyncEnd)
            {
              this._rotation = Quaternion.AngleAxis(this.moveSyncEndDirection, Vector3.up);
              this.ActIdle();
              break;
            }
            break;
          }
          break;
        }
        if (this.moveType == Character.MOVE_TYPE.SIDEWAYS)
        {
          this.UpdateMoveSideAction();
          break;
        }
        break;
      case Character.ACTION_ID.ROTATE:
        if (this.rotateType == Character.ROTATE_TYPE.TO_DIRECTION)
        {
          float diff_angle = 0.0f;
          if (this.CalcDiffAngle(this.rotateDirection, ref diff_angle) != this.rotateSign)
          {
            this.ActIdle();
            return;
          }
          if ((double) diff_angle < 0.10000000149011612)
          {
            this.ActIdle();
            return;
          }
          Quaternion rotation = this._rotation;
          Vector3 eulerAngles = ((Quaternion) ref rotation).eulerAngles;
          float num = Mathf.SmoothDampAngle(0.0f, diff_angle * (float) this.rotateSign, ref this.rotateVelocity, this.rotateMinimumTime, this.rotateMaxSpeed, Time.deltaTime);
          this._rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + num, eulerAngles.z);
          break;
        }
        break;
      case Character.ACTION_ID.PARALYZE:
        this.UpdateParalyzeAction();
        break;
      case Character.ACTION_ID.FREEZE:
        this.UpdateFreezeAction();
        break;
      case Character.ACTION_ID.MOVE_POINT:
        this.UpdateMovePointAction();
        break;
      case Character.ACTION_ID.MOVE_LOOKAT:
        this.UpdateMoveLookAtAction();
        break;
    }
    if (!this.IsPlayingMotion(1))
      return;
    this.OnPlayingEndMotion();
  }

  public float moveAngle_deg
  {
    get => this.m_moveAngle_deg;
    set => this.m_moveAngle_deg = value;
  }

  public float moveAngleSpeed_deg
  {
    get => this.m_moveAngleSpeed_deg;
    set => this.m_moveAngleSpeed_deg = value;
  }

  public virtual bool ActMoveSideways(int moveAngleSign = 0, bool isPacket = false)
  {
    if ((this.IsCoopNone() || this.IsOriginal()) && Object.op_Equality((Object) this.actionTarget, (Object) null))
      return false;
    this.EndAction();
    if (this.IsCoopNone() || this.IsOriginal())
      this.SetAttackActionPosition();
    this.actionID = Character.ACTION_ID.MOVE;
    Character.MOTION_ID motion_id = Character.MOTION_ID.MOVE_SIDE_R;
    if (isPacket)
    {
      if (moveAngleSign == 0 || moveAngleSign == 1 || moveAngleSign == -1)
        this.m_moveAngleSign = moveAngleSign;
    }
    else
    {
      switch (moveAngleSign)
      {
        case -1:
        case 1:
          this.m_moveAngleSign = moveAngleSign;
          break;
        case 0:
          Vector3 vector3 = Vector3.op_Subtraction(this._position, this.actionPosition);
          vector3.y = 0.0f;
          Vector3 target_pos1 = Vector3.op_Addition(this.actionPosition, Quaternion.op_Multiply(Quaternion.AngleAxis(-this.moveAngle_deg, Vector3.up), vector3));
          Vector3 target_pos2 = Vector3.op_Addition(this.actionPosition, Quaternion.op_Multiply(Quaternion.AngleAxis(this.moveAngle_deg, Vector3.up), vector3));
          RaycastHit hit1 = new RaycastHit();
          RaycastHit hit2 = new RaycastHit();
          bool flag1 = AIUtility.RaycastObstacle((StageObject) this, target_pos1, out hit1);
          bool flag2 = AIUtility.RaycastObstacle((StageObject) this, target_pos2, out hit2);
          this.m_moveAngleSign = !(flag1 & flag2) ? (!flag1 ? (!flag2 ? (Random.Range(0, 2) == 0 ? 1 : -1) : -1) : 1) : 0;
          break;
      }
    }
    switch (this.m_moveAngleSign)
    {
      case -1:
        motion_id = Character.MOTION_ID.MOVE_SIDE_R;
        break;
      case 1:
        motion_id = Character.MOTION_ID.MOVE_SIDE_L;
        break;
    }
    this.PlayMotion((int) motion_id);
    this.moveType = Character.MOVE_TYPE.SIDEWAYS;
    if (Object.op_Inequality((Object) this.characterSender, (Object) null))
      this.characterSender.OnActMoveSideways(this.m_moveAngleSign);
    return true;
  }

  private void UpdateMoveSideAction()
  {
    if (this.m_moveAngleSign == 0)
    {
      this.ActIdle();
    }
    else
    {
      Vector3 vector3_1 = Vector3.op_Subtraction(this.actionPosition, this._position);
      vector3_1.y = 0.0f;
      ((Vector3) ref vector3_1).Normalize();
      Vector3 forward = this._forward;
      forward.y = 0.0f;
      ((Vector3) ref forward).Normalize();
      this.m_diffAngle_deg = Vector3.Angle(forward, vector3_1);
      if ((double) this.m_diffAngle_deg > 90.0)
      {
        this.ActIdle();
      }
      else
      {
        this.m_diffAngle_deg *= (double) Vector3.Cross(forward, vector3_1).y >= 0.0 ? 1f : -1f;
        Quaternion rotation = this._rotation;
        Vector3 eulerAngles = ((Quaternion) ref rotation).eulerAngles;
        float num1 = Mathf.SmoothDampAngle(0.0f, this.m_diffAngle_deg, ref this.rotateVelocity, this.rotateMinimumTime, this.rotateMaxSpeed, Time.deltaTime);
        this._rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + num1, eulerAngles.z);
        Vector3 vector3_2 = Vector3.op_Subtraction(this._position, this.actionPosition);
        ((Vector3) ref vector3_2).Normalize();
        Vector3 vector3_3 = Vector3.op_Multiply(vector3_2, AIUtility.GetLengthWithBetweenPosition(this._position, this.actionPosition));
        float num2 = this.moveAngle_deg * Time.deltaTime;
        float num3 = this.moveAngleSpeed_deg * Time.deltaTime;
        if ((double) num2 > (double) num3)
          num2 = num3;
        this._position = Vector3.op_Addition(this._position, Vector3.op_Subtraction(Vector3.op_Addition(this.actionPosition, Quaternion.op_Multiply(Quaternion.AngleAxis(num2 * (float) this.m_moveAngleSign, Vector3.up), vector3_3)), this._position));
        this.m_movedAngle_deg += num2;
        if ((double) this.moveAngle_deg > (double) this.m_movedAngle_deg)
          return;
        this.SetNextTrigger();
      }
    }
  }

  public Vector3 movePointPos { get; set; }

  protected Character.STATE_MOVE_POINT stateMovePoint { get; private set; }

  protected void SetStateMovePoint(Character.STATE_MOVE_POINT state) => this.stateMovePoint = state;

  public virtual void ActMovePoint(Vector3 targetPos)
  {
  }

  protected virtual void UpdateMovePointAction()
  {
  }

  protected bool IsNeedToRotate(Vector3 targetDir)
  {
    return (double) Vector3.Dot(this._forward, targetDir) < 1.0;
  }

  public Vector3 moveLookAtPos { get; set; }

  public float moveLookAtAngle { get; set; }

  protected Character.STATE_MOVE_LOOKAT stateMoveLookAt { get; private set; }

  protected void SetStateMoveLookAt(Character.STATE_MOVE_LOOKAT state)
  {
    this.stateMoveLookAt = state;
  }

  public virtual void ActMoveLookAt(Vector3 moveLookAtPos, bool isPacket = false)
  {
  }

  protected virtual void UpdateMoveLookAtAction()
  {
  }

  protected virtual void OnPlayingEndMotion()
  {
    bool flag = false;
    if (this.actionID == Character.ACTION_ID.ROTATE)
    {
      if (this.rotateType == Character.ROTATE_TYPE.MOTION_TO_TARGET)
      {
        if (this.rotateTargetEnd)
        {
          this.ActIdle();
          return;
        }
        if (Object.op_Equality((Object) this.actionTarget, (Object) null))
        {
          this.ActIdle();
          return;
        }
        Vector3 vector3 = Vector3.op_Subtraction(this.GetTargetPosition(this.actionTarget), this._position);
        vector3.y = 0.0f;
        ((Vector3) ref vector3).Normalize();
        if (Vector3.op_Equality(vector3, Vector3.zero))
        {
          this.ActIdle();
          return;
        }
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        float y = ((Quaternion) ref quaternion).eulerAngles.y;
        float diff_angle = 0.0f;
        if (this.CalcDiffAngle(y, ref diff_angle) != this.rotateSign)
        {
          this.ActIdle();
          return;
        }
        if ((double) diff_angle < 10.0)
        {
          this.ActIdle();
          return;
        }
        this.ActRotateMotionToTarget(true);
      }
      else if (this.rotateType == Character.ROTATE_TYPE.MOTION_TO_DIRECTION)
      {
        float diff_angle = 0.0f;
        if (this.CalcDiffAngle(this.rotateDirection, ref diff_angle) != this.rotateSign)
        {
          this.ActIdle();
          return;
        }
        if ((double) diff_angle < 10.0)
        {
          this.ActIdle();
          return;
        }
        this.rootRotationRate = this.CalcRotateMotionRate(diff_angle);
        if (this.rotateSign > 0)
          this.PlayMotion(5);
        else
          this.PlayMotion(4);
      }
      else
        flag = true;
    }
    else
      flag = true;
    if (!flag)
      return;
    this.isPlayingEndMotion = true;
    this.ActIdle();
  }

  protected virtual void EndAction()
  {
    if (!this.isInitialized)
      return;
    if (this.animEventProcessor != null)
      this.animEventProcessor.ExecuteLastEvent();
    if (Object.op_Inequality((Object) this.characterSender, (Object) null))
      this.characterSender.OnEndAction();
    if (Object.op_Inequality((Object) this.controller, (Object) null))
      this.controller.OnCharacterEndAction((int) this.actionID);
    switch (this.actionID)
    {
      case Character.ACTION_ID.MOVE:
        this.moveType = Character.MOVE_TYPE.NONE;
        this.rotateVelocity = 0.0f;
        this.moveTargetPos = Vector3.zero;
        this.moveSyncDirection = 0.0f;
        this.moveSyncDirectionTime = 0.0f;
        this.moveSyncTime = 0.0f;
        this.moveSyncEnd = false;
        this.moveSyncEndDirection = 0.0f;
        this.moveSyncSpeed = 0.0f;
        this.moveSyncMotionID = 0;
        this.moveBeforePos = Vector3.zero;
        this.moveNowDistance = 0.0f;
        this.moveMaxDistance = 0.0f;
        this.m_movedAngle_deg = 0.0f;
        this.m_diffAngle_deg = 0.0f;
        this.m_moveAngleSign = 0;
        break;
      case Character.ACTION_ID.ROTATE:
        this.rotateDirection = 0.0f;
        this.rotateSign = 0;
        this.rotateVelocity = 0.0f;
        this.rootRotationRate = 1f;
        this.rotateTargetEnd = false;
        this.rotateTargetCnt = 0;
        break;
      case Character.ACTION_ID.ATTACK:
        this.attackID = 0;
        break;
      case Character.ACTION_ID.PARALYZE:
        this.ActParalyzeEnd();
        break;
      case Character.ACTION_ID.FREEZE:
        this.ActFreezeEnd();
        break;
    }
    this.EndWaitingPacket(StageObject.WAITING_PACKET.CHARACTER_MOVE_VELOCITY);
    this.EndWaitingPacket(StageObject.WAITING_PACKET.CHARACTER_UPDATE_ACTION_POSITION);
    this.EndWaitingPacket(StageObject.WAITING_PACKET.CHARACTER_UPDATE_DIRECTION);
    int type = 0;
    for (int count = this.objectList.Count; type < count; ++type)
    {
      if (this.objectTypeAutoDelete[type])
        this.DestroyObjectList((Character.OBJECT_LIST_TYPE) type);
    }
    if (Object.op_Inequality((Object) this.animator, (Object) null))
    {
      int index = 0;
      for (int count = this.changeTriggerList.Count; index < count; ++index)
        this.animator.ResetTrigger(this.changeTriggerList[index]);
      this.changeTriggerList.Clear();
    }
    if (Object.op_Inequality((Object) this.animator, (Object) null))
    {
      int index = 0;
      for (int count = this.animatorBoolList.Count; index < count; ++index)
        this.animator.SetBool(this.animatorBoolList[index], false);
      this.animatorBoolList.Clear();
    }
    int index1 = 0;
    for (int count = this.animEventColliderList.Count; index1 < count; ++index1)
    {
      if (!this.animEventColliderList[index1].isReleased)
        this.animEventColliderList[index1].ReserveRelease();
    }
    if (this.hideRendererList.Count > 0 && !this.IsCarrying())
    {
      List<string> range = this.hideRendererList.GetRange(0, this.hideRendererList.Count);
      int index2 = 0;
      for (int count = range.Count; index2 < count; ++index2)
        this.SetEnableNodeRenderer(range[index2], true);
    }
    if (this.referenceCheckerFlag)
    {
      this.attackHitChecker = new AttackHitChecker();
      this.referenceCheckerFlag = false;
    }
    int index3 = 0;
    for (int count = this.loopSeForceEndList.Count; index3 < count; ++index3)
    {
      if (this.loopSeForceEndList[index3] >= 0)
        SoundManager.LoopOff(this.loopSeForceEndList[index3], (DisableNotifyMonoBehaviour) this);
    }
    this.loopSeForceEndList.Clear();
    this.wallStayTimer = 0.0f;
    this.isControllable = false;
    this.enableMotionCancel = false;
    this.enableMoveSuppress = false;
    this.SetVelocity(Vector3.zero);
    this.addForce = Vector3.zero;
    this.actionMoveRate = 1f;
    this.rootMotionMoveRate = 1f;
    if (Object.op_Inequality((Object) this.animator, (Object) null))
      this.animator.applyRootMotion = true;
    this.enableRootMotion = true;
    this.rotateEventSpeed = 0.0f;
    this.rotateEventDirection = 0.0f;
    this.rotateEventKeep = false;
    this.rotateToTargetFlag = false;
    this.rotateToTargetDiffAngle = 0.0f;
    this.rotateSafeMode = false;
    this.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.INVICIBLE;
    this.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.DEAD;
    this.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.DEAD_REVIVE;
    this.enableEventMove = false;
    this.eventMoveVelocity = Vector3.zero;
    this.eventMoveTimeCount = 0.0f;
    this.enableAddForce = false;
    this.addForceBeforePos = Vector3.zero;
    this.waitAddForce = false;
    this.lastActionID = this.actionID;
    this.actionID = Character.ACTION_ID.NONE;
    this.actionPosition = Vector3.zero;
    this.targetPointPos = Vector3.zero;
    this.actionPositionFlag = false;
    this.actionPositionThroughFlag = false;
    this.actionPositionWaitSync = false;
    this.actionPositionWaitTrigger = (string) null;
    this.directionWaitSync = false;
    this.directionWaitTrigger = (string) null;
    this.periodicSyncActionPositionFlag = false;
    this.periodicSyncActionPositionLastTime = 0.0f;
    this.periodicSyncActionPositionList.Clear();
    this.attackStartTarget = (StageObject) null;
    this.isDead = false;
    this.lerpRotateVec = Vector3.zero;
    this.enableReactionDelay = false;
    this.isPlayingEndMotion = false;
    this.actionRendererModel = (GameObject) null;
    this.actionRendererNodeName = (string) null;
    this.actMotionStartTime = -1f;
    this.actionReceiveDamageRate = 1f;
    this.isWallStay = false;
    this.wallStayTimer = 0.0f;
    this.SetPeriodicSyncTarget((StageObject) null);
    if (Object.op_Inequality((Object) this.stepCtrl, (Object) null))
      this.stepCtrl.enableAutoStampEffect = true;
    if (Object.op_Inequality((Object) this._collider, (Object) null))
      this._collider.enabled = true;
    if (Object.op_Inequality((Object) this.damegeRemainEffect, (Object) null))
    {
      EffectManager.ReleaseEffect(this.damegeRemainEffect);
      this.damegeRemainEffect = (GameObject) null;
    }
    if (this.animEventProcessor != null)
      this.animEventProcessor.IgnoreEventByNextAnim();
    if (Object.op_Inequality((Object) this.actionRendererInstance, (Object) null))
    {
      Object.Destroy((Object) ((Component) this.actionRendererInstance).gameObject);
      this.actionRendererInstance = (Transform) null;
    }
    this.DeleteExAtkColliderAll();
  }

  protected virtual void _EndDebuffAction(Character.ACTION_ID beforeActId)
  {
  }

  private static string _GetMotionStateName(int motion_id, string _layerName)
  {
    string str = string.IsNullOrEmpty(_layerName) ? "Base Layer." : _layerName;
    if (motion_id >= 115)
      return (string) null;
    Character.stateNameBuilder.Length = 0;
    Character.stateNameBuilder.Append(str);
    if (motion_id >= 15 && motion_id <= 114)
      Character.stateNameBuilder.AppendFormat(Character.motionStateName[15], (object) (motion_id - 15));
    else
      Character.stateNameBuilder.Append(Character.motionStateName[motion_id]);
    return Character.stateNameBuilder.ToString();
  }

  protected virtual string GetMotionStateName(int motion_id, string _layerName = "")
  {
    return motion_id < 115 ? Character._GetMotionStateName(motion_id, _layerName) : (string) null;
  }

  public int GetMotionHash(int motion_id)
  {
    string motionStateName = this.GetMotionStateName(motion_id, this.ReplaceMotionLayer(motion_id));
    if (motionStateName == null)
      return 0;
    int cachedHash = this._GetCachedHash(motionStateName);
    if (cachedHash != 0)
      return cachedHash;
    int motionHash = this.GetMotionHash(motionStateName);
    this._CacheHash(motionStateName, motionHash);
    return motionHash;
  }

  protected int _GetCachedHash(string motionName)
  {
    return !Character.motionHashCaches.ContainsKey(motionName) ? 0 : Character.motionHashCaches[motionName];
  }

  protected void _CacheHash(string motionName, int hash)
  {
    Character.motionHashCaches[motionName] = hash;
  }

  public int GetMotionHash(string state_name)
  {
    object obj = Character.motionHash.Get(state_name);
    int motionHash;
    if (obj == null)
    {
      motionHash = Animator.StringToHash(state_name);
      Character.motionHash.Add(state_name, motionHash);
    }
    else
      motionHash = (int) obj;
    return motionHash;
  }

  public bool PlayMotion(Character.PlayMotionParam _param)
  {
    _param.MotionLayerName = this.ReplaceMotionLayer(_param.MotionID, _param.MotionLayerName);
    string motionStateName = this.GetMotionStateName(_param.MotionID, _param.MotionLayerName);
    if (string.IsNullOrEmpty(motionStateName))
    {
      Log.Warning(LOG.INGAME, "Character::PlayMotion motion_id is none");
      return false;
    }
    int num = this._PlayMotion(motionStateName, _transition_time: _param.TransitionTime) ? 1 : 0;
    if (!Object.op_Inequality((Object) this.controller, (Object) null))
      return num != 0;
    this.controller.OnCharacterPlayMotion(_param.MotionID);
    return num != 0;
  }

  public bool PlayMotion(int motion_id, float transition_time = -1f)
  {
    string motionStateName = this.GetMotionStateName(motion_id, this.ReplaceMotionLayer(motion_id));
    if (string.IsNullOrEmpty(motionStateName))
    {
      Log.Warning(LOG.INGAME, "Character::PlayMotion motion_id is none");
      return false;
    }
    int num = this._PlayMotion(motionStateName, _transition_time: transition_time) ? 1 : 0;
    if (!Object.op_Inequality((Object) this.controller, (Object) null))
      return num != 0;
    this.controller.OnCharacterPlayMotion(motion_id);
    return num != 0;
  }

  protected virtual string ReplaceMotionLayer(int motionId, string layerName = "Base Layer.")
  {
    return layerName;
  }

  public bool PlayMotion(string anim_format_name, float _transition_time = -1f)
  {
    if (string.IsNullOrEmpty(anim_format_name))
    {
      Log.Warning(LOG.INGAME, "Character::PlayMotion anim_format_name is null or empty");
      return false;
    }
    string ctrl_name;
    string state_name;
    Character.SeparateAnimFormatName(anim_format_name, out ctrl_name, out state_name);
    if (!string.IsNullOrEmpty(state_name))
      return this._PlayMotion("Base Layer." + state_name, ctrl_name, _transition_time);
    Log.Warning(LOG.INGAME, "Character::PlayMotion state_name is null or empty");
    return false;
  }

  public static void SeparateAnimFormatName(
    string anim_format_name,
    out string ctrl_name,
    out string state_name)
  {
    ctrl_name = (string) null;
    state_name = (string) null;
    if (string.IsNullOrEmpty(anim_format_name))
      return;
    int length = anim_format_name.IndexOf("@");
    if (length < 0)
    {
      state_name = anim_format_name;
    }
    else
    {
      ctrl_name = anim_format_name.Substring(0, length);
      state_name = anim_format_name.Substring(length + 1, anim_format_name.Length - (length + 1));
      if (!(ctrl_name == ""))
        return;
      ctrl_name = state_name.ToUpper();
    }
  }

  public static string GetCtrlNameFromAnimFormatName(string anim_format_name)
  {
    string ctrl_name;
    Character.SeparateAnimFormatName(anim_format_name, out ctrl_name, out string _);
    return ctrl_name;
  }

  public bool PlayMotionImmidate(string ctrlName, string stateName, float _transition_time = -1f)
  {
    return this._PlayMotion("Base Layer." + stateName, ctrlName, _transition_time);
  }

  protected bool _PlayMotion(string state_name, string controller_name = null, float _transition_time = -1f)
  {
    if (string.IsNullOrEmpty(state_name))
    {
      Log.Warning(LOG.INGAME, "Character::_PlayMotion state_name is null or empty");
      return false;
    }
    this.nextAnimCtrlName = controller_name;
    this.nextMotionHash = this.GetMotionHash(state_name);
    this.nextMotionTransitionTime = _transition_time;
    if (this.charaName == "Hellish Zaark" && this.nextMotionHash == 423958061)
      Debug.Log((object) $"hash: {(object) this.nextMotionHash} state name: {state_name}");
    this.hitOffFlag |= StageObject.HIT_OFF_FLAG.PLAY_MOTION;
    return true;
  }

  public virtual RuntimeAnimatorController GetAnimCtrl(string ctrl_name)
  {
    return (RuntimeAnimatorController) null;
  }

  public virtual AnimEventData GetAnimEvent(string ctrl_name) => (AnimEventData) null;

  public int GetPlayingMotionHash()
  {
    if (Object.op_Equality((Object) this.animator, (Object) null))
      return 0;
    int fullPathHash;
    if (this.animator.IsInTransition(0))
    {
      AnimatorStateInfo animatorStateInfo = this.animator.GetNextAnimatorStateInfo(0);
      fullPathHash = ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash;
    }
    else
    {
      AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
      fullPathHash = ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash;
    }
    return fullPathHash;
  }

  public bool IsPlayingMotion(int motion_id, bool check_next = true)
  {
    int motionHash = this.GetMotionHash(motion_id);
    if (check_next)
    {
      if (this.nextMotionHash != 0)
        return this.nextMotionHash == motionHash;
      int num = 0;
      if (this.animEventProcessor != null)
        num = this.animEventProcessor.GetWaitMotionHash();
      if (num != 0)
        return num == motionHash;
    }
    return this.GetPlayingMotionHash() == motionHash;
  }

  public void SetNextTrigger(int index = 0)
  {
    string str = "";
    if (index > 0)
      str = (index + 1).ToString();
    this.SetChangeTrigger("next" + str);
  }

  public void SetChangeTrigger(string motion_trigger)
  {
    if (Object.op_Equality((Object) this.animator, (Object) null))
      return;
    this.animator.SetTrigger(motion_trigger);
    this.changeTriggerList.Add(motion_trigger);
  }

  public void SendBuffSync(BuffParam.BUFFTYPE nowBuffType = BuffParam.BUFFTYPE.NONE)
  {
    if (!this.IsOriginal())
      return;
    BuffParam.BuffSyncParam syncParam = this.buffParam.CreateSyncParam(nowBuffType);
    if (Object.op_Inequality((Object) this.characterSender, (Object) null))
      this.characterSender.OnSendBuffSync(syncParam);
    this.buffSyncLastTime = Time.time;
  }

  public void OnBuffReceive(BuffParam.BuffData buffData)
  {
    if (this.IsCoopNone() || this.IsOriginal())
    {
      this.OnBuffStart(buffData);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.characterSender, (Object) null))
        return;
      this.characterSender.OnBuffReceive(buffData.type, buffData.value, buffData.time);
    }
  }

  public virtual bool OnBuffStart(BuffParam.BuffData buffData)
  {
    if (!this.buffParam.BuffStart(buffData))
      return false;
    this.UpdateAnimatorSpeed();
    if (buffData.sync)
      this.SendBuffSync();
    return true;
  }

  protected virtual void OnUIBuffRoutine(BuffParam.BUFFTYPE type, int value)
  {
  }

  public virtual void OnBuffRoutine(BuffParam.BuffData buffData, bool packet = false)
  {
    BuffParam.BUFFTYPE type = buffData.type;
    int damage = buffData.value;
    if ((type == BuffParam.BUFFTYPE.ELECTRIC_SHOCK || type == BuffParam.BUFFTYPE.SOIL_SHOCK) && !packet)
      damage = buffData.damage;
    this.OnBuffRoutine(type, damage, packet);
    if (!Object.op_Inequality((Object) this.characterSender, (Object) null) || packet)
      return;
    this.characterSender.OnBuffRoutine(buffData.type, damage, buffData.fromObjectID, buffData.fromEquipIndex, buffData.fromSkillIndex);
  }

  private void OnBuffRoutine(BuffParam.BUFFTYPE type, int value, bool packet = false)
  {
    switch (type)
    {
      case BuffParam.BUFFTYPE.INVINCIBLECOUNT:
      case BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS:
      case BuffParam.BUFFTYPE.SUBSTITUTE:
      case BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION:
      case BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS:
        this.buffParam.ResetInterval(type);
        break;
      case BuffParam.BUFFTYPE.REGENERATE:
        if (this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP))
          return;
        value = (int) ((double) value * (double) this.buffParam.GetHealUp());
        if (value <= 0)
          value = 1;
        this.RecoverHp(value, false);
        EffectManager.GetEffect("ef_btl_sk_heal_02", this.FindNode("Hip"));
        break;
      case BuffParam.BUFFTYPE.POISON:
      case BuffParam.BUFFTYPE.DEADLY_POISON:
        float poisonDamageDownRate = this.buffParam.GetPoisonDamageDownRate();
        if ((double) poisonDamageDownRate > 0.0)
        {
          float num = Mathf.Clamp(1f - poisonDamageDownRate, 0.0f, 1f);
          value = (int) ((double) value * (double) num);
          if (value <= 0)
            value = 1;
        }
        this.hp -= value;
        if (this.hp <= 1)
        {
          this.hp = 1;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.BURNING:
        float burnDamageDownRate = this.buffParam.GetBurnDamageDownRate();
        if ((double) burnDamageDownRate > 0.0)
        {
          float num = Mathf.Clamp(1f - burnDamageDownRate, 0.0f, 1f);
          value = (int) ((double) value * (double) num);
          if (value <= 0)
            value = 1;
        }
        this.hp -= value;
        if (this.hp <= 1)
        {
          this.hp = 1;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.ELECTRIC_SHOCK:
      case BuffParam.BUFFTYPE.EROSION:
      case BuffParam.BUFFTYPE.SOIL_SHOCK:
      case BuffParam.BUFFTYPE.ACID:
      case BuffParam.BUFFTYPE.CORRUPTION:
      case BuffParam.BUFFTYPE.STIGMATA:
      case BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM:
        this.hp -= value;
        if (this.hp <= 1)
        {
          this.hp = 1;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.REGENERATE_PROPORTION:
        if (this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP))
          return;
        value = (int) ((double) this.hpMax * (double) (Mathf.Clamp((float) value, 0.0f, 100f) / 100f));
        this.RecoverHp(value, false);
        EffectManager.GetEffect("ef_btl_sk_heal_02", this.FindNode("Hip"));
        break;
    }
    this.OnUIBuffRoutine(type, value);
  }

  public virtual bool OnBuffEnd(BuffParam.BUFFTYPE type, bool sync, bool isPlayEndEffect = true)
  {
    if (!this.buffParam.BuffEnd(type, isPlayEndEffect))
      return false;
    switch (type)
    {
      case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
        this.badStatusTotal.speedDown = 0.0f;
        break;
      case BuffParam.BUFFTYPE.POISON:
        this.badStatusTotal.poison = 0.0f;
        break;
      case BuffParam.BUFFTYPE.BURNING:
        this.badStatusTotal.burning = 0.0f;
        break;
      case BuffParam.BUFFTYPE.DEADLY_POISON:
        this.badStatusTotal.deadlyPoison = 0.0f;
        break;
      case BuffParam.BUFFTYPE.INK_SPLASH:
        this.badStatusTotal.inkSplash = 0.0f;
        break;
      case BuffParam.BUFFTYPE.SHIELD:
        this.OnBuffEnd(BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR, false);
        this.OnBuffEnd(BuffParam.BUFFTYPE.SHIELD_REFLECT, false);
        this.OnBuffEnd(BuffParam.BUFFTYPE.SHIELD_REFLECT_DAMAGE_UP, false);
        this.ShieldHp = (XorInt) 0;
        break;
      case BuffParam.BUFFTYPE.SLIDE:
        this.badStatusTotal.slide = 0.0f;
        break;
      case BuffParam.BUFFTYPE.SILENCE:
        this.badStatusTotal.silence = 0.0f;
        break;
      case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
        this.badStatusTotal.attackSpeedDown = 0.0f;
        break;
      case BuffParam.BUFFTYPE.CANT_HEAL_HP:
        this.badStatusTotal.cantHealHp = 0.0f;
        break;
      case BuffParam.BUFFTYPE.BLIND:
        this.badStatusTotal.blind = 0.0f;
        break;
      case BuffParam.BUFFTYPE.EROSION:
        this.badStatusTotal.erosion = 0.0f;
        break;
      case BuffParam.BUFFTYPE.STONE:
        this.badStatusTotal.stone = 0.0f;
        break;
      case BuffParam.BUFFTYPE.BLEEDING:
        this.badStatusTotal.bleeding = 0.0f;
        break;
      case BuffParam.BUFFTYPE.ACID:
        this.badStatusTotal.acid = 0.0f;
        break;
      case BuffParam.BUFFTYPE.DAMAGE_MOTION_STOP:
        this.badStatusTotal.damageMotionStop = 0.0f;
        break;
      case BuffParam.BUFFTYPE.CORRUPTION:
        this.badStatusTotal.corruption = 0.0f;
        break;
    }
    this.UpdateAnimatorSpeed();
    if (sync)
      this.SendBuffSync();
    return true;
  }

  public virtual void OnPoisonStart(int fromObjectID = 0)
  {
  }

  public virtual void OnBurningStart()
  {
  }

  public virtual void OnBleedingStart()
  {
  }

  public virtual void OnSpeedDown()
  {
  }

  public virtual void OnAttackSpeedDown()
  {
  }

  public virtual void OnDeadlyPoisonStart()
  {
  }

  public virtual void OnInkSplash(InkSplashInfo info)
  {
  }

  public virtual void OnSlideStart()
  {
  }

  public virtual void OnSilenceStart()
  {
  }

  public virtual void OnCantHealHpStart()
  {
  }

  public virtual void OnBlindStart()
  {
  }

  public virtual void OnBuffCancellation()
  {
  }

  public virtual bool IsValidLightRing() => false;

  public virtual void OnStoneStart()
  {
  }

  public virtual void OnAcidStart()
  {
  }

  public virtual void OnDamageMotionStopStart(float time)
  {
  }

  public virtual void OnCorruptionStart()
  {
  }

  public virtual bool IsValidBuff(BuffParam.BUFFTYPE targetType)
  {
    return this.buffParam.IsValidBuff(targetType);
  }

  public bool IsValidBuffByAbility(BuffParam.BUFFTYPE targetType)
  {
    return this.buffParam.IsValidBuffByAbility(targetType);
  }

  public bool IsValidBuffBlind() => this.IsValidBuff(BuffParam.BUFFTYPE.BLIND);

  public virtual void RecoverHp(int recoverValue, bool isSend)
  {
    this.hp += recoverValue;
    if (this.hp > this.hpMax)
      this.hp = this.hpMax;
    if (recoverValue <= 0)
      return;
    this.OnBuffEnd(BuffParam.BUFFTYPE.BLEEDING, false);
  }

  public override bool CheckHitAttack(
    AttackHitInfo info,
    Collider to_collider,
    StageObject to_object)
  {
    if (info.canAttackGrabbedPlayer)
    {
      if ((to_object.hitOffFlag & ~StageObject.HIT_OFF_FLAG.GRAB) != StageObject.HIT_OFF_FLAG.NONE)
        return false;
    }
    else if (info.attackType == AttackHitInfo.ATTACK_TYPE.SNATCH)
    {
      if (to_object.hitOffFlag != StageObject.HIT_OFF_FLAG.NONE && (to_object.hitOffFlag & (StageObject.HIT_OFF_FLAG.INVICIBLE | StageObject.HIT_OFF_FLAG.DEAD)) == StageObject.HIT_OFF_FLAG.NONE)
        return false;
    }
    else if (to_object.hitOffFlag != StageObject.HIT_OFF_FLAG.NONE)
      return false;
    return base.CheckHitAttack(info, to_collider, to_object);
  }

  public override void OnAttackedHit(
    AttackHitInfo info,
    AttackHitColliderProcessor.HitParam hit_param)
  {
    base.OnAttackedHit(info, hit_param);
    if (!this.IsValidAttackedHit(hit_param.fromObject) || this.IsPuppet() || hit_param.fromObject.IsPuppet() || !this.IsMirror() && !this.IsPuppet() || !this.isLocalDamageApply || this.hp - this.localDamage > 0)
      return;
    this.ActDead(true);
  }

  protected override bool IsValidAttackedHit(StageObject from_object)
  {
    return !this.isDead && base.IsValidAttackedHit(from_object);
  }

  protected override void OnAttackedHitDirection(AttackedHitStatusDirection status)
  {
    if (status.hitParam.processor != null)
    {
      BulletObject colliderInterface = status.hitParam.processor.colliderInterface as BulletObject;
      if (Object.op_Inequality((Object) colliderInterface, (Object) null))
      {
        status.atk = colliderInterface.masterAtk;
        status.skillParam = colliderInterface.masterSkill;
      }
      else
      {
        AtkAttribute atk = new AtkAttribute();
        status.fromObject.GetAtk(status.attackInfo, ref atk);
        status.atk = atk;
        Player fromObject = status.fromObject as Player;
        if (Object.op_Inequality((Object) fromObject, (Object) null))
          status.skillParam = fromObject.skillInfo.actSkillParam;
      }
    }
    if (this.IsDamageValid(status))
    {
      status.validDamage = true;
      status.badStatusAdd.Copy(this.CalcBadStatus(status));
    }
    base.OnAttackedHitDirection(status);
  }

  protected virtual bool IsDamageValid(AttackedHitStatusDirection status) => false;

  protected virtual BadStatus CalcBadStatus(AttackedHitStatusDirection status)
  {
    BadStatus targetBadStatus = new BadStatus();
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
      return targetBadStatus;
    targetBadStatus.Copy(status.attackInfo.badStatus);
    if (status.attackInfo.isSkillReference && status.skillParam != null)
    {
      for (int index = 0; index < 3; ++index)
      {
        switch (status.skillParam.tableData.supportType[index])
        {
          case BuffParam.BUFFTYPE.HIT_PARALYZE:
            targetBadStatus.paralyze += (float) status.skillParam.supportValue[index];
            break;
          case BuffParam.BUFFTYPE.HIT_POISON:
            targetBadStatus.poison += (float) status.skillParam.supportValue[index];
            break;
        }
      }
    }
    Character fromObject = status.fromObject as Character;
    if (Object.op_Implicit((Object) fromObject))
    {
      targetBadStatus.Add(fromObject.atkBadStatus);
      targetBadStatus.paralyze += (float) fromObject.buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_PARALYZE);
      targetBadStatus.poison += (float) fromObject.buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_POISON);
      targetBadStatus.freeze += (float) fromObject.buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_FREEZE);
      if ((double) targetBadStatus.paralyze > 0.0)
      {
        targetBadStatus.paralyze *= fromObject.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.PARALYZE);
        targetBadStatus.paralyze += fromObject.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.PARALYZE);
      }
      if ((double) targetBadStatus.poison > 0.0)
      {
        targetBadStatus.poison *= fromObject.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.POISON);
        targetBadStatus.poison += fromObject.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.POISON);
      }
      if ((double) targetBadStatus.freeze > 0.0)
      {
        targetBadStatus.freeze *= fromObject.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.FREEZE);
        targetBadStatus.freeze += fromObject.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.FREEZE);
      }
    }
    targetBadStatus.Mul(status.attackInfo.atkRate);
    this.buffParam.ApplyBadStatusGuard(ref targetBadStatus);
    return targetBadStatus;
  }

  protected override void OnAttackedHitLocal(AttackedHitStatusLocal status)
  {
    if (this.buffParam.IsValidInvincibleCountBuff())
      return;
    base.OnAttackedHitLocal(status);
    if (!status.validDamage)
      return;
    AtkAttribute damage_details = new AtkAttribute();
    status.damage = this.CalcDamage(status, ref damage_details);
    status.damageDetails = damage_details;
    StageObject fromObject = status.fromObject;
    if (Object.op_Inequality((Object) fromObject, (Object) null))
    {
      fromObject.AbsorptionProc(this, status);
      fromObject.AbsorptionProcByBuff(status);
    }
    int num = this.CutAndAbsorbDamageByBuff(this, status) ? 1 : 0;
    bool flag = false;
    if (num == 0)
      flag = this.InvincibleDamageByBuff(this, status);
    if (num == 0 && !flag)
      this.ChargeSkillWhenDamagedByBuff();
    if (!this.isLocalDamageApply || !this.IsPuppet() && !this.IsMirror())
      return;
    this.localDamage += status.damage;
  }

  protected virtual int CalcDamage(AttackedHitStatusLocal status, ref AtkAttribute damage_details)
  {
    return 0;
  }

  protected AtkAttribute GetInvinsibleMulRate()
  {
    AtkAttribute invinsibleMulRate = new AtkAttribute();
    invinsibleMulRate.Set(1f);
    List<BuffParam.BuffData> invincibleBuffDataList = this.buffParam.GetInvincibleBuffDataList();
    if (invincibleBuffDataList.IsNullOrEmpty<BuffParam.BuffData>())
      return invinsibleMulRate;
    for (int index = 0; index < invincibleBuffDataList.Count; ++index)
    {
      switch (invincibleBuffDataList[index].type)
      {
        case BuffParam.BUFFTYPE.INVINCIBLE_NORMAL:
          invinsibleMulRate.normal = 0.0f;
          break;
        case BuffParam.BUFFTYPE.INVINCIBLE_FIRE:
          invinsibleMulRate.SetTargetElement(ELEMENT_TYPE.FIRE, 0.0f);
          break;
        case BuffParam.BUFFTYPE.INVINCIBLE_WATER:
          invinsibleMulRate.SetTargetElement(ELEMENT_TYPE.WATER, 0.0f);
          break;
        case BuffParam.BUFFTYPE.INVINCIBLE_THUNDER:
          invinsibleMulRate.SetTargetElement(ELEMENT_TYPE.THUNDER, 0.0f);
          break;
        case BuffParam.BUFFTYPE.INVINCIBLE_SOIL:
          invinsibleMulRate.SetTargetElement(ELEMENT_TYPE.SOIL, 0.0f);
          break;
        case BuffParam.BUFFTYPE.INVINCIBLE_ALL:
          invinsibleMulRate.normal = 0.0f;
          invinsibleMulRate.SetTargetElemetAll(0.0f);
          break;
        case BuffParam.BUFFTYPE.INVINCIBLE_LIGHT:
          invinsibleMulRate.SetTargetElement(ELEMENT_TYPE.LIGHT, 0.0f);
          break;
        case BuffParam.BUFFTYPE.INVINCIBLE_DARK:
          invinsibleMulRate.SetTargetElement(ELEMENT_TYPE.DARK, 0.0f);
          break;
        case BuffParam.BUFFTYPE.INVINCIBLE_ALL_ELEMENT:
          invinsibleMulRate.SetTargetElemetAll(0.0f);
          break;
      }
    }
    return invinsibleMulRate;
  }

  protected virtual AtkAttribute CalcAtk(AttackedHitStatusLocal status)
  {
    AtkAttribute atkAttribute = new AtkAttribute();
    atkAttribute.Add(status.atk);
    atkAttribute.Mul(status.attackInfo.atkRate);
    if (status.damageDistanceData != null)
    {
      float rate = status.damageDistanceData.GetRate(status.distanceXZ);
      atkAttribute.Mul(rate);
    }
    return atkAttribute;
  }

  protected virtual AtkAttribute CalcTolerance(AttackedHitStatusLocal status)
  {
    AtkAttribute _tolerance = new AtkAttribute();
    _tolerance.Add(this.tolerance);
    AtkAttribute val = new AtkAttribute();
    val.Set(0.0f);
    val.AddElementOnly(1f);
    val.Add(this.buffParam.passive.tolUpRate);
    val.Sub(this.buffParam.passive.tolDownRate);
    _tolerance.Mul(val);
    _tolerance.fire += (float) this.buffParam.passive.tolList[0];
    _tolerance.water += (float) this.buffParam.passive.tolList[1];
    _tolerance.thunder += (float) this.buffParam.passive.tolList[2];
    _tolerance.soil += (float) this.buffParam.passive.tolList[3];
    _tolerance.light += (float) this.buffParam.passive.tolList[4];
    _tolerance.dark += (float) this.buffParam.passive.tolList[5];
    _tolerance.CheckMinus();
    val.Set(0.0f);
    val.AddElementOnly(1f);
    val.Add(this.buffParam.GetBuffToleranceRate());
    _tolerance.Mul(val);
    this.AddToleranceBuff(ref _tolerance);
    _tolerance.CheckMinus();
    return _tolerance;
  }

  public void AddToleranceBuff(ref AtkAttribute _tolerance)
  {
    float num = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT);
    _tolerance.fire += (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_FIRE) + num;
    _tolerance.water += (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_WATER) + num;
    _tolerance.thunder += (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_THUNDER) + num;
    _tolerance.soil += (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_SOIL) + num;
    _tolerance.light += (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_LIGHT) + num;
    _tolerance.dark += (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_DARK) + num;
  }

  protected virtual AtkAttribute CalcDefense(AttackedHitStatusLocal status) => this.defense;

  public void AddDefenceBuff(ref AtkAttribute _defence)
  {
    float rate = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_NORMAL);
    _defence.AddRate(rate);
  }

  public void AddElementDefenceBuff(ref AtkAttribute _defence)
  {
    float num1 = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT);
    float num2 = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_FIRE);
    _defence.AddTargetElement(ELEMENT_TYPE.FIRE, num2 + num1);
    float num3 = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_WATER);
    _defence.AddTargetElement(ELEMENT_TYPE.WATER, num3 + num1);
    float num4 = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_THUNDER);
    _defence.AddTargetElement(ELEMENT_TYPE.THUNDER, num4 + num1);
    float num5 = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_SOIL);
    _defence.AddTargetElement(ELEMENT_TYPE.SOIL, num5 + num1);
    float num6 = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_LIGHT);
    _defence.AddTargetElement(ELEMENT_TYPE.LIGHT, num6 + num1);
    float num7 = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_DARK);
    _defence.AddTargetElement(ELEMENT_TYPE.DARK, num7 + num1);
  }

  public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
  {
    if (Object.op_Inequality((Object) this.controller, (Object) null))
      this.controller.OnCharacterAttackedHitOwner(status);
    bool flag = false;
    if (status.attackInfo != null && status.attackInfo.isImmediateDeath)
    {
      status.damage = this.hpMax;
      flag = true;
    }
    status.afterHP = this.hp;
    status.afterShieldHp = (int) this.ShieldHp;
    if (status.validDamage && !status.aegisParam.isChange)
    {
      if (!status.isDamageRegionOnly)
      {
        status.afterHP -= status.damage;
        if (this.IsNarrowEscape(status))
        {
          this.UseNarrowEscape(status);
          status.afterHP = 1;
        }
        if (status.afterHP < 0)
          status.afterHP = 0;
      }
      status.afterShieldHp -= status.shieldDamage;
      if (status.afterShieldHp < 0)
        status.afterShieldHp = 0;
    }
    status.badStatusTotal.Copy(this.badStatusTotal);
    status.badStatusTotal.Add(status.badStatusAdd);
    if ((double) status.badStatusAdd.paralyze > 0.0 && (double) status.badStatusTotal.paralyze >= (double) this.badStatusMax.paralyze && this.actionID != Character.ACTION_ID.PARALYZE)
    {
      if (!this.buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.PARALYZE))
        status.reactionType = 10;
      else
        status.badStatusTotal.paralyze = 0.0f;
    }
    if ((double) status.badStatusAdd.freeze > 0.0 && (double) status.badStatusTotal.freeze >= (double) this.badStatusMax.freeze && !this.IsFreeze())
    {
      if (!this.buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.FREEZE))
        status.reactionType = 12;
      else
        status.badStatusTotal.freeze = 0.0f;
    }
    if ((double) status.badStatusAdd.lightRing > 0.0 && (double) status.badStatusTotal.lightRing >= (double) this.badStatusMax.lightRing && !this.IsLightRing() && this.IsValidLightRing())
      status.reactionType = 19;
    if ((double) status.badStatusAdd.poison > 0.0 && (double) status.badStatusTotal.poison >= (double) this.badStatusMax.poison)
      this.OnPoisonStart(status.fromObjectID);
    if ((double) status.badStatusAdd.deadlyPoison > 0.0 && (double) status.badStatusTotal.deadlyPoison >= (double) this.badStatusMax.deadlyPoison)
      this.OnDeadlyPoisonStart();
    if ((double) status.badStatusAdd.burning > 0.0 && (double) status.badStatusTotal.burning >= (double) this.badStatusMax.burning)
      this.OnBurningStart();
    if ((double) status.badStatusAdd.speedDown > 0.0 && (double) status.badStatusTotal.speedDown >= (double) this.badStatusMax.speedDown)
      this.OnSpeedDown();
    if ((double) status.badStatusAdd.bleeding > 0.0 && (double) status.badStatusTotal.bleeding >= (double) this.badStatusMax.bleeding)
      this.OnBleedingStart();
    if ((double) status.badStatusAdd.attackSpeedDown > 0.0 && (double) status.badStatusTotal.attackSpeedDown >= (double) this.badStatusMax.attackSpeedDown)
      this.OnAttackSpeedDown();
    if ((double) status.badStatusAdd.inkSplash > 0.0 && (double) status.badStatusTotal.inkSplash >= (double) this.badStatusMax.inkSplash && !this.IsInkSplash())
      this.OnInkSplash(status.attackInfo.inkSplashInfo);
    if ((double) status.badStatusAdd.slide > 0.0 && (double) status.badStatusTotal.slide >= (double) this.badStatusMax.slide)
      this.OnSlideStart();
    if ((double) status.badStatusAdd.silence > 0.0 && (double) status.badStatusTotal.silence >= (double) this.badStatusMax.silence)
      this.OnSilenceStart();
    if ((double) status.badStatusAdd.cantHealHp > 0.0 && (double) status.badStatusTotal.cantHealHp >= (double) this.badStatusMax.cantHealHp)
      this.OnCantHealHpStart();
    if ((double) status.badStatusAdd.blind > 0.0 && (double) status.badStatusTotal.blind >= (double) this.badStatusMax.blind)
      this.OnBlindStart();
    if ((double) status.badStatusTotal.stone >= (double) this.badStatusMax.stone && !this.IsStone())
    {
      if (!this.buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.STONE))
        status.reactionType = 21;
      else
        status.badStatusTotal.stone = 0.0f;
    }
    if ((double) status.badStatusAdd.acid > 0.0 && (double) status.badStatusTotal.acid >= (double) this.badStatusMax.acid)
      this.OnAcidStart();
    if ((double) status.badStatusAdd.damageMotionStop > 0.0 && (double) status.badStatusTotal.damageMotionStop >= (double) this.badStatusMax.damageMotionStop)
      this.OnDamageMotionStopStart(status.attackInfo.motionStopTime);
    if ((double) status.badStatusAdd.corruption > 0.0 && (double) status.badStatusTotal.corruption >= (double) this.badStatusMax.corruption)
      this.OnCorruptionStart();
    Quaternion rotation1 = this._rotation;
    float y1 = ((Quaternion) ref rotation1).eulerAngles.y;
    if (status.afterHP <= 0)
    {
      int deadReviveCount = this.GetDeadReviveCount();
      if (0 < deadReviveCount)
      {
        status.afterHP = 1;
        status.reactionType = 22;
        status.deadReviveCount = deadReviveCount;
        status.badStatusAdd.Reset();
        status.downTotal = 0.0f;
        status.downAddBase = 0.0f;
        status.downAddWeak = 0.0f;
        status.concussionTotal = 0.0f;
        status.concussionAdd = 0.0f;
        status.isArrowBleed = false;
        status.isShadowSealing = false;
        status.isArrowBomb = false;
      }
      else
        status.reactionType = 8;
      if (flag)
        this.PlayImmediateDeathEffect();
    }
    else if (status.reactionType == 0 && !this.isDead)
    {
      Character.REACTION_TYPE reactionType = Character.REACTION_TYPE.NONE;
      if (this.IsHitReactionValid(status))
        reactionType = this.OnHitReaction(status);
      status.reactionType = (int) reactionType;
    }
    if (status.reactionType != 0)
    {
      status.reactionType = (int) this.CheckReActionTolerance(status);
      if (status.reactionType == 0)
        this._rotation = Quaternion.AngleAxis(y1, Vector3.up);
    }
    if (this.enableReactionDelay && this.IsReactionDelayType(status.reactionType))
    {
      this.RegisterReacionDelayInfo(new Character.DelayReactionInfo()
      {
        type = (Character.REACTION_TYPE) status.reactionType,
        targetId = status.fromObjectID,
        reactionLoopTime = status.attackInfo.toEnemy.reactionInfo.reactionLoopTime
      });
      status.reactionType = 0;
      this.isReactionDelaySet = true;
    }
    status.hostPos = this._position;
    AttackedHitStatusOwner attackedHitStatusOwner = status;
    Quaternion rotation2 = this._rotation;
    double y2 = (double) ((Quaternion) ref rotation2).eulerAngles.y;
    attackedHitStatusOwner.hostDir = (float) y2;
    status.damageHpRate = (float) ((1.0 - (double) status.afterHP / (double) this.hpMax) * 100.0);
    if (status.validDamage)
      this.buffParam.DecreaseInvincibleCount();
    base.OnAttackedHitOwner(status);
  }

  protected void ApplyInvicibleCount(AttackedHitStatusOwner status)
  {
    if (!this.buffParam.IsValidInvincibleCountBuff())
      return;
    status.damage = 0;
    status.damageDetails.Set(0.0f);
    status.badStatusAdd.Reset();
  }

  protected virtual bool ApplyInvicibleBadStatus(AttackedHitStatusOwner status)
  {
    if (!this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS) || !status.badStatusAdd.isExist())
      return false;
    status.badStatusAdd.Reset();
    return true;
  }

  protected void PlayImmediateDeathEffect()
  {
    if (Object.op_Equality((Object) this.effectPlayProcessor, (Object) null))
      return;
    List<EffectPlayProcessor.EffectSetting> settings = this.effectPlayProcessor.GetSettings("IMMEDIATE_DEATH_EFFECT");
    if (settings == null)
      return;
    for (int index = 0; index < settings.Count; ++index)
    {
      if (settings[index] != null)
        this.effectPlayProcessor.PlayEffect(settings[index], this._transform);
    }
  }

  protected virtual bool IsNarrowEscape(AttackedHitStatusOwner status) => false;

  protected virtual void UseNarrowEscape(AttackedHitStatusOwner status)
  {
  }

  protected virtual bool IsHitReactionValid(AttackedHitStatusOwner status) => true;

  protected virtual int GetDeadReviveCount() => 0;

  protected virtual bool IsReactionDelayType(int type)
  {
    switch ((Character.REACTION_TYPE) type)
    {
      case Character.REACTION_TYPE.PARALYZE:
      case Character.REACTION_TYPE.FREEZE:
        return true;
      default:
        return false;
    }
  }

  protected virtual Character.REACTION_TYPE OnHitReaction(AttackedHitStatusOwner status)
  {
    return Character.REACTION_TYPE.NONE;
  }

  protected virtual Character.REACTION_TYPE CheckReActionTolerance(AttackedHitStatusOwner status)
  {
    Character.REACTION_TYPE reactionType = (Character.REACTION_TYPE) status.reactionType;
    if (reactionType == Character.REACTION_TYPE.GUARD_DAMAGE)
      return reactionType;
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.SOUNDWAVE)
    {
      if (this.buffParam.IsHalfReaction(BuffParam.TOLERANCETYPE.SOUNDWAVE))
        reactionType = Character.REACTION_TYPE.DAMAGE;
      else if (this.buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.SOUNDWAVE))
        reactionType = Character.REACTION_TYPE.NONE;
    }
    if (reactionType == Character.REACTION_TYPE.STUNNED_BLOW && this.buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.STUMBLE))
      reactionType = Character.REACTION_TYPE.BLOW;
    if (reactionType == Character.REACTION_TYPE.SHAKE && this.buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.SHAKE))
      reactionType = Character.REACTION_TYPE.NONE;
    if (reactionType == Character.REACTION_TYPE.CHARM_BLOW && this.buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.CHARM))
      reactionType = Character.REACTION_TYPE.BLOW;
    return reactionType;
  }

  public override void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    base.OnAttackedHitFix(status);
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      MonoBehaviourSingleton<InGameProgress>.I.OnDamage(status, this);
    if (this.isDead)
      return;
    if (this.isLocalDamageApply && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId == status.fromClientID)
    {
      this.localDamage -= status.damage;
      if (this.localDamage <= 0)
        this.localDamage = 0;
    }
    this.hp = status.afterHP;
    this.ShieldHp = (XorInt) status.afterShieldHp;
    this.badStatusTotal = status.badStatusTotal;
    Character.ReactionInfo reactionInfo;
    this.MakeReactionInfo(status, out reactionInfo);
    if (reactionInfo.reactionType != Character.REACTION_TYPE.NONE)
      this.ApplySyncPosition(status.hostPos, status.hostDir);
    this.ActReaction(reactionInfo);
    if (string.IsNullOrEmpty(status.attackInfo.remainEffectName) || this.IsIgnoreHitEffect(status.attackInfo))
      return;
    Transform effect = EffectManager.GetEffect(status.attackInfo.remainEffectName, this.rootNode);
    if (!Object.op_Inequality((Object) effect, (Object) null))
      return;
    this.damegeRemainEffect = ((Component) effect).gameObject;
  }

  protected virtual void MakeReactionInfo(
    AttackedHitStatusFix status,
    out Character.ReactionInfo reactionInfo)
  {
    reactionInfo = new Character.ReactionInfo();
    reactionInfo.reactionType = (Character.REACTION_TYPE) status.reactionType;
    reactionInfo.blowForce = status.blowForce;
    reactionInfo.loopTime = status.attackInfo.toPlayer.reactionLoopTime;
    reactionInfo.targetId = status.fromObjectID;
  }

  private bool IsIgnoreHitEffect(AttackHitInfo _info)
  {
    if (_info == null)
      return false;
    switch (_info.attackType)
    {
      case AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT:
      case AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST:
        return true;
      default:
        return false;
    }
  }

  public virtual void ActReaction(Character.ReactionInfo info, bool isSync = false)
  {
    switch (info.reactionType)
    {
      case Character.REACTION_TYPE.DAMAGE:
        this.ActDamage();
        break;
      case Character.REACTION_TYPE.DEAD:
        this.ActDead();
        break;
      case Character.REACTION_TYPE.PARALYZE:
        this.ActParalyze();
        break;
      case Character.REACTION_TYPE.FREEZE:
        this.ActFreezeStart();
        break;
    }
    if (!Object.op_Inequality((Object) this.characterSender, (Object) null) || info.reactionType == Character.REACTION_TYPE.NONE)
      return;
    this.characterSender.OnActReaction(info, isSync);
  }

  protected void RegisterReacionDelayInfo(Character.DelayReactionInfo newInfo)
  {
    if (this.SearchReactionDelayInfo(newInfo.type) != null)
      return;
    this.m_reactionDelayList.Add(newInfo);
  }

  protected Character.DelayReactionInfo SearchReactionDelayInfo(Character.REACTION_TYPE targetType)
  {
    int count = this.m_reactionDelayList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.m_reactionDelayList[index].type == targetType)
        return this.m_reactionDelayList[index];
    }
    return (Character.DelayReactionInfo) null;
  }

  private void UpdateReactionDelay()
  {
    if (!this.isReactionDelaySet || this.enableReactionDelay || !this.IsCoopNone() && !this.IsOriginal())
      return;
    this.OnReactionDelay(this.m_reactionDelayList);
    this.m_reactionDelayList.Clear();
    this.isReactionDelaySet = false;
  }

  public virtual void OnReactionDelay(
    List<Character.DelayReactionInfo> reactionDelayList)
  {
    int count = reactionDelayList.Count;
    if (count <= 0)
      return;
    for (int index = 0; index < count; ++index)
    {
      switch (reactionDelayList[index].type)
      {
        case Character.REACTION_TYPE.PARALYZE:
          this.ActParalyze();
          break;
        case Character.REACTION_TYPE.FREEZE:
          this.ActFreezeStart();
          break;
      }
    }
    if (!Object.op_Inequality((Object) this.characterSender, (Object) null))
      return;
    this.characterSender.OnReactionDelay(reactionDelayList);
  }

  protected override void OnAttackedContinuationFixedUpdate(
    StageObject.AttackedContinuationStatus status)
  {
    base.OnAttackedContinuationFixedUpdate(status);
    if (this.isDead)
      return;
    float continuationTimeChangeRate = this.GetContinuationTimeChangeRate(status);
    if (status.attackInfo.type != AttackContinuationInfo.CONTINUATION_TYPE.INHALE || this.actionID == Character.ACTION_ID.MOVE && this.moveType == Character.MOVE_TYPE.SYNC_VELOCITY || status.fromCollider == null)
      return;
    Bounds bounds = status.fromCollider.bounds;
    Vector3 vector3 = Vector3.op_Subtraction(((Bounds) ref bounds).center, this._position);
    vector3.y = 0.0f;
    float num = status.attackInfo.inhale.speed * continuationTimeChangeRate;
    float magnitude = ((Vector3) ref vector3).magnitude;
    if ((double) num * (double) Time.fixedDeltaTime > (double) magnitude)
      num = magnitude / Time.fixedDeltaTime;
    ((Vector3) ref vector3).Normalize();
    this.externalVelocity = Vector3.op_Multiply(vector3, num);
  }

  public virtual Vector3 GetTransformForward()
  {
    Vector3 transformForward = this._forward;
    if (Vector3.op_Inequality(this.lerpRotateVec, Vector3.zero))
      transformForward = this.lerpRotateVec;
    transformForward.y = 0.0f;
    return transformForward;
  }

  public virtual void AddObjectList(GameObject game_object, Character.OBJECT_LIST_TYPE type = Character.OBJECT_LIST_TYPE.DEFAULT)
  {
    if (type < Character.OBJECT_LIST_TYPE.DEFAULT || type >= Character.OBJECT_LIST_TYPE.NUM)
      return;
    game_object.AddComponent<DisableNotifyMonoBehaviour>().SetNotifyMaster((DisableNotifyMonoBehaviour) this);
    this.objectList[(int) type].Add(game_object);
  }

  public virtual void DestroyObjectList(Character.OBJECT_LIST_TYPE type)
  {
    List<GameObject> gameObjectList = this.objectList[(int) type];
    gameObjectList.GetRange(0, gameObjectList.Count).ForEach((Action<GameObject>) (o => EffectManager.ReleaseEffect(o)));
    gameObjectList.Clear();
  }

  protected override void OnDetachServant(DisableNotifyMonoBehaviour servant)
  {
    this.objectList.ForEach((Action<List<GameObject>>) (o => o.Remove(((Component) servant).gameObject)));
    this.buffParam.OnDetachServant(servant);
  }

  public virtual string EffectNameAnalyzer(string effect_name) => effect_name;

  public override Transform FindNode(string name)
  {
    return name == "BODY" && Object.op_Inequality((Object) this.body, (Object) null) ? this.body : base.FindNode(name);
  }

  public virtual bool CanPlayEffectEvent() => true;

  private void EventExAtkColliderStart(AnimEventData.EventData data)
  {
    float[] floatArgs = data.floatArgs;
    Vector3 pos;
    // ISSUE: explicit constructor call
    ((Vector3) ref pos).\u002Ector(floatArgs[0], floatArgs[1], floatArgs[2]);
    Vector3 rot;
    // ISSUE: explicit constructor call
    ((Vector3) ref rot).\u002Ector(floatArgs[3], floatArgs[4], floatArgs[5]);
    float radius = floatArgs[6];
    float height = floatArgs[7];
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    int intArg = data.intArgs[0];
    AttackInfo attackInfo = this.FindAttackInfo(stringArg1);
    if (attackInfo == null)
      return;
    Transform node = this.FindNode(stringArg2);
    if (Object.op_Equality((Object) node, (Object) null))
      return;
    int attackLayer = this.objectType == StageObject.OBJECT_TYPE.ENEMY ? 15 : 14;
    AttackColliderObject attackColliderObject = new GameObject("AttackColliderObject").AddComponent<AttackColliderObject>();
    attackColliderObject.InitializeForExAtkCollider((StageObject) this, node, attackInfo, pos, rot, radius, height, attackLayer);
    attackColliderObject.UniqueID = intArg;
    attackColliderObject.DetachRigidbody();
    this.m_exAtkColliderObjectList.Add(attackColliderObject);
  }

  private void EventExAtkColliderEnd(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    for (int index = this.m_exAtkColliderObjectList.Count - 1; index >= 0; --index)
    {
      if (this.m_exAtkColliderObjectList[index].UniqueID == intArg)
      {
        Object.Destroy((Object) ((Component) this.m_exAtkColliderObjectList[index]).gameObject);
        this.m_exAtkColliderObjectList.RemoveAt(index);
      }
    }
  }

  private void DeleteExAtkColliderAll()
  {
    for (int index = 0; index < this.m_exAtkColliderObjectList.Count; ++index)
      Object.Destroy((Object) ((Component) this.m_exAtkColliderObjectList[index]).gameObject);
    this.m_exAtkColliderObjectList.Clear();
  }

  private void EventRootMotionON(AnimEventData.EventData data)
  {
    this.animator.applyRootMotion = true;
  }

  private void EventRootMotionOFF(AnimEventData.EventData data)
  {
    this.animator.applyRootMotion = false;
    if (this.velocityType != Character.VELOCITY_TYPE.ROOT_MOTION)
      return;
    this.SetVelocity(Vector3.zero);
  }

  private void EventRootMotionMoveRate(AnimEventData.EventData data)
  {
    this.rootMotionMoveRate = data.floatArgs[0];
  }

  private void EventHideRendererON(AnimEventData.EventData data)
  {
    this.SetEnableNodeRenderer(data.stringArgs[0], false);
  }

  private void EventHideRendererOFF(AnimEventData.EventData data)
  {
    this.SetEnableNodeRenderer(data.stringArgs[0], true);
  }

  public void EventActionRendererON(AnimEventData.EventData data)
  {
    if (!Object.op_Inequality((Object) this.actionRendererModel, (Object) null))
      return;
    Transform node = this.FindNode(this.actionRendererNodeName);
    if (!Object.op_Inequality((Object) node, (Object) null))
      return;
    this.actionRendererInstance = ResourceUtility.Realizes((Object) this.actionRendererModel, node);
  }

  private void EventActionRendererOFF(AnimEventData.EventData data)
  {
    if (!Object.op_Inequality((Object) this.actionRendererInstance, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this.actionRendererInstance).gameObject);
    this.actionRendererInstance = (Transform) null;
  }

  private void EventEffectDelete(AnimEventData.EventData data)
  {
    string str = this.EffectNameAnalyzer(data.stringArgs[0]);
    bool immediate = !((IList<int>) data.intArgs).IsNullOrEmpty<int>() && data.intArgs[0] > 0;
    int count = this.objectList[2].Count;
    List<GameObject> range = this.objectList[2].GetRange(0, count);
    for (int index = 0; index < count; ++index)
    {
      if (string.IsNullOrEmpty(str) || ((Object) range[index]).name.StartsWith(str))
      {
        EffectManager.ReleaseEffect(range[index], !immediate, immediate);
        this.objectList[2].Remove(range[index]);
      }
    }
  }

  private void EventUpdateActionPosition(AnimEventData.EventData data)
  {
    string trigger = data.stringArgs.Length != 0 ? data.stringArgs[0] : (string) null;
    if (string.IsNullOrEmpty(trigger))
      trigger = "next";
    if (this.IsCoopNone() || this.IsOriginal())
      this.UpdateActionPosition(trigger);
    else if (this.actionPositionWaitSync)
    {
      Log.Error(LOG.INGAME, "Character UPDATE_ACTION_POSITION Err. ( WaitSync already. ) trigger : " + trigger);
    }
    else
    {
      this.actionPositionWaitSync = true;
      this.actionPositionWaitTrigger = trigger;
      this.StartWaitingPacket(StageObject.WAITING_PACKET.CHARACTER_UPDATE_ACTION_POSITION, false);
    }
  }

  private void EventUpdateDirection(AnimEventData.EventData data)
  {
    string trigger = data.stringArgs.Length != 0 ? data.stringArgs[0] : (string) null;
    if (string.IsNullOrEmpty(trigger))
      trigger = "next";
    if (this.IsCoopNone() || this.IsOriginal())
      this.UpdateDirection(trigger);
    else if (this.directionWaitSync)
    {
      Log.Error(LOG.INGAME, "Character UPDATE_DIRECTION Err. ( WaitSync already. ) trigger : " + trigger);
    }
    else
    {
      this.directionWaitSync = true;
      this.directionWaitTrigger = trigger;
      this.StartWaitingPacket(StageObject.WAITING_PACKET.CHARACTER_UPDATE_DIRECTION, false);
    }
  }

  private void EventPeriodicSyncActionPositionStart(AnimEventData.EventData data)
  {
    this.periodicSyncActionPositionLastTime = this.GetActMotionTime();
    this.periodicSyncActionPositionFlag = true;
    this.SetPeriodicSyncTarget(this.actionTarget);
  }

  private void EventPeriodicSyncActionPositionEnd(AnimEventData.EventData data)
  {
    this.periodicSyncActionPositionFlag = false;
    this.periodicSyncActionPositionLastTime = 0.0f;
    this.SetPeriodicSyncTarget((StageObject) null);
  }

  protected virtual void EventMoveStart(AnimEventData.EventData data, Vector3 targetDir)
  {
    float floatArg = data.floatArgs[0];
    this.EventMoveEnd();
    this.enableEventMove = true;
    this.enableAddForce = false;
    this.eventMoveVelocity = Vector3.op_Multiply(targetDir, floatArg);
    this.SetVelocity(Quaternion.op_Multiply(Quaternion.LookRotation(this.GetTransformForward()), this.eventMoveVelocity), Character.VELOCITY_TYPE.EVENT_MOVE);
    this.eventMoveTimeCount = 0.0f;
  }

  private void EventMoveForwardToTarget(AnimEventData.EventData data)
  {
    float floatArg = data.floatArgs[0];
    float num1 = data.floatArgs.Length > 1 ? data.floatArgs[1] : 0.0f;
    float num2 = data.floatArgs.Length > 2 ? data.floatArgs[2] : 0.0f;
    this.EventMoveEnd();
    if (!this.actionPositionFlag || (double) floatArg <= 0.0)
      return;
    Vector3 vector3 = Vector3.op_Subtraction(this.actionPosition, this._position);
    float num3 = ((Vector3) ref vector3).magnitude - num1;
    if ((double) num2 != 0.0 && (double) num3 > (double) num2)
      num3 = num2;
    float num4 = num3 / floatArg;
    this.enableEventMove = true;
    this.enableAddForce = false;
    this.eventMoveVelocity = Vector3.op_Multiply(Vector3.forward, num4);
    this.SetVelocity(Quaternion.op_Multiply(Quaternion.LookRotation(this.GetTransformForward()), this.eventMoveVelocity), Character.VELOCITY_TYPE.EVENT_MOVE);
    this.eventMoveTimeCount = floatArg;
  }

  private void EventMoveToWorldPos(AnimEventData.EventData data)
  {
    float floatArg = data.floatArgs[0];
    Vector3 vector3_1;
    vector3_1.x = data.floatArgs[1];
    vector3_1.y = data.floatArgs[2];
    vector3_1.z = data.floatArgs[3];
    this.EventMoveEnd();
    Vector3 vector3_2 = Vector3.op_Subtraction(vector3_1, this._position);
    float magnitude = ((Vector3) ref vector3_2).magnitude;
    if ((double) floatArg <= 1.0000000116860974E-07)
      return;
    float num = magnitude / floatArg;
    if ((double) num <= 0.0)
      return;
    Vector3 vector3_3 = Vector3.op_Subtraction(vector3_1, this._position);
    Vector3 normalized = ((Vector3) ref vector3_3).normalized;
    this.enableEventMove = false;
    this.enableAddForce = false;
    this.enableRootMotion = false;
    this.eventMoveVelocity = Vector3.op_Multiply(normalized, floatArg);
    this.SetVelocity(this.eventMoveVelocity, Character.VELOCITY_TYPE.EVENT_MOVE);
    this.eventMoveTimeCount = num;
  }

  private void EventMoveSidewaysLookTarget(AnimEventData.EventData data)
  {
    if (data.floatArgs.Length < 2)
      return;
    this.moveAngle_deg = data.floatArgs[0];
    this.moveAngleSpeed_deg = data.floatArgs[1];
  }

  private void EventMoveLookAtPosition(AnimEventData.EventData data)
  {
    double floatArg1 = (double) data.floatArgs[0];
    float floatArg2 = data.floatArgs[1];
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(data.floatArgs[2], 0.0f, data.floatArgs[3]);
    if (floatArg1 <= 0.0)
      return;
    this.moveLookAtAngle = floatArg2;
    this.moveLookAtPos = vector3;
  }

  private void EventRotateToTargetStart(AnimEventData.EventData data)
  {
    float floatArg = data.floatArgs[0];
    float num = data.floatArgs.Length > 1 ? data.floatArgs[1] : 0.0f;
    this.EndRotate();
    this.rotateToTargetFlag = true;
    this.rotateEventSpeed = floatArg;
    this.rotateToTargetDiffAngle = num;
  }

  private void EventRotateKeepToTargetStart(AnimEventData.EventData data)
  {
    float floatArg = data.floatArgs[0];
    this.EndRotate();
    this.rotateEventSpeed = floatArg;
    this.rotateEventKeep = true;
  }

  private void EventRotateToAngleStart(AnimEventData.EventData data)
  {
    float floatArg1 = data.floatArgs[0];
    float floatArg2 = data.floatArgs[1];
    this.EndRotate();
    Quaternion rotation = this._rotation;
    this.rotateEventDirection = ((Quaternion) ref rotation).eulerAngles.y + floatArg2 * this.rootRotationRate;
    if ((double) floatArg1 > 0.0)
      this.rotateEventSpeed = floatArg1;
    else
      this._rotation = Quaternion.AngleAxis(this.rotateEventDirection, Vector3.up);
  }

  private void EventRotateToTargetOffset(AnimEventData.EventData data)
  {
    float floatArg = data.floatArgs[0];
    this.EndRotate();
    bool flag = data.intArgs[0] != 0;
    Vector3 vector3_1;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_1).\u002Ector(0.0f, 0.0f, data.floatArgs[1]);
    Vector3 pos = this._position;
    Quaternion quaternion;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
    {
      pos = MonoBehaviourSingleton<StageObjectManager>.I.boss._position;
      if (flag)
      {
        quaternion = MonoBehaviourSingleton<StageObjectManager>.I.boss._rotation;
        vector3_1 = Quaternion.op_Multiply(Quaternion.Euler(((Quaternion) ref quaternion).eulerAngles), vector3_1);
      }
    }
    else
      this.GetTargetPos(out pos);
    pos.y = 0.0f;
    if (!flag)
      vector3_1 = Quaternion.op_Multiply(Quaternion.LookRotation(Vector3.op_Subtraction(this._position, pos)), vector3_1);
    Vector3 vector3_2 = pos;
    if (Vector3.op_Inequality(vector3_2, this._position))
      vector3_2 = Vector3.op_Addition(vector3_2, vector3_1);
    quaternion = Quaternion.LookRotation(Vector3.op_Subtraction(vector3_2, this._position));
    this.rotateEventDirection = ((Quaternion) ref quaternion).eulerAngles.y;
    if ((double) floatArg > 0.0)
      this.rotateEventSpeed = floatArg;
    else
      this._rotation = Quaternion.AngleAxis(this.rotateEventDirection, Vector3.up);
  }

  private void EventAnimatorBoolON(AnimEventData.EventData data)
  {
    string stringArg = data.stringArgs[0];
    this.animator.SetBool(stringArg, true);
    if (this.animatorBoolList.IndexOf(stringArg) >= 0)
      return;
    this.animatorBoolList.Add(stringArg);
  }

  private void EventAnimatorBoolOFF(AnimEventData.EventData data)
  {
    string stringArg = data.stringArgs[0];
    this.animator.SetBool(stringArg, false);
    this.animatorBoolList.Remove(stringArg);
  }

  protected void EventShotGeneric(AnimEventData.EventData data)
  {
    AttackInfo attackInfo = this.FindAttackInfo(data.stringArgs[0]);
    if (attackInfo == null)
      return;
    Vector3 offset;
    // ISSUE: explicit constructor call
    ((Vector3) ref offset).\u002Ector(0.0f, 0.0f, 0.0f);
    if (data.intArgs.Length > 1 && data.intArgs[1] != 0)
    {
      if (Object.op_Inequality((Object) this.actionTarget, (Object) null) && !this.IsValidBuffBlind())
      {
        Vector3 vector3;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
        Quaternion rot = Quaternion.op_Multiply(this._rotation, Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5])));
        Vector3 localScale = this.actionTarget._transform.localScale;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3).\u002Ector(vector3.x / localScale.x, vector3.y / localScale.y, vector3.z / localScale.z);
        Matrix4x4 localToWorldMatrix = this.actionTarget._transform.localToWorldMatrix;
        Vector3 pos = ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(vector3);
        AnimEventShot.Create((StageObject) this, attackInfo, pos, rot);
        return;
      }
      offset.z += 2f;
    }
    AnimEventShot.Create((StageObject) this, data, attackInfo, offset);
  }

  protected virtual void EventShotPresent(AnimEventData.EventData data)
  {
  }

  protected virtual void EventShotZone(AnimEventData.EventData data)
  {
  }

  public virtual void EventShotDecoy(AnimEventData.EventData data)
  {
  }

  private void EventGenerateTrackingAttack(AnimEventData.EventData data)
  {
    if (data.stringArgs == null || data.stringArgs.Length == 0)
    {
      Log.Error(LOG.INGAME, "String Data is Empty. Check AnimEvent ( GENERATE_TRACKING ). ");
    }
    else
    {
      AttackTrackingTarget attackTrackingTarget = new GameObject("AttackTrackingTarget").AddComponent<AttackTrackingTarget>();
      attackTrackingTarget.Initialize((StageObject) this, this.IsValidBuffBlind() ? (StageObject) null : this.actionTarget, this.FindAttackInfo(data.stringArgs[0]));
      this.TrackingTargetBullet = attackTrackingTarget;
    }
  }

  private void EventTrackingBulletOff()
  {
    if (Object.op_Equality((Object) this.TrackingTargetBullet, (Object) null))
      return;
    this.TrackingTargetBullet.TrackOff();
  }

  protected virtual void EventStatusUpDefenceON(AnimEventData.EventData data)
  {
  }

  protected virtual void EventStatusUpDefenceOFF()
  {
  }

  protected virtual void EventCameraTargetOffsetOn(AnimEventData.EventData data)
  {
  }

  protected virtual void EventCameraTargetOffsetOff()
  {
  }

  public virtual void EventCameraTargetRotateOn(AnimEventData.EventData data)
  {
  }

  public virtual void EventCameraTargetRotateOff()
  {
  }

  protected virtual void EventExecuteEvolve(AnimEventData.EventData data)
  {
  }

  protected virtual void EventCameraStopOn(AnimEventData.EventData data)
  {
  }

  protected virtual void EventCameraStopOff()
  {
  }

  protected virtual void EventCameraCutOn(AnimEventData.EventData data)
  {
  }

  protected virtual void EventCameraCutOff()
  {
  }

  public override void OnAnimEvent(AnimEventData.EventData data)
  {
    if (this.CanPlayEffectEvent())
    {
      bool beforeTrailSetting = this.SetTrailSetting();
      bool is_oneshot_priority = this.IsOneShotPriority();
      bool isExecEffect = true;
      if ((data.id == AnimEventFormat.ID.EFFECT || data.id == AnimEventFormat.ID.EFFECT_LOOP_CUSTOM || data.id == AnimEventFormat.ID.EFFECT_ONESHOT || data.id == AnimEventFormat.ID.EFFECT_STATIC || data.id == AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE || data.id == AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT || data.id == AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE || data.id == AnimEventFormat.ID.CAMERA_EFFECT || data.id == AnimEventFormat.ID.EFFECT_SWITCH_OBJECT_BY_CONDITION || data.id == AnimEventFormat.ID.EFFECT_TILING || data.id == AnimEventFormat.ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS) && data.intArgs != null && data.intArgs.Length > 1)
      {
        switch (data.intArgs[0])
        {
          case 0:
            isExecEffect = true;
            break;
          case 1:
            isExecEffect = !this.buffParam.IsEnableBuff((BuffParam.BUFFTYPE) data.intArgs[1]);
            break;
          default:
            Log.Error(LOG.EFFECT, "Not Defined EFFECT_EXEC_CONDITION");
            break;
        }
      }
      if (this.ShotAnimEvent(data, beforeTrailSetting, is_oneshot_priority, isExecEffect))
        return;
    }
    else if ((data.id == AnimEventFormat.ID.EFFECT || data.id == AnimEventFormat.ID.EFFECT_LOOP_CUSTOM || data.id == AnimEventFormat.ID.EFFECT_ONESHOT || data.id == AnimEventFormat.ID.EFFECT_STATIC || data.id == AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE || data.id == AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT || data.id == AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE || data.id == AnimEventFormat.ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS) && data.intArgs != null && data.intArgs.Length > 2 && data.intArgs[2] == 1)
    {
      bool beforeTrailSetting = this.SetTrailSetting();
      bool is_oneshot_priority = this.IsOneShotPriority();
      bool isExecEffect = true;
      switch (data.intArgs[0])
      {
        case 0:
          isExecEffect = true;
          break;
        case 1:
          isExecEffect = !this.buffParam.IsEnableBuff((BuffParam.BUFFTYPE) data.intArgs[1]);
          break;
        default:
          Log.Error(LOG.EFFECT, "Not Defined EFFECT_EXEC_CONDITION");
          break;
      }
      if (this.ShotAnimEvent(data, beforeTrailSetting, is_oneshot_priority, isExecEffect))
        return;
    }
    if (Object.op_Inequality((Object) this.stepCtrl, (Object) null) && this.stepCtrl.OnAnimEvent(data))
      return;
    switch (data.id)
    {
      case AnimEventFormat.ID.ROOT_MOTION_ON:
        this.EventRootMotionON(data);
        break;
      case AnimEventFormat.ID.ROOT_MOTION_OFF:
        this.EventRootMotionOFF(data);
        break;
      case AnimEventFormat.ID.ROOT_MOTION_MOVE_RATE:
        this.EventRootMotionMoveRate(data);
        break;
      case AnimEventFormat.ID.HIDE_RENDERER_ON:
        this.EventHideRendererON(data);
        break;
      case AnimEventFormat.ID.HIDE_RENDERER_OFF:
        this.EventHideRendererOFF(data);
        break;
      case AnimEventFormat.ID.ACTION_RENDERER_ON:
        this.EventActionRendererON(data);
        break;
      case AnimEventFormat.ID.ACTION_RENDERER_OFF:
        this.EventActionRendererOFF(data);
        break;
      case AnimEventFormat.ID.EFFECT:
        break;
      case AnimEventFormat.ID.EFFECT_ONESHOT:
        break;
      case AnimEventFormat.ID.EFFECT_STATIC:
        break;
      case AnimEventFormat.ID.EFFECT_DELETE:
        this.EventEffectDelete(data);
        break;
      case AnimEventFormat.ID.EFFECT_LOOP_CUSTOM:
        break;
      case AnimEventFormat.ID.CAMERA_EFFECT:
        break;
      case AnimEventFormat.ID.UPDATE_ACTION_POSITION:
        this.EventUpdateActionPosition(data);
        break;
      case AnimEventFormat.ID.UPDATE_DIRECTION:
        this.EventUpdateDirection(data);
        break;
      case AnimEventFormat.ID.PERIODIC_SYNC_ACTION_POSITION_START:
        this.EventPeriodicSyncActionPositionStart(data);
        break;
      case AnimEventFormat.ID.PERIODIC_SYNC_ACTION_POSITION_END:
        this.EventPeriodicSyncActionPositionEnd(data);
        break;
      case AnimEventFormat.ID.MOVE_FORWARD_START:
        this.EventMoveStart(data, Vector3.forward);
        break;
      case AnimEventFormat.ID.MOVE_LEFT_START:
        this.EventMoveStart(data, Vector3.op_UnaryNegation(Vector3.right));
        break;
      case AnimEventFormat.ID.MOVE_RIGHT_START:
        this.EventMoveStart(data, Vector3.right);
        break;
      case AnimEventFormat.ID.MOVE_FORWARD_TO_TARGET:
        this.EventMoveForwardToTarget(data);
        break;
      case AnimEventFormat.ID.MOVE_END:
        this.EventMoveEnd();
        break;
      case AnimEventFormat.ID.ROTATE_TO_TARGET_START:
        this.EventRotateToTargetStart(data);
        break;
      case AnimEventFormat.ID.ROTATE_KEEP_TO_TARGET_START:
        this.EventRotateKeepToTargetStart(data);
        break;
      case AnimEventFormat.ID.ROTATE_TO_ANGLE_START:
        this.EventRotateToAngleStart(data);
        break;
      case AnimEventFormat.ID.ROTATE_END:
        this.EndRotate();
        break;
      case AnimEventFormat.ID.MOTION_CANCEL_ON:
        this.enableMotionCancel = true;
        break;
      case AnimEventFormat.ID.MOTION_CANCEL_OFF:
        this.enableMotionCancel = false;
        break;
      case AnimEventFormat.ID.ANIMATOR_BOOL_ON:
        this.EventAnimatorBoolON(data);
        break;
      case AnimEventFormat.ID.ANIMATOR_BOOL_OFF:
        this.EventAnimatorBoolOFF(data);
        break;
      case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE:
      case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_START:
        this.CreateAttackCollider(data);
        break;
      case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_END:
        this.RemoveEventCollider(data.stringArgs[0]);
        break;
      case AnimEventFormat.ID.SHOT_GENERIC:
        this.EventShotGeneric(data);
        break;
      case AnimEventFormat.ID.MOVE_SUPPRESS_ON:
        this.enableMoveSuppress = true;
        break;
      case AnimEventFormat.ID.MOVE_SUPPRESS_OFF:
        this.enableMoveSuppress = false;
        break;
      case AnimEventFormat.ID.REACTON_DELAY_ON:
        this.enableReactionDelay = true;
        break;
      case AnimEventFormat.ID.REACTON_DELAY_OFF:
        this.enableReactionDelay = false;
        break;
      case AnimEventFormat.ID.DELETE_REMAIN_DMG_EFFECT:
        if (!Object.op_Inequality((Object) this.damegeRemainEffect, (Object) null))
          break;
        EffectManager.ReleaseEffect(this.damegeRemainEffect);
        this.damegeRemainEffect = (GameObject) null;
        break;
      case AnimEventFormat.ID.BUFF_START:
        if (!this.IsCoopNone() && !this.IsOriginal())
          break;
        if (data.intArgs.Length == 0 || data.floatArgs.Length == 0)
          Log.Error(LOG.INGAME, "No data. Check AnimEvent ( BUFF_START ).");
        float num1 = data.floatArgs[0];
        if ((double) num1 <= 0.0)
        {
          Log.Error(LOG.INGAME, "Not set Buff time. Check AnimEvent ( BUFF_START ).");
          break;
        }
        float num2 = 0.0f;
        if (data.floatArgs.Length > 1)
          num2 = data.floatArgs[1];
        float num3 = 0.0f;
        if (data.floatArgs.Length > 2)
          num3 = data.floatArgs[2];
        int intArg = data.intArgs[0];
        if (intArg <= -1 || intArg >= 221)
        {
          Log.Error(LOG.INGAME, "Not set valid BUFFTYPE. CHECK AnimEvent ( BUFF_START ).");
          break;
        }
        BuffParam.VALUE_TYPE valueType = BuffParam.VALUE_TYPE.CONSTANT;
        if (data.intArgs.Length >= 3)
          valueType = (BuffParam.VALUE_TYPE) data.intArgs[2];
        bool flag = false;
        if (data.intArgs.Length >= 4)
        {
          flag = data.intArgs[3] > 0;
          if (flag)
            num1 = -1f;
        }
        BuffParam.BuffData data1 = new BuffParam.BuffData();
        data1.type = (BuffParam.BUFFTYPE) intArg;
        data1.time = (double) num3 == 0.0 ? num1 : num3;
        data1.interval = num2;
        data1.endless = new bool?(flag);
        data1.valueType = valueType;
        data1.value = data.intArgs[1];
        this.SetFromInfo(ref data1);
        this.OnBuffStart(data1);
        break;
      case AnimEventFormat.ID.BUFF_END:
        if (!this.IsCoopNone() && !this.IsOriginal())
          break;
        this.OnBuffEnd((BuffParam.BUFFTYPE) data.intArgs[0], true);
        break;
      case AnimEventFormat.ID.CONTINUS_ATTACK:
        if (!this.IsCoopNone() && !this.IsOriginal())
          break;
        this.CreateContinusAttack(data, true);
        break;
      case AnimEventFormat.ID.NWAY_LASER_ATTACK:
        this.EventNWayLaserAttack(data);
        break;
      case AnimEventFormat.ID.CHANGE_SHADER_PARAM:
        this.EventChangeShaderParam(data);
        break;
      case AnimEventFormat.ID.PLAYER_DISABLE_MOVE:
        if (!MonoBehaviourSingleton<InputManager>.IsValid() || (MonoBehaviourSingleton<InputManager>.I.disableFlags & INPUT_DISABLE_FACTOR.INGAME_COMMAND) != (INPUT_DISABLE_FACTOR) 0)
          break;
        MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_COMMAND, true);
        this.StartCoroutine(this.SetEnableInputAfterSeconds(data.floatArgs[0]));
        break;
      case AnimEventFormat.ID.EXATK_COLLIDER_START:
        this.EventExAtkColliderStart(data);
        break;
      case AnimEventFormat.ID.EXATK_COLLIDER_END:
        this.EventExAtkColliderEnd(data);
        break;
      case AnimEventFormat.ID.GENERATE_TRACKING:
        this.EventGenerateTrackingAttack(data);
        break;
      case AnimEventFormat.ID.MOVE_SIDEWAYS_LOOK_TARGET:
        this.EventMoveSidewaysLookTarget(data);
        break;
      case AnimEventFormat.ID.ACTION_MINE_ATTACK:
        this.EventActionMineAttack(data);
        break;
      case AnimEventFormat.ID.SHOT_REFLECT_BULLET:
        this.EventReflectBulletAttack(data);
        break;
      case AnimEventFormat.ID.SHOT_PRESENT:
        if (!this.IsCoopNone() && !this.IsOriginal())
          break;
        this.EventShotPresent(data);
        break;
      case AnimEventFormat.ID.MOVE_POINT_DATA:
        break;
      case AnimEventFormat.ID.STATUS_UP_DEFENCE_ON:
        this.EventStatusUpDefenceON(data);
        break;
      case AnimEventFormat.ID.STATUS_UP_DEFENCE_OFF:
        this.EventStatusUpDefenceOFF();
        break;
      case AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE:
        break;
      case AnimEventFormat.ID.SHOT_ZONE:
        if (!this.IsCoopNone() && !this.IsOriginal())
          break;
        this.EventShotZone(data);
        break;
      case AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT:
        break;
      case AnimEventFormat.ID.ATTACKHIT_CLEAR_ALL:
        this.AttackHitCheckerClearAll();
        break;
      case AnimEventFormat.ID.ATTACKHIT_CLEAR_INFO:
        this.AttackHitCheckerClearInfo(data);
        break;
      case AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE:
        break;
      case AnimEventFormat.ID.ROOT_COLLIDER_ON:
        this._collider.enabled = true;
        break;
      case AnimEventFormat.ID.ROOT_COLLIDER_OFF:
        this._collider.enabled = false;
        break;
      case AnimEventFormat.ID.SHOT_DECOY:
        if (!this.IsCoopNone() && !this.IsOriginal())
          break;
        this.EventShotDecoy(data);
        break;
      case AnimEventFormat.ID.CAMERA_TARGET_OFFSET_ON:
        this.EventCameraTargetOffsetOn(data);
        break;
      case AnimEventFormat.ID.CAMERA_TARGET_OFFSET_OFF:
        this.EventCameraTargetOffsetOff();
        break;
      case AnimEventFormat.ID.MOVE_LOOKAT_DATA:
        break;
      case AnimEventFormat.ID.MOVE_TO_WORLDPOS_START:
        this.EventMoveToWorldPos(data);
        break;
      case AnimEventFormat.ID.EXECUTE_EVOLVE:
        this.EventExecuteEvolve(data);
        break;
      case AnimEventFormat.ID.DBG_TIME_START:
        this.DbgTimeCount(true);
        break;
      case AnimEventFormat.ID.DBG_TIME_END:
        this.DbgTimeCount(false);
        break;
      case AnimEventFormat.ID.CAMERA_STOP_ON:
        this.EventCameraStopOn(data);
        break;
      case AnimEventFormat.ID.CAMERA_STOP_OFF:
        this.EventCameraStopOff();
        break;
      case AnimEventFormat.ID.CAMERA_CUT_ON:
        this.EventCameraCutOn(data);
        break;
      case AnimEventFormat.ID.CAMERA_CUT_OFF:
        this.EventCameraCutOff();
        break;
      case AnimEventFormat.ID.EFFECT_SWITCH_OBJECT_BY_CONDITION:
      case AnimEventFormat.ID.EFFECT_TILING:
        break;
      case AnimEventFormat.ID.LOAD_BULLET:
        break;
      case AnimEventFormat.ID.TRACKING_BULLET_OFF:
        this.EventTrackingBulletOff();
        break;
      case AnimEventFormat.ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS:
        break;
      case AnimEventFormat.ID.ROTATE_TO_TARGET_OFFSET:
        this.EventRotateToTargetOffset(data);
        break;
      case AnimEventFormat.ID.CAMERA_TARGET_ROTATE_ON:
        this.EventCameraTargetRotateOn(data);
        break;
      case AnimEventFormat.ID.CAMERA_TARGET_ROTATE_OFF:
        this.EventCameraTargetRotateOff();
        break;
      case AnimEventFormat.ID.ACTION_RECEIVE_DAMAGE_RATE:
        if (data.floatArgs.Length != 0)
        {
          this.actionReceiveDamageRate = data.floatArgs[0];
          break;
        }
        this.actionReceiveDamageRate = 1f;
        break;
      default:
        base.OnAnimEvent(data);
        break;
    }
  }

  private bool SetTrailSetting()
  {
    bool flag = false;
    if (!this.animUpdatePhysics)
    {
      flag = Trail.settingFixedUpdate;
      Trail.settingFixedUpdate = false;
    }
    return flag;
  }

  private bool IsOneShotPriority()
  {
    bool flag = this is Self;
    Enemy enemy = this as Enemy;
    if (Object.op_Inequality((Object) enemy, (Object) null))
      flag = enemy.isBoss;
    return flag;
  }

  private bool ShotAnimEvent(
    AnimEventData.EventData data,
    bool beforeTrailSetting,
    bool is_oneshot_priority,
    bool isExecEffect)
  {
    Transform transform = (Transform) null;
    if (isExecEffect && data.id != AnimEventFormat.ID.EFFECT_TILING)
      transform = AnimEventFormat.EffectEventExec(data.id, data, this._transform, is_oneshot_priority, new AnimEventFormat.EffectNameAnalyzer(this.EffectNameAnalyzer), new AnimEventFormat.NodeFinder(((StageObject) this).FindNode), this);
    if (!this.animUpdatePhysics)
      Trail.settingFixedUpdate = beforeTrailSetting;
    if (Object.op_Inequality((Object) transform, (Object) null))
    {
      if (data.id == AnimEventFormat.ID.EFFECT || data.id == AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE || data.id == AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT || data.id == AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE || data.id == AnimEventFormat.ID.EFFECT_SWITCH_OBJECT_BY_CONDITION)
        this.AddObjectList(((Component) transform).gameObject, Character.OBJECT_LIST_TYPE.ANIM_EVENT);
      return true;
    }
    if (!(data.id == AnimEventFormat.ID.EFFECT_TILING & isExecEffect))
      return false;
    Transform[] transformArray = AnimEventFormat.EffectsEventExec(data.id, data, this._transform, is_oneshot_priority, new AnimEventFormat.EffectNameAnalyzer(this.EffectNameAnalyzer), new AnimEventFormat.NodeFinder(((StageObject) this).FindNode), this);
    if (transformArray != null)
    {
      int index = 0;
      for (int length = transformArray.Length; index < length; ++index)
        this.AddObjectList(((Component) transformArray[index]).gameObject, Character.OBJECT_LIST_TYPE.ANIM_EVENT);
    }
    return true;
  }

  protected virtual void SetFromInfo(ref BuffParam.BuffData data)
  {
  }

  protected virtual void EventActionMineAttack(AnimEventData.EventData data)
  {
  }

  protected virtual void EventReflectBulletAttack(AnimEventData.EventData data)
  {
  }

  protected virtual void EventNWayLaserAttack(AnimEventData.EventData data)
  {
  }

  private IEnumerator SetEnableInputAfterSeconds(float seconds)
  {
    yield return (object) new WaitForSeconds(seconds);
    MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_COMMAND, false);
  }

  public void CreateContinusAttack(AnimEventData.EventData eventData, bool isSync, float exEndTime = 0.0f)
  {
    if (eventData == null)
      return;
    float endTime = (float) eventData.intArgs[1];
    if ((double) exEndTime > 0.0)
      endTime = exEndTime;
    int eventIndex = -1;
    int count = this.continusAtkEventDataList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.continusAtkEventDataList[index] == eventData)
      {
        eventIndex = index;
        break;
      }
    }
    AnimEventCollider attackCollider = this.CreateAttackCollider(eventData, false);
    attackCollider.SetFixedUpdateFlag(false);
    attackCollider.SetFixTransformUpdateFlag(false);
    attackCollider.ValidTriggerStay();
    Transform effectTrans = (Transform) null;
    string stringArg1 = eventData.stringArgs[2];
    string stringArg2 = eventData.stringArgs[3];
    if (!string.IsNullOrEmpty(stringArg1) && !string.IsNullOrEmpty(stringArg2))
    {
      Vector3 zero = Vector3.zero;
      Quaternion quaternion = Quaternion.identity;
      float[] floatArgs = eventData.floatArgs;
      if (floatArgs.Length > 8)
      {
        // ISSUE: explicit constructor call
        ((Vector3) ref zero).\u002Ector(floatArgs[8], floatArgs[9], floatArgs[10]);
        quaternion = Quaternion.Euler(floatArgs[11], floatArgs[12], floatArgs[13]);
      }
      Transform parent = Utility.Find(this._transform, stringArg2);
      effectTrans = EffectManager.GetEffect(stringArg1, parent);
      effectTrans.localPosition = zero;
      effectTrans.localRotation = quaternion;
    }
    this.continusAttackParam.Register(eventIndex, endTime, attackCollider, effectTrans);
    if (!isSync)
      return;
    this.SendContinusAttackSync();
  }

  public void CreateContinusAttackBySyncData(ContinusAttackParam.SyncData syncData)
  {
    int eventIndex = syncData.eventIndex;
    if (eventIndex < 0 || eventIndex >= this.continusAtkEventDataList.Count)
      return;
    this.CreateContinusAttack(this.continusAtkEventDataList[syncData.eventIndex], false, syncData.endTime);
  }

  public void SendContinusAttackSync()
  {
    if (!this.IsOriginal())
      return;
    ContinusAttackParam.SyncParam syncParam = this.continusAttackParam.CreateSyncParam();
    if (!Object.op_Inequality((Object) this.characterSender, (Object) null))
      return;
    this.characterSender.OnSendContinusAttackSync(syncParam);
  }

  public void ReceiveContinusAttackParam(ContinusAttackParam.SyncParam syncParam)
  {
    this.continusAttackParam.ApplySyncParam(syncParam);
  }

  protected AnimEventCollider CreateAttackCollider(
    AnimEventData.EventData eventData,
    bool isUseColliderList = true)
  {
    bool flag = true;
    AnimEventCollider attackCollider = (AnimEventCollider) null;
    if (isUseColliderList)
    {
      int index = 0;
      for (int count = this.animEventColliderList.Count; index < count; ++index)
      {
        if (this.animEventColliderList[index].isReleased)
        {
          attackCollider = this.animEventColliderList[index];
          flag = false;
          break;
        }
      }
    }
    if (flag)
    {
      attackCollider = new AnimEventCollider();
      if (isUseColliderList)
        this.animEventColliderList.Add(attackCollider);
    }
    attackCollider.Initialize((StageObject) this, eventData, this.FindAttackInfo(eventData.stringArgs[0]));
    if (eventData.id == AnimEventFormat.ID.ATK_COLLIDER_CAPSULE || eventData.id == AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE)
      attackCollider.ReserveRelease();
    return attackCollider;
  }

  public IEnumerator CreateMultiAttackCollider(
    AnimEventData.EventData eventData,
    bool isUseColliderList = true)
  {
    int generateCount = this.GetColliderGenerateCount(eventData);
    AnimEventCollider[] event_colliders = new AnimEventCollider[generateCount];
    List<AnimEventCollider> colList = new List<AnimEventCollider>((IEnumerable<AnimEventCollider>) this.animEventColliderList);
    for (int colIdx = 0; colIdx < generateCount; ++colIdx)
    {
      AnimEventCollider animEventCollider1 = event_colliders[colIdx];
      AnimEventCollider animEventCollider2 = (AnimEventCollider) null;
      if (isUseColliderList)
      {
        int index = 0;
        for (int count = colList.Count; index < count; ++index)
        {
          if (colList[index].isReleased)
          {
            animEventCollider2 = colList[index];
            colList.RemoveAt(index);
            break;
          }
        }
      }
      if (animEventCollider2 == null)
      {
        animEventCollider2 = new AnimEventCollider();
        if (isUseColliderList)
          this.animEventColliderList.Add(animEventCollider2);
      }
      animEventCollider2.Initialize((StageObject) this, eventData, this.FindAttackInfo(eventData.stringArgs[0]));
      animEventCollider2.InitTransformSettings((StageObject) this, eventData);
      if (12 != eventData.intArgs[2])
        animEventCollider2.OverwriteObjectLayer(eventData.intArgs[2]);
      animEventCollider2.ReserveRelease();
      yield return (object) null;
    }
    yield return (object) null;
  }

  protected virtual int GetColliderGenerateCount(AnimEventData.EventData eventData)
  {
    return eventData.intArgs == null || eventData.intArgs.Length < 2 ? 1 : eventData.intArgs[1];
  }

  protected void RemoveEventCollider(string targetName)
  {
    int count = this.animEventColliderList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (!this.animEventColliderList[index].isReleased)
      {
        AttackInfo attackInfo = this.animEventColliderList[index].attackInfo;
        if (attackInfo != null && attackInfo.name == targetName)
          this.animEventColliderList[index].ReserveRelease();
      }
    }
  }

  public void EventMoveEnd()
  {
    this.enableEventMove = false;
    this.enableAddForce = false;
    if (this.velocityType == Character.VELOCITY_TYPE.EVENT_MOVE)
      this.SetVelocity(Vector3.zero);
    this.eventMoveVelocity = Vector3.zero;
    this.eventMoveTimeCount = 0.0f;
  }

  protected virtual void EndRotate()
  {
    this.rotateEventSpeed = 0.0f;
    this.rotateEventDirection = 0.0f;
    this.rotateEventKeep = false;
    this.rotateToTargetFlag = false;
    this.rotateToTargetDiffAngle = 0.0f;
  }

  protected void SetPeriodicSyncTarget(StageObject target)
  {
    Character periodicSyncTarget1 = this.periodicSyncTarget as Character;
    if (Object.op_Inequality((Object) periodicSyncTarget1, (Object) null))
      periodicSyncTarget1.periodicSyncOwnerList.Remove(this);
    this.periodicSyncTarget = (StageObject) null;
    this.periodicSyncTarget = target;
    Character periodicSyncTarget2 = this.periodicSyncTarget as Character;
    if (!Object.op_Inequality((Object) periodicSyncTarget2, (Object) null))
      return;
    periodicSyncTarget2.periodicSyncOwnerList.Add(this);
  }

  public override AttackInfo[] GetAttackInfos() => this.attackInfos;

  public void SetEnableNodeRenderer(string node_name, bool enable)
  {
    Transform node = this.FindNode(node_name);
    if (Object.op_Equality((Object) node, (Object) null))
      return;
    ((Component) node).GetComponentsInChildren<Renderer>(Temporary.rendererList);
    int index1 = 0;
    for (int count = Temporary.rendererList.Count; index1 < count; ++index1)
      Temporary.rendererList[index1].enabled = enable;
    Temporary.rendererList.Clear();
    ((Component) node).GetComponentsInChildren<ParticleSystemRenderer>(Temporary.particleRenderList);
    int index2 = 0;
    for (int count = Temporary.particleRenderList.Count; index2 < count; ++index2)
      ((Renderer) Temporary.particleRenderList[index2]).enabled = enable;
    Temporary.particleRenderList.Clear();
    ((Component) node).GetComponentsInChildren<rymFX>(Temporary.fxList);
    int index3 = 0;
    for (int count = Temporary.fxList.Count; index3 < count; ++index3)
      ((Behaviour) Temporary.fxList[index3]).enabled = enable;
    Temporary.fxList.Clear();
    ((Component) node).GetComponentsInChildren<TargetPoint>(Temporary.targetPointList);
    int index4 = 0;
    for (int count = Temporary.targetPointList.Count; index4 < count; ++index4)
      ((Behaviour) Temporary.targetPointList[index4]).enabled = enable;
    Temporary.targetPointList.Clear();
    if (enable)
    {
      this.hideRendererList.Remove(node_name);
    }
    else
    {
      if (this.hideRendererList.Contains(node_name))
        return;
      this.hideRendererList.Add(node_name);
    }
  }

  public void SetEnableNodeTrailRenderer(string node_name)
  {
    Transform node = this.FindNode(node_name);
    if (Object.op_Equality((Object) node, (Object) null))
      return;
    ((Component) node).GetComponentsInChildren<Trail>(Temporary.trailList);
    for (int index = 0; index < Temporary.trailList.Count; ++index)
      Temporary.trailList[index].Reset();
    Temporary.trailList.Clear();
  }

  public virtual void ChatSay(int chatID)
  {
    if (this.IsOriginal() && MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopStage.StageChat(this.id, chatID);
    SoundManager.PlaySystemSE(SoundID.UISE.CHAT_BALOON);
  }

  public virtual void ChatSay(string message)
  {
    if (this.IsOriginal() && MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatMessage(this.id, message);
    SoundManager.PlaySystemSE(SoundID.UISE.CHAT_BALOON);
  }

  public virtual void ChatSayStamp(int stamp_id)
  {
    if (this.IsOriginal() && MonoBehaviourSingleton<CoopManager>.IsValid())
    {
      if (QuestManager.IsValidInGameExplore())
        MonoBehaviourSingleton<CoopManager>.I.coopRoom.SendChatStamp(stamp_id);
      else
        MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatStamp(this.id, stamp_id);
    }
    SoundManager.PlaySystemSE(SoundID.UISE.CHAT_BALOON);
  }

  protected void ResetStatusParam()
  {
    this.attack.Set(0.0f);
    this.defense.Set(0.0f);
    this.tolerance.Set(0.0f);
  }

  public override void OnFailedWaitingPacket(StageObject.WAITING_PACKET type)
  {
    switch (type)
    {
      case StageObject.WAITING_PACKET.CHARACTER_MOVE_VELOCITY:
        this.ActIdle();
        break;
      case StageObject.WAITING_PACKET.CHARACTER_UPDATE_ACTION_POSITION:
        this.UpdateActionPosition(this.actionPositionWaitTrigger);
        break;
      case StageObject.WAITING_PACKET.CHARACTER_UPDATE_DIRECTION:
        this.UpdateDirection(this.directionWaitTrigger);
        break;
      case StageObject.WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON:
        this.ActIdle();
        break;
    }
    base.OnFailedWaitingPacket(type);
  }

  public override Vector3 GetPredictivePosition()
  {
    if (this.IsPuppet() || this.IsMirror())
    {
      Vector3 pos;
      if (Object.op_Inequality((Object) this.packetReceiver, (Object) null) && this.packetReceiver.GetPredictivePosition(out pos))
        return pos;
      if (this.actionID == Character.ACTION_ID.MOVE && this.moveType == Character.MOVE_TYPE.SYNC_VELOCITY)
        return this.moveTargetPos;
    }
    return base.GetPredictivePosition();
  }

  public virtual void SetAppearPos(Vector3 pos)
  {
    this.isSetAppearPos = true;
    this.appearPos = pos;
  }

  public virtual void SetAppearRandomPosFixDistance(
    Vector3 center_pos,
    float distance,
    int try_count)
  {
    int capacity = try_count;
    List<int> intList = new List<int>(capacity);
    for (int index = 0; index < capacity; ++index)
      intList.Add(index);
    float num1 = 360f / (float) capacity;
    float num2 = num1 * Random.value;
    Vector3 pos = Vector3.zero;
    for (int index1 = 0; index1 < capacity; ++index1)
    {
      int index2 = (int) ((double) intList.Count * (double) Random.value);
      int num3 = intList[index2];
      intList.RemoveAt(index2);
      float num4 = num2 + num1 * (float) num3;
      if ((double) num4 >= 360.0)
        num4 -= 360f;
      Vector3 check_pos = Vector3.op_Addition(center_pos, Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.Euler(0.0f, num4, 0.0f), Vector3.forward), distance));
      if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(check_pos))
      {
        pos = check_pos;
        break;
      }
    }
    this._position = pos;
    this._rotation = Quaternion.AngleAxis(Random.value * 360f, Vector3.up);
    this.SetAppearPos(pos);
  }

  public override AttackHitChecker ReferenceAttackHitChecker()
  {
    this.referenceCheckerFlag = true;
    return this.attackHitChecker;
  }

  public void AttackHitCheckerClearAll()
  {
    if (this.attackHitChecker == null)
      return;
    this.attackHitChecker.ClearAll();
  }

  public void AttackHitCheckerClearInfo(AnimEventData.EventData evData)
  {
    if (this.attackHitChecker == null || evData.stringArgs.Length == 0)
      return;
    this.attackHitChecker.ClearHitInfo(evData.stringArgs[0]);
  }

  private void EventChangeShaderParam(AnimEventData.EventData evData)
  {
    if (evData == null)
      return;
    int length = evData.stringArgs.Length;
    if (length < 2)
      return;
    string stringArg = evData.stringArgs[0];
    if (string.IsNullOrEmpty(stringArg))
      return;
    Transform transform = Utility.Find(this._transform, stringArg);
    if (Object.op_Equality((Object) transform, (Object) null))
      return;
    Renderer component = ((Component) transform).GetComponent<Renderer>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    Material material = component.material;
    if (Object.op_Equality((Object) material, (Object) null))
      return;
    int result1 = 0;
    float result2 = 0.0f;
    Color white = Color.white;
    for (int index = 1; index < length; ++index)
    {
      string[] strArray = evData.stringArgs[index].Split(':');
      string str1 = strArray[0];
      string str2 = strArray[1];
      string s = strArray[2];
      if (material.HasProperty(str2))
      {
        switch (str1)
        {
          case "F":
            if (float.TryParse(s, out result2))
            {
              material.SetFloat(str2, result2);
              continue;
            }
            continue;
          case "I":
            if (int.TryParse(s, out result1))
            {
              material.SetInt(str2, result1);
              continue;
            }
            continue;
          case "C":
            if (ColorUtility.TryParseHtmlString("#" + s, ref white))
            {
              material.SetColor(str2, white);
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
  }

  protected void SetShader(string shaderName, string containsString)
  {
    if (string.IsNullOrEmpty(shaderName) || this.m_rendererList == null)
      return;
    Utility.MaterialForEach(this.m_rendererList, (Action<Material>) (material =>
    {
      if (!((Object) material.shader).name.Contains(containsString))
        return;
      Shader shader = ResourceUtility.FindShader(shaderName);
      if (!Object.op_Inequality((Object) shader, (Object) null))
        return;
      material.shader = shader;
    }));
  }

  protected void ChangeGhostShaderParam(float endParam, float duration)
  {
    if (this.m_rendererList == null || this.m_rendererList.Length == 0)
      return;
    string SHADER_PARAM_ALPHA = "_Alpha";
    string SHADER_PARAM_ALPHA_BLUR = "_Blend";
    Utility.MaterialForEach(this.m_rendererList, (Action<Material>) (material =>
    {
      if (material.HasProperty(SHADER_PARAM_ALPHA_BLUR))
        this.StartCoroutine(this.ChangeShaderParam(material, SHADER_PARAM_ALPHA_BLUR, endParam, duration));
      if (!material.HasProperty(SHADER_PARAM_ALPHA) || !((Object) material.shader).name.Contains("enemy_"))
        return;
      this.StartCoroutine(this.ChangeShaderParam(material, SHADER_PARAM_ALPHA, endParam, duration));
    }));
  }

  private IEnumerator ChangeShaderParam(
    Material mat,
    string propertyName,
    float endParam,
    float duration)
  {
    if (!Object.op_Equality((Object) mat, (Object) null))
    {
      float timer = duration;
      float inputParam = mat.GetFloat(propertyName);
      bool isPlus = (double) endParam >= (double) inputParam;
      bool isFinish = false;
      while (!isFinish)
      {
        timer -= duration * Time.deltaTime;
        if ((double) duration <= 0.0)
          inputParam = endParam;
        else if (isPlus)
          inputParam += endParam / duration * Time.deltaTime;
        else
          inputParam -= inputParam / duration * Time.deltaTime;
        if (isPlus && (double) inputParam >= (double) endParam || !isPlus && (double) inputParam <= (double) endParam)
          inputParam = endParam;
        mat.SetFloat(propertyName, inputParam);
        if ((double) timer <= 0.0)
          isFinish = true;
        yield return (object) null;
      }
    }
  }

  public bool IsValidShield() => (int) this.m_shieldHpMax > 0 && (int) this.m_shieldHp > 0;

  private void DbgTimeCount(bool start)
  {
  }

  protected virtual bool GetTargetPos(out Vector3 pos)
  {
    pos = Vector3.zero;
    return false;
  }

  public virtual bool IsCarrying() => false;

  public void OnCheckAndResizeColliderOsMapByWeapon(int weapontId) => this.OnResizeColliderOfMap();

  private void OnResizeColliderOfMap()
  {
    Vector3 colliderOfMapScale = MonoBehaviourSingleton<GoGameSettingsManager>.I.colliderOfMapScale;
    MonoBehaviourSingleton<SceneSettingsManager>.I.OnResizeGObjContainColliders(MonoBehaviourSingleton<GoGameSettingsManager>.I.colliderOfMapScale);
  }

  public void OnCheckAndResizeColliderOsMapByEnemy(int enemyId)
  {
  }

  public enum ACTION_ID
  {
    NONE,
    IDLE,
    MOVE,
    ROTATE,
    DAMAGE,
    DEAD,
    ATTACK,
    PARALYZE,
    FREEZE,
    HIDE,
    MOVE_POINT,
    MOVE_LOOKAT,
    POSE,
    MAX,
  }

  public enum MOTION_ID
  {
    NONE = 0,
    END = 1,
    IDLE = 2,
    WALK = 3,
    ROTATE_L = 4,
    ROTATE_R = 5,
    DAMAGE = 6,
    DEAD = 7,
    PARALYZE = 8,
    MOVE_SIDE_R = 9,
    MOVE_SIDE_L = 10, // 0x0000000A
    HIDE = 11, // 0x0000000B
    HIDE_END = 12, // 0x0000000C
    MOVE_POINT = 13, // 0x0000000D
    MOVE_LOOKAT = 14, // 0x0000000E
    ATTACK_ID_BEGIN = 15, // 0x0000000F
    ATTACK_ID_NUM = 100, // 0x00000064
    ATTACK_ID_END = 114, // 0x00000072
    MAX = 115, // 0x00000073
  }

  protected class MotionHashTable : StringKeyTableBase
  {
    public void Add(string key, int value) => this._Add(key, (object) value);

    public object Get(string key) => this._Get(key);
  }

  [Serializable]
  public class PeriodicSyncActionPositionInfo
  {
    public float applyTime;
    public Vector3 actionPosition = Vector3.zero;
    public Vector3 targetPointPos = Vector3.zero;
    public bool actionPositionFlag;
  }

  public enum MOVE_TYPE
  {
    NONE,
    VELOCITY,
    SYNC_VELOCITY,
    TO_POSITION,
    HOMING,
    SIDEWAYS,
  }

  public enum ROTATE_TYPE
  {
    NONE,
    TO_DIRECTION,
    MOTION_TO_TARGET,
    MOTION_TO_DIRECTION,
  }

  public enum VELOCITY_TYPE
  {
    NONE,
    ROOT_MOTION,
    EVENT_MOVE,
    ACT_MOVE,
  }

  public enum OBJECT_LIST_TYPE
  {
    DEFAULT,
    STATIC,
    ANIM_EVENT,
    CHANGE_WEAPON,
    NUM,
  }

  public enum REACTION_TYPE
  {
    NONE,
    DAMAGE,
    BLOW,
    STUNNED_BLOW,
    STUMBLE,
    FALL_BLOW,
    SHAKE,
    DOWN,
    DEAD,
    GUARD_DAMAGE,
    PARALYZE,
    ANGRY,
    FREEZE,
    COUNTER,
    ELECTRIC_SHOCK,
    INK_SPLASH,
    DIZZY,
    SHADOWSEALING,
    MAD_MODE,
    LIGHT_RING,
    BIND,
    STONE,
    DEAD_REVIVE,
    SOIL_SHOCK,
    CONCUSSION,
    CHARM_BLOW,
  }

  public class ReactionInfo
  {
    public Character.REACTION_TYPE reactionType;
    public Vector3 blowForce = Vector3.zero;
    public float loopTime;
    public int targetId;
    public int deadReviveCount;
  }

  [Serializable]
  public class DelayReactionInfo
  {
    public Character.REACTION_TYPE type;
    public int targetId;
    public float reactionLoopTime;
  }

  public enum STATE_MOVE_POINT
  {
    NONE,
    INIT,
    ROTATE,
    CHECK,
    FINISH,
  }

  protected enum STATE_MOVE_LOOKAT
  {
    NONE,
    INIT,
    MOVE,
    FINISH,
  }

  public class PlayMotionParam
  {
    public int MotionID;
    public string MotionLayerName = "Base Layer.";
    public float TransitionTime = -1f;
  }

  public class HealData
  {
    public int healHp;
    public HEAL_TYPE healType;
    public HEAL_EFFECT_TYPE effectType;
    public List<int> applyAbilityTypeList = new List<int>();

    public HealData(
      int healHp,
      HEAL_TYPE healType,
      HEAL_EFFECT_TYPE healEffectType,
      List<int> applyAbilityTypeList)
    {
      this.healHp = healHp;
      this.healType = healType;
      this.effectType = healEffectType;
      this.applyAbilityTypeList = applyAbilityTypeList;
    }

    public override string ToString()
    {
      return $"HealData( healHp: {this.healHp}, healType: {this.healType}, effectType: {this.effectType}, applyAbilityTypeList: {this.applyAbilityTypeList.ToJoinString<int>()}";
    }
  }
}
