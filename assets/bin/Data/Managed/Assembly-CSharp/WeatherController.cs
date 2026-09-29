// Decompiled with JetBrains decompiler
// Type: WeatherController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class WeatherController
{
  private WeatherController.ShaderSettings originalSettings = new WeatherController.ShaderSettings();
  [SerializeField]
  private WeatherController.ShaderSettings afterShaderSettings = new WeatherController.ShaderSettings();
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
      if (Object.op_Equality((Object) this._skyDome, (Object) null) && MonoBehaviourSingleton<StageManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.skyObject, (Object) null))
        this._skyDome = ((Component) MonoBehaviourSingleton<StageManager>.I.skyObject).GetComponent<SkyDomeWeatherController>();
      return this._skyDome;
    }
  }

  public void Init()
  {
    this.LIGHTMAPBLEND_PARAMTER_KEY = Shader.PropertyToID("_LightMapBlend");
    if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
    {
      this.originalSettings.fogColor = MonoBehaviourSingleton<SceneSettingsManager>.I.fogColor;
      this.originalSettings.linearFogStart = MonoBehaviourSingleton<SceneSettingsManager>.I.linearFogStart;
      this.originalSettings.linearFogEnd = MonoBehaviourSingleton<SceneSettingsManager>.I.linearFogEnd;
      this.originalSettings.limitFogStart = MonoBehaviourSingleton<SceneSettingsManager>.I.limitFogStart;
      this.originalSettings.limitFogEnd = MonoBehaviourSingleton<SceneSettingsManager>.I.limitFogEnd;
      this.originalSettings.rimColor = MonoBehaviourSingleton<SceneSettingsManager>.I.rimColor;
      this.originalSettings.lightProbeMul = MonoBehaviourSingleton<SceneSettingsManager>.I.lightProbeMul;
      this.originalSettings.lightProbeAdd = MonoBehaviourSingleton<SceneSettingsManager>.I.lightProbeAdd;
      this.originalSettings.lightProbePeak = MonoBehaviourSingleton<SceneSettingsManager>.I.lightProbePeak;
      this.originalSettings.npcAmbientColor = MonoBehaviourSingleton<SceneSettingsManager>.I.npcAmbientColor;
    }
    if (!Application.isPlaying)
      return;
    this.Update(0.0f);
    if (this.enableObjects == null)
      return;
    for (int index = 0; index < this.enableObjects.Length; ++index)
      this.enableObjects[index].SetActive(false);
  }

  public void Update(float rate)
  {
    rate = Mathf.Clamp01(rate);
    ShaderGlobal.fogColor = Color.Lerp(this.originalSettings.fogColor, this.afterShaderSettings.fogColor, rate);
    ShaderGlobal.fogNear = Mathf.Lerp(this.originalSettings.linearFogStart, this.afterShaderSettings.linearFogStart, rate);
    ShaderGlobal.fogFar = Mathf.Lerp(this.originalSettings.linearFogEnd, this.afterShaderSettings.linearFogEnd, rate);
    ShaderGlobal.fogNearLimit = Mathf.Lerp(this.originalSettings.limitFogStart, this.afterShaderSettings.limitFogStart, rate);
    ShaderGlobal.fogFarLimit = Mathf.Lerp(this.originalSettings.limitFogEnd, this.afterShaderSettings.limitFogEnd, rate);
    ShaderGlobal.globalRimColor = Color.Lerp(this.originalSettings.rimColor, this.afterShaderSettings.rimColor, rate);
    ShaderGlobal.lightProbeMul = Color.Lerp(this.originalSettings.lightProbeMul, this.afterShaderSettings.lightProbeMul, rate);
    ShaderGlobal.lightProbeAdd = Color.Lerp(this.originalSettings.lightProbeAdd, this.afterShaderSettings.lightProbeAdd, rate);
    ShaderGlobal.lightProbePeak = Mathf.Lerp(this.originalSettings.lightProbePeak, this.afterShaderSettings.lightProbePeak, rate);
    ShaderGlobal.npcAmbientColor = Color.Lerp(this.originalSettings.npcAmbientColor, this.afterShaderSettings.npcAmbientColor, rate);
    if (this.blendLightMapRenderer != null)
    {
      for (int index = 0; index < this.blendLightMapRenderer.Length; ++index)
      {
        if (!Object.op_Equality((Object) this.blendLightMapRenderer[index], (Object) null))
        {
          foreach (Material sharedMaterial in this.blendLightMapRenderer[index].sharedMaterials)
          {
            if (!Object.op_Equality((Object) sharedMaterial, (Object) null) && sharedMaterial.HasProperty(this.LIGHTMAPBLEND_PARAMTER_KEY))
              sharedMaterial.SetFloat(this.LIGHTMAPBLEND_PARAMTER_KEY, rate);
          }
        }
      }
    }
    if (!Object.op_Inequality((Object) this.skyDome, (Object) null))
      return;
    this.skyDome.UpdateRenderers(rate);
  }

  public void OnStartWeatherChange()
  {
    if (MonoBehaviourSingleton<StageManager>.IsValid())
    {
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect, (Object) null))
        ((Component) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect).gameObject.SetActive(!this.cameraLinkEffectEnable);
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0, (Object) null))
        ((Component) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0).gameObject.SetActive(!this.cameraLinkEffectY0Enable);
    }
    if (this.disableObjects == null)
      return;
    for (int index = 0; index < this.disableObjects.Length; ++index)
      this.disableObjects[index].SetActive(false);
  }

  public void OnFinishedWeatherChange()
  {
    if (MonoBehaviourSingleton<StageManager>.IsValid())
    {
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect, (Object) null))
        ((Component) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect).gameObject.SetActive(this.cameraLinkEffectEnable);
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0, (Object) null))
        ((Component) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0).gameObject.SetActive(this.cameraLinkEffectY0Enable);
    }
    if (this.enableObjects == null)
      return;
    for (int index = 0; index < this.enableObjects.Length; ++index)
      this.enableObjects[index].SetActive(true);
  }

  public void OnStartReturnToOriginal()
  {
    if (MonoBehaviourSingleton<StageManager>.IsValid())
    {
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect, (Object) null))
        ((Component) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffect).gameObject.SetActive(!this.cameraLinkEffectEnable);
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0, (Object) null))
        ((Component) MonoBehaviourSingleton<StageManager>.I.cameraLinkEffectY0).gameObject.SetActive(!this.cameraLinkEffectY0Enable);
    }
    if (this.enableObjects == null)
      return;
    for (int index = 0; index < this.enableObjects.Length; ++index)
      this.enableObjects[index].SetActive(false);
  }

  public void OnFinishedReturnToOriginal()
  {
    if (this.disableObjects == null)
      return;
    for (int index = 0; index < this.disableObjects.Length; ++index)
      this.disableObjects[index].SetActive(true);
  }

  [Serializable]
  private class ShaderSettings
  {
    public Color fogColor = Color.white;
    public float linearFogStart;
    public float linearFogEnd = 300f;
    public Color rimColor = new Color(1f, 1f, 1f, 1f);
    public float limitFogStart;
    public float limitFogEnd = 1f;
    public Color lightProbeMul = new Color(1f, 1f, 1f, 1f);
    public Color lightProbeAdd = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public float lightProbePeak = 2f;
    public Color npcAmbientColor = new Color(1f, 1f, 1f, 0.0f);
  }
}
