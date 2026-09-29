// Decompiled with JetBrains decompiler
// Type: PacketStringStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class PacketStringStream
{
  private string stream = string.Empty;
  private int position;

  public int Position => this.position;

  public PacketStringStream()
  {
  }

  public PacketStringStream(string stream) => this.stream = stream;

  public void Write(string str)
  {
    this.stream += str;
    this.position += str.Length;
  }

  public void WriteInt(int val) => this.Write(val.ToString());

  public string Read(int len)
  {
    string str = this.stream.Substring(this.position, len);
    this.position += len;
    return str;
  }

  public string Read()
  {
    string str = this.stream.Substring(this.position);
    this.position = this.stream.Length;
    return str;
  }

  public override string ToString() => this.stream;

  public string Substring(int start, int len = 0)
  {
    return len != 0 ? this.stream.Substring(start, len) : this.stream.Substring(start);
  }

  public void Close()
  {
    this.stream = string.Empty;
    this.position = 0;
  }
}
