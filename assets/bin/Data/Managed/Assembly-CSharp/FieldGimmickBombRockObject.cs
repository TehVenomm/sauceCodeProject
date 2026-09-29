// Decompiled with JetBrains decompiler
// Type: FieldGimmickBombRockObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class FieldGimmickBombRockObject : StageObject, IFieldGimmickObject
{
  public const string EFFECT_NAME_BOMBROCK_EXPLOSION = "ef_btl_enemy_explosion_01_02";
  public const string EFFECT_NAME_BOMBROCK_BASE = "ef_btl_bg_bombrock_01";
  private const string NAME_ATTACK_HIT_INFO = "bombrock";
  private const float TIME_REMAIN_DESTROY = 0.7f;
  private const float RADIUS_ATTACK_COLLIDER = 2f;
  private const float HEIGHT_ATTACK_COLLIDER = 1f;
  public const int SE_EXPLOSION = 30000102;
  private int m_id;
  private FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE m_gimmickType;
  private Transform m_modelTrans;
  private Transform m_effectBase;
  private SphereCollider m_sphereCollider;
  private float m_attackRate;

  public void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    this.m_id = (int) pointData.pointID;
    this.m_gimmickType = pointData.gimmickType;
    this.m_attackRate = pointData.value1;
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable == null)
      return;
    LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get((uint) this.m_gimmickType);
    if (loadObject == null)
      return;
    this.m_modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, this.GetTransform());
  }

  public void RequestDestroy()
  {
    if (MonoBehaviourSingleton<EffectManager>.IsValid())
      EffectManager.GetEffect("ef_btl_enemy_explosion_01_02", this._transform).localScale = new Vector3(0.8f, 0.8f, 0.8f);
    if (MonoBehaviourSingleton<SoundManager>.IsValid())
      SoundManager.PlayOneShotSE(30000102, this._position);
    ((Component) this.m_modelTrans).gameObject.SetActive(false);
    BombRockAttackObject rockAttackObject1 = new GameObject("BombRockAttackToEnemy").AddComponent<BombRockAttackObject>();
    AttackInfo attackInfo = MonoBehaviourSingleton<StageObjectManager>.I.self.GetAttackInfos().Find<AttackInfo>((Predicate<AttackInfo>) (info => info.name == "bombrock"));
    if (attackInfo != null)
    {
      AttackHitInfo atkInfo = attackInfo as AttackHitInfo;
      atkInfo.atk.normal = this.m_attackRate;
      rockAttackObject1.Initialize((StageObject) MonoBehaviourSingleton<StageObjectManager>.I.self, this._transform, (AttackInfo) atkInfo, Vector3.zero, Vector3.zero, 2f, 1f, 14);
    }
    BombRockAttackObject rockAttackObject2 = new GameObject("BombRockAttackToSelf").AddComponent<BombRockAttackObject>();
    AttackHitInfo attackHitInfo = new AttackHitInfo();
    attackHitInfo.attackType = AttackHitInfo.ATTACK_TYPE.BOMBROCK;
    attackHitInfo.toPlayer.reactionType = AttackHitInfo.ToPlayer.REACTION_TYPE.BLOW;
    attackHitInfo.toPlayer.reactionBlowForce = 100f;
    attackHitInfo.toPlayer.reactionBlowAngle = 20f;
    Transform transform = this._transform;
    AttackHitInfo atkInfo1 = attackHitInfo;
    Vector3 zero1 = Vector3.zero;
    Vector3 zero2 = Vector3.zero;
    rockAttackObject2.Initialize((StageObject) this, transform, (AttackInfo) atkInfo1, zero1, zero2, 2f, 1f, 15);
    if (this.m_effectBase != null)
    {
      Object.Destroy((Object) ((Component) this.m_effectBase).gameObject);
      this.m_effectBase = (Transform) null;
    }
    this.StartCoroutine(this.ProcessDestroy());
  }

  private IEnumerator ProcessDestroy()
  {
    yield return (object) new WaitForSeconds(0.7f);
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  public int GetId() => this.m_id;

  public Transform GetTransform() => this._transform;

  public string GetObjectName() => "BombRock";

  public void SetTransform(Transform trans) => this.m_modelTrans = trans;

  public float GetTargetRadius() => 0.0f;

  public float GetTargetSqrRadius() => 0.0f;

  public void UpdateTargetMarker(bool isNear)
  {
  }

  public bool IsSearchableNearest() => true;

  protected override void Awake()
  {
    base.Awake();
    Utility.SetLayerWithChildren(((Component) this).transform, 18);
    if (this.m_sphereCollider != null)
      return;
    this.m_sphereCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
    this.m_sphereCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
    this.m_sphereCollider.radius = 1.2f;
  }

  protected override void Start()
  {
    if (!MonoBehaviourSingleton<EffectManager>.IsValid())
      return;
    this.m_effectBase = EffectManager.GetEffect("ef_btl_bg_bombrock_01", this._transform);
  }

  protected override bool IsValidAttackedHit(StageObject from_object)
  {
    return from_object is Enemy && base.IsValidAttackedHit(from_object);
  }

  public override void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    base.OnAttackedHitFix(status);
    ((Collider) this.m_sphereCollider).enabled = false;
    this.RequestDestroy();
  }
}
