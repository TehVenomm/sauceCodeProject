using UnityEngine;

public class TargetPoint : MonoBehaviour
{
	public class Param
	{
		public bool isShowRange;

		public Vector3 markerPos = Vector3.zero;

		public Quaternion markerRot = Quaternion.identity;

		public Vector3 targetPos = Vector3.zero;

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

	[Tooltip("isAimEnableはfalseだけどArrowSpWeak表示したい")]
	public bool isDispArrowSpWeak;

	[Tooltip("継続ダメ\u30fcジエフェクトオフセット")]
	public Vector3 bleedOffsetPos = Vector3.zero;

	[Tooltip("継続ダメ\u30fcジエフェクト回転オフセット")]
	public Vector3 bleedOffsetRot = Vector3.zero;

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
	{
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
		_transform = base.transform;
	}

	private void Start()
	{
		scaledCalc();
	}

	public void scaledCalc()
	{
		scaledOffset = offset.Mul(_transform.lossyScale);
		float num = markerZShift;
		Vector3 lossyScale = _transform.lossyScale;
		scaledMarkerZShift = num * lossyScale.z;
	}

	public bool IsEneble()
	{
		Enemy enemy = owner as Enemy;
		if ((Object)enemy == (Object)null || enemy.isDead || !enemy.enableTargetPoint)
		{
			return false;
		}
		return true;
	}

	public Vector3 GetTargetPoint()
	{
		return _transform.position + _transform.rotation * scaledOffset;
	}

	public Transform PlayArrowBleedEffect(string effect_name, int show_index)
	{
		Transform effect = EffectManager.GetEffect(effect_name, _transform);
		if ((Object)effect != (Object)null)
		{
			effect.localScale = Vector3.one.Div(_transform.lossyScale);
			effect.localPosition = GetArrowBleedLocalPosition();
			effect.localRotation = GetArrowBleedLocalRotation(show_index);
			effectTransform = effect;
			effectOrgLossyScele = effectTransform.lossyScale;
			effectShowIndex = show_index;
		}
		return effect;
	}

	public Transform PlayArrowBurstEffect(string effect_name, Transform trs)
	{
		Transform effect = EffectManager.GetEffect(effect_name, null);
		if ((Object)effect != (Object)null)
		{
			effect.localScale = Vector3.one.Div(_transform.lossyScale);
			if ((Object)null != (Object)trs)
			{
				effect.position = trs.position;
			}
			else
			{
				effect.position = _transform.position;
			}
			effect.rotation = Quaternion.identity;
		}
		return effect;
	}

	public Vector3 GetArrowBleedLocalPosition()
	{
		return bleedOffsetPos;
	}

	public Quaternion GetArrowBleedLocalRotation(int show_index = 0)
	{
		Quaternion lhs = Quaternion.identity;
		if (offset != Vector3.zero)
		{
			lhs = Quaternion.LookRotation(-offset);
		}
		lhs *= Quaternion.Euler(bleedOffsetRot.x, bleedOffsetRot.y, bleedOffsetRot.z);
		if (show_index > 0 && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			InGameSettingsManager.Player.SpecialActionInfo.ArrowBleedOther arrowBleedOther = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedOther;
			if (arrowBleedOther != null)
			{
				Quaternion rhs = Quaternion.AngleAxis((float)(show_index - 1) * 120f + arrowBleedOther.axisRandomAngle * Random.value - arrowBleedOther.axisRandomAngle * 0.5f, Vector3.forward);
				Quaternion rhs2 = Quaternion.Euler(arrowBleedOther.openFixAngle + arrowBleedOther.openRandomAngle * Random.value, 0f, 0f);
				lhs = lhs * rhs * rhs2;
			}
		}
		return lhs;
	}

	public void ArrowBleedEffectUpdate(bool isPostion, bool isRotation, bool isOrgScele)
	{
		if (!((Object)effectTransform == (Object)null))
		{
			if (isOrgScele)
			{
				Vector3 lossyScale = effectTransform.lossyScale;
				Vector3 localScale = effectTransform.localScale;
				effectTransform.localScale = new Vector3(localScale.x / lossyScale.x * effectOrgLossyScele.x, localScale.y / lossyScale.y * effectOrgLossyScele.y, localScale.z / lossyScale.z * effectOrgLossyScele.z);
			}
			if (isPostion)
			{
				effectTransform.localPosition = GetArrowBleedLocalPosition();
			}
			if (isRotation)
			{
				effectTransform.localRotation = GetArrowBleedLocalRotation(effectShowIndex);
			}
		}
	}
}
