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
		if ((Object)parentTransfom != (Object)null)
		{
			parentRigidbody = parentTransfom.gameObject.GetComponent<Rigidbody>();
		}
	}

	private void OnAnimatorMove()
	{
		if (!((Object)parentTransfom == (Object)null) && !((Object)animator == (Object)null))
		{
			if (!animator.applyRootMotion)
			{
				if ((Object)parentRigidbody != (Object)null)
				{
					parentRigidbody.velocity = Vector3.zero;
				}
			}
			else if ((Object)parentRigidbody != (Object)null && !parentRigidbody.isKinematic)
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
}
