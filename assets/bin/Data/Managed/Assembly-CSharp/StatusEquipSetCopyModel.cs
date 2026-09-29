// Decompiled with JetBrains decompiler
// Type: StatusEquipSetCopyModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class StatusEquipSetCopyModel : BaseModel
{
  public static string URL = "ajax/status/copyequipset";

  public class RequestSendForm
  {
    public int no;
    public string name;
    public string wuid0;
    public string wuid1;
    public string wuid2;
    public string auid;
    public string ruid;
    public string luid;
    public string huid;
    public int show;
    public List<string> euids = new List<string>();
    public List<string> suids = new List<string>();
    public List<int> slots = new List<int>();

    public override string ToString()
    {
      return $"RequestSendForm no:{this.no}, name: {this.name}, wuid0:{this.wuid0}, wuid1:{this.wuid1}, wuid2:{this.wuid2}, auid:{this.auid}, ruid:{this.ruid}, luid:{this.luid}, huid:{this.huid}, show:{this.show}\n euids:{this.euids.ToJoinString<string>()}, Count:{this.euids.Count}\n suids:{this.suids.ToJoinString<string>()}, Count:{this.suids.Count}\n slots:{this.slots.ToJoinString<int>()}, Count:{this.slots.Count}";
    }
  }
}
