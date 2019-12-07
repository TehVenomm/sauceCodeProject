using System;
using System.Collections;
using UnityEngine;

public class UISkillButton : MonoBehaviour
{
	[Serializable]
	public class GaugeEffect
	{
		public GameObject obj;

		public TweenAlpha alpha;

		public TweenScale scale;

		private IEnumerator work;

		private bool isActive = true;

		public void Init(UISkillButton parent)
		{
			if (work != null)
			{
				parent.StopCoroutine(work);
				work = null;
			}
			if (isActive)
			{
				isActive = false;
				if (obj != null)
				{
					obj.SetActive(value: false);
				}
			}
		}

		public void Play(UISkillButton parent)
		{
			isActive = true;
			if (obj != null)
			{
				obj.SetActive(value: true);
			}
			if (alpha != null)
			{
				alpha.ResetToBeginning();
				alpha.PlayForward();
			}
			if (scale != null)
			{
				scale.ResetToBeginning();
				scale.PlayForward();
			}
			if (work != null)
			{
				parent.StopCoroutine(work);
			}
			work = EndCheck();
			parent.StartCoroutine(work);
		}

		private IEnumerator EndCheck()
		{
			if (alpha != null)
			{
				while (alpha.enabled)
				{
					yield return null;
				}
			}
			if (scale != null)
			{
				while (scale.enabled)
				{
					yield return null;
				}
			}
			work = null;
			if (obj != null)
			{
				obj.SetActive(value: false);
			}
			isActive = false;
		}
	}

	[Serializable]
	protected class SkillGauge
	{
		public UISprite typeSprite;

		public UITexture iconTexture;

		public GaugeEffect maxEffect;

		[HideInInspector]
		public Transform effectTransform;

		[HideInInspector]
		public float btnSize;

		[HideInInspector]
		public Vector3 skillIconOnPos;

		[HideInInspector]
		public string[] useEffectNames;

		[HideInInspector]
		public float percent;

		[HideInInspector]
		public bool isPrevGaugeMax;

		[HideInInspector]
		public float depth = -1f;

		private Transform gaugeEffect_1;

		private Transform gaugeEffect_2;

		private Transform gaugeEffect_3;

		private Transform gaugeEffect_Max;

		private UITweener alphaTween;

		private UITweener scaleTween;

		public void Init(float btnSize)
		{
			this.btnSize = btnSize;
			effectTransform = iconTexture.transform;
			skillIconOnPos = effectTransform.localPosition;
		}

		public void SetActive(bool isActive)
		{
			typeSprite.gameObject.SetActive(isActive);
			iconTexture.gameObject.SetActive(isActive);
		}

		private void SetDepth(int depth)
		{
			if (this.depth != (float)depth)
			{
				iconTexture.depth = depth + 1;
				typeSprite.depth = depth;
				this.depth = depth;
			}
		}

		public void MoveToFront(int depth)
		{
			SetDepth(depth + 1);
		}

		public void MoveToBack(int depth)
		{
			SetDepth(depth - 2);
		}

		public bool PlayEffect1(float dispPercent)
		{
			bool result = false;
			if (gaugeEffect_1 == null)
			{
				gaugeEffect_1 = EffectManager.GetUIEffect(useEffectNames[0], effectTransform, -1f);
				if (gaugeEffect_1 != null)
				{
					Vector3 localPosition = gaugeEffect_1.localPosition;
					localPosition.x = btnSize * 2f;
					gaugeEffect_1.localPosition = localPosition;
					result = true;
				}
			}
			else
			{
				Vector3 localPosition2 = gaugeEffect_1.localPosition;
				localPosition2.x = 0f;
				localPosition2.y = (0f - btnSize) * dispPercent + btnSize * 0.5f;
				gaugeEffect_1.localPosition = localPosition2;
			}
			return result;
		}

