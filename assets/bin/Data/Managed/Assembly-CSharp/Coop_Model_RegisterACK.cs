// Decompiled with JetBrains decompiler
// Type: Coop_Model_RegisterACK
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class Coop_Model_RegisterACK : Coop_Model_ACK
{
  public int sid;
  public bool of;
  public List<int> ids = new List<int>();
  public List<int> stgids = new List<int>();
  public List<int> stgidxs = new List<int>();
  public List<bool> stghosts = new List<bool>();

  public Coop_Model_RegisterACK() => this.packetType = PACKET_TYPE.REGISTER_ACK;

  public override string ToString()
  {
    string str = "";
    int index = 0;
    for (int count = this.ids.Count; index < count; ++index)
      str = $"{str}({(object) this.ids[index]},{(object) this.stgids[index]},{(object) this.stgidxs[index]},{this.stghosts[index].ToString()}),";
    return $"{base.ToString()},clients={str}";
  }
}
