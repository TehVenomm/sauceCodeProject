using UnityEngine;

public class ColliderWeightCtl : MonoBehaviour
{
	public enum COLLIDER_TYPE
	{
		CAPSULE,
		SPHERE,
		BOX
	}

	public Collider collider;

	public COLLIDER_TYPE colliderType;

	public int layerIndex = 1;

	public Vector3 startCenter;

	public Vector3 endCenter;

	public float startRadius;

	public float endRadius;

	public float startHeight;

	public float endHeight;

	public Vector3 startSize;

	public Vector3 endSize;

	private float currentWeight;

	private Vector3 diffCenter;

	private float diffRadius;

	private float diffHeight;

	private Vector3 diffSize;

	private Animator animator;

	public ColliderWeightCtl()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		diffCenter = endCenter - startCenter;
		diffRadius = endRadius - startRadius;
		diffHeight = endHeight - startHeight;
		diffSize = endSize - startSize;
		currentWeight = 0f;
	}

	public void SetAnimator(Animator _animator)
	{
		animator = _animator;
		currentWeight = animator.GetLayerWeight(layerIndex);
		if (collider != null)
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
		if (!(collider == null) && !(animator == null))
		{
			float layerWeight = animator.GetLayerWeight(layerIndex);
			if (currentWeight != layerWeight)
			{
				currentWeight = layerWeight;
				calc();
			}
		}
	}

	private void calc()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		Vector3 center = diffCenter * currentWeight + startCenter;
		switch (colliderType)
		{
		case COLLIDER_TYPE.BOX:
		{
			Vector3 size = diffSize * currentWeight + startSize;
			BoxCollider val3 = collider as BoxCollider;
			val3.set_center(center);
			val3.set_size(size);
			break;
		}
		case COLLIDER_TYPE.CAPSULE:
		{
			float radius2 = diffRadius * currentWeight + startRadius;
			float height = diffHeight * currentWeight + startHeight;
			CapsuleCollider val2 = collider as CapsuleCollider;
			val2.set_center(center);
			val2.set_radius(radius2);
			val2.set_height(height);
			break;
		}
		case COLLIDER_TYPE.SPHERE:
		{
			float radius = diffRadius * currentWeight + startRadius;
			SphereCollider val = collider as SphereCollider;
			val.set_center(center);
			val.set_radius(radius);
			break;
		}
		}
	}
}
