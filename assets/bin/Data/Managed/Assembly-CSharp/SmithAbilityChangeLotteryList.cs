using Network;
using System;
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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	protected unsafe virtual IEnumerator DoInitialize()
	{
		object[] datas = GameSection.GetEventData() as object[];
		EquipItemInfo info = datas[0] as EquipItemInfo;
		SmithEquipBase.SmithType smithType = (SmithEquipBase.SmithType)(int)datas[1];
		bool wait = true;
		switch (smithType)
		{
		case SmithEquipBase.SmithType.ABILITY_CHANGE:
			MonoBehaviourSingleton<SmithManager>.I.SendGetAbilityList(info.uniqueID, new Action<Error, List<SmithGetAbilityList.Param>>((object)/*Error near IL_0087: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			break;
		case SmithEquipBase.SmithType.GENERATE:
		{
			SmithManager.SmithCreateData createdata = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
			MonoBehaviourSingleton<SmithManager>.I.SendGetAbilityListPreGenerate(createdata.createEquipItemTable.id, new Action<Error, List<SmithGetAbilityListForCreateModel.Param>>((object)/*Error near IL_00c2: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	public unsafe override void UpdateUI()
	{
		SetLabelText((Enum)UI.STR_TITLE_REFLECT, base.sectionData.GetText("STR_TITLE"));
		SetDynamicList((Enum)UI.GRD_ABILITY, "SmithAbilityChangeLotteryListItem", abilities.Count, false, null, null, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
