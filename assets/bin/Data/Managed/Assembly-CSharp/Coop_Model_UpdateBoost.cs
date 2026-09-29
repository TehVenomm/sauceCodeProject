// Decompiled with JetBrains decompiler
// Type: Coop_Model_UpdateBoost
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_UpdateBoost : Coop_Model_Base
{
  public int expUpEnd;
  public int moneyUpEnd;
  public int dropUpEnd;
  public int happenQuestUpEnd;

  public Coop_Model_UpdateBoost() => this.packetType = PACKET_TYPE.UPDATE_BOOST;

  public override string ToString()
  {
    return $"{base.ToString()},expUpEnd={(object) this.expUpEnd},moneyUpEnd={(object) this.moneyUpEnd},dropUpEnd={(object) this.dropUpEnd},happenQuestUpEnd={(object) this.happenQuestUpEnd}";
  }
}
