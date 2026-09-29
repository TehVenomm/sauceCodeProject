// Decompiled with JetBrains decompiler
// Type: AttackTrackingTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class AttackTrackingTarget : MonoBehaviour
{
  private const string ANIM_STATE_DISAPPEAR = "END";
  private const float Z_OFFSET_FOR_NO_TARGET = 2f;
  private BulletData.BulletTracking m_trackingData;
  private StageObject m_attacker;
  private Player m_attackerPlayer;
  private Enemy m_attackerEnemy;
  private AttackInfo m_atkInfo;
  private string[] m_atkInfoNames;
  private Transform m_cachedTransform;
  private StageObject m_target;
  private GameObject m_effectObj;
  private Animator m_effectAnimator;
  private int m_effectDeleteAnimHash;
  private AttackTrackingTarget.Function m_func;
  private bool m_isDeleted;
  private bool m_isEmitting;
  private bool m_isShooting;
  private float m_aliveTimer;
  private float m_attackIntervalTimer;
  private float m_emitInterval;
  private int m_emissionNum;
  private float m_moveSpeed;
  private float m_moveThreshold;
  private bool m_isPerfectTrack;
  private bool m_isTargetDead;
  private Vector3 m_targetingPoint = Vector3.zero;
  private AtkAttribute[] m_exAtkList;
  private Player.ATTACK_MODE m_attackMode;
  private bool m_isTracking;

  public SkillInfo.SkillParam SkillParamForBullet { get; private set; }

  public AtkAttribute PlayerAttackAttribute { get; private set; }

  public bool IsReplaceSkill { get; private set; }

  public void Initialize(StageObject attacker, StageObject target, AttackInfo atkInfo)
  {
    if (Object.op_Equality((Object) attacker, (Object) null))
    {
      this.RequestDestroy();
    }
    else
    {
      this.m_atkInfo = atkInfo;
      this.m_attacker = attacker;
      this.m_target = target;
      if (this.m_attacker is Player)
      {
        Player attacker1 = this.m_attacker as Player;
        if (Object.op_Inequality((Object) attacker1, (Object) null))
        {
          this.m_attackerPlayer = attacker1;
          this.m_attackMode = attacker1.attackMode;
        }
      }
      if (this.m_attacker is Enemy)
      {
        Enemy attacker2 = this.m_attacker as Enemy;
        if (Object.op_Inequality((Object) attacker2, (Object) null))
          this.m_attackerEnemy = attacker2;
      }
      BulletData bulletData = (BulletData) null;
      if (atkInfo != null)
      {
        bulletData = atkInfo.bulletData;
      }
      else
      {
        Player player = attacker as Player;
        if (Object.op_Inequality((Object) player, (Object) null))
        {
          AtkAttribute atk = new AtkAttribute();
          player.GetAtk((AttackHitInfo) null, ref atk, (SkillInfo.SkillParam) null);
          this.PlayerAttackAttribute = atk;
          SkillInfo.SkillParam actSkillParam = player.skillInfo.actSkillParam;
          if (actSkillParam != null)
          {
            bulletData = actSkillParam.bullet;
            this.m_atkInfoNames = actSkillParam.tableData.attackInfoNames;
            this.SkillParamForBullet = actSkillParam;
          }
          if (Object.op_Inequality((Object) player.targetingPoint, (Object) null))
            this.m_targetingPoint = player.targetingPoint.param.targetPos;
        }
      }
      if (Object.op_Equality((Object) bulletData, (Object) null))
      {
        this.RequestDestroy();
      }
      else
      {
        this.m_aliveTimer = bulletData.data.appearTime;
        BulletData.BulletTracking dataTracking = bulletData.dataTracking;
        if (dataTracking == null)
        {
          this.RequestDestroy();
        }
        else
        {
          this.m_trackingData = dataTracking;
          this.m_isDeleted = false;
          this.m_isEmitting = false;
          this.m_moveThreshold = dataTracking.moveThreshold;
          this.m_attackIntervalTimer = dataTracking.attackInterval;
          this.m_emitInterval = dataTracking.emitInterval;
          this.m_emissionNum = dataTracking.emissionNum;
          this.m_isPerfectTrack = dataTracking.isPerfectTrack;
          if (this.m_emissionNum > 0)
          {
            this.m_exAtkList = new AtkAttribute[this.m_emissionNum];
            this.m_moveSpeed = bulletData.data.speed;
            this.m_cachedTransform = ((Component) this).transform;
            this.m_cachedTransform.parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
            Vector3 zero = Vector3.zero;
            if (Object.op_Inequality((Object) this.m_attackerPlayer, (Object) null) && this.m_attackerPlayer.IsValidBuffBlind())
            {
              this.m_target = (StageObject) null;
              this.m_targetingPoint = Vector3.zero;
            }
            Vector3 vector3 = !Object.op_Inequality((Object) this.m_target, (Object) null) ? (!Vector3.op_Inequality(this.m_targetingPoint, Vector3.zero) ? Vector3.op_Addition(this.m_attacker._position, Vector3.op_Multiply(this.m_attacker._forward, 2f)) : this.m_targetingPoint) : this.m_target._position;
            vector3.y = 0.0f;
            this.m_cachedTransform.position = vector3;
            this.m_cachedTransform.rotation = Quaternion.identity;
            Transform effect = EffectManager.GetEffect(bulletData.data.effectName, ((Component) this).transform);
            if (Object.op_Inequality((Object) effect, (Object) null))
            {
              effect.localPosition = bulletData.data.dispOffset;
              effect.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
              effect.localScale = Vector3.one;
              this.m_effectObj = ((Component) effect).gameObject;
              this.m_effectAnimator = this.m_effectObj.GetComponent<Animator>();
            }
            this.m_isTracking = true;
            this.RequestMain();
          }
          else
          {
            Log.Error("BulletTracking.emissionNum is zero!!");
            this.RequestDestroy();
          }
        }
      }
    }
  }

  public void TrackOff() => this.m_isTracking = false;

  private void Update()
  {
    switch (this.m_func)
    {
      case AttackTrackingTarget.Function.MAIN:
        this.FuncMain();
        break;
      case AttackTrackingTarget.Function.DELETE:
        this.FuncDelete();
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

  private void RequestFunction(AttackTrackingTarget.Function func) => this.m_func = func;

  private void RequestMain() => this.RequestFunction(AttackTrackingTarget.Function.MAIN);

  private void FuncMain()
  {
    if (this.m_isDeleted)
      return;
    this.m_aliveTimer -= Time.deltaTime;
    if ((double) this.m_aliveTimer <= 0.0)
    {
      this.RequestDestroy();
    }
    else
    {
      Vector3 vector3_1 = Vector3.zero;
      if (Object.op_Inequality((Object) this.m_target, (Object) null))
      {
        Character target = this.m_target as Character;
        if (Object.op_Inequality((Object) target, (Object) null) && target.isDead)
          this.m_isTargetDead = true;
        vector3_1 = ((Component) this.m_target).transform.position;
        if (this.m_isTargetDead)
          vector3_1 = this.m_cachedTransform.position;
      }
      else if (Vector3.op_Inequality(this.m_targetingPoint, Vector3.zero))
        vector3_1 = this.m_targetingPoint;
      vector3_1.y = 0.0f;
      Vector3 position = this.m_cachedTransform.position;
      Vector3 vector3_2 = Vector3.op_Subtraction(vector3_1, position);
      ((Vector3) ref vector3_2).Normalize();
      if (Vector3.op_Inequality(vector3_2, Vector3.zero))
        this.m_cachedTransform.rotation = Quaternion.LookRotation(vector3_2);
      if ((Object.op_Inequality((Object) this.m_target, (Object) null) || Vector3.op_Inequality(this.m_targetingPoint, Vector3.zero)) && this.m_isTracking)
      {
        if (this.m_isPerfectTrack)
        {
          this.m_cachedTransform.position = vector3_1;
        }
        else
        {
          Vector3 vector3_3 = Vector3.op_Addition(this.m_cachedTransform.position, Vector3.op_Multiply(this.m_cachedTransform.forward, this.m_moveSpeed * Time.deltaTime));
          if ((double) Vector3.Distance(vector3_1, vector3_3) >= (double) this.m_moveThreshold)
            this.m_cachedTransform.position = vector3_3;
        }
      }
      if (this.m_isEmitting)
      {
        if (this.m_isShooting)
          return;
        this.StartCoroutine(this.ShotBullets());
        this.m_isShooting = true;
      }
      else
      {
        this.m_attackIntervalTimer -= Time.deltaTime;
        if ((double) this.m_attackIntervalTimer > 0.0)
          return;
        this.m_attackIntervalTimer = this.m_trackingData.attackInterval;
        this.m_isEmitting = true;
      }
    }
  }

  private void RequestDestroy()
  {
    if (this.m_func == AttackTrackingTarget.Function.DELETE || this.m_isDeleted)
      return;
    this.RequestFunction(AttackTrackingTarget.Function.DELETE);
    if (Object.op_Equality((Object) this.m_effectAnimator, (Object) null))
    {
      this.Destroy();
    }
    else
    {
      this.m_effectDeleteAnimHash = Animator.StringToHash("END");
      if (this.m_effectAnimator.HasState(0, this.m_effectDeleteAnimHash))
      {
        this.m_effectAnimator.Play(this.m_effectDeleteAnimHash, 0, 0.0f);
        this.m_effectAnimator.Update(0.0f);
      }
      else
        this.Destroy();
    }
  }

  private void FuncDelete()
  {
    if (Object.op_Equality((Object) this.m_effectAnimator, (Object) null))
      this.Destroy();
    AnimatorStateInfo animatorStateInfo = this.m_effectAnimator.GetCurrentAnimatorStateInfo(0);
    if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
      return;
    this.Destroy();
  }

  private void Destroy()
  {
    if (this.m_isDeleted)
      return;
    this.m_isDeleted = true;
    if (Object.op_Inequality((Object) this.m_attacker, (Object) null))
    {
      if (Object.op_Inequality((Object) this.m_attackerPlayer, (Object) null))
        this.m_attackerPlayer.TrackingTargetBullet = (AttackTrackingTarget) null;
      this.m_attacker = (StageObject) null;
    }
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private IEnumerator ShotBullets()
  {
    for (int i = 0; i < this.m_emissionNum; ++i)
    {
      this.CreateBullet(i);
      yield return (object) new WaitForSeconds(this.m_emitInterval);
    }
    this.m_isEmitting = false;
    this.m_isShooting = false;
  }

  private AnimEventShot CreateBullet(int index)
  {
    if (this.m_trackingData == null)
      return (AnimEventShot) null;
    if (Object.op_Equality((Object) this.m_attacker, (Object) null))
      return (AnimEventShot) null;
    if (this.m_atkInfoNames == null || this.m_atkInfoNames.Length == 0)
      return (AnimEventShot) null;
    if (this.m_atkInfoNames.Length < index)
      return (AnimEventShot) null;
    AttackInfo attackInfo = this.m_attacker.FindAttackInfo(this.m_atkInfoNames[index]);
    if (attackInfo == null)
      return (AnimEventShot) null;
    Quaternion rotation = this.m_cachedTransform.rotation;
    Vector3 position = this.m_cachedTransform.position;
    if (Object.op_Inequality((Object) this.m_attackerPlayer, (Object) null))
      this.IsReplaceSkill = true;
    AtkAttribute atk = this.m_exAtkList[index];
    if (atk == null)
    {
      atk = new AtkAttribute();
      this.m_attacker.GetAtk(attackInfo as AttackHitInfo, ref atk);
      this.m_exAtkList[index] = atk;
    }
    AnimEventShot externalBulletData = AnimEventShot.CreateByExternalBulletData(this.m_trackingData.emissionBullet, this.m_attacker, attackInfo, position, rotation, atk, this.m_attackMode);
    if (Object.op_Equality((Object) externalBulletData, (Object) null))
    {
      Log.Error("Failed to create AnimEventShot for tracking bullet!");
      return (AnimEventShot) null;
    }
    if (Object.op_Inequality((Object) this.m_attackerPlayer, (Object) null))
      this.IsReplaceSkill = false;
    return externalBulletData;
  }

  public enum Function
  {
    NONE,
    MAIN,
    DELETE,
  }
}