		public bool PlayEffect2()
		{
			bool result = false;
			if (gaugeEffect_2 == null)
			{
				gaugeEffect_2 = EffectManager.GetUIEffect(useEffectNames[1], effectTransform);
				if (gaugeEffect_2 != null)
				{
					result = true;
				}
			}
			return result;
		}

		public bool PlayEffect3()
		{
			bool result = false;
			if (gaugeEffect_3 == null)
			{
				gaugeEffect_3 = EffectManager.GetUIEffect(useEffectNames[3], effectTransform);
				if (gaugeEffect_3 != null)
				{
					result = true;
				}
			}
			gaugeEffect_3.gameObject.SetActive(value: true);
			return result;
		}

		public void PlayEffectMax(Transform frame)
		{
			gaugeEffect_Max = EffectManager.GetUIEffect(useEffectNames[2], frame);
		}

		public void PlayEffectPlaySkill(Transform frame)
		{
			EffectManager.GetUIEffect(useEffectNames[4], frame);
		}

		public void ReleaseEffect1()
		{
			if (!(gaugeEffect_1 == null))
			{
				Vector3 localPosition = gaugeEffect_1.localPosition;
				localPosition.y = btnSize * 0.5f;
				gaugeEffect_1.localPosition = localPosition;
				EffectManager.ReleaseEffect(gaugeEffect_1.gameObject);
				gaugeEffect_1 = null;
			}
		}

		public void ReleaseEffect2()
		{
			if (!(gaugeEffect_2 == null))
			{
				EffectManager.ReleaseEffect(gaugeEffect_2.gameObject);
				gaugeEffect_2 = null;
			}
		}

		public void HideEffect3()
		{
			if (!(gaugeEffect_3 == null))
			{
				gaugeEffect_3.gameObject.SetActive(value: false);
			}
		}

		public void ReleaseEffects()
		{
			if (gaugeEffect_1 != null)
			{
				UnityEngine.Object.Destroy(gaugeEffect_1.gameObject);
				gaugeEffect_1 = null;
			}
			if (gaugeEffect_2 != null)
			{
				UnityEngine.Object.Destroy(gaugeEffect_2.gameObject);
				gaugeEffect_2 = null;
			}
			if (gaugeEffect_3 != null)
			{
				UnityEngine.Object.Destroy(gaugeEffect_3.gameObject);
				gaugeEffect_3 = null;
			}
			if (gaugeEffect_Max != null)
			{
				UnityEngine.Object.Destroy(gaugeEffect_Max.gameObject);
				gaugeEffect_Max = null;
			}
		}

		public void OnDisable()
		{
			iconTexture.transform.localPosition = skillIconOnPos;
			iconTexture.transform.localScale = Vector3.one;
			iconTexture.alpha = 1f;
			if (alphaTween != null)
			{
				alphaTween.enabled = false;
				alphaTween = null;
			}
			if (scaleTween != null)
			{
				scaleTween.enabled = false;
				scaleTween = null;
			}
		}

		public void PlayTween()
		{
			Transform transform = iconTexture.transform;
			Vector3 localPosition = iconTexture.transform.localPosition;
			localPosition.z = 0f;
			transform.localPosition = localPosition;
			alphaTween = TweenAlpha.Begin(transform.gameObject, 0.5f, 0.01f);
			scaleTween = TweenScale.Begin(transform.gameObject, 0.5f, new Vector3(2f, 2f, 2f));
		}
	}

	private enum GAUGE_GRADE
	{
		NONE,
		FIRST,
		SECOND
	}

	private const string SKILL_ON_ATK_GAUGE_NAME = "skill_plate_r_on";

	private const string SKILL_ON_HEAL_GAUGE_NAME = "skill_plate_g_on";

	private const string SKILL_ON_SUPPORT_GAUGE_NAME = "skill_plate_b_on";

	private const string SKILL_ON_ATK_GAUGE_NAME_2ND = "skill_plate_y_on";

