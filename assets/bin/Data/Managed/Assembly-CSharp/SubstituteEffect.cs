using UnityEngine;

public class SubstituteEffect
{
	private int index;

	private Player owner;

	private SubstituteEffect lerpTarget;

	private Transform effectTransform;

	private InGameSettingsManager.BuffParamInfo info;

	public bool IsEnable()
	{
		return effectTransform != null;
	}

	public Transform GetEffectTransform()
	{
		return effectTransform;
	}

	public void Initialize(Transform parentTrans, int i, Player p, SubstituteEffect se, InGameSettingsManager.BuffParamInfo bpi)
	{
		index = i;
		owner = p;
		lerpTarget = se;
		info = bpi;
		Create(parentTrans);
	}

	public void Create(Transform parentTrans)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (!(effectTransform != null))
		{
			effectTransform = EffectManager.GetEffect("ef_btl_sk_magi_shikigami_01_02", parentTrans);
			effectTransform.set_position(GetTargetPosition(isLerp: false));
		}
	}

	public void End()
	{
		if (!(effectTransform == null))
		{
			EffectManager.ReleaseEffect(ref effectTransform);
		}
	}

	public void Update(bool isLerp = true)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (!(effectTransform == null))
		{
			effectTransform.set_position(GetTargetPosition(isLerp));
		}
	}

	private Vector3 GetTargetPosition(bool isLerp)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		if (info == null)
		{
			return Vector3.get_zero();
		}
		Vector3 val = (lerpTarget != null) ? (lerpTarget.effectTransform.get_position() - owner._forward * info.substituteOffset2) : (owner._transform.get_position() - owner._forward * info.substituteOffset1);
		val.y = info.substituteHeight;
		if (isLerp)
		{
			return Vector3.Lerp(effectTransform.get_position(), val, info.substituteLerpSpeed * Time.get_deltaTime());
		}
		return val;
	}
}
