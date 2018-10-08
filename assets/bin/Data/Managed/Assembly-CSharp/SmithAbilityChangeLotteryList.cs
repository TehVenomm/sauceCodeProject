using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithAbilityChangeLotteryList : GameSection
{
	private enum UI
	{
		STR_TITLE_REFLECT,
		GRD_ABILITY,
		LBL_ABILITY_DETAIL_NAME,
		LBL_ABILITY_DETAIL_DESC,
		LBL_ABILITY_DETAIL_POINT
	}

	private struct MinMaxAp
	{
		public int minAp;

		public int maxAp;

		public MinMaxAp(int minAp, int maxAp)
		{
			this.minAp = minAp;
			this.maxAp = maxAp;
		}
	}

	private List<Transform> touchAndReleaseButtons = new List<Transform>();

	private List<EquipItemAbility> abilities;

	private List<MinMaxAp> minMaxAps;

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	protected virtual IEnumerator DoInitialize()
	{
		object[] datas = GameSection.GetEventData() as object[];
		EquipItemInfo info = datas[0] as EquipItemInfo;
		SmithEquipBase.SmithType smithType = (SmithEquipBase.SmithType)(int)datas[1];
		bool wait = true;
		switch (smithType)
		{
		case SmithEquipBase.SmithType.ABILITY_CHANGE:
			MonoBehaviourSingleton<SmithManager>.I.SendGetAbilityList(info.uniqueID, delegate(Error error, List<SmithGetAbilityList.Param> list)
			{
				((_003CDoInitialize_003Ec__IteratorD7)/*Error near IL_0087: stateMachine*/)._003Cwait_003E__3 = false;
				((_003CDoInitialize_003Ec__IteratorD7)/*Error near IL_0087: stateMachine*/)._003C_003Ef__this.SetAbilities(list);
			});
			break;
		case SmithEquipBase.SmithType.GENERATE:
		{
			SmithManager.SmithCreateData createdata = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
			MonoBehaviourSingleton<SmithManager>.I.SendGetAbilityListPreGenerate(createdata.createEquipItemTable.id, delegate(Error error, List<SmithGetAbilityListForCreateModel.Param> list)
			{
				((_003CDoInitialize_003Ec__IteratorD7)/*Error near IL_00c2: stateMachine*/)._003Cwait_003E__3 = false;
				((_003CDoInitialize_003Ec__IteratorD7)/*Error near IL_00c2: stateMachine*/)._003C_003Ef__this.SetAbilities(list);
			});
			break;
		}
		}
		while (wait)
		{
			yield return (object)null;
		}
		base.Initialize();
	}

	protected void InitializeBase()
	{
		base.Initialize();
	}

	protected void SetAbilities(List<SmithGetAbilityListForCreateModel.Param> list)
	{
		ClearAbilities();
		if (list != null)
		{
			foreach (SmithGetAbilityListForCreateModel.Param item2 in list)
			{
				EquipItemAbility item = new EquipItemAbility((uint)item2.aid, 0);
				abilities.Add(item);
				minMaxAps.Add(new MinMaxAp(item2.minap, item2.maxap));
			}
		}
	}

	private void SetAbilities(List<SmithGetAbilityList.Param> list)
	{
		ClearAbilities();
		if (list != null)
		{
			foreach (SmithGetAbilityList.Param item2 in list)
			{
				EquipItemAbility item = new EquipItemAbility((uint)item2.aid, 0);
				abilities.Add(item);
				minMaxAps.Add(new MinMaxAp(item2.minap, item2.maxap));
			}
		}
	}

	private void ClearAbilities()
	{
		abilities = new List<EquipItemAbility>();
		minMaxAps = new List<MinMaxAp>();
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.STR_TITLE_REFLECT, base.sectionData.GetText("STR_TITLE"));
		SetDynamicList(UI.GRD_ABILITY, "SmithAbilityChangeLotteryListItem", abilities.Count, false, null, null, delegate(int index, Transform t, bool reset)
		{
			EquipItemAbility equipItemAbility = abilities[index];
			MinMaxAp minMaxAp = minMaxAps[index];
			GetAbilityDetail(equipItemAbility, minMaxAp.minAp, minMaxAp.maxAp, out string ap, out string description);
			SetLabelText(t, UI.LBL_ABILITY_DETAIL_NAME, equipItemAbility.GetName());
			SetLabelText(t, UI.LBL_ABILITY_DETAIL_POINT, ap);
			SetLabelText(t, UI.LBL_ABILITY_DETAIL_DESC, description);
		});
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
		if ((flags & NOTIFY_FLAG.PRETREAT_SCENE) != (NOTIFY_FLAG)0L)
		{
			NoEventReleaseTouchAndReleases(touchAndReleaseButtons);
		}
	}

	private void GetAbilityDetail(EquipItemAbility ability, int minAp, int maxAp, out string ap, out string description)
	{
		ap = string.Empty;
		description = string.Empty;
		if (minAp == maxAp)
		{
			ap = "+" + minAp.ToString();
			description = Singleton<AbilityDataTable>.I.GetAbilityData(ability.id, minAp).description;
		}
		else
		{
			ap = "+" + minAp.ToString() + "〜" + maxAp.ToString();
			description = Singleton<AbilityDataTable>.I.GenerateAbilityDescriptionPreGrant(ability.id, minAp, maxAp);
		}
	}
}
