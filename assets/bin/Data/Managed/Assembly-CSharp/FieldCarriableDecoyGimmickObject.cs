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

	public bool isActive
	{
		get
		{
			if (!base.isCarrying && hasDeploied)
			{
				return activeTime > 0f;
			}
			return false;
		}
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		activeTime = maxActiveTime;
		targetObjectForEnemy = base.gameObject.AddComponent<DecoyBulletObject>();
		if (targetObjectForEnemy != null)
		{
			targetObjectForEnemy.Initialize(-1, -1, null, m_transform.position, null, isHit: false);
		}
		targetObjectForEnemy.SetCarriable(this);
		Transform transform = base.transform.Find("CMN_decoytrap01");
		if (transform != null)
		{
			transform = transform.transform.Find("object01");
			Renderer component = transform.GetComponent<Renderer>();
			if (component != null)
			{
				gaugeMat = component.materials[1];
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
				string a = array2[0];
				if (a == "t")
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
			EffectManager.ReleaseEffect(decoyEffect.gameObject);
			decoyEffect = null;
		}
		MonoBehaviourSingleton<StageObjectManager>.I.CheckAllEnemiesMissDecoy(targetObjectForEnemy);
	}

	protected override void OnEndCarry()
	{
		base.OnEndCarry();
		if (decoyEffect == null)
		{
			decoyEffect = EffectManager.GetEffect(kDecoyEffectName, GetTransform());
		}
		MonoBehaviourSingleton<StageObjectManager>.I.SetAllEnemiesTargetDecoy();
		EffectManager.OneShot(kPutEffectName, GetTransform().position, GetTransform().rotation);
		SoundManager.PlayOneShotSE(kPutSEId, GetTransform().position);
	}

	private void LateUpdate()
	{
		if (isActive)
		{
			activeTime = Mathf.Max(0f, activeTime - Time.deltaTime);
			UpdateGauge();
			if (activeTime <= 0f)
			{
				OnActiveEnd();
			}
		}
	}

	private void UpdateGauge()
	{
		float num = 0f - activeTime / maxActiveTime;
		if (gaugeMat != null)
		{
			num += kRenderOffSet;
			gaugeMat.SetTextureOffset("_MainTex", new Vector2(0f, num));
		}
	}

	protected virtual void OnActiveEnd()
	{
		base.gameObject.SetActive(value: false);
		EffectManager.OneShot(kBreakEffectName, GetTransform().position, GetTransform().rotation);
		SoundManager.PlayOneShotSE(kBreakSEId, GetTransform().position);
		if (decoyEffect != null)
		{
			EffectManager.ReleaseEffect(decoyEffect.gameObject);
			decoyEffect = null;
		}
	}
}
