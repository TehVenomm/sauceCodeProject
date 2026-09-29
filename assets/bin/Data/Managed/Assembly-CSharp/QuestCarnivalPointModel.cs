// Decompiled with JetBrains decompiler
// Type: QuestCarnivalPointModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuestCarnivalPointModel : BaseModel
{
  public static string URL = "ajax/quest/carnivalpoint";
  public QuestCarnivalPointModel.Param result = new QuestCarnivalPointModel.Param();

  public class Param
  {
    public int point;
    public int rank;
    public int pointForNextClass;
    public int status;
    public string rankingURL;
    public string linkName;
    public int border;
    public string eventName;
    public int maxLevel;
    public int clearTime;
  }

  public class RequestSendForm
  {
    public int eid;
  }
}
