// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.TextureExporter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

public class TextureExporter
{
  public static bool CreateExportDirectory(string path)
  {
    string str1 = path;
    if (string.IsNullOrEmpty(str1))
    {
      Debug.LogError((object) "Can not create directory, filename is null or empty.");
      return false;
    }
    string str2 = str1.Replace("\\", "/");
    if (!str2.Contains("/"))
    {
      Debug.LogError((object) ("Can not create directory, filename is not a valid path : " + path));
      return false;
    }
    string path1 = str2.Substring(0, str2.LastIndexOf('/'));
    if (!Directory.Exists(path1))
    {
      Debug.Log((object) ("Creating directory " + path1));
      try
      {
        Directory.CreateDirectory(path1);
      }
      catch
      {
        Debug.LogError((object) ("Failed to create directory : " + path1));
        return false;
      }
    }
    return true;
  }

  public static bool ExportToFile(
    Texture2D texture,
    string filename,
    TextureExporter.ImageFileFormat imageFormat,
    int JPGQuality = 70,
    bool addToGallery = true)
  {
    if (Object.op_Equality((Object) texture, (Object) null))
    {
      Debug.LogError((object) $"Can not export the texture to file {filename}, texture is empty.");
      return false;
    }
    byte[] bytes = imageFormat != TextureExporter.ImageFileFormat.JPG ? ImageConversion.EncodeToPNG(texture) : ImageConversion.EncodeToJPG(texture, JPGQuality);
    if (!TextureExporter.CreateExportDirectory(filename))
      return false;
    try
    {
      File.WriteAllBytes(filename, bytes);
    }
    catch
    {
      Debug.LogError((object) ("Failed to create the file : " + filename));
      return false;
    }
    if (addToGallery)
    {
      try
      {
        AndroidUtils.AddImageToGallery(filename);
      }
      catch
      {
        Debug.LogError((object) "Failed to update Android Gallery");
        return false;
      }
    }
    return true;
  }

  public static Texture2D LoadFromFile(string fullname)
  {
    if (!File.Exists(fullname))
    {
      Debug.LogError((object) $"Can not load texture from file {fullname}, file does not exists.");
      return (Texture2D) null;
    }
    byte[] numArray = File.ReadAllBytes(fullname);
    Texture2D texture2D = new Texture2D(2, 2);
    if (ImageConversion.LoadImage(texture2D, numArray))
      return texture2D;
    Debug.LogError((object) $"Failed to load the texture {fullname}.");
    return texture2D;
  }

  public static List<TextureExporter.ImageFile> LoadFromPath(string path)
  {
    List<TextureExporter.ImageFile> imageFileList = new List<TextureExporter.ImageFile>();
    if (!Directory.Exists(path))
    {
      Debug.LogError((object) $"Can not load images from directory {path}, directory does not exists.");
      return imageFileList;
    }
    foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
    {
      if (file.Extension == ".jpg" || file.Extension == ".png")
        imageFileList.Add(new TextureExporter.ImageFile()
        {
          m_Name = file.Name,
          m_Fullname = file.FullName,
          m_CreationDate = file.CreationTime,
          m_Texture = TextureExporter.LoadFromFile(file.FullName)
        });
    }
    return imageFileList;
  }

  public enum ImageFileFormat
  {
    PNG,
    JPG,
  }

  [Serializable]
  public class ImageFile
  {
    public Texture2D m_Texture;
    public string m_Name;
    public string m_Fullname;
    public DateTime m_CreationDate;
  }
}
