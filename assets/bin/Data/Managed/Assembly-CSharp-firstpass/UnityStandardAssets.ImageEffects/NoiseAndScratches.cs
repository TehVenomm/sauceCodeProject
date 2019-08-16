using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Noise/Noise and Scratches")]
	[RequireComponent(typeof(Camera))]
	public class NoiseAndScratches : MonoBehaviour
	{
		public bool monochrome = true;

		private bool rgbFallback;

		[Range(0f, 5f)]
		public float grainIntensityMin = 0.1f;

		[Range(0f, 5f)]
		public float grainIntensityMax = 0.2f;

		[Range(0.1f, 50f)]
		public float grainSize = 2f;

		[Range(0f, 5f)]
		public float scratchIntensityMin = 0.05f;

		[Range(0f, 5f)]
		public float scratchIntensityMax = 0.25f;

		[Range(1f, 30f)]
		public float scratchFPS = 10f;

		[Range(0f, 1f)]
		public float scratchJitter = 0.01f;

		public Texture grainTexture;

		public Texture scratchTexture;

		public Shader shaderRGB;

		public Shader shaderYUV;

		private Material m_MaterialRGB;

		private Material m_MaterialYUV;

		private float scratchTimeLeft;

		private float scratchX;

		private float scratchY;

		protected Material material
		{
			get
			{
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				//IL_0022: Expected O, but got Unknown
				//IL_0052: Unknown result type (might be due to invalid IL or missing references)
				//IL_005c: Expected O, but got Unknown
				if (m_MaterialRGB == null)
				{
					m_MaterialRGB = new Material(shaderRGB);
					m_MaterialRGB.set_hideFlags(61);
				}
				if (m_MaterialYUV == null && !rgbFallback)
				{
					m_MaterialYUV = new Material(shaderYUV);
					m_MaterialYUV.set_hideFlags(61);
				}
				return (rgbFallback || monochrome) ? m_MaterialRGB : m_MaterialYUV;
			}
		}

		public NoiseAndScratches()
			: this()
		{
		}

		protected void Start()
		{
			if (!SystemInfo.get_supportsImageEffects())
			{
				this.set_enabled(false);
			}
			else if (shaderRGB == null || shaderYUV == null)
			{
				Debug.Log((object)"Noise shaders are not set up! Disabling noise effect.");
				this.set_enabled(false);
			}
			else if (!shaderRGB.get_isSupported())
			{
				this.set_enabled(false);
			}
			else if (!shaderYUV.get_isSupported())
			{
				rgbFallback = true;
			}
		}

		protected void OnDisable()
		{
			if (Object.op_Implicit(m_MaterialRGB))
			{
				Object.DestroyImmediate(m_MaterialRGB);
			}
			if (Object.op_Implicit(m_MaterialYUV))
			{
				Object.DestroyImmediate(m_MaterialYUV);
			}
		}

		private void SanitizeParameters()
		{
			grainIntensityMin = Mathf.Clamp(grainIntensityMin, 0f, 5f);
			grainIntensityMax = Mathf.Clamp(grainIntensityMax, 0f, 5f);
			scratchIntensityMin = Mathf.Clamp(scratchIntensityMin, 0f, 5f);
			scratchIntensityMax = Mathf.Clamp(scratchIntensityMax, 0f, 5f);
			scratchFPS = Mathf.Clamp(scratchFPS, 1f, 30f);
			scratchJitter = Mathf.Clamp(scratchJitter, 0f, 1f);
			grainSize = Mathf.Clamp(grainSize, 0.1f, 50f);
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0122: Unknown result type (might be due to invalid IL or missing references)
			//IL_015e: Unknown result type (might be due to invalid IL or missing references)
			SanitizeParameters();
			if (scratchTimeLeft <= 0f)
			{
				scratchTimeLeft = Random.get_value() * 2f / scratchFPS;
				scratchX = Random.get_value();
				scratchY = Random.get_value();
			}
			scratchTimeLeft -= Time.get_deltaTime();
			Material material = this.material;
			material.SetTexture("_GrainTex", grainTexture);
			material.SetTexture("_ScratchTex", scratchTexture);
			float num = 1f / grainSize;
			material.SetVector("_GrainOffsetScale", new Vector4(Random.get_value(), Random.get_value(), (float)Screen.get_width() / (float)grainTexture.get_width() * num, (float)Screen.get_height() / (float)grainTexture.get_height() * num));
			material.SetVector("_ScratchOffsetScale", new Vector4(scratchX + Random.get_value() * scratchJitter, scratchY + Random.get_value() * scratchJitter, (float)Screen.get_width() / (float)scratchTexture.get_width(), (float)Screen.get_height() / (float)scratchTexture.get_height()));
			material.SetVector("_Intensity", new Vector4(Random.Range(grainIntensityMin, grainIntensityMax), Random.Range(scratchIntensityMin, scratchIntensityMax), 0f, 0f));
			Graphics.Blit(source, destination, material);
		}
	}
}
