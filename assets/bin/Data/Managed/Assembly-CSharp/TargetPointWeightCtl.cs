using UnityEngine;

public class TargetPointWeightCtl : MonoBehaviour
{
	public TargetPoint targetPoint;

	public int layerIndex = 1;

	public Vector3 startOffset;

	public Vector3 endOffset;

	public float startMarkerZShift;

	public float endMarkerZShift;

	public Vector3 startBleedOffsetPos = Vector3.zero;

	public Vector3 endBleedOffsetPos = Vector3.zero;

	public Vector3 startBleedOffsetRot = Vector3.zero;

	public Vector3 endBleedOffsetRot = Vector3.zero;

	public float startAimMarkerPointRate = 1f;

	public float endAimMarkerPointRate = 1f;

	public float startWeight;

	public float endWeight;

	public bool isOffsetChange = true;

	public bool isMarkerZShiftChange = true;

	public bool isBleedOffsetPosChange = true;

	public bool isBleedOffsetRotChange = true;

	public bool isAimMarkerPointRateChange = true;

	public bool isWeightChange = true;

	public bool isArrowPosUpdate;

	public bool isArrowRotUpdate;

	public bool isArrowOriginalScele;

	private float currentWeight;

	private Vector3 diffOffset;

	private float diffMarkerZShift;

	private Vector3 diffBleedOffsetPos = Vector3.zero;

	private Vector3 diffBleedOffsetRot = Vector3.zero;

	private float diffAimMarkerPointRate = 1f;

	private float diffWeight;

	private Animator animator;

	private void Awake()
	{
		diffOffset = endOffset - startOffset;
		diffMarkerZShift = endMarkerZShift - startMarkerZShift;
		diffBleedOffsetPos = endBleedOffsetPos - startBleedOffsetPos;
		diffBleedOffsetRot = endBleedOffsetRot - startBleedOffsetRot;
		diffAimMarkerPointRate = endAimMarkerPointRate - startAimMarkerPointRate;
		diffWeight = endWeight - startWeight;
		currentWeight = 0f;
	}

	public void SetAnimator(Animator _animator)
	{
		animator = _animator;
		currentWeight = animator.GetLayerWeight(layerIndex);
		if ((Object)targetPoint != (Object)null)
		{
			calc();
		}
	}

	private float GetAnimatorLayerWeight()
	{
		if ((Object)animator == (Object)null)
		{
			return 0f;
		}
		return animator.GetLayerWeight(layerIndex);
	}

	private void Update()
	{
		if (!((Object)targetPoint == (Object)null) && !((Object)animator == (Object)null))
		{
			float layerWeight = animator.GetLayerWeight(layerIndex);
			if (currentWeight != layerWeight)
			{
				currentWeight = layerWeight;
				if (isOffsetChange)
				{
					Vector3 offset = diffOffset * currentWeight + startOffset;
					targetPoint.offset = offset;
				}
				if (isMarkerZShiftChange)
				{
					float markerZShift = diffMarkerZShift * currentWeight + startMarkerZShift;
					targetPoint.markerZShift = markerZShift;
				}
				if (isBleedOffsetPosChange)
				{
					Vector3 bleedOffsetPos = diffBleedOffsetPos * currentWeight + startBleedOffsetPos;
					targetPoint.bleedOffsetPos = bleedOffsetPos;
				}
				if (isBleedOffsetRotChange)
				{
					Vector3 bleedOffsetRot = diffBleedOffsetRot * currentWeight + startBleedOffsetRot;
					targetPoint.bleedOffsetRot = bleedOffsetRot;
				}
				if (isAimMarkerPointRateChange)
				{
					float aimMarkerPointRate = diffAimMarkerPointRate * currentWeight + startAimMarkerPointRate;
					targetPoint.aimMarkerPointRate = aimMarkerPointRate;
				}
				if (isWeightChange)
				{
					float weight = diffWeight * currentWeight + startWeight;
					targetPoint.weight = weight;
				}
				targetPoint.scaledCalc();
				calc();
			}
		}
	}

	private void calc()
	{
		if (isOffsetChange)
		{
			Vector3 offset = diffOffset * currentWeight + startOffset;
			targetPoint.offset = offset;
		}
		if (isMarkerZShiftChange)
		{
			float markerZShift = diffMarkerZShift * currentWeight + startMarkerZShift;
			targetPoint.markerZShift = markerZShift;
		}
		if (isBleedOffsetPosChange)
		{
			Vector3 bleedOffsetPos = diffBleedOffsetPos * currentWeight + startBleedOffsetPos;
			targetPoint.bleedOffsetPos = bleedOffsetPos;
		}
		if (isBleedOffsetRotChange)
		{
			Vector3 bleedOffsetRot = diffBleedOffsetRot * currentWeight + startBleedOffsetRot;
			targetPoint.bleedOffsetRot = bleedOffsetRot;
		}
		if (isAimMarkerPointRateChange)
		{
			float aimMarkerPointRate = diffAimMarkerPointRate * currentWeight + startAimMarkerPointRate;
			targetPoint.aimMarkerPointRate = aimMarkerPointRate;
		}
		if (isWeightChange)
		{
			float weight = diffWeight * currentWeight + startWeight;
			targetPoint.weight = weight;
		}
		targetPoint.scaledCalc();
		targetPoint.ArrowBleedEffectUpdate(isArrowPosUpdate, isArrowRotUpdate, isArrowOriginalScele);
	}
}
