// Decompiled with JetBrains decompiler
// Type: BestHTTP.PlatformSupport.Cryptography.MD5
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Runtime.InteropServices;
using System.Security.Cryptography;

#nullable disable
namespace BestHTTP.PlatformSupport.Cryptography;

[ComVisible(true)]
public abstract class MD5 : HashAlgorithm
{
  protected MD5() => this.HashSizeValue = 128 /*0x80*/;

  public static MD5 Create() => (MD5) new MD5CryptoServiceProvider();
}
