using Network;
using System;
using System.Collections;
using UnityEngine;

public abstract class GachaResultBase : GameSection
{
	protected bool isRetry;

	protected int userCrystalNum;

	protected GameObject buttonObj;

	protected Transform footerRoot;

	protected GachaGuaranteeCampaignInfo nextGuachaGuarantee;

	public override void Initialize()
	{
		if (nextGuachaGuarantee != null && nextGuachaGuarantee.IsValid())
		{
			MonoBehaviourSingleton<GachaManager>.I.selectGacha.SetCrystalNum(nextGuachaGuarantee.crystalNum);
		}
		userCrystalNum = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		base.Initialize();
	}

	protected string CreateButtonName()
	{
		string str = MonoBehaviourSingleton<GachaManager>.I.CreateButtonBaseName(MonoBehaviourSingleton<GachaManager>.I.selectGacha, nextGuachaGuarantee, true);
		return str + "_RESULT";
	}

	protected IEnumerator LoadGachaGuaranteeCounter(Action<LoadObject> callback)
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		string imgName = nextGuachaGuarantee.GetTitleImageName();
		LoadObject loadObject = loadQueue.Load(RESOURCE_CATEGORY.GACHA_GUARANTEE_COUNTER, imgName, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		callback(loadObject);
	}

	protected override void OnQuery_GachaConfirm_YES()
	{
		isRetry = true;
		base.OnQuery_GachaConfirm_YES();
	}

	protected void CheckUpdateCrystalNum()
	{
		int crystal = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
		if (userCrystalNum < crystal)
		{
			isRetry = false;
		}
		userCrystalNum = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
	}

	protected void OnQuery_GACHA()
	{
		if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.IsEnd)
		{
			GameSection.ChangeEvent("END", null);
			SetGachaButtonActive(false);
		}
		else
		{
			string empty = string.Empty;
			if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId > 0)
			{
				int ticketId = MonoBehaviourSingleton<GachaManager>.I.selectGacha.requiredItemId;
				int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == ticketId, 1, false);
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)ticketId);
				empty = itemData.name + " " + MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum + StringTable.Get(STRING_CATEGORY.COMMON, 4000u) + "\n";
				if (MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum > itemNum)
				{
					object[] event_data = new object[2]
					{
						itemData.name,
						(MonoBehaviourSingleton<GachaManager>.I.selectGacha.needItemNum - itemNum).ToString() + StringTable.Get(STRING_CATEGORY.COMMON, 4000u)
					};
					GameSection.ChangeEvent("NOT_ENOUGH_GACHA_TICKET", event_data);
					return;
				}
			}
			else
			{
				empty = StringTable.Get(STRING_CATEGORY.COMMON, 100u) + " " + GetCrystalNum() + StringTable.Get(STRING_CATEGORY.COMMON, 3000u);
			}
			GameSection.SetEventData(new object[1]
			{
				empty
			});
		}
	}

	public int GetCrystalNum()
	{
		if (nextGuachaGuarantee == null || !nextGuachaGuarantee.IsValid())
		{
			return MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum;
		}
		return (!nextGuachaGuarantee.hasFreeGachaReward) ? MonoBehaviourSingleton<GachaManager>.I.selectGacha.crystalNum : 0;
	}

	protected abstract void SetGachaButtonActive(bool enableRetry);
}
