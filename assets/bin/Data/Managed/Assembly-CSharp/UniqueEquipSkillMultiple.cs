// Decompiled with JetBrains decompiler
// Type: UniqueEquipSkillMultiple
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class UniqueEquipSkillMultiple : BaseModel
{
  public static string URL = "ajax/status/unique-equip-skill-multiple";

  public class RequestSendForm
  {
    public List<string> euids = new List<string>();
    public List<string> suids = new List<string>();
    public List<string> slots = new List<string>();
  }
}
