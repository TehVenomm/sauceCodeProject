// Decompiled with JetBrains decompiler
// Type: QuestRetireModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class QuestRetireModel : BaseModel
{
  public static string URL = "ajax/quest/retire";
  public QuestRetireModel.Param result = new QuestRetireModel.Param();

  [Serializable]
  public class Param
  {
    public List<FollowPartyMember> friend;
    public int followNum;
    public PointEventCurrentData waveMatchPoint;
  }

  public class RequestSendForm
  {
    public string qt;
    public int timeout;
    public List<int> memids = new List<int>();
    public int wave;
    public string fieldId = "0";
    public List<QuestCompleteModel.BattleUserLog> logs = new List<QuestCompleteModel.BattleUserLog>();
    public TaskUpdateInfo actioncount = new TaskUpdateInfo();
    public int enemyHp;
    public int dc;
    public int dbc;
    public int pdbc;
    public float rSec;
    public int wmwave;
  }
}
