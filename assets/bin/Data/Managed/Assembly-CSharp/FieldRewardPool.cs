// Decompiled with JetBrains decompiler
// Type: FieldRewardPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class FieldRewardPool
{
  private string fieldId = "";
  private int mapId;
  private List<FieldRewardPool.DefeatEnemy> defeatList = new List<FieldRewardPool.DefeatEnemy>();
  private List<FieldRewardPool.Reward> rewardList = new List<FieldRewardPool.Reward>();
  private SpanTimer sendTimer = new SpanTimer(30f);
  private const string SAVE_KEY = "FieldRewardPool";
  private const string VER_SAVE_KEY = "FieldRewardPool.ver";
  private const string SAVE_VERSION = "1.0";

  public void Clear()
  {
    this.fieldId = "";
    this.mapId = 0;
    this.defeatList.Clear();
    this.rewardList.Clear();
  }

  public void OnUpdate()
  {
    if (!this.sendTimer.IsReady())
      return;
    this.SendFieldDrop();
  }

  public void SetFieldId(string fieldId) => this.fieldId = fieldId;

  public void SetMapId(int mapId) => this.mapId = mapId;

  public void AddEnemyDefeat(Coop_Model_EnemyDefeat model, bool IsDefeatFieldDelivery)
  {
    this.defeatList.Add(new FieldRewardPool.DefeatEnemy(model));
    this.rewardList.Add(new FieldRewardPool.Reward(model, true));
    if (IsDefeatFieldDelivery)
      this.rewardList.Add(new FieldRewardPool.Reward(model, false));
    this.Save();
  }

  public void AddRewardPickup(Coop_Model_RewardPickup model)
  {
    if (model.rewardKeyId == 0)
      return;
    FieldRewardPool.Reward reward = this.GetReward(model.rewardId);
    if (reward == null)
      return;
    reward.AddPickup(model);
    this.Save();
  }

  public FieldRewardPool.Reward GetReward(int rewardId)
  {
    return this.rewardList.Find((Predicate<FieldRewardPool.Reward>) (o => o.rewardId == rewardId));
  }

  public void ExpireRewardPickup(Coop_Model_RewardPickup model)
  {
    FieldRewardPool.Reward reward = this.GetReward(model.rewardId);
    if (reward == null)
      return;
    this.rewardList.Remove(reward);
    this.Save();
  }

  public void SendFieldDrop(Action<bool> call_back = null)
  {
    bool is_send = false;
    FieldDropModel.RequestSendForm form = new FieldDropModel.RequestSendForm();
    form.fieldId = this.fieldId;
    form.mapId = this.mapId;
    this.defeatList.ForEach((Action<FieldRewardPool.DefeatEnemy>) (o =>
    {
      if (o.isSended)
        return;
      is_send = true;
      form.eids.Add(o.enemyId);
      form.esigs.Add(o.sigInfo);
      o.isSended = true;
    }));
    this.rewardList.ForEach((Action<FieldRewardPool.Reward>) (o =>
    {
      if (o.isSended || !o.IsPickup())
        return;
      is_send = true;
      form.deids.Add(o.enemyId);
      form.dsigs.Add(o.sigInfo);
      o.isSended = true;
    }));
    if (!is_send)
    {
      if (call_back == null)
        return;
      call_back(true);
    }
    else
      MonoBehaviourSingleton<FieldManager>.I.SendFieldDrop(form, (Action<bool>) (is_success =>
      {
        this.RecvFieldDrop(is_success);
        if (call_back == null)
          return;
        call_back(is_success);
      }));
  }

  public void RecvFieldDrop(bool success)
  {
    if (success)
    {
      this.defeatList.RemoveAll((Predicate<FieldRewardPool.DefeatEnemy>) (o => o.isSended));
      this.rewardList.RemoveAll((Predicate<FieldRewardPool.Reward>) (o => o.isSended));
      this.MergeFieldDrop();
    }
    else
    {
      this.defeatList.ForEach((Action<FieldRewardPool.DefeatEnemy>) (o => o.isSended = false));
      this.rewardList.ForEach((Action<FieldRewardPool.Reward>) (o => o.isSended = false));
    }
    this.Save();
  }

  public void MergeFieldDrop()
  {
    Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
    int index = 0;
    for (int count = this.rewardList.Count; index < count; ++index)
      this.ProgressDelivery(deliveryList, this.rewardList[index]);
  }

  private void ProgressDelivery(Delivery[] list, FieldRewardPool.Reward reward)
  {
    if (!reward.IsPickup())
      return;
    int index = 0;
    for (int length1 = list.Length; index < length1; ++index)
    {
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) list[index].dId);
      if (deliveryTableData != null)
      {
        int num1 = 0;
        for (int length2 = deliveryTableData.needs.Length; num1 < length2; ++num1)
        {
          uint num2 = (uint) num1;
          if (deliveryTableData.IsNeedTarget(num2, (uint) reward.enemyId, (uint) this.mapId) && (reward.sigInfo.deliver.bit & 1 << (int) (deliveryTableData.GetRateType(num2) & DELIVERY_RATE_TYPE.RATE_1)) > 0)
          {
            int have = 0;
            int need = 0;
            MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(list[index].dId, out have, out need, num2);
            if (have < need)
            {
              int add_num = 1;
              if ((reward.sigInfo.deliver.boostBit & 1 << (int) (deliveryTableData.GetRateType(num2) & DELIVERY_RATE_TYPE.RATE_1)) > 0)
                add_num += reward.sigInfo.deliver.boostNum;
              MonoBehaviourSingleton<DeliveryManager>.I.ProgressDelivery(list[index].dId, (int) num2, add_num);
            }
          }
        }
      }
    }
  }

  public void Save()
  {
    int pickupRewardCount = 0;
    this.rewardList.ForEach((Action<FieldRewardPool.Reward>) (o => pickupRewardCount += o.IsPickup() ? 1 : 0));
    if (this.defeatList.Count <= 0 && pickupRewardCount <= 0)
    {
      if (!FieldRewardPool.HasSave())
        return;
      FieldRewardPool.DeleteSave();
    }
    else
    {
      FieldRewardPool.SaveData savedata = new FieldRewardPool.SaveData();
      savedata.fieldId = this.fieldId;
      savedata.mapId = this.mapId;
      this.defeatList.ForEach((Action<FieldRewardPool.DefeatEnemy>) (d => savedata.defeatList.Add(new FieldRewardPool.SaveData_1_0.SaveDefeatEnemy(d))));
      this.rewardList.ForEach((Action<FieldRewardPool.Reward>) (r => savedata.rewardList.Add(new FieldRewardPool.SaveData_1_0.SaveReward(r))));
      string json = JsonUtility.ToJson((object) savedata);
      PlayerPrefs.SetString("FieldRewardPool.ver", "1.0");
      PlayerPrefs.SetString(nameof (FieldRewardPool), json);
    }
  }

  public static bool HasSave() => PlayerPrefs.HasKey(nameof (FieldRewardPool));

  public static void DeleteSave()
  {
    PlayerPrefs.DeleteKey("FieldRewardPool.ver");
    PlayerPrefs.DeleteKey(nameof (FieldRewardPool));
  }

  public static FieldRewardPool LoadAndCreate()
  {
    string str1 = PlayerPrefs.GetString("FieldRewardPool.ver");
    string str2 = PlayerPrefs.GetString(nameof (FieldRewardPool));
    FieldRewardPool.SaveDataAbs saveDataAbs = (FieldRewardPool.SaveDataAbs) null;
    if (str1 == "1.0")
      saveDataAbs = (FieldRewardPool.SaveDataAbs) JsonUtility.FromJson<FieldRewardPool.SaveData_1_0>(str2);
    return saveDataAbs?.ToSelf();
  }

  [Serializable]
  public class DefeatEnemy
  {
    public int enemyId;
    public FieldDropModel.RequestSendForm.EnemySignatureInfo sigInfo = new FieldDropModel.RequestSendForm.EnemySignatureInfo();
    public bool isSended;

    public DefeatEnemy()
    {
    }

    public DefeatEnemy(Coop_Model_EnemyDefeat model)
    {
      this.enemyId = model.eid;
      this.sigInfo.defeatKeyId = model.defeatKeyId;
      this.sigInfo.signature = model.sig;
      this.sigInfo.sid = model.sid;
      this.sigInfo.exp = model.exp;
      this.sigInfo.money = model.money;
      this.sigInfo.ppt = model.ppt;
    }
  }

  [Serializable]
  public class Reward
  {
    public int rewardId;
    public int enemyId;
    public FieldDropModel.RequestSendForm.DropSignatureInfo sigInfo = new FieldDropModel.RequestSendForm.DropSignatureInfo();
    public bool isSended;

    public Reward()
    {
    }

    public Reward(Coop_Model_EnemyDefeat model, bool isTreasure)
    {
      this.rewardId = !isTreasure ? model.rewardId2 : model.rewardId;
      this.enemyId = model.eid;
      int index = 0;
      for (int count = model.dropIds.Count; index < count; ++index)
        this.sigInfo.drops.Add(new FieldDropModel.RequestSendForm.DropSignatureInfo.DropData()
        {
          dropId = model.dropIds[index],
          type = model.dropTypes[index],
          itemId = model.dropItemIds[index],
          num = model.dropNums[index],
          param_0 = model.dropParam_0s[index]
        });
      this.sigInfo.deliver.bit = model.deliver;
      this.sigInfo.deliver.boostBit = model.boostBit;
      this.sigInfo.deliver.boostNum = model.boostNum;
      this.sigInfo.deliver.isTreasure = isTreasure;
    }

    public void AddPickup(Coop_Model_RewardPickup model)
    {
      this.sigInfo.rewardKeyId = model.rewardKeyId;
      this.sigInfo.signature = model.sig;
    }

    public bool IsPickup() => this.sigInfo.rewardKeyId > 0;
  }

  [Serializable]
  public class SaveData : FieldRewardPool.SaveData_1_0
  {
  }

  [Serializable]
  public abstract class SaveDataAbs
  {
    public abstract FieldRewardPool ToSelf();
  }

  [Serializable]
  public class SaveData_1_0 : FieldRewardPool.SaveDataAbs
  {
    public string fieldId = "0";
    public int mapId;
    public List<FieldRewardPool.SaveData_1_0.SaveDefeatEnemy> defeatList = new List<FieldRewardPool.SaveData_1_0.SaveDefeatEnemy>();
    public List<FieldRewardPool.SaveData_1_0.SaveReward> rewardList = new List<FieldRewardPool.SaveData_1_0.SaveReward>();

    public override FieldRewardPool ToSelf()
    {
      FieldRewardPool self = new FieldRewardPool();
      self.fieldId = this.fieldId;
      self.mapId = this.mapId;
      this.defeatList.ForEach((Action<FieldRewardPool.SaveData_1_0.SaveDefeatEnemy>) (d => self.defeatList.Add(d.ToData())));
      this.rewardList.ForEach((Action<FieldRewardPool.SaveData_1_0.SaveReward>) (d => self.rewardList.Add(d.ToData())));
      return self;
    }

    [Serializable]
    public class SaveDefeatEnemy
    {
      public int enemyId;
      public FieldRewardPool.SaveData_1_0.SaveDefeatEnemy.SaveEnemySignatureInfo sigInfo = new FieldRewardPool.SaveData_1_0.SaveDefeatEnemy.SaveEnemySignatureInfo();

      public SaveDefeatEnemy()
      {
      }

      public SaveDefeatEnemy(FieldRewardPool.DefeatEnemy data)
      {
        this.enemyId = data.enemyId;
        this.sigInfo.defeatKeyId = data.sigInfo.defeatKeyId;
        this.sigInfo.signature = data.sigInfo.signature;
        this.sigInfo.sid = data.sigInfo.sid;
        this.sigInfo.exp = data.sigInfo.exp;
        this.sigInfo.money = data.sigInfo.money;
        this.sigInfo.ppt = data.sigInfo.ppt;
      }

      public FieldRewardPool.DefeatEnemy ToData()
      {
        return new FieldRewardPool.DefeatEnemy()
        {
          enemyId = this.enemyId,
          sigInfo = {
            defeatKeyId = this.sigInfo.defeatKeyId,
            signature = this.sigInfo.signature,
            sid = this.sigInfo.sid,
            exp = this.sigInfo.exp,
            money = this.sigInfo.money,
            ppt = this.sigInfo.ppt
          }
        };
      }

      [Serializable]
      public class SaveEnemySignatureInfo
      {
        public int defeatKeyId;
        public string signature;
        public int sid;
        public int exp;
        public int money;
        public int ppt;
      }
    }

    [Serializable]
    public class SaveReward
    {
      public int rewardId;
      public int enemyId;
      public FieldRewardPool.SaveData_1_0.SaveReward.SaveDropSignatureInfo sigInfo = new FieldRewardPool.SaveData_1_0.SaveReward.SaveDropSignatureInfo();

      public SaveReward()
      {
      }

      public SaveReward(FieldRewardPool.Reward data)
      {
        this.rewardId = data.rewardId;
        this.enemyId = data.enemyId;
        this.sigInfo.rewardKeyId = data.sigInfo.rewardKeyId;
        this.sigInfo.signature = data.sigInfo.signature;
        data.sigInfo.drops.ForEach((Action<FieldDropModel.RequestSendForm.DropSignatureInfo.DropData>) (d => this.sigInfo.drops.Add(new FieldRewardPool.SaveData_1_0.SaveReward.SaveDropSignatureInfo.SaveDropData()
        {
          dropId = d.dropId,
          type = d.type,
          itemId = d.itemId,
          num = d.num,
          param_0 = d.param_0
        })));
        this.sigInfo.deliver.bit = data.sigInfo.deliver.bit;
        this.sigInfo.deliver.boostBit = data.sigInfo.deliver.boostBit;
        this.sigInfo.deliver.boostNum = data.sigInfo.deliver.boostNum;
        this.sigInfo.deliver.isTreasure = data.sigInfo.deliver.isTreasure;
      }

      public FieldRewardPool.Reward ToData()
      {
        FieldRewardPool.Reward data = new FieldRewardPool.Reward();
        data.rewardId = this.rewardId;
        data.enemyId = this.enemyId;
        data.sigInfo.rewardKeyId = this.sigInfo.rewardKeyId;
        data.sigInfo.signature = this.sigInfo.signature;
        this.sigInfo.drops.ForEach((Action<FieldRewardPool.SaveData_1_0.SaveReward.SaveDropSignatureInfo.SaveDropData>) (sd => data.sigInfo.drops.Add(new FieldDropModel.RequestSendForm.DropSignatureInfo.DropData()
        {
          dropId = sd.dropId,
          type = sd.type,
          itemId = sd.itemId,
          num = sd.num,
          param_0 = sd.param_0
        })));
        data.sigInfo.deliver.bit = this.sigInfo.deliver.bit;
        data.sigInfo.deliver.boostBit = this.sigInfo.deliver.boostBit;
        data.sigInfo.deliver.boostNum = this.sigInfo.deliver.boostNum;
        data.sigInfo.deliver.isTreasure = this.sigInfo.deliver.isTreasure;
        return data;
      }

      [Serializable]
      public class SaveDropSignatureInfo
      {
        public int rewardKeyId;
        public string signature;
        public List<FieldRewardPool.SaveData_1_0.SaveReward.SaveDropSignatureInfo.SaveDropData> drops = new List<FieldRewardPool.SaveData_1_0.SaveReward.SaveDropSignatureInfo.SaveDropData>();
        public FieldRewardPool.SaveData_1_0.SaveReward.SaveDropSignatureInfo.SaveDeliverData deliver = new FieldRewardPool.SaveData_1_0.SaveReward.SaveDropSignatureInfo.SaveDeliverData();

        [Serializable]
        public class SaveDropData
        {
          public int dropId;
          public int type;
          public int itemId;
          public int num;
          public int param_0;
        }

        [Serializable]
        public class SaveDeliverData
        {
          public int bit;
          public int boostBit;
          public int boostNum;
          public bool isTreasure;
        }
      }
    }
  }
}
