using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class PostEffectsBase : MonoBehaviour
	{
		protected bool supportHDRTextures = true;

		protected bool supportDX11;

		protected bool isSupported = true;

		public PostEffectsBase()
			: this()
		{
		}

		protected Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
		{
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Expected O, but got Unknown
			if (!Object.op_Implicit(s))
			{
				Debug.Log((object)("Missing shader in " + ((object)this).ToString()));
				this.set_enabled(false);
				return null;
			}
			if (s.get_isSupported() && Object.op_Implicit(m2Create) && m2Create.get_shader() == s)
			{
				return m2Create;
			}
			if (!s.get_isSupported())
			{
				NotSupported();
				Debug.Log((object)("The shader " + ((object)s).ToString() + " on effect " + ((object)this).ToString() + " is not supported on this platform!"));
				return null;
			}
			m2Create = new Material(s);
			m2Create.set_hideFlags(52);
			if (Object.op_Implicit(m2Create))
			{
				return m2Create;
			}
			return null;
		}

		protected Material CreateMaterial(Shader s, Material m2Create)
		{
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			if (!Object.op_Implicit(s))
			{
				Debug.Log((object)("Missing shader in " + ((object)this).ToString()));
				return null;
			}
			if (Object.op_Implicit(m2Create) && m2Create.get_shader() == s && s.get_isSupported())
			{
				return m2Create;
			}
			if (!s.get_isSupported())
			{
				return null;
			}
			m2Create = new Material(s);
			m2Create.set_hideFlags(52);
			if (Object.op_Implicit(m2Create))
			{
				return m2Create;
			}
			return null;
		}

		private void OnEnable()
		{
			isSupported = true;
		}

		protected bool CheckSupport()
		{
			return CheckSupport(needDepth: false);
		}

		public virtual bool CheckResources()
		{
			Debug.LogWarning((object)("CheckResources () for " + ((object)this).ToString() + " should be overwritten."));
			return isSupported;
		}

		protected void Start()
		{
			CheckResources();
		}

		protected bool CheckSupport(bool needDepth)
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			isSupported = true;
			supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(2);
			supportDX11 = (SystemInfo.get_graphicsShaderLevel() >= 50 && SystemInfo.get_supportsComputeShaders());
			if (!SystemInfo.get_supportsImageEffects() || !SystemInfo.get_supportsRenderTextures())
			{
				NotSupported();
				return false;
			}
			if (needDepth && !SystemInfo.SupportsRenderTextureFormat(1))
			{
				NotSupported();
				return false;
			}
			if (needDepth)
			{
				Camera component = this.GetComponent<Camera>();
				component.set_depthTextureMode(component.get_depthTextureMode() | 1);
			}
			return true;
		}

		protected bool CheckSupport(bool needDepth, bool needHdr)
		{
			if (!CheckSupport(needDepth))
			{
				return false;
			}
			if (needHdr && !supportHDRTextures)
			{
				NotSupported();
				return false;
			}
			return true;
		}

		public bool Dx11Support()
		{
			return supportDX11;
		}

		protected void ReportAutoDisable()
		{
			Debug.LogWarning((object)("The image effect " + ((object)this).ToString() + " has been disabled as it's not supported on the current platform."));
		}

		private bool CheckShader(Shader s)
		{
			Debug.Log((object)("The shader " + ((object)s).ToString() + " on effect " + ((object)this).ToString() + " is not part of the Unity 3.2+ effects suite anymore. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects (Pro only) package."));
			if (!s.get_isSupported())
			{
				NotSupported();
				return false;
			}
			return false;
		}

		protected void NotSupported()
		{
			this.set_enabled(false);
			isSupported = false;
		}

		protected void DrawBorder(RenderTexture dest, Material material)
		{
			RenderTexture.set_active(dest);
			bool flag = true;
			GL.PushMatrix();
			GL.LoadOrtho();
			for (int i = 0; i < material.get_passCount(); i++)
			{
				material.SetPass(i);
				float num;
				float num2;
				if (flag)
				{
					num = 1f;
					num2 = 0f;
				}
				else
				{
					num = 0f;
					num2 = 1f;
				}
				float num3 = 0f;
				float num4 = 1f / ((float)dest.get_width() * 1f);
				float num5 = 0f;
				float num6 = 1f;
				GL.Begin(7);
				GL.TexCoord2(0f, num);
				GL.Vertex3(num3, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num3, num6, 0.1f);
				num3 = 1f - 1f / ((float)dest.get_width() * 1f);
				num4 = 1f;
				num5 = 0f;
				num6 = 1f;
				GL.TexCoord2(0f, num);
				GL.Vertex3(num3, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num3, num6, 0.1f);
				num3 = 0f;
				num4 = 1f;
				num5 = 0f;
				num6 = 1f / ((float)dest.get_height() * 1f);
				GL.TexCoord2(0f, num);
				GL.Vertex3(num3, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num3, num6, 0.1f);
				num3 = 0f;
				num4 = 1f;
				num5 = 1f - 1f / ((float)dest.get_height() * 1f);
				num6 = 1f;
				GL.TexCoord2(0f, num);
				GL.Vertex3(num3, num5, 0.1f);
				GL.TexCoord2(1f, num);
				GL.Vertex3(num4, num5, 0.1f);
				GL.TexCoord2(1f, num2);
				GL.Vertex3(num4, num6, 0.1f);
				GL.TexCoord2(0f, num2);
				GL.Vertex3(num3, num6, 0.1f);
				GL.End();
			}
			GL.PopMatrix();
		}
	}
}
