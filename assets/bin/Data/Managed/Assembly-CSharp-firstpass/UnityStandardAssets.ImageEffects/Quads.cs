using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	internal class Quads
	{
		private static Mesh[] meshes;

		private static int currentQuads;

		private static bool HasMeshes()
		{
			if (meshes == null)
			{
				return false;
			}
			Mesh[] array = meshes;
			foreach (Mesh val in array)
			{
				if (null == val)
				{
					return false;
				}
			}
			return true;
		}

		public static void Cleanup()
		{
			if (meshes == null)
			{
				return;
			}
			for (int i = 0; i < meshes.Length; i++)
			{
				if (null != meshes[i])
				{
					Object.DestroyImmediate(meshes[i]);
					meshes[i] = null;
				}
			}
			meshes = null;
		}

		public static Mesh[] GetMeshes(int totalWidth, int totalHeight)
		{
			if (HasMeshes() && currentQuads == totalWidth * totalHeight)
			{
				return meshes;
			}
			int num = 10833;
			int num2 = currentQuads = totalWidth * totalHeight;
			int num3 = Mathf.CeilToInt(1f * (float)num2 / (1f * (float)num));
			meshes = (Mesh[])new Mesh[num3];
			int num4 = 0;
			int num5 = 0;
			for (num4 = 0; num4 < num2; num4 += num)
			{
				int triCount = Mathf.FloorToInt((float)Mathf.Clamp(num2 - num4, 0, num));
				meshes[num5] = GetMesh(triCount, num4, totalWidth, totalHeight);
				num5++;
			}
			return meshes;
		}

		private static Mesh GetMesh(int triCount, int triOffset, int totalWidth, int totalHeight)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Expected O, but got Unknown
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0123: Unknown result type (might be due to invalid IL or missing references)
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_0157: Unknown result type (might be due to invalid IL or missing references)
			//IL_015c: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0174: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_018c: Unknown result type (might be due to invalid IL or missing references)
			//IL_019f: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
			Mesh val = new Mesh();
			val.set_hideFlags(52);
			Vector3[] array = (Vector3[])new Vector3[triCount * 4];
			Vector2[] array2 = (Vector2[])new Vector2[triCount * 4];
			Vector2[] array3 = (Vector2[])new Vector2[triCount * 4];
			int[] array4 = new int[triCount * 6];
			for (int i = 0; i < triCount; i++)
			{
				int num = i * 4;
				int num2 = i * 6;
				int num3 = triOffset + i;
				float num4 = Mathf.Floor((float)(num3 % totalWidth)) / (float)totalWidth;
				float num5 = Mathf.Floor((float)(num3 / totalWidth)) / (float)totalHeight;
				Vector3 val2 = default(Vector3);
				val2._002Ector(num4 * 2f - 1f, num5 * 2f - 1f, 1f);
				array[num] = val2;
				array[num + 1] = val2;
				array[num + 2] = val2;
				array[num + 3] = val2;
				array2[num] = new Vector2(0f, 0f);
				array2[num + 1] = new Vector2(1f, 0f);
				array2[num + 2] = new Vector2(0f, 1f);
				array2[num + 3] = new Vector2(1f, 1f);
				array3[num] = new Vector2(num4, num5);
				array3[num + 1] = new Vector2(num4, num5);
				array3[num + 2] = new Vector2(num4, num5);
				array3[num + 3] = new Vector2(num4, num5);
				array4[num2] = num;
				array4[num2 + 1] = num + 1;
				array4[num2 + 2] = num + 2;
				array4[num2 + 3] = num + 1;
				array4[num2 + 4] = num + 2;
				array4[num2 + 5] = num + 3;
			}
			val.set_vertices(array);
			val.set_triangles(array4);
			val.set_uv(array2);
			val.set_uv2(array3);
			return val;
		}
	}
}
