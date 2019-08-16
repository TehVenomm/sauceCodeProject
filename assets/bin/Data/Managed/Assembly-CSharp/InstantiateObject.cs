using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
	public GameObject prefab;

	public InstantiateObject()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (prefab != null)
		{
			Transform transform = this.get_transform();
			Transform val = ResourceUtility.Realizes(prefab, transform.get_parent(), this.get_gameObject().get_layer());
			val.set_localPosition(transform.get_localPosition());
			val.set_localRotation(transform.get_localRotation());
			val.set_localScale(transform.get_localScale());
			Object.DestroyImmediate(this.get_gameObject());
		}
	}
}
