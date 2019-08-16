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
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (!disable)
		{
			Remove(camera);
			if (!Object.op_Implicit(mesh))
			{
				mesh = CreateMesh();
			}
			if (!Object.op_Implicit(material))
			{
				material = CreateMaterial();
			}
			Transform val = CreateObject(mesh, material);
			val.set_parent(camera.get_transform());
			val.set_localPosition(Vector3.get_forward() * (camera.get_nearClipPlane() + camera.get_farClipPlane()) * 0.5f);
			val.set_localScale(Vector3.get_one());
			val.get_gameObject().set_layer(GetEnabledLayer(camera));
		}
	}

	public static void Remove(Camera camera)
	{
		Transform val = camera.get_transform().Find(temporaryObjectName);
		if (Object.op_Implicit(val))
		{
			Object.Destroy(val.get_gameObject());
		}
	}

	private static int GetEnabledLayer(Camera camera)
	{
		int num = camera.get_cullingMask();
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
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		GameObject val = new GameObject(temporaryObjectName);
		MeshFilter val2 = val.AddComponent<MeshFilter>();
		val2.set_sharedMesh(mesh);
		MeshRenderer val3 = val.AddComponent<MeshRenderer>();
		val3.set_sharedMaterial(material);
		return val.get_transform();
	}

	private static Mesh CreateMesh()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Mesh val = new Mesh();
		val.set_name("n6cw_mesh");
		val.set_vertices((Vector3[])new Vector3[3]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(0f, 0f, 0.001f),
			new Vector3(0f, 0.001f, 0f)
		});
		val.set_triangles(new int[3]
		{
			0,
			1,
			2
		});
		return val;
	}

	private static Material CreateMaterial()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Material val = new Material(ResourceUtility.FindShader("mobile/Custom/tex_color"));
		val.set_name("n6cw_mat");
		val.set_mainTexture(Resources.Load("Texture/White") as Texture);
		val.set_color(Color.get_clear());
		return val;
	}
}
