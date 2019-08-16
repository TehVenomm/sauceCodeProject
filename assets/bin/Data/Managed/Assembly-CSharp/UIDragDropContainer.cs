using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag and Drop Container")]
public class UIDragDropContainer : MonoBehaviour
{
	public Transform reparentTarget;

	public UIDragDropContainer()
		: this()
	{
	}

	protected virtual void Start()
	{
		if (reparentTarget == null)
		{
			reparentTarget = this.get_transform();
		}
	}
}
