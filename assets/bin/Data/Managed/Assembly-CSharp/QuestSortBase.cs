using System;

public class QuestSortBase : SortBase
{
	private enum UI
	{
		SCR_VIEW,
		BTN_1,
		BTN_2,
		BTN_3,
		BTN_4,
		BTN_5,
		BTN_6,
		BTN_7,
		BTN_8,
		BTN_9,
		BTN_10,
		BTN_11,
		BTN_12,
		BTN_13,
		BTN_14,
		BTN_15,
		BTN_16,
		BTN_17,
		BTN_18,
		BTN_ID,
		BTN_NUM,
		BTN_RARITY,
		BTN_DIFFICULTY,
		BTN_ASC,
		BTN_DESC,
		GRD_ENEMY,
		OBJ_ANCHOR_BOTTOM
	}

	private static readonly UI[] enemyButton = new UI[18]
	{
		UI.BTN_1,
		UI.BTN_2,
		UI.BTN_3,
		UI.BTN_4,
		UI.BTN_5,
		UI.BTN_6,
		UI.BTN_7,
		UI.BTN_8,
		UI.BTN_9,
		UI.BTN_10,
		UI.BTN_11,
		UI.BTN_12,
		UI.BTN_13,
		UI.BTN_14,
		UI.BTN_15,
		UI.BTN_16,
		UI.BTN_17,
		UI.BTN_18
	};

	private static readonly TYPE[] enemyValue = new TYPE[18]
	{
		TYPE.ONE_HAND_SWORD,
		TYPE.TWO_HAND_SWORD,
		TYPE.SPEAR,
		TYPE.PAIR_SWORDS,
		TYPE.ARROW,
		TYPE.ARMOR,
		TYPE.HELM,
		TYPE.ARM,
		TYPE.LEG,
		TYPE.SKILL_LIMITED,
		TYPE.SKILL_GROW,
		TYPE.ENEMY_ELEMENTAL,
		TYPE.ENEMY_CHICKEN,
		TYPE.ENEMY_MUSHROOM,
		TYPE.ENEMY_COW,
		TYPE.ENEMY_FROG,
		TYPE.ENEMY_BAT,
		TYPE.ENEMY_SLIME
	};

	private static readonly UI[] requirementButton = new UI[4]
	{
		UI.BTN_ID,
		UI.BTN_NUM,
		UI.BTN_RARITY,
		UI.BTN_DIFFICULTY
	};

	private static readonly SORT_REQUIREMENT[] requirementValue = new SORT_REQUIREMENT[4]
	{
		SORT_REQUIREMENT.ID,
		SORT_REQUIREMENT.NUM,
		SORT_REQUIREMENT.RARITY,
		SORT_REQUIREMENT.DIFFICULTY
	};

	private static readonly UI[] ascButton = new UI[2]
	{
		UI.BTN_ASC,
		UI.BTN_DESC
	};

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		int i = 0;
		for (int num = enemyButton.Length; i < num; i++)
		{
			SetActive(GetCtrl(enemyButton[i]).get_parent(), false);
		}
		QuestCollection questCollection = MonoBehaviourSingleton<QuestManager>.I.questCollection;
		questCollection.GetEnemyTypeList(QUEST_TYPE.ORDER)?.ForEach(delegate(ENEMY_TYPE _enemy)
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			int num7 = (int)(_enemy - 1);
			SetActive(GetCtrl(enemyButton[num7]).get_parent(), true);
		});
		base.GetComponent<UIGrid>((Enum)UI.GRD_ENEMY).Reposition();
		UpdateAnchors();
		int j = 0;
		for (int num2 = enemyButton.Length; j < num2; j++)
		{
			bool value = (sortOrder.type & (1 << j)) != 0;
			SetEvent((Enum)enemyButton[j], "ENEMY", j);
			SetToggle(GetCtrl(enemyButton[j]).get_parent(), value);
		}
		int num3 = 1035;
		int k = 0;
		for (int num4 = requirementButton.Length; k < num4; k++)
		{
			int num5 = (int)requirementValue[k];
			if ((num5 & num3) != 0)
			{
				bool value2 = sortOrder.requirement == (SORT_REQUIREMENT)num5;
				SetEvent((Enum)requirementButton[k], "REQUIREMENT", num5);
				SetToggle((Enum)requirementButton[k], value2);
			}
			else
			{
				SetActive((Enum)requirementButton[k], false);
			}
		}
		int l = 0;
		for (int num6 = ascButton.Length; l < num6; l++)
		{
			bool value3 = false;
			if ((l == 0 && sortOrder.orderTypeAsc) || (l == 1 && !sortOrder.orderTypeAsc))
			{
				value3 = true;
			}
			SetEvent((Enum)ascButton[l], "ORDER_TYPE", l);
			SetToggle((Enum)ascButton[l], value3);
		}
	}

	private void OnQuery_ENEMY()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		int num = (int)GameSection.GetEventData();
		int num2 = (int)enemyValue[num];
		bool value;
		if ((sortOrder.type & num2) == 0)
		{
			value = true;
			sortOrder.type += num2;
		}
		else
		{
			value = false;
			sortOrder.type -= num2;
		}
		SetToggle(GetCtrl(enemyButton[num]).get_parent(), value);
	}
}
