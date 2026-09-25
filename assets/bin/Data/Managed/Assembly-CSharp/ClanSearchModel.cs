// Decompiled with JetBrains decompiler
// Type: ClanSearchModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class ClanSearchModel : BaseModel
{
  public static string URL = "ajax/clan/search";
  public List<ClanData> result = new List<ClanData>();

  public class RequestSendForm
  {
    public string name = "";
    public int jt = -1;
    public int lbl;
    public int isCF;

    public void Copy(ref ClanSearchModel.RequestSendForm dst)
    {
      dst.name = this.name;
      dst.jt = this.jt;
      dst.lbl = this.lbl;
      dst.isCF = this.isCF;
    }
  }
}
