// Decompiled with JetBrains decompiler
// Type: BestHTTP.Authentication.Digest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using BestHTTP.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace BestHTTP.Authentication;

internal sealed class Digest
{
  public Uri Uri { get; private set; }

  public AuthenticationTypes Type { get; private set; }

  public string Realm { get; private set; }

  public bool Stale { get; private set; }

  private string Nonce { get; set; }

  private string Opaque { get; set; }

  private string Algorithm { get; set; }

  public List<string> ProtectedUris { get; private set; }

  private string QualityOfProtections { get; set; }

  private int NonceCount { get; set; }

  private string HA1Sess { get; set; }

  internal Digest(Uri uri)
  {
    this.Uri = uri;
    this.Algorithm = "md5";
  }

  public void ParseChallange(string header)
  {
    this.Type = AuthenticationTypes.Unknown;
    this.Stale = false;
    this.Opaque = (string) null;
    this.HA1Sess = (string) null;
    this.NonceCount = 0;
    this.QualityOfProtections = (string) null;
    if (this.ProtectedUris != null)
      this.ProtectedUris.Clear();
    foreach (KeyValuePair keyValuePair in new WWWAuthenticateHeaderParser(header).Values)
    {
      switch (keyValuePair.Key)
      {
        case "algorithm":
          this.Algorithm = keyValuePair.Value;
          continue;
        case "basic":
          this.Type = AuthenticationTypes.Basic;
          continue;
        case "digest":
          this.Type = AuthenticationTypes.Digest;
          continue;
        case "domain":
          if (!string.IsNullOrEmpty(keyValuePair.Value) && keyValuePair.Value.Length != 0)
          {
            if (this.ProtectedUris == null)
              this.ProtectedUris = new List<string>();
            int pos = 0;
            string str = keyValuePair.Value.Read(ref pos, ' ');
            do
            {
              this.ProtectedUris.Add(str);
              str = keyValuePair.Value.Read(ref pos, ' ');
            }
            while (pos < keyValuePair.Value.Length);
            continue;
          }
          continue;
        case "nonce":
          this.Nonce = keyValuePair.Value;
          continue;
        case "opaque":
          this.Opaque = keyValuePair.Value;
          continue;
        case "qop":
          this.QualityOfProtections = keyValuePair.Value;
          continue;
        case "realm":
          this.Realm = keyValuePair.Value;
          continue;
        case "stale":
          this.Stale = bool.Parse(keyValuePair.Value);
          continue;
        default:
          continue;
      }
    }
  }

  public string GenerateResponseHeader(HTTPRequest request)
  {
    try
    {
      switch (this.Type)
      {
        case AuthenticationTypes.Basic:
          return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{request.Credentials.UserName}:{request.Credentials.Password}"));
        case AuthenticationTypes.Digest:
          ++this.NonceCount;
          string empty1 = string.Empty;
          string str1 = new Random(request.GetHashCode()).Next(int.MinValue, int.MaxValue).ToString("X8");
          string str2 = this.NonceCount.ToString("X8");
          string str3;
          switch (this.Algorithm.TrimAndLower())
          {
            case "md5":
              str3 = $"{request.Credentials.UserName}:{this.Realm}:{request.Credentials.Password}".CalculateMD5Hash();
              break;
            case "md5-sess":
              if (string.IsNullOrEmpty(this.HA1Sess))
                this.HA1Sess = $"{request.Credentials.UserName}:{this.Realm}:{request.Credentials.Password}:{this.Nonce}:{str2}".CalculateMD5Hash();
              str3 = this.HA1Sess;
              break;
            default:
              return string.Empty;
          }
          string empty2 = string.Empty;
          string str4 = this.QualityOfProtections != null ? this.QualityOfProtections.TrimAndLower() : (string) null;
          string md5Hash1;
          if (str4 == null)
          {
            string md5Hash2 = $"{request.MethodType.ToString().ToUpper()}:{request.CurrentUri.PathAndQuery}".CalculateMD5Hash();
            md5Hash1 = $"{str3}:{this.Nonce}:{md5Hash2}".CalculateMD5Hash();
          }
          else if (str4.Contains("auth-int"))
          {
            str4 = "auth-int";
            byte[] input = request.GetEntityBody() ?? string.Empty.GetASCIIBytes();
            string md5Hash3 = $"{request.MethodType.ToString().ToUpper()}:{request.CurrentUri.PathAndQuery}:{input.CalculateMD5Hash()}".CalculateMD5Hash();
            md5Hash1 = $"{str3}:{this.Nonce}:{str2}:{str1}:{str4}:{md5Hash3}".CalculateMD5Hash();
          }
          else
          {
            if (!str4.Contains("auth"))
              return string.Empty;
            str4 = "auth";
            string md5Hash4 = $"{request.MethodType.ToString().ToUpper()}:{request.CurrentUri.PathAndQuery}".CalculateMD5Hash();
            md5Hash1 = $"{str3}:{this.Nonce}:{str2}:{str1}:{str4}:{md5Hash4}".CalculateMD5Hash();
          }
          string responseHeader = $"Digest username=\"{request.Credentials.UserName}\", realm=\"{this.Realm}\", nonce=\"{this.Nonce}\", uri=\"{request.Uri.PathAndQuery}\", cnonce=\"{str1}\", response=\"{md5Hash1}\"";
          if (str4 != null)
            responseHeader = $"{responseHeader}, qop=\"{str4}\", nc={str2}";
          if (!string.IsNullOrEmpty(this.Opaque))
            responseHeader = $"{responseHeader},\"{this.Opaque}\"";
          return responseHeader;
      }
    }
    catch
    {
    }
    return string.Empty;
  }

  public bool IsUriProtected(Uri uri)
  {
    if (string.CompareOrdinal(uri.Host, this.Uri.Host) != 0)
      return false;
    string str = uri.ToString();
    if (this.ProtectedUris != null && this.ProtectedUris.Count > 0)
    {
      int index = 0;
      while (index < this.ProtectedUris.Count && !str.Contains(this.ProtectedUris[index]))
        ++index;
    }
    return true;
  }
}
