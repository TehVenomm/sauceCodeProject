// Decompiled with JetBrains decompiler
// Type: DebugAddEquipItemModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class DebugAddEquipItemModel : BaseModel
{
  public static string URL = "ajax/debug/addequipitem";

  public class RequestSendForm
  {
    public int equipItemId;
    public int level = 1;
    public List<int> aIds = new List<int>();
    public List<int> aPts = new List<int>();
    public int exceedCnt;
  }
}
