// Decompiled with JetBrains decompiler
// Type: FixOffsetPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
[Serializable]
public class FixOffsetPosition
{
  public string Path;
  public float OffsetHeigh;
  public float OffsetWidt;
  public bool IsOnlyInphoneX;

  public FixOffsetPosition(string _path)
  {
    this.Path = _path;
    this.OffsetHeigh = 0.0f;
    this.OffsetWidt = 0.0f;
  }
}
