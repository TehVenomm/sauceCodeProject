// Decompiled with JetBrains decompiler
// Type: Coop_Model_CharacterBuffReceive
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_CharacterBuffReceive : Coop_Model_ObjectBase
{
  public int type;
  public int value;
  public float time;

  public Coop_Model_CharacterBuffReceive() => this.packetType = PACKET_TYPE.CHARACTER_BUFFRECEIVE;

  public BuffParam.BuffData Deserialize()
  {
    return new BuffParam.BuffData()
    {
      type = (BuffParam.BUFFTYPE) this.type,
      value = this.value,
      time = this.time
    };
  }
}
