// Decompiled with JetBrains decompiler
// Type: MsgPack.TypePrefixes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace MsgPack;

public enum TypePrefixes : byte
{
  PositiveFixNum = 0,
  FixMap = 128, // 0x80
  FixArray = 144, // 0x90
  FixRaw = 160, // 0xA0
  Nil = 192, // 0xC0
  False = 194, // 0xC2
  True = 195, // 0xC3
  Bin8 = 196, // 0xC4
  Bin16 = 197, // 0xC5
  Bin32 = 198, // 0xC6
  Ext8 = 199, // 0xC7
  Ext16 = 200, // 0xC8
  Ext32 = 201, // 0xC9
  Float = 202, // 0xCA
  Double = 203, // 0xCB
  UInt8 = 204, // 0xCC
  UInt16 = 205, // 0xCD
  UInt32 = 206, // 0xCE
  UInt64 = 207, // 0xCF
  Int8 = 208, // 0xD0
  Int16 = 209, // 0xD1
  Int32 = 210, // 0xD2
  Int64 = 211, // 0xD3
  FixExt = 215, // 0xD7
  Raw8 = 217, // 0xD9
  Raw16 = 218, // 0xDA
  Raw32 = 219, // 0xDB
  Array16 = 220, // 0xDC
  Array32 = 221, // 0xDD
  Map16 = 222, // 0xDE
  Map32 = 223, // 0xDF
  NegativeFixNum = 224, // 0xE0
}
