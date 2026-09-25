// Decompiled with JetBrains decompiler
// Type: BlendColorCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BlendColorCtrl
{
  public const string kDefaultShaderName = "enemy_custamaizable_blend";
  public const string kDefaultPropetyName = "_BlendColor";
  private const int kColorElementNum = 3;
  private Color cacheColor = Color.white;
  private List<Material> materialList = new List<Material>();
  private Dictionary<string, BlendColorCtrl.ShaderParam> shaderParams = new Dictionary<string, BlendColorCtrl.ShaderParam>();

  public void Enable(AnimEventData.EventData data, bool enable, SkinnedMeshRenderer[] renderers)
  {
    string key = "enemy_custamaizable_blend";
    if (!((IList<string>) data.stringArgs).IsNullOrEmpty<string>())
      key = data.stringArgs[0];
    BlendColorCtrl.ShaderParam shaderParam;
    if (this.shaderParams.ContainsKey(key))
    {
      shaderParam = this.shaderParams[key];
    }
    else
    {
      shaderParam = new BlendColorCtrl.ShaderParam();
      shaderParam.Init();
      this.shaderParams.Add(key, shaderParam);
    }
    shaderParam.shaderName = key;
    shaderParam.isBlendEnable = true;
    shaderParam.blendEnable = enable;
    int index1 = 0;
    for (int length1 = renderers.Length; index1 < length1; ++index1)
    {
      int index2 = 0;
      for (int length2 = ((Renderer) renderers[index1]).materials.Length; index2 < length2; ++index2)
      {
        Material material = ((Renderer) renderers[index1]).materials[index2];
        if (((Object) material.shader).name.Contains(key))
          material.SetFloat("_BlendEnable", enable ? 1f : 0.0f);
      }
    }
  }

  public void Change(AnimEventData.EventData data, SkinnedMeshRenderer[] renderers)
  {
    if (((IList<SkinnedMeshRenderer>) renderers).IsNullOrEmpty<SkinnedMeshRenderer>())
      return;
    string key = "enemy_custamaizable_blend";
    string str = "_BlendColor";
    if (!((IList<string>) data.stringArgs).IsNullOrEmpty<string>())
    {
      key = data.stringArgs[0];
      if (data.stringArgs.Length > 1)
        str = data.stringArgs[1];
    }
    BlendColorCtrl.ShaderParam shaderParam;
    if (this.shaderParams.ContainsKey(key))
    {
      shaderParam = this.shaderParams[key];
    }
    else
    {
      shaderParam = new BlendColorCtrl.ShaderParam();
      shaderParam.Init();
      this.shaderParams.Add(key, shaderParam);
    }
    shaderParam.shaderName = key;
    shaderParam.propetyName = str;
    for (int index = 0; index < 3; ++index)
    {
      bool flag = false;
      float num = (float) data.intArgs[index] / (float) byte.MaxValue;
      if ((double) num < 0.0)
      {
        num = 0.0f;
        flag = true;
      }
      if ((double) num > 1.0)
      {
        num = 1f;
        flag = true;
      }
      shaderParam.fColor[index].isEnd = flag;
      shaderParam.fColor[index].tValue = num;
    }
    if (data.intArgs.Length > 3)
      shaderParam.forceEndFlag = data.intArgs[3] != 0;
    bool flag1 = false;
    float num1 = data.floatArgs[0];
    if ((double) num1 < -1.0)
    {
      num1 = -1f;
      flag1 = true;
    }
    if ((double) num1 > 1.0)
    {
      num1 = 1f;
      flag1 = true;
    }
    shaderParam.fBlend.isEnd = flag1;
    shaderParam.fBlend.tValue = num1;
    float floatArg = data.floatArgs[1];
    shaderParam.aliveFlag = false;
    this.materialList.Clear();
    bool flag2 = true;
    if ((double) floatArg == 0.0)
    {
      Color color = Color.white;
      int index1 = 0;
      for (int length1 = renderers.Length; index1 < length1; ++index1)
      {
        int index2 = 0;
        for (int length2 = ((Renderer) renderers[index1]).materials.Length; index2 < length2; ++index2)
        {
          Material material = ((Renderer) renderers[index1]).materials[index2];
          if (((Object) material.shader).name.Contains(shaderParam.shaderName))
          {
            if (flag2)
            {
              color = material.GetColor(shaderParam.propetyName);
              if (!shaderParam.fColor[0].isEnd)
                color.r = shaderParam.fColor[0].tValue;
              if (!shaderParam.fColor[1].isEnd)
                color.g = shaderParam.fColor[1].tValue;
              if (!shaderParam.fColor[2].isEnd)
                color.b = shaderParam.fColor[2].tValue;
              flag2 = false;
            }
            material.SetColor(shaderParam.propetyName, color);
            if (!shaderParam.fBlend.isEnd)
              material.SetFloat("_BlendRate", shaderParam.fBlend.tValue);
          }
        }
      }
    }
    else
    {
      int index3 = 0;
      for (int length3 = renderers.Length; index3 < length3; ++index3)
      {
        int index4 = 0;
        for (int length4 = ((Renderer) renderers[index3]).materials.Length; index4 < length4; ++index4)
        {
          Material material = ((Renderer) renderers[index3]).materials[index4];
          if (((Object) material.shader).name.Contains(shaderParam.shaderName))
          {
            if (flag2)
            {
              Color color = material.GetColor(shaderParam.propetyName);
              shaderParam.fColor[0].nValue = color.r;
              if (shaderParam.fColor[0].isEnd)
                shaderParam.fColor[0].tValue = shaderParam.fColor[0].nValue;
              shaderParam.fColor[1].nValue = color.g;
              if (shaderParam.fColor[1].isEnd)
                shaderParam.fColor[1].tValue = shaderParam.fColor[1].nValue;
              shaderParam.fColor[2].nValue = color.b;
              if (shaderParam.fColor[2].isEnd)
                shaderParam.fColor[2].tValue = shaderParam.fColor[2].nValue;
              shaderParam.fBlend.nValue = material.GetFloat("_BlendRate");
              if (shaderParam.fBlend.isEnd)
                shaderParam.fBlend.tValue = shaderParam.fBlend.nValue;
            }
            this.materialList.Add(material);
            break;
          }
        }
      }
      if (this.materialList.Count == 0)
      {
        Debug.LogError((object) $"not shader [{shaderParam.shaderName}]");
        return;
      }
      if ((double) shaderParam.fColor[0].nValue == (double) shaderParam.fColor[0].tValue && (double) shaderParam.fColor[1].nValue == (double) shaderParam.fColor[1].tValue && (double) shaderParam.fColor[2].nValue == (double) shaderParam.fColor[2].tValue && (double) shaderParam.fBlend.tValue == (double) shaderParam.fBlend.nValue && (double) shaderParam.fBlend.tValue >= 0.0)
        return;
      shaderParam.aliveFlag = true;
      for (int index5 = 0; index5 < 3; ++index5)
        shaderParam.fColor[index5].CalcSpeed(floatArg);
      shaderParam.fBlend.CalcSpeed(floatArg);
    }
    shaderParam.isColor = true;
    if (shaderParam.fBlend.isEnd)
      return;
    shaderParam.isBlend = true;
  }

  public void ForceEnd()
  {
    foreach (KeyValuePair<string, BlendColorCtrl.ShaderParam> shaderParam1 in this.shaderParams)
    {
      BlendColorCtrl.ShaderParam shaderParam2 = shaderParam1.Value;
      if (shaderParam2.aliveFlag && shaderParam2.forceEndFlag)
        shaderParam2.aliveFlag = false;
    }
  }

  public void Update()
  {
    if (this.materialList.IsNullOrEmpty<Material>())
      return;
    foreach (KeyValuePair<string, BlendColorCtrl.ShaderParam> shaderParam1 in this.shaderParams)
    {
      BlendColorCtrl.ShaderParam shaderParam2 = shaderParam1.Value;
      if (shaderParam2.aliveFlag)
      {
        for (int index = 0; index < 3; ++index)
          shaderParam2.fColor[index].Update();
        shaderParam2.fBlend.Update();
        this.cacheColor.r = shaderParam2.fColor[0].nValue;
        this.cacheColor.g = shaderParam2.fColor[1].nValue;
        this.cacheColor.b = shaderParam2.fColor[2].nValue;
        int index1 = 0;
        for (int count = this.materialList.Count; index1 < count; ++index1)
        {
          Material material = this.materialList[index1];
          material.SetColor(shaderParam2.propetyName, this.cacheColor);
          material.SetFloat("_BlendRate", shaderParam2.fBlend.nValue);
        }
        if (shaderParam2.fColor[0].isEnd && shaderParam2.fColor[1].isEnd && shaderParam2.fColor[2].isEnd && shaderParam2.fBlend.isEnd)
          shaderParam2.aliveFlag = false;
      }
    }
  }

  public List<BlendColorCtrl.ShaderSyncParam> GetShaderParamList()
  {
    if (this.shaderParams.Count == 0)
      return (List<BlendColorCtrl.ShaderSyncParam>) null;
    List<BlendColorCtrl.ShaderSyncParam> shaderParamList = new List<BlendColorCtrl.ShaderSyncParam>();
    foreach (KeyValuePair<string, BlendColorCtrl.ShaderParam> shaderParam in this.shaderParams)
      shaderParamList.Add(shaderParam.Value.GetSyncParam());
    return shaderParamList;
  }

  public void Sync(
    SkinnedMeshRenderer[] renderers,
    List<BlendColorCtrl.ShaderSyncParam> shaderParamList)
  {
    if (((IList<SkinnedMeshRenderer>) renderers).IsNullOrEmpty<SkinnedMeshRenderer>() || shaderParamList.IsNullOrEmpty<BlendColorCtrl.ShaderSyncParam>())
      return;
    for (int index1 = 0; index1 < shaderParamList.Count; ++index1)
    {
      BlendColorCtrl.ShaderSyncParam shaderParam1 = shaderParamList[index1];
      BlendColorCtrl.ShaderParam shaderParam2;
      if (this.shaderParams.ContainsKey(shaderParam1.shaderName))
      {
        shaderParam2 = this.shaderParams[shaderParam1.shaderName];
      }
      else
      {
        shaderParam2 = new BlendColorCtrl.ShaderParam();
        shaderParam2.Init();
        shaderParam2.shaderName = shaderParam1.shaderName;
        shaderParam2.propetyName = shaderParam1.propetyName;
        this.shaderParams.Add(shaderParam1.shaderName, shaderParam2);
      }
      shaderParam2.shaderName = shaderParam1.shaderName;
      shaderParam2.propetyName = shaderParam1.propetyName;
      shaderParam2.isColor = shaderParam1.isColor;
      if (shaderParam2.isColor)
      {
        shaderParam2.fColor[0].tValue = shaderParam1.color.r;
        shaderParam2.fColor[0].isEnd = true;
        shaderParam2.fColor[1].tValue = shaderParam1.color.g;
        shaderParam2.fColor[1].isEnd = true;
        shaderParam2.fColor[2].tValue = shaderParam1.color.b;
        shaderParam2.fColor[2].isEnd = true;
      }
      shaderParam2.isBlend = shaderParam1.isBlendRate;
      if (shaderParam2.isBlend)
      {
        shaderParam2.fBlend.tValue = shaderParam1.blendRate;
        shaderParam2.fBlend.isEnd = true;
      }
      shaderParam2.isBlendEnable = shaderParam1.isBlendEnable;
      shaderParam2.blendEnable = shaderParam1.blendEnable;
      int index2 = 0;
      for (int length1 = renderers.Length; index2 < length1; ++index2)
      {
        int index3 = 0;
        for (int length2 = ((Renderer) renderers[index2]).materials.Length; index3 < length2; ++index3)
        {
          Material material = ((Renderer) renderers[index2]).materials[index3];
          if (((Object) material.shader).name.Contains(shaderParam2.shaderName))
          {
            if (shaderParam2.isColor)
              material.SetColor(shaderParam2.propetyName, shaderParam1.color);
            if (shaderParam2.isBlend)
              material.SetFloat("_BlendRate", shaderParam1.blendRate);
            if (shaderParam2.isBlendEnable)
              material.SetFloat("_BlendEnable", shaderParam2.blendEnable ? 1f : 0.0f);
          }
        }
      }
    }
  }

  private class floatUpdater
  {
    public float nValue;
    public float tValue;
    public float speed;
    public bool isEnd;

    public void CalcSpeed(float sec) => this.speed = (this.tValue - this.nValue) / sec;

    public void Update()
    {
      if (this.isEnd)
        return;
      float num = this.speed * Time.deltaTime;
      this.nValue += num;
      if ((double) num > 0.0)
      {
        if ((double) this.nValue < (double) this.tValue)
          return;
        this.nValue = this.tValue;
        this.isEnd = true;
      }
      else
      {
        if ((double) this.nValue > (double) this.tValue)
          return;
        this.nValue = this.tValue;
        this.isEnd = true;
      }
    }
  }

  private class ShaderParam
  {
    public string shaderName = "";
    public string propetyName = "";
    public bool forceEndFlag;
    public bool aliveFlag;
    public bool isColor;
    public BlendColorCtrl.floatUpdater[] fColor = new BlendColorCtrl.floatUpdater[3];
    public bool isBlend;
    public BlendColorCtrl.floatUpdater fBlend = new BlendColorCtrl.floatUpdater();
    public bool isBlendEnable;
    public bool blendEnable;

    public void Init()
    {
      for (int index = 0; index < 3; ++index)
        this.fColor[index] = new BlendColorCtrl.floatUpdater();
    }

    public BlendColorCtrl.ShaderSyncParam GetSyncParam()
    {
      return new BlendColorCtrl.ShaderSyncParam()
      {
        shaderName = this.shaderName,
        propetyName = this.propetyName,
        isColor = this.isColor,
        color = new Color(this.fColor[0].tValue, this.fColor[1].tValue, this.fColor[2].tValue),
        isBlendRate = this.isBlend,
        blendRate = this.fBlend.tValue,
        isBlendEnable = this.isBlendEnable,
        blendEnable = this.blendEnable
      };
    }
  }

  public class ShaderSyncParam
  {
    public string shaderName;
    public string propetyName;
    public bool isColor;
    public Color color;
    public bool isBlendRate;
    public float blendRate;
    public bool isBlendEnable;
    public bool blendEnable;
  }
}
