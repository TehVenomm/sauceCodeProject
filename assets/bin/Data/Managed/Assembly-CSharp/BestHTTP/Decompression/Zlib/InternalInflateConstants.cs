// Decompiled with JetBrains decompiler
// Type: BestHTTP.Decompression.Zlib.InternalInflateConstants
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace BestHTTP.Decompression.Zlib;

internal static class InternalInflateConstants
{
  internal static readonly int[] InflateMask = new int[17]
  {
    0,
    1,
    3,
    7,
    15,
    31 /*0x1F*/,
    63 /*0x3F*/,
    (int) sbyte.MaxValue,
    (int) byte.MaxValue,
    511 /*0x01FF*/,
    1023 /*0x03FF*/,
    2047 /*0x07FF*/,
    4095 /*0x0FFF*/,
    8191 /*0x1FFF*/,
    16383 /*0x3FFF*/,
    (int) short.MaxValue,
    (int) ushort.MaxValue
  };
}
