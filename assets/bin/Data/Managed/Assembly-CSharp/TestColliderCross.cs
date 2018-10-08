using UnityEngine;

public class TestColliderCross
{
	public GameObject checkObject;

	public GameObject moveObject;

	public TestColliderCross()
		: this()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Collider component = this.GetComponent<Collider>();
		if (component != null && checkObject != null)
		{
			Vector3 val = Utility.ClosestPointOnCollider(component, checkObject.get_transform().get_position());
			Debug.Log((object)("############### : " + val));
			if (moveObject != null)
			{
				moveObject.get_transform().set_position(val);
			}
		}
	}
}
