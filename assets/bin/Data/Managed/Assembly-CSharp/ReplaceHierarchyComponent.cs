using UnityEngine;

public class ReplaceHierarchyComponent : MonoBehaviour
{
	[Tooltip("階層を入れ替えるオブジェクト")]
	public GameObject targetObject;

	public void Awake()
	{
		if (!((Object)targetObject == (Object)null))
		{
			targetObject.transform.parent = base.transform.parent;
			targetObject.transform.localPosition = base.transform.localPosition;
			targetObject.transform.localScale = base.transform.localScale;
			targetObject.transform.localRotation = base.transform.localRotation;
			Object.Destroy(base.gameObject);
		}
	}
}
