using UnityEngine;

public class RootMotionProxy : MonoBehaviour
{
	private Animator animator;

	private Transform parentTransfom;

	private Rigidbody parentRigidbody;

	private void Start()
	{
		animator = base.gameObject.GetComponent<Animator>();
		parentTransfom = base.transform.parent;
		if (parentTransfom != null)
		{
			parentRigidbody = parentTransfom.gameObject.GetComponent<Rigidbody>();
		}
	}

	private void OnAnimatorMove()
	{
		if (parentTransfom == null || animator == null)
		{
			return;
		}
		if (!animator.applyRootMotion)
		{
			if (parentRigidbody != null)
			{
				parentRigidbody.velocity = Vector3.zero;
			}
		}
		else if (parentRigidbody != null && !parentRigidbody.isKinematic)
		{
			if (Time.deltaTime > 0f)
			{
				parentRigidbody.velocity = animator.deltaPosition / Time.deltaTime;
			}
			else
			{
				parentRigidbody.velocity = Vector3.zero;
			}
		}
		else
		{
			parentTransfom.localPosition += animator.deltaPosition;
			parentTransfom.localRotation *= animator.deltaRotation;
		}
	}
}
