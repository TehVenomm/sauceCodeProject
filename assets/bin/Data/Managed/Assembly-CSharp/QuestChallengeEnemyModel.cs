// Decompiled with JetBrains decompiler
// Type: QuestChallengeEnemyModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class QuestChallengeEnemyModel : BaseModel
{
  public static string URL = "ajax/quest/challenge-enemy";
  public QuestChallengeEnemyModel.Param result = new QuestChallengeEnemyModel.Param();

  public class Param
  {
    public List<QuestData> shadow = new List<QuestData>();
  }

  public class RequestSendForm
  {
    public int enemyId;
  }
}
