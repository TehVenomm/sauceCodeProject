using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag and Drop Container")]
public class UIDragDropContainer
{
	public Transform reparentTarget;

	public UIDragDropContainer()
		: this()
	{
	}

	protected virtual void Start()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		if (reparentTarget == null)
		{
			reparentTarget = this.get_transform();
		}
	}
}
