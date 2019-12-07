using UnityEngine;

public class FieldCarriableBombGimmickObject : FieldCarriableGimmickObject
{
	public static readonly string kPutEffectName = "ef_btl_trap_01_02";

	public static readonly string kFuseEffectNameFormat = "ef_btl_trap_05_{0:D2}";

	public static readonly string kFuseEffectStateLoop = "LOOP";

	public static readonly string kFuseEffectStateEnd = "END";

	public static readonly int kPutSEId = 10000058;

	public static readonly float kTimerLimit = 5f;

	private static readonly string kDefaultAttackInfoName = "field_bomb";

	public static readonly int kBombSEId = 10000159;

	protected float timer;

	protected string attackInfoName = kDefaultAttackInfoName;

	protected Player ownerPlayer;

	protected EffectCtrl fuseEffectCtrl;

	protected override void ParseParam(string value2)
	{
		base.ParseParam(value2);
		if (value2.IsNullOrWhiteSpace())
		{
			return;
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2 && array2[0] == "ai")
			{
				attackInfoName = array2[1];
			}
		}
	}

	protected override void OnStartCarry(Player owner)
	{
		base.OnStartCarry(owner);
		ownerPlayer = owner;
		if (fuseEffectCtrl == null)
		{
			Transform effect = EffectManager.GetEffect(GetFuseEffectNameByModelIndex(modelIndex), GetTransform());
			fuseEffectCtrl = effect.GetComponent<EffectCtrl>();
		}
		else
		{
			fuseEffectCtrl.Play(kFuseEffectStateLoop);
		}
	}

	protected override void OnEndCarry()
	{
		base.OnEndCarry();
		timer = 0f;
		EffectManager.OneShot(kPutEffectName, GetTransform().position, GetTransform().rotation);
		SoundManager.PlayOneShotSE(kPutSEId, GetTransform().position);
		if (fuseEffectCtrl != null)
		{
			fuseEffectCtrl.Play(kFuseEffectStateEnd);
		}
	}

	public override void RequestDestroy()
	{
		if (fuseEffectCtrl != null)
		{
			EffectManager.ReleaseEffect(fuseEffectCtrl.gameObject);
			fuseEffectCtrl = null;
		}
		base.RequestDestroy();
	}

	private void Update()
	{
		if (!base.isCarrying && hasDeploied)
		{
			timer += Time.deltaTime;
		}
		if (timer >= kTimerLimit)
		{
			Explosion();
		}
	}

	protected void Explosion()
	{
		if (base.gameObject.activeSelf && !(ownerPlayer == null))
		{
			AttackInfo attackInfo = ownerPlayer.FindAttackInfo(attackInfoName);
			if (attackInfo != null)
			{
				SoundManager.PlayOneShotSE(kBombSEId);
				AnimEventShot.Create(ownerPlayer, attackInfo, GetTransform().position, GetTransform().rotation);
			}
			RequestDestroy();
		}
	}

	public static string GetAttackInfoName(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return kDefaultAttackInfoName;
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2 && array2[0] == "ai")
			{
				return array2[1];
			}
		}
		return kDefaultAttackInfoName;
	}

	public static string GetFuseEffectNameByModelIndex(int index)
	{
		return string.Format(kFuseEffectNameFormat, index + 1);
	}
}
