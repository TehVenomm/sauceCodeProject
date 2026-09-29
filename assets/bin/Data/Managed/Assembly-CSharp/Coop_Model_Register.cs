// Decompiled with JetBrains decompiler
// Type: Coop_Model_Register
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_Register : Coop_Model_Base
{
  public string roomId;
  public string token;

  public Coop_Model_Register() => this.packetType = PACKET_TYPE.REGISTER;
}
