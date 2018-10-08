using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag and Drop Container")]
public class UIDragDropContainer : MonoBehaviour
{
	public Transform reparentTarget;

	protected virtual void Start()
	{
		if ((Object)reparentTarget == (Object)null)
		{
			reparentTarget = base.transform;
		}
	}
}
