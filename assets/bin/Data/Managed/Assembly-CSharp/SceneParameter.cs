// Decompiled with JetBrains decompiler
// Type: SceneParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SceneParameter : MonoBehaviour
{
  public Texture2D[] lightmapsFar;
  public Texture2D[] lightmapsNear;
  public LightmapsMode lightmapMode;
  public LightProbes lightProbes;

  private void OnDisable()
  {
    LightmapSettings.lightmaps = (LightmapData[]) null;
    LightmapSettings.lightProbes = (LightProbes) null;
  }

  public void Apply()
  {
    if (Object.op_Inequality((Object) this.lightProbes, (Object) null))
    {
      LightmapSettings.lightProbes = this.lightProbes;
      ShaderGlobal.lightProbe = true;
    }
    else
      ShaderGlobal.lightProbe = false;
    if (this.lightmapsFar == null || this.lightmapsFar.Length == 0)
      return;
    LightmapData[] lightmapDataArray = new LightmapData[this.lightmapsFar.Length];
    int index = 0;
    for (int length = this.lightmapsFar.Length; index < length; ++index)
    {
      lightmapDataArray[index] = new LightmapData();
      lightmapDataArray[index].lightmapColor = this.lightmapsFar[index];
      if (index < this.lightmapsNear.Length)
        lightmapDataArray[index].lightmapDir = this.lightmapsNear[index];
    }
    LightmapSettings.lightmapsMode = this.lightmapMode;
    LightmapSettings.lightmaps = lightmapDataArray;
  }
}