	private const string SKILL_ON_HEAL_GAUGE_NAME_2ND = "skill_plate_g_on";

	private const string SKILL_ON_SUPPORT_GAUGE_NAME_2ND = "skill_plate_b_on";

	private const string SKILL_OFF_ATK_GAUGE_NAME = "skill_plate_attack_off";

	private const string SKILL_OFF_HEAL_GAUGE_NAME = "skill_plate_heal_off";

	private const string SKILL_OFF_SUPPORT_GAUGE_NAME = "skill_plate_off";

	private const string SILENCE_ATK_ICON_NAME = "skill_plate_r_lock";

	private const string SILENCE_HEAL_ICON_NAME = "skill_plate_g_lock";

	private const string SILENCE_SUPPORT_ICON_NAME = "skill_plate_b_lock";

	public static readonly string[] effect_red = new string[5]
	{
		"ef_ui_skillgauge_red_01",
		"ef_ui_skillgauge_red_02",
		"ef_ui_skillgauge_red_03",
		"ef_ui_skillgauge_red_04",
		"ef_ui_skillgauge_red_05"
	};

	public static readonly string[] effect_yellow = new string[5]
	{
		"ef_ui_skillgauge_yellow_01",
		"ef_ui_skillgauge_yellow_02",
		"ef_ui_skillgauge_yellow_03",
		"ef_ui_skillgauge_yellow_04",
		"ef_ui_skillgauge_yellow_05"
	};

	public static readonly string[] effect_green = new string[5]
	{
		"ef_ui_skillgauge_green_01",
		"ef_ui_skillgauge_green_02",
		"ef_ui_skillgauge_green_03",
		"ef_ui_skillgauge_green_04",
		"ef_ui_skillgauge_green_05"
	};

	public static readonly string[] effect_blue = new string[5]
	{
		"ef_ui_skillgauge_blue_01",
		"ef_ui_skillgauge_blue_02",
		"ef_ui_skillgauge_blue_03",
		"ef_ui_skillgauge_blue_04",
		"ef_ui_skillgauge_blue_05"
	};

	[SerializeField]
	protected Transform frame;

	[SerializeField]
	protected UITexture skillIconOff;

	[SerializeField]
	protected UISprite skillTypeOFF;

	[SerializeField]
	protected SkillGauge skillGauge1;

	[SerializeField]
	protected SkillGauge skillGauge2;

	[SerializeField]
	protected UIButton skillButton;

	[SerializeField]
	protected UITexture skillTypeMask;

	[SerializeField]
	protected UIHGauge skillGaugeMask;

	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	[SerializeField]
	protected GameObject silenceBase;

	[SerializeField]
	protected UISprite silenceBg;

	[SerializeField]
	protected UISprite silenceIcon;

	protected bool btnEnable;

	protected bool isPrevGaugeMax;

	protected bool playSkill;

	protected Player target;

	public bool upDateStop;

	protected bool requestCheck;

	protected int gaugeMaxSEId;

	private IEnumerator routineWork;

	public int buttonIndex
	{
		get;
		protected set;
	}

	public UIHGauge GetCoolTimeGauge()
	{
		return skillGaugeMask;
	}

	public UISkillButton()
	{
		buttonIndex = -1;
	}

	private void Awake()
	{
		skillGauge1.maxEffect.Init(this);
		skillGauge2.maxEffect.Init(this);
		silenceBase.SetActive(value: false);
		if (skillGaugeMask != null)
		{
			UIWidget component = skillGaugeMask.gameObject.GetComponent<UIWidget>();
			skillGauge1.Init(component.height);
			skillGauge2.Init(component.height);
		}
		if (skillButton != null)
		{
			btnEnable = skillButton.isEnabled;
		}
		base.gameObject.AddComponent<UIButtonEffect>().isSimple = true;
	}

