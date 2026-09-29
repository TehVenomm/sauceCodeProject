// Decompiled with JetBrains decompiler
// Type: DataCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#nullable disable
public class DataCache
{
  public string cachePath { get; private set; }

  protected void SetCachePath(string cachePath)
  {
    this.cachePath = cachePath;
    Directory.CreateDirectory(this.cachePath);
  }

  public bool IsCached(DataLoadRequest req) => File.Exists(this.GetCachePath(req));

  public void Remove(DataLoadRequest req) => File.Delete(this.GetCachePath(req));

  public void RemoveAll()
  {
    Directory.Delete(this.cachePath, true);
    Directory.CreateDirectory(this.cachePath);
  }

  public byte[] Load(DataLoadRequest req) => File.ReadAllBytes(this.GetCachePath(req));

  public void Save(DataLoadRequest req, byte[] data)
  {
    foreach (string file in Directory.GetFiles(this.cachePath, req.filename + "*"))
    {
      try
      {
        File.Delete(file);
      }
      catch (Exception ex)
      {
        Log.Error(LOG.DATA_TABLE, "cache delete exception({0}): {1}\n{2}\n{3}", (object) ex, (object) req.filename, (object) ex.Message, (object) ex.StackTrace);
      }
    }
    File.WriteAllBytes(this.GetCachePathGeneratedMD5(req, data), data);
  }

  private string GetCachePath(DataLoadRequest req)
  {
    return Path.Combine(this.cachePath, $"{req.filename}.{req.hash.ToString()}");
  }

  private string GetCachePathGeneratedMD5(DataLoadRequest req, byte[] data)
  {
    using (MD5 md5Hash = MD5.Create())
      return Path.Combine(this.cachePath, $"{req.filename}.{DataCache.GetMd5Hash(md5Hash, data)}");
  }

  public byte[] LoadManifest(string manifestName, int version)
  {
    return File.ReadAllBytes(this.GetManifestCachePath(manifestName, version));
  }

  private string GetManifestCachePath(string manifestName, int version)
  {
    return Path.Combine(this.cachePath, $"{manifestName}.{version.ToString()}");
  }

  private static string GetMd5Hash(MD5 md5Hash, byte[] input)
  {
    byte[] hash = md5Hash.ComputeHash(input);
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < hash.Length; ++index)
      stringBuilder.Append(hash[index].ToString("x2"));
    return stringBuilder.ToString();
  }
}
