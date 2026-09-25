// Decompiled with JetBrains decompiler
// Type: AttackedHitStatusDirection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AttackedHitStatusDirection
{
  public AttackedHitStatus origin { get; protected set; }

  public AttackedHitStatusDirection() => this.origin = new AttackedHitStatus();

  public AttackedHitStatusDirection(AttackedHitStatus status) => this.origin = status;

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

  public Vector3 exHitPos => this.origin.exHitPos;

  public AtkAttribute atk
  {
    get => this.origin.atk;
    set => this.origin.atk = value;
  }

  public SkillInfo.SkillParam skillParam
  {
    get => this.origin.skillParam;
    set => this.origin.skillParam = value;
  }

  public bool validDamage
  {
    get => this.origin.validDamage;
    set => this.origin.validDamage = value;
  }

  public BadStatus badStatusAdd
  {
    get => this.origin.badStatusAdd;
    set => this.origin.badStatusAdd = value;
  }

  public int regionID
  {
    get => this.origin.regionID;
    set => this.origin.regionID = value;
  }

  public bool isDamageRegionOnly
  {
    get => this.origin.isDamageRegionOnly;
    set => this.origin.isDamageRegionOnly = value;
  }

  public Enemy.WEAK_STATE weakState
  {
    get => this.origin.weakState;
    set => this.origin.weakState = value;
  }
}
