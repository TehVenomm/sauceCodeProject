using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[AddComponentMenu("Image Effects/Rendering/Sun Shafts")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class SunShafts : PostEffectsBase
	{
		public enum SunShaftsResolution
		{
			Low,
			Normal,
			High
		}

		public enum ShaftsScreenBlendMode
		{
			Screen,
			Add
		}

		public SunShaftsResolution resolution = SunShaftsResolution.Normal;

		public ShaftsScreenBlendMode screenBlendMode;

		public Transform sunTransform;

		public int radialBlurIterations = 2;

		public Color sunColor = Color.get_white();

		public Color sunThreshold = new Color(0.87f, 0.74f, 0.65f);

		public float sunShaftBlurRadius = 2.5f;

		public float sunShaftIntensity = 1.15f;

		public float maxRadius = 0.75f;

		public bool useDepthTexture = true;

		public Shader sunShaftsShader;

		private Material sunShaftsMaterial;

		public Shader simpleClearShader;

		private Material simpleClearMaterial;

		public override bool CheckResources()
		{
			CheckSupport(useDepthTexture);
			sunShaftsMaterial = CheckShaderAndCreateMaterial(sunShaftsShader, sunShaftsMaterial);
			simpleClearMaterial = CheckShaderAndCreateMaterial(simpleClearShader, simpleClearMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Expected O, but got Unknown
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0132: Unknown result type (might be due to invalid IL or missing references)
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_0164: Unknown result type (might be due to invalid IL or missing references)
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_0175: Unknown result type (might be due to invalid IL or missing references)
			//IL_017a: Expected O, but got Unknown
			//IL_0213: Unknown result type (might be due to invalid IL or missing references)
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_0258: Unknown result type (might be due to invalid IL or missing references)
			//IL_025d: Expected O, but got Unknown
			//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c5: Expected O, but got Unknown
			//IL_031b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0380: Unknown result type (might be due to invalid IL or missing references)
			//IL_038b: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				if (useDepthTexture)
				{
					Camera component = this.GetComponent<Camera>();
					component.set_depthTextureMode(component.get_depthTextureMode() | 1);
				}
				int num = 4;
				if (resolution == SunShaftsResolution.Normal)
				{
					num = 2;
				}
				else if (resolution == SunShaftsResolution.High)
				{
					num = 1;
				}
				Vector3 val = Vector3.get_one() * 0.5f;
				if (Object.op_Implicit(sunTransform))
				{
					val = this.GetComponent<Camera>().WorldToViewportPoint(sunTransform.get_position());
				}
				else
				{
					val._002Ector(0.5f, 0.5f, 0f);
				}
				int num2 = source.get_width() / num;
				int num3 = source.get_height() / num;
				RenderTexture val2 = RenderTexture.GetTemporary(num2, num3, 0);
				sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(1f, 1f, 0f, 0f) * sunShaftBlurRadius);
				sunShaftsMaterial.SetVector("_SunPosition", new Vector4(val.x, val.y, val.z, maxRadius));
				sunShaftsMaterial.SetVector("_SunThreshold", Color.op_Implicit(sunThreshold));
				if (!useDepthTexture)
				{
					RenderTextureFormat val3 = (!this.GetComponent<Camera>().get_hdr()) ? 7 : 9;
					RenderTexture val4 = RenderTexture.GetTemporary(source.get_width(), source.get_height(), 0, val3);
					RenderTexture.set_active(val4);
					GL.ClearWithSkybox(false, this.GetComponent<Camera>());
					sunShaftsMaterial.SetTexture("_Skybox", val4);
					Graphics.Blit(source, val2, sunShaftsMaterial, 3);
					RenderTexture.ReleaseTemporary(val4);
				}
				else
				{
					Graphics.Blit(source, val2, sunShaftsMaterial, 2);
				}
				DrawBorder(val2, simpleClearMaterial);
				radialBlurIterations = Mathf.Clamp(radialBlurIterations, 1, 4);
				float num4 = sunShaftBlurRadius * 0.00130208337f;
				sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num4, num4, 0f, 0f));
				sunShaftsMaterial.SetVector("_SunPosition", new Vector4(val.x, val.y, val.z, maxRadius));
				for (int i = 0; i < radialBlurIterations; i++)
				{
					RenderTexture val5 = RenderTexture.GetTemporary(num2, num3, 0);
					Graphics.Blit(val2, val5, sunShaftsMaterial, 1);
					RenderTexture.ReleaseTemporary(val2);
					num4 = sunShaftBlurRadius * (((float)i * 2f + 1f) * 6f) / 768f;
					sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num4, num4, 0f, 0f));
					val2 = RenderTexture.GetTemporary(num2, num3, 0);
					Graphics.Blit(val5, val2, sunShaftsMaterial, 1);
					RenderTexture.ReleaseTemporary(val5);
					num4 = sunShaftBlurRadius * (((float)i * 2f + 2f) * 6f) / 768f;
					sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num4, num4, 0f, 0f));
				}
				if (val.z >= 0f)
				{
					sunShaftsMaterial.SetVector("_SunColor", new Vector4(sunColor.r, sunColor.g, sunColor.b, sunColor.a) * sunShaftIntensity);
				}
				else
				{
					sunShaftsMaterial.SetVector("_SunColor", Vector4.get_zero());
				}
				sunShaftsMaterial.SetTexture("_ColorBuffer", val2);
				Graphics.Blit(source, destination, sunShaftsMaterial, (screenBlendMode != 0) ? 4 : 0);
				RenderTexture.ReleaseTemporary(val2);
			}
		}
	}
}
