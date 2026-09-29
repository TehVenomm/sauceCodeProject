// Decompiled with JetBrains decompiler
// Type: QuestListModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class QuestListModel : BaseModel
{
  public static string URL = "ajax/quest/list";
  public QuestListModel.Param result = new QuestListModel.Param();

  public class Param
  {
    public List<QuestData> quests = new List<QuestData>();
    public List<QuestData> order = new List<QuestData>();
    public List<QuestData> explores = new List<QuestData>();
    public List<Network.EventData> events = new List<Network.EventData>();
    public List<int> futureEventIds = new List<int>();
    public List<Network.EventData> bingoEvents = new List<Network.EventData>();
    public float dailyRemainTime = -1f;
    public float weeklyRemainTime = -1f;
    public int carnivalEventId;
  }

  public class RequestSendForm
  {
    public int req_q;
    public int req_gq;
    public int req_eq;
    public int req_d;
    public int req_e;
    public int req_bingo;
  }
}
