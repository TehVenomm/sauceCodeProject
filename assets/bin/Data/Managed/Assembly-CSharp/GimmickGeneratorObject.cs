// Decompiled with JetBrains decompiler
// Type: GimmickGeneratorObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GimmickGeneratorObject : StageObject, IFieldGimmickObject
{
  private const string NAME_NODE_ATK_COLLIDER_R = "attack_R";
  private const string NAME_NODE_ATK_COLLIDER_L = "attack_L";
  private int generateType;
  private float interval;
  private float duration;
  private float startX;
  private float startZ;
  private float endX;
  private float endZ;
  private string effectName = string.Empty;
  private float normalAtk;
  private int colliderDirection = 2;
  private float colliderRadius;
  private float colliderHeight;
  private float colliderCenterX;
  private float colliderCenterY;
  private float colliderCenterZ;
  private Vector3 startPos = Vector3.zero;
  private Vector3 endPos = Vector3.zero;
  private Vector3 center = Vector3.zero;
  private float generateTimer;
  private AttackHitChecker attackHitChecker = new AttackHitChecker();
  private bool referenceCheckerFlag;
  private List<GimmickGeneratorObject.GeneratedObject> generatedObjectList = new List<GimmickGeneratorObject.GeneratedObject>();

  public void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    if (pointData == null)
      return;
    this.id = (int) pointData.pointID;
    this.ParseParam(pointData.value2);
    this.SetAppearPoints();
    this.SetCenter();
  }

  public int GetId() => this.id;

  public void RequestDestroy() => this.DestroyObject();

  public void SetTransform(Transform trans)
  {
  }

  public Transform GetTransform() => this._transform;

  public string GetObjectName() => nameof (GimmickGeneratorObject);

  public float GetTargetRadius() => 0.0f;

  public float GetTargetSqrRadius() => 0.0f;

  public void UpdateTargetMarker(bool isNear)
  {
  }

  public bool IsSearchableNearest() => true;

  protected override void Update()
  {
    base.Update();
    int index = 0;
    for (int count = this.generatedObjectList.Count; index < count; ++index)
      this.generatedObjectList[index].Update();
    this.generatedObjectList.RemoveAll((Predicate<GimmickGeneratorObject.GeneratedObject>) (o => !o.IsEnable()));
    if (!this.IsCoopNone() && !this.IsOriginal())
      return;
    this.generateTimer += Time.deltaTime;
    if (this.generateType != 1 || (double) this.generateTimer <= (double) this.interval)
      return;
    this.OnGenerateForLinearMove(this.startPos);
    this.generateTimer = 0.0f;
  }

  public void OnGenerateForLinearMove(Vector3 pos)
  {
    if (this.generatedObjectList == null || (this.IsCoopNone() || this.IsOriginal()) && this.generatedObjectList.Count > 0)
      return;
    Transform gameObject = Utility.CreateGameObject("GimmickShot", this._transform);
    gameObject.position = pos;
    Transform transform1 = gameObject;
    Vector3 vector3 = Vector3.op_Subtraction(this.endPos, this.startPos);
    Quaternion quaternion = Quaternion.LookRotation(((Vector3) ref vector3).normalized);
    transform1.rotation = quaternion;
    Transform transform2 = (Transform) null;
    if (!this.effectName.IsNullOrWhiteSpace())
    {
      transform2 = EffectManager.GetEffect(this.effectName, gameObject);
      transform2.localPosition = Vector3.zero;
      transform2.localRotation = Quaternion.identity;
    }
    if (Object.op_Inequality((Object) transform2, (Object) null))
    {
      Transform transform3 = Utility.Find(transform2, "attack_R");
      Transform transform4 = Utility.Find(transform2, "attack_L");
      GeneratedAttackObject generatedAttackObject1 = ((Component) transform3).gameObject.AddComponent<GeneratedAttackObject>();
      GeneratedAttackObject generatedAttackObject2 = ((Component) transform4).gameObject.AddComponent<GeneratedAttackObject>();
      GeneratedAttackObject generatedAttackObject3 = ((Component) transform3).gameObject.AddComponent<GeneratedAttackObject>();
      GeneratedAttackObject generatedAttackObject4 = ((Component) transform4).gameObject.AddComponent<GeneratedAttackObject>();
      AttackHitInfo atkInfo1 = new AttackHitInfo();
      atkInfo1.name = "generatedAttack";
      atkInfo1.attackType = AttackHitInfo.ATTACK_TYPE.GIMMICK_GENERATED;
      atkInfo1.toPlayer.reactionType = AttackHitInfo.ToPlayer.REACTION_TYPE.BLOW;
      atkInfo1.toPlayer.reactionBlowForce = 100f;
      atkInfo1.toPlayer.reactionBlowAngle = 20f;
      atkInfo1.atk.normal = this.normalAtk;
      Transform parent = transform3.parent;
      AttackHitInfo atkInfo2 = atkInfo1;
      Vector3 zero1 = Vector3.zero;
      Vector3 zero2 = Vector3.zero;
      double colliderRadius = (double) this.colliderRadius;
      double colliderHeight = (double) this.colliderHeight;
      int colliderDirection = this.colliderDirection;
      Vector3 center = this.center;
      generatedAttackObject1.Initialize((StageObject) this, parent, (AttackInfo) atkInfo2, zero1, zero2, (float) colliderRadius, (float) colliderHeight, colliderDirection, center, 31 /*0x1F*/);
      generatedAttackObject2.Initialize((StageObject) this, transform4.parent, (AttackInfo) atkInfo1, Vector3.zero, Vector3.zero, this.colliderRadius, this.colliderHeight, this.colliderDirection, Vector3.op_UnaryNegation(this.center), 31 /*0x1F*/);
      generatedAttackObject3.Initialize((StageObject) this, transform3.parent, (AttackInfo) atkInfo1, Vector3.zero, Vector3.zero, this.colliderRadius, this.colliderHeight, this.colliderDirection, this.center, 31 /*0x1F*/);
      generatedAttackObject4.Initialize((StageObject) this, transform4.parent, (AttackInfo) atkInfo1, Vector3.zero, Vector3.zero, this.colliderRadius, this.colliderHeight, this.colliderDirection, Vector3.op_UnaryNegation(this.center), 31 /*0x1F*/);
    }
    this.generatedObjectList.Add(new GimmickGeneratorObject.GeneratedObject(gameObject, 10f));
    if (this.referenceCheckerFlag)
    {
      this.attackHitChecker = new AttackHitChecker();
      this.referenceCheckerFlag = false;
    }
    if (!Object.op_Inequality((Object) this.packetSender, (Object) null))
      return;
    this.packetSender.OnShotGimmickGenerator(pos);
  }

  private void SetAppearPoints()
  {
    this.startPos = new Vector3(this.startX, 0.0f, this.startZ);
    this.endPos = new Vector3(this.endX, 0.0f, this.endZ);
  }

  private void SetCenter()
  {
    this.center = new Vector3(this.colliderCenterX, this.colliderCenterY, this.colliderCenterZ);
  }

  private void ParseParam(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return;
    string[] strArray1 = value2.Split(',');
    int index = 0;
    for (int length = strArray1.Length; index < length; ++index)
    {
      string[] strArray2 = strArray1[index].Split(':');
      if (strArray2 != null && strArray2.Length == 2)
      {
        switch (strArray2[0])
        {
          case "atk":
            float.TryParse(strArray2[1], out this.normalAtk);
            continue;
          case "cX":
            float.TryParse(strArray2[1], out this.colliderCenterX);
            continue;
          case "cY":
            float.TryParse(strArray2[1], out this.colliderCenterY);
            continue;
          case "cZ":
            float.TryParse(strArray2[1], out this.colliderCenterZ);
            continue;
          case "dir":
            int.TryParse(strArray2[1], out this.colliderDirection);
            continue;
          case "du":
            float.TryParse(strArray2[1], out this.duration);
            continue;
          case "eX":
            float.TryParse(strArray2[1], out this.endX);
            continue;
          case "eZ":
            float.TryParse(strArray2[1], out this.endZ);
            continue;
          case "eff":
            this.effectName = strArray2[1];
            continue;
          case "h":
            float.TryParse(strArray2[1], out this.colliderHeight);
            continue;
          case "in":
            float.TryParse(strArray2[1], out this.interval);
            continue;
          case "r":
            float.TryParse(strArray2[1], out this.colliderRadius);
            continue;
          case "sX":
            float.TryParse(strArray2[1], out this.startX);
            continue;
          case "sZ":
            float.TryParse(strArray2[1], out this.startZ);
            continue;
          case "type":
            int.TryParse(strArray2[1], out this.generateType);
            continue;
          default:
            continue;
        }
      }
    }
  }

  public static string[] GetEffectNames(string value2)
  {
    List<string> stringList = new List<string>();
    if (value2.IsNullOrWhiteSpace())
      return new string[0];
    string[] strArray1 = value2.Split(',');
    int index = 0;
    for (int length = strArray1.Length; index < length; ++index)
    {
      string[] strArray2 = strArray1[index].Split(':');
      if (strArray2 != null && strArray2.Length == 2 && strArray2[0] == "eff")
        stringList.Add(strArray2[1]);
    }
    return stringList.ToArray();
  }

  protected override bool IsValidAttackedHit(StageObject from_object) => false;

  public override AttackHitChecker ReferenceAttackHitChecker()
  {
    this.referenceCheckerFlag = true;
    return this.attackHitChecker;
  }

  private enum GENERATE_TYPE
  {
    NONE,
    LINEAR_MOVE,
    MAX,
  }

  private class GeneratedObject
  {
    private Transform trans;
    private bool enable;
    private float duration;
    private float timer;

    public GeneratedObject(Transform trans, float duration)
    {
      this.trans = trans;
      this.duration = duration;
      this.enable = true;
      this.timer = 0.0f;
    }

    public void Update()
    {
      this.timer += Time.deltaTime;
      if ((double) this.timer < (double) this.duration)
        return;
      this.Destroy();
    }

    public void Destroy()
    {
      if (Object.op_Inequality((Object) this.trans, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.trans).gameObject);
        this.trans = (Transform) null;
      }
      this.enable = false;
    }

    public bool IsEnable() => this.enable;

    public Vector3 GetPosition() => this.trans.position;
  }
}
