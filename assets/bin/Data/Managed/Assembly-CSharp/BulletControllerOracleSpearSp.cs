using UnityEngine;

public class BulletControllerOracleSpearSp : BulletControllerBase
{
	private BulletData.BulletOracleSpearSp data;

	private string chargedEffect;

	private Vector3 basePos = Vector3.get_zero();

	private Quaternion baseRot = Quaternion.get_identity();

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
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, skillInfoParam, pos, rot);
		data = bullet.dataOracleSpearSp;
	}

	public override void RegisterFromObject(StageObject obj)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		base.RegisterFromObject(obj);
		chargedEffect = data.GetEffectName(obj as Player);
		bulletObject.get_transform().SetParent(obj.get_transform());
		basePos = bulletObject._transform.get_localPosition();
		baseRot = bulletObject._transform.get_localRotation();
	}

	private void UpdateBulletTransform()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		bulletObject._transform.set_localPosition(basePos);
		bulletObject._transform.set_localRotation(baseRot);
	}

	public void UpdateChargedEffect()
	{
		Charged = true;
		EffectManager.ReleaseEffect(bulletObject.bulletEffect.get_gameObject(), isPlayEndAnimation: false, immediate: true);
		bulletObject.bulletEffect = EffectManager.GetEffect(chargedEffect, bulletObject.get_transform());
		SoundManager.PlayUISE(data.chargedSEId);
	}

	private void UpdateEffectScale(float rate)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (!Charged)
		{
			bulletObject.bulletEffect.set_localScale(Vector3.get_one() * Mathf.Lerp(1f, 1.2f, rate));
		}
	}
}
