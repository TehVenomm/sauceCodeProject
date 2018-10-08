using UnityEngine;

[AddComponentMenu("NGUI/Internal/Snapshot Point")]
[ExecuteInEditMode]
public class UISnapshotPoint
{
	public bool isOrthographic = true;

	public float nearClip = -100f;

	public float farClip = 100f;

	[Range(10f, 80f)]
	public int fieldOfView = 35;

	public float orthoSize = 30f;

	public Texture2D thumbnail;

	public UISnapshotPoint()
		: this()
	{
	}

	private void Start()
	{
		if (this.get_tag() != "EditorOnly")
		{
			this.set_tag("EditorOnly");
		}
	}
}
