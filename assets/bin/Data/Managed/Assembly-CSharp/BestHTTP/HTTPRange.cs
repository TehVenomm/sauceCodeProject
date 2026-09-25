// Decompiled with JetBrains decompiler
// Type: BestHTTP.HTTPRange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace BestHTTP;

public sealed class HTTPRange
{
  public int FirstBytePos { get; private set; }

  public int LastBytePos { get; private set; }

  public int ContentLength { get; private set; }

  public bool IsValid { get; private set; }

  internal HTTPRange()
  {
    this.ContentLength = -1;
    this.IsValid = false;
  }

  internal HTTPRange(int contentLength)
  {
    this.ContentLength = contentLength;
    this.IsValid = false;
  }

  internal HTTPRange(int fbp, int lbp, int contentLength)
  {
    this.FirstBytePos = fbp;
    this.LastBytePos = lbp;
    this.ContentLength = contentLength;
    this.IsValid = this.FirstBytePos <= this.LastBytePos && this.ContentLength > this.LastBytePos;
  }
}
