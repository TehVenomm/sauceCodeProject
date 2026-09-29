// Decompiled with JetBrains decompiler
// Type: BestHTTP.Authentication.DigestStore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace BestHTTP.Authentication;

internal static class DigestStore
{
  private static Dictionary<string, Digest> Digests = new Dictionary<string, Digest>();

  public static Digest Get(Uri uri)
  {
    Digest digest = (Digest) null;
    return DigestStore.Digests.TryGetValue(uri.Host, out digest) && !digest.IsUriProtected(uri) ? (Digest) null : digest;
  }

  public static Digest GetOrCreate(Uri uri)
  {
    Digest digest = (Digest) null;
    if (!DigestStore.Digests.TryGetValue(uri.Host, out digest))
      DigestStore.Digests.Add(uri.Host, digest = new Digest(uri));
    return digest;
  }

  public static void Remove(Uri uri) => DigestStore.Digests.Remove(uri.Host);
}
