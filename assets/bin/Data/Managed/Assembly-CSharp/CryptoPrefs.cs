// Decompiled with JetBrains decompiler
// Type: CryptoPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

#nullable disable
public class CryptoPrefs
{
  private static string sKEY = "ZTdkNTNmNDE2NTM3MWM0NDFhNTEzNzU1";
  private static string sIV = "4rZymEMfa/PpeJ89qY4gyA==";

  public static void SetInt(string key, int val)
  {
    PlayerPrefs.SetString(CryptoPrefs.GetHash(key), CryptoPrefs.Encrypt(val.ToString()));
  }

  public static int GetInt(string key, int defaultValue = 0)
  {
    string s = CryptoPrefs.GetString(key, defaultValue.ToString());
    int num = defaultValue;
    ref int local = ref num;
    int.TryParse(s, out local);
    return num;
  }

  public static void SetFloat(string key, float val)
  {
    PlayerPrefs.SetString(CryptoPrefs.GetHash(key), CryptoPrefs.Encrypt(val.ToString()));
  }

  public static float GetFloat(string key, float defaultValue = 0.0f)
  {
    string s = CryptoPrefs.GetString(key, defaultValue.ToString());
    float num = defaultValue;
    ref float local = ref num;
    float.TryParse(s, out local);
    return num;
  }

  public static void SetString(string key, string val)
  {
    PlayerPrefs.SetString(CryptoPrefs.GetHash(key), CryptoPrefs.Encrypt(val));
  }

  public static string GetString(string key, string defaultValue = "")
  {
    string str = defaultValue;
    string encString = PlayerPrefs.GetString(CryptoPrefs.GetHash(key), defaultValue.ToString());
    if (!str.Equals(encString))
      str = CryptoPrefs.Decrypt(encString);
    return str;
  }

  public static bool HasKey(string key) => PlayerPrefs.HasKey(CryptoPrefs.GetHash(key));

  public static void DeleteKey(string key) => PlayerPrefs.DeleteKey(CryptoPrefs.GetHash(key));

  public static void DeleteAll() => PlayerPrefs.DeleteAll();

  public static void Save() => PlayerPrefs.Save();

  private static string Decrypt(string encString)
  {
    string s = encString;
    RijndaelManaged rijndaelManaged = new RijndaelManaged();
    rijndaelManaged.Padding = PaddingMode.Zeros;
    rijndaelManaged.Mode = CipherMode.CBC;
    rijndaelManaged.KeySize = 128 /*0x80*/;
    rijndaelManaged.BlockSize = 128 /*0x80*/;
    ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(Encoding.UTF8.GetBytes(CryptoPrefs.sKEY), Convert.FromBase64String(CryptoPrefs.sIV));
    byte[] buffer = Convert.FromBase64String(s);
    byte[] numArray = new byte[buffer.Length];
    new CryptoStream((Stream) new MemoryStream(buffer), decryptor, CryptoStreamMode.Read).Read(numArray, 0, numArray.Length);
    return Encoding.UTF8.GetString(numArray).TrimEnd(new char[1]);
  }

  private static string Encrypt(string rawString)
  {
    string s = rawString;
    RijndaelManaged rijndaelManaged = new RijndaelManaged();
    rijndaelManaged.Padding = PaddingMode.Zeros;
    rijndaelManaged.Mode = CipherMode.CBC;
    rijndaelManaged.KeySize = 128 /*0x80*/;
    rijndaelManaged.BlockSize = 128 /*0x80*/;
    ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(Encoding.UTF8.GetBytes(CryptoPrefs.sKEY), Convert.FromBase64String(CryptoPrefs.sIV));
    MemoryStream memoryStream = new MemoryStream();
    CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write);
    byte[] bytes = Encoding.UTF8.GetBytes(s);
    cryptoStream.Write(bytes, 0, bytes.Length);
    cryptoStream.FlushFinalBlock();
    return Convert.ToBase64String(memoryStream.ToArray());
  }

  private static string GetHash(string key)
  {
    byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < hash.Length; ++index)
      stringBuilder.Append(hash[index].ToString("x2"));
    return stringBuilder.ToString();
  }
}
