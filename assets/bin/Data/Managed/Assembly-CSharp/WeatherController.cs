using System;
using UnityEngine;

[Serializable]
public class WeatherController
{
	[Serializable]
	private class ShaderSettings
	{
		public Color fogColor = Color.get_white();

		public float linearFogStart;

		public float linearFogEnd = 300f;

		public Color rimColor = new Color(1f, 1f, 1f, 1f);

		public float limitFogStart;

		public float limitFogEnd = 1f;

		public Color lightProbeMul = new Color(1f, 1f, 1f, 1f);

		public Color lightProbeAdd = new Color(0f, 0f, 0f, 0f);

		public float lightProbePeak = 2f;

		public Color npcAmbientColor = new Color(1f, 1f, 1f, 0f);
	}

	private ShaderSettings originalSettings = new ShaderSettings();

	[SerializeField]
	private ShaderSettings afterShaderSettings = new ShaderSettings();

	[SerializeField]
	private Renderer[] blendLightMapRenderer;

	public bool cameraLinkEffectEnable;

	public bool cameraLinkEffectY0Enable;

	public GameObject[] disableObjects;

	public GameObject[] enableObjects;

	private int LIGHTMAPBLEND_PARAMTER_KEY;

	private SkyDomeWeatherController _skyDome;

	private SkyDomeWeatherController skyDome
	{
		get
		{
			if (_skyDome == null && MonoBehaviourSingleton<StageManager>.IsValid() && MonoBehaviourSingleton<StageManager>.I.skyObject != null)
			{
				_skyDome = MonoBehaviourSingleton<StageManager>.I.skyObject.GetComponent<SkyDomeWeatherController>();
			}
			return _skyDome;
		}
	}

	public void Init()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		LIGHTMAPBLEND_PARAMTER_KEY = Shader.PropertyToID("_LightMapBlend");
		if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
		{
			originalSettings.fogColor = MonoBehaviourSingleton<SceneSettingsManager>.I.fogColor;
			originalSettings.linearFogStart = MonoBehaviourSingleton<SceneSettingsManager>.I.linearFogStart;
			originalSettings.linearFogEnd = MonoBehaviourSingleton<SceneSettingsManager>.I.linearFogEnd;
			originalSettings.limitFogStart = MonoBehaviourSingleton<SceneSettingsManager>.I.limitFogStart;
			originalSettings.limitFogEnd = MonoBehaviourSingleton<SceneSettingsManager>.I.limitFogEnd;
			originalSettings.rimColor = MonoBehaviourSingleton<SceneSettingsManager>.I.rimColor;
			originalSettings.lightProbeMul = MonoBehaviourSingleton<SceneSettingsManager>.I.lightProbeMul;
			originalSettings.lightProbeAdd = MonoBehaviourSingleton<SceneSettingsManager>.I.lightProbeAdd;
			originalSettings.lightProbePeak = MonoBehaviourSingleton<SceneSettingsManager>.I.lightProbePeak;
			originalSettings.npcAmbientColor = MonoBehaviourSingleton<SceneSettingsManager>.I.npcAmbientColor;
		}
		if (!Application.get_isPlaying())
		{
			return;
		}
		Update(0f);
		if (enableObjects != null)
		{
			for (int i = 0; i < enableObjects.Length; i++)
			{
				enableObjects[i].SetActive(false);
			}
		}
	}

	public void Update(float rate)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		rate = Mathf.Clamp01(rate);
		ShaderGlobal.fogColor = Color.Lerp(originalSettings.fogColor, afterShaderSettings.fogColor, rate);
		ShaderGlobal.fogNear = Mathf.Lerp(originalSettings.linearFogStart, afterShaderSettings.linearFogStart, rate);
		ShaderGlobal.fogFar = Mathf.Lerp(originalSettings.linearFogEnd, afterShaderSettings.linearFogEnd, rate);
		ShaderGlobal.fogNearLimit = Mathf.Lerp(originalSettings.limitFogStart, afterShaderSettings.limitFogStart, rate);
		ShaderGlobal.fogFarLimit = Mathf.Lerp(originalSettings.limitFogEnd, afterShaderSettings.limitFogEnd, rate);
		ShaderGlobal.globalRimColor = Color.Lerp(originalSettings.rimColor, afterShaderSettings.rimColor, rate);
		ShaderGlobal.lightProbeMul = Color.Lerp(originalSettings.lightProbeMul, afterShaderSettings.lightProbeMul, rate);
		ShaderGlobal.lightProbeAdd = Color.Lerp(originalSettings.lightProbeAdd, afterShaderSettings.lightProbeAdd, rate);
		ShaderGlobal.lightProbePeak = Mathf.Lerp(originalSettings.lightProbePeak, afterShaderSettings.lightProbePeak, rate);
		ShaderGlobal.npcAmbientColor = Color.Lerp(originalSettings.npcAmbientColor, afterShaderSettings.npcAmbientColor, rate);
		if (blendLightMapRenderer != null)
		{
			for (int i = 0; i < blendLightMapRenderer.Length; i++)
			{
				if (blendLightMapRenderer[i] == null)
				{
					continue;
				}
				Material[] sharedMaterials = blendLightMapRenderer[i].get_sharedMaterials();
				foreach (Material val in sharedMaterials)
				{
					if (!(val == null) && val.HasProperty(LIGHTMAPBLEND_PARAMTER_KEY))
					{
						val.SetFloat(LIGHTMAPBLEND_PARAMTER_KEY, rate);
					}
				}
			}
		}
		if (skyDome != null)
		{
			skyDome.UpdateRenderers(rate);
		}
	}

	public void OnStartWeatherChange()
	{
		if (MonoBehaviourSingleton<StageManager>.IsValid())
		{
			if (MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect != null)
			{
				MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect.get_gameObject().SetActive(!cameraLinkEffectEnable);
			}
			if (MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0 != null)
			{
				MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0.get_gameObject().SetActive(!cameraLinkEffectY0Enable);
			}
		}
		if (disableObjects != null)
		{
			for (int i = 0; i < disableObjects.Length; i++)
			{
				disableObjects[i].SetActive(false);
			}
		}
	}

	public void OnFinishedWeatherChange()
	{
		if (MonoBehaviourSingleton<StageManager>.IsValid())
		{
			if (MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect != null)
			{
				MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect.get_gameObject().SetActive(cameraLinkEffectEnable);
			}
			if (MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0 != null)
			{
				MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0.get_gameObject().SetActive(cameraLinkEffectY0Enable);
			}
		}
		if (enableObjects != null)
		{
			for (int i = 0; i < enableObjects.Length; i++)
			{
				enableObjects[i].SetActive(true);
			}
		}
	}

	public void OnStartReturnToOriginal()
	{
		if (MonoBehaviourSingleton<StageManager>.IsValid())
		{
			if (MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect != null)
			{
				MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect.get_gameObject().SetActive(!cameraLinkEffectEnable);
			}
			if (MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0 != null)
			{
				MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0.get_gameObject().SetActive(!cameraLinkEffectY0Enable);
			}
		}
		if (enableObjects != null)
		{
			for (int i = 0; i < enableObjects.Length; i++)
			{
				enableObjects[i].SetActive(false);
			}
		}
	}

	public void OnFinishedReturnToOriginal()
	{
		if (disableObjects != null)
		{
			for (int i = 0; i < disableObjects.Length; i++)
			{
				disableObjects[i].SetActive(true);
			}
		}
	}
}
