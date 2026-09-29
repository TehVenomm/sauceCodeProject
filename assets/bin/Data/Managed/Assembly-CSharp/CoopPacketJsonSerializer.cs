// Decompiled with JetBrains decompiler
// Type: CoopPacketJsonSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Globalization;

#nullable disable
public class CoopPacketJsonSerializer : CoopPacketSerializer
{
  public string version = "10";

  public string ConvertUserToken(int client_id, int user_token_len)
  {
    string str;
    switch (client_id)
    {
      case -2000:
        str = " ";
        break;
      case -1000:
        str = "";
        break;
      default:
        str = client_id.ToString().PadLeft(user_token_len);
        break;
    }
    return str;
  }

  public int ConvertClientId(string user_token)
  {
    int num;
    switch (user_token)
    {
      case "":
        num = -1000;
        break;
      case " ":
        num = -2000;
        break;
      default:
        num = int.Parse(user_token);
        break;
    }
    return num;
  }

  public override PacketStream Serialize(CoopPacket packet) => this.SerializeString(packet);

  protected override void OnSerializeStringPrefix(PacketStringStream stream)
  {
    this.version = "10";
    stream.Write(this.version);
  }

  protected override void OnSerializeStringHeader(
    PacketStringStream stream,
    CoopPacketHeader header)
  {
    int user_token_len = this.version == "00" ? 11 : 1;
    string str1 = "" + this.ConvertUserToken(header.from, user_token_len) + this.ConvertUserToken(header.to, user_token_len) + (header.promise ? "1" : "0") + header.sequenceNo.ToString().PadLeft(16 /*0x10*/);
    string str2 = str1.Length.ToString("X4");
    stream.Write(str2);
    stream.Write(str1);
  }

  protected override void OnSerializeStringModel(PacketStringStream stream, Coop_Model_Base model)
  {
    System.Type modelType = ((PACKET_TYPE) model.c).GetModelType();
    string str = JSONSerializer.Serialize((object) model, modelType);
    stream.Write(str);
  }

  protected override void OnDeserializeStringPrefix(PacketStringStream stream)
  {
    this.version = stream.Read("10".Length);
  }

  protected override CoopPacketHeader OnDeserializeStringHeader(PacketStringStream stream)
  {
    string s1 = stream.Read(4);
    int len = this.version == "00" ? 11 : 1;
    int position = stream.Position;
    string user_token1 = stream.Read(len);
    string user_token2 = stream.Read(len);
    string str = stream.Read(1);
    string s2 = stream.Read(16 /*0x10*/);
    int num = stream.Position - position;
    if (num.ToString("X4") != s1)
      Log.Error(LOG.WEBSOCK, "break header packet! {0} != {1}", (object) num, (object) int.Parse(s1, NumberStyles.HexNumber));
    return new CoopPacketHeader(0, this.ConvertClientId(user_token1), this.ConvertClientId(user_token2), str == "1", int.Parse(s2));
  }

  protected override Coop_Model_Base OnDeserializeStringModel(
    PacketStringStream stream,
    System.Type type,
    CoopPacketHeader header)
  {
    string message = stream.Read();
    return JSONSerializer.Deserialize<Coop_Model_Base>(message, ((PACKET_TYPE) JSONSerializer.Deserialize<Coop_Model_Base>(message).c).GetModelType());
  }
}
