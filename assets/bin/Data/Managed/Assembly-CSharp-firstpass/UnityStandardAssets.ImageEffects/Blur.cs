using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Blur/Blur")]
	public class Blur : MonoBehaviour
	{
		[Range(0f, 10f)]
		public int iterations = 3;

		[Range(0f, 1f)]
		public float blurSpread = 0.6f;

		public Shader blurShader;

		private static Material m_Material;

		protected Material material
		{
			get
			{
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Expected O, but got Unknown
				if (m_Material == null)
				{
					m_Material = new Material(blurShader);
					m_Material.set_hideFlags(52);
				}
				return m_Material;
			}
		}

		public Blur()
			: this()
		{
		}

		protected void OnDisable()
		{
			if (Object.op_Implicit(m_Material))
			{
				Object.DestroyImmediate(m_Material);
			}
		}

		protected void Start()
		{
			if (!SystemInfo.get_supportsImageEffects())
			{
				this.set_enabled(false);
			}
			else if (!Object.op_Implicit(blurShader) || !material.get_shader().get_isSupported())
			{
				this.set_enabled(false);
			}
		}

		public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			float num = 0.5f + (float)iteration * blurSpread;
			Graphics.BlitMultiTap(source, dest, material, (Vector2[])new Vector2[4]
			{
				new Vector2(0f - num, 0f - num),
				new Vector2(0f - num, num),
				new Vector2(num, num),
				new Vector2(num, 0f - num)
			});
		}

		private void DownSample4x(RenderTexture source, RenderTexture dest)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			float num = 1f;
			Graphics.BlitMultiTap(source, dest, material, (Vector2[])new Vector2[4]
			{
				new Vector2(0f - num, 0f - num),
				new Vector2(0f - num, num),
				new Vector2(num, num),
				new Vector2(num, 0f - num)
			});
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			int num = source.get_width() / 4;
			int num2 = source.get_height() / 4;
			RenderTexture val = RenderTexture.GetTemporary(num, num2, 0);
			DownSample4x(source, val);
			for (int i = 0; i < iterations; i++)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0);
				FourTapCone(val, temporary, i);
				RenderTexture.ReleaseTemporary(val);
				val = temporary;
			}
			Graphics.Blit(val, destination);
			RenderTexture.ReleaseTemporary(val);
		}
	}
}
