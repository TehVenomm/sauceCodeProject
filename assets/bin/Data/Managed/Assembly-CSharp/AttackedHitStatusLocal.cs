// Decompiled with JetBrains decompiler
// Type: AttackedHitStatusLocal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AttackedHitStatusLocal
{
  public AttackedHitStatus origin { get; protected set; }

  public AttackedHitStatusLocal() => this.origin = new AttackedHitStatus();

  public AttackedHitStatusLocal(AttackedHitStatus status) => this.origin = status;

  public AttackHitColliderProcessor.HitParam hitParam => this.origin.hitParam;

  public AttackHitInfo attackInfo => this.origin.attackInfo;

  public int fromObjectID => this.origin.fromObjectID;

  public StageObject fromObject => this.origin.fromObject;

  public StageObject.OBJECT_TYPE fromType => this.origin.fromType;

  public Vector3 fromPos => this.origin.fromPos;

  public Vector3 hitPos => this.origin.hitPos;

  public float distanceXZ => this.origin.distanceXZ;

  public float hitTime => this.origin.hitTime;

  public int fromClientID => this.origin.fromClientID;

  public DamageDistanceTable.DamageDistanceData damageDistanceData
  {
    get => this.origin.damageDistanceData;
  }

  public AtkAttribute atk => this.origin.atk;

  public SkillInfo.SkillParam skillParam => this.origin.skillParam;

  public bool validDamage => this.origin.validDamage;

  public BadStatus badStatusAdd => this.origin.badStatusAdd;

  public int regionID => this.origin.regionID;

  public bool isDamageRegionOnly => this.origin.isDamageRegionOnly;

  public Enemy.WEAK_STATE weakState => this.origin.weakState;

  public int damage
  {
    get => this.origin.damage;
    set => this.origin.damage = value;
  }

  public AtkAttribute damageDetails
  {
    get => this.origin.damageDetails;
    set => this.origin.damageDetails = value;
  }

  public float downAddBase
  {
    get => this.origin.downAddBase;
    set => this.origin.downAddBase = value;
  }

  public float downAddWeak
  {
    get => this.origin.downAddWeak;
    set => this.origin.downAddWeak = value;
  }

  public bool isForceDown
  {
    get => this.origin.isForceDown;
    set => this.origin.isForceDown = value;
  }

  public float concussionAdd
  {
    get => this.origin.concussionAdd;
    set => this.origin.concussionAdd = value;
  }

  public bool isArrowBleed
  {
    get => this.origin.isArrowBleed;
    set => this.origin.isArrowBleed = value;
  }

  public int arrowBleedDamage
  {
    get => this.origin.arrowBleedDamage;
    set => this.origin.arrowBleedDamage = value;
  }

  public int arrowBurstDamage
  {
    get => this.origin.arrowBurstDamage;
    set => this.origin.arrowBurstDamage = value;
  }

  public bool isShadowSealing
  {
    get => this.origin.isShadowSealing;
    set => this.origin.isShadowSealing = value;
  }

  public bool isArrowBomb
  {
    get => this.origin.isArrowBomb;
    set => this.origin.isArrowBomb = value;
  }

  public Player.ATTACK_MODE attackMode
  {
    get => this.origin.attackMode;
    set => this.origin.attackMode = value;
  }
}
