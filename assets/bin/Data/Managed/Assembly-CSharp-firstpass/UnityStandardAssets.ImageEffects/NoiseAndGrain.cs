using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Noise/Noise And Grain (Filmic)")]
	public class NoiseAndGrain : PostEffectsBase
	{
		public float intensityMultiplier = 0.25f;

		public float generalIntensity = 0.5f;

		public float blackIntensity = 1f;

		public float whiteIntensity = 1f;

		public float midGrey = 0.2f;

		public bool dx11Grain;

		public float softness;

		public bool monochrome;

		public Vector3 intensities = new Vector3(1f, 1f, 1f);

		public Vector3 tiling = new Vector3(64f, 64f, 64f);

		public float monochromeTiling = 64f;

		public FilterMode filterMode = 1;

		public Texture2D noiseTexture;

		public Shader noiseShader;

		private Material noiseMaterial;

		public Shader dx11NoiseShader;

		private Material dx11NoiseMaterial;

		private static float TILE_AMOUNT = 64f;

		public override bool CheckResources()
		{
			CheckSupport(false);
			noiseMaterial = CheckShaderAndCreateMaterial(noiseShader, noiseMaterial);
			if (dx11Grain && supportDX11)
			{
				dx11NoiseMaterial = CheckShaderAndCreateMaterial(dx11NoiseShader, dx11NoiseMaterial);
			}
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			//IL_012d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0138: Unknown result type (might be due to invalid IL or missing references)
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0181: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Expected O, but got Unknown
			//IL_0223: Unknown result type (might be due to invalid IL or missing references)
			//IL_0259: Unknown result type (might be due to invalid IL or missing references)
			//IL_0264: Unknown result type (might be due to invalid IL or missing references)
			//IL_0269: Unknown result type (might be due to invalid IL or missing references)
			//IL_0289: Unknown result type (might be due to invalid IL or missing references)
			//IL_0294: Unknown result type (might be due to invalid IL or missing references)
			//IL_029f: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0309: Unknown result type (might be due to invalid IL or missing references)
			//IL_0314: Unknown result type (might be due to invalid IL or missing references)
			//IL_0319: Unknown result type (might be due to invalid IL or missing references)
			//IL_035d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0362: Expected O, but got Unknown
			if (!CheckResources() || null == noiseTexture)
			{
				Graphics.Blit(source, destination);
				if (null == noiseTexture)
				{
					Debug.LogWarning((object)"Noise & Grain effect failing as noise texture is not assigned. please assign.", this.get_transform());
				}
			}
			else
			{
				softness = Mathf.Clamp(softness, 0f, 0.99f);
				if (dx11Grain && supportDX11)
				{
					dx11NoiseMaterial.SetFloat("_DX11NoiseTime", (float)Time.get_frameCount());
					dx11NoiseMaterial.SetTexture("_NoiseTex", noiseTexture);
					dx11NoiseMaterial.SetVector("_NoisePerChannel", Vector4.op_Implicit((!monochrome) ? intensities : Vector3.get_one()));
					dx11NoiseMaterial.SetVector("_MidGrey", Vector4.op_Implicit(new Vector3(midGrey, 1f / (1f - midGrey), -1f / midGrey)));
					dx11NoiseMaterial.SetVector("_NoiseAmount", Vector4.op_Implicit(new Vector3(generalIntensity, blackIntensity, whiteIntensity) * intensityMultiplier));
					if (softness > Mathf.Epsilon)
					{
						RenderTexture val = RenderTexture.GetTemporary((int)((float)source.get_width() * (1f - softness)), (int)((float)source.get_height() * (1f - softness)));
						DrawNoiseQuadGrid(source, val, dx11NoiseMaterial, noiseTexture, (!monochrome) ? 2 : 3);
						dx11NoiseMaterial.SetTexture("_NoiseTex", val);
						Graphics.Blit(source, destination, dx11NoiseMaterial, 4);
						RenderTexture.ReleaseTemporary(val);
					}
					else
					{
						DrawNoiseQuadGrid(source, destination, dx11NoiseMaterial, noiseTexture, monochrome ? 1 : 0);
					}
				}
				else
				{
					if (Object.op_Implicit(noiseTexture))
					{
						noiseTexture.set_wrapMode(0);
						noiseTexture.set_filterMode(filterMode);
					}
					noiseMaterial.SetTexture("_NoiseTex", noiseTexture);
					noiseMaterial.SetVector("_NoisePerChannel", Vector4.op_Implicit((!monochrome) ? intensities : Vector3.get_one()));
					noiseMaterial.SetVector("_NoiseTilingPerChannel", Vector4.op_Implicit((!monochrome) ? tiling : (Vector3.get_one() * monochromeTiling)));
					noiseMaterial.SetVector("_MidGrey", Vector4.op_Implicit(new Vector3(midGrey, 1f / (1f - midGrey), -1f / midGrey)));
					noiseMaterial.SetVector("_NoiseAmount", Vector4.op_Implicit(new Vector3(generalIntensity, blackIntensity, whiteIntensity) * intensityMultiplier));
					if (softness > Mathf.Epsilon)
					{
						RenderTexture val2 = RenderTexture.GetTemporary((int)((float)source.get_width() * (1f - softness)), (int)((float)source.get_height() * (1f - softness)));
						DrawNoiseQuadGrid(source, val2, noiseMaterial, noiseTexture, 2);
						noiseMaterial.SetTexture("_NoiseTex", val2);
						Graphics.Blit(source, destination, noiseMaterial, 1);
						RenderTexture.ReleaseTemporary(val2);
					}
					else
					{
						DrawNoiseQuadGrid(source, destination, noiseMaterial, noiseTexture, 0);
					}
				}
			}
		}

		private static void DrawNoiseQuadGrid(RenderTexture source, RenderTexture dest, Material fxMaterial, Texture2D noise, int passNr)
		{
			RenderTexture.set_active(dest);
			float num = (float)noise.get_width() * 1f;
			float num2 = 1f * (float)source.get_width() / TILE_AMOUNT;
			fxMaterial.SetTexture("_MainTex", source);
			GL.PushMatrix();
			GL.LoadOrtho();
			float num3 = 1f * (float)source.get_width() / (1f * (float)source.get_height());
			float num4 = 1f / num2;
			float num5 = num4 * num3;
			float num6 = num / ((float)noise.get_width() * 1f);
			fxMaterial.SetPass(passNr);
			GL.Begin(7);
			for (float num7 = 0f; num7 < 1f; num7 += num4)
			{
				for (float num8 = 0f; num8 < 1f; num8 += num5)
				{
					float num9 = Random.Range(0f, 1f);
					float num10 = Random.Range(0f, 1f);
					num9 = Mathf.Floor(num9 * num) / num;
					num10 = Mathf.Floor(num10 * num) / num;
					float num11 = 1f / num;
					GL.MultiTexCoord2(0, num9, num10);
					GL.MultiTexCoord2(1, 0f, 0f);
					GL.Vertex3(num7, num8, 0.1f);
					GL.MultiTexCoord2(0, num9 + num6 * num11, num10);
					GL.MultiTexCoord2(1, 1f, 0f);
					GL.Vertex3(num7 + num4, num8, 0.1f);
					GL.MultiTexCoord2(0, num9 + num6 * num11, num10 + num6 * num11);
					GL.MultiTexCoord2(1, 1f, 1f);
					GL.Vertex3(num7 + num4, num8 + num5, 0.1f);
					GL.MultiTexCoord2(0, num9, num10 + num6 * num11);
					GL.MultiTexCoord2(1, 0f, 1f);
					GL.Vertex3(num7, num8 + num5, 0.1f);
				}
			}
			GL.End();
			GL.PopMatrix();
		}
	}
}
