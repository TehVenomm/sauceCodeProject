using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[AddComponentMenu("Image Effects/Color Adjustments/Contrast Stretch")]
	public class ContrastStretch : MonoBehaviour
	{
		[Range(0.0001f, 1f)]
		public float adaptationSpeed = 0.02f;

		[Range(0f, 1f)]
		public float limitMinimum = 0.2f;

		[Range(0f, 1f)]
		public float limitMaximum = 0.6f;

		private RenderTexture[] adaptRenderTex = (RenderTexture[])new RenderTexture[2];

		private int curAdaptIndex;

		public Shader shaderLum;

		private Material m_materialLum;

		public Shader shaderReduce;

		private Material m_materialReduce;

		public Shader shaderAdapt;

		private Material m_materialAdapt;

		public Shader shaderApply;

		private Material m_materialApply;

		protected Material materialLum
		{
			get
			{
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				//IL_0022: Expected O, but got Unknown
				if (m_materialLum == null)
				{
					m_materialLum = new Material(shaderLum);
					m_materialLum.set_hideFlags(61);
				}
				return m_materialLum;
			}
		}

		protected Material materialReduce
		{
			get
			{
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				//IL_0022: Expected O, but got Unknown
				if (m_materialReduce == null)
				{
					m_materialReduce = new Material(shaderReduce);
					m_materialReduce.set_hideFlags(61);
				}
				return m_materialReduce;
			}
		}

		protected Material materialAdapt
		{
			get
			{
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				//IL_0022: Expected O, but got Unknown
				if (m_materialAdapt == null)
				{
					m_materialAdapt = new Material(shaderAdapt);
					m_materialAdapt.set_hideFlags(61);
				}
				return m_materialAdapt;
			}
		}

		protected Material materialApply
		{
			get
			{
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				//IL_0022: Expected O, but got Unknown
				if (m_materialApply == null)
				{
					m_materialApply = new Material(shaderApply);
					m_materialApply.set_hideFlags(61);
				}
				return m_materialApply;
			}
		}

		public ContrastStretch()
			: this()
		{
		}

		private void Start()
		{
			if (!SystemInfo.get_supportsImageEffects())
			{
				this.set_enabled(false);
			}
			else if (!shaderAdapt.get_isSupported() || !shaderApply.get_isSupported() || !shaderLum.get_isSupported() || !shaderReduce.get_isSupported())
			{
				this.set_enabled(false);
			}
		}

		private void OnEnable()
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			for (int i = 0; i < 2; i++)
			{
				if (!Object.op_Implicit(adaptRenderTex[i]))
				{
					adaptRenderTex[i] = new RenderTexture(1, 1, 0);
					adaptRenderTex[i].set_hideFlags(61);
				}
			}
		}

		private void OnDisable()
		{
			for (int i = 0; i < 2; i++)
			{
				Object.DestroyImmediate(adaptRenderTex[i]);
				adaptRenderTex[i] = null;
			}
			if (Object.op_Implicit(m_materialLum))
			{
				Object.DestroyImmediate(m_materialLum);
			}
			if (Object.op_Implicit(m_materialReduce))
			{
				Object.DestroyImmediate(m_materialReduce);
			}
			if (Object.op_Implicit(m_materialAdapt))
			{
				Object.DestroyImmediate(m_materialAdapt);
			}
			if (Object.op_Implicit(m_materialApply))
			{
				Object.DestroyImmediate(m_materialApply);
			}
		}

		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			RenderTexture val = RenderTexture.GetTemporary(source.get_width(), source.get_height());
			Graphics.Blit(source, val, materialLum);
			while (val.get_width() > 1 || val.get_height() > 1)
			{
				int num = val.get_width() / 2;
				if (num < 1)
				{
					num = 1;
				}
				int num2 = val.get_height() / 2;
				if (num2 < 1)
				{
					num2 = 1;
				}
				RenderTexture temporary = RenderTexture.GetTemporary(num, num2);
				Graphics.Blit(val, temporary, materialReduce);
				RenderTexture.ReleaseTemporary(val);
				val = temporary;
			}
			CalculateAdaptation(val);
			materialApply.SetTexture("_AdaptTex", adaptRenderTex[curAdaptIndex]);
			Graphics.Blit(source, destination, materialApply);
			RenderTexture.ReleaseTemporary(val);
		}

		private void CalculateAdaptation(Texture curTexture)
		{
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			int num = curAdaptIndex;
			curAdaptIndex = (curAdaptIndex + 1) % 2;
			float num2 = 1f - Mathf.Pow(1f - adaptationSpeed, 30f * Time.get_deltaTime());
			num2 = Mathf.Clamp(num2, 0.01f, 1f);
			materialAdapt.SetTexture("_CurTex", curTexture);
			materialAdapt.SetVector("_AdaptParams", new Vector4(num2, limitMinimum, limitMaximum, 0f));
			Graphics.SetRenderTarget(adaptRenderTex[curAdaptIndex]);
			GL.Clear(false, true, Color.get_black());
			Graphics.Blit(adaptRenderTex[num], adaptRenderTex[curAdaptIndex], materialAdapt);
		}
	}
}
