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
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			if (_mat == null)
			{
				MeshRenderer component = this.GetComponent<MeshRenderer>();
				_mat = new Material(component.get_sharedMaterial());
				component.set_material(_mat);
			}
			return _mat;
		}
	}

	public UIBreakableSphere()
		: this()
	{
	}

	private void OnDestroy()
	{
		Object.Destroy(_mat);
		Object.Destroy(newMesh);
	}

	private void Awake()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		breakRate = 1f;
		MeshFilter component = this.GetComponent<MeshFilter>();
		int[] indices = _sourceMesh.GetIndices(0);
		Mesh val = new Mesh();
		Vector3[] array = (Vector3[])new Vector3[indices.Length];
		Vector2[] array2 = (Vector2[])new Vector2[indices.Length];
		Vector2[] array3 = (Vector2[])new Vector2[indices.Length];
		Color[] array4 = (Color[])new Color[indices.Length];
		int[] array5 = new int[indices.Length];
		for (int i = 0; i < indices.Length; i += 3)
		{
			int num = indices[i];
			int num2 = indices[i + 1];
			int num3 = indices[i + 2];
			Vector3 val2 = _sourceMesh.get_vertices()[num];
			Vector3 val3 = _sourceMesh.get_vertices()[num2];
			Vector3 val4 = _sourceMesh.get_vertices()[num3];
			Vector3 val5 = (val2 + val3 + val4) / 3f;
			array5[i] = i;
			array5[i + 1] = i + 1;
			array5[i + 2] = i + 2;
			array[i] = val2;
			array[i + 1] = val3;
			array[i + 2] = val4;
			array2[i] = new Vector2(val5.x, val5.y);
			array2[i + 1] = new Vector2(val5.x, val5.y);
			array2[i + 2] = new Vector2(val5.x, val5.y);
			array3[i] = new Vector2(val5.z, 0f);
			array3[i + 1] = new Vector2(val5.z, 0f);
			array3[i + 2] = new Vector2(val5.z, 0f);
			array4[i] = new Color(_sourceMesh.get_colors()[num].a, _sourceMesh.get_uv()[num].x, _sourceMesh.get_uv()[num].y, 0f);
			array4[i + 1] = new Color(_sourceMesh.get_colors()[num].a, _sourceMesh.get_uv()[num2].x, _sourceMesh.get_uv()[num2].y, 0f);
			array4[i + 2] = new Color(_sourceMesh.get_colors()[num].a, _sourceMesh.get_uv()[num3].x, _sourceMesh.get_uv()[num3].y, 0f);
		}
		val.set_vertices(array);
		val.set_uv(array2);
		val.set_uv2(array3);
		val.set_colors(array4);
		val.SetIndices(array5, 0, 0);
		component.set_mesh(val);
		newMesh = val;
	}
}
