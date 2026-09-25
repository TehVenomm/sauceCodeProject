// Decompiled with JetBrains decompiler
// Type: GachaGachaModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class GachaGachaModel : BaseModel
{
  public static string URL = "ajax/gacha/gacha";
  public GachaResult result = new GachaResult();
  public List<GachaResult> resultArray = new List<GachaResult>();
  public GachaResult resultBonus = new GachaResult();

  public class RequestSendForm
  {
    public int id;
    public int crystalCL;
    public int ticketCL;
    public string productId;
    public int guaranteeCampaignType;
    public int guaranteeCampaignId;
    public int guaranteeRemainCount;
    public int guaranteeUserCount;
    public int useStepUpTicket;
    public int seriesId;
  }
}