	private void OnDisable()
	{
		skillGauge1.maxEffect.Init(this);
		skillGauge2.maxEffect.Init(this);
		silenceBase.SetActive(value: false);
		if (playSkill)
		{
			StopCoroutine(routineWork);
			int depth = skillGaugeMask.GetComponent<UIWidget>().depth;
			skillGauge1.MoveToFront(depth);
			skillGauge2.MoveToFront(depth);
			skillGauge1.OnDisable();
			skillGauge2.OnDisable();
			panelChange.Lock();
			playSkill = false;
			routineWork = null;
		}
	}

	public void SetTareget(Player player)
	{
		target = player;
	}

	public void SetButtonIndex(int button_index)
	{
		this.buttonIndex = button_index + target.skillInfo.weaponOffset;
		if (!(target == null))
		{
			int buttonIndex = this.buttonIndex;
			SkillInfo.SkillParam skillParam = target.skillInfo.GetSkillParam(buttonIndex);
			if (skillParam != null && skillParam.IsActiveType())
			{
				float percent = 1f - target.skillInfo.GetPercentUseGauge(buttonIndex);
				ChengeSkillType(buttonIndex, skillParam.tableData, percent);
			}
		}
	}

	private GAUGE_GRADE GetGaugeGrade()
	{
		int buttonIndex = this.buttonIndex;
		SkillInfo.SkillParam skillParam = target.skillInfo.GetSkillParam(buttonIndex);
		if (skillParam == null)
		{
			return GAUGE_GRADE.NONE;
		}
		if ((float)(int)skillParam.useGauge2 <= 0f)
		{
			return GAUGE_GRADE.FIRST;
		}
		if (skillGauge1.percent != 0f)
		{
			return GAUGE_GRADE.FIRST;
		}
		return GAUGE_GRADE.SECOND;
	}

	private void GetGauges(out SkillGauge activeGauge, out SkillGauge inactiveGauge)
	{
		switch (GetGaugeGrade())
		{
		default:
			activeGauge = skillGauge1;
			inactiveGauge = skillGauge2;
			break;
		case GAUGE_GRADE.SECOND:
			activeGauge = skillGauge2;
			inactiveGauge = skillGauge1;
			break;
		case GAUGE_GRADE.NONE:
			activeGauge = null;
			inactiveGauge = null;
			break;
		}
	}

	private void Update()
	{
		if (buttonIndex == -1)
		{
			return;
		}
		skillGauge1.percent = 1f - target.skillInfo.GetPercentUseGauge(buttonIndex);
		skillGauge2.percent = 1f - target.skillInfo.GetPercentUseGauge2nd(buttonIndex);
		if (target.isUsingSecondGradeSkill && target.skillInfo.GetSkillParam(buttonIndex).isUsingSecondGrade)
		{
			skillGauge1.percent = 1f;
			skillGauge2.percent = 1f;
		}
		GetGauges(out SkillGauge activeGauge, out SkillGauge inactiveGauge);
		if (activeGauge == null || inactiveGauge == null)
		{
			skillGauge1.SetActive(isActive: false);
			skillGauge2.SetActive(isActive: false);
			skillGaugeMask.SetPercent(1f, anim: false);
			return;
		}
		RequestCheck(activeGauge, inactiveGauge);
		if (upDateStop)
		{
			skillGauge1.SetActive(isActive: false);
			skillGauge2.SetActive(isActive: false);
			skillGaugeMask.SetPercent(1f, anim: false);
			return;
		}
		skillGauge1.SetActive(isActive: true);
		if (activeGauge == skillGauge2)
		{
			skillGauge2.SetActive(isActive: true);
		}
		else
		{
			skillGauge2.SetActive(isActive: false);
		}
		UpdateSilence();
		if (skillButton != null)
		{
			bool flag = IsEnable();
			if (btnEnable != flag)
			{
				skillButton.isEnabled = flag;
				btnEnable = flag;
			}
		}
		UpdateSkillButton(activeGauge, inactiveGauge);
	}

