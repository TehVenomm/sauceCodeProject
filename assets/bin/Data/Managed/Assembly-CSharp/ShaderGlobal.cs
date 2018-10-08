using UnityEngine;

public static class ShaderGlobal
{
	private static Color _FogColor_f;

	private static float _Fog_n;

	private static float _Fog_f;

	private static float _Near_limit;

	private static float _Far_limit;

	private static Color _GlobalRimColor;

	private static bool _light_probe;

	private static Color _LightProbeMul;

	private static Color _LightProbeAdd;

	private static float _LightProbePeak;

	private static Color _npc_ambient_color;

	public static Color fogColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _FogColor_f;
		}
		set
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			Shader.SetGlobalColor("_FogColor_f", _FogColor_f = value);
		}
	}

	public static float fogNear
	{
		get
		{
			return _Fog_n;
		}
		set
		{
			Shader.SetGlobalFloat("_Fog_n", _Fog_n = value);
		}
	}

	public static float fogFar
	{
		get
		{
			return _Fog_f;
		}
		set
		{
			Shader.SetGlobalFloat("_Fog_f", _Fog_f = value);
		}
	}

	public static float fogNearLimit
	{
		get
		{
			return _Near_limit;
		}
		set
		{
			Shader.SetGlobalFloat("_Near_limit", _Near_limit = value);
		}
	}

	public static float fogFarLimit
	{
		get
		{
			return _Far_limit;
		}
		set
		{
			Shader.SetGlobalFloat("_Far_limit", _Far_limit = value);
		}
	}

	public static Color globalRimColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _GlobalRimColor;
		}
		set
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			Shader.SetGlobalColor("_GlobalRimColor", _GlobalRimColor = value);
		}
	}

	public static bool lightProbe
	{
		get
		{
			return _light_probe;
		}
		set
		{
			Shader.SetGlobalFloat("_light_probe", (!(_light_probe = value)) ? 0f : 1f);
		}
	}

	public static Color lightProbeMul
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _LightProbeMul;
		}
		set
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			Shader.SetGlobalColor("_LightProbeMul", _LightProbeMul = value);
		}
	}

	public static Color lightProbeAdd
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _LightProbeAdd;
		}
		set
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			Shader.SetGlobalColor("_LightProbeAdd", _LightProbeAdd = value);
		}
	}

	public static float lightProbePeak
	{
		get
		{
			return _LightProbePeak;
		}
		set
		{
			Shader.SetGlobalFloat("_LightProbePeak", _LightProbePeak = value);
		}
	}

	public static Color npcAmbientColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _npc_ambient_color;
		}
		set
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			Shader.SetGlobalColor("_npc_ambient_color", _npc_ambient_color = value);
		}
	}

	public static void Initialize()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		fogColor = new Color(0.75f, 0.8f, 1f, 1f);
		fogNear = 0f;
		fogFar = 40f;
		fogNearLimit = 0f;
		fogFarLimit = 1f;
		globalRimColor = new Color(1f, 1f, 1f, 1f);
		lightProbe = false;
		lightProbeMul = new Color(1f, 1f, 1f, 1f);
		lightProbeAdd = new Color(0f, 0f, 0f, 0f);
		lightProbePeak = 2f;
		npcAmbientColor = new Color(1f, 1f, 1f, 0f);
	}

	public static bool IsWantLightweight()
	{
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType >= 1 && !FieldManager.IsValidInGameNoQuest())
		{
			return false;
		}
		return true;
	}

	public static SHADER_TYPE GetCharacterShaderType()
	{
		if (IsWantLightweight())
		{
			return SHADER_TYPE.LIGHTWEIGHT;
		}
		return SHADER_TYPE.NORMAL;
	}

	public static void ChangeWantLightweightShader(Renderer[] renderers)
	{
		if (!renderers.IsNullOrEmpty())
		{
			Utility.MaterialForEach(renderers, delegate(Material material)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				Shader val = ResourceUtility.FindShader(material.get_shader().get_name() + "__l");
				if (val != null)
				{
					material.set_shader(val);
				}
			});
		}
	}

	public static void ChangeWantUIShader(Renderer[] renderers)
	{
		if (!renderers.IsNullOrEmpty())
		{
			Utility.MaterialForEach(renderers, delegate(Material material)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				Shader val = ResourceUtility.FindShader(material.get_shader().get_name() + "__u");
				if (val != null)
				{
					material.set_shader(val);
				}
			});
		}
	}
}
