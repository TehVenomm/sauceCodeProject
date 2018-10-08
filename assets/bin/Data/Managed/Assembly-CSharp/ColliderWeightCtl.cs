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

	private void Awake()
	{
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
		if ((Object)collider != (Object)null)
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
		if (!((Object)collider == (Object)null) && !((Object)animator == (Object)null))
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
		Vector3 center = diffCenter * currentWeight + startCenter;
		switch (colliderType)
		{
		case COLLIDER_TYPE.BOX:
		{
			Vector3 size = diffSize * currentWeight + startSize;
			BoxCollider boxCollider = collider as BoxCollider;
			boxCollider.center = center;
			boxCollider.size = size;
			break;
		}
		case COLLIDER_TYPE.CAPSULE:
		{
			float radius2 = diffRadius * currentWeight + startRadius;
			float height = diffHeight * currentWeight + startHeight;
			CapsuleCollider capsuleCollider = collider as CapsuleCollider;
			capsuleCollider.center = center;
			capsuleCollider.radius = radius2;
			capsuleCollider.height = height;
			break;
		}
		case COLLIDER_TYPE.SPHERE:
		{
			float radius = diffRadius * currentWeight + startRadius;
			SphereCollider sphereCollider = collider as SphereCollider;
			sphereCollider.center = center;
			sphereCollider.radius = radius;
			break;
		}
		}
	}
}
