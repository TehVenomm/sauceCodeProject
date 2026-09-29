// Decompiled with JetBrains decompiler
// Type: UIDependency
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.IO;

#nullable disable
public class UIDependency
{
  public string path;
  public string filePath;
  public string[] atlasPaths;

  public static string GetPath(string longPath)
  {
    string path = longPath.Replace("Assets/App/Resources/", "");
    return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
  }

  public static string GetFilePath(string longPath)
  {
    return $"InternalUI/Deps/{Path.GetFileNameWithoutExtension(longPath).ToLower()}.txt";
  }

  public static string GetAtlasName(string atlasPath)
  {
    return Path.GetFileNameWithoutExtension(atlasPath);
  }
}
