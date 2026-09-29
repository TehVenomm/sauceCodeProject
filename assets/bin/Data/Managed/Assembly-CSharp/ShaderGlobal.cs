// Decompiled with JetBrains decompiler
// Type: ShaderGlobal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
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

  public static void Initialize()
  {
    ShaderGlobal.fogColor = new Color(0.75f, 0.8f, 1f, 1f);
    ShaderGlobal.fogNear = 0.0f;
    ShaderGlobal.fogFar = 40f;
    ShaderGlobal.fogNearLimit = 0.0f;
    ShaderGlobal.fogFarLimit = 1f;
    ShaderGlobal.globalRimColor = new Color(1f, 1f, 1f, 1f);
    ShaderGlobal.lightProbe = false;
    ShaderGlobal.lightProbeMul = new Color(1f, 1f, 1f, 1f);
    ShaderGlobal.lightProbeAdd = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    ShaderGlobal.lightProbePeak = 2f;
    ShaderGlobal.npcAmbientColor = new Color(1f, 1f, 1f, 0.0f);
  }

  public static Color fogColor
  {
    get => ShaderGlobal._FogColor_f;
    set => Shader.SetGlobalColor("_FogColor_f", ShaderGlobal._FogColor_f = value);
  }

  public static float fogNear
  {
    get => ShaderGlobal._Fog_n;
    set => Shader.SetGlobalFloat("_Fog_n", ShaderGlobal._Fog_n = value);
  }

  public static float fogFar
  {
    get => ShaderGlobal._Fog_f;
    set => Shader.SetGlobalFloat("_Fog_f", ShaderGlobal._Fog_f = value);
  }

  public static float fogNearLimit
  {
    get => ShaderGlobal._Near_limit;
    set => Shader.SetGlobalFloat("_Near_limit", ShaderGlobal._Near_limit = value);
  }

  public static float fogFarLimit
  {
    get => ShaderGlobal._Far_limit;
    set => Shader.SetGlobalFloat("_Far_limit", ShaderGlobal._Far_limit = value);
  }

  public static Color globalRimColor
  {
    get => ShaderGlobal._GlobalRimColor;
    set => Shader.SetGlobalColor("_GlobalRimColor", ShaderGlobal._GlobalRimColor = value);
  }

  public static bool lightProbe
  {
    get => ShaderGlobal._light_probe;
    set => Shader.SetGlobalFloat("_light_probe", (ShaderGlobal._light_probe = value) ? 1f : 0.0f);
  }

  public static Color lightProbeMul
  {
    get => ShaderGlobal._LightProbeMul;
    set => Shader.SetGlobalColor("_LightProbeMul", ShaderGlobal._LightProbeMul = value);
  }

  public static Color lightProbeAdd
  {
    get => ShaderGlobal._LightProbeAdd;
    set => Shader.SetGlobalColor("_LightProbeAdd", ShaderGlobal._LightProbeAdd = value);
  }

  public static float lightProbePeak
  {
    get => ShaderGlobal._LightProbePeak;
    set => Shader.SetGlobalFloat("_LightProbePeak", ShaderGlobal._LightProbePeak = value);
  }

  public static Color npcAmbientColor
  {
    get => ShaderGlobal._npc_ambient_color;
    set => Shader.SetGlobalColor("_npc_ambient_color", ShaderGlobal._npc_ambient_color = value);
  }

  public static bool IsWantLightweight()
  {
    return !MonoBehaviourSingleton<InGameManager>.IsValid() || MonoBehaviourSingleton<InGameManager>.I.graphicOptionType < 1 || FieldManager.IsValidInGameNoQuest();
  }

  public static SHADER_TYPE GetCharacterShaderType()
  {
    return ShaderGlobal.IsWantLightweight() ? SHADER_TYPE.LIGHTWEIGHT : SHADER_TYPE.NORMAL;
  }

  public static void ChangeWantLightweightShader(Renderer[] renderers)
  {
    if (((IList<Renderer>) renderers).IsNullOrEmpty<Renderer>())
      return;
    Utility.MaterialForEach(renderers, (Action<Material>) (material =>
    {
      Shader shader = ResourceUtility.FindShader(((Object) material.shader).name + "__l");
      if (!Object.op_Inequality((Object) shader, (Object) null))
        return;
      material.shader = shader;
    }));
  }

  public static void ChangeWantUIShader(Renderer[] renderers)
  {
    if (((IList<Renderer>) renderers).IsNullOrEmpty<Renderer>())
      return;
    Utility.MaterialForEach(renderers, (Action<Material>) (material =>
    {
      Shader shader = ResourceUtility.FindShader(((Object) material.shader).name + "__u");
      if (!Object.op_Inequality((Object) shader, (Object) null))
        return;
      material.shader = shader;
    }));
  }
}
