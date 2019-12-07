using UnityEngine;

public class TestColliderCross : MonoBehaviour
{
	public GameObject checkObject;

	public GameObject moveObject;

	private void Start()
	{
	}

	private void Update()
	{
		Collider component = GetComponent<Collider>();
		if (component != null && checkObject != null)
		{
			Vector3 vector = Utility.ClosestPointOnCollider(component, checkObject.transform.position);
			Debug.Log("############### : " + vector);
			if (moveObject != null)
			{
				moveObject.transform.position = vector;
			}
		}
	}
}