	private void SetDepthOfGauges(SkillGauge activeGauge, SkillGauge inactiveGauge)
	{
		int depth = skillGaugeMask.GetComponent<UIWidget>().depth;
		activeGauge.MoveToFront(depth);
		inactiveGauge.MoveToBack(depth);
	}

	private void UpdateSkillButton(SkillGauge activeGauge, SkillGauge inactiveGauge)
	{
		if (!(skillGaugeMask == null))
		{
			SetDepthOfGauges(activeGauge, inactiveGauge);
			bool isGraphicOptOverLow = MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0;
			if (UpdateSkillGauge(activeGauge, inactiveGauge, isGraphicOptOverLow))
			{
				activeGauge.effectTransform.gameObject.SetActive(value: false);
				activeGauge.effectTransform.gameObject.SetActive(value: true);
			}
		}
	}

	private bool UpdateSkillGauge(SkillGauge activeGauge, SkillGauge inactiveGauge, bool isGraphicOptOverLow)
	{
		bool result = false;
		float num = activeGauge.percent;
		if (num > 0f && num < 0.3f)
		{
			num = 0.3f;
		}
		if (num <= 0f)
		{
			skillGaugeMask.SetPercent(num, anim: false);
			result = UpdateFullChargedGaugeEffect(activeGauge, isGraphicOptOverLow);
			if (!activeGauge.isPrevGaugeMax)
			{
				PlayFullChargeEffect(activeGauge, isGraphicOptOverLow);
			}
			activeGauge.isPrevGaugeMax = true;
		}
		else
		{
			skillGaugeMask.SetPercent(num, anim: false);
			activeGauge.isPrevGaugeMax = false;
			activeGauge.maxEffect.Init(this);
			if (num >= 1f)
			{
				activeGauge.ReleaseEffects();
			}
			else
			{
				result = UpdateChargingGaugeEffect(activeGauge, num, isGraphicOptOverLow);
			}
		}
		if (inactiveGauge.percent <= 0f)
		{
			if (!inactiveGauge.isPrevGaugeMax)
			{
				PlayFullChargeEffect(inactiveGauge, isGraphicOptOverLow);
			}
			inactiveGauge.isPrevGaugeMax = true;
		}
		else
		{
			inactiveGauge.ReleaseEffects();
			inactiveGauge.maxEffect.Init(this);
			inactiveGauge.isPrevGaugeMax = false;
		}
		return result;
	}

	private bool UpdateChargingGaugeEffect(SkillGauge skillGauge, float percent, bool isGraphicOptOverLow)
	{
		if (!isGraphicOptOverLow)
		{
			skillGauge.ReleaseEffects();
			return false;
		}
		int result = 0 | (skillGauge.PlayEffect1(percent) ? 1 : 0) | (skillGauge.PlayEffect2() ? 1 : 0);
		skillGauge.HideEffect3();
		return (byte)result != 0;
	}

	private bool UpdateFullChargedGaugeEffect(SkillGauge skillGauge, bool isGraphicOptOverLow)
	{
		if (!isGraphicOptOverLow)
		{
			skillGauge.ReleaseEffects();
			return false;
		}
		skillGauge.ReleaseEffect1();
		skillGauge.ReleaseEffect2();
		return (byte)(0 | (skillGauge.PlayEffect3() ? 1 : 0)) != 0;
	}

	private void PlayFullChargeEffect(SkillGauge skillGauge, bool isGraphicOptOverLow)
	{
		if (!target.IsValidBuffSilence())
		{
			SoundManager.PlayOneShotUISE(gaugeMaxSEId);
			if (isGraphicOptOverLow)
			{
				skillGauge.maxEffect.Play(this);
			}
		}
	}

