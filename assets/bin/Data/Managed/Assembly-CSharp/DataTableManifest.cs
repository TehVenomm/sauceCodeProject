// Decompiled with JetBrains decompiler
// Type: DataTableManifest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.IO;

#nullable disable
public class DataTableManifest
{
  private Dictionary<int, MD5Hash> dict;
  private List<string> fileNames = new List<string>();

  public int version { get; private set; }

  public MD5Hash GetTableHash(string filename)
  {
    if (this.dict == null)
      return MD5Hash.invalidHash;
    MD5Hash md5Hash = (MD5Hash) null;
    return this.dict.TryGetValue(filename.ToLower().GetHashCode(), out md5Hash) ? md5Hash : MD5Hash.invalidHash;
  }

  public List<string> GetAllFileNames() => this.fileNames;

  private uint GetNameHash(string name) => MD5Hash.Calc(name).GetUIntHashCode();

  public static DataTableManifest Create(string csv, int version)
  {
    Dictionary<int, MD5Hash> dictionary = new Dictionary<int, MD5Hash>();
    List<string> stringList = new List<string>();
    using (StringReader stringReader = new StringReader(csv))
    {
      string str;
      while ((str = stringReader.ReadLine()) != null)
      {
        if (!string.IsNullOrEmpty(str))
        {
          string[] strArray = str.Split(',');
          if (strArray.Length >= 2)
          {
            string lower = strArray[0].ToLower();
            string hashString = strArray[1];
            int hashCode = lower.GetHashCode();
            MD5Hash md5Hash = MD5Hash.Parse(hashString);
            dictionary.Add(hashCode, md5Hash);
            stringList.Add(lower);
          }
        }
      }
    }
    return new DataTableManifest()
    {
      version = version,
      dict = dictionary,
      fileNames = stringList
    };
  }
}
