using UnityEngine;

public class ReplaceHierarchyComponent : MonoBehaviour
{
	[Tooltip("階層を入れ替えるオブジェクト")]
	public GameObject targetObject;

	public ReplaceHierarchyComponent()
		: this()
	{
	}

	public void Awake()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (!(targetObject == null))
		{
			targetObject.get_transform().set_parent(this.get_transform().get_parent());
			targetObject.get_transform().set_localPosition(this.get_transform().get_localPosition());
			targetObject.get_transform().set_localScale(this.get_transform().get_localScale());
			targetObject.get_transform().set_localRotation(this.get_transform().get_localRotation());
			Object.Destroy(this.get_gameObject());
		}
	}
}
