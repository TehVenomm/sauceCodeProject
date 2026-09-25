// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.ScreenshotResolution
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

[Serializable]
public class ScreenshotResolution
{
  public bool m_Active = true;
  public int m_Width = 1920;
  public int m_Height = 1080;
  public float m_Scale = 1f;
  public int m_PPI;
  [Tooltip("If the Screen.dpi device value returned by Unity is not equals to the real device screen dpi, you can set this value to render the device content like it will be it on the device.")]
  public int m_ForcedUnityPPI;
  public string m_ResolutionName = "";
  public string m_Ratio = "";
  public float m_Stats;
  public string m_Category = "Custom";
  public string m_FileName = "";
  [NonSerialized]
  public Texture2D m_Texture;
  public ScreenshotResolution.Orientation m_Orientation;
  public bool m_IgnoreOrientation;

  public ScreenshotResolution()
  {
  }

  public ScreenshotResolution(ScreenshotResolution res)
  {
    this.m_Active = res.m_Active;
    this.m_Width = res.m_Width;
    this.m_Height = res.m_Height;
    this.m_Scale = res.m_Scale;
    this.m_PPI = res.m_PPI;
    this.m_ForcedUnityPPI = res.m_ForcedUnityPPI;
    this.m_ResolutionName = res.m_ResolutionName;
    this.m_Ratio = res.m_Ratio;
    this.m_Stats = res.m_Stats;
    this.m_Category = res.m_Category;
  }

  public ScreenshotResolution(int width, int height)
  {
    this.m_Width = width;
    this.m_Height = height;
  }

  public ScreenshotResolution(
    string category,
    int width,
    int height,
    string name = "",
    int dpi = 0,
    float stats = 0.0f)
  {
    this.m_Category = category;
    this.m_Width = width;
    this.m_Height = height;
    this.m_ResolutionName = name;
    this.m_PPI = dpi;
    this.m_Stats = stats;
    this.UpdateRatio();
  }

  public void UpdateRatio()
  {
    int num1 = this.GCD(this.m_Width, this.m_Height);
    float num2 = (float) this.m_Width / (float) num1;
    string str1 = num2.ToString();
    num2 = (float) this.m_Height / (float) num1;
    string str2 = num2.ToString();
    this.m_Ratio = $"{str1}:{str2}";
  }

  private int GCD(int a, int b) => b == 0 ? a : this.GCD(b, a % b);

  public bool IsValid() => this.m_Width > 0 && this.m_Height > 0;

  public override string ToString()
  {
    this.UpdateRatio();
    string str1 = "";
    if (this.m_ResolutionName != "")
      str1 = $"{str1}{this.m_ResolutionName}   -   ";
    string str2 = $"{$"{str1}{(object) this.m_Width}x{(object) this.m_Height}"}  {this.m_Ratio}";
    if (this.m_PPI > 0)
      str2 = $"{str2}  {(object) this.m_PPI}ppi";
    if ((double) this.m_Stats > 0.0)
      str2 = $"{str2}          {(object) this.m_Stats}%";
    return str2;
  }

  public int ComputeTargetWidth()
  {
    float targetWidth = this.m_IgnoreOrientation || this.m_Orientation == ScreenshotResolution.Orientation.LANDSCAPE ? (float) this.m_Width : (float) this.m_Height;
    if ((double) this.m_Scale > 0.0)
      targetWidth *= this.m_Scale;
    return (int) targetWidth;
  }

  public int ComputeTargetHeight()
  {
    float targetHeight = this.m_IgnoreOrientation || this.m_Orientation == ScreenshotResolution.Orientation.LANDSCAPE ? (float) this.m_Height : (float) this.m_Width;
    if ((double) this.m_Scale > 0.0)
      targetHeight *= this.m_Scale;
    return (int) targetHeight;
  }

  public enum Orientation
  {
    LANDSCAPE,
    PORTRAIT,
  }
}
