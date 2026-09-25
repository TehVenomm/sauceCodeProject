// Decompiled with JetBrains decompiler
// Type: BestHTTP.Decompression.Zlib.ZlibConstants
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace BestHTTP.Decompression.Zlib;

public static class ZlibConstants
{
  public const int WindowBitsMax = 15;
  public const int WindowBitsDefault = 15;
  public const int Z_OK = 0;
  public const int Z_STREAM_END = 1;
  public const int Z_NEED_DICT = 2;
  public const int Z_STREAM_ERROR = -2;
  public const int Z_DATA_ERROR = -3;
  public const int Z_BUF_ERROR = -5;
  public const int WorkingBufferSizeDefault = 16384 /*0x4000*/;
  public const int WorkingBufferSizeMin = 1024 /*0x0400*/;
}
