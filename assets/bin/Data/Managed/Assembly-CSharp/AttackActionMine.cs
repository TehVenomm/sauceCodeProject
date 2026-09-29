// Decompiled with JetBrains decompiler
// Type: AttackActionMine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AttackActionMine : MonoBehaviour, IAttackCollider
{
  public const string ANIM_STATE_END = "END";
  private float m_timeCount;
  private BulletData.BulletActionMine m_mineData;
  private CapsuleCollider m_capsule;
  private StageObject m_attacker;
  private AttackInfo m_atkInfo;
  private AttackHitChecker m_attackHitChecker;
  private AttackColliderProcessor m_colliderProcessor;
  private Transform m_cachedTransform;
  private GameObject m_effectObj;
  private string m_landHitEffectName = string.Empty;
  private float m_aliveTime;
  private int m_effectDeleteAnimHash;
  private Animator m_effectDeleteAnimator;
  private AttackActionMine.Function m_func;
  private int m_state;
  private bool m_isDeleted;
  private float m_unbreakableTimer;
  private float m_actionCoolTime;
  private float m_actionCoolTimer;
  private Random rand;
  public int objId;
  public AttackActionMine.REACTION_TYPE reactionType;
  public int ignoreLayerMask;
  public GameObject hitObject;
  public int hitLayer;
  public int reflectCount;
  private StageObject fromObject;

  public void ResetRandomSeed(int seed) => this.rand = new Random(seed);

  public void Initialize(AttackActionMine.InitParamActionMine initParam)
  {
    this.m_timeCount = 0.0f;
    ((Component) this).gameObject.layer = 31 /*0x1F*/;
    this.m_atkInfo = initParam.atkInfo;
    if (this.m_atkInfo is AttackHitInfo atkInfo)
      atkInfo.enableIdentityCheck = false;
    BulletData bulletData = this.m_atkInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return;
    BulletData.BulletBase data = bulletData.data;
    if (data == null)
      return;
    BulletData.BulletActionMine dataActionMine = bulletData.dataActionMine;
    if (dataActionMine == null)
      return;
    this.m_actionCoolTime = dataActionMine.actionCoolTime;
    this.m_attacker = initParam.attacker;
    this.m_landHitEffectName = data.landHiteffectName;
    this.m_aliveTime = data.appearTime;
    this.m_unbreakableTimer = dataActionMine.unbrakableTime;
    this.m_mineData = dataActionMine;
    this.m_isDeleted = false;
    this.m_cachedTransform = ((Component) this).transform;
    this.m_cachedTransform.parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
    this.m_cachedTransform.position = initParam.position;
    this.m_cachedTransform.rotation = initParam.rotation;
    this.m_cachedTransform.localScale = data.timeStartScale;
    Transform effect = EffectManager.GetEffect(data.effectName, ((Component) this).transform);
    if (Object.op_Inequality((Object) effect, (Object) null))
    {
      effect.localPosition = data.dispOffset;
      effect.localRotation = Quaternion.Euler(data.dispRotation);
      effect.localScale = Vector3.one;
      this.m_effectObj = ((Component) effect).gameObject;
      this.m_effectDeleteAnimator = this.m_effectObj.GetComponent<Animator>();
    }
    this.m_capsule = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
    this.m_capsule.direction = 2;
    this.m_capsule.radius = data.radius;
    this.m_capsule.height = 0.0f;
    ((Collider) this.m_capsule).enabled = true;
    this.m_capsule.center = Vector3.zero;
    ((Collider) this.m_capsule).isTrigger = true;
    ((Component) this).gameObject.AddComponent<Rigidbody>().useGravity = false;
    int num1 = 0;
    if (dataActionMine.isIgnoreHitEnemyAttack)
      num1 |= 8192 /*0x2000*/;
    if (dataActionMine.isIgnoreHitEnemyMove)
    {
      int num2 = num1 | 1024 /*0x0400*/;
    }
    if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
    {
      this.m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(this.m_atkInfo, this.m_attacker, (Collider) this.m_capsule, (IAttackCollider) this);
      this.m_attackHitChecker = this.m_attacker.ReferenceAttackHitChecker();
    }
    this.rand = new Random(initParam.randomSeed);
    this.objId = initParam.id;
    if (!string.IsNullOrEmpty(this.m_mineData.appearEffectName))
    {
      Vector3 pos;
      // ISSUE: explicit constructor call
      ((Vector3) ref pos).\u002Ector(this.m_cachedTransform.position.x, 0.1f, this.m_cachedTransform.position.z);
      EffectManager.OneShot(this.m_mineData.appearEffectName, pos, this.m_cachedTransform.rotation, true);
    }
    this.RequestMain();
  }

  private void Update()
  {
    this.m_timeCount += Time.deltaTime;
    switch (this.m_func)
    {
      case AttackActionMine.Function.MAIN:
        this.FuncMain();
        break;
      case AttackActionMine.Function.DELETE:
        this.FuncDelete();
        break;
    }
  }

  private void RequestMain() => this.RequestFunction(AttackActionMine.Function.MAIN);

  private void FuncMain()
  {
    if (this.m_isDeleted)
      return;
    this.m_unbreakableTimer -= Time.deltaTime;
    this.m_actionCoolTimer -= Time.deltaTime;
    if ((double) this.m_timeCount >= (double) this.m_aliveTime)
    {
      this.RequestDestroy();
    }
    else
    {
      if (this.m_mineData.actionType != BulletData.BulletActionMine.ACTION_TYPE.REFLECT)
        return;
      switch (this.reactionType)
      {
        case AttackActionMine.REACTION_TYPE.EXPLODE:
          if (Object.op_Inequality((Object) this.hitObject, (Object) null) && (double) this.m_unbreakableTimer <= 0.0)
          {
            bool isExplode = false;
            if (!this.IsAvoidExplode(this.hitLayer) && this.IsLayerExplode(this.hitLayer))
              isExplode = true;
            if (Object.op_Inequality((Object) this.m_attacker, (Object) null))
            {
              Enemy attacker = this.m_attacker as Enemy;
              if (Object.op_Inequality((Object) attacker, (Object) null) && Object.op_Inequality((Object) attacker.enemySender, (Object) null))
              {
                attacker.ActDestroyActionMine(this.objId, isExplode);
                attacker.enemySender.OnDestroyActionMine(this.objId, isExplode);
                break;
              }
              break;
            }
            break;
          }
          break;
        case AttackActionMine.REACTION_TYPE.REFLECT:
          if ((double) this.m_actionCoolTimer <= 0.0)
          {
            this.RequestReflect();
            break;
          }
          break;
        case AttackActionMine.REACTION_TYPE.DIRECTION_CHANGE:
          if ((double) this.m_actionCoolTimer <= 0.0)
          {
            this.RequestReflect(false);
            break;
          }
          break;
      }
      this.ResetHit();
    }
  }

  public void RequestDestroy(bool isExplode = true)
  {
    if (this.m_func == AttackActionMine.Function.DELETE || this.m_isDeleted)
      return;
    this.RequestFunction(AttackActionMine.Function.DELETE);
    if (isExplode)
    {
      this.CreateExplosion();
      this.ResetHit();
    }
    if (Object.op_Equality((Object) this.m_effectDeleteAnimator, (Object) null))
    {
      this.Destroy();
    }
    else
    {
      string str = isExplode ? string.Empty : "END";
      if (string.IsNullOrEmpty(str))
      {
        this.Destroy();
      }
      else
      {
        this.m_effectDeleteAnimHash = Animator.StringToHash(str);
        if (!this.m_effectDeleteAnimator.HasState(0, this.m_effectDeleteAnimHash))
          return;
        this.m_effectDeleteAnimator.Play(this.m_effectDeleteAnimHash, 0, 0.0f);
      }
    }
  }

  private void Destroy()
  {
    if (this.m_isDeleted)
      return;
    this.m_isDeleted = true;
    if (!string.IsNullOrEmpty(this.m_landHitEffectName))
    {
      Transform effect = EffectManager.GetEffect(this.m_landHitEffectName);
      if (Object.op_Inequality((Object) effect, (Object) null))
      {
        effect.position = this.m_cachedTransform.position;
        effect.rotation = this.m_cachedTransform.rotation;
      }
    }
    Enemy attacker = this.m_attacker as Enemy;
    if (Object.op_Inequality((Object) attacker, (Object) null))
      attacker.OnDestroyActionMine(this);
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private AnimEventShot CreateExplosion()
  {
    BulletData bulletData = this.m_atkInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return (AnimEventShot) null;
    BulletData.BulletActionMine dataActionMine = bulletData.dataActionMine;
    if (dataActionMine == null)
      return (AnimEventShot) null;
    if (Object.op_Equality((Object) this.m_attacker, (Object) null))
      return (AnimEventShot) null;
    if (Object.op_Equality((Object) dataActionMine.explodeBullet, (Object) null))
      return (AnimEventShot) null;
    Quaternion rotation = this.m_cachedTransform.rotation;
    Vector3 position = this.m_cachedTransform.position;
    AnimEventShot externalBulletData = AnimEventShot.CreateByExternalBulletData(dataActionMine.explodeBullet, this.m_attacker, this.m_atkInfo, position, rotation);
    return Object.op_Equality((Object) externalBulletData, (Object) null) ? (AnimEventShot) null : externalBulletData;
  }

  private void RequestReflect(bool isCreate = true)
  {
    this.m_actionCoolTimer = this.m_actionCoolTime;
    if (this.m_func == AttackActionMine.Function.DELETE || this.m_isDeleted)
      return;
    if (isCreate)
    {
      Enemy attacker = this.m_attacker as Enemy;
      if (!Object.op_Inequality((Object) attacker, (Object) null) || !Object.op_Inequality((Object) this.fromObject, (Object) null) || !(this.fromObject is Self))
        return;
      int num = this.rand.Next(-1073741824 /*0xC0000000*/, 1073741823 /*0x3FFFFFFF*/);
      attacker.ActResetActionMineRandom(num);
      if (Object.op_Inequality((Object) attacker.enemySender, (Object) null))
        attacker.enemySender.OnCreateReflectBullet(this.objId, num);
      this.CreateReflectBullet();
    }
    else
      this.ChangeBulletDirection();
  }

  public AnimEventShot CreateReflectBullet()
  {
    if (Object.op_Equality((Object) this.m_atkInfo.bulletData, (Object) null))
      return (AnimEventShot) null;
    if (Object.op_Equality((Object) this.m_attacker, (Object) null))
      return (AnimEventShot) null;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.playerList == null)
      return (AnimEventShot) null;
    Quaternion rotation = this.m_cachedTransform.rotation;
    if (!this.IsGetTarget(this.m_cachedTransform.position, ref rotation))
      return (AnimEventShot) null;
    AnimEventShot externalBulletData = AnimEventShot.CreateByExternalBulletData(this.m_mineData.actionBullet, this.m_attacker, this.m_atkInfo, this.m_cachedTransform.position, rotation);
    if (Object.op_Equality((Object) externalBulletData, (Object) null))
      return (AnimEventShot) null;
    ((Component) externalBulletData).gameObject.AddComponent<AttackActionMine.ReflectBulletCondition>().actionMineID = this.objId;
    if (!string.IsNullOrEmpty(this.m_mineData.actionEffectName1))
      EffectManager.OneShot(this.m_mineData.actionEffectName1, Vector3.op_Addition(this.m_cachedTransform.position, this.m_mineData.actionEffectOffset), rotation);
    if (!string.IsNullOrEmpty(this.m_mineData.actionEffectName2))
      EffectManager.OneShot(this.m_mineData.actionEffectName2, Vector3.op_Addition(this.m_cachedTransform.position, this.m_mineData.actionEffectOffset), rotation);
    return externalBulletData;
  }

  private void ChangeBulletDirection()
  {
    Quaternion rotation = this.hitObject.transform.rotation;
    if (!this.IsGetTarget(this.hitObject.transform.position, ref rotation))
      return;
    BulletControllerBase component = this.hitObject.GetComponent<BulletControllerBase>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Initialize(this.m_mineData.actionBullet, (SkillInfo.SkillParam) null, component._transform.position, rotation);
    if (string.IsNullOrEmpty(this.m_mineData.actionEffectName2))
      return;
    EffectManager.OneShot(this.m_mineData.actionEffectName2, Vector3.op_Addition(component._transform.position, this.m_mineData.actionEffectOffset), rotation);
  }

  private bool IsGetTarget(Vector3 nowPos, ref Quaternion rot)
  {
    List<StageObject> stageObjectList = new List<StageObject>((IEnumerable<StageObject>) MonoBehaviourSingleton<StageObjectManager>.I.playerList);
    stageObjectList.RemoveAll((Predicate<StageObject>) (obj =>
    {
      Player player = obj as Player;
      return Object.op_Inequality((Object) player, (Object) null) && player.hp <= 0;
    }));
    stageObjectList.Sort((Comparison<StageObject>) ((a, b) => a.id - b.id));
    if (stageObjectList.Count < 1)
      return false;
    List<AttackActionMine> attackActionMineList = new List<AttackActionMine>((IEnumerable<AttackActionMine>) (this.m_attacker as Enemy).GetActionMineList());
    attackActionMineList.Remove(this);
    attackActionMineList.RemoveAll((Predicate<AttackActionMine>) (x => Object.op_Equality((Object) x, (Object) null)));
    if (attackActionMineList == null || attackActionMineList.Count < 1)
    {
      this.GetTargetPlayer(stageObjectList.ToArray(), nowPos, ref rot);
      return true;
    }
    if (this.rand.NextDouble() < (double) this.m_mineData.targettingPlayerRate)
      this.GetTargetPlayer(stageObjectList.ToArray(), nowPos, ref rot);
    else
      this.GetTargetMine(attackActionMineList.ToArray(), nowPos, ref rot);
    return true;
  }

  private void GetTargetPlayer(StageObject[] players, Vector3 nowPos, ref Quaternion rot)
  {
    int index = this.rand.Next(0, players.Length);
    Vector3 vector3 = Vector3.op_Subtraction(players[index]._position, nowPos);
    rot = Quaternion.LookRotation(vector3);
  }

  private void GetTargetMine(AttackActionMine[] mines, Vector3 nowPos, ref Quaternion rot)
  {
    int index = this.rand.Next(0, mines.Length);
    AttackActionMine mine = mines[index];
    rot = Quaternion.LookRotation(Vector3.op_Subtraction(mine.m_cachedTransform.position, nowPos));
  }

  private void FuncDelete()
  {
    switch (this.m_state)
    {
      case 1:
        if (Object.op_Equality((Object) this.m_effectDeleteAnimator, (Object) null))
        {
          this.SetState(3);
          break;
        }
        this.ForwardState();
        break;
      case 2:
        AnimatorStateInfo animatorStateInfo = this.m_effectDeleteAnimator.GetCurrentAnimatorStateInfo(0);
        if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
          break;
        this.ForwardState();
        break;
      case 3:
        this.Destroy();
        this.ForwardState();
        break;
    }
  }

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.m_effectObj, (Object) null))
      return;
    EffectManager.ReleaseEffect(this.m_effectObj);
    this.m_effectObj = (GameObject) null;
  }

  private void RequestFunction(AttackActionMine.Function func)
  {
    this.m_func = func;
    this.SetState(1);
  }

  private bool IsAvoidExplode(int layer)
  {
    if (layer == 8)
    {
      Player component = this.hitObject.GetComponent<Player>();
      if (!Object.op_Equality((Object) component, (Object) null) && component.isActSpecialAction && component.attackMode == Player.ATTACK_MODE.SPEAR)
        return true;
    }
    return false;
  }

  private bool IsLayerExplode(int layer)
  {
    switch (layer)
    {
      case 8:
      case 9:
      case 13:
      case 15:
      case 17:
      case 18:
      case 21:
        return true;
      default:
        return false;
    }
  }

  private void SetState(int state) => this.m_state = state;

  private void ForwardState() => ++this.m_state;

  private void BackState()
  {
    if (this.m_state <= 0)
      return;
    --this.m_state;
  }

  public string AttackInfoName => this.m_atkInfo.name;

  public virtual float GetTime() => this.m_timeCount;

  public virtual bool IsEnable() => true;

  public virtual void SortHitStackList(
    List<AttackHitColliderProcessor.HitResult> stack_list)
  {
  }

  public virtual Vector3 GetCrossCheckPoint(Collider from_collider)
  {
    Bounds bounds = from_collider.bounds;
    Vector3 crossCheckPoint = ((Bounds) ref bounds).center;
    Character attacker = this.m_attacker as Character;
    if (Object.op_Inequality((Object) attacker, (Object) null) && Object.op_Inequality((Object) attacker.rootNode, (Object) null))
      crossCheckPoint = attacker.rootNode.position;
    return crossCheckPoint;
  }

  public bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
  {
    return (this.m_attackHitChecker == null || this.m_attackHitChecker.CheckHitAttack(info, to_collider, to_object)) && (info.attackType != AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK || !(to_object is Enemy) || (double) (to_object as Enemy).healDamageRate > 0.0);
  }

  public void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
  {
    if (this.m_attackHitChecker == null)
      return;
    this.m_attackHitChecker.OnHitAttack(info, hit_param);
  }

  public AttackInfo GetAttackInfo() => this.m_colliderProcessor.attackInfo;

  public StageObject GetFromObject() => this.m_colliderProcessor.fromObject;

  public void SetIgnoreLayerMask(int mask) => this.ignoreLayerMask = mask;

  public void ResetHit()
  {
    this.hitObject = (GameObject) null;
    this.hitLayer = 0;
    this.reactionType = AttackActionMine.REACTION_TYPE.NONE;
    this.fromObject = (StageObject) null;
    ((Collider) this.m_capsule).enabled = true;
  }

  private void OnTriggerEnter(Collider collider)
  {
    this.hitObject = ((Component) collider).gameObject;
    this.hitLayer = this.hitObject.layer;
    if ((1 << this.hitLayer & this.ignoreLayerMask) > 0 || this.hitLayer == 8 && Object.op_Inequality((Object) this.hitObject.GetComponent<DangerRader>(), (Object) null) || Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<HealAttackObject>(), (Object) null))
      return;
    IAttackCollider component1 = this.hitObject.GetComponent<IAttackCollider>();
    if (component1 != null)
    {
      AttackInfo attackInfo = component1.GetAttackInfo();
      this.fromObject = component1.GetFromObject();
      if (attackInfo != null)
      {
        if (attackInfo.isSkillReference)
        {
          this.reactionType = AttackActionMine.REACTION_TYPE.REFLECT;
          collider.enabled = false;
          BulletObject component2 = ((Component) collider).GetComponent<BulletObject>();
          if (!Object.op_Inequality((Object) component2, (Object) null))
            return;
          component2.OnDestroy();
          return;
        }
        AttackActionMine.ReflectBulletCondition component3 = ((Component) collider).GetComponent<AttackActionMine.ReflectBulletCondition>();
        if (Object.op_Inequality((Object) component3, (Object) null))
        {
          if (component3.actionMineID == this.objId)
            return;
          this.reactionType = AttackActionMine.REACTION_TYPE.DIRECTION_CHANGE;
          component3.actionMineID = this.objId;
          ++component3.reflectCount;
          ++this.reflectCount;
          return;
        }
      }
    }
    this.reactionType = AttackActionMine.REACTION_TYPE.EXPLODE;
    ((Collider) this.m_capsule).enabled = false;
  }

  public enum Function
  {
    NONE,
    MAIN,
    DELETE,
  }

  public class InitParamActionMine
  {
    public StageObject attacker;
    public AttackInfo atkInfo;
    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;
    public int randomSeed;
    public int id;
  }

  public enum REACTION_TYPE
  {
    NONE,
    EXPLODE,
    REFLECT,
    DIRECTION_CHANGE,
  }

  public class ReflectBulletCondition : MonoBehaviour
  {
    public int actionMineID = -1;
    public int reflectCount;
  }
}
