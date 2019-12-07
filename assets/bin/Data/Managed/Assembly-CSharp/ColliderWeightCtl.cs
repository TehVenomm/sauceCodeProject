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
		Vector3 center = diffCenter * currentWeight + startCenter;
		switch (colliderType)
		{
		case COLLIDER_TYPE.BOX:
		{
			Vector3 size = diffSize * currentWeight + startSize;
			BoxCollider obj3 = collider as BoxCollider;
			obj3.center = center;
			obj3.size = size;
			break;
		}
		case COLLIDER_TYPE.CAPSULE:
		{
			float radius2 = diffRadius * currentWeight + startRadius;
			float height = diffHeight * currentWeight + startHeight;
			CapsuleCollider obj2 = collider as CapsuleCollider;
			obj2.center = center;
			obj2.radius = radius2;
			obj2.height = height;
			break;
		}
		case COLLIDER_TYPE.SPHERE:
		{
			float radius = diffRadius * currentWeight + startRadius;
			SphereCollider obj = collider as SphereCollider;
			obj.center = center;
			obj.radius = radius;
			break;
		}
		}
	}
}
