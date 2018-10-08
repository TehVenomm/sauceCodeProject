using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[RequireComponent(typeof(Camera))]
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Bloom and Glow/Bloom (Optimized)")]
	public class BloomOptimized : PostEffectsBase
	{
		public enum Resolution
		{
			Low,
			High
		}

		public enum BlurType
		{
			Standard,
			Sgx
		}

		[Range(0f, 1.5f)]
		public float threshold = 0.25f;

		[Range(0f, 2.5f)]
		public float intensity = 0.75f;

		[Range(0.25f, 5.5f)]
		public float blurSize = 1f;

		private Resolution resolution;

		[Range(1f, 4f)]
		public int blurIterations = 1;

		public BlurType blurType;

		public Shader fastBloomShader;

		private Material fastBloomMaterial;

		public override bool CheckResources()
		{
			CheckSupport(false);
			fastBloomMaterial = CheckShaderAndCreateMaterial(fastBloomShader, fastBloomMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnDisable()
		{
			if (Object.op_Implicit(fastBloomMaterial))
			{
				Object.DestroyImmediate(fastBloomMaterial);
			}
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Expected O, but got Unknown
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Unknown result type (might be due to invalid IL or missing references)
			//IL_010c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0111: Expected O, but got Unknown
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0142: Unknown result type (might be due to invalid IL or missing references)
			//IL_0147: Expected O, but got Unknown
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				int num = (resolution != 0) ? 2 : 4;
				float num2 = (resolution != 0) ? 1f : 0.5f;
				fastBloomMaterial.SetVector("_Parameter", new Vector4(blurSize * num2, 0f, threshold, intensity));
				source.set_filterMode(1);
				int num3 = source.get_width() / num;
				int num4 = source.get_height() / num;
				RenderTexture val = RenderTexture.GetTemporary(num3, num4, 0, source.get_format());
				val.set_filterMode(1);
				Graphics.Blit(source, val, fastBloomMaterial, 1);
				int num5 = (blurType != 0) ? 2 : 0;
				for (int i = 0; i < blurIterations; i++)
				{
					fastBloomMaterial.SetVector("_Parameter", new Vector4(blurSize * num2 + (float)i * 1f, 0f, threshold, intensity));
					RenderTexture val2 = RenderTexture.GetTemporary(num3, num4, 0, source.get_format());
					val2.set_filterMode(1);
					Graphics.Blit(val, val2, fastBloomMaterial, 2 + num5);
					RenderTexture.ReleaseTemporary(val);
					val = val2;
					val2 = RenderTexture.GetTemporary(num3, num4, 0, source.get_format());
					val2.set_filterMode(1);
					Graphics.Blit(val, val2, fastBloomMaterial, 3 + num5);
					RenderTexture.ReleaseTemporary(val);
					val = val2;
				}
				fastBloomMaterial.SetTexture("_Bloom", val);
				Graphics.Blit(source, destination, fastBloomMaterial, 0);
				RenderTexture.ReleaseTemporary(val);
			}
		}
	}
}
