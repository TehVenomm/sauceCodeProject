// Decompiled with JetBrains decompiler
// Type: FieldCarriableTurretGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldCarriableTurretGimmickObject : FieldCarriableGimmickObject
{
  public static readonly string kShotEffectName = "ef_btl_trap_04_01_01";
  public static readonly string kPutEffectName = "ef_btl_trap_01_02";
  public static readonly int kPutSEId = 10000058;
  private static readonly int kShiftIndex = 1000;
  private static readonly string kCannonHeadName = "head01";
  private static readonly string kCannonShotPosName = "shot01";
  private static readonly string kDefaultAttackInfoName = "cannonball_field";
  private static readonly string[] kDefaultAttackInfoNames = new string[1]
  {
    FieldCarriableTurretGimmickObject.kDefaultAttackInfoName
  };
  private static readonly int kDefaultShotSEId = 10000094;
  private static readonly float kDefaultMaxCoolTime = 2f;
  protected Transform cannonHead;
  protected Transform cannonShotPos;
  protected Enemy targetObject;
  protected float searchRange = 10f;
  protected float[] maxCoolTimes;
  protected float coolTime;
  protected string[] attackInfoNames;
  protected Player ownerPlayer;
  protected int shotSEId = FieldCarriableTurretGimmickObject.kDefaultShotSEId;
  protected float rotationSpeed = 1.5f;
  private float targetingTime;
  protected bool isTargeting;

  protected override void Awake()
  {
    base.Awake();
    this.maxCoolTimes = new float[1]
    {
      FieldCarriableTurretGimmickObject.kDefaultMaxCoolTime
    };
    this.attackInfoNames = new string[1]
    {
      FieldCarriableTurretGimmickObject.kDefaultAttackInfoName
    };
  }

  protected override void ParseParam(string value2)
  {
    base.ParseParam(value2);
    if (value2.IsNullOrWhiteSpace())
      return;
    List<string> stringList = new List<string>();
    List<float> floatList = new List<float>();
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2)
      {
        if (strArray[0].StartsWith("ct"))
          floatList.Add(float.Parse(strArray[1]));
        else if (strArray[0].StartsWith("ai"))
        {
          stringList.Add(strArray[1]);
        }
        else
        {
          switch (strArray[0])
          {
            case "sr":
              this.searchRange = float.Parse(strArray[1]);
              continue;
            case "se":
              this.shotSEId = int.Parse(strArray[1]);
              continue;
            case "rt":
              this.rotationSpeed = float.Parse(strArray[1]);
              continue;
            default:
              continue;
          }
        }
      }
    }
    this.attackInfoNames = stringList.ToArray();
    this.maxCoolTimes = floatList.ToArray();
    stringList.Clear();
    floatList.Clear();
  }

  protected override void CreateModel()
  {
    base.CreateModel();
    if (!Object.op_Inequality((Object) this.modelTrans, (Object) null))
      return;
    this.cannonHead = Utility.Find(this.modelTrans, FieldCarriableTurretGimmickObject.kCannonHeadName);
    this.cannonShotPos = Utility.Find(this.modelTrans, FieldCarriableTurretGimmickObject.kCannonShotPosName);
  }

  protected override void OnStartCarry(Player owner)
  {
    base.OnStartCarry(owner);
    if (Object.op_Inequality((Object) owner, (Object) null) && Object.op_Equality((Object) this.ownerPlayer, (Object) null))
      this.ownerPlayer = owner;
    this.targetingTime = 0.0f;
  }

  protected override void OnEndCarry()
  {
    base.OnEndCarry();
    EffectManager.OneShot(FieldCarriableTurretGimmickObject.kPutEffectName, this.GetTransform().position, this.GetTransform().rotation);
    SoundManager.PlayOneShotSE(FieldCarriableTurretGimmickObject.kPutSEId, this.GetTransform().position);
  }

  private void LateUpdate()
  {
    if (this.isCarrying || !this.hasDeploied)
      return;
    this.UpdateTarget();
    this.UpdateHeadRotation();
    if ((double) this.coolTime <= 0.0 && this.Shot())
      this.coolTime = this.GetMaxCoolTime();
    else
      this.coolTime -= Time.deltaTime;
  }

  public float GetMaxCoolTime()
  {
    return this.maxCoolTimes.Length > this.currentLv ? this.maxCoolTimes[this.currentLv] : FieldCarriableTurretGimmickObject.kDefaultMaxCoolTime;
  }

  public string GetAttackInfoName()
  {
    return this.attackInfoNames.Length > this.currentLv ? this.attackInfoNames[this.currentLv] : FieldCarriableTurretGimmickObject.kDefaultAttackInfoName;
  }

  protected virtual void UpdateTarget()
  {
    Enemy enemy1 = this.targetObject;
    if (Object.op_Inequality((Object) enemy1, (Object) null))
    {
      if ((double) Vector3.Magnitude(Vector3.op_Subtraction(enemy1._transform.position, this.GetTransform().position)) > (double) this.searchRange || !this.targetObject.HasValidTargetPoint())
        this.targetObject = (Enemy) null;
      else
        this.targetingTime += Time.deltaTime;
    }
    else
    {
      List<StageObject> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
      for (int index = 0; index < enemyList.Count; ++index)
      {
        Enemy enemy2 = enemyList[index] as Enemy;
        if (enemy2.HasValidTargetPoint())
        {
          float num1 = Vector3.Magnitude(Vector3.op_Subtraction(enemy2._transform.position, this.GetTransform().position));
          if ((double) num1 <= (double) this.searchRange)
          {
            if (Object.op_Inequality((Object) enemy1, (Object) null))
            {
              float num2 = Vector3.Magnitude(Vector3.op_Subtraction(enemy1._transform.position, this.GetTransform().position));
              if ((double) num1 > (double) num2)
                continue;
            }
            enemy1 = enemy2;
          }
        }
      }
      this.targetObject = enemy1;
      this.targetingTime = 0.0f;
      this.isTargeting = false;
    }
  }

  protected virtual void UpdateHeadRotation()
  {
    if (Object.op_Equality((Object) this.targetObject, (Object) null) || Object.op_Equality((Object) this.cannonHead, (Object) null))
      return;
    Vector3 position1 = this.cannonHead.position;
    position1.y = 0.0f;
    Vector3 position2 = this.targetObject._transform.position;
    position2.y = 0.0f;
    this.cannonHead.rotation = Quaternion.Lerp(this.cannonHead.rotation, Quaternion.LookRotation(Vector3.op_Subtraction(position2, position1), Vector3.up), this.targetingTime * this.rotationSpeed);
    if ((double) this.targetingTime * (double) this.rotationSpeed < 1.0)
      return;
    this.isTargeting = true;
  }

  protected virtual bool Shot()
  {
    if (Object.op_Equality((Object) this.cannonHead, (Object) null) || Object.op_Equality((Object) this.cannonShotPos, (Object) null) || Object.op_Equality((Object) this.targetObject, (Object) null) || Object.op_Equality((Object) this.ownerPlayer, (Object) null) || this.isCarrying || !this.isTargeting)
      return false;
    AttackInfo attackInfo = this.ownerPlayer.FindAttackInfo(this.GetAttackInfoName());
    if (attackInfo == null)
      return false;
    if (this.shotSEId > 0)
      SoundManager.PlayOneShotSE(this.shotSEId, this.cannonHead.position);
    AnimEventShot.Create((StageObject) this.ownerPlayer, attackInfo, this.cannonShotPos.position, this.cannonHead.rotation);
    return true;
  }

  public static string[] GetAttackInfoNames(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return FieldCarriableTurretGimmickObject.kDefaultAttackInfoNames;
    List<string> stringList = new List<string>();
    stringList.Add(FieldCarriableTurretGimmickObject.kDefaultAttackInfoName);
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0].StartsWith("ai"))
        stringList.Add(strArray[1]);
    }
    return stringList.ToArray();
  }

  public static int GetShotSEId(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return FieldCarriableTurretGimmickObject.kDefaultShotSEId;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "se")
        return int.Parse(strArray[1]);
    }
    return FieldCarriableTurretGimmickObject.kDefaultShotSEId;
  }
}
