using UnityEngine;

public class BulletControllerOracleSpearSp : BulletControllerBase
{
	private BulletData.BulletOracleSpearSp data;

	private string chargedEffect;

	private Vector3 basePos = Vector3.zero;

	private Quaternion baseRot = Quaternion.identity;

	public bool Charged
	{
		get;
		protected set;
	}

	public override void Update()
	{
		base.Update();
		Player player = fromObject as Player;
		if (player != null && player.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
		{
			UpdateBulletTransform();
			UpdateEffectScale(Mathf.Min(1f, Mathf.Max(0f, player.GetChargingRate())));
		}
	}

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam skillInfoParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, skillInfoParam, pos, rot);
		data = bullet.dataOracleSpearSp;
	}

	public override void RegisterFromObject(StageObject obj)
	{
		base.RegisterFromObject(obj);
		chargedEffect = data.GetEffectName(obj as Player);
		bulletObject.transform.SetParent(obj.transform);
		basePos = bulletObject._transform.localPosition;
		baseRot = bulletObject._transform.localRotation;
	}

	private void UpdateBulletTransform()
	{
		bulletObject._transform.localPosition = basePos;
		bulletObject._transform.localRotation = baseRot;
	}

	public void UpdateChargedEffect()
	{
		Charged = true;
		EffectManager.ReleaseEffect(bulletObject.bulletEffect.gameObject, isPlayEndAnimation: false, immediate: true);
		bulletObject.bulletEffect = EffectManager.GetEffect(chargedEffect, bulletObject.transform);
		SoundManager.PlayUISE(data.chargedSEId);
	}

	private void UpdateEffectScale(float rate)
	{
		if (!Charged)
		{
			bulletObject.bulletEffect.localScale = Vector3.one * Mathf.Lerp(1f, 1.2f, rate);
		}
	}
}
