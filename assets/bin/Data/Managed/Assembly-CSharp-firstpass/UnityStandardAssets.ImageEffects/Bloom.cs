using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Bloom and Glow/Bloom")]
	public class Bloom : PostEffectsBase
	{
		public enum LensFlareStyle
		{
			Ghosting,
			Anamorphic,
			Combined
		}

		public enum TweakMode
		{
			Basic,
			Complex
		}

		public enum HDRBloomMode
		{
			Auto,
			On,
			Off
		}

		public enum BloomScreenBlendMode
		{
			Screen,
			Add
		}

		public enum BloomQuality
		{
			Cheap,
			High
		}

		public TweakMode tweakMode;

		public BloomScreenBlendMode screenBlendMode = BloomScreenBlendMode.Add;

		public HDRBloomMode hdr;

		private bool doHdr;

		public float sepBlurSpread = 2.5f;

		public BloomQuality quality = BloomQuality.High;

		public float bloomIntensity = 0.5f;

		public float bloomThreshold = 0.5f;

		public Color bloomThresholdColor = Color.get_white();

		public int bloomBlurIterations = 2;

		public int hollywoodFlareBlurIterations = 2;

		public float flareRotation;

		public LensFlareStyle lensflareMode = LensFlareStyle.Anamorphic;

		public float hollyStretchWidth = 2.5f;

		public float lensflareIntensity;

		public float lensflareThreshold = 0.3f;

		public float lensFlareSaturation = 0.75f;

		public Color flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);

		public Color flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);

		public Color flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);

		public Color flareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);

		public Texture2D lensFlareVignetteMask;

		public Shader lensFlareShader;

		private Material lensFlareMaterial;

		public Shader screenBlendShader;

		private Material screenBlend;

		public Shader blurAndFlaresShader;

		private Material blurAndFlaresMaterial;

		public Shader brightPassFilterShader;

		private Material brightPassFilterMaterial;

		public override bool CheckResources()
		{
			CheckSupport(false);
			screenBlend = CheckShaderAndCreateMaterial(screenBlendShader, screenBlend);
			lensFlareMaterial = CheckShaderAndCreateMaterial(lensFlareShader, lensFlareMaterial);
			blurAndFlaresMaterial = CheckShaderAndCreateMaterial(blurAndFlaresShader, blurAndFlaresMaterial);
			brightPassFilterMaterial = CheckShaderAndCreateMaterial(brightPassFilterShader, brightPassFilterMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		public void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Invalid comparison between Unknown and I4
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Expected O, but got Unknown
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Expected O, but got Unknown
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_011d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0122: Expected O, but got Unknown
			//IL_0174: Unknown result type (might be due to invalid IL or missing references)
			//IL_0175: Unknown result type (might be due to invalid IL or missing references)
			//IL_017a: Expected O, but got Unknown
			//IL_0184: Unknown result type (might be due to invalid IL or missing references)
			//IL_0189: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ef: Expected O, but got Unknown
			//IL_0210: Unknown result type (might be due to invalid IL or missing references)
			//IL_023a: Unknown result type (might be due to invalid IL or missing references)
			//IL_023b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0240: Expected O, but got Unknown
			//IL_0264: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_032c: Unknown result type (might be due to invalid IL or missing references)
			//IL_032d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0332: Expected O, but got Unknown
			//IL_0389: Unknown result type (might be due to invalid IL or missing references)
			//IL_039c: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0482: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0508: Unknown result type (might be due to invalid IL or missing references)
			//IL_0575: Unknown result type (might be due to invalid IL or missing references)
			//IL_064d: Unknown result type (might be due to invalid IL or missing references)
			//IL_068d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0749: Unknown result type (might be due to invalid IL or missing references)
			//IL_074a: Unknown result type (might be due to invalid IL or missing references)
			//IL_074f: Expected O, but got Unknown
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
				int num = source.get_width() / 2;
				int num2 = source.get_height() / 2;
				int num3 = source.get_width() / 4;
				int num4 = source.get_height() / 4;
				float num5 = 1f * (float)source.get_width() / (1f * (float)source.get_height());
				float num6 = 0.001953125f;
				RenderTexture val2 = RenderTexture.GetTemporary(num3, num4, 0, val);
				RenderTexture val3 = RenderTexture.GetTemporary(num, num2, 0, val);
				if (quality > BloomQuality.Cheap)
				{
					Graphics.Blit(source, val3, screenBlend, 2);
					RenderTexture val4 = RenderTexture.GetTemporary(num3, num4, 0, val);
					Graphics.Blit(val3, val4, screenBlend, 2);
					Graphics.Blit(val4, val2, screenBlend, 6);
					RenderTexture.ReleaseTemporary(val4);
				}
				else
				{
					Graphics.Blit(source, val3);
					Graphics.Blit(val3, val2, screenBlend, 6);
				}
				RenderTexture.ReleaseTemporary(val3);
				RenderTexture val5 = RenderTexture.GetTemporary(num3, num4, 0, val);
				BrightFilter(bloomThreshold * bloomThresholdColor, val2, val5);
				if (bloomBlurIterations < 1)
				{
					bloomBlurIterations = 1;
				}
				else if (bloomBlurIterations > 10)
				{
					bloomBlurIterations = 10;
				}
				for (int i = 0; i < bloomBlurIterations; i++)
				{
					float num7 = (1f + (float)i * 0.25f) * sepBlurSpread;
					RenderTexture val6 = RenderTexture.GetTemporary(num3, num4, 0, val);
					blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(0f, num7 * num6, 0f, 0f));
					Graphics.Blit(val5, val6, blurAndFlaresMaterial, 4);
					RenderTexture.ReleaseTemporary(val5);
					val5 = val6;
					val6 = RenderTexture.GetTemporary(num3, num4, 0, val);
					blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num7 / num5 * num6, 0f, 0f, 0f));
					Graphics.Blit(val5, val6, blurAndFlaresMaterial, 4);
					RenderTexture.ReleaseTemporary(val5);
					val5 = val6;
					if (quality > BloomQuality.Cheap)
					{
						if (i == 0)
						{
							Graphics.SetRenderTarget(val2);
							GL.Clear(false, true, Color.get_black());
							Graphics.Blit(val5, val2);
						}
						else
						{
							val2.MarkRestoreExpected();
							Graphics.Blit(val5, val2, screenBlend, 10);
						}
					}
				}
				if (quality > BloomQuality.Cheap)
				{
					Graphics.SetRenderTarget(val5);
					GL.Clear(false, true, Color.get_black());
					Graphics.Blit(val2, val5, screenBlend, 6);
				}
				if (lensflareIntensity > Mathf.Epsilon)
				{
					RenderTexture val7 = RenderTexture.GetTemporary(num3, num4, 0, val);
					if (lensflareMode == LensFlareStyle.Ghosting)
					{
						BrightFilter(lensflareThreshold, val5, val7);
						if (quality > BloomQuality.Cheap)
						{
							blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(0f, 1.5f / (1f * (float)val2.get_height()), 0f, 0f));
							Graphics.SetRenderTarget(val2);
							GL.Clear(false, true, Color.get_black());
							Graphics.Blit(val7, val2, blurAndFlaresMaterial, 4);
							blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(1.5f / (1f * (float)val2.get_width()), 0f, 0f, 0f));
							Graphics.SetRenderTarget(val7);
							GL.Clear(false, true, Color.get_black());
							Graphics.Blit(val2, val7, blurAndFlaresMaterial, 4);
						}
						Vignette(0.975f, val7, val7);
						BlendFlares(val7, val5);
					}
					else
					{
						float num8 = 1f * Mathf.Cos(flareRotation);
						float num9 = 1f * Mathf.Sin(flareRotation);
						float num10 = hollyStretchWidth * 1f / num5 * num6;
						blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num8, num9, 0f, 0f));
						blurAndFlaresMaterial.SetVector("_Threshhold", new Vector4(lensflareThreshold, 1f, 0f, 0f));
						blurAndFlaresMaterial.SetVector("_TintColor", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * flareColorA.a * lensflareIntensity);
						blurAndFlaresMaterial.SetFloat("_Saturation", lensFlareSaturation);
						val2.DiscardContents();
						Graphics.Blit(val7, val2, blurAndFlaresMaterial, 2);
						val7.DiscardContents();
						Graphics.Blit(val2, val7, blurAndFlaresMaterial, 3);
						blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num8 * num10, num9 * num10, 0f, 0f));
						blurAndFlaresMaterial.SetFloat("_StretchWidth", hollyStretchWidth);
						val2.DiscardContents();
						Graphics.Blit(val7, val2, blurAndFlaresMaterial, 1);
						blurAndFlaresMaterial.SetFloat("_StretchWidth", hollyStretchWidth * 2f);
						val7.DiscardContents();
						Graphics.Blit(val2, val7, blurAndFlaresMaterial, 1);
						blurAndFlaresMaterial.SetFloat("_StretchWidth", hollyStretchWidth * 4f);
						val2.DiscardContents();
						Graphics.Blit(val7, val2, blurAndFlaresMaterial, 1);
						for (int j = 0; j < hollywoodFlareBlurIterations; j++)
						{
							num10 = hollyStretchWidth * 2f / num5 * num6;
							blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num10 * num8, num10 * num9, 0f, 0f));
							val7.DiscardContents();
							Graphics.Blit(val2, val7, blurAndFlaresMaterial, 4);
							blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num10 * num8, num10 * num9, 0f, 0f));
							val2.DiscardContents();
							Graphics.Blit(val7, val2, blurAndFlaresMaterial, 4);
						}
						if (lensflareMode == LensFlareStyle.Anamorphic)
						{
							AddTo(1f, val2, val5);
						}
						else
						{
							Vignette(1f, val2, val7);
							BlendFlares(val7, val2);
							AddTo(1f, val2, val5);
						}
					}
					RenderTexture.ReleaseTemporary(val7);
				}
				int num11 = (int)bloomScreenBlendMode;
				screenBlend.SetFloat("_Intensity", bloomIntensity);
				screenBlend.SetTexture("_ColorBuffer", source);
				if (quality > BloomQuality.Cheap)
				{
					RenderTexture val8 = RenderTexture.GetTemporary(num, num2, 0, val);
					Graphics.Blit(val5, val8);
					Graphics.Blit(val8, destination, screenBlend, num11);
					RenderTexture.ReleaseTemporary(val8);
				}
				else
				{
					Graphics.Blit(val5, destination, screenBlend, num11);
				}
				RenderTexture.ReleaseTemporary(val2);
				RenderTexture.ReleaseTemporary(val5);
			}
		}

		private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
		{
			screenBlend.SetFloat("_Intensity", intensity_);
			to.MarkRestoreExpected();
			Graphics.Blit(from, to, screenBlend, 9);
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
			to.MarkRestoreExpected();
			Graphics.Blit(from, to, lensFlareMaterial);
		}

		private void BrightFilter(float thresh, RenderTexture from, RenderTexture to)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			brightPassFilterMaterial.SetVector("_Threshhold", new Vector4(thresh, thresh, thresh, thresh));
			Graphics.Blit(from, to, brightPassFilterMaterial, 0);
		}

		private void BrightFilter(Color threshColor, RenderTexture from, RenderTexture to)
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			brightPassFilterMaterial.SetVector("_Threshhold", Color.op_Implicit(threshColor));
			Graphics.Blit(from, to, brightPassFilterMaterial, 1);
		}

		private void Vignette(float amount, RenderTexture from, RenderTexture to)
		{
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			if (Object.op_Implicit(lensFlareVignetteMask))
			{
				screenBlend.SetTexture("_ColorBuffer", lensFlareVignetteMask);
				to.MarkRestoreExpected();
				Graphics.Blit((!(from == to)) ? from : null, to, screenBlend, (!(from == to)) ? 3 : 7);
			}
			else if (from != to)
			{
				Graphics.SetRenderTarget(to);
				GL.Clear(false, true, Color.get_black());
				Graphics.Blit(from, to);
			}
		}
	}
}