	private void UpdateSilence()
	{
		bool flag = false;
		if (silenceBase != null)
		{
			if (target != null && target.IsValidBuffSilence())
			{
				flag = true;
			}
			if (silenceBase.activeInHierarchy != flag)
			{
				silenceBase.SetActive(flag);
			}
		}
	}

	public void ReleaseEffects()
	{
		skillGauge1.ReleaseEffects();
		skillGauge2.ReleaseEffects();
	}

	protected void ChengeSkillType(int index, SkillItemTable.SkillItemData data, float percent)
	{
		ReleaseEffects();
		if (skillGaugeMask != null)
		{
			if (percent > 0f)
			{
				if (percent < 0.2f)
				{
					percent = 0.2f;
				}
				skillGaugeMask.SetPercent(percent, anim: false);
			}
			else
			{
				skillGaugeMask.SetPercent(percent, anim: false);
			}
		}
		if (skillIconOff != null)
		{
			skillIconOff.gameObject.SetActive(value: true);
			ResourceLoad.LoadItemIconTexture(skillIconOff, data.iconID);
		}
		if (skillGauge1 != null && skillGauge1.iconTexture != null)
		{
			ResourceLoad.LoadItemIconTexture(skillGauge1.iconTexture, data.iconID);
		}
		if (skillGauge2 != null && skillGauge2.iconTexture != null)
		{
			ResourceLoad.LoadItemIconTexture(skillGauge2.iconTexture, data.iconID);
		}
		SetSlotType(data.type);
		skillTypeOFF.alpha = 1f;
		isPrevGaugeMax = (percent <= 0f);
		skillGauge1.maxEffect.Init(this);
		skillGauge2.maxEffect.Init(this);
		UpdateSilence();
	}

	public void SetInActiveSlot(SKILL_SLOT_TYPE type)
	{
		buttonIndex = -1;
		ReleaseEffects();
		if (skillGaugeMask != null)
		{
			skillGaugeMask.SetPercent(1f, anim: false);
		}
		if (skillIconOff != null)
		{
			skillIconOff.gameObject.SetActive(value: false);
		}
		if (skillGauge1 != null)
		{
			skillGauge1.iconTexture.gameObject.SetActive(value: false);
		}
		if (skillGauge2 != null)
		{
			skillGauge2.iconTexture.gameObject.SetActive(value: false);
		}
		SetSlotType(type);
		skillTypeOFF.alpha = 0.5f;
		isPrevGaugeMax = false;
		int depth = skillGaugeMask.GetComponent<UIWidget>().depth;
		skillGauge1.SetActive(isActive: false);
		skillGauge2.SetActive(isActive: false);
		skillGauge1.MoveToFront(depth);
		skillGauge2.MoveToFront(depth);
		skillGauge1.maxEffect.Init(this);
		skillGauge2.maxEffect.Init(this);
		silenceBase.SetActive(value: false);
		skillButton.isEnabled = false;
		btnEnable = false;
	}

	protected void SetSlotType(SKILL_SLOT_TYPE type)
	{
		switch (type)
		{
		case SKILL_SLOT_TYPE.ATTACK:
			skillGauge1.typeSprite.spriteName = "skill_plate_r_on";
			skillGauge2.typeSprite.spriteName = "skill_plate_y_on";
			skillTypeOFF.spriteName = "skill_plate_attack_off";
			silenceBg.spriteName = "skill_plate_r_on";
			silenceIcon.spriteName = "skill_plate_r_lock";
			skillGauge1.useEffectNames = effect_red;
			skillGauge2.useEffectNames = effect_yellow;
			gaugeMaxSEId = 40000120;
			break;
		case SKILL_SLOT_TYPE.HEAL:
			skillGauge1.typeSprite.spriteName = "skill_plate_g_on";
			skillGauge2.typeSprite.spriteName = "skill_plate_g_on";
			skillTypeOFF.spriteName = "skill_plate_heal_off";
			silenceBg.spriteName = "skill_plate_g_on";
			silenceIcon.spriteName = "skill_plate_g_lock";
			skillGauge1.useEffectNames = effect_green;
			skillGauge2.useEffectNames = effect_yellow;
			gaugeMaxSEId = 40000120;
			break;
		case SKILL_SLOT_TYPE.SUPPORT:
			skillGauge1.typeSprite.spriteName = "skill_plate_b_on";
			skillGauge2.typeSprite.spriteName = "skill_plate_b_on";
			skillTypeOFF.spriteName = "skill_plate_off";
			silenceBg.spriteName = "skill_plate_b_on";
			silenceIcon.spriteName = "skill_plate_b_lock";
			skillGauge1.useEffectNames = effect_blue;
			skillGauge2.useEffectNames = effect_yellow;
			gaugeMaxSEId = 40000120;
			break;
		}
		skillTypeMask.mainTexture = MonoBehaviourSingleton<UISkillButtonGroup>.I.GetMaskTexture(type);
	}

