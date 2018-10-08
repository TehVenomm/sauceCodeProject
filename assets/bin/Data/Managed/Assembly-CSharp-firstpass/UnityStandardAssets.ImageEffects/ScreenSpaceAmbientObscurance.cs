using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[AddComponentMenu("Image Effects/Rendering/Screen Space Ambient Obscurance")]
	[RequireComponent(typeof(Camera))]
	[ExecuteInEditMode]
	internal class ScreenSpaceAmbientObscurance : PostEffectsBase
	{
		[Range(0f, 3f)]
		public float intensity = 0.5f;

		[Range(0.1f, 3f)]
		public float radius = 0.2f;

		[Range(0f, 3f)]
		public int blurIterations = 1;

		[Range(0f, 5f)]
		public float blurFilterDistance = 1.25f;

		[Range(0f, 1f)]
		public int downsample;

		public Texture2D rand;

		public Shader aoShader;

		private Material aoMaterial;

		public override bool CheckResources()
		{
			CheckSupport(true);
			aoMaterial = CheckShaderAndCreateMaterial(aoShader, aoMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnDisable()
		{
			if (Object.op_Implicit(aoMaterial))
			{
				Object.DestroyImmediate(aoMaterial);
			}
			aoMaterial = null;
		}

		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_0148: Expected O, but got Unknown
			//IL_0168: Unknown result type (might be due to invalid IL or missing references)
			//IL_016d: Expected O, but got Unknown
			//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Expected O, but got Unknown
			//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0203: Expected O, but got Unknown
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				Matrix4x4 projectionMatrix = this.GetComponent<Camera>().get_projectionMatrix();
				Matrix4x4 inverse = projectionMatrix.get_inverse();
				Vector4 val = default(Vector4);
				val._002Ector(-2f / ((float)Screen.get_width() * projectionMatrix.get_Item(0)), -2f / ((float)Screen.get_height() * projectionMatrix.get_Item(5)), (1f - projectionMatrix.get_Item(2)) / projectionMatrix.get_Item(0), (1f + projectionMatrix.get_Item(6)) / projectionMatrix.get_Item(5));
				aoMaterial.SetVector("_ProjInfo", val);
				aoMaterial.SetMatrix("_ProjectionInv", inverse);
				aoMaterial.SetTexture("_Rand", rand);
				aoMaterial.SetFloat("_Radius", radius);
				aoMaterial.SetFloat("_Radius2", radius * radius);
				aoMaterial.SetFloat("_Intensity", intensity);
				aoMaterial.SetFloat("_BlurFilterDistance", blurFilterDistance);
				int width = source.get_width();
				int height = source.get_height();
				RenderTexture val2 = RenderTexture.GetTemporary(width >> downsample, height >> downsample);
				Graphics.Blit(source, val2, aoMaterial, 0);
				if (downsample > 0)
				{
					RenderTexture val3 = RenderTexture.GetTemporary(width, height);
					Graphics.Blit(val2, val3, aoMaterial, 4);
					RenderTexture.ReleaseTemporary(val2);
					val2 = val3;
				}
				for (int i = 0; i < blurIterations; i++)
				{
					aoMaterial.SetVector("_Axis", Vector4.op_Implicit(new Vector2(1f, 0f)));
					RenderTexture val3 = RenderTexture.GetTemporary(width, height);
					Graphics.Blit(val2, val3, aoMaterial, 1);
					RenderTexture.ReleaseTemporary(val2);
					aoMaterial.SetVector("_Axis", Vector4.op_Implicit(new Vector2(0f, 1f)));
					val2 = RenderTexture.GetTemporary(width, height);
					Graphics.Blit(val3, val2, aoMaterial, 1);
					RenderTexture.ReleaseTemporary(val3);
				}
				aoMaterial.SetTexture("_AOTex", val2);
				Graphics.Blit(source, destination, aoMaterial, 2);
				RenderTexture.ReleaseTemporary(val2);
			}
		}
	}
}
