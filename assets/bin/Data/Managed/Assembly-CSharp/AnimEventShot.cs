// Decompiled with JetBrains decompiler
// Type: AnimEventShot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimEventShot : BulletObject
{
  private float pierceArrowSec;
  private float pierceArrowInterval = 0.033f;
  private TargetPoint _targetPoint;
  protected GameObject attachObject;

  public TargetPoint targetPoint => this._targetPoint;

  public void SetArrowInfo(bool isAim, bool isBossPierce, bool isArrowRain = false)
  {
    this.isShotArrow = true;
    this.isAimMode = isAim;
    this.isBossPierceArrow = isBossPierce;
    this.pierceArrowSec = 0.0f;
    if (isArrowRain)
      this.pierceArrowInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainPierceDamageInterval;
    else
      this.pierceArrowInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.pierceInterval;
  }

  public static AnimEventShot Create(
    StageObject stage_object,
    AnimEventData.EventData data,
    AttackInfo atk_info,
    Vector3 offset)
  {
    Transform transform = stage_object.FindNode(data.stringArgs[1]);
    if (Object.op_Equality((Object) transform, (Object) null))
      transform = stage_object._transform;
    if (Object.op_Inequality((Object) ((Component) transform).gameObject, (Object) null) && !((Component) transform).gameObject.activeInHierarchy)
      return (AnimEventShot) null;
    Vector3 vector3 = Vector3.op_Addition(new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]), offset);
    Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
    Vector3 pos = ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(vector3);
    Quaternion quaternion = Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
    Quaternion rot = data.intArgs[0] != 0 ? Quaternion.op_Multiply(((Component) stage_object).gameObject.transform.rotation, quaternion) : Quaternion.op_Multiply(transform.rotation, quaternion);
    return AnimEventShot.Create(stage_object, atk_info, pos, rot);
  }

  public static AnimEventShot CreateByExternalBulletData(
    BulletData exBulletData,
    StageObject stageObj,
    AttackInfo atkInfo,
    Vector3 pos,
    Quaternion rot,
    AtkAttribute exAtk = null,
    Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE,
    SkillInfo.SkillParam exSkillParam = null)
  {
    if (!Object.op_Equality((Object) exBulletData, (Object) null))
      return AnimEventShot.Create(stageObj, atkInfo, pos, rot, exBulletData: exBulletData, exAtk: exAtk, attackMode: attackMode, exSkillParam: exSkillParam);
    Log.Error("exBulletData is null !!");
    return (AnimEventShot) null;
  }

  public static AnimEventShot CreateArrow(
    StageObject stage_object,
    AttackInfo atk_info,
    Vector3 pos,
    Quaternion rot,
    GameObject attach_object,
    bool isScaling,
    string change_effect,
    DamageDistanceTable.DamageDistanceData damageDistanceData)
  {
    return AnimEventShot.Create(stage_object, atk_info, pos, rot, attach_object, isScaling, change_effect, damageDistanceData: damageDistanceData);
  }

  public static AnimEventShot Create(
    StageObject stage_object,
    AttackInfo atk_info,
    Vector3 pos,
    Quaternion rot,
    GameObject attach_object = null,
    bool isScaling = true,
    string change_effect = null,
    BulletData exBulletData = null,
    AtkAttribute exAtk = null,
    Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE,
    DamageDistanceTable.DamageDistanceData damageDistanceData = null,
    SkillInfo.SkillParam exSkillParam = null)
  {
    BulletData bulletData = atk_info.bulletData;
    if (Object.op_Inequality((Object) exBulletData, (Object) null))
      bulletData = exBulletData;
    if (atk_info.isBulletSkillReference)
    {
      Player player = stage_object as Player;
      if (Object.op_Inequality((Object) player, (Object) null))
      {
        SkillInfo.SkillParam actSkillParam = player.skillInfo.actSkillParam;
        if (actSkillParam != null)
          bulletData = actSkillParam.bullet;
      }
    }
    if (Object.op_Equality((Object) bulletData, (Object) null))
    {
      Log.Error("Failed to shoot bullet!! atk_info:" + (atk_info != null ? atk_info.name : ""));
      return (AnimEventShot) null;
    }
    if (Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I, (Object) null))
      return (AnimEventShot) null;
    AnimEventShot animEventShot = ((Component) Utility.CreateGameObject(((Object) bulletData).name, MonoBehaviourSingleton<StageObjectManager>.I._transform)).gameObject.AddComponent<AnimEventShot>();
    if (isScaling)
    {
      Transform transform = ((Component) stage_object).gameObject.transform;
      animEventShot.SetBaseScale(transform.lossyScale);
    }
    else
      animEventShot.SetBaseScale(Vector3.one);
    animEventShot.SetAttachObject(attach_object);
    if (bulletData.type == BulletData.BULLET_TYPE.BREAKABLE)
      animEventShot.SetTargetPoint();
    animEventShot.Shot(stage_object, atk_info, bulletData, pos, rot, change_effect, exAtk: exAtk, attackMode: attackMode, damageDistanceData: damageDistanceData, exSkillParam: exSkillParam);
    return animEventShot;
  }

  protected override void Awake()
  {
    base.Awake();
    if (!Object.op_Equality((Object) this._collider, (Object) null))
      return;
    CapsuleCollider capsuleCollider = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
    capsuleCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
    capsuleCollider.direction = 2;
    ((Collider) capsuleCollider).isTrigger = true;
    this._collider = (Collider) capsuleCollider;
    this.capsuleCollider = capsuleCollider;
    this.isColliderCreate = true;
  }

  protected override void Start() => base.Start();

  public override void OnDestroy()
  {
    if (AppMain.isApplicationQuit || this.isDestroyed)
      return;
    if (Object.op_Inequality((Object) this.attachObject, (Object) null))
    {
      Object.Destroy((Object) this.attachObject);
      this.attachObject = (GameObject) null;
    }
    base.OnDestroy();
  }

  protected override bool IsLoopEnd() => true;

  public void SetAttachObject(GameObject attach_object)
  {
    this.attachObject = attach_object;
    if (Object.op_Equality((Object) attach_object, (Object) null))
      return;
    attach_object.transform.parent = this._transform;
    attach_object.transform.localPosition = Vector3.zero;
    attach_object.transform.localRotation = Quaternion.identity;
  }

  public void SetTargetPoint()
  {
    this._targetPoint = ((Component) this).gameObject.AddComponent<TargetPoint>();
    this._targetPoint.isAimEnable = false;
    this._targetPoint.isTargetEnable = false;
  }

  protected override void Update()
  {
    base.Update();
    if (this.isDestroyed || !this.isBossPierceArrow)
      return;
    this.pierceArrowSec += Time.deltaTime;
    if ((double) this.pierceArrowSec < (double) this.pierceArrowInterval)
      return;
    this.pierceArrowSec -= this.pierceArrowInterval;
    if (this.attackHitChecker == null)
      return;
    this.attackHitChecker.ClearHitInfo(this.GetAttackInfo().name);
  }

  public enum TARGET_TYPE
  {
    PLAYER_ALL,
    RANDOM_PICKUP,
    RANDOM_POSITION,
  }
}
