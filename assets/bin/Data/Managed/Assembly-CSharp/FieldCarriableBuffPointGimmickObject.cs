// Decompiled with JetBrains decompiler
// Type: FieldCarriableBuffPointGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FieldCarriableBuffPointGimmickObject : FieldCarriableGimmickObject
{
  public static readonly string kPutEffectName = "ef_btl_trap_01_02";
  public static readonly int kPutSEId = 10000058;
  private static readonly int kShiftIndex = 1000;
  private static readonly string kHeadEffectNameFormat = "ef_btl_trap_03_{0:D2}_01";
  private static readonly string kBuffEffectNameFormat = "ef_btl_trap_03_{0:D2}_02";
  private static readonly float kDefaultBuffRadius = 2.5f;
  protected Transform buffEffect;
  protected Transform headEffect;
  protected bool isTargetPlayer;
  protected List<FieldCarriableBuffPointGimmickObject.BuffData> buffList;

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    if (this.buffList.IsNullOrEmpty<FieldCarriableBuffPointGimmickObject.BuffData>())
      return;
    int index1 = 0;
    for (int count1 = this.buffList.Count; index1 < count1; ++index1)
    {
      int index2 = 0;
      for (int count2 = this.buffList[index1].buffIdList.Count; index2 < count2; ++index2)
      {
        BuffTable.BuffData data = Singleton<BuffTable>.I.GetData(this.buffList[index1].buffIdList[index2]);
        if (data != null)
          this.buffList[index1].buffParamList.Add(new BuffParam.BuffData()
          {
            type = data.type,
            valueType = data.valueType,
            value = data.value,
            time = data.duration,
            interval = data.interval
          });
      }
    }
  }

  protected override void ParseParam(string value2)
  {
    base.ParseParam(value2);
    if (value2.IsNullOrWhiteSpace())
      return;
    this.buffList = new List<FieldCarriableBuffPointGimmickObject.BuffData>();
    for (int index = 0; index <= this.maxLv; ++index)
      this.buffList.Add(new FieldCarriableBuffPointGimmickObject.BuffData());
    string[] strArray1 = new string[2]{ "bf", "r" };
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray2 = str2.Split(chArray2);
      if (strArray2 != null && strArray2.Length == 2)
      {
        if (strArray2[0] == "pl")
        {
          this.isTargetPlayer = int.Parse(strArray2[1]) > 0;
          continue;
        }
        for (int index = 0; index < strArray1.Length; ++index)
        {
          int result = 0;
          if (strArray2[0].StartsWith(strArray1[index]) && int.TryParse(strArray2[0].Replace(strArray1[index], ""), out result) && result <= this.maxLv)
          {
            switch (strArray1[index])
            {
              case "bf":
                this.buffList[result].buffIdList.Add(uint.Parse(strArray2[1]));
                goto label_18;
              case "r":
                this.buffList[result].buffRadius = float.Parse(strArray2[1]);
                goto label_18;
              default:
                goto label_18;
            }
          }
        }
        continue;
      }
      continue;
label_18:;
    }
  }

  protected override void OnStartCarry(Player owner)
  {
    base.OnStartCarry(owner);
    if (Object.op_Inequality((Object) this.buffEffect, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.buffEffect).gameObject);
      this.buffEffect = (Transform) null;
    }
    if (!Object.op_Inequality((Object) this.headEffect, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this.headEffect).gameObject);
    this.headEffect = (Transform) null;
  }

  protected override void OnEndCarry()
  {
    base.OnEndCarry();
    if (Object.op_Equality((Object) this.buffEffect, (Object) null))
    {
      this.buffEffect = EffectManager.GetEffect(FieldCarriableBuffPointGimmickObject.GetBuffEffectNameByModelIndex(this.modelIndex), this.GetTransform());
      Transform buffEffect = this.buffEffect;
      buffEffect.localScale = Vector3.op_Multiply(buffEffect.localScale, this.buffList[this.currentLv].buffRadius / FieldCarriableBuffPointGimmickObject.kDefaultBuffRadius);
    }
    if (Object.op_Equality((Object) this.headEffect, (Object) null))
      this.headEffect = EffectManager.GetEffect(FieldCarriableBuffPointGimmickObject.GetHeadEffectNameByModelIndex(this.modelIndex), this.GetTransform());
    EffectManager.OneShot(FieldCarriableBuffPointGimmickObject.kPutEffectName, this.GetTransform().position, this.GetTransform().rotation);
    SoundManager.PlayOneShotSE(FieldCarriableBuffPointGimmickObject.kPutSEId, this.GetTransform().position);
  }

  protected override void OnEvolved()
  {
    base.OnEvolved();
    if (Object.op_Inequality((Object) this.buffEffect, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.buffEffect).gameObject);
      this.buffEffect = (Transform) null;
    }
    if (Object.op_Inequality((Object) this.headEffect, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.headEffect).gameObject);
      this.headEffect = (Transform) null;
    }
    this.buffEffect = EffectManager.GetEffect(FieldCarriableBuffPointGimmickObject.GetBuffEffectNameByModelIndex(this.modelIndex), this.GetTransform());
    Transform buffEffect = this.buffEffect;
    buffEffect.localScale = Vector3.op_Multiply(buffEffect.localScale, this.buffList[this.currentLv].buffRadius / FieldCarriableBuffPointGimmickObject.kDefaultBuffRadius);
    this.headEffect = EffectManager.GetEffect(FieldCarriableBuffPointGimmickObject.GetHeadEffectNameByModelIndex(this.modelIndex), this.GetTransform());
  }

  private void LateUpdate()
  {
    if (this.buffList.IsNullOrEmpty<FieldCarriableBuffPointGimmickObject.BuffData>() || this.buffList[this.currentLv].buffParamList.IsNullOrEmpty<BuffParam.BuffData>() || Object.op_Equality((Object) this.buffEffect, (Object) null))
      return;
    FieldCarriableBuffPointGimmickObject.BuffData buff = this.buffList[this.currentLv];
    List<StageObject> stageObjectList = !this.isTargetPlayer ? MonoBehaviourSingleton<StageObjectManager>.I.enemyList : MonoBehaviourSingleton<StageObjectManager>.I.playerList;
    for (int index1 = 0; index1 < stageObjectList.Count; ++index1)
    {
      Character character = stageObjectList[index1] as Character;
      if (!Object.op_Equality((Object) character, (Object) null) && !character.isDead && (double) Vector3.Magnitude(Vector3.op_Subtraction(this.GetTransform().position, character._transform.position)) <= (double) buff.buffRadius)
      {
        for (int index2 = 0; index2 < buff.buffParamList.Count; ++index2)
        {
          if (!character.IsValidBuff(buff.buffParamList[index2].type))
            character.OnBuffStart(buff.buffParamList[index2]);
        }
      }
    }
  }

  public static string GetHeadEffectNameByModelIndex(int index)
  {
    return string.Format(FieldCarriableBuffPointGimmickObject.kHeadEffectNameFormat, (object) (index + 1));
  }

  public static string GetBuffEffectNameByModelIndex(int index)
  {
    return string.Format(FieldCarriableBuffPointGimmickObject.kBuffEffectNameFormat, (object) (index + 1));
  }

  protected class BuffData
  {
    public float buffRadius = FieldCarriableBuffPointGimmickObject.kDefaultBuffRadius;
    public List<uint> buffIdList = new List<uint>();
    public List<BuffParam.BuffData> buffParamList = new List<BuffParam.BuffData>();
  }
}
