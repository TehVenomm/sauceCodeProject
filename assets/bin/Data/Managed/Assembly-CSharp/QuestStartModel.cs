// Decompiled with JetBrains decompiler
// Type: QuestStartModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class QuestStartModel : BaseModel
{
  public static string URL = "ajax/quest/start";
  public QuestStartData result = new QuestStartData();

  public class RequestSendForm
  {
    public int qid;
    public string qt;
    public int setNo;
    public int crystalCL;
    public int free;
    public int dId;
    public string d;
    public TaskUpdateInfo actioncount = new TaskUpdateInfo();
  }
}
