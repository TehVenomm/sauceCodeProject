// Decompiled with JetBrains decompiler
// Type: AttackInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class AttackInfo
{
  [Tooltip("名前")]
  public string name;
  [Tooltip("倍率変化時攻撃情報の名前")]
  public string rateInfoName;
  [Tooltip("弾の終了時生成用の攻撃情報の名前")]
  public string nextBulletInfoName;
  [Tooltip("スキル参照")]
  public bool isSkillReference;
  [Tooltip("複属性スキルの場合どの属性を使うか")]
  public int skillElementIndex;
  [Tooltip("時間変化パラメータ")]
  public AttackInfo.TimeChange timeChange = new AttackInfo.TimeChange();
  public BulletData bulletData;
  public bool isBulletSkillReference;

  public float rateInfoRate { get; set; }

  public AttackInfo() => this.rateInfoRate = 0.0f;

  public virtual AttackInfo GetRateAttackInfo(AttackInfo rate_info, float rate)
  {
    if (rate_info == null || (double) rate <= 0.0)
      return this;
    AttackInfo info = this.CreateInfo();
    info.name = this.name;
    info.rateInfoName = this.rateInfoName;
    info.nextBulletInfoName = this.nextBulletInfoName;
    info.isSkillReference = this.isSkillReference;
    info.skillElementIndex = this.skillElementIndex;
    info.bulletData = !Object.op_Inequality((Object) this.bulletData, (Object) null) ? rate_info.bulletData : this.bulletData.GetRateBulletData(rate_info.bulletData, rate);
    info.isBulletSkillReference = this.isBulletSkillReference;
    info.rateInfoRate = rate;
    return info;
  }

  protected virtual AttackInfo CreateInfo() => new AttackInfo();

  public AttackInfo Duplicate()
  {
    AttackInfo info = this.CreateInfo();
    this.Copy(ref info);
    return info;
  }

  public virtual void Copy(ref AttackInfo rInfo)
  {
    rInfo.name = this.name;
    rInfo.rateInfoName = this.rateInfoName;
    rInfo.nextBulletInfoName = this.nextBulletInfoName;
    rInfo.isSkillReference = this.isSkillReference;
    rInfo.skillElementIndex = this.skillElementIndex;
    rInfo.timeChange.startTime = this.timeChange.startTime;
    rInfo.timeChange.intervalTime = this.timeChange.intervalTime;
    rInfo.timeChange.startRate = this.timeChange.startRate;
    rInfo.timeChange.endRate = this.timeChange.endRate;
    rInfo.bulletData = this.bulletData;
    rInfo.isBulletSkillReference = this.isBulletSkillReference;
    rInfo.rateInfoRate = this.rateInfoRate;
  }

  public static int GetRateValue(int val_a, int val_b, float rate)
  {
    return (int) ((double) val_a + ((double) val_b - (double) val_a) * (double) rate);
  }

  public static float GetRateValue(float val_a, float val_b, float rate)
  {
    return val_a + (val_b - val_a) * rate;
  }

  public static bool GetRateValue(bool val_a, bool val_b, float rate)
  {
    return (double) rate >= 1.0 ? val_b : val_a;
  }

  public static Vector3 GetRateValue(Vector3 val_a, Vector3 val_b, float rate)
  {
    return Vector3.op_Addition(val_a, Vector3.op_Multiply(Vector3.op_Subtraction(val_b, val_a), rate));
  }

  [Serializable]
  public class TimeChange
  {
    [Tooltip("開始時間（秒")]
    public float startTime;
    [Tooltip("変化時間間隔（秒")]
    public float intervalTime;
    [Tooltip("変化前係数")]
    public float startRate = 1f;
    [Tooltip("変化後係数")]
    public float endRate = 1f;
  }
}
