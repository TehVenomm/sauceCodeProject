// Decompiled with JetBrains decompiler
// Type: TaskChecker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
[Serializable]
public class TaskChecker : BattleCheckerBase
{
  [SerializeField]
  private TaskUpdateInfo taskCount = new TaskUpdateInfo();

  public void Clear() => this.taskCount = new TaskUpdateInfo();

  public TaskUpdateInfo GetTaskCount() => this.taskCount;

  public void OnRevival() => ++this.taskCount.revival;

  public void OnGuard() => ++this.taskCount.guard;

  protected override void OnCounter(int damage) => ++this.taskCount.counter;

  protected override void OnSpearSpecialAttack(int damage) => ++this.taskCount.lance;

  protected override void OnSpearExChargeAttack(int damage)
  {
  }

  protected override void OnPairSwordsCombo(int damage)
  {
    ++this.taskCount.combo;
    this.isEnableAttackCount = false;
  }

  protected override void OnTwoHandSwordChargeAttack(int damage) => ++this.taskCount.chargesword;

  protected override void OnTwoHandSwordExChargeAttack(int damage)
  {
  }

  protected override void OnArrowChargeAttack(int damage) => ++this.taskCount.chargebow;

  public void OnUseMagi() => ++this.taskCount.usemagi;

  protected override void OnNormalWeakHit(int damage) => ++this.taskCount.weak;

  protected override void OnWeaponWeakHit(int damage) => ++this.taskCount.weaponweak;

  public void OnDeath() => ++this.taskCount.death;

  public void OnJustGuard() => ++this.taskCount.justGuard;

  protected override void OnRevengeBurst(int damage) => ++this.taskCount.revengeBurst;

  protected override void OnHeatTwoHandSword(int damage) => ++this.taskCount.heatTwoHandSword;

  public void OnJump() => ++this.taskCount.jump;

  public void OnHeatPairSwords() => ++this.taskCount.heatPairSwords;

  public void OnShadowSealing() => ++this.taskCount.shadowSealing;

  public void OnSoulOneHandSword() => ++this.taskCount.soulOneHandSword;

  public void OnSoulTwoHandSword() => ++this.taskCount.soulTwoHandSword;

  public void OnSoulSpear() => ++this.taskCount.soulSpear;

  public void OnSoulPairSwords() => ++this.taskCount.soulPairSwords;

  public void OnSoulArrow() => ++this.taskCount.soulArrow;

  protected override void OnBurstOneHandSword(int damage) => ++this.taskCount.burstOneHandSword;

  public void OnBurstTwoHandSword() => ++this.taskCount.thsFullBurst;

  public void OnBurstPairSwords() => ++this.taskCount.burstPairSwords;

  public void OnBurstSpear() => ++this.taskCount.burstSpear;

  public void OnBurstArrow() => ++this.taskCount.burstArrow;

  public void OnConcussion() => ++this.taskCount.concussion;

  public void OnOracleOneHandSword() => ++this.taskCount.oracleOneHandSword;

  public void OnOracleSpear() => ++this.taskCount.oracleSpear;

  public void OnOraclePairSwords() => ++this.taskCount.oraclePairSwords;
}
