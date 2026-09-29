// Decompiled with JetBrains decompiler
// Type: BinaryTableReader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Ionic.Zlib;
using System.IO;
using System.Text;

#nullable disable
public class BinaryTableReader
{
  private BinaryReader reader;
  private int allDataSize;
  private int rowSize;
  private int currentPosition;
  private byte[] tmpBuffer = new byte[1024 /*0x0400*/];
  private int maxByteBufferSize_;
  private int maxCharBufferSize_;
  private byte[] byteBuffer_;
  private char[] charBuffer_;
  private Encoding encoding = (Encoding) new UTF8Encoding();

  public BinaryTableReader(byte[] bytes)
  {
    MemoryStream memoryStream = new MemoryStream(bytes);
    memoryStream.Seek(256L /*0x0100*/, SeekOrigin.Begin);
    this.reader = new BinaryReader((Stream) new ZlibStream((Stream) memoryStream, (CompressionMode) 1));
    this.allDataSize = bytes.Length - 256 /*0x0100*/;
  }

  public BinaryTableReader(MemoryStream stream)
  {
    this.reader = new BinaryReader((Stream) stream);
    this.allDataSize = (int) stream.Length;
  }

  public bool MoveNext()
  {
    int count = this.rowSize - this.currentPosition;
    if (count > 0)
      this.reader.Read(this.tmpBuffer, 0, count);
    if (this.reader.BaseStream.Position >= (long) this.allDataSize)
      return false;
    this.rowSize = this.reader.ReadInt32();
    this.currentPosition = 0;
    return true;
  }

  public bool ReadBoolean(bool defaultValue = false)
  {
    bool flag;
    if (this.currentPosition < this.rowSize)
    {
      flag = this.reader.ReadBoolean();
      ++this.currentPosition;
    }
    else
      flag = defaultValue;
    return flag;
  }

  public int ReadInt32(int defaultValue = 0)
  {
    int num;
    if (this.currentPosition < this.rowSize)
    {
      num = this.reader.ReadInt32();
      this.currentPosition += 4;
    }
    else
      num = defaultValue;
    return num;
  }

  public uint ReadUInt32(uint defaultValue = 0)
  {
    uint num;
    if (this.currentPosition < this.rowSize)
    {
      num = this.reader.ReadUInt32();
      this.currentPosition += 4;
    }
    else
      num = defaultValue;
    return num;
  }

  public float ReadSingle(float defaultValue = 0.0f)
  {
    float num;
    if (this.currentPosition < this.rowSize)
    {
      num = this.reader.ReadSingle();
      this.currentPosition += 4;
    }
    else
      num = defaultValue;
    return num;
  }

  public string ReadString(string defaultValue = "")
  {
    string str;
    if (this.currentPosition < this.rowSize)
    {
      int num = this.Read7BitEncodedInt();
      if (0 > num || num == 0)
        return defaultValue;
      if (this.maxByteBufferSize_ < num)
      {
        this.maxByteBufferSize_ = num;
        this.byteBuffer_ = new byte[this.maxByteBufferSize_];
        this.maxCharBufferSize_ = this.encoding.GetMaxCharCount(this.maxByteBufferSize_);
        this.charBuffer_ = new char[this.maxCharBufferSize_];
      }
      if (this.byteBuffer_ == null || this.charBuffer_ == null)
      {
        this.maxByteBufferSize_ = 0;
        this.maxCharBufferSize_ = 0;
        return defaultValue;
      }
      this.reader.Read(this.byteBuffer_, 0, num);
      str = new string(this.charBuffer_, 0, this.encoding.GetChars(this.byteBuffer_, 0, num, this.charBuffer_, 0));
      this.currentPosition += num;
    }
    else
      str = defaultValue;
    return str;
  }

  public byte ReadByte(byte defaultValue = 0)
  {
    byte num;
    if (this.currentPosition < this.rowSize)
    {
      num = this.reader.ReadByte();
      ++this.currentPosition;
    }
    else
      num = defaultValue;
    return num;
  }

  private int Read7BitEncodedInt()
  {
    int num1 = 0;
    int num2 = 0;
    byte num3;
    do
    {
      num3 = this.ReadByte();
      num1 |= ((int) num3 & (int) sbyte.MaxValue) << num2;
      num2 += 7;
    }
    while (((int) num3 & 128 /*0x80*/) != 0);
    return num1;
  }
}
