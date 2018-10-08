using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Edge Detection/Crease Shading")]
	public class CreaseShading : PostEffectsBase
	{
		public float intensity = 0.5f;

		public int softness = 1;

		public float spread = 1f;

		public Shader blurShader;

		private Material blurMaterial;

		public Shader depthFetchShader;

		private Material depthFetchMaterial;

		public Shader creaseApplyShader;

		private Material creaseApplyMaterial;

		public override bool CheckResources()
		{
			CheckSupport(true);
			blurMaterial = CheckShaderAndCreateMaterial(blurShader, blurMaterial);
			depthFetchMaterial = CheckShaderAndCreateMaterial(depthFetchShader, depthFetchMaterial);
			creaseApplyMaterial = CheckShaderAndCreateMaterial(creaseApplyShader, creaseApplyMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Expected O, but got Unknown
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Expected O, but got Unknown
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d0: Expected O, but got Unknown
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				int width = source.get_width();
				int height = source.get_height();
				float num = 1f * (float)width / (1f * (float)height);
				float num2 = 0.001953125f;
				RenderTexture val = RenderTexture.GetTemporary(width, height, 0);
				RenderTexture val2 = RenderTexture.GetTemporary(width / 2, height / 2, 0);
				Graphics.Blit(source, val, depthFetchMaterial);
				Graphics.Blit(val, val2);
				for (int i = 0; i < softness; i++)
				{
					RenderTexture val3 = RenderTexture.GetTemporary(width / 2, height / 2, 0);
					blurMaterial.SetVector("offsets", new Vector4(0f, spread * num2, 0f, 0f));
					Graphics.Blit(val2, val3, blurMaterial);
					RenderTexture.ReleaseTemporary(val2);
					val2 = val3;
					val3 = RenderTexture.GetTemporary(width / 2, height / 2, 0);
					blurMaterial.SetVector("offsets", new Vector4(spread * num2 / num, 0f, 0f, 0f));
					Graphics.Blit(val2, val3, blurMaterial);
					RenderTexture.ReleaseTemporary(val2);
					val2 = val3;
				}
				creaseApplyMaterial.SetTexture("_HrDepthTex", val);
				creaseApplyMaterial.SetTexture("_LrDepthTex", val2);
				creaseApplyMaterial.SetFloat("intensity", intensity);
				Graphics.Blit(source, destination, creaseApplyMaterial);
				RenderTexture.ReleaseTemporary(val);
				RenderTexture.ReleaseTemporary(val2);
			}
		}
	}
}
