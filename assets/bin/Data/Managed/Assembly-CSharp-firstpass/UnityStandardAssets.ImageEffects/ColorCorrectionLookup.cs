using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[AddComponentMenu("Image Effects/Color Adjustments/Color Correction (3D Lookup Texture)")]
	[ExecuteInEditMode]
	public class ColorCorrectionLookup : PostEffectsBase
	{
		public Shader shader;

		private Material material;

		public Texture3D converted3DLut;

		public string basedOnTempTex = string.Empty;

		public override bool CheckResources()
		{
			CheckSupport(false);
			material = CheckShaderAndCreateMaterial(shader, material);
			if (!isSupported || !SystemInfo.get_supports3DTextures())
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnDisable()
		{
			if (Object.op_Implicit(material))
			{
				Object.DestroyImmediate(material);
				material = null;
			}
		}

		private void OnDestroy()
		{
			if (Object.op_Implicit(converted3DLut))
			{
				Object.DestroyImmediate(converted3DLut);
			}
			converted3DLut = null;
		}

		public void SetIdentityLut()
		{
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Expected O, but got Unknown
			int num = 16;
			Color[] array = (Color[])new Color[num * num * num];
			float num2 = 1f / (1f * (float)num - 1f);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						array[i + j * num + k * num * num] = new Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
					}
				}
			}
			if (Object.op_Implicit(converted3DLut))
			{
				Object.DestroyImmediate(converted3DLut);
			}
			converted3DLut = new Texture3D(num, num, num, 5, false);
			converted3DLut.SetPixels(array);
			converted3DLut.Apply();
			basedOnTempTex = string.Empty;
		}

		public bool ValidDimensions(Texture2D tex2d)
		{
			if (!Object.op_Implicit(tex2d))
			{
				return false;
			}
			int height = tex2d.get_height();
			if (height != Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.get_width())))
			{
				return false;
			}
			return true;
		}

		public void Convert(Texture2D temp2DTex, string path)
		{
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Expected O, but got Unknown
			if (Object.op_Implicit(temp2DTex))
			{
				int num = temp2DTex.get_width() * temp2DTex.get_height();
				num = temp2DTex.get_height();
				if (!ValidDimensions(temp2DTex))
				{
					Debug.LogWarning((object)("The given 2D texture " + temp2DTex.get_name() + " cannot be used as a 3D LUT."));
					basedOnTempTex = string.Empty;
				}
				else
				{
					Color[] pixels = temp2DTex.GetPixels();
					Color[] array = (Color[])new Color[pixels.Length];
					for (int i = 0; i < num; i++)
					{
						for (int j = 0; j < num; j++)
						{
							for (int k = 0; k < num; k++)
							{
								int num2 = num - j - 1;
								array[i + j * num + k * num * num] = pixels[k * num + i + num2 * num * num];
							}
						}
					}
					if (Object.op_Implicit(converted3DLut))
					{
						Object.DestroyImmediate(converted3DLut);
					}
					converted3DLut = new Texture3D(num, num, num, 5, false);
					converted3DLut.SetPixels(array);
					converted3DLut.Apply();
					basedOnTempTex = path;
				}
			}
			else
			{
				Debug.LogError((object)"Couldn't color correct with 3D LUT texture. Image Effect will be disabled.");
			}
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Invalid comparison between Unknown and I4
			if (!CheckResources() || !SystemInfo.get_supports3DTextures())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				if (converted3DLut == null)
				{
					SetIdentityLut();
				}
				int width = converted3DLut.get_width();
				converted3DLut.set_wrapMode(1);
				material.SetFloat("_Scale", (float)(width - 1) / (1f * (float)width));
				material.SetFloat("_Offset", 1f / (2f * (float)width));
				material.SetTexture("_ClutTex", converted3DLut);
				Graphics.Blit(source, destination, material, ((int)QualitySettings.get_activeColorSpace() == 1) ? 1 : 0);
			}
		}
	}
}
