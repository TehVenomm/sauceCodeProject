using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FieldRewardPool
{
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
			enemyId = model.eid;
			sigInfo.defeatKeyId = model.defeatKeyId;
			sigInfo.signature = model.sig;
			sigInfo.sid = model.sid;
			sigInfo.exp = model.exp;
			sigInfo.money = model.money;
			sigInfo.ppt = model.ppt;
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
			if (isTreasure)
			{
				rewardId = model.rewardId;
			}
			else
			{
				rewardId = model.rewardId2;
			}
			enemyId = model.eid;
			int i = 0;
			for (int count = model.dropIds.Count; i < count; i++)
			{
				FieldDropModel.RequestSendForm.DropSignatureInfo.DropData item = new FieldDropModel.RequestSendForm.DropSignatureInfo.DropData
				{
					dropId = model.dropIds[i],
					type = model.dropTypes[i],
					itemId = model.dropItemIds[i],
					num = model.dropNums[i],
					param_0 = model.dropParam_0s[i]
				};
				sigInfo.drops.Add(item);
			}
			sigInfo.deliver.bit = model.deliver;
			sigInfo.deliver.boostBit = model.boostBit;
			sigInfo.deliver.boostNum = model.boostNum;
			sigInfo.deliver.isTreasure = isTreasure;
		}

		public void AddPickup(Coop_Model_RewardPickup model)
		{
			sigInfo.rewardKeyId = model.rewardKeyId;
			sigInfo.signature = model.sig;
		}

		public bool IsPickup()
		{
			return sigInfo.rewardKeyId > 0;
		}
	}

	[Serializable]
	public class SaveData : SaveData_1_0
	{
	}

	[Serializable]
	public abstract class SaveDataAbs
	{
		public abstract FieldRewardPool ToSelf();
	}

	[Serializable]
	public class SaveData_1_0 : SaveDataAbs
	{
		[Serializable]
		public class SaveDefeatEnemy
		{
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

			public int enemyId;

			public SaveEnemySignatureInfo sigInfo = new SaveEnemySignatureInfo();

			public SaveDefeatEnemy()
			{
			}

			public SaveDefeatEnemy(DefeatEnemy data)
			{
				enemyId = data.enemyId;
				sigInfo.defeatKeyId = data.sigInfo.defeatKeyId;
				sigInfo.signature = data.sigInfo.signature;
				sigInfo.sid = data.sigInfo.sid;
				sigInfo.exp = data.sigInfo.exp;
				sigInfo.money = data.sigInfo.money;
				sigInfo.ppt = data.sigInfo.ppt;
			}

			public DefeatEnemy ToData()
			{
				DefeatEnemy defeatEnemy = new DefeatEnemy();
				defeatEnemy.enemyId = enemyId;
				defeatEnemy.sigInfo.defeatKeyId = sigInfo.defeatKeyId;
				defeatEnemy.sigInfo.signature = sigInfo.signature;
				defeatEnemy.sigInfo.sid = sigInfo.sid;
				defeatEnemy.sigInfo.exp = sigInfo.exp;
				defeatEnemy.sigInfo.money = sigInfo.money;
				defeatEnemy.sigInfo.ppt = sigInfo.ppt;
				return defeatEnemy;
			}
		}

		[Serializable]
		public class SaveReward
		{
			[Serializable]
			public class SaveDropSignatureInfo
			{
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

				public int rewardKeyId;

				public string signature;

				public List<SaveDropData> drops = new List<SaveDropData>();

				public SaveDeliverData deliver = new SaveDeliverData();
			}

			public int rewardId;

			public int enemyId;

			public SaveDropSignatureInfo sigInfo = new SaveDropSignatureInfo();

			public SaveReward()
			{
			}

			public SaveReward(Reward data)
			{
				rewardId = data.rewardId;
				enemyId = data.enemyId;
				sigInfo.rewardKeyId = data.sigInfo.rewardKeyId;
				sigInfo.signature = data.sigInfo.signature;
				data.sigInfo.drops.ForEach(delegate(FieldDropModel.RequestSendForm.DropSignatureInfo.DropData d)
				{
					SaveDropSignatureInfo.SaveDropData item = new SaveDropSignatureInfo.SaveDropData
					{
						dropId = d.dropId,
						type = d.type,
						itemId = d.itemId,
						num = d.num,
						param_0 = d.param_0
					};
					sigInfo.drops.Add(item);
				});
				sigInfo.deliver.bit = data.sigInfo.deliver.bit;
				sigInfo.deliver.boostBit = data.sigInfo.deliver.boostBit;
				sigInfo.deliver.boostNum = data.sigInfo.deliver.boostNum;
				sigInfo.deliver.isTreasure = data.sigInfo.deliver.isTreasure;
			}

			public Reward ToData()
			{
				Reward data = new Reward();
				data.rewardId = rewardId;
				data.enemyId = enemyId;
				data.sigInfo.rewardKeyId = sigInfo.rewardKeyId;
				data.sigInfo.signature = sigInfo.signature;
				sigInfo.drops.ForEach(delegate(SaveDropSignatureInfo.SaveDropData sd)
				{
					FieldDropModel.RequestSendForm.DropSignatureInfo.DropData item = new FieldDropModel.RequestSendForm.DropSignatureInfo.DropData
					{
						dropId = sd.dropId,
						type = sd.type,
						itemId = sd.itemId,
						num = sd.num,
						param_0 = sd.param_0
					};
					data.sigInfo.drops.Add(item);
				});
				data.sigInfo.deliver.bit = sigInfo.deliver.bit;
				data.sigInfo.deliver.boostBit = sigInfo.deliver.boostBit;
				data.sigInfo.deliver.boostNum = sigInfo.deliver.boostNum;
				data.sigInfo.deliver.isTreasure = sigInfo.deliver.isTreasure;
				return data;
			}
		}

		public string fieldId = "0";

		public int mapId;

		public List<SaveDefeatEnemy> defeatList = new List<SaveDefeatEnemy>();

		public List<SaveReward> rewardList = new List<SaveReward>();

		public override FieldRewardPool ToSelf()
		{
			FieldRewardPool self = new FieldRewardPool();
			self.fieldId = fieldId;
			self.mapId = mapId;
			defeatList.ForEach(delegate(SaveDefeatEnemy d)
			{
				self.defeatList.Add(d.ToData());
			});
			rewardList.ForEach(delegate(SaveReward d)
			{
				self.rewardList.Add(d.ToData());
			});
			return self;
		}
	}

	private string fieldId = string.Empty;

	private int mapId;

	private List<DefeatEnemy> defeatList = new List<DefeatEnemy>();

	private List<Reward> rewardList = new List<Reward>();

	private SpanTimer sendTimer = new SpanTimer(30f);

	private const string SAVE_KEY = "FieldRewardPool";

	private const string VER_SAVE_KEY = "FieldRewardPool.ver";

	private const string SAVE_VERSION = "1.0";

	public void Clear()
	{
		fieldId = string.Empty;
		mapId = 0;
		defeatList.Clear();
		rewardList.Clear();
	}

	public void OnUpdate()
	{
		if (sendTimer.IsReady())
		{
			SendFieldDrop();
		}
	}

	public void SetFieldId(string fieldId)
	{
		this.fieldId = fieldId;
	}

	public void SetMapId(int mapId)
	{
		this.mapId = mapId;
	}

	public void AddEnemyDefeat(Coop_Model_EnemyDefeat model, bool IsDefeatFieldDelivery)
	{
		defeatList.Add(new DefeatEnemy(model));
		rewardList.Add(new Reward(model, isTreasure: true));
		if (IsDefeatFieldDelivery)
		{
			rewardList.Add(new Reward(model, isTreasure: false));
		}
		Save();
	}

	public void AddRewardPickup(Coop_Model_RewardPickup model)
	{
		if (model.rewardKeyId != 0)
		{
			Reward reward = GetReward(model.rewardId);
			if (reward != null)
			{
				reward.AddPickup(model);
				Save();
			}
		}
	}

	public Reward GetReward(int rewardId)
	{
		return rewardList.Find((Reward o) => o.rewardId == rewardId);
	}

	public void ExpireRewardPickup(Coop_Model_RewardPickup model)
	{
		Reward reward = GetReward(model.rewardId);
		if (reward != null)
		{
			rewardList.Remove(reward);
			Save();
		}
	}

	public void SendFieldDrop(Action<bool> call_back = null)
	{
		bool is_send = false;
		FieldDropModel.RequestSendForm form = new FieldDropModel.RequestSendForm();
		form.fieldId = fieldId;
		form.mapId = mapId;
		defeatList.ForEach(delegate(DefeatEnemy o)
		{
			if (!o.isSended)
			{
				is_send = true;
				form.eids.Add(o.enemyId);
				form.esigs.Add(o.sigInfo);
				o.isSended = true;
			}
		});
		rewardList.ForEach(delegate(Reward o)
		{
			if (!o.isSended && o.IsPickup())
			{
				is_send = true;
				form.deids.Add(o.enemyId);
				form.dsigs.Add(o.sigInfo);
				o.isSended = true;
			}
		});
		if (!is_send)
		{
			if (call_back != null)
			{
				call_back(obj: true);
			}
		}
		else
		{
			MonoBehaviourSingleton<FieldManager>.I.SendFieldDrop(form, delegate(bool is_success)
			{
				RecvFieldDrop(is_success);
				if (call_back != null)
				{
					call_back(is_success);
				}
			});
		}
	}

	public void RecvFieldDrop(bool success)
	{
		if (success)
		{
			defeatList.RemoveAll((DefeatEnemy o) => o.isSended);
			rewardList.RemoveAll((Reward o) => o.isSended);
			MergeFieldDrop();
		}
		else
		{
			defeatList.ForEach(delegate(DefeatEnemy o)
			{
				o.isSended = false;
			});
			rewardList.ForEach(delegate(Reward o)
			{
				o.isSended = false;
			});
		}
		Save();
	}

	public void MergeFieldDrop()
	{
		Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(do_sort: false);
		int i = 0;
		for (int count = rewardList.Count; i < count; i++)
		{
			ProgressDelivery(deliveryList, rewardList[i]);
		}
	}

	private void ProgressDelivery(Delivery[] list, Reward reward)
	{
		if (!reward.IsPickup())
		{
			return;
		}
		int i = 0;
		for (int num = list.Length; i < num; i++)
		{
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)list[i].dId);
			if (deliveryTableData == null)
			{
				continue;
			}
			int j = 0;
			for (int num2 = deliveryTableData.needs.Length; j < num2; j++)
			{
				uint num3 = (uint)j;
				if (!deliveryTableData.IsNeedTarget(num3, (uint)reward.enemyId, (uint)mapId) || (reward.sigInfo.deliver.bit & (1 << (int)deliveryTableData.GetRateType(num3))) <= 0)
				{
					continue;
				}
				int have = 0;
				int need = 0;
				MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(list[i].dId, out have, out need, num3);
				if (have < need)
				{
					int num4 = 1;
					if ((reward.sigInfo.deliver.boostBit & (1 << (int)deliveryTableData.GetRateType(num3))) > 0)
					{
						num4 += reward.sigInfo.deliver.boostNum;
					}
					MonoBehaviourSingleton<DeliveryManager>.I.ProgressDelivery(list[i].dId, (int)num3, num4);
				}
			}
		}
	}

	public void Save()
	{
		int pickupRewardCount = 0;
		rewardList.ForEach(delegate(Reward o)
		{
			pickupRewardCount += (o.IsPickup() ? 1 : 0);
		});
		if (defeatList.Count <= 0 && pickupRewardCount <= 0)
		{
			if (HasSave())
			{
				DeleteSave();
			}
			return;
		}
		SaveData savedata = new SaveData();
		savedata.fieldId = fieldId;
		savedata.mapId = mapId;
		defeatList.ForEach(delegate(DefeatEnemy d)
		{
			savedata.defeatList.Add(new SaveData_1_0.SaveDefeatEnemy(d));
		});
		rewardList.ForEach(delegate(Reward r)
		{
			savedata.rewardList.Add(new SaveData_1_0.SaveReward(r));
		});
		string text = JsonUtility.ToJson((object)savedata);
		PlayerPrefs.SetString("FieldRewardPool.ver", "1.0");
		PlayerPrefs.SetString("FieldRewardPool", text);
	}

	public static bool HasSave()
	{
		return PlayerPrefs.HasKey("FieldRewardPool");
	}

	public static void DeleteSave()
	{
		PlayerPrefs.DeleteKey("FieldRewardPool.ver");
		PlayerPrefs.DeleteKey("FieldRewardPool");
	}

	public static FieldRewardPool LoadAndCreate()
	{
		string @string = PlayerPrefs.GetString("FieldRewardPool.ver");
		string string2 = PlayerPrefs.GetString("FieldRewardPool");
		SaveDataAbs saveDataAbs = null;
		if (@string == "1.0")
		{
			saveDataAbs = JsonUtility.FromJson<SaveData_1_0>(string2);
		}
		return saveDataAbs?.ToSelf();
	}
}
