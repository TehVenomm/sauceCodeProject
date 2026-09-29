// Decompiled with JetBrains decompiler
// Type: QuestCompleteModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class QuestCompleteModel : BaseModel
{
  public static string URL = "ajax/quest/complete";
  public QuestCompleteData result = new QuestCompleteData();

  public class RequestSendForm
  {
    public string qt;
    public List<int> breakIds0 = new List<int>();
    public List<int> breakIds1 = new List<int>();
    public List<int> breakIds2 = new List<int>();
    public List<int> breakIds3 = new List<int>();
    public List<int> breakIds4 = new List<int>();
    public List<int> memids = new List<int>();
    public List<int> mClear = new List<int>();
    public float hpRate = 100f;
    public List<int> givenDamageList = new List<int>();
    public string fieldId = "0";
    public List<QuestCompleteModel.BattleUserLog> logs = new List<QuestCompleteModel.BattleUserLog>();
    public TaskUpdateInfo actioncount = new TaskUpdateInfo();
    public DeliveryBattleInfo deliveryBattleInfo = new DeliveryBattleInfo();
    public int enemyHp;
    public float remainSec;
    public float elapseSec;
    public int dc;
    public int dbc;
    public int pdbc;
    public float rHp;
    public float rSec;
    public int wmwave;
  }

  public class BattleUserLog
  {
    public int leaveCnt;
    public int userId;
    public string name;
    public int baseId;
    public int objId;
    public bool isNpc;
    public int hostUserId;
    public float startRemaindTime;
    public List<QuestCompleteModel.BattleUserLog.AtkInfo> atkInfos = new List<QuestCompleteModel.BattleUserLog.AtkInfo>();

    public void AddAtkInfo(string name, int count, int damage, int skillId = 0)
    {
      this.atkInfos.Add(new QuestCompleteModel.BattleUserLog.AtkInfo()
      {
        name = name,
        count = count,
        damage = damage,
        skillId = skillId
      });
    }

    public class AtkInfo
    {
      public string name;
      public int count;
      public int damage;
      public int skillId;
    }
  }
}
