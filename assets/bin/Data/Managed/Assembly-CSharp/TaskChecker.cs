using Network;
using System;
using UnityEngine;

[Serializable]
public class TaskChecker : BattleCheckerBase
{
	[SerializeField]
	private TaskUpdateInfo taskCount = new TaskUpdateInfo();

	public void Clear()
	{
		taskCount = new TaskUpdateInfo();
	}

	public TaskUpdateInfo GetTaskCount()
	{
		return taskCount;
	}

	public void OnRevival()
	{
		taskCount.revival++;
	}

	public void OnGuard()
	{
		taskCount.guard++;
	}

	protected override void OnCounter(int damage)
	{
		taskCount.counter++;
	}

	protected override void OnSpearSpecialAttack(int damage)
	{
		taskCount.lance++;
	}

	protected override void OnSpearExChargeAttack(int damage)
	{
	}

	protected override void OnPairSwordsCombo(int damage)
	{
		taskCount.combo++;
		base.isEnableAttackCount = false;
	}

	protected override void OnTwoHandSwordChargeAttack(int damage)
	{
		taskCount.chargesword++;
	}

	protected override void OnTwoHandSwordExChargeAttack(int damage)
	{
	}

	protected override void OnArrowChargeAttack(int damage)
	{
		taskCount.chargebow++;
	}

	public void OnUseMagi()
	{
		taskCount.usemagi++;
	}

	protected override void OnNormalWeakHit(int damage)
	{
		taskCount.weak++;
	}

	protected override void OnWeaponWeakHit(int damage)
	{
		taskCount.weaponweak++;
	}

	public void OnDeath()
	{
		taskCount.death++;
	}

	public void OnJustGuard()
	{
		taskCount.justGuard++;
	}

	protected override void OnRevengeBurst(int damage)
	{
		taskCount.revengeBurst++;
	}

	protected override void OnHeatTwoHandSword(int damage)
	{
		taskCount.heatTwoHandSword++;
	}

	public void OnJump()
	{
		taskCount.jump++;
	}

	public void OnHeatPairSwords()
	{
		taskCount.heatPairSwords++;
	}

	public void OnShadowSealing()
	{
		taskCount.shadowSealing++;
	}

	public void OnSoulOneHandSword()
	{
		taskCount.soulOneHandSword++;
	}

	public void OnSoulTwoHandSword()
	{
		taskCount.soulTwoHandSword++;
	}

	public void OnSoulSpear()
	{
		taskCount.soulSpear++;
	}

	public void OnSoulPairSwords()
	{
		taskCount.soulPairSwords++;
	}

	public void OnSoulArrow()
	{
		taskCount.soulArrow++;
	}

	protected override void OnBurstOneHandSword(int damage)
	{
		taskCount.burstOneHandSword++;
	}

	public void OnBurstTwoHandSword()
	{
		taskCount.thsFullBurst++;
	}
}
