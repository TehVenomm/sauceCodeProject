// Decompiled with JetBrains decompiler
// Type: StatusEquipModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class StatusEquipModel : BaseModel
{
  public static string URL = "ajax/status/equip";

  public class RequestSendForm
  {
    public int select;
    public List<int> nos = new List<int>();
    public List<string> wuids0 = new List<string>();
    public List<string> wuids1 = new List<string>();
    public List<string> wuids2 = new List<string>();
    public List<string> auids = new List<string>();
    public List<string> ruids = new List<string>();
    public List<string> luids = new List<string>();
    public List<string> huids = new List<string>();
    public List<int> shows = new List<int>();
    public List<AccessoryPlaceInfo> accs = new List<AccessoryPlaceInfo>();
  }
}
