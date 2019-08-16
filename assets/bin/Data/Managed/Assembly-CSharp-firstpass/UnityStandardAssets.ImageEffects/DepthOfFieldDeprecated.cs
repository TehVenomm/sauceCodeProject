using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Camera/Depth of Field (deprecated)")]
	public class DepthOfFieldDeprecated : PostEffectsBase
	{
		public enum Dof34QualitySetting
		{
			OnlyBackground = 1,
			BackgroundAndForeground
		}

		public enum DofResolution
		{
			High = 2,
			Medium,
			Low
		}

		public enum DofBlurriness
		{
			Low = 1,
			High = 2,
			VeryHigh = 4
		}

		public enum BokehDestination
		{
			Background = 1,
			Foreground,
			BackgroundAndForeground
		}

		private static int SMOOTH_DOWNSAMPLE_PASS = 6;

		private static float BOKEH_EXTRA_BLUR = 2f;

		public Dof34QualitySetting quality = Dof34QualitySetting.OnlyBackground;

		public DofResolution resolution = DofResolution.Low;

		public bool simpleTweakMode = true;

		public float focalPoint = 1f;

		public float smoothness = 0.5f;

		public float focalZDistance;

		public float focalZStartCurve = 1f;

		public float focalZEndCurve = 1f;

		private float focalStartCurve = 2f;

		private float focalEndCurve = 2f;

		private float focalDistance01 = 0.1f;

		public Transform objectFocus;

		public float focalSize;

		public DofBlurriness bluriness = DofBlurriness.High;

		public float maxBlurSpread = 1.75f;

		public float foregroundBlurExtrude = 1.15f;

		public Shader dofBlurShader;

		private Material dofBlurMaterial;

		public Shader dofShader;

		private Material dofMaterial;

		public bool visualize;

		public BokehDestination bokehDestination = BokehDestination.Background;

		private float widthOverHeight = 1.25f;

		private float oneOverBaseSize = 0.001953125f;

		public bool bokeh;

		public bool bokehSupport = true;

		public Shader bokehShader;

		public Texture2D bokehTexture;

		public float bokehScale = 2.4f;

		public float bokehIntensity = 0.15f;

		public float bokehThresholdContrast = 0.1f;

		public float bokehThresholdLuminance = 0.55f;

		public int bokehDownsample = 1;

		private Material bokehMaterial;

		private Camera _camera;

		private RenderTexture foregroundTexture;

		private RenderTexture mediumRezWorkTexture;

		private RenderTexture finalDefocus;

		private RenderTexture lowRezWorkTexture;

		private RenderTexture bokehSource;

		private RenderTexture bokehSource2;

		private void CreateMaterials()
		{
			dofBlurMaterial = CheckShaderAndCreateMaterial(dofBlurShader, dofBlurMaterial);
			dofMaterial = CheckShaderAndCreateMaterial(dofShader, dofMaterial);
			bokehSupport = bokehShader.get_isSupported();
			if (bokeh && bokehSupport && Object.op_Implicit(bokehShader))
			{
				bokehMaterial = CheckShaderAndCreateMaterial(bokehShader, bokehMaterial);
			}
		}

		public override bool CheckResources()
		{
			CheckSupport(needDepth: true);
			dofBlurMaterial = CheckShaderAndCreateMaterial(dofBlurShader, dofBlurMaterial);
			dofMaterial = CheckShaderAndCreateMaterial(dofShader, dofMaterial);
			bokehSupport = bokehShader.get_isSupported();
			if (bokeh && bokehSupport && Object.op_Implicit(bokehShader))
			{
				bokehMaterial = CheckShaderAndCreateMaterial(bokehShader, bokehMaterial);
			}
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnDisable()
		{
			Quads.Cleanup();
		}

		private void OnEnable()
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			_camera = this.GetComponent<Camera>();
			Camera camera = _camera;
			camera.set_depthTextureMode(camera.get_depthTextureMode() | 1);
		}

		private float FocalDistance01(float worldDist)
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			Vector3 val = _camera.WorldToViewportPoint((worldDist - _camera.get_nearClipPlane()) * _camera.get_transform().get_forward() + _camera.get_transform().get_position());
			return val.z / (_camera.get_farClipPlane() - _camera.get_nearClipPlane());
		}

		private int GetDividerBasedOnQuality()
		{
			int result = 1;
			if (resolution == DofResolution.Medium)
			{
				result = 2;
			}
			else if (resolution == DofResolution.Low)
			{
				result = 2;
			}
			return result;
		}

		private int GetLowResolutionDividerBasedOnQuality(int baseDivider)
		{
			int num = baseDivider;
			if (resolution == DofResolution.High)
			{
				num *= 2;
			}
			if (resolution == DofResolution.Low)
			{
				num *= 2;
			}
			return num;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0150: Unknown result type (might be due to invalid IL or missing references)
			//IL_0155: Unknown result type (might be due to invalid IL or missing references)
			//IL_026e: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0349: Unknown result type (might be due to invalid IL or missing references)
			//IL_051e: Unknown result type (might be due to invalid IL or missing references)
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (smoothness < 0.1f)
			{
				smoothness = 0.1f;
			}
			bokeh = (bokeh && bokehSupport);
			float num = (!bokeh) ? 1f : BOKEH_EXTRA_BLUR;
			bool flag = quality > Dof34QualitySetting.OnlyBackground;
			float num2 = focalSize / (_camera.get_farClipPlane() - _camera.get_nearClipPlane());
			if (simpleTweakMode)
			{
				float num3;
				if (Object.op_Implicit(objectFocus))
				{
					Vector3 val = _camera.WorldToViewportPoint(objectFocus.get_position());
					num3 = val.z / _camera.get_farClipPlane();
				}
				else
				{
					num3 = FocalDistance01(focalPoint);
				}
				focalDistance01 = num3;
				focalStartCurve = focalDistance01 * smoothness;
				focalEndCurve = focalStartCurve;
				flag = (flag && focalPoint > _camera.get_nearClipPlane() + Mathf.Epsilon);
			}
			else
			{
				if (Object.op_Implicit(objectFocus))
				{
					Vector3 val2 = _camera.WorldToViewportPoint(objectFocus.get_position());
					val2.z /= _camera.get_farClipPlane();
					focalDistance01 = val2.z;
				}
				else
				{
					focalDistance01 = FocalDistance01(focalZDistance);
				}
				focalStartCurve = focalZStartCurve;
				focalEndCurve = focalZEndCurve;
				flag = (flag && focalPoint > _camera.get_nearClipPlane() + Mathf.Epsilon);
			}
			widthOverHeight = 1f * (float)source.get_width() / (1f * (float)source.get_height());
			oneOverBaseSize = 0.001953125f;
			dofMaterial.SetFloat("_ForegroundBlurExtrude", foregroundBlurExtrude);
			dofMaterial.SetVector("_CurveParams", new Vector4((!simpleTweakMode) ? focalStartCurve : (1f / focalStartCurve), (!simpleTweakMode) ? focalEndCurve : (1f / focalEndCurve), num2 * 0.5f, focalDistance01));
			dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)source.get_width()), 1f / (1f * (float)source.get_height()), 0f, 0f));
			int dividerBasedOnQuality = GetDividerBasedOnQuality();
			int lowResolutionDividerBasedOnQuality = GetLowResolutionDividerBasedOnQuality(dividerBasedOnQuality);
			AllocateTextures(flag, source, dividerBasedOnQuality, lowResolutionDividerBasedOnQuality);
			Graphics.Blit(source, source, dofMaterial, 3);
			Downsample(source, mediumRezWorkTexture);
			Blur(mediumRezWorkTexture, mediumRezWorkTexture, DofBlurriness.Low, 4, maxBlurSpread);
			if (bokeh && (BokehDestination.Foreground & bokehDestination) != 0)
			{
				dofMaterial.SetVector("_Threshhold", new Vector4(bokehThresholdContrast, bokehThresholdLuminance, 0.95f, 0f));
				Graphics.Blit(mediumRezWorkTexture, bokehSource2, dofMaterial, 11);
				Graphics.Blit(mediumRezWorkTexture, lowRezWorkTexture);
				Blur(lowRezWorkTexture, lowRezWorkTexture, bluriness, 0, maxBlurSpread * num);
			}
			else
			{
				Downsample(mediumRezWorkTexture, lowRezWorkTexture);
				Blur(lowRezWorkTexture, lowRezWorkTexture, bluriness, 0, maxBlurSpread);
			}
			dofBlurMaterial.SetTexture("_TapLow", lowRezWorkTexture);
			dofBlurMaterial.SetTexture("_TapMedium", mediumRezWorkTexture);
			Graphics.Blit(null, finalDefocus, dofBlurMaterial, 3);
			if (bokeh && (BokehDestination.Foreground & bokehDestination) != 0)
			{
				AddBokeh(bokehSource2, bokehSource, finalDefocus);
			}
			dofMaterial.SetTexture("_TapLowBackground", finalDefocus);
			dofMaterial.SetTexture("_TapMedium", mediumRezWorkTexture);
			Graphics.Blit(source, (!flag) ? destination : foregroundTexture, dofMaterial, visualize ? 2 : 0);
			if (flag)
			{
				Graphics.Blit(foregroundTexture, source, dofMaterial, 5);
				Downsample(source, mediumRezWorkTexture);
				BlurFg(mediumRezWorkTexture, mediumRezWorkTexture, DofBlurriness.Low, 2, maxBlurSpread);
				if (bokeh && (BokehDestination.Foreground & bokehDestination) != 0)
				{
					dofMaterial.SetVector("_Threshhold", new Vector4(bokehThresholdContrast * 0.5f, bokehThresholdLuminance, 0f, 0f));
					Graphics.Blit(mediumRezWorkTexture, bokehSource2, dofMaterial, 11);
					Graphics.Blit(mediumRezWorkTexture, lowRezWorkTexture);
					BlurFg(lowRezWorkTexture, lowRezWorkTexture, bluriness, 1, maxBlurSpread * num);
				}
				else
				{
					BlurFg(mediumRezWorkTexture, lowRezWorkTexture, bluriness, 1, maxBlurSpread);
				}
				Graphics.Blit(lowRezWorkTexture, finalDefocus);
				dofMaterial.SetTexture("_TapLowForeground", finalDefocus);
				Graphics.Blit(source, destination, dofMaterial, visualize ? 1 : 4);
				if (bokeh && (BokehDestination.Foreground & bokehDestination) != 0)
				{
					AddBokeh(bokehSource2, bokehSource, destination);
				}
			}
			ReleaseTextures();
		}

		private void Blur(RenderTexture from, RenderTexture to, DofBlurriness iterations, int blurPass, float spread)
		{
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0117: Unknown result type (might be due to invalid IL or missing references)
			RenderTexture temporary = RenderTexture.GetTemporary(to.get_width(), to.get_height());
			if (iterations > DofBlurriness.Low)
			{
				BlurHex(from, to, blurPass, spread, temporary);
				if (iterations > DofBlurriness.High)
				{
					dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
					Graphics.Blit(to, temporary, dofBlurMaterial, blurPass);
					dofBlurMaterial.SetVector("offsets", new Vector4(spread / widthOverHeight * oneOverBaseSize, 0f, 0f, 0f));
					Graphics.Blit(temporary, to, dofBlurMaterial, blurPass);
				}
			}
			else
			{
				dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
				Graphics.Blit(from, temporary, dofBlurMaterial, blurPass);
				dofBlurMaterial.SetVector("offsets", new Vector4(spread / widthOverHeight * oneOverBaseSize, 0f, 0f, 0f));
				Graphics.Blit(temporary, to, dofBlurMaterial, blurPass);
			}
			RenderTexture.ReleaseTemporary(temporary);
		}

		private void BlurFg(RenderTexture from, RenderTexture to, DofBlurriness iterations, int blurPass, float spread)
		{
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0128: Unknown result type (might be due to invalid IL or missing references)
			dofBlurMaterial.SetTexture("_TapHigh", from);
			RenderTexture temporary = RenderTexture.GetTemporary(to.get_width(), to.get_height());
			if (iterations > DofBlurriness.Low)
			{
				BlurHex(from, to, blurPass, spread, temporary);
				if (iterations > DofBlurriness.High)
				{
					dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
					Graphics.Blit(to, temporary, dofBlurMaterial, blurPass);
					dofBlurMaterial.SetVector("offsets", new Vector4(spread / widthOverHeight * oneOverBaseSize, 0f, 0f, 0f));
					Graphics.Blit(temporary, to, dofBlurMaterial, blurPass);
				}
			}
			else
			{
				dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
				Graphics.Blit(from, temporary, dofBlurMaterial, blurPass);
				dofBlurMaterial.SetVector("offsets", new Vector4(spread / widthOverHeight * oneOverBaseSize, 0f, 0f, 0f));
				Graphics.Blit(temporary, to, dofBlurMaterial, blurPass);
			}
			RenderTexture.ReleaseTemporary(temporary);
		}

		private void BlurHex(RenderTexture from, RenderTexture to, int blurPass, float spread, RenderTexture tmp)
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			dofBlurMaterial.SetVector("offsets", new Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
			Graphics.Blit(from, tmp, dofBlurMaterial, blurPass);
			dofBlurMaterial.SetVector("offsets", new Vector4(spread / widthOverHeight * oneOverBaseSize, 0f, 0f, 0f));
			Graphics.Blit(tmp, to, dofBlurMaterial, blurPass);
			dofBlurMaterial.SetVector("offsets", new Vector4(spread / widthOverHeight * oneOverBaseSize, spread * oneOverBaseSize, 0f, 0f));
			Graphics.Blit(to, tmp, dofBlurMaterial, blurPass);
			dofBlurMaterial.SetVector("offsets", new Vector4(spread / widthOverHeight * oneOverBaseSize, (0f - spread) * oneOverBaseSize, 0f, 0f));
			Graphics.Blit(tmp, to, dofBlurMaterial, blurPass);
		}

		private void Downsample(RenderTexture from, RenderTexture to)
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)to.get_width()), 1f / (1f * (float)to.get_height()), 0f, 0f));
			Graphics.Blit(from, to, dofMaterial, SMOOTH_DOWNSAMPLE_PASS);
		}

		private void AddBokeh(RenderTexture bokehInfo, RenderTexture tempTex, RenderTexture finalTarget)
		{
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0132: Unknown result type (might be due to invalid IL or missing references)
			if (!Object.op_Implicit(bokehMaterial))
			{
				return;
			}
			Mesh[] meshes = Quads.GetMeshes(tempTex.get_width(), tempTex.get_height());
			RenderTexture.set_active(tempTex);
			GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
			GL.PushMatrix();
			GL.LoadIdentity();
			bokehInfo.set_filterMode(0);
			float num = (float)bokehInfo.get_width() * 1f / ((float)bokehInfo.get_height() * 1f);
			float num2 = 2f / (1f * (float)bokehInfo.get_width());
			num2 += bokehScale * maxBlurSpread * BOKEH_EXTRA_BLUR * oneOverBaseSize;
			bokehMaterial.SetTexture("_Source", bokehInfo);
			bokehMaterial.SetTexture("_MainTex", bokehTexture);
			bokehMaterial.SetVector("_ArScale", new Vector4(num2, num2 * num, 0.5f, 0.5f * num));
			bokehMaterial.SetFloat("_Intensity", bokehIntensity);
			bokehMaterial.SetPass(0);
			Mesh[] array = meshes;
			foreach (Mesh val in array)
			{
				if (Object.op_Implicit(val))
				{
					Graphics.DrawMeshNow(val, Matrix4x4.get_identity());
				}
			}
			GL.PopMatrix();
			Graphics.Blit(tempTex, finalTarget, dofMaterial, 8);
			bokehInfo.set_filterMode(1);
		}

		private void ReleaseTextures()
		{
			if (Object.op_Implicit(foregroundTexture))
			{
				RenderTexture.ReleaseTemporary(foregroundTexture);
			}
			if (Object.op_Implicit(finalDefocus))
			{
				RenderTexture.ReleaseTemporary(finalDefocus);
			}
			if (Object.op_Implicit(mediumRezWorkTexture))
			{
				RenderTexture.ReleaseTemporary(mediumRezWorkTexture);
			}
			if (Object.op_Implicit(lowRezWorkTexture))
			{
				RenderTexture.ReleaseTemporary(lowRezWorkTexture);
			}
			if (Object.op_Implicit(bokehSource))
			{
				RenderTexture.ReleaseTemporary(bokehSource);
			}
			if (Object.op_Implicit(bokehSource2))
			{
				RenderTexture.ReleaseTemporary(bokehSource2);
			}
		}

		private void AllocateTextures(bool blurForeground, RenderTexture source, int divider, int lowTexDivider)
		{
			//IL_0127: Unknown result type (might be due to invalid IL or missing references)
			foregroundTexture = null;
			if (blurForeground)
			{
				foregroundTexture = RenderTexture.GetTemporary(source.get_width(), source.get_height(), 0);
			}
			mediumRezWorkTexture = RenderTexture.GetTemporary(source.get_width() / divider, source.get_height() / divider, 0);
			finalDefocus = RenderTexture.GetTemporary(source.get_width() / divider, source.get_height() / divider, 0);
			lowRezWorkTexture = RenderTexture.GetTemporary(source.get_width() / lowTexDivider, source.get_height() / lowTexDivider, 0);
			bokehSource = null;
			bokehSource2 = null;
			if (bokeh)
			{
				bokehSource = RenderTexture.GetTemporary(source.get_width() / (lowTexDivider * bokehDownsample), source.get_height() / (lowTexDivider * bokehDownsample), 0, 2);
				bokehSource2 = RenderTexture.GetTemporary(source.get_width() / (lowTexDivider * bokehDownsample), source.get_height() / (lowTexDivider * bokehDownsample), 0, 2);
				bokehSource.set_filterMode(1);
				bokehSource2.set_filterMode(1);
				RenderTexture.set_active(bokehSource2);
				GL.Clear(false, true, new Color(0f, 0f, 0f, 0f));
			}
			source.set_filterMode(1);
			finalDefocus.set_filterMode(1);
			mediumRezWorkTexture.set_filterMode(1);
			lowRezWorkTexture.set_filterMode(1);
			if (Object.op_Implicit(foregroundTexture))
			{
				foregroundTexture.set_filterMode(1);
			}
		}
	}
}
