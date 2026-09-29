// Decompiled with JetBrains decompiler
// Type: BestHTTP.Decompression.Zlib.SharedUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.IO;
using System.Text;

#nullable disable
namespace BestHTTP.Decompression.Zlib;

internal class SharedUtils
{
  public static int URShift(int number, int bits) => number >>> bits;

  public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
  {
    if (target.Length == 0)
      return 0;
    char[] buffer = new char[target.Length];
    int num = sourceTextReader.Read(buffer, start, count);
    if (num == 0)
      return -1;
    for (int index = start; index < start + num; ++index)
      target[index] = (byte) buffer[index];
    return num;
  }

  internal static byte[] ToByteArray(string sourceString) => Encoding.UTF8.GetBytes(sourceString);

  internal static char[] ToCharArray(byte[] byteArray) => Encoding.UTF8.GetChars(byteArray);
}
