using UnityEngine;

public class InstantiateObject
{
	public GameObject prefab;

	public InstantiateObject()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (prefab != null)
		{
			Transform val = this.get_transform();
			Transform val2 = ResourceUtility.Realizes(prefab, val.get_parent(), this.get_gameObject().get_layer());
			val2.set_localPosition(val.get_localPosition());
			val2.set_localRotation(val.get_localRotation());
			val2.set_localScale(val.get_localScale());
			Object.DestroyImmediate(this.get_gameObject());
		}
	}
}
