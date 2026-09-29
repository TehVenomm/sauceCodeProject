// Decompiled with JetBrains decompiler
// Type: BadStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class BadStatus
{
  [Tooltip("麻痺")]
  public float paralyze;
  [Tooltip("毒")]
  public float poison;
  [Tooltip("燃焼")]
  public float burning;
  [Tooltip("移動速度低下")]
  public float speedDown;
  [Tooltip("猛毒")]
  public float deadlyPoison;
  [Tooltip("凍結")]
  public float freeze;
  [Tooltip("感電")]
  public float electricShock;
  [Tooltip("墨汚れ")]
  public float inkSplash;
  [Tooltip("滑り")]
  public float slide;
  [Tooltip("沈黙")]
  public float silence;
  [Tooltip("影縫矢存在時間減少割合")]
  public float shadowSealing;
  [Tooltip("影縫拘束時間減少割合")]
  public float shadowSealingBind;
  [Tooltip("攻撃速度低下")]
  public float attackSpeedDown;
  [Tooltip("回復無効")]
  public float cantHealHp;
  [Tooltip("暗闇")]
  public float blind;
  [Tooltip("光輪")]
  public float lightRing;
  [Tooltip("侵食")]
  public float erosion;
  [Tooltip("石化")]
  public float stone;
  [Tooltip("重縛")]
  public float soilShock;
  [Tooltip("出血")]
  public float bleeding;
  [Tooltip("酸")]
  public float acid;
  [Tooltip("モーションストップ")]
  public float damageMotionStop;
  [Tooltip("腐敗")]
  public float corruption;

  public BadStatus()
  {
  }

  public BadStatus(float f)
  {
    this.paralyze = this.poison = this.burning = this.speedDown = this.deadlyPoison = this.freeze = f;
    this.electricShock = f;
    this.inkSplash = f;
    this.slide = f;
    this.silence = f;
    this.shadowSealing = f;
    this.shadowSealingBind = f;
    this.attackSpeedDown = f;
    this.cantHealHp = f;
    this.blind = f;
    this.lightRing = f;
    this.erosion = f;
    this.stone = f;
    this.soilShock = f;
    this.bleeding = f;
    this.acid = f;
    this.damageMotionStop = f;
    this.corruption = f;
  }

  public void Copy(BadStatus status)
  {
    this.paralyze = status.paralyze;
    this.poison = status.poison;
    this.burning = status.burning;
    this.speedDown = status.speedDown;
    this.deadlyPoison = status.deadlyPoison;
    this.freeze = status.freeze;
    this.electricShock = status.electricShock;
    this.inkSplash = status.inkSplash;
    this.slide = status.slide;
    this.silence = status.silence;
    this.shadowSealing = status.shadowSealing;
    this.shadowSealingBind = status.shadowSealingBind;
    this.attackSpeedDown = status.attackSpeedDown;
    this.cantHealHp = status.cantHealHp;
    this.blind = status.blind;
    this.lightRing = status.lightRing;
    this.erosion = status.erosion;
    this.stone = status.stone;
    this.soilShock = status.soilShock;
    this.bleeding = status.bleeding;
    this.acid = status.acid;
    this.damageMotionStop = status.damageMotionStop;
    this.corruption = status.corruption;
  }

  public void Mul(float val)
  {
    this.paralyze *= val;
    this.poison *= val;
    this.burning *= val;
    this.speedDown *= val;
    this.deadlyPoison *= val;
    this.freeze *= val;
    this.electricShock *= val;
    this.inkSplash *= val;
    this.slide *= val;
    this.silence *= val;
    this.shadowSealing *= val;
    this.shadowSealingBind *= val;
    this.attackSpeedDown *= val;
    this.cantHealHp *= val;
    this.blind *= val;
    this.lightRing *= val;
    this.erosion *= val;
    this.stone *= val;
    this.soilShock *= val;
    this.bleeding *= val;
    this.acid *= val;
    this.damageMotionStop *= val;
    this.corruption *= val;
  }

  public void Add(BadStatus status)
  {
    this.paralyze += status.paralyze;
    this.poison += status.poison;
    this.burning += status.burning;
    this.speedDown += status.speedDown;
    this.deadlyPoison += status.deadlyPoison;
    this.freeze += status.freeze;
    this.electricShock += status.electricShock;
    this.inkSplash += status.inkSplash;
    this.slide += status.slide;
    this.silence += status.silence;
    this.shadowSealing += status.shadowSealing;
    this.shadowSealingBind += status.shadowSealingBind;
    this.attackSpeedDown += status.attackSpeedDown;
    this.cantHealHp += status.cantHealHp;
    this.blind += status.blind;
    this.lightRing += status.lightRing;
    this.erosion += status.erosion;
    this.stone += status.stone;
    this.soilShock += status.soilShock;
    this.bleeding += status.bleeding;
    this.acid += status.acid;
    this.damageMotionStop += status.damageMotionStop;
    this.corruption += status.corruption;
  }

  public void Reset()
  {
    this.paralyze = 0.0f;
    this.poison = 0.0f;
    this.burning = 0.0f;
    this.speedDown = 0.0f;
    this.deadlyPoison = 0.0f;
    this.freeze = 0.0f;
    this.electricShock = 0.0f;
    this.inkSplash = 0.0f;
    this.slide = 0.0f;
    this.silence = 0.0f;
    this.shadowSealing = 0.0f;
    this.shadowSealingBind = 0.0f;
    this.attackSpeedDown = 0.0f;
    this.cantHealHp = 0.0f;
    this.blind = 0.0f;
    this.lightRing = 0.0f;
    this.erosion = 0.0f;
    this.stone = 0.0f;
    this.soilShock = 0.0f;
    this.bleeding = 0.0f;
    this.acid = 0.0f;
    this.damageMotionStop = 0.0f;
    this.corruption = 0.0f;
  }

  public bool isExist()
  {
    return (double) this.paralyze != 0.0 || (double) this.poison != 0.0 || (double) this.burning != 0.0 || (double) this.speedDown != 0.0 || (double) this.deadlyPoison != 0.0 || (double) this.freeze != 0.0 || (double) this.electricShock != 0.0 || (double) this.inkSplash != 0.0 || (double) this.slide != 0.0 || (double) this.silence != 0.0 || (double) this.shadowSealing != 0.0 || (double) this.shadowSealingBind != 0.0 || (double) this.attackSpeedDown != 0.0 || (double) this.cantHealHp != 0.0 || (double) this.blind != 0.0 || (double) this.lightRing != 0.0 || (double) this.erosion != 0.0 || (double) this.stone != 0.0 || (double) this.soilShock != 0.0 || (double) this.bleeding != 0.0 || (double) this.acid != 0.0 || (double) this.damageMotionStop != 0.0 || (double) this.corruption != 0.0;
  }

  public override string ToString()
  {
    return $"[BadStatus] paralyze:{this.paralyze} poison:{this.poison} burning:{this.burning} speedDown:{this.speedDown} deadlyPoison:{this.deadlyPoison} freeze:{this.freeze} electricShock:{this.electricShock} inkSplash:{this.inkSplash} slide:{this.slide} silence:{this.silence} shadowSealing:{this.shadowSealing} shadowSealingBind:{this.shadowSealingBind} attackSpeedDown:{this.attackSpeedDown} cantHealHp:{this.cantHealHp} blind:{this.blind} lightRing:{this.lightRing} erosion:{this.erosion} stone:{this.stone} soilShock:{this.soilShock} bleeding:{this.bleeding} acid:{this.acid} corruption:{this.corruption}";
  }
}
