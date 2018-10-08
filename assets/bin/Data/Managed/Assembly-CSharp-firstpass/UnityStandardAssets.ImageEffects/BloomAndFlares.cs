using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[AddComponentMenu("Image Effects/Bloom and Glow/BloomAndFlares (3.5, Deprecated)")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class BloomAndFlares : PostEffectsBase
	{
		public TweakMode34 tweakMode;

		public BloomScreenBlendMode screenBlendMode = BloomScreenBlendMode.Add;

		public HDRBloomMode hdr;

		private bool doHdr;

		public float sepBlurSpread = 1.5f;

		public float useSrcAlphaAsMask = 0.5f;

		public float bloomIntensity = 1f;

		public float bloomThreshold = 0.5f;

		public int bloomBlurIterations = 2;

		public bool lensflares;

		public int hollywoodFlareBlurIterations = 2;

		public LensflareStyle34 lensflareMode = LensflareStyle34.Anamorphic;

		public float hollyStretchWidth = 3.5f;

		public float lensflareIntensity = 1f;

		public float lensflareThreshold = 0.3f;

		public Color flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);

		public Color flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);

		public Color flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);

		public Color flareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);

		public Texture2D lensFlareVignetteMask;

		public Shader lensFlareShader;

		private Material lensFlareMaterial;

		public Shader vignetteShader;

		private Material vignetteMaterial;

		public Shader separableBlurShader;

		private Material separableBlurMaterial;

		public Shader addBrightStuffOneOneShader;

		private Material addBrightStuffBlendOneOneMaterial;

		public Shader screenBlendShader;

		private Material screenBlend;

		public Shader hollywoodFlaresShader;

		private Material hollywoodFlaresMaterial;

		public Shader brightPassFilterShader;

		private Material brightPassFilterMaterial;

		public override bool CheckResources()
		{
			CheckSupport(false);
			screenBlend = CheckShaderAndCreateMaterial(screenBlendShader, screenBlend);
			lensFlareMaterial = CheckShaderAndCreateMaterial(lensFlareShader, lensFlareMaterial);
			vignetteMaterial = CheckShaderAndCreateMaterial(vignetteShader, vignetteMaterial);
			separableBlurMaterial = CheckShaderAndCreateMaterial(separableBlurShader, separableBlurMaterial);
			addBrightStuffBlendOneOneMaterial = CheckShaderAndCreateMaterial(addBrightStuffOneOneShader, addBrightStuffBlendOneOneMaterial);
			hollywoodFlaresMaterial = CheckShaderAndCreateMaterial(hollywoodFlaresShader, hollywoodFlaresMaterial);
			brightPassFilterMaterial = CheckShaderAndCreateMaterial(brightPassFilterShader, brightPassFilterMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Invalid comparison between Unknown and I4
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Expected O, but got Unknown
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Expected O, but got Unknown
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Expected O, but got Unknown
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Expected O, but got Unknown
			//IL_01af: Unknown result type (might be due to invalid IL or missing references)
			//IL_0202: Unknown result type (might be due to invalid IL or missing references)
			//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_030d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0318: Unknown result type (might be due to invalid IL or missing references)
			//IL_037c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0459: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0526: Unknown result type (might be due to invalid IL or missing references)
			//IL_0572: Unknown result type (might be due to invalid IL or missing references)
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				doHdr = false;
				if (hdr == HDRBloomMode.Auto)
				{
					doHdr = ((int)source.get_format() == 2 && this.GetComponent<Camera>().get_hdr());
				}
				else
				{
					doHdr = (hdr == HDRBloomMode.On);
				}
				doHdr = (doHdr && supportHDRTextures);
				BloomScreenBlendMode bloomScreenBlendMode = screenBlendMode;
				if (doHdr)
				{
					bloomScreenBlendMode = BloomScreenBlendMode.Add;
				}
				RenderTextureFormat val = (!doHdr) ? 7 : 2;
				RenderTexture val2 = RenderTexture.GetTemporary(source.get_width() / 2, source.get_height() / 2, 0, val);
				RenderTexture val3 = RenderTexture.GetTemporary(source.get_width() / 4, source.get_height() / 4, 0, val);
				RenderTexture val4 = RenderTexture.GetTemporary(source.get_width() / 4, source.get_height() / 4, 0, val);
				RenderTexture val5 = RenderTexture.GetTemporary(source.get_width() / 4, source.get_height() / 4, 0, val);
				float num = 1f * (float)source.get_width() / (1f * (float)source.get_height());
				float num2 = 0.001953125f;
				Graphics.Blit(source, val2, screenBlend, 2);
				Graphics.Blit(val2, val3, screenBlend, 2);
				RenderTexture.ReleaseTemporary(val2);
				BrightFilter(bloomThreshold, useSrcAlphaAsMask, val3, val4);
				val3.DiscardContents();
				if (bloomBlurIterations < 1)
				{
					bloomBlurIterations = 1;
				}
				for (int i = 0; i < bloomBlurIterations; i++)
				{
					float num3 = (1f + (float)i * 0.5f) * sepBlurSpread;
					separableBlurMaterial.SetVector("offsets", new Vector4(0f, num3 * num2, 0f, 0f));
					RenderTexture val6 = (i != 0) ? val3 : val4;
					Graphics.Blit(val6, val5, separableBlurMaterial);
					val6.DiscardContents();
					separableBlurMaterial.SetVector("offsets", new Vector4(num3 / num * num2, 0f, 0f, 0f));
					Graphics.Blit(val5, val3, separableBlurMaterial);
					val5.DiscardContents();
				}
				if (lensflares)
				{
					if (lensflareMode == LensflareStyle34.Ghosting)
					{
						BrightFilter(lensflareThreshold, 0f, val3, val5);
						val3.DiscardContents();
						Vignette(0.975f, val5, val4);
						val5.DiscardContents();
						BlendFlares(val4, val3);
						val4.DiscardContents();
					}
					else
					{
						hollywoodFlaresMaterial.SetVector("_threshold", new Vector4(lensflareThreshold, 1f / (1f - lensflareThreshold), 0f, 0f));
						hollywoodFlaresMaterial.SetVector("tintColor", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * flareColorA.a * lensflareIntensity);
						Graphics.Blit(val5, val4, hollywoodFlaresMaterial, 2);
						val5.DiscardContents();
						Graphics.Blit(val4, val5, hollywoodFlaresMaterial, 3);
						val4.DiscardContents();
						hollywoodFlaresMaterial.SetVector("offsets", new Vector4(sepBlurSpread * 1f / num * num2, 0f, 0f, 0f));
						hollywoodFlaresMaterial.SetFloat("stretchWidth", hollyStretchWidth);
						Graphics.Blit(val5, val4, hollywoodFlaresMaterial, 1);
						val5.DiscardContents();
						hollywoodFlaresMaterial.SetFloat("stretchWidth", hollyStretchWidth * 2f);
						Graphics.Blit(val4, val5, hollywoodFlaresMaterial, 1);
						val4.DiscardContents();
						hollywoodFlaresMaterial.SetFloat("stretchWidth", hollyStretchWidth * 4f);
						Graphics.Blit(val5, val4, hollywoodFlaresMaterial, 1);
						val5.DiscardContents();
						if (lensflareMode == LensflareStyle34.Anamorphic)
						{
							for (int j = 0; j < hollywoodFlareBlurIterations; j++)
							{
								separableBlurMaterial.SetVector("offsets", new Vector4(hollyStretchWidth * 2f / num * num2, 0f, 0f, 0f));
								Graphics.Blit(val4, val5, separableBlurMaterial);
								val4.DiscardContents();
								separableBlurMaterial.SetVector("offsets", new Vector4(hollyStretchWidth * 2f / num * num2, 0f, 0f, 0f));
								Graphics.Blit(val5, val4, separableBlurMaterial);
								val5.DiscardContents();
							}
							AddTo(1f, val4, val3);
							val4.DiscardContents();
						}
						else
						{
							for (int k = 0; k < hollywoodFlareBlurIterations; k++)
							{
								separableBlurMaterial.SetVector("offsets", new Vector4(hollyStretchWidth * 2f / num * num2, 0f, 0f, 0f));
								Graphics.Blit(val4, val5, separableBlurMaterial);
								val4.DiscardContents();
								separableBlurMaterial.SetVector("offsets", new Vector4(hollyStretchWidth * 2f / num * num2, 0f, 0f, 0f));
								Graphics.Blit(val5, val4, separableBlurMaterial);
								val5.DiscardContents();
							}
							Vignette(1f, val4, val5);
							val4.DiscardContents();
							BlendFlares(val5, val4);
							val5.DiscardContents();
							AddTo(1f, val4, val3);
							val4.DiscardContents();
						}
					}
				}
				screenBlend.SetFloat("_Intensity", bloomIntensity);
				screenBlend.SetTexture("_ColorBuffer", source);
				Graphics.Blit(val3, destination, screenBlend, (int)bloomScreenBlendMode);
				RenderTexture.ReleaseTemporary(val3);
				RenderTexture.ReleaseTemporary(val4);
				RenderTexture.ReleaseTemporary(val5);
			}
		}

		private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
		{
			addBrightStuffBlendOneOneMaterial.SetFloat("_Intensity", intensity_);
			Graphics.Blit(from, to, addBrightStuffBlendOneOneMaterial);
		}

		private void BlendFlares(RenderTexture from, RenderTexture to)
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_011b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0126: Unknown result type (might be due to invalid IL or missing references)
			lensFlareMaterial.SetVector("colorA", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * lensflareIntensity);
			lensFlareMaterial.SetVector("colorB", new Vector4(flareColorB.r, flareColorB.g, flareColorB.b, flareColorB.a) * lensflareIntensity);
			lensFlareMaterial.SetVector("colorC", new Vector4(flareColorC.r, flareColorC.g, flareColorC.b, flareColorC.a) * lensflareIntensity);
			lensFlareMaterial.SetVector("colorD", new Vector4(flareColorD.r, flareColorD.g, flareColorD.b, flareColorD.a) * lensflareIntensity);
			Graphics.Blit(from, to, lensFlareMaterial);
		}

		private void BrightFilter(float thresh, float useAlphaAsMask, RenderTexture from, RenderTexture to)
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			if (doHdr)
			{
				brightPassFilterMaterial.SetVector("threshold", new Vector4(thresh, 1f, 0f, 0f));
			}
			else
			{
				brightPassFilterMaterial.SetVector("threshold", new Vector4(thresh, 1f / (1f - thresh), 0f, 0f));
			}
			brightPassFilterMaterial.SetFloat("useSrcAlphaAsMask", useAlphaAsMask);
			Graphics.Blit(from, to, brightPassFilterMaterial);
		}

		private void Vignette(float amount, RenderTexture from, RenderTexture to)
		{
			if (Object.op_Implicit(lensFlareVignetteMask))
			{
				screenBlend.SetTexture("_ColorBuffer", lensFlareVignetteMask);
				Graphics.Blit(from, to, screenBlend, 3);
			}
			else
			{
				vignetteMaterial.SetFloat("vignetteIntensity", amount);
				Graphics.Blit(from, to, vignetteMaterial);
			}
		}
	}
}
