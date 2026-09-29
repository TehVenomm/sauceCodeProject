// Decompiled with JetBrains decompiler
// Type: Coop_Model_ActionMine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_ActionMine : Coop_Model_ObjectBase
{
  public string atkInfoName;
  public string nodeName;
  public int type;
  public int objId;
  public int randSeed;

  public Coop_Model_ActionMine() => this.packetType = PACKET_TYPE.ACTION_MINE;

  public enum ACTION_TYPE
  {
    DESTROY,
    EXPLODE,
    REFLECT,
    CREATE,
  }
}
