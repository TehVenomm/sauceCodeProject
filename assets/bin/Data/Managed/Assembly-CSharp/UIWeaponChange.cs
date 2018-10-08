using System;
using System.Collections;
using UnityEngine;

public class UIWeaponChange : UIInGamePopBase
{
	[Serializable]
	public class WeaponIcons
	{
		public UIButton button;

		public UISprite weaponIcon;

		public GameObject elementIconBase;

		public UISprite elementIcon;

		public UILabel weaponName;

		public GameObject requestEffect;

		public int index = -1;

		private bool enable = true;

		public bool isEnable
		{
			get
			{
				return enable;
			}
			set
			{
				if (enable != value)
				{
					enable = value;
					if ((UnityEngine.Object)button != (UnityEngine.Object)null)
					{
						button.isEnabled = value;
					}
				}
			}
		}
	}

	[Serializable]
	public class EndPoint
	{
		public Vector3 position;

		public int width;
	}

	public static readonly string[] WEAPONICON_PATH = new string[6]
	{
		"WeaponIconSword",
		"WeaponIconBrade",
		"WeaponIconLance",
		"dummy",
		"WeaponIconEdge",
		"WeaponIconAllow"
	};

	public static readonly string[] ELEMENT_PATH = new string[6]
	{
		"IconElementFire",
		"IconElementWater",
		"IconElementThunder",
		"IconElementSoil",
		"IconElementLight",
		"IconElementDark"
	};

	[SerializeField]
	protected WeaponIcons[] weaponIcons;

	[SerializeField]
	protected UISpriteAnimation changeAnim;

	[SerializeField]
	protected float animDelay;

	[SerializeField]
	protected UISprite animSprite;

	[SerializeField]
	protected UITweener[] changeStartAnimTweens;

	[SerializeField]
	protected UITweener[] changeEndAnimTweens;

	[SerializeField]
	protected UIButton changeButton;

	[SerializeField]
	protected GameObject[] oldUI;

	[SerializeField]
	protected GameObject[] newUI;

	[SerializeField]
	protected UIButton[] disableButton;

	[SerializeField]
	protected UISprite changeBtnWepSprite;

	[SerializeField]
	protected UISprite changeBtnEleSprite;

	[SerializeField]
	protected GameObject changeBtnEleObj;

	protected Player targetPlayer;

	private int prevIndex = -1;

	private bool requestCheck;

	private IEnumerator routineWork;

	public Transform rallyBtn;

	public Transform autoBtn;

	public Transform btnRootGroup;

	public Transform btnFrame;

	public Transform btnFrameOver;

	public bool restrictPopMenu
	{
		get;
		protected set;
	}

	public void SetRestrictPopMenu(bool isRestrict)
	{
		restrictPopMenu = isRestrict;
	}

