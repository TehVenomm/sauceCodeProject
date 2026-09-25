// Decompiled with JetBrains decompiler
// Type: QuestPointRewardModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class QuestPointRewardModel : BaseModel
{
  public static string URL = "ajax/quest/point-reward";
  public QuestPointRewardModel.Param result = new QuestPointRewardModel.Param();

  public class Param
  {
    public int point;
    public QuestPointRewardModel.Param.NextData reward;

    public class Reward
    {
      public int type;
      public int itemId;
      public int num;
    }

    public class NextData
    {
      public int point;
      public List<QuestPointRewardModel.Param.Reward> reward = new List<QuestPointRewardModel.Param.Reward>();
    }
  }

  public class RequestSendForm
  {
    public int eid;
  }
}
