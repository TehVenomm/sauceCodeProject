// Decompiled with JetBrains decompiler
// Type: FieldGimmickCannonField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class FieldGimmickCannonField : FieldGimmickCannonBase
{
  private const string kDefaultAttackInfoName = "cannonball_field";
  private const int kDefaultModelIndex = 5;
  private const int kShiftIndex = 1000;
  private readonly Vector3 OFFSET_LEFT = new Vector3(-0.4f, 0.0f, 0.0f);
  private readonly Vector3 OFFSET_RIGHT = new Vector3(0.4f, 0.0f, 0.0f);
  private readonly Vector3 OFFSET_ZERO = Vector3.zero;
  private Vector3[] offsetArray;
  private int shotSeId;
  private bool isAimCamera;
  private string attackInfoName = "cannonball_field";
  private int modelIndex = 5;

  public static string GetAttackInfoName(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return "cannonball_field";
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "ai")
        return strArray[1];
    }
    return "cannonball_field";
  }

  public static int GetModelIndex(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return 5;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "mi")
      {
        int result = 0;
        if (int.TryParse(strArray[1], out result))
          return result;
      }
    }
    return 5;
  }

  public static string ConvertModelIndexToName(int idx) => $"CMN_cannon{idx:D2}";

  public static uint ConvertModelIndexToKey(int idx) => (uint) (idx * 1000 + 11);

  public override bool IsAimCamera() => this.isAimCamera;

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.m_coolTime = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.coolTimeForField;
    this.m_baseTrans = this.modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot");
    this.m_cannonTrans = this.modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot");
    this.offsetArray = new Vector3[3]
    {
      this.OFFSET_ZERO,
      this.OFFSET_RIGHT,
      this.OFFSET_LEFT
    };
    this.shotSeId = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForField;
  }

  protected override void CreateModel()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable == null)
      return;
    LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(FieldGimmickCannonField.ConvertModelIndexToKey(this.modelIndex));
    if (loadObject == null)
      return;
    this.modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, this.m_transform);
  }

  protected override void UpdateStateStandBy()
  {
    if (Object.op_Equality((Object) this.m_owner, (Object) null) || Object.op_Equality((Object) this.m_baseTrans, (Object) null))
      return;
    this.m_owner._rotation = Quaternion.LookRotation(this.m_baseTrans.forward);
    this.SetState(FieldGimmickCannonBase.STATE.READY);
  }

  public override void Shot()
  {
    if (!this.IsReadyForShot())
      return;
    if (Object.op_Inequality((Object) this._animator, (Object) null))
      this._animator.Play("Reaction", 0, 0.0f);
    AttackInfo attackHitInfo = this.GetAttackHitInfo();
    if (attackHitInfo == null)
      return;
    int index = Random.Range(0, 3);
    new GameObject("AttackCannonball").AddComponent<AttackCannonball>().Initialize(new AttackCannonball.InitParamCannonball()
    {
      attacker = (StageObject) this.m_owner,
      atkInfo = attackHitInfo,
      launchTrans = this.m_cannonTrans,
      offsetPos = this.offsetArray[index],
      offsetRot = Quaternion.identity,
      shotRotation = this.m_cannonTrans.rotation
    });
    if (this.shotSeId > 0)
      SoundManager.PlayOneShotSE(this.shotSeId, this.m_cannonTrans.position);
    this.StartCoolTime();
    this.SetState(FieldGimmickCannonBase.STATE.COOLTIME);
  }

  protected override AttackInfo GetAttackHitInfo()
  {
    return Object.op_Equality((Object) this.m_owner, (Object) null) ? (AttackInfo) null : this.m_owner.GetAttackInfos().Find<AttackInfo>((Predicate<AttackInfo>) (info => info.name == this.attackInfoName)) ?? (AttackInfo) null;
  }

  protected override void ParseParam(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2)
      {
        switch (strArray[0])
        {
          case "a":
            this.isAimCamera = strArray[1] != "0";
            continue;
          case "ai":
            this.attackInfoName = strArray[1];
            continue;
          case "mi":
            int.TryParse(strArray[1], out this.modelIndex);
            continue;
          default:
            continue;
        }
      }
    }
  }
}
