// Decompiled with JetBrains decompiler
// Type: LockAnchorPath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
[Serializable]
public class LockAnchorPath
{
  public string Path;
  public bool IsFullAnchor;

  public LockAnchorPath(string _path, bool _isFullAnchor)
  {
    this.Path = _path;
    this.IsFullAnchor = _isFullAnchor;
  }
}
