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
			return _FogColor_f;
		}
		set
		{
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
			return _GlobalRimColor;
		}
		set
		{
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
			Shader.SetGlobalFloat("_light_probe", (_light_probe = value) ? 1f : 0f);
		}
	}

	public static Color lightProbeMul
	{
		get
		{
			return _LightProbeMul;
		}
		set
		{
			Shader.SetGlobalColor("_LightProbeMul", _LightProbeMul = value);
		}
	}

	public static Color lightProbeAdd
	{
		get
		{
			return _LightProbeAdd;
		}
		set
		{
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
			return _npc_ambient_color;
		}
		set
		{
			Shader.SetGlobalColor("_npc_ambient_color", _npc_ambient_color = value);
		}
	}

	public static void Initialize()
	{
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
				Shader shader = ResourceUtility.FindShader(material.shader.name + "__l");
				if (shader != null)
				{
					material.shader = shader;
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
				Shader shader = ResourceUtility.FindShader(material.shader.name + "__u");
				if (shader != null)
				{
					material.shader = shader;
				}
			});
		}
	}
}
