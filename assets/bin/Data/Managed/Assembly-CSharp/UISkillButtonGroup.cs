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
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
		SyncRotatePosition();
	}

	protected override void OnDestroySingleton()
	{
		base.OnDestroySingleton();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		SyncRotatePosition();
	}

	private void SyncRotatePosition()
	{
		if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.NeedModifyInGameSkillButtonPosition)
		{
			return;
		}
		DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
		Transform parent = this.get_gameObject().get_transform().get_parent();
		if (!(parent != null))
		{
			return;
		}
		UIWidget component = parent.get_gameObject().GetComponent<UIWidget>();
		if (component != null)
		{
			if (SpecialDeviceManager.IsPortrait)
			{
				component.leftAnchor.absolute = specialDeviceInfo.SkillButtonAnchorPortrait.left;
				component.rightAnchor.absolute = specialDeviceInfo.SkillButtonAnchorPortrait.right;
				component.bottomAnchor.absolute = specialDeviceInfo.SkillButtonAnchorPortrait.bottom;
				component.topAnchor.absolute = specialDeviceInfo.SkillButtonAnchorPortrait.top;
			}
			else
			{
				component.leftAnchor.absolute = specialDeviceInfo.SkillButtonAnchorLandscape.left;
				component.rightAnchor.absolute = specialDeviceInfo.SkillButtonAnchorLandscape.right;
				component.bottomAnchor.absolute = specialDeviceInfo.SkillButtonAnchorLandscape.bottom;
				component.topAnchor.absolute = specialDeviceInfo.SkillButtonAnchorLandscape.top;
			}
			component.UpdateAnchors();
		}
	}

	public UISkillButton GetUISkillButton(int index)
	{
		if (skillButtons.Count < index)
		{
			return null;
		}
		return skillButtons[index];
	}

	public UISkillButton GetSameButtonIndex(int buttonIndex, ref int arrayIndex)
	{
		arrayIndex = -1;
		int i = 0;
		for (int count = skillButtons.Count; i < count; i++)
		{
			UISkillButton uISkillButton = skillButtons[i];
			if (uISkillButton.buttonIndex == buttonIndex)
			{
				arrayIndex = i;
				return uISkillButton;
			}
		}
		return null;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		int num = changeEndAnimTweens.Length;
		for (int i = 0; i < num; i++)
		{
			changeEndAnimTweens[i].set_enabled(false);
			changeEndAnimTweens[i].Sample(1f, isFinished: true);
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
		if (target == null || target.weaponData == null)
		{
			return;
		}
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)target.weaponData.eId);
		if (equipItemData == null)
		{
			return;
		}
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
			skillButtons[num3].get_gameObject().SetActive(false);
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
				skillButtons[num3].get_gameObject().SetActive(true);
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

	private void Update()
	{
		if (!IsEnable())
		{
			this.get_gameObject().SetActive(false);
		}
	}

	public bool IsEnable()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		if (target == null)
		{
			return false;
		}
		SelfController selfController = target.controller as SelfController;
		if (selfController == null)
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
		if (this.get_gameObject().get_activeInHierarchy())
		{
			isChangeAnimStartWait = true;
			this.StartCoroutine(_ChangeAnimStart());
		}
	}

	private IEnumerator _ChangeAnimStart()
	{
		int l = 0;
		for (int count = skillButtons.Count; l < count; l++)
		{
			skillButtons[l].ReleaseEffects();
			skillButtons[l].upDateStop = true;
		}
		yield return null;
		int k = changeStartAnimTweens.Length;
		for (int m = 0; m < k; m++)
		{
			changeStartAnimTweens[m].ResetToBeginning();
			changeStartAnimTweens[m].PlayForward();
		}
		for (int j = 0; j < k; j++)
		{
			while (changeStartAnimTweens[j].get_isActiveAndEnabled())
			{
				yield return null;
			}
		}
		UpdateIndex();
		k = changeEndAnimTweens.Length;
		for (int n = 0; n < k; n++)
		{
			changeEndAnimTweens[n].ResetToBeginning();
		}
		isChangeAnimStartWait = false;
	}

	public void ChangeAnimEnd()
	{
		if (!this.get_gameObject().get_activeInHierarchy())
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
			this.StartCoroutine(_ChangeAnimEnd());
		}
	}

	private IEnumerator _ChangeAnimEnd()
	{
		int k = changeEndAnimTweens.Length;
		for (int l = 0; l < k; l++)
		{
			changeEndAnimTweens[l].PlayForward();
		}
		for (int j = 0; j < k; j++)
		{
			while (changeEndAnimTweens[j].get_isActiveAndEnabled())
			{
				yield return null;
			}
		}
		k = skillButtons.Count;
		for (int m = 0; m < k; m++)
		{
			skillButtons[m].upDateStop = false;
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
		this.get_gameObject().SetActive(true);
	}

	public void DoDisable()
	{
		this.get_gameObject().SetActive(false);
	}
}
