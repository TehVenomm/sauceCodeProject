using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AlphaClear : MonoBehaviour
{
	[SerializeField]
	private Mesh quad;

	private Matrix4x4 matrix;

	private Material alphaClearMaterial;

	private void Awake()
	{
		matrix = Matrix4x4.identity;
		matrix.SetTRS(Vector3.zero, Quaternion.AngleAxis(90f, Vector3.right), new Vector3(100f, 100f, 1f));
		alphaClearMaterial = new Material(ResourceUtility.FindShader("Custom/AlphaClear"));
	}

	private void OnPostRender()
	{
		alphaClearMaterial.SetPass(0);
		Graphics.DrawMeshNow(quad, matrix, 0);
	}
}
