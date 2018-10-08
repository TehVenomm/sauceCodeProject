using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AlphaClear
{
	[SerializeField]
	private Mesh quad;

	private Matrix4x4 matrix;

	private Material alphaClearMaterial;

	public AlphaClear()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		matrix = Matrix4x4.get_identity();
		matrix.SetTRS(Vector3.get_zero(), Quaternion.AngleAxis(90f, Vector3.get_right()), new Vector3(100f, 100f, 1f));
		alphaClearMaterial = new Material(ResourceUtility.FindShader("Custom/AlphaClear"));
	}

	private void OnPostRender()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		alphaClearMaterial.SetPass(0);
		Graphics.DrawMeshNow(quad, matrix, 0);
	}
}
