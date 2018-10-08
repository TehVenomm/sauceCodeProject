using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[AddComponentMenu("Image Effects/Blur/Motion Blur (Color Accumulation)")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class MotionBlur : ImageEffectBase
	{
		[Range(0f, 0.92f)]
		public float blurAmount = 0.8f;

		public bool extraBlur;

		private RenderTexture accumTexture;

		protected override void Start()
		{
			if (!SystemInfo.get_supportsRenderTextures())
			{
				this.set_enabled(false);
			}
			else
			{
				base.Start();
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Object.DestroyImmediate(accumTexture);
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Expected O, but got Unknown
			if (accumTexture == null || accumTexture.get_width() != source.get_width() || accumTexture.get_height() != source.get_height())
			{
				Object.DestroyImmediate(accumTexture);
				accumTexture = new RenderTexture(source.get_width(), source.get_height(), 0);
				accumTexture.set_hideFlags(61);
				Graphics.Blit(source, accumTexture);
			}
			if (extraBlur)
			{
				RenderTexture val = RenderTexture.GetTemporary(source.get_width() / 4, source.get_height() / 4, 0);
				accumTexture.MarkRestoreExpected();
				Graphics.Blit(accumTexture, val);
				Graphics.Blit(val, accumTexture);
				RenderTexture.ReleaseTemporary(val);
			}
			blurAmount = Mathf.Clamp(blurAmount, 0f, 0.92f);
			base.material.SetTexture("_MainTex", accumTexture);
			base.material.SetFloat("_AccumOrig", 1f - blurAmount);
			accumTexture.MarkRestoreExpected();
			Graphics.Blit(source, accumTexture, base.material);
			Graphics.Blit(accumTexture, destination);
		}
	}
}
