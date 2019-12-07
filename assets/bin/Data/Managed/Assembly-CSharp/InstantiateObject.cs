using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
	public GameObject prefab;

	private void Awake()
	{
		if (prefab != null)
		{
			Transform transform = base.transform;
			Transform transform2 = ResourceUtility.Realizes(prefab, transform.parent, base.gameObject.layer);
			transform2.localPosition = transform.localPosition;
			transform2.localRotation = transform.localRotation;
			transform2.localScale = transform.localScale;
			Object.DestroyImmediate(base.gameObject);
		}
	}
}
