// Decompiled with JetBrains decompiler
// Type: BulletObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BulletObject : 
  MonoBehaviour,
  IAttackCollider,
  StageObjectManager.IDetachedNotify,
  IBulletObservable
{
  public AtkAttribute masterAtk = new AtkAttribute();
  public SkillInfo.SkillParam masterSkill;
  public bool isShotArrow;
  public bool isAimMode;
  public bool isBossPierceArrow;
  private StageObject m_targetObject;
  private const float IS_LAND_HIT_MARGIN = 1f;
  protected AttackColliderProcessor colliderProcessor;
  protected BulletControllerBase controller;
  protected bool isColliderCreate;
  public AtkAttribute m_exAtk;
  public Player.ATTACK_MODE m_attackMode;
  private int m_endBulletSkillIndex = -1;
  protected CapsuleCollider capsuleCollider;
  protected bool isDestroyed;
  protected bool isLandHitDelete;
  protected Vector3 landHitPosition = Vector3.zero;
  protected Quaternion landHitRotation = Quaternion.identity;
  public Transform bulletEffect;
  protected AttackHitChecker attackHitChecker;
  protected bool m_isDisablePlayEndAnim;
  private int observedID;
  private List<IBulletObserver> bulletObserverList = new List<IBulletObserver>();

  public Transform _transform { get; protected set; }

  public Rigidbody _rigidbody { get; protected set; }

  public Collider _collider { get; protected set; }

  public StageObject stageObject { get; protected set; }

  public float timeCount { get; protected set; }

  public Vector3 endVec { get; private set; }

  public Vector3 prevPosition { get; protected set; }

  public void SetEndBulletSkillIndex(int skillIndex) => this.m_endBulletSkillIndex = skillIndex;

  public bool HasEndBulletSkillIndex => this.m_endBulletSkillIndex > -1;

  public BulletData bulletData { get; private set; }

  protected BulletData.BULLET_TYPE type { get; private set; }

  protected Vector3 dispOffset { get; private set; }

  protected Vector3 dispRotation { get; private set; }

  protected Vector3 offset { get; private set; }

  protected Vector3 baseScale { get; private set; }

  protected float appearTime { get; private set; }

  protected Vector3 timeStartScale { get; private set; }

  protected Vector3 timeEndScale { get; private set; }

  protected BulletData endBullet { get; private set; }

  protected bool isCharacterHitDelete { get; private set; }

  protected bool isObjectHitDelete { get; private set; }

  protected bool isLandHit { get; private set; }

  protected string landHitEfect { get; private set; }

  protected bool isBulletTakeoverTarget { get; private set; }

  public float radius { get; private set; }

  public float capsuleHeight { get; private set; }

  public Vector3 boxSize { get; private set; }

  public Vector3 startColliderPos { get; private set; }

  public void SetDisablePlayEndAnim() => this.m_isDisablePlayEndAnim = true;

  public BulletObject()
  {
    this.prevPosition = Vector3.zero;
    this.dispOffset = Vector3.zero;
    this.dispRotation = Vector3.zero;
    this.offset = Vector3.zero;
    this.baseScale = Vector3.one;
    this.appearTime = 0.0f;
    this.radius = 0.0f;
    this.timeStartScale = Vector3.one;
    this.timeEndScale = Vector3.one;
    this.isCharacterHitDelete = true;
    this.isObjectHitDelete = true;
    this.isLandHit = false;
    this.isBulletTakeoverTarget = false;
    this.endVec = Vector3.zero;
    this.capsuleHeight = 0.0f;
    this.boxSize = Vector3.zero;
  }

  protected virtual void Awake()
  {
    this._transform = ((Component) this).transform;
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    this._collider = ((Component) this).GetComponent<Collider>();
    if (Object.op_Equality((Object) this._rigidbody, (Object) null))
      this._rigidbody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    if (Object.op_Equality((Object) this._collider, (Object) null))
      this._collider = ((Component) this).gameObject.GetComponentInChildren<Collider>();
    if (Object.op_Inequality((Object) this._collider, (Object) null))
    {
      this._collider.isTrigger = true;
      this.capsuleCollider = this._collider as CapsuleCollider;
    }
    ((Component) this).gameObject.SetActive(false);
    this._rigidbody.useGravity = false;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.AddNotifyInterface((StageObjectManager.IDetachedNotify) this);
  }

  protected virtual void Start()
  {
  }

  protected virtual void Update()
  {
    if (!((Component) this).gameObject.activeSelf)
      return;
    this.timeCount += Time.deltaTime;
    float num1 = this.timeCount / this.appearTime;
    if ((double) this.appearTime >= 0.0 && (double) this.appearTime < (double) this.timeCount)
    {
      this.OnDestroy();
    }
    else
    {
      if (Vector3.op_Inequality(this.timeStartScale, this.timeEndScale))
        this.SetScale(Vector3.Lerp(this.timeStartScale, this.timeEndScale, num1));
      if (this.isLandHit)
      {
        Vector3 position = this._transform.position;
        float height = StageManager.GetHeight(position);
        if (!this.isShotArrow)
          height += this.radius;
        if ((double) height - 1.0 >= (double) position.y)
        {
          Vector3 vector3 = Vector3.op_Subtraction(position, this.prevPosition);
          float num2 = 0.0f;
          if ((double) vector3.y != 0.0)
            num2 = (height - this.prevPosition.y) / vector3.y;
          if (Object.op_Inequality((Object) this.controller, (Object) null))
            this.controller.OnLandHit();
          this.isLandHitDelete = true;
          this.landHitPosition = Vector3.op_Addition(Vector3.op_Multiply(vector3, num2), this.prevPosition);
          this.landHitRotation = Quaternion.identity;
          position.y = 0.0f;
          this.OnDestroy();
          return;
        }
      }
    }
    if (!this.isLandHitDelete || this.isDestroyed)
      return;
    this.OnDestroy();
  }

  protected virtual void FixedUpdate()
  {
    this.endVec = Vector3.zero;
    if ((double) this.capsuleHeight <= 0.0)
    {
      float num = Vector3.Distance(this.prevPosition, this._transform.position) / this._transform.localScale.x;
      if ((double) num > 0.0 && Object.op_Inequality((Object) this.capsuleCollider, (Object) null))
      {
        Vector3 offset = this.offset;
        offset.z -= num * 0.5f;
        this.capsuleCollider.center = offset;
        this.capsuleCollider.height = num + this.capsuleCollider.radius * 2f;
        this.endVec = Quaternion.op_Multiply(this._transform.rotation, Vector3.op_Multiply(Vector3.back, num * 0.5f + this.capsuleCollider.radius));
      }
    }
    this.prevPosition = this._transform.position;
  }

  protected virtual bool IsLoopEnd() => false;

  private void OnTriggerEnter(Collider collider)
  {
    if (this.isObjectHitDelete && (((Component) collider).gameObject.layer == 9 || ((Component) collider).gameObject.layer == 21))
    {
      this.isLandHitDelete = true;
      this.landHitPosition = Utility.ClosestPointOnCollider(collider, this.prevPosition);
      this.landHitRotation = Quaternion.op_Multiply(this._transform.rotation, Quaternion.Euler(new Vector3(-90f, 0.0f, 0.0f)));
      this._rigidbody.Sleep();
    }
    else
    {
      if (Object.op_Inequality((Object) this.controller, (Object) null))
      {
        if (!this.controller.IsHit(collider))
          return;
        this.controller.OnHit(collider);
        if (this.controller.IsBreak(collider))
        {
          this.NotifyBroken(true);
          this.endBullet = (BulletData) null;
          this.OnDestroy();
          return;
        }
      }
      if (this.colliderProcessor == null)
        return;
      this.colliderProcessor.OnTriggerEnter(collider);
    }
  }

  private void OnTriggerStay(Collider collider)
  {
    if (Object.op_Inequality((Object) this.controller, (Object) null))
    {
      if (!this.controller.IsHit(collider))
        return;
      this.controller.OnHitStay(collider);
    }
    if (this.colliderProcessor == null)
      return;
    this.colliderProcessor.OnTriggerStay(collider);
  }

  public void OnTriggerExit(Collider collider)
  {
    if (this.colliderProcessor == null)
      return;
    this.colliderProcessor.OnTriggerExit(collider);
  }

  public virtual void OnDetachedObject(StageObject stage_object)
  {
    if (!Object.op_Equality((Object) this.stageObject, (Object) stage_object))
      return;
    this.stageObject = (StageObject) null;
  }

  public virtual float GetTime() => this.timeCount;

  public virtual bool IsEnable() => !this.isDestroyed;

  public virtual void SortHitStackList(
    List<AttackHitColliderProcessor.HitResult> stack_list)
  {
    stack_list.Sort((Comparison<AttackHitColliderProcessor.HitResult>) ((a, b) =>
    {
      Vector3 vector3 = Vector3.op_Subtraction(a.target._position, this.prevPosition);
      double sqrMagnitude1 = (double) ((Vector3) ref vector3).sqrMagnitude;
      vector3 = Vector3.op_Subtraction(b.target._position, this.prevPosition);
      double sqrMagnitude2 = (double) ((Vector3) ref vector3).sqrMagnitude;
      return sqrMagnitude1 - sqrMagnitude2 < 0.0 ? -1 : 1;
    }));
  }

  public virtual Vector3 GetCrossCheckPoint(Collider from_collider)
  {
    Bounds bounds = from_collider.bounds;
    Vector3 crossCheckPoint = ((Bounds) ref bounds).center;
    if (Vector3.op_Inequality(this.endVec, Vector3.zero))
      crossCheckPoint = Vector3.op_Addition(crossCheckPoint, Vector3.op_Multiply(this.endVec, 2f));
    return crossCheckPoint;
  }

  public bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
  {
    return this.attackHitChecker == null || this.attackHitChecker.CheckHitAttack(info, to_collider, to_object);
  }

  public void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
  {
    if (this.attackHitChecker != null)
      this.attackHitChecker.OnHitAttack(info, hit_param);
    if (this.isCharacterHitDelete && hit_param.toObject is Character)
    {
      this.isLandHitDelete = false;
      this.OnDestroy();
    }
    else
    {
      if (!this.isCharacterHitDelete && this.isShotArrow || hit_param.fromObject is Player && hit_param.toObject is BarrierBulletObject || !this.isObjectHitDelete)
        return;
      this.isLandHitDelete = true;
      this.landHitPosition = Utility.ClosestPointOnCollider(hit_param.toCollider, this.prevPosition);
      this.landHitRotation = Quaternion.op_Multiply(this._transform.rotation, Quaternion.Euler(new Vector3(-90f, 0.0f, 0.0f)));
      this._rigidbody.Sleep();
    }
  }

  public AttackInfo GetAttackInfo() => this.colliderProcessor.attackInfo;

  public StageObject GetFromObject() => this.colliderProcessor.fromObject;

  public virtual void OnDestroy()
  {
    if (AppMain.isApplicationQuit || this.isDestroyed)
      return;
    this.isDestroyed = true;
    if (Object.op_Inequality((Object) this._rigidbody, (Object) null))
      this._rigidbody.Sleep();
    if (Object.op_Inequality((Object) this._collider, (Object) null))
      this._collider.enabled = false;
    this.capsuleCollider = (CapsuleCollider) null;
    if (Object.op_Inequality((Object) this.controller, (Object) null))
    {
      this.controller.DestroyBulletObject();
      ((Behaviour) this.controller).enabled = false;
    }
    if (this.colliderProcessor != null)
    {
      if (Object.op_Inequality((Object) this.endBullet, (Object) null))
        this.CreateEndBullet();
      this.colliderProcessor.OnDestroy();
      this.colliderProcessor = (AttackColliderProcessor) null;
    }
    if (this.isLandHitDelete && !string.IsNullOrEmpty(this.landHitEfect))
    {
      Transform effect = EffectManager.GetEffect(this.landHitEfect);
      if (Object.op_Inequality((Object) effect, (Object) null))
      {
        effect.localPosition = this.landHitPosition;
        effect.localRotation = this.landHitRotation;
      }
    }
    Transform transform = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
    if (Object.op_Inequality((Object) this.bulletEffect, (Object) null))
    {
      this.bulletEffect.parent = transform;
      EffectManager.ReleaseEffect(((Component) this.bulletEffect).gameObject, !this.m_isDisablePlayEndAnim);
    }
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      MonoBehaviourSingleton<StageObjectManager>.I.RemoveNotifyInterface((StageObjectManager.IDetachedNotify) this);
    this.NotifyDestroy();
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private void CreateEndBullet()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    if (string.IsNullOrEmpty(((Object) this.endBullet).name))
      Log.Error("endBullet.name is empty, so can't create EndBullet!!");
    else if (this.endBullet.type == BulletData.BULLET_TYPE.ICE_FLOOR)
    {
      Enemy stageObject = this.stageObject as Enemy;
      if (Object.op_Equality((Object) stageObject, (Object) null))
        return;
      List<Vector3> posList = new List<Vector3>(1);
      List<Quaternion> rotList = new List<Quaternion>();
      posList.Add(this._transform.position);
      stageObject.ActCreateIceFloor(this.endBullet, posList, rotList);
    }
    else if (this.endBullet.IsDecoy())
    {
      this.CreateEndBulletDecoy();
    }
    else
    {
      AttackInfo bulletAttackInfo = this.GetEndBulletAttackInfo();
      if (bulletAttackInfo == null)
      {
        Log.Error("AttackInfo is null!!");
      }
      else
      {
        if (Object.op_Equality((Object) this.stageObject, (Object) null))
          return;
        Transform transform = MonoBehaviourSingleton<StageObjectManager>.I._transform;
        if (Object.op_Equality((Object) transform, (Object) null))
          Log.Error("parentTrans is null, so can't create EndBullet!!");
        else if (this.type == BulletData.BULLET_TYPE.HIGH_EXPLOSIVE)
        {
          this.HighExplosiveSettings(bulletAttackInfo, transform);
        }
        else
        {
          BulletObject bulletObject = this.ShotEndBullet(bulletAttackInfo, transform);
          BulletData.BulletHoming dataHoming = this.endBullet.dataHoming;
          if (dataHoming != null && dataHoming.isTakeOverTarget && Object.op_Inequality((Object) this.m_targetObject, (Object) null))
            bulletObject.SetTarget(this.m_targetObject);
          if (this.type != BulletData.BULLET_TYPE.BREAKABLE || this.bulletData.dataBreakable == null)
            return;
          if (this.bulletData.dataBreakable.isTakeOverTarget && Object.op_Inequality((Object) this.m_targetObject, (Object) null))
            bulletObject.SetTarget(this.m_targetObject);
          if (!this.bulletData.dataBreakable.isTakeOverHitCount)
            return;
          BulletControllerBreakable controller1 = this.controller as BulletControllerBreakable;
          BulletControllerBreakable controller2 = bulletObject.controller as BulletControllerBreakable;
          if (!Object.op_Inequality((Object) controller1, (Object) null) || !Object.op_Inequality((Object) controller2, (Object) null))
            return;
          controller2.SetHitCount(controller1.GetHitCount());
        }
      }
    }
  }

  private void CreateEndBulletDecoy()
  {
    Self stageObject = this.stageObject as Self;
    if (Object.op_Equality((Object) stageObject, (Object) null))
      return;
    stageObject.EventShotDecoy(new AnimEventData.EventData()
    {
      stringArgs = new string[1]
      {
        ((Object) this.endBullet).name
      },
      floatArgs = new float[3]
      {
        this._transform.position.x,
        0.0f,
        this._transform.position.z
      },
      intArgs = new int[1]
      {
        this.masterSkill != null ? this.masterSkill.skillIndex : -1
      }
    });
  }

  protected void HighExplosiveSettings(AttackInfo _atkInfo, Transform _parentTrans)
  {
    if (Object.op_Equality((Object) this.bulletData, (Object) null))
      return;
    BulletData.BulletHighExplosive dataHighExplosive = this.bulletData.dataHighExplosive;
    if (dataHighExplosive == null || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
    if (playerList == null || playerList.Count <= 0)
      return;
    switch (dataHighExplosive.targetingType)
    {
      case BulletData.BulletHighExplosive.TARGETING_TYPE.ALL_PLAYERS:
        for (int index = 0; index < playerList.Count; ++index)
        {
          BulletObject bulletObject = this.ShotEndBullet(_atkInfo, _parentTrans);
          if (this.endBullet.dataHoming != null || this.endBullet.dataHealingHomingBullet != null || this.endBullet.dataResurrectionHomingBullet != null)
            bulletObject.SetTarget(playerList[index]);
        }
        break;
      case BulletData.BulletHighExplosive.TARGETING_TYPE.ALL_PLAYERS_EXCEPT_ME:
        int index1 = 0;
        for (int count = playerList.Count; index1 < count; ++index1)
        {
          if (!Object.op_Equality((Object) playerList[index1], (Object) this.stageObject))
          {
            BulletObject bulletObject = this.ShotEndBullet(_atkInfo, _parentTrans);
            if (this.endBullet.dataHoming != null || this.endBullet.dataHealingHomingBullet != null || this.endBullet.dataResurrectionHomingBullet != null)
              bulletObject.SetTarget(playerList[index1]);
          }
        }
        break;
    }
  }

  private List<StageObject> GetSortedListByPlayerDistance(
    List<StageObject> allPlayerList,
    StageObject me)
  {
    if (allPlayerList == null || allPlayerList.Count < 1 || Object.op_Equality((Object) me, (Object) null))
      return (List<StageObject>) null;
    List<StageObject> byPlayerDistance = new List<StageObject>((IEnumerable<StageObject>) allPlayerList);
    byPlayerDistance.Remove(me);
    Vector3 targetPos = ((Component) me).transform.position;
    byPlayerDistance.Sort((Comparison<StageObject>) ((a, b) =>
    {
      Vector3 vector3 = Vector3.op_Subtraction(((Component) a).transform.position, targetPos);
      double magnitude1 = (double) ((Vector3) ref vector3).magnitude;
      vector3 = Vector3.op_Subtraction(((Component) b).transform.position, targetPos);
      double magnitude2 = (double) ((Vector3) ref vector3).magnitude;
      return Mathf.RoundToInt((float) (magnitude1 - magnitude2));
    }));
    return byPlayerDistance;
  }

  private BulletObject ShotEndBullet(AttackInfo atkInfo, Transform parentTrans)
  {
    Transform gameObject = Utility.CreateGameObject(((Object) this.endBullet).name, parentTrans);
    if (Object.op_Equality((Object) gameObject, (Object) null))
    {
      Log.Error("Failed to create Bullet!! name:" + ((Object) this.endBullet).name);
      return (BulletObject) null;
    }
    BulletObject bulletObject = ((Component) gameObject).gameObject.AddComponent(((object) this).GetType()) as BulletObject;
    if (Object.op_Equality((Object) bulletObject, (Object) null))
    {
      Object.Destroy((Object) ((Component) gameObject).gameObject);
      return (BulletObject) null;
    }
    bulletObject.SetBaseScale(this.baseScale);
    if (this.masterSkill != null)
      bulletObject.SetEndBulletSkillIndex(this.masterSkill.skillIndex);
    bulletObject.Shot(this.stageObject, atkInfo, this.endBullet, this._transform.position, this._transform.rotation, reference_attack: false, exAtk: this.m_exAtk, attackMode: this.m_attackMode);
    bulletObject.attackHitChecker = this.attackHitChecker;
    if (this.isBulletTakeoverTarget)
      bulletObject.SetTarget(this.m_targetObject);
    return bulletObject;
  }

  private AttackInfo GetEndBulletAttackInfo()
  {
    AttackInfo attackInfo1 = this.colliderProcessor.attackInfo;
    if (attackInfo1 == null)
      return (AttackInfo) null;
    if (Object.op_Equality((Object) this.stageObject, (Object) null) || string.IsNullOrEmpty(attackInfo1.nextBulletInfoName))
      return attackInfo1;
    AttackInfo attackInfo2 = this.stageObject.FindAttackInfo(attackInfo1.nextBulletInfoName);
    if (attackInfo2 != null)
      return attackInfo2;
    Log.Error("Not found AttackInfo for EndBullet!! nextBulletInfoName:" + attackInfo1.nextBulletInfoName);
    return attackInfo1;
  }

  public void SetBaseScale(Vector3 _scale) => this.baseScale = _scale;

  public void SetRadius(float _radius)
  {
    this.radius = _radius;
    if (!this.isColliderCreate || !Object.op_Inequality((Object) this.capsuleCollider, (Object) null))
      return;
    if ((double) this.radius <= 0.0)
    {
      Object.Destroy((Object) this._collider);
      this._collider = (Collider) null;
    }
    else
    {
      this.capsuleCollider.radius = this.radius;
      this.capsuleCollider.height = this.radius * 2f;
    }
  }

  public void SetScale(Vector3 scale)
  {
    this._transform.localScale = Vector3.Scale(this.baseScale, scale);
  }

  public void SetHitOffset(Vector3 _offset)
  {
    this.offset = _offset;
    if (!Object.op_Inequality((Object) this.capsuleCollider, (Object) null))
      return;
    this.capsuleCollider.center = _offset;
  }

  public void SetCapsuleAxis(BulletData.AXIS axis)
  {
    if (!this.isColliderCreate || !Object.op_Inequality((Object) this.capsuleCollider, (Object) null))
      return;
    int num = 2;
    if (axis != BulletData.AXIS.NONE)
      num = (int) axis;
    this.capsuleCollider.direction = num;
  }

  public void SetCapsuleHeight(float height)
  {
    if (!this.isColliderCreate || (double) height <= 0.0 || !Object.op_Inequality((Object) this.capsuleCollider, (Object) null))
      return;
    this.capsuleCollider.height = height;
    this.capsuleHeight = height;
  }

  public virtual void Shot(
    StageObject master,
    AttackInfo atkInfo,
    BulletData bulletData,
    Vector3 pos,
    Quaternion rot,
    string exEffectName = null,
    bool reference_attack = true,
    AtkAttribute exAtk = null,
    Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE,
    DamageDistanceTable.DamageDistanceData damageDistanceData = null,
    SkillInfo.SkillParam exSkillParam = null)
  {
    Player player = master as Player;
    ((Component) this).gameObject.SetActive(true);
    this.stageObject = master;
    this.m_exAtk = exAtk;
    this.m_attackMode = attackMode;
    string effect_name = bulletData.data.GetEffectName(player);
    if (!string.IsNullOrEmpty(exEffectName))
      effect_name = exEffectName;
    if (!string.IsNullOrEmpty(effect_name))
    {
      this.bulletEffect = EffectManager.GetEffect(effect_name, this._transform);
      if (Object.op_Inequality((Object) this.bulletEffect, (Object) null))
      {
        this.bulletEffect.localPosition = bulletData.data.dispOffset;
        this.bulletEffect.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
        this.bulletEffect.localScale = Vector3.one;
      }
    }
    AttackHitInfo info = atkInfo as AttackHitInfo;
    if (exAtk != null)
      this.masterAtk = exAtk;
    else if (info != null)
    {
      if (Object.op_Inequality((Object) player, (Object) null) && this.HasEndBulletSkillIndex)
      {
        int skillIndex = player.skillInfo.skillIndex;
        player.skillInfo.skillIndex = this.m_endBulletSkillIndex;
        master.GetAtk(info, ref this.masterAtk);
        player.skillInfo.skillIndex = skillIndex;
      }
      else
        master.GetAtk(info, ref this.masterAtk);
    }
    this.masterSkill = (SkillInfo.SkillParam) null;
    if (Object.op_Inequality((Object) player, (Object) null))
    {
      if (exSkillParam != null)
      {
        this.masterSkill = exSkillParam;
      }
      else
      {
        this.masterSkill = player.skillInfo.actSkillParam;
        if (Object.op_Inequality((Object) player.TrackingTargetBullet, (Object) null) && player.TrackingTargetBullet.IsReplaceSkill && atkInfo.isSkillReference)
          this.masterSkill = player.TrackingTargetBullet.SkillParamForBullet;
        if (this.HasEndBulletSkillIndex)
          this.masterSkill = player.GetSkillParam(this.m_endBulletSkillIndex);
      }
    }
    if (bulletData.data.isEmitGround)
      pos.y = 0.0f;
    this.SetBulletData(bulletData, this.masterSkill, pos, rot);
    if (bulletData.type == BulletData.BULLET_TYPE.OBSTACLE)
      ((Component) this).gameObject.AddComponent<AttackObstacle>().Initialize(this as AnimEventShot, bulletData.dataObstacle.colliderStartTime);
    else if (bulletData.type == BulletData.BULLET_TYPE.BARRIER)
      ((Component) this).gameObject.AddComponent<BarrierBulletObject>().Initialize(this);
    else if (bulletData.type != BulletData.BULLET_TYPE.HEALING_HOMING && bulletData.type != BulletData.BULLET_TYPE.ENEMY_PRESENT && bulletData.type != BulletData.BULLET_TYPE.SPEAR_BARRIER && bulletData.type != BulletData.BULLET_TYPE.RESURRECTION_HOMING)
      Utility.SetLayerWithChildren(this._transform, master is Player ? 14 : 15);
    this.timeCount = 0.0f;
    if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
    {
      this.colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(atkInfo, this.stageObject, this._collider, (IAttackCollider) this, attackMode, damageDistanceData);
      if (reference_attack)
        this.attackHitChecker = this.stageObject.ReferenceAttackHitChecker();
      if (bulletData.type == BulletData.BULLET_TYPE.SNATCH || bulletData.type == BulletData.BULLET_TYPE.PAIR_SWORDS_LASER)
        this.colliderProcessor.ValidTriggerStay();
      if (bulletData.type == BulletData.BULLET_TYPE.CRASH_BIT || info != null && info.isValidTriggerStay)
      {
        this.colliderProcessor.ValidTriggerStay();
        this.colliderProcessor.ValidMultiHitInterval();
      }
    }
    Vector3 vector3 = Vector3.zero;
    if (this._collider is BoxCollider)
      vector3 = (this._collider as BoxCollider).center;
    else if (this._collider is SphereCollider)
      vector3 = (this._collider as SphereCollider).center;
    else if (this._collider is CapsuleCollider)
      vector3 = (this._collider as CapsuleCollider).center;
    this.startColliderPos = Vector3.op_Addition(this._transform.position, vector3);
    this.isDestroyed = false;
    this.prevPosition = pos;
    if (!Object.op_Inequality((Object) this.controller, (Object) null))
      return;
    this.controller.OnShot();
  }

  protected virtual void SetBulletData(
    BulletData bullet,
    SkillInfo.SkillParam _skillParam,
    Vector3 pos,
    Quaternion rot)
  {
    if (Object.op_Equality((Object) bullet, (Object) null))
      return;
    this.bulletData = bullet;
    this.type = bullet.type;
    this.appearTime = bullet.data.appearTime;
    this.dispOffset = this.bulletData.data.dispOffset;
    this.dispRotation = this.bulletData.data.dispRotation;
    this.SetRadius(bullet.data.radius);
    this.SetCapsuleHeight(bullet.data.capsuleHeight);
    this.SetCapsuleAxis(bullet.data.capsuleAxis);
    this.SetScale(bullet.data.timeStartScale);
    this.SetHitOffset(bullet.data.hitOffset);
    this.timeStartScale = bullet.data.timeStartScale;
    this.timeEndScale = bullet.data.timeEndScale;
    this.isCharacterHitDelete = bullet.data.isCharacterHitDelete;
    this.isObjectHitDelete = bullet.data.isObjectHitDelete;
    this.isLandHit = bullet.data.isLandHit;
    this.landHitEfect = bullet.data.landHiteffectName;
    this.endBullet = bullet.data.endBullet;
    this.isBulletTakeoverTarget = bullet.data.isBulletTakeoverTarget;
    switch (bullet.type)
    {
      case BulletData.BULLET_TYPE.FALL:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerFall>();
        break;
      case BulletData.BULLET_TYPE.HOMING:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerHoming>();
        break;
      case BulletData.BULLET_TYPE.CURVE:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerCurve>();
        break;
      case BulletData.BULLET_TYPE.BREAKABLE:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerBreakable>();
        break;
      case BulletData.BULLET_TYPE.OBSTACLE_CYLINDER:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerObstacleCylinder>();
        break;
      case BulletData.BULLET_TYPE.SNATCH:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerSnatch>();
        break;
      case BulletData.BULLET_TYPE.PAIR_SWORDS_SOUL:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerPairSwordsSoul>();
        break;
      case BulletData.BULLET_TYPE.PAIR_SWORDS_LASER:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerPairSwordsLaser>();
        break;
      case BulletData.BULLET_TYPE.HEALING_HOMING:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerHealingHoming>();
        break;
      case BulletData.BULLET_TYPE.ARROW_SOUL:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerArrowSoul>();
        break;
      case BulletData.BULLET_TYPE.ENEMY_PRESENT:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerEnemyPresent>();
        break;
      case BulletData.BULLET_TYPE.CRASH_BIT:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerCrashBit>();
        break;
      case BulletData.BULLET_TYPE.BARRIER:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerBarrier>();
        break;
      case BulletData.BULLET_TYPE.ROTATE_BIT:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerRotateBit>();
        break;
      case BulletData.BULLET_TYPE.SPEAR_BARRIER:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerSpearBarrier>();
        break;
      case BulletData.BULLET_TYPE.RESURRECTION_HOMING:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerResurrectionHoming>();
        break;
      case BulletData.BULLET_TYPE.SEARCH:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerSearch>();
        break;
      case BulletData.BULLET_TYPE.TURRET_BIT:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerTurretBit>();
        break;
      case BulletData.BULLET_TYPE.RANDOM_HOMING:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerRondomHoming>();
        break;
      case BulletData.BULLET_TYPE.ORACLE_SPEAR_SP:
        this.controller = (BulletControllerBase) ((Component) this).gameObject.AddComponent<BulletControllerOracleSpearSp>();
        break;
      default:
        this.controller = ((Component) this).gameObject.AddComponent<BulletControllerBase>();
        break;
    }
    if (Object.op_Inequality((Object) this.controller, (Object) null))
    {
      this.controller.Initialize(bullet, _skillParam, pos, rot);
      this.controller.RegisterBulletObject(this);
      this.controller.RegisterFromObject(this.stageObject);
      switch (bullet.type)
      {
        case BulletData.BULLET_TYPE.BREAKABLE:
        case BulletData.BULLET_TYPE.ENEMY_PRESENT:
        case BulletData.BULLET_TYPE.BARRIER:
        case BulletData.BULLET_TYPE.SEARCH:
        case BulletData.BULLET_TYPE.TURRET_BIT:
          this.RegisterObserver();
          break;
        case BulletData.BULLET_TYPE.PAIR_SWORDS_SOUL:
          if (this.controller is IObservable controller)
          {
            Player stageObject = this.stageObject as Player;
            if (Object.op_Inequality((Object) stageObject, (Object) null))
            {
              controller.RegisterObserver((IObserver) stageObject.pairSwordsCtrl);
              break;
            }
            break;
          }
          break;
      }
      this.controller.PostInitialize();
    }
    Character stageObject1 = this.stageObject as Character;
    if (!Object.op_Inequality((Object) stageObject1, (Object) null))
      return;
    this.SetTarget(stageObject1.actionTarget);
  }

  public void SetTarget(StageObject obj)
  {
    this.m_targetObject = obj;
    if (Object.op_Inequality((Object) this.stageObject, (Object) null))
    {
      Character stageObject = this.stageObject as Character;
      if (Object.op_Inequality((Object) stageObject, (Object) null) && stageObject.IsValidBuffBlind())
        this.m_targetObject = (StageObject) null;
    }
    this.controller.RegisterTargetObject(this.m_targetObject);
  }

  public void SetTarget(TargetPoint targetPoint)
  {
    BulletControllerArrowSoul controller = this.controller as BulletControllerArrowSoul;
    if (Object.op_Equality((Object) controller, (Object) null))
      return;
    controller.SetTarget(targetPoint);
  }

  public void SetPuppetTargetPos(Vector3 pos)
  {
    BulletControllerArrowSoul controller = this.controller as BulletControllerArrowSoul;
    if (Object.op_Equality((Object) controller, (Object) null))
      return;
    controller.SetPuppetTargetPos(pos);
  }

  public void EndBossPierceArrow()
  {
    this.isBossPierceArrow = false;
    this.isCharacterHitDelete = false;
  }

  public int GetObservedID() => this.observedID;

  public void SetObservedID(int id) => this.observedID = id;

  public void RegisterObserver()
  {
    if (this.bulletObserverList.Contains((IBulletObserver) this.stageObject))
      return;
    this.bulletObserverList.Add((IBulletObserver) this.stageObject);
    this.SetObservedID(this.stageObject.GetObservedID());
    this.stageObject.RegisterObservable((IBulletObservable) this);
  }

  public void NotifyBroken(bool isSendOnlyOriginal = true)
  {
    for (int index = 0; index < this.bulletObserverList.Count; ++index)
      this.bulletObserverList[index].OnBreak(this.observedID, isSendOnlyOriginal);
  }

  public void NotifyDestroy()
  {
    for (int index = 0; index < this.bulletObserverList.Count; ++index)
      this.bulletObserverList[index].OnBulletDestroy(this.observedID);
  }

  public void ForceBreak()
  {
    this.endBullet = (BulletData) null;
    this.OnDestroy();
  }

  public void SetSearchTarget(int targetID)
  {
    if (Object.op_Equality((Object) this.controller, (Object) null))
      return;
    BulletControllerSearch controller = this.controller as BulletControllerSearch;
    if (Object.op_Equality((Object) controller, (Object) null))
      return;
    controller.SetTargetId(targetID);
  }

  public void SetTurretBitTarget(int targetID, int regionID)
  {
    if (Object.op_Equality((Object) this.controller, (Object) null))
      return;
    BulletControllerTurretBit controller = this.controller as BulletControllerTurretBit;
    if (Object.op_Equality((Object) controller, (Object) null))
      return;
    controller.SetTargetId(targetID, regionID);
  }
}
