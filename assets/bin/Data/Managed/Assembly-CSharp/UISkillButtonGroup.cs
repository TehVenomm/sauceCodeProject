using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillButtonGroup : MonoBehaviourSingleton<UISkillButtonGroup>
{
	[SerializeField]
	protected List<UISkillButton> skillButtons = new List<UISkillButton>();

	[SerializeField]
	protected UITweener[] changeStartAnimTweens;

	[SerializeField]
	protected UITweener[] changeEndAnimTweens;

	[SerializeField]
	protected Texture[] maskTextures;

	protected Player target;

	protected bool _isChangeAnimStartWait;

	public bool isChangeAnimStartWait
	{
		get
		{
			return _isChangeAnimStartWait;
		}
		private set
		{
			_isChangeAnimStartWait = value;
		}
	}

	protected override void Awake()
	{
		base.Awake();
	}

	public UISkillButton GetUISkillButton(int index)
	{
		if (skillButtons.Count < index)
		{
			return null;
		}
		return skillButtons[index];
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		int num = changeEndAnimTweens.Length;
		for (int i = 0; i < num; i++)
		{
			changeEndAnimTweens[i].enabled = false;
			changeEndAnimTweens[i].Sample(1f, true);
		}
		num = skillButtons.Count;
		for (int j = 0; j < num; j++)
		{
			skillButtons[j].upDateStop = false;
		}
		UpdateIndex();
	}

	public void SetTarget(Player player)
	{
		int i = 0;
		for (int count = skillButtons.Count; i < count; i++)
		{
			skillButtons[i].SetTareget(player);
		}
		target = player;
		UpdateIndex();
	}

	public void UpdateIndex()
	{
		if (!((Object)target == (Object)null) && target.weaponData != null)
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)target.weaponData.eId);
			if (equipItemData != null)
			{
				SkillItemTable.SkillSlotData[] array = equipItemData.GetSkillSlot(target.weaponData.exceed);
				if (!TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02))
				{
					array = new SkillItemTable.SkillSlotData[1]
					{
						new SkillItemTable.SkillSlotData()
					};
					array[0].slotType = SKILL_SLOT_TYPE.ATTACK;
					array[0].skill_id = 0u;
				}
				int num = 0;
				int i = 0;
				for (int num2 = array.Length; i < num2; i++)
				{
					if (array[i].slotType != SKILL_SLOT_TYPE.ATTACK && array[i].slotType != SKILL_SLOT_TYPE.SUPPORT && array[i].slotType != SKILL_SLOT_TYPE.HEAL)
					{
						num++;
					}
				}
				int num3 = 0;
				int j = array.Length - num;
				for (int count = skillButtons.Count; j < count; j++)
				{
					skillButtons[num3].gameObject.SetActive(false);
					skillButtons[num3].SetButtonIndex(-1);
					num3++;
				}
				int num4 = 0;
				int k = 0;
				for (int num5 = array.Length; k < num5; k++)
				{
					if (num3 >= skillButtons.Count)
					{
						break;
					}
					if (array[k].slotType == SKILL_SLOT_TYPE.ATTACK || array[k].slotType == SKILL_SLOT_TYPE.SUPPORT || array[k].slotType == SKILL_SLOT_TYPE.HEAL)
					{
						skillButtons[num3].gameObject.SetActive(true);
						SkillInfo.SkillParam skillParam = target.skillInfo.GetSkillParam(target.skillInfo.weaponOffset + num4);
						if (skillParam != null && skillParam.tableData.type == array[k].slotType)
						{
							skillButtons[num3].SetButtonIndex(num4);
							num4++;
						}
						else
						{
							skillButtons[num3].SetInActiveSlot(array[k].slotType);
						}
						num3++;
					}
				}
			}
		}
	}

	private void Update()
	{
		if (!IsEnable())
		{
			base.gameObject.SetActive(false);
		}
	}

	public bool IsEnable()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		if ((Object)target == (Object)null)
		{
			return false;
		}
		SelfController x = target.controller as SelfController;
		if ((Object)x == (Object)null)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return false;
		}
		return true;
	}

	public void ChangeAnimStart()
	{
		if (base.gameObject.activeInHierarchy)
		{
			isChangeAnimStartWait = true;
			StartCoroutine(_ChangeAnimStart());
		}
	}

	private IEnumerator _ChangeAnimStart()
	{
		int n = 0;
		for (int len = skillButtons.Count; n < len; n++)
		{
			skillButtons[n].ReleaseEffects();
			skillButtons[n].upDateStop = true;
		}
		yield return (object)null;
		int m = changeStartAnimTweens.Length;
		for (int l = 0; l < m; l++)
		{
			changeStartAnimTweens[l].ResetToBeginning();
			changeStartAnimTweens[l].PlayForward();
		}
		for (int k = 0; k < m; k++)
		{
			while (changeStartAnimTweens[k].isActiveAndEnabled)
			{
				yield return (object)null;
			}
		}
		UpdateIndex();
		m = changeEndAnimTweens.Length;
		for (int i = 0; i < m; i++)
		{
			changeEndAnimTweens[i].ResetToBeginning();
		}
		isChangeAnimStartWait = false;
	}

	public void ChangeAnimEnd()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			int num = changeEndAnimTweens.Length;
			for (int i = 0; i < num; i++)
			{
				changeEndAnimTweens[i].PlayForward();
			}
			num = skillButtons.Count;
			for (int j = 0; j < num; j++)
			{
				skillButtons[j].upDateStop = false;
			}
		}
		else
		{
			StartCoroutine(_ChangeAnimEnd());
		}
	}

	private IEnumerator _ChangeAnimEnd()
	{
		int m = changeEndAnimTweens.Length;
		for (int l = 0; l < m; l++)
		{
			changeEndAnimTweens[l].PlayForward();
		}
		for (int k = 0; k < m; k++)
		{
			while (changeEndAnimTweens[k].isActiveAndEnabled)
			{
				yield return (object)null;
			}
		}
		m = skillButtons.Count;
		for (int i = 0; i < m; i++)
		{
			skillButtons[i].upDateStop = false;
		}
	}

	public Texture GetMaskTexture(SKILL_SLOT_TYPE type)
	{
		switch (type)
		{
		case SKILL_SLOT_TYPE.ATTACK:
			return maskTextures[0];
		case SKILL_SLOT_TYPE.HEAL:
			return maskTextures[1];
		case SKILL_SLOT_TYPE.SUPPORT:
			return maskTextures[2];
		default:
			return null;
		}
	}

	public void DoEnable()
	{
		base.gameObject.SetActive(true);
	}

	public void DoDisable()
	{
		base.gameObject.SetActive(false);
	}
}
