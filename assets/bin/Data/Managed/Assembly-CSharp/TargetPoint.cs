using UnityEngine;

public class TargetPoint
{
	public class Param
	{
		public bool isShowRange;

		public Vector3 markerPos = Vector3.get_zero();

		public Quaternion markerRot = Quaternion.get_identity();

		public Vector3 targetPos = Vector3.get_zero();

		public float vecSqrMagnitude;

		public float targetSelectCounter;

		public bool isTargetEnable;

		public float aimMarkerScale = 1f;

		public Enemy.WEAK_STATE weakState;

		public int weakSubParam = -1;

		public Enemy.WEAK_STATE prevWeakState;

		public int prevWeakSubParam = -1;

		public bool playSignEffect;

		public int validElementType = -1;
	}

	[Tooltip("位置のオフセット")]
	public Vector3 offset;

	[Tooltip("マ\u30fcカ\u30fc表示位置のZオフセット。マイナス値で手前に移動")]
	public float markerZShift;

	[Tooltip("部位ID")]
	public int regionID = -1;

	[Tooltip("通常タ\u30fcゲット有効")]
	public bool isTargetEnable = true;

	[Tooltip("弓の狙い時有効")]
	public bool isAimEnable = true;

	[Tooltip("継続ダメ\u30fcジエフェクトオフセット")]
	public Vector3 bleedOffsetPos = Vector3.get_zero();

	[Tooltip("継続ダメ\u30fcジエフェクト回転オフセット")]
	public Vector3 bleedOffsetRot = Vector3.get_zero();

	[Tooltip("狙いマ\u30fcカ\u30fcのポイントサイズ")]
	public float aimMarkerPointRate = 1f;

	[Tooltip("タ\u30fcゲット選定時のウェイト")]
	public float weight;

	[Tooltip("ヒット判定のDot計算をスキップ")]
	public bool isSkipDotCalc;

	private bool m_isForceDisplay;

	private Transform effectTransform;

	private Vector3 effectOrgLossyScele = new Vector3(1f, 1f, 1f);

	private int effectShowIndex;

	public Transform _transform
	{
		get;
		private set;
	}

	public Vector3 scaledOffset
	{
		get;
		private set;
	}

	public float scaledMarkerZShift
	{
		get;
		private set;
	}

	public StageObject owner
	{
		get;
		set;
	}

	public RegionRoot subRegionRoot
	{
		get;
		set;
	}

	public bool IsForceDisplay => m_isForceDisplay;

	public Param param
	{
		get;
		set;
	}

	public TargetPoint()
		: this()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		param = new Param();
		owner = null;
		subRegionRoot = null;
	}

	public void ForceDisplay()
	{
		m_isForceDisplay = true;
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		_transform = this.get_transform();
	}

	private void Start()
	{
		scaledCalc();
	}

	public void scaledCalc()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		scaledOffset = offset.Mul(_transform.get_lossyScale());
		float num = markerZShift;
		Vector3 lossyScale = _transform.get_lossyScale();
		scaledMarkerZShift = num * lossyScale.z;
	}

	public bool IsEneble()
	{
		Enemy enemy = owner as Enemy;
		if (enemy == null || enemy.isDead || !enemy.enableTargetPoint)
		{
			return false;
		}
		return true;
	}

	public Vector3 GetTargetPoint()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		return _transform.get_position() + _transform.get_rotation() * scaledOffset;
	}

	public Transform PlayArrowBleedEffect(string effect_name, int show_index)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		Transform effect = EffectManager.GetEffect(effect_name, _transform);
		if (effect != null)
		{
			effect.set_localScale(Vector3.get_one().Div(_transform.get_lossyScale()));
			effect.set_localPosition(GetArrowBleedLocalPosition());
			effect.set_localRotation(GetArrowBleedLocalRotation(show_index));
			effectTransform = effect;
			effectOrgLossyScele = effectTransform.get_lossyScale();
			effectShowIndex = show_index;
		}
		return effect;
	}

	public Transform PlayArrowBurstEffect(string effect_name, Transform trs)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		Transform effect = EffectManager.GetEffect(effect_name, null);
		if (effect != null)
		{
			effect.set_localScale(Vector3.get_one().Div(_transform.get_lossyScale()));
			if (null != trs)
			{
				effect.set_position(trs.get_position());
			}
			else
			{
				effect.set_position(_transform.get_position());
			}
			effect.set_rotation(Quaternion.get_identity());
		}
		return effect;
	}

	public Vector3 GetArrowBleedLocalPosition()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return bleedOffsetPos;
	}

	public Quaternion GetArrowBleedLocalRotation(int show_index = 0)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		Quaternion val = Quaternion.get_identity();
		if (offset != Vector3.get_zero())
		{
			val = Quaternion.LookRotation(-offset);
		}
		val *= Quaternion.Euler(bleedOffsetRot.x, bleedOffsetRot.y, bleedOffsetRot.z);
		if (show_index > 0 && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			InGameSettingsManager.Player.SpecialActionInfo.ArrowBleedOther arrowBleedOther = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedOther;
			if (arrowBleedOther != null)
			{
				Quaternion val2 = Quaternion.AngleAxis((float)(show_index - 1) * 120f + arrowBleedOther.axisRandomAngle * Random.get_value() - arrowBleedOther.axisRandomAngle * 0.5f, Vector3.get_forward());
				Quaternion val3 = Quaternion.Euler(arrowBleedOther.openFixAngle + arrowBleedOther.openRandomAngle * Random.get_value(), 0f, 0f);
				val = val * val2 * val3;
			}
		}
		return val;
	}

	public void ArrowBleedEffectUpdate(bool isPostion, bool isRotation, bool isOrgScele)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		if (!(effectTransform == null))
		{
			if (isOrgScele)
			{
				Vector3 lossyScale = effectTransform.get_lossyScale();
				Vector3 localScale = effectTransform.get_localScale();
				effectTransform.set_localScale(new Vector3(localScale.x / lossyScale.x * effectOrgLossyScele.x, localScale.y / lossyScale.y * effectOrgLossyScele.y, localScale.z / lossyScale.z * effectOrgLossyScele.z));
			}
			if (isPostion)
			{
				effectTransform.set_localPosition(GetArrowBleedLocalPosition());
			}
			if (isRotation)
			{
				effectTransform.set_localRotation(GetArrowBleedLocalRotation(effectShowIndex));
			}
		}
	}
}
