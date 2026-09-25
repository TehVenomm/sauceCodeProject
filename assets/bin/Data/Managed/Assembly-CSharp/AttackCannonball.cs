// Decompiled with JetBrains decompiler
// Type: AttackCannonball
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AttackCannonball : MonoBehaviour
{
  public const string EFFECT_NAME_HIT_BASIS = "ef_btl_magibullet_landing_01";
  public const string EFFECT_NAME_HIT_CRITICAL = "ef_btl_magibullet_landing_02";
  public const string EFFECT_NAME_HIT_NOSHIELD = "ef_btl_magibullet_landing_03";
  public const string EFFECT_NAME_SHOT = "ef_btl_magibullet_shot_01";
  public const string EFFECT_NAME_BREAK_SHIELD = "ef_btl_goldbird_aura_01_01";
  public const string EFFECT_NAME_GUIDE_TAP = "ef_btl_cannon_tap";
  private StageObject m_attacker;
  private AttackInfo m_atkInfo;
  private Transform m_cachedTransform;
  private GameObject m_effectObj;
  private CannonballAttackObject m_cannonballAttackObj;
  private Rigidbody cannonballRigidbody;
  private Transform cannonballTransform;
  private float gravityStartTime;
  private float gravityRate;
  private bool hasExistTime;
  private float existTimer;
  private float existTime;

  public void Initialize(AttackCannonball.InitParamCannonball initParam)
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
    BulletData.BulletCannonball dataCannonball = bulletData.dataCannonball;
    if (dataCannonball == null)
      return;
    this.m_attacker = initParam.attacker;
    this.m_cachedTransform = ((Component) this).transform;
    this.m_cachedTransform.parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
    Transform launchTrans = initParam.launchTrans;
    this.m_cachedTransform.position = Vector3.op_Addition(launchTrans.position, Quaternion.op_Multiply(launchTrans.rotation, initParam.offsetPos));
    this.m_cachedTransform.rotation = Quaternion.op_Multiply(launchTrans.rotation, initParam.offsetRot);
    this.m_cachedTransform.localScale = data.timeStartScale;
    this.existTime = data.appearTime;
    this.hasExistTime = (double) data.appearTime > 0.0;
    float radius = data.radius;
    float height = 0.0f;
    Vector3 hitOffset = data.hitOffset;
    int mask = 20736;
    GameObject gameObject = new GameObject("CannonballAttackObject");
    CannonballAttackObject cannonballAttackObject = gameObject.AddComponent<CannonballAttackObject>();
    cannonballAttackObject.Initialize(this.m_attacker, this.m_cachedTransform, this.m_atkInfo, hitOffset, Vector3.zero, radius, height, 14);
    cannonballAttackObject.SetIgnoreLayerMask(mask);
    cannonballAttackObject.SetOwner(this);
    this.m_cannonballAttackObj = cannonballAttackObject;
    this.gravityStartTime = dataCannonball.gravityStartTime;
    this.gravityRate = dataCannonball.gravityRate;
    this.cannonballRigidbody = gameObject.GetComponent<Rigidbody>();
    this.cannonballTransform = gameObject.transform;
    float speed = data.speed;
    this.cannonballRigidbody.velocity = Vector3.op_Multiply(Quaternion.op_Multiply(initParam.shotRotation, Vector3.forward), speed);
    Transform effect = EffectManager.GetEffect(data.effectName, ((Component) cannonballAttackObject).transform);
    if (!Object.op_Inequality((Object) effect, (Object) null))
      return;
    effect.localPosition = data.dispOffset;
    effect.localRotation = Quaternion.Euler(data.dispRotation);
    effect.localScale = Vector3.one;
    this.m_effectObj = ((Component) effect).gameObject;
  }

  private void FixedUpdate()
  {
    if ((double) this.gravityStartTime < 0.0 || (double) this.m_cannonballAttackObj.GetTime() < (double) this.gravityStartTime)
      return;
    this.cannonballRigidbody.AddForce(Vector3.op_Multiply(Physics.gravity, this.gravityRate), (ForceMode) 5);
    if (!Vector3.op_Inequality(this.cannonballRigidbody.velocity, Vector3.zero))
      return;
    this.cannonballTransform.forward = this.cannonballRigidbody.velocity;
  }

  private void Update()
  {
    if (this.hasExistTime)
    {
      this.existTimer += Time.deltaTime;
      if ((double) this.existTimer > (double) this.existTime)
        Object.Destroy((Object) ((Component) this).gameObject);
    }
    if ((double) this.cannonballTransform.position.y > -1.0)
      return;
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private void OnDestroy()
  {
    if (Object.op_Equality((Object) this.m_effectObj, (Object) null))
      return;
    EffectManager.ReleaseEffect(this.m_effectObj);
    this.m_effectObj = (GameObject) null;
  }

  public void OnHit() => Object.Destroy((Object) ((Component) this).gameObject);

  public class InitParamCannonball
  {
    public StageObject attacker;
    public AttackInfo atkInfo;
    public Transform launchTrans;
    public Vector3 offsetPos = Vector3.zero;
    public Quaternion offsetRot = Quaternion.identity;
    public Quaternion shotRotation = Quaternion.identity;
  }
}
