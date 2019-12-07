using UnityEngine;

public static class Nexus6CrashWorkaround
{
	private static readonly string temporaryObjectName = "n6cw";

	private static Mesh mesh;

	private static Material material;

	public static bool disable
	{
		get;
		set;
	}

	public static void Apply(Camera camera)
	{
		if (!disable)
		{
			Remove(camera);
			if (!mesh)
			{
				mesh = CreateMesh();
			}
			if (!material)
			{
				material = CreateMaterial();
			}
			Transform transform = CreateObject(mesh, material);
			transform.parent = camera.transform;
			transform.localPosition = Vector3.forward * (camera.nearClipPlane + camera.farClipPlane) * 0.5f;
			transform.localScale = Vector3.one;
			transform.gameObject.layer = GetEnabledLayer(camera);
		}
	}

	public static void Remove(Camera camera)
	{
		Transform transform = camera.transform.Find(temporaryObjectName);
		if ((bool)transform)
		{
			Object.Destroy(transform.gameObject);
		}
	}

	private static int GetEnabledLayer(Camera camera)
	{
		int num = camera.cullingMask;
		int num2 = 0;
		for (int i = 0; i < 32; i++)
		{
			if ((num & 1) != 0)
			{
				return num2;
			}
			num >>= 1;
			num2++;
		}
		return 0;
	}

	private static Transform CreateObject(Mesh mesh, Material material)
	{
		GameObject gameObject = new GameObject(temporaryObjectName);
		gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
		gameObject.AddComponent<MeshRenderer>().sharedMaterial = material;
		return gameObject.transform;
	}

	private static Mesh CreateMesh()
	{
		Mesh mesh = new Mesh();
		mesh.name = "n6cw_mesh";
		mesh.vertices = new Vector3[3]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(0f, 0f, 0.001f),
			new Vector3(0f, 0.001f, 0f)
		};
		mesh.triangles = new int[3]
		{
			0,
			1,
			2
		};
		return mesh;
	}

	private static Material CreateMaterial()
	{
		return new Material(ResourceUtility.FindShader("mobile/Custom/tex_color"))
		{
			name = "n6cw_mat",
			mainTexture = (Resources.Load("Texture/White") as Texture),
			color = Color.clear
		};
	}
}
