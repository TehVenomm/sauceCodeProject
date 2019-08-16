using UnityEngine;

public class FieldCarriableDecoyGimmickObject : FieldCarriableGimmickObject
{
	public static readonly string kDecoyEffectName = "ef_btl_trap_02_01";

	public static readonly string kBreakEffectName = "ef_btl_trap_02_02";

	public static readonly string kPutEffectName = "ef_btl_trap_01_02";

	public static readonly int kPutSEId = 10000058;

	public static readonly int kBreakSEId = 20000022;

	private static readonly int kShiftIndex = 1000;

	private static readonly float kRenderOffSet = 0.5f;

	public float activeTime;

	public float maxActiveTime = 10f;

	private Material gaugeMat;

	private DecoyBulletObject targetObjectForEnemy;

	private Transform decoyEffect;

	public bool isActive => !base.isCarrying && hasDeploied && activeTime > 0f;

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(pointData);
		activeTime = maxActiveTime;
		targetObjectForEnemy = this.get_gameObject().AddComponent<DecoyBulletObject>();
		if (targetObjectForEnemy != null)
		{
			targetObjectForEnemy.Initialize(-1, -1, null, m_transform.get_position(), null, isHit: false);
		}
		targetObjectForEnemy.SetCarriable(this);
		Transform val = this.get_transform().Find("CMN_decoytrap01");
		if (val != null)
		{
			val = val.get_transform().Find("object01");
			Renderer component = val.GetComponent<Renderer>();
			if (component != null)
			{
				gaugeMat = component.get_materials()[1];
				UpdateGauge();
			}
		}
	}

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
			if (array2 != null && array2.Length == 2)
			{
				string text = array2[0];
				if (text != null && text == "t")
				{
					maxActiveTime = float.Parse(array2[1]);
				}
			}
		}
	}

	protected override void OnStartCarry(Player owner)
	{
		base.OnStartCarry(owner);
		if (decoyEffect != null)
		{
			EffectManager.ReleaseEffect(decoyEffect.get_gameObject());
			decoyEffect = null;
		}
		MonoBehaviourSingleton<StageObjectManager>.I.CheckAllEnemiesMissDecoy(targetObjectForEnemy);
	}

	protected override void OnEndCarry()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		base.OnEndCarry();
		if (decoyEffect == null)
		{
			decoyEffect = EffectManager.GetEffect(kDecoyEffectName, GetTransform());
		}
		MonoBehaviourSingleton<StageObjectManager>.I.SetAllEnemiesTargetDecoy();
		EffectManager.OneShot(kPutEffectName, GetTransform().get_position(), GetTransform().get_rotation());
		SoundManager.PlayOneShotSE(kPutSEId, GetTransform().get_position());
	}

	private void LateUpdate()
	{
		if (isActive)
		{
			activeTime = Mathf.Max(0f, activeTime - Time.get_deltaTime());
			UpdateGauge();
			if (activeTime <= 0f)
			{
				OnActiveEnd();
			}
		}
	}

	private void UpdateGauge()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		float num = 0f - activeTime / maxActiveTime;
		if (gaugeMat != null)
		{
			num += kRenderOffSet;
			gaugeMat.SetTextureOffset("_MainTex", new Vector2(0f, num));
		}
	}

	protected virtual void OnActiveEnd()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(false);
		EffectManager.OneShot(kBreakEffectName, GetTransform().get_position(), GetTransform().get_rotation());
		SoundManager.PlayOneShotSE(kBreakSEId, GetTransform().get_position());
		if (decoyEffect != null)
		{
			EffectManager.ReleaseEffect(decoyEffect.get_gameObject());
			decoyEffect = null;
		}
	}
}
