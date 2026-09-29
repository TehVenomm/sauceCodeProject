// Decompiled with JetBrains decompiler
// Type: ArenaProgressModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class ArenaProgressModel : BaseModel
{
  public static string URL = "ajax/arena/progress";
  public QuestArenaProgressData result = new QuestArenaProgressData();

  public class RequestSendForm
  {
    public int wave;
    public string qt;
    public XorInt remainMilliSec;
    public XorInt elapseMilliSec;
    public List<int> breakIds = new List<int>();
    public int enemyHp;
    public List<QuestCompleteModel.BattleUserLog> logs = new List<QuestCompleteModel.BattleUserLog>();
    public TaskUpdateInfo actioncount = new TaskUpdateInfo();
    public DeliveryBattleInfo deliveryBattleInfo = new DeliveryBattleInfo();
  }
}
