using UnityEngine;

public class UIBreakableSphere : MonoBehaviour
{
	[SerializeField]
	private float _breakRate;

	private Material _mat;

	[SerializeField]
	private Mesh _sourceMesh;

	private Mesh newMesh;

	public float breakRate
	{
		get
		{
			return _breakRate;
		}
		set
		{
			_breakRate = value;
			mat.SetFloat("_BreakRate", _breakRate);
		}
	}

	private Material mat
	{
		get
		{
			if (_mat == null)
			{
				MeshRenderer component = GetComponent<MeshRenderer>();
				_mat = new Material(component.sharedMaterial);
				component.material = _mat;
			}
			return _mat;
		}
	}

	private void OnDestroy()
	{
		Object.Destroy(_mat);
		Object.Destroy(newMesh);
	}

	private void Awake()
	{
		breakRate = 1f;
		MeshFilter component = GetComponent<MeshFilter>();
		int[] indices = _sourceMesh.GetIndices(0);
		Mesh mesh = new Mesh();
		Vector3[] array = new Vector3[indices.Length];
		Vector2[] array2 = new Vector2[indices.Length];
		Vector2[] array3 = new Vector2[indices.Length];
		Color[] array4 = new Color[indices.Length];
		int[] array5 = new int[indices.Length];
		for (int i = 0; i < indices.Length; i += 3)
		{
			int num = indices[i];
			int num2 = indices[i + 1];
			int num3 = indices[i + 2];
			Vector3 vector = _sourceMesh.vertices[num];
			Vector3 vector2 = _sourceMesh.vertices[num2];
			Vector3 vector3 = _sourceMesh.vertices[num3];
			Vector3 vector4 = (vector + vector2 + vector3) / 3f;
			array5[i] = i;
			array5[i + 1] = i + 1;
			array5[i + 2] = i + 2;
			array[i] = vector;
			array[i + 1] = vector2;
			array[i + 2] = vector3;
			array2[i] = new Vector2(vector4.x, vector4.y);
			array2[i + 1] = new Vector2(vector4.x, vector4.y);
			array2[i + 2] = new Vector2(vector4.x, vector4.y);
			array3[i] = new Vector2(vector4.z, 0f);
			array3[i + 1] = new Vector2(vector4.z, 0f);
			array3[i + 2] = new Vector2(vector4.z, 0f);
			array4[i] = new Color(_sourceMesh.colors[num].a, _sourceMesh.uv[num].x, _sourceMesh.uv[num].y, 0f);
			array4[i + 1] = new Color(_sourceMesh.colors[num].a, _sourceMesh.uv[num2].x, _sourceMesh.uv[num2].y, 0f);
			array4[i + 2] = new Color(_sourceMesh.colors[num].a, _sourceMesh.uv[num3].x, _sourceMesh.uv[num3].y, 0f);
		}
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.uv2 = array3;
		mesh.colors = array4;
		mesh.SetIndices(array5, MeshTopology.Triangles, 0);
		component.mesh = mesh;
		newMesh = mesh;
	}
}
