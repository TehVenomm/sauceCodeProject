// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.ScreenshotNameParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

public class ScreenshotNameParser
{
  public static string ParseSymbols(
    string name,
    ScreenshotResolution resolution,
    string currentLayer = "")
  {
    if (DateTime.Now.Month < 10)
      name = name.Replace("{month}", "0{month}");
    if (DateTime.Now.Day < 10)
      name = name.Replace("{day}", "0{day}");
    if (DateTime.Now.Hour < 10)
      name = name.Replace("{hour}", "0{hour}");
    if (DateTime.Now.Minute < 10)
      name = name.Replace("{minute}", "0{minute}");
    if (DateTime.Now.Second < 10)
      name = name.Replace("{second}", "0{second}");
    name = name.Replace("{year}", DateTime.Now.Year.ToString());
    name = name.Replace("{month}", DateTime.Now.Month.ToString());
    name = name.Replace("{day}", DateTime.Now.Day.ToString());
    name = name.Replace("{hour}", DateTime.Now.Hour.ToString());
    name = name.Replace("{minute}", DateTime.Now.Minute.ToString());
    name = name.Replace("{second}", DateTime.Now.Second.ToString());
    name = name.Replace("{width}", resolution.m_Width.ToString());
    name = name.Replace("{height}", resolution.m_Height.ToString());
    name = name.Replace("{scale}", resolution.m_Scale.ToString());
    name = name.Replace("{ratio}", resolution.m_Ratio).Replace(":", "_");
    name = name.Replace("{orientation}", resolution.m_Orientation.ToString());
    name = name.Replace("{name}", resolution.m_ResolutionName);
    name = name.Replace("{ppi}", resolution.m_PPI.ToString());
    name = name.Replace("{category}", resolution.m_Category);
    name = name.Replace("{percent}", resolution.m_Stats.ToString());
    name = name.Replace("{layer}", currentLayer);
    return name;
  }

  public static string ParseExtension(TextureExporter.ImageFileFormat format)
  {
    return format == TextureExporter.ImageFileFormat.PNG ? ".png" : ".jpg";
  }

  public static string ParsePath(
    ScreenshotNameParser.DestinationFolder destinationFolder,
    string customPath)
  {
    string path = "";
    switch (destinationFolder)
    {
      case ScreenshotNameParser.DestinationFolder.CUSTOM_FOLDER:
        path = customPath;
        break;
      case ScreenshotNameParser.DestinationFolder.DATA_PATH:
        path = $"{Application.dataPath}/{customPath}";
        break;
      case ScreenshotNameParser.DestinationFolder.PERSISTENT_DATA_PATH:
        path = $"{AndroidUtils.GetFirstAvailableMediaStorage()}/{customPath}";
        break;
      case ScreenshotNameParser.DestinationFolder.PICTURES_FOLDER:
        path = $"{AndroidUtils.GetExternalPictureDirectory()}/{customPath}";
        break;
    }
    if (path.Length > 0)
    {
      path = path.Replace("//", "/");
      if (path[path.Length - 1] != '/' && path[path.Length - 1] != '\\')
        path += "/";
    }
    return path;
  }

  public static string ParseFileName(
    string screenshotName,
    ScreenshotResolution resolution,
    ScreenshotNameParser.DestinationFolder destination,
    string customPath,
    TextureExporter.ImageFileFormat format,
    bool overrideFiles,
    string currentLayer = "")
  {
    string str1 = "" + ScreenshotNameParser.ParsePath(destination, customPath) + ScreenshotNameParser.ParseSymbols(screenshotName, resolution, currentLayer);
    string str2 = ScreenshotNameParser.ParseExtension(format);
    if (overrideFiles || !File.Exists(str1 + str2))
      return str1 + str2;
    int num = 1;
    while (true)
    {
      if (File.Exists($"{str1} ({num.ToString()}){str2}"))
        ++num;
      else
        break;
    }
    return $"{str1} ({num.ToString()}){str2}";
  }

  public enum DestinationFolder
  {
    CUSTOM_FOLDER,
    DATA_PATH,
    PERSISTENT_DATA_PATH,
    PICTURES_FOLDER,
  }
}
