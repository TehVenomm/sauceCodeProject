// Decompiled with JetBrains decompiler
// Type: AttackFloatingMine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AttackFloatingMine : MonoBehaviour
{
  public const string ANIM_STATE_END = "END";
  private BulletData.BulletMine m_mineData;
  private StageObject m_attacker;
  private AttackInfo m_atkInfo;
  private Transform m_cachedTransform;
  private GameObject m_effectObj;
  private string m_landHitEffectName = string.Empty;
  private float m_moveSpeed;
  private float m_slowDownRate;
  private float m_aliveTimer;
  private int m_effectDeleteAnimHash;
  private Animator m_effectDeleteAnimator;
  private AttackFloatingMine.Function m_func;
  private int m_state;
  private bool m_isDeleted;
  private MineAttackObject m_mineAttackObj;
  private float m_unbreakableTimer;

  public void Initialize(AttackFloatingMine.InitParamFloatingMine initParam)
  {
    if (initParam.atkInfo == null)
      return;
    this.m_atkInfo = initParam.atkInfo;
    if (this.m_atkInfo is AttackHitInfo atkInfo)
      atkInfo.enableIdentityCheck = false;
    BulletData bulletData = this.m_atkInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return;
    BulletData.BulletBase data = bulletData.data;
    if (data == null)
      return;
    BulletData.BulletMine dataMine = bulletData.dataMine;
    if (dataMine == null)
      return;
    this.m_attacker = initParam.attacker;
    this.m_landHitEffectName = data.landHiteffectName;
    this.m_aliveTimer = data.appearTime;
    this.m_moveSpeed = data.speed;
    this.m_slowDownRate = dataMine.slowDownRate;
    this.m_unbreakableTimer = dataMine.unbrakableTime;
    this.m_mineData = dataMine;
    this.m_isDeleted = false;
    this.m_cachedTransform = ((Component) this).transform;
    this.m_cachedTransform.parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
    Transform launchTrans = initParam.launchTrans;
    this.m_cachedTransform.position = Vector3.op_Addition(launchTrans.position, Quaternion.op_Multiply(launchTrans.rotation, initParam.offsetPos));
    this.m_cachedTransform.rotation = Quaternion.op_Multiply(launchTrans.rotation, initParam.offsetRot);
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
    float radius = data.radius;
    float height = 0.0f;
    Vector3 hitOffset = data.hitOffset;
    int mask = 0;
    if (dataMine.isIgnoreHitEnemyAttack)
      mask |= 40960 /*0xA000*/;
    if (dataMine.isIgnoreHitEnemyMove)
      mask |= 1024 /*0x0400*/;
    MineAttackObject mineAttackObject = new GameObject("MineAttackObject").AddComponent<MineAttackObject>();
    mineAttackObject.Initialize(this.m_attacker, this.m_cachedTransform, this.m_atkInfo, hitOffset, Vector3.zero, radius, height, 31 /*0x1F*/);
    mineAttackObject.SetIgnoreLayerMask(mask);
    this.m_mineAttackObj = mineAttackObject;
    this.RequestMain();
  }

  private void Update()
  {
    switch (this.m_func)
    {
      case AttackFloatingMine.Function.MAIN:
        this.FuncMain();
        break;
      case AttackFloatingMine.Function.DELETE:
        this.FuncDelete();
        break;
    }
  }

  private void OnDestroy()
  {
    if (Object.op_Equality((Object) this.m_effectObj, (Object) null))
      return;
    EffectManager.ReleaseEffect(this.m_effectObj);
    this.m_effectObj = (GameObject) null;
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
    this.m_attacker = (StageObject) null;
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private void SetState(int state) => this.m_state = state;

  private void ForwardState() => ++this.m_state;

  private void BackState()
  {
    if (this.m_state <= 0)
      return;
    --this.m_state;
  }

  private void RequestFunction(AttackFloatingMine.Function func)
  {
    this.m_func = func;
    this.SetState(1);
  }

  private void RequestMain() => this.RequestFunction(AttackFloatingMine.Function.MAIN);

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
      this.m_unbreakableTimer -= Time.deltaTime;
      if (this.m_mineAttackObj.isHit)
      {
        if ((double) this.m_unbreakableTimer <= 0.0)
        {
          bool isExplode = false;
          if (!this.IsAvoidExplode(this.m_mineAttackObj.hitLayer) && this.IsLayerExplode(this.m_mineAttackObj.hitLayer))
            isExplode = true;
          this.RequestDestroy(isExplode);
          return;
        }
        this.m_mineAttackObj.ResetHit();
      }
      switch (this.m_state)
      {
        case 1:
          Vector3 vector3 = Vector3.op_Addition(this.m_cachedTransform.position, Vector3.op_Multiply(this.m_cachedTransform.forward, this.m_moveSpeed * Time.deltaTime));
          if (this.m_mineData != null)
          {
            vector3.y = this.m_mineData.floatingHeight;
            if ((double) this.m_mineData.floatingRate > 0.0)
              vector3.y += Mathf.Sin(3.14159274f * Mathf.PingPong(Time.time, this.m_mineData.floatingRate));
          }
          this.m_cachedTransform.position = vector3;
          this.m_moveSpeed -= Time.deltaTime * this.m_slowDownRate;
          if ((double) this.m_moveSpeed > 0.0)
            break;
          this.m_moveSpeed = 0.0f;
          this.ForwardState();
          break;
        case 2:
          Vector3 position = this.m_cachedTransform.position;
          if (this.m_mineData != null)
          {
            position.y = this.m_mineData.floatingHeight;
            if ((double) this.m_mineData.floatingRate > 0.0)
              position.y += Mathf.Sin(3.14159274f * Mathf.PingPong(Time.time, this.m_mineData.floatingRate));
          }
          this.m_cachedTransform.position = position;
          break;
      }
    }
  }

  private void RequestDestroy(bool isExplode = true)
  {
    if (this.m_func == AttackFloatingMine.Function.DELETE || this.m_isDeleted)
      return;
    this.RequestFunction(AttackFloatingMine.Function.DELETE);
    if (isExplode)
    {
      this.CreateExplosion();
      if (Object.op_Inequality((Object) this.m_mineAttackObj, (Object) null))
        this.m_mineAttackObj.ResetHit();
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

  private void FuncDelete()
  {
    switch (this.m_state)
    {
      case 1:
        if (Object.op_Equality((Object) this.m_effectDeleteAnimator, (Object) null))
        {
          this.ForwardState();
          break;
        }
        AnimatorStateInfo animatorStateInfo = this.m_effectDeleteAnimator.GetCurrentAnimatorStateInfo(0);
        if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
          break;
        this.ForwardState();
        break;
      case 2:
        this.Destroy();
        this.ForwardState();
        break;
    }
  }

  private AnimEventShot CreateExplosion()
  {
    BulletData bulletData = this.m_atkInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return (AnimEventShot) null;
    BulletData.BulletMine dataMine = bulletData.dataMine;
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return (AnimEventShot) null;
    if (Object.op_Equality((Object) this.m_attacker, (Object) null))
      return (AnimEventShot) null;
    Quaternion rotation = this.m_cachedTransform.rotation;
    Vector3 position = this.m_cachedTransform.position;
    AnimEventShot externalBulletData = AnimEventShot.CreateByExternalBulletData(dataMine.explodeBullet, this.m_attacker, this.m_atkInfo, position, rotation);
    return Object.op_Equality((Object) externalBulletData, (Object) null) ? (AnimEventShot) null : externalBulletData;
  }

  private bool IsAvoidExplode(int layer)
  {
    return !Object.op_Equality((Object) this.m_mineAttackObj, (Object) null) && layer == 8 && !Object.op_Equality((Object) this.m_mineAttackObj.hitPlayer, (Object) null) && this.m_mineAttackObj.hitPlayer.isActSpecialAction && this.m_mineAttackObj.hitPlayer.attackMode == Player.ATTACK_MODE.SPEAR;
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

  public enum Function
  {
    NONE,
    MAIN,
    DELETE,
  }

  public class InitParamFloatingMine
  {
    public StageObject attacker;
    public AttackInfo atkInfo;
    public Transform launchTrans;
    public Vector3 offsetPos = Vector3.zero;
    public Quaternion offsetRot = Quaternion.identity;
  }
}