	protected override void Awake()
	{
		base.Awake();
		InitAnim();
		if ((UnityEngine.Object)changeButton != (UnityEngine.Object)null)
		{
			UIButtonEffect uIButtonEffect = changeButton.gameObject.AddComponent<UIButtonEffect>();
			uIButtonEffect.isSimple = true;
		}
		for (int i = 0; i < this.weaponIcons.Length; i++)
		{
			WeaponIcons weaponIcons = this.weaponIcons[i];
			if ((UnityEngine.Object)weaponIcons.button != (UnityEngine.Object)null)
			{
				UIButtonEffect uIButtonEffect2 = weaponIcons.button.gameObject.AddComponent<UIButtonEffect>();
				uIButtonEffect2.isSimple = true;
			}
		}
		bool flag = TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02);
		int j = 0;
		for (int num = oldUI.Length; j < num; j++)
		{
			oldUI[j].SetActive(!flag);
		}
		int k = 0;
		for (int num2 = newUI.Length; k < num2; k++)
		{
			newUI[k].SetActive(flag);
		}
		if (flag)
		{
			InitRally();
		}
		restrictPopMenu = false;
		InitWepIcons();
	}

	private void InitRally()
	{
		if (LoungeMatchingManager.IsValidInLounge() && !QuestManager.IsValidInGameArena())
		{
			rallyBtn.gameObject.SetActive(true);
			autoBtn.GetComponent<TweenPosition>().to = new Vector3(0f, 410f, 0f);
			btnRootGroup.GetComponent<TweenPosition>().from = new Vector3(0f, -40f, 0f);
			btnFrameOver.GetComponent<TweenPosition>().to = new Vector3(-4f, 337f, 0f);
			btnFrame.GetComponent<TweenWidth>().to = 550;
		}
		else
		{
			rallyBtn.gameObject.SetActive(false);
			autoBtn.GetComponent<TweenPosition>().to = new Vector3(0f, 355f, 0f);
			btnRootGroup.GetComponent<TweenPosition>().from = new Vector3(0f, 24f, 0f);
			btnFrameOver.GetComponent<TweenPosition>().to = new Vector3(0f, 280f, 0f);
			btnFrame.GetComponent<TweenWidth>().to = 470;
		}
	}

	public void SetDisableRallyBtn(bool isDisable)
	{
		UIButton componentInChildren = rallyBtn.GetComponentInChildren<UIButton>();
		componentInChildren.isEnabled = !isDisable;
	}

	private void InitAnim()
	{
		int i = 0;
		for (int num = weaponIcons.Length; i < num; i++)
		{
			if ((UnityEngine.Object)weaponIcons[i].requestEffect != (UnityEngine.Object)null)
			{
				weaponIcons[i].requestEffect.SetActive(false);
			}
		}
		int j = 0;
		for (int num2 = changeStartAnimTweens.Length; j < num2; j++)
		{
			changeStartAnimTweens[j].enabled = false;
			changeStartAnimTweens[j].Sample(1f, true);
		}
		int k = 0;
		for (int num3 = changeEndAnimTweens.Length; k < num3; k++)
		{
			changeEndAnimTweens[k].enabled = false;
			changeEndAnimTweens[k].Sample(1f, true);
		}
		if ((UnityEngine.Object)animSprite != (UnityEngine.Object)null)
		{
			animSprite.alpha = 0f;
		}
	}

	private void InitWepIcons()
	{
		int num = 0;
		int i = 0;
		for (int count = targetPlayer.equipWeaponList.Count; i < count; i++)
		{
			if (num > 3)
			{
				break;
			}
			if (targetPlayer.equipWeaponList[i] != null)
			{
				SetWeaponData(weaponIcons[num], targetPlayer.equipWeaponList[i].eId, targetPlayer.equipWeaponList[i].exceed, false);
				weaponIcons[num].index = i;
			}
			else
			{
				SetWeaponData(weaponIcons[num], -1, 0, false);
				weaponIcons[num].index = -1;
			}
			num++;
		}
		int j = num;
		for (int num2 = weaponIcons.Length; j < num2; j++)
		{
			SetWeaponData(weaponIcons[j], -1, 0, false);
		}
		ChangeWepBtnIcon(targetPlayer.weaponIndex);
	}

	private void OnDisable()
	{
		if (routineWork != null)
		{
			StopCoroutine(routineWork);
			routineWork = null;
			changeAnim.gameObject.SetActive(false);
			panelChange.Lock();
			InitAnim();
		}
	}

	public void SetTarget(Player player)
	{
		targetPlayer = player;
		SetNowWeapon();
	}

	public override void OnClickPopMenu()
	{
		if (MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.mainChat != (UnityEngine.Object)null && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
		}
		if (!restrictPopMenu || isPopMenu)
		{
			base.OnClickPopMenu();
		}
	}

	protected override void LateUpdate()
	{
		int i = 0;
		for (int num = weaponIcons.Length; i < num; i++)
		{
			if (weaponIcons[i].index != -1 && weaponIcons[i].isEnable != IsEnable(i))
			{
				weaponIcons[i].isEnable = IsEnable(i);
			}
		}
		base.LateUpdate();
		SetNowWeapon();
		if (requestCheck && !IsSelfCommandCheck())
		{
			int j = 0;
			for (int num2 = weaponIcons.Length; j < num2; j++)
			{
				if ((UnityEngine.Object)weaponIcons[j].requestEffect != (UnityEngine.Object)null)
				{
					weaponIcons[j].requestEffect.SetActive(false);
				}
			}
			requestCheck = false;
		}
	}

	private void SetNowWeapon()
	{
		if (prevIndex != targetPlayer.weaponIndex || targetPlayer.weaponIndex == -1)
		{
			if (prevIndex != -1 && base.gameObject.activeInHierarchy)
			{
				if (routineWork != null)
				{
					StopCoroutine(routineWork);
					routineWork = null;
				}
				else
				{
					panelChange.UnLock();
				}
				if (isPopMenu)
				{
					OnClickPopMenu();
				}
				routineWork = ChangeAnim(true, null);
				StartCoroutine(routineWork);
			}
			prevIndex = targetPlayer.weaponIndex;
		}
	}

	public void PlayEvolveIconAnim(Action cb)
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (routineWork != null)
			{
				StopCoroutine(routineWork);
				routineWork = null;
			}
			else
			{
				panelChange.UnLock();
			}
			if (isPopMenu)
			{
				OnClickPopMenu();
			}
			routineWork = ChangeAnim(false, cb);
			StartCoroutine(routineWork);
		}
	}

	private void SetWeaponData(WeaponIcons icon, int weaponId, int exceed = 0, bool is_top = false)
	{
		if (weaponId == -1)
		{
			if ((UnityEngine.Object)icon.weaponName != (UnityEngine.Object)null)
			{
				icon.weaponName.text = StringTable.Get(STRING_CATEGORY.IN_GAME, 3000u);
			}
			if ((UnityEngine.Object)icon.weaponIcon != (UnityEngine.Object)null)
			{
				icon.weaponIcon.gameObject.SetActive(false);
			}
			if ((UnityEngine.Object)icon.elementIconBase != (UnityEngine.Object)null)
			{
				icon.elementIconBase.SetActive(false);
			}
			icon.isEnable = false;
		}
		else if (Singleton<EquipItemTable>.IsValid())
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)weaponId);
			if (equipItemData != null)
			{
				icon.isEnable = true;
				if ((UnityEngine.Object)icon.weaponName != (UnityEngine.Object)null)
				{
					icon.weaponName.text = equipItemData.name;
				}
				if ((UnityEngine.Object)icon.weaponIcon != (UnityEngine.Object)null)
				{
					if (!is_top)
					{
						icon.weaponIcon.gameObject.SetActive(true);
					}
					icon.weaponIcon.spriteName = WEAPONICON_PATH[(int)equipItemData.type];
				}
				EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = equipItemData.GetExceedParam((uint)exceed);
				bool active = false;
				if ((UnityEngine.Object)icon.elementIcon != (UnityEngine.Object)null)
				{
					int i = 0;
					for (int num = equipItemData.atkElement.Length; i < num; i++)
					{
						if (equipItemData.atkElement[i] > 0)
						{
							icon.elementIcon.spriteName = ELEMENT_PATH[i];
							active = true;
							break;
						}
						if (exceedParam != null && exceedParam.atkElement[i] > 0)
						{
							icon.elementIcon.spriteName = ELEMENT_PATH[i];
							active = true;
							break;
						}
					}
				}
				if ((UnityEngine.Object)icon.elementIconBase != (UnityEngine.Object)null)
				{
					icon.elementIconBase.SetActive(active);
				}
			}
		}
	}

	public void OnChangeWeapon0()
	{
		ChangeWeapon(weaponIcons[0].index);
		if ((UnityEngine.Object)weaponIcons[0].requestEffect != (UnityEngine.Object)null)
		{
			weaponIcons[0].requestEffect.SetActive(true);
		}
		requestCheck = true;
	}

	public void OnChangeWeapon1()
	{
		ChangeWeapon(weaponIcons[1].index);
		if ((UnityEngine.Object)weaponIcons[1].requestEffect != (UnityEngine.Object)null)
		{
			weaponIcons[1].requestEffect.SetActive(true);
		}
		requestCheck = true;
	}

	public void OnChangeWeapon2()
	{
		ChangeWeapon(weaponIcons[2].index);
		if ((UnityEngine.Object)weaponIcons[2].requestEffect != (UnityEngine.Object)null)
		{
			weaponIcons[2].requestEffect.SetActive(true);
		}
		requestCheck = true;
	}

	private void ChangeWepBtnIcon(int index)
	{
		if ((UnityEngine.Object)animSprite != (UnityEngine.Object)null)
		{
			animSprite.spriteName = weaponIcons[index].weaponIcon.spriteName;
		}
		changeBtnWepSprite.spriteName = weaponIcons[index].weaponIcon.spriteName;
		changeBtnEleSprite.spriteName = weaponIcons[index].elementIcon.spriteName;
		if (!weaponIcons[index].elementIconBase.activeSelf)
		{
			changeBtnEleObj.SetActive(false);
		}
		else
		{
			changeBtnEleObj.SetActive(true);
		}
	}

	private bool IsEnable(int index)
	{
		if (requestCheck)
		{
			return false;
		}
		if (targetPlayer.isDead)
		{
			return false;
		}
		if (targetPlayer.weaponIndex == index)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StatusManager>.IsValid())
		{
			return false;
		}
		SelfController selfController = targetPlayer.controller as SelfController;
		if ((UnityEngine.Object)selfController == (UnityEngine.Object)null)
		{
			return false;
		}
		if (selfController.nextCommand != null && selfController.nextCommand.type == SelfController.COMMAND_TYPE.CHANGE_WEAPON)
		{
			return false;
		}
		return true;
	}

	private bool IsSelfCommandCheck()
	{
		if (targetPlayer.actionID == (Character.ACTION_ID)26)
		{
			return true;
		}
		SelfController selfController = targetPlayer.controller as SelfController;
		if ((UnityEngine.Object)selfController == (UnityEngine.Object)null)
		{
			return false;
		}
		if (selfController.nextCommand != null && selfController.nextCommand.type == SelfController.COMMAND_TYPE.CHANGE_WEAPON)
		{
			return true;
		}
		return false;
	}

	public void ChangeWeapon(int index)
	{
		if (IsEnable(index))
		{
			SelfController selfController = targetPlayer.controller as SelfController;
			if (!((UnityEngine.Object)selfController == (UnityEngine.Object)null))
			{
				selfController.OnWeaponChangeButtonPress(index);
			}
		}
	}

	private IEnumerator ChangeAnim(bool isChangeWeapon = true, Action cb = null)
	{
		yield return (object)new WaitForSeconds(animDelay);
		if (isChangeWeapon && MonoBehaviourSingleton<UISkillButtonGroup>.IsValid())
		{
			MonoBehaviourSingleton<UISkillButtonGroup>.I.ChangeAnimStart();
		}
		changeAnim.gameObject.SetActive(true);
		changeAnim.Play();
		int n = changeStartAnimTweens.Length;
		for (int m = 0; m < n; m++)
		{
			changeStartAnimTweens[m].ResetToBeginning();
			changeStartAnimTweens[m].PlayForward();
		}
		for (int l = 0; l < n; l++)
		{
			while (changeStartAnimTweens[l].isActiveAndEnabled)
			{
				yield return (object)null;
			}
		}
		if (isChangeWeapon)
		{
			ChangeWepBtnIcon(targetPlayer.weaponIndex);
			if (MonoBehaviourSingleton<UISkillButtonGroup>.IsValid())
			{
				while (MonoBehaviourSingleton<UISkillButtonGroup>.I.isChangeAnimStartWait)
				{
					yield return (object)null;
				}
				MonoBehaviourSingleton<UISkillButtonGroup>.I.ChangeAnimEnd();
			}
		}
		else if (!object.ReferenceEquals(cb, null))
		{
			cb();
		}
		n = changeEndAnimTweens.Length;
		for (int j = 0; j < n; j++)
		{
			changeEndAnimTweens[j].ResetToBeginning();
			changeEndAnimTweens[j].PlayForward();
		}
		for (int i = 0; i < n; i++)
		{
			while (changeEndAnimTweens[i].isActiveAndEnabled)
			{
				yield return (object)null;
			}
		}
		while (changeAnim.isPlaying)
		{
			yield return (object)null;
		}
		changeAnim.gameObject.SetActive(false);
		panelChange.Lock();
		routineWork = null;
	}

	public void SetDisableButtons(bool disable)
	{
		int i = 0;
		for (int num = disableButton.Length; i < num; i++)
		{
			if ((UnityEngine.Object)disableButton[i] != (UnityEngine.Object)null)
			{
				disableButton[i].isEnabled = !disable;
			}
		}
	}
}
