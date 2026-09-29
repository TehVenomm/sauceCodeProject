// Decompiled with JetBrains decompiler
// Type: AttackCannonBeam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AttackCannonBeam : MonoBehaviour
{
  private StageObject m_attacker;
  private AttackInfo m_atkInfo;
  private Transform m_cachedTransform;
  private bool m_hasExistTime;
  private float m_existTimer;
  private float m_existTime;
  private Transform m_effectTrans;
  private GameObject m_effectObj;
  private AttackCannonBeam.CannonBeamAttackObject m_attackObj;

  public void Initialize(AttackCannonBeam.InitParamCannonBeam initParam)
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
    this.m_attacker = initParam.attacker;
    this.m_cachedTransform = ((Component) this).transform;
    this.m_cachedTransform.parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
    Transform launchTrans = initParam.launchTrans;
    this.m_cachedTransform.position = launchTrans.position;
    this.m_cachedTransform.rotation = launchTrans.rotation;
    this.m_cachedTransform.localScale = data.timeStartScale;
    this.m_existTime = data.appearTime;
    this.m_hasExistTime = (double) data.appearTime > 0.0;
    float radius = data.radius;
    float capsuleHeight = data.capsuleHeight;
    Vector3 hitOffset = data.hitOffset;
    int mask = 20736;
    AttackCannonBeam.CannonBeamAttackObject beamAttackObject = new GameObject("CannonBeamAttackObject").AddComponent<AttackCannonBeam.CannonBeamAttackObject>();
    beamAttackObject.Initialize(this.m_attacker, this.m_cachedTransform, this.m_atkInfo, hitOffset, Vector3.zero, radius, capsuleHeight, 14);
    beamAttackObject.SetIgnoreLayerMask(mask);
    this.m_attackObj = beamAttackObject;
    this.m_effectTrans = EffectManager.GetEffect(data.effectName, launchTrans);
  }

  private void Update()
  {
    if (!this.m_hasExistTime)
      return;
    this.m_existTimer += Time.deltaTime;
    if ((double) this.m_existTimer <= (double) this.m_existTime)
      return;
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private void OnDestroy()
  {
    Transform transform = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
    if (!Object.op_Inequality((Object) this.m_effectTrans, (Object) null))
      return;
    this.m_effectTrans.parent = transform;
    EffectManager.ReleaseEffect(((Component) this.m_effectTrans).gameObject);
  }

  public class InitParamCannonBeam
  {
    public StageObject attacker;
    public AttackInfo atkInfo;
    public Transform launchTrans;
  }

  public class CannonBeamAttackObject : AttackColliderObject
  {
    public int ignoreLayerMask;
    private float fixedTime;

    public bool isHit { get; private set; }

    public int hitLayer { get; private set; }

    public Enemy hitEnemy { get; private set; }

    public void SetIgnoreLayerMask(int mask) => this.ignoreLayerMask = mask;

    public override float GetTime() => this.fixedTime;

    private void FixedUpdate() => this.fixedTime += Time.fixedDeltaTime;

    protected override void OnTriggerEnter(Collider collider)
    {
      this.hitLayer = ((Component) collider).gameObject.layer;
      if ((1 << this.hitLayer & this.ignoreLayerMask) != 0)
        return;
      if (this.hitLayer == 11)
        this.hitEnemy = ((Component) collider).gameObject.GetComponent<Enemy>();
      this.isHit = true;
      base.OnTriggerEnter(collider);
      this.DeactivateOwnCollider();
      this.Destroy();
    }
  }
}
