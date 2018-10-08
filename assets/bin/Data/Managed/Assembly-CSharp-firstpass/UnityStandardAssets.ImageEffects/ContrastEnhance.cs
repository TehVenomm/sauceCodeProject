using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Color Adjustments/Contrast Enhance (Unsharp Mask)")]
	[ExecuteInEditMode]
	public class ContrastEnhance : PostEffectsBase
	{
		[Range(0f, 1f)]
		public float intensity = 0.5f;

		[Range(0f, 0.999f)]
		public float threshold;

		private Material separableBlurMaterial;

		private Material contrastCompositeMaterial;

		[Range(0f, 1f)]
		public float blurSpread = 1f;

		public Shader separableBlurShader;

		public Shader contrastCompositeShader;

		public override bool CheckResources()
		{
			CheckSupport(false);
			contrastCompositeMaterial = CheckShaderAndCreateMaterial(contrastCompositeShader, contrastCompositeMaterial);
			separableBlurMaterial = CheckShaderAndCreateMaterial(separableBlurShader, separableBlurMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Expected O, but got Unknown
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Expected O, but got Unknown
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Expected O, but got Unknown
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				int width = source.get_width();
				int height = source.get_height();
				RenderTexture val = RenderTexture.GetTemporary(width / 2, height / 2, 0);
				Graphics.Blit(source, val);
				RenderTexture val2 = RenderTexture.GetTemporary(width / 4, height / 4, 0);
				Graphics.Blit(val, val2);
				RenderTexture.ReleaseTemporary(val);
				separableBlurMaterial.SetVector("offsets", new Vector4(0f, blurSpread * 1f / (float)val2.get_height(), 0f, 0f));
				RenderTexture val3 = RenderTexture.GetTemporary(width / 4, height / 4, 0);
				Graphics.Blit(val2, val3, separableBlurMaterial);
				RenderTexture.ReleaseTemporary(val2);
				separableBlurMaterial.SetVector("offsets", new Vector4(blurSpread * 1f / (float)val2.get_width(), 0f, 0f, 0f));
				val2 = RenderTexture.GetTemporary(width / 4, height / 4, 0);
				Graphics.Blit(val3, val2, separableBlurMaterial);
				RenderTexture.ReleaseTemporary(val3);
				contrastCompositeMaterial.SetTexture("_MainTexBlurred", val2);
				contrastCompositeMaterial.SetFloat("intensity", intensity);
				contrastCompositeMaterial.SetFloat("threshold", threshold);
				Graphics.Blit(source, destination, contrastCompositeMaterial);
				RenderTexture.ReleaseTemporary(val2);
			}
		}
	}
}
