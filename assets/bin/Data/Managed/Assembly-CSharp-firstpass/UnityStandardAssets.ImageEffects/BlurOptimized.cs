using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Blur/Blur (Optimized)")]
	[ExecuteInEditMode]
	public class BlurOptimized : PostEffectsBase
	{
		public enum BlurType
		{
			StandardGauss,
			SgxGauss
		}

		[Range(0f, 2f)]
		public int downsample = 1;

		[Range(0f, 10f)]
		public float blurSize = 3f;

		[Range(1f, 4f)]
		public int blurIterations = 2;

		public BlurType blurType;

		public Shader blurShader;

		private Material blurMaterial;

		public override bool CheckResources()
		{
			CheckSupport(false);
			blurMaterial = CheckShaderAndCreateMaterial(blurShader, blurMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		public void OnDisable()
		{
			if (Object.op_Implicit(blurMaterial))
			{
				Object.DestroyImmediate(blurMaterial);
			}
		}

		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Expected O, but got Unknown
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0114: Expected O, but got Unknown
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0142: Unknown result type (might be due to invalid IL or missing references)
			//IL_0147: Expected O, but got Unknown
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				float num = 1f / (1f * (float)(1 << downsample));
				blurMaterial.SetVector("_Parameter", new Vector4(blurSize * num, (0f - blurSize) * num, 0f, 0f));
				source.set_filterMode(1);
				int num2 = source.get_width() >> downsample;
				int num3 = source.get_height() >> downsample;
				RenderTexture val = RenderTexture.GetTemporary(num2, num3, 0, source.get_format());
				val.set_filterMode(1);
				Graphics.Blit(source, val, blurMaterial, 0);
				int num4 = (blurType != 0) ? 2 : 0;
				for (int i = 0; i < blurIterations; i++)
				{
					float num5 = (float)i * 1f;
					blurMaterial.SetVector("_Parameter", new Vector4(blurSize * num + num5, (0f - blurSize) * num - num5, 0f, 0f));
					RenderTexture val2 = RenderTexture.GetTemporary(num2, num3, 0, source.get_format());
					val2.set_filterMode(1);
					Graphics.Blit(val, val2, blurMaterial, 1 + num4);
					RenderTexture.ReleaseTemporary(val);
					val = val2;
					val2 = RenderTexture.GetTemporary(num2, num3, 0, source.get_format());
					val2.set_filterMode(1);
					Graphics.Blit(val, val2, blurMaterial, 2 + num4);
					RenderTexture.ReleaseTemporary(val);
					val = val2;
				}
				Graphics.Blit(val, destination);
				RenderTexture.ReleaseTemporary(val);
			}
		}
	}
}
