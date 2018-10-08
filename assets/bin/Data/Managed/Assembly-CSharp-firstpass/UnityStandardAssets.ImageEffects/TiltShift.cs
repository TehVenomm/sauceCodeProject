using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[AddComponentMenu("Image Effects/Camera/Tilt Shift (Lens Blur)")]
	[RequireComponent(typeof(Camera))]
	internal class TiltShift : PostEffectsBase
	{
		public enum TiltShiftMode
		{
			TiltShiftMode,
			IrisMode
		}

		public enum TiltShiftQuality
		{
			Preview,
			Normal,
			High
		}

		public TiltShiftMode mode;

		public TiltShiftQuality quality = TiltShiftQuality.Normal;

		[Range(0f, 15f)]
		public float blurArea = 1f;

		[Range(0f, 25f)]
		public float maxBlurSize = 5f;

		[Range(0f, 1f)]
		public int downsample;

		public Shader tiltShiftShader;

		private Material tiltShiftMaterial;

		public override bool CheckResources()
		{
			CheckSupport(true);
			tiltShiftMaterial = CheckShaderAndCreateMaterial(tiltShiftShader, tiltShiftMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Expected O, but got Unknown
			if (!CheckResources())
			{
				Graphics.Blit(source, destination);
			}
			else
			{
				tiltShiftMaterial.SetFloat("_BlurSize", (!(maxBlurSize < 0f)) ? maxBlurSize : 0f);
				tiltShiftMaterial.SetFloat("_BlurArea", blurArea);
				source.set_filterMode(1);
				RenderTexture val = destination;
				if ((float)downsample > 0f)
				{
					val = RenderTexture.GetTemporary(source.get_width() >> downsample, source.get_height() >> downsample, 0, source.get_format());
					val.set_filterMode(1);
				}
				int num = (int)quality;
				num *= 2;
				Graphics.Blit(source, val, tiltShiftMaterial, (mode != 0) ? (num + 1) : num);
				if (downsample > 0)
				{
					tiltShiftMaterial.SetTexture("_Blurred", val);
					Graphics.Blit(source, destination, tiltShiftMaterial, 6);
				}
				if (val != destination)
				{
					RenderTexture.ReleaseTemporary(val);
				}
			}
		}
	}
}
