using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag and Drop Root")]
public class UIDragDropRoot : MonoBehaviour
{
	public static Transform root;

	public UIDragDropRoot()
		: this()
	{
	}

	private void OnEnable()
	{
		root = this.get_transform();
	}

	private void OnDisable()
	{
		if (root == this.get_transform())
		{
			root = null;
		}
	}
}
