using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[AddComponentMenu("Image Effects/Camera/Vignette and Chromatic Aberration")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class VignetteAndChromaticAberration : PostEffectsBase
	{
		public enum AberrationMode
		{
			Simple,
			Advanced
		}

		public AberrationMode mode;

		public float intensity = 0.036f;

		public float chromaticAberration = 0.2f;

		public float axialAberration = 0.5f;

		public float blur;

		public float blurSpread = 0.75f;

		public float luminanceDependency = 0.25f;

		public float blurDistance = 2.5f;

		public Shader vignetteShader;

		public Shader separableBlurShader;

		public Shader chromAberrationShader;

		private Material m_VignetteMaterial;

		private Material m_SeparableBlurMaterial;

		private Material m_ChromAberrationMaterial;

		public override bool CheckResources()
		{
			CheckSupport(false);
			m_VignetteMaterial = CheckShaderAndCreateMaterial(vignetteShader, m_VignetteMaterial);
			m_SeparableBlurMaterial = CheckShaderAndCreateMaterial(separableBlurShader, m_SeparableBlurMaterial);
			m_ChromAberrationMaterial = CheckShaderAndCreateMaterial(chromAberrationShader, m_ChromAberrationMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Expected O, but got Unknown
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Expected O, but got Unknown
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Expected O, but got Unknown
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			//IL_014e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0153: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Expected O, but got Unknown
			//IL_0233: Unknown result type (might be due to invalid IL or missing references)
			//IL_0238: Unknown result type (might be due to invalid IL or missing references)
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				int width = source.get_width();
				int height = source.get_height();
				bool flag = Mathf.Abs(blur) > 0f || Mathf.Abs(intensity) > 0f;
				float num = 1f * (float)width / (1f * (float)height);
				RenderTexture val = null;
				RenderTexture val2 = null;
				if (flag)
				{
					val = RenderTexture.GetTemporary(width, height, 0, source.get_format());
					if (Mathf.Abs(blur) > 0f)
					{
						val2 = RenderTexture.GetTemporary(width / 2, height / 2, 0, source.get_format());
						Graphics.Blit(source, val2, m_ChromAberrationMaterial, 0);
						for (int i = 0; i < 2; i++)
						{
							m_SeparableBlurMaterial.SetVector("offsets", new Vector4(0f, blurSpread * 0.001953125f, 0f, 0f));
							RenderTexture val3 = RenderTexture.GetTemporary(width / 2, height / 2, 0, source.get_format());
							Graphics.Blit(val2, val3, m_SeparableBlurMaterial);
							RenderTexture.ReleaseTemporary(val2);
							m_SeparableBlurMaterial.SetVector("offsets", new Vector4(blurSpread * 0.001953125f / num, 0f, 0f, 0f));
							val2 = RenderTexture.GetTemporary(width / 2, height / 2, 0, source.get_format());
							Graphics.Blit(val3, val2, m_SeparableBlurMaterial);
							RenderTexture.ReleaseTemporary(val3);
						}
					}
					m_VignetteMaterial.SetFloat("_Intensity", 1f / (1f - intensity) - 1f);
					m_VignetteMaterial.SetFloat("_Blur", 1f / (1f - blur) - 1f);
					m_VignetteMaterial.SetTexture("_VignetteTex", val2);
					Graphics.Blit(source, val, m_VignetteMaterial, 0);
				}
				m_ChromAberrationMaterial.SetFloat("_ChromaticAberration", chromaticAberration);
				m_ChromAberrationMaterial.SetFloat("_AxialAberration", axialAberration);
				m_ChromAberrationMaterial.SetVector("_BlurDistance", Vector4.op_Implicit(new Vector2(0f - blurDistance, blurDistance)));
				m_ChromAberrationMaterial.SetFloat("_Luminance", 1f / Mathf.Max(Mathf.Epsilon, luminanceDependency));
				if (flag)
				{
					val.set_wrapMode(1);
				}
				else
				{
					source.set_wrapMode(1);
				}
				Graphics.Blit((!flag) ? source : val, destination, m_ChromAberrationMaterial, (mode != AberrationMode.Advanced) ? 1 : 2);
				RenderTexture.ReleaseTemporary(val);
				RenderTexture.ReleaseTemporary(val2);
			}
		}
	}
}
