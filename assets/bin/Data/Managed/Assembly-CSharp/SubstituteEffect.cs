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
		if (!(effectTransform != null))
		{
			effectTransform = EffectManager.GetEffect("ef_btl_sk_magi_shikigami_01_02", parentTrans);
			effectTransform.position = GetTargetPosition(isLerp: false);
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
		if (!(effectTransform == null))
		{
			effectTransform.position = GetTargetPosition(isLerp);
		}
	}

	private Vector3 GetTargetPosition(bool isLerp)
	{
		if (info == null)
		{
			return Vector3.zero;
		}
		Vector3 vector = (lerpTarget != null) ? (lerpTarget.effectTransform.position - owner._forward * info.substituteOffset2) : (owner._transform.position - owner._forward * info.substituteOffset1);
		vector.y = info.substituteHeight;
		if (isLerp)
		{
			return Vector3.Lerp(effectTransform.position, vector, info.substituteLerpSpeed * Time.deltaTime);
		}
		return vector;
	}
}
