// Decompiled with JetBrains decompiler
// Type: QuestRushProgressModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class QuestRushProgressModel : BaseModel
{
  public static string URL = "ajax/quest/rush-progress";
  public QuestRushProgressData result = new QuestRushProgressData();

  public class RequestSendForm
  {
    public int wave;
    public string qt;
    public int remainSec;
    public List<int> breakIds = new List<int>();
    public List<int> memids = new List<int>();
    public List<int> mClear = new List<int>();
    public float hpRate = 100f;
    public List<int> givenDamageList = new List<int>();
    public string fieldId = "0";
    public List<QuestCompleteModel.BattleUserLog> logs = new List<QuestCompleteModel.BattleUserLog>();
    public TaskUpdateInfo actioncount = new TaskUpdateInfo();
    public DeliveryBattleInfo deliveryBattleInfo = new DeliveryBattleInfo();
    public int enemyHp;
  }
}