	public bool IsEnable()
	{
		if (this.buttonIndex < 0)
		{
			return false;
		}
		if (target == null)
		{
			return false;
		}
		int buttonIndex = this.buttonIndex;
		SkillInfo.SkillParam skillParam = target.skillInfo.GetSkillParam(buttonIndex);
		if (skillParam == null || !skillParam.IsActiveType())
		{
			return false;
		}
		SelfController selfController = target.controller as SelfController;
		if (selfController == null)
		{
			return false;
		}
		if (selfController.IsCancelNextNotCancel())
		{
			return false;
		}
		return target.IsActSkillAction(buttonIndex);
	}

	private bool IsSelfCommandCheck()
	{
		int buttonIndex = this.buttonIndex;
		SelfController selfController = target.controller as SelfController;
		if (selfController == null)
		{
			return false;
		}
		if (selfController.nextCommand != null && selfController.nextCommand.type == SelfController.COMMAND_TYPE.SKILL && selfController.nextCommand.skillIndex == buttonIndex)
		{
			return true;
		}
		return false;
	}

	public void OnClick()
	{
		if (this.buttonIndex < 0 || target == null)
		{
			return;
		}
		SelfController selfController = target.controller as SelfController;
		if (!(selfController == null) && !selfController.IsCancelNextNotCancel())
		{
			int buttonIndex = this.buttonIndex;
			if (selfController.OnSkillButtonPress(buttonIndex))
			{
				requestCheck = true;
				skillButton.isEnabled = false;
				btnEnable = false;
			}
		}
	}

	private void RequestCheck(SkillGauge activeGauge, SkillGauge inactiveGauge)
	{
		if (!requestCheck || IsSelfCommandCheck())
		{
			return;
		}
		activeGauge.HideEffect3();
		inactiveGauge.HideEffect3();
		requestCheck = false;
		if (target.actionID != (Character.ACTION_ID)22)
		{
			return;
		}
		if (frame != null)
		{
			activeGauge.PlayEffectPlaySkill(frame);
		}
		ReleaseEffects();
		if (skillGaugeMask != null && activeGauge == skillGauge2)
		{
			if (skillGauge2.percent > 0f)
			{
				inactiveGauge.percent = activeGauge.percent;
			}
			else
			{
				inactiveGauge.percent = 1f;
				activeGauge.percent = 1f;
			}
		}
		if (activeGauge != null && !playSkill)
		{
			if (routineWork != null)
			{
				StopCoroutine(routineWork);
			}
			routineWork = SkillStart(activeGauge, inactiveGauge);
			StartCoroutine(routineWork);
		}
	}

	private IEnumerator SkillStart(SkillGauge activeGauge, SkillGauge inactiveGauge)
	{
		playSkill = true;
		yield return new WaitForEndOfFrame();
		panelChange.UnLock();
		yield return new WaitForSeconds(1.5f);
		panelChange.Lock();
		playSkill = false;
		routineWork = null;
	}
}
