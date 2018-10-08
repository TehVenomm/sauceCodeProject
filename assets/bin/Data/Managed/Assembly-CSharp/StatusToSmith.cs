using UnityEngine;

public class StatusToSmith : GameSection
{
	private enum UI
	{
		BTN_EXCHANGE,
		BTN_CREATE_WEAPON,
		BTN_CREATE_DEFENSE
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_USER_STATUS | NOTIFY_FLAG.UPDATE_SMITH_BADGE;
	}

	public override void Initialize()
	{
		if (!TutorialStep.HasAllTutorialCompleted() && (Object)MonoBehaviourSingleton<UIManager>.I.npcMessage != (Object)null)
		{
			MonoBehaviourSingleton<UIManager>.I.npcMessage.HideMessage();
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetBadge(UI.BTN_CREATE_WEAPON, MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetAllWeaponBadgeNum(), SpriteAlignment.TopLeft, 7, -9, true);
		SetBadge(UI.BTN_CREATE_DEFENSE, MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetAllDefenseBadgeNum(), SpriteAlignment.TopLeft, 7, -9, true);
		base.UpdateUI();
	}

	protected override void OnOpen()
	{
		MonoBehaviourSingleton<ItemExchangeManager>.I.SetExchangeType(EXCHANGE_TYPE.NONE);
		base.OnOpen();
	}

	private void OnQuery_CREATE_WEAPON()
	{
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
		{
			if (MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.ARROW) > 0)
			{
				GameSection.ChangeEvent("CREATE", EQUIPMENT_TYPE.ARROW);
			}
			else if (MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.TWO_HAND_SWORD) > 0)
			{
				GameSection.ChangeEvent("CREATE", EQUIPMENT_TYPE.TWO_HAND_SWORD);
			}
			else if (MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.SPEAR) > 0)
			{
				GameSection.ChangeEvent("CREATE", EQUIPMENT_TYPE.SPEAR);
			}
			else if (MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.PAIR_SWORDS) > 0)
			{
				GameSection.ChangeEvent("CREATE", EQUIPMENT_TYPE.PAIR_SWORDS);
			}
			else
			{
				GameSection.ChangeEvent("CREATE", EQUIPMENT_TYPE.ONE_HAND_SWORD);
			}
		}
		else
		{
			GameSection.ChangeEvent("CREATE", EQUIPMENT_TYPE.ONE_HAND_SWORD);
		}
	}

	private void OnQuery_CREATE_DEFENSE()
	{
		GameSection.ChangeEvent("CREATE", EQUIPMENT_TYPE.HELM);
	}

	private void OnQuery_GROW_WEAPON()
	{
		GameSection.ChangeEvent("GROW", EQUIPMENT_TYPE.ONE_HAND_SWORD);
		ToGrow();
	}

	private void OnQuery_GROW_DEFENSE()
	{
		GameSection.ChangeEvent("GROW", EQUIPMENT_TYPE.HELM);
		ToGrow();
	}

	private void ToGrow()
	{
		EQUIPMENT_TYPE eQUIPMENT_TYPE = EQUIPMENT_TYPE.ONE_HAND_SWORD;
		object eventData = GameSection.GetEventData();
		if (eventData is EQUIPMENT_TYPE)
		{
			eQUIPMENT_TYPE = (EQUIPMENT_TYPE)(int)eventData;
		}
		GameSection.SetEventData(new object[2]
		{
			SmithEquipBase.SmithType.GROW,
			eQUIPMENT_TYPE
		});
	}

	private void OnQuery_SKILL_GROW()
	{
		if (MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.GetCount() == 0)
		{
			GameSection.ChangeEvent("NOT_HAVE_SKILL_ITEM", null);
		}
	}

	private void OnQuery_EXCHANGE()
	{
		Transform ctrl = GetCtrl(UI.BTN_EXCHANGE);
		if (!((Object)ctrl == (Object)null) && !ctrl.gameObject.activeSelf)
		{
			return;
		}
	}
}
