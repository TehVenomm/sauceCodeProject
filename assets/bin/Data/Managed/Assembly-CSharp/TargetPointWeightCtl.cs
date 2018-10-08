using UnityEngine;

public class TargetPointWeightCtl
{
	public TargetPoint targetPoint;

	public int layerIndex = 1;

	public Vector3 startOffset;

	public Vector3 endOffset;

	public float startMarkerZShift;

	public float endMarkerZShift;

	public Vector3 startBleedOffsetPos = Vector3.get_zero();

	public Vector3 endBleedOffsetPos = Vector3.get_zero();

	public Vector3 startBleedOffsetRot = Vector3.get_zero();

	public Vector3 endBleedOffsetRot = Vector3.get_zero();

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

	private Vector3 diffBleedOffsetPos = Vector3.get_zero();

	private Vector3 diffBleedOffsetRot = Vector3.get_zero();

	private float diffAimMarkerPointRate = 1f;

	private float diffWeight;

	private Animator animator;

	public TargetPointWeightCtl()
		: this()
	{
	}//IL_0008: Unknown result type (might be due to invalid IL or missing references)
	//IL_000d: Unknown result type (might be due to invalid IL or missing references)
	//IL_0013: Unknown result type (might be due to invalid IL or missing references)
	//IL_0018: Unknown result type (might be due to invalid IL or missing references)
	//IL_001e: Unknown result type (might be due to invalid IL or missing references)
	//IL_0023: Unknown result type (might be due to invalid IL or missing references)
	//IL_0029: Unknown result type (might be due to invalid IL or missing references)
	//IL_002e: Unknown result type (might be due to invalid IL or missing references)
	//IL_0074: Unknown result type (might be due to invalid IL or missing references)
	//IL_0079: Unknown result type (might be due to invalid IL or missing references)
	//IL_007f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0084: Unknown result type (might be due to invalid IL or missing references)


	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
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
		if (targetPoint != null)
		{
			calc();
		}
	}

	private float GetAnimatorLayerWeight()
	{
		if (animator == null)
		{
			return 0f;
		}
		return animator.GetLayerWeight(layerIndex);
	}

	private void Update()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetPoint == null) && !(animator == null))
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
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
