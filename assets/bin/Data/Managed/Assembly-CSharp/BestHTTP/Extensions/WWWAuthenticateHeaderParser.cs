// Decompiled with JetBrains decompiler
// Type: BestHTTP.Extensions.WWWAuthenticateHeaderParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace BestHTTP.Extensions;

public sealed class WWWAuthenticateHeaderParser : KeyValuePairList
{
  public WWWAuthenticateHeaderParser(string headerValue)
  {
    this.Values = this.ParseQuotedHeader(headerValue).AsReadOnly();
  }

  private List<KeyValuePair> ParseQuotedHeader(string str)
  {
    List<KeyValuePair> quotedHeader = new List<KeyValuePair>();
    if (str != null)
    {
      int pos = 0;
      string key = str.Read(ref pos, (Func<char, bool>) (ch => !char.IsWhiteSpace(ch) && !char.IsControl(ch))).TrimAndLower();
      quotedHeader.Add(new KeyValuePair(key));
      while (pos < str.Length)
      {
        KeyValuePair keyValuePair = new KeyValuePair(str.Read(ref pos, '=').TrimAndLower());
        str.SkipWhiteSpace(ref pos);
        keyValuePair.Value = str.ReadQuotedText(ref pos);
        quotedHeader.Add(keyValuePair);
      }
    }
    return quotedHeader;
  }
}
