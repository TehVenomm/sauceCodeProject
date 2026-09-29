// Decompiled with JetBrains decompiler
// Type: BestHTTP.Decompression.Zlib.InternalConstants
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace BestHTTP.Decompression.Zlib;

internal static class InternalConstants
{
  internal static readonly int MAX_BITS = 15;
  internal static readonly int BL_CODES = 19;
  internal static readonly int D_CODES = 30;
  internal static readonly int LITERALS = 256 /*0x0100*/;
  internal static readonly int LENGTH_CODES = 29;
  internal static readonly int L_CODES = InternalConstants.LITERALS + 1 + InternalConstants.LENGTH_CODES;
  internal static readonly int MAX_BL_BITS = 7;
  internal static readonly int REP_3_6 = 16 /*0x10*/;
  internal static readonly int REPZ_3_10 = 17;
  internal static readonly int REPZ_11_138 = 18;
}
