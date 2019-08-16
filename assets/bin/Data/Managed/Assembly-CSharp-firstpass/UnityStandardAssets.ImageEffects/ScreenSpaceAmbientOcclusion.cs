using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[AddComponentMenu("Image Effects/Rendering/Screen Space Ambient Occlusion")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class ScreenSpaceAmbientOcclusion : MonoBehaviour
	{
		public enum SSAOSamples
		{
			Low,
			Medium,
			High
		}

		[Range(0.05f, 1f)]
		public float m_Radius = 0.4f;

		public SSAOSamples m_SampleCount = SSAOSamples.Medium;

		[Range(0.5f, 4f)]
		public float m_OcclusionIntensity = 1.5f;

		[Range(0f, 4f)]
		public int m_Blur = 2;

		[Range(1f, 6f)]
		public int m_Downsampling = 2;

		[Range(0.2f, 2f)]
		public float m_OcclusionAttenuation = 1f;

		[Range(1E-05f, 0.5f)]
		public float m_MinZ = 0.01f;

		public Shader m_SSAOShader;

		private Material m_SSAOMaterial;

		public Texture2D m_RandomTexture;

		private bool m_Supported;

		public ScreenSpaceAmbientOcclusion()
			: this()
		{
		}

		private static Material CreateMaterial(Shader shader)
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (!Object.op_Implicit(shader))
			{
				return null;
			}
			Material val = new Material(shader);
			val.set_hideFlags(61);
			return val;
		}

		private static void DestroyMaterial(Material mat)
		{
			if (Object.op_Implicit(mat))
			{
				Object.DestroyImmediate(mat);
				mat = null;
			}
		}

		private void OnDisable()
		{
			DestroyMaterial(m_SSAOMaterial);
		}

		private void Start()
		{
			if (!SystemInfo.get_supportsImageEffects() || !SystemInfo.SupportsRenderTextureFormat(1))
			{
				m_Supported = false;
				this.set_enabled(false);
				return;
			}
			CreateMaterials();
			if (!Object.op_Implicit(m_SSAOMaterial) || m_SSAOMaterial.get_passCount() != 5)
			{
				m_Supported = false;
				this.set_enabled(false);
			}
			else
			{
				m_Supported = true;
			}
		}

		private void OnEnable()
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			Camera component = this.GetComponent<Camera>();
			component.set_depthTextureMode(component.get_depthTextureMode() | 2);
		}

		private void CreateMaterials()
		{
			if (!Object.op_Implicit(m_SSAOMaterial) && m_SSAOShader.get_isSupported())
			{
				m_SSAOMaterial = CreateMaterial(m_SSAOShader);
				m_SSAOMaterial.SetTexture("_RandomTexture", m_RandomTexture);
			}
		}

		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			//IL_0127: Unknown result type (might be due to invalid IL or missing references)
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0191: Unknown result type (might be due to invalid IL or missing references)
			//IL_0196: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0242: Unknown result type (might be due to invalid IL or missing references)
			//IL_02af: Unknown result type (might be due to invalid IL or missing references)
			if (!m_Supported || !m_SSAOShader.get_isSupported())
			{
				this.set_enabled(false);
				return;
			}
			CreateMaterials();
			m_Downsampling = Mathf.Clamp(m_Downsampling, 1, 6);
			m_Radius = Mathf.Clamp(m_Radius, 0.05f, 1f);
			m_MinZ = Mathf.Clamp(m_MinZ, 1E-05f, 0.5f);
			m_OcclusionIntensity = Mathf.Clamp(m_OcclusionIntensity, 0.5f, 4f);
			m_OcclusionAttenuation = Mathf.Clamp(m_OcclusionAttenuation, 0.2f, 2f);
			m_Blur = Mathf.Clamp(m_Blur, 0, 4);
			RenderTexture val = RenderTexture.GetTemporary(source.get_width() / m_Downsampling, source.get_height() / m_Downsampling, 0);
			float fieldOfView = this.GetComponent<Camera>().get_fieldOfView();
			float farClipPlane = this.GetComponent<Camera>().get_farClipPlane();
			float num = Mathf.Tan(fieldOfView * ((float)Math.PI / 180f) * 0.5f) * farClipPlane;
			float num2 = num * this.GetComponent<Camera>().get_aspect();
			m_SSAOMaterial.SetVector("_FarCorner", Vector4.op_Implicit(new Vector3(num2, num, farClipPlane)));
			int num3;
			int num4;
			if (Object.op_Implicit(m_RandomTexture))
			{
				num3 = m_RandomTexture.get_width();
				num4 = m_RandomTexture.get_height();
			}
			else
			{
				num3 = 1;
				num4 = 1;
			}
			m_SSAOMaterial.SetVector("_NoiseScale", Vector4.op_Implicit(new Vector3((float)val.get_width() / (float)num3, (float)val.get_height() / (float)num4, 0f)));
			m_SSAOMaterial.SetVector("_Params", new Vector4(m_Radius, m_MinZ, 1f / m_OcclusionAttenuation, m_OcclusionIntensity));
			bool flag = m_Blur > 0;
			Graphics.Blit((!flag) ? source : null, val, m_SSAOMaterial, (int)m_SampleCount);
			if (flag)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(source.get_width(), source.get_height(), 0);
				m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4((float)m_Blur / (float)source.get_width(), 0f, 0f, 0f));
				m_SSAOMaterial.SetTexture("_SSAO", val);
				Graphics.Blit(null, temporary, m_SSAOMaterial, 3);
				RenderTexture.ReleaseTemporary(val);
				RenderTexture temporary2 = RenderTexture.GetTemporary(source.get_width(), source.get_height(), 0);
				m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4(0f, (float)m_Blur / (float)source.get_height(), 0f, 0f));
				m_SSAOMaterial.SetTexture("_SSAO", temporary);
				Graphics.Blit(source, temporary2, m_SSAOMaterial, 3);
				RenderTexture.ReleaseTemporary(temporary);
				val = temporary2;
			}
			m_SSAOMaterial.SetTexture("_SSAO", val);
			Graphics.Blit(source, destination, m_SSAOMaterial, 4);
			RenderTexture.ReleaseTemporary(val);
		}
	}
}
