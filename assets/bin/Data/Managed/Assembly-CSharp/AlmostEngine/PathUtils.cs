// Decompiled with JetBrains decompiler
// Type: AlmostEngine.PathUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.IO;

#nullable disable
namespace AlmostEngine;

public class PathUtils
{
  public static bool IsValidPath(string path)
  {
    if (string.IsNullOrEmpty(path))
      return false;
    foreach (char invalidPathChar in Path.GetInvalidPathChars())
    {
      if (path.Contains(invalidPathChar.ToString()))
        return false;
    }
    try
    {
      Path.GetFullPath(path);
    }
    catch
    {
      return false;
    }
    return true;
  }
}
