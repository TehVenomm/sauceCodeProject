using UnityEngine;

public class RootMotionProxy : MonoBehaviour
{
	private Animator animator;

	private Transform parentTransfom;

	private Rigidbody parentRigidbody;

	public RootMotionProxy()
		: this()
	{
	}

	private void Start()
	{
		animator = this.get_gameObject().GetComponent<Animator>();
		parentTransfom = this.get_transform().get_parent();
		if (parentTransfom != null)
		{
			parentRigidbody = parentTransfom.get_gameObject().GetComponent<Rigidbody>();
		}
	}

	private void OnAnimatorMove()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		if (parentTransfom == null || animator == null)
		{
			return;
		}
		if (!animator.get_applyRootMotion())
		{
			if (parentRigidbody != null)
			{
				parentRigidbody.set_velocity(Vector3.get_zero());
			}
		}
		else if (parentRigidbody != null && !parentRigidbody.get_isKinematic())
		{
			if (Time.get_deltaTime() > 0f)
			{
				parentRigidbody.set_velocity(animator.get_deltaPosition() / Time.get_deltaTime());
			}
			else
			{
				parentRigidbody.set_velocity(Vector3.get_zero());
			}
		}
		else
		{
			parentTransfom.set_localPosition(parentTransfom.get_localPosition() + animator.get_deltaPosition());
			parentTransfom.set_localRotation(parentTransfom.get_localRotation() * animator.get_deltaRotation());
		}
	}
}
