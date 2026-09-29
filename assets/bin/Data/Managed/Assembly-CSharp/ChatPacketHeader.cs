// Decompiled with JetBrains decompiler
// Type: ChatPacketHeader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
[Serializable]
public class ChatPacketHeader
{
  protected const string reserved = "00";
  public static readonly int LENGTH = 40;

  public int version { get; protected set; }

  public int cmd { get; protected set; }

  public string fromId { get; protected set; }

  public ChatPacketHeader()
  {
  }

  public ChatPacketHeader(int _version, int _cmd, string _fromId)
  {
    this.version = _version;
    this.cmd = _cmd;
    this.fromId = _fromId;
  }

  public override string ToString()
  {
    long result = 0;
    long.TryParse(this.fromId, out result);
    return $"{this.version:D2}{this.cmd:D4}{"00"}{result:D32}";
  }

  public static ChatPacketHeader Parse(string str)
  {
    string s1 = str.Substring(0, 2);
    string s2 = str.Substring(2, 4);
    string _fromId = str.Substring(8, 32 /*0x20*/);
    return new ChatPacketHeader(int.Parse(s1), int.Parse(s2), _fromId);
  }
}
