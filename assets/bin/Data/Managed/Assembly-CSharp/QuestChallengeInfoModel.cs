// Decompiled with JetBrains decompiler
// Type: QuestChallengeInfoModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class QuestChallengeInfoModel : BaseModel
{
  public static string URL = "ajax/quest/challenge-info";
  public QuestChallengeInfoModel.Param result = new QuestChallengeInfoModel.Param();

  public class Param
  {
    public int enable;
    public int satisfy;
    public string message;
    public int num;
    public int firstClear;
    public int isRankingEvent = -1;
    public ShadowCount oldShadowCount;
    public ShadowCount currentShadowCount;

    public bool IsEnable() => this.enable == 1;

    public bool IsSatisfy() => this.satisfy == 1;

    public bool NotClaer() => this.IsEnable() && this.IsSatisfy() && this.firstClear != 1;

    public bool IsRankingEvent() => this.isRankingEvent == 1;
  }

  public class RequestSendForm
  {
  }
}
