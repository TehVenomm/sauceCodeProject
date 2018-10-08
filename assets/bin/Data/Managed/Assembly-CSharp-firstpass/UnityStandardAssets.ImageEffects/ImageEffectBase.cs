using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(Camera))]
	public class ImageEffectBase
	{
		public Shader shader;

		private Material m_Material;

		protected Material material
		{
			get
			{
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Expected O, but got Unknown
				if (m_Material == null)
				{
					m_Material = new Material(shader);
					m_Material.set_hideFlags(61);
				}
				return m_Material;
			}
		}

		public ImageEffectBase()
			: this()
		{
		}

		protected virtual void Start()
		{
			if (!SystemInfo.get_supportsImageEffects())
			{
				this.set_enabled(false);
			}
			else if (!Object.op_Implicit(shader) || !shader.get_isSupported())
			{
				this.set_enabled(false);
			}
		}

		protected virtual void OnDisable()
		{
			if (Object.op_Implicit(m_Material))
			{
				Object.DestroyImmediate(m_Material);
			}
		}
	}
}
