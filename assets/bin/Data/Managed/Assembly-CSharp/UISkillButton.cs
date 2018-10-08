using System;
using System.Collections;
using UnityEngine;

public class UISkillButton
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
					obj.SetActive(false);
				}
			}
		}

		public void Play(UISkillButton parent)
		{
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			isActive = true;
			if (obj != null)
			{
				obj.SetActive(true);
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
				while (alpha.get_enabled())
				{
					yield return (object)null;
				}
			}
			if (scale != null)
			{
				while (scale.get_enabled())
				{
					yield return (object)null;
				}
			}
			work = null;
			if (obj != null)
			{
				obj.SetActive(false);
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
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			this.btnSize = btnSize;
			effectTransform = iconTexture.get_transform();
			skillIconOnPos = effectTransform.get_localPosition();
		}

		public void SetActive(bool isActive)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			typeSprite.get_gameObject().SetActive(isActive);
			iconTexture.get_gameObject().SetActive(isActive);
		}

		private void SetDepth(int depth)
		{
			if (this.depth != (float)depth)
			{
				iconTexture.depth = depth + 1;
				typeSprite.depth = depth;
				this.depth = (float)depth;
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
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			if (gaugeEffect_1 == null)
			{
				gaugeEffect_1 = EffectManager.GetUIEffect(useEffectNames[0], effectTransform, -1f, 0, null);
				if (gaugeEffect_1 != null)
				{
					Vector3 localPosition = gaugeEffect_1.get_localPosition();
					localPosition.x = btnSize * 2f;
					gaugeEffect_1.set_localPosition(localPosition);
					result = true;
				}
			}
			else
			{
				Vector3 localPosition2 = gaugeEffect_1.get_localPosition();
				localPosition2.x = 0f;
				localPosition2.y = (0f - btnSize) * dispPercent + btnSize * 0.5f;
				gaugeEffect_1.set_localPosition(localPosition2);
			}
			return result;
		}

		public bool PlayEffect2()
		{
			bool result = false;
			if (gaugeEffect_2 == null)
			{
				gaugeEffect_2 = EffectManager.GetUIEffect(useEffectNames[1], effectTransform, -0.001f, 0, null);
				if (gaugeEffect_2 != null)
				{
					result = true;
				}
			}
			return result;
		}

		public bool PlayEffect3()
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			if (gaugeEffect_3 == null)
			{
				gaugeEffect_3 = EffectManager.GetUIEffect(useEffectNames[3], effectTransform, -0.001f, 0, null);
				if (gaugeEffect_3 != null)
				{
					result = true;
				}
			}
			gaugeEffect_3.get_gameObject().SetActive(true);
			return result;
		}

		public void PlayEffectMax(Transform frame)
		{
			gaugeEffect_Max = EffectManager.GetUIEffect(useEffectNames[2], frame, -0.001f, 0, null);
		}

		public void PlayEffectPlaySkill(Transform frame)
		{
			EffectManager.GetUIEffect(useEffectNames[4], frame, -0.001f, 0, null);
		}

		public void ReleaseEffect1()
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Expected O, but got Unknown
			if (!(gaugeEffect_1 == null))
			{
				Vector3 localPosition = gaugeEffect_1.get_localPosition();
				localPosition.y = btnSize * 0.5f;
				gaugeEffect_1.set_localPosition(localPosition);
				EffectManager.ReleaseEffect(gaugeEffect_1.get_gameObject(), true, false);
				gaugeEffect_1 = null;
			}
		}

		public void ReleaseEffect2()
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			if (!(gaugeEffect_2 == null))
			{
				EffectManager.ReleaseEffect(gaugeEffect_2.get_gameObject(), true, false);
				gaugeEffect_2 = null;
			}
		}

		public void HideEffect3()
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			if (!(gaugeEffect_3 == null))
			{
				gaugeEffect_3.get_gameObject().SetActive(false);
			}
		}

		public void ReleaseEffects()
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			if (gaugeEffect_1 != null)
			{
				Object.Destroy(gaugeEffect_1.get_gameObject());
				gaugeEffect_1 = null;
			}
			if (gaugeEffect_2 != null)
			{
				Object.Destroy(gaugeEffect_2.get_gameObject());
				gaugeEffect_2 = null;
			}
			if (gaugeEffect_3 != null)
			{
				Object.Destroy(gaugeEffect_3.get_gameObject());
				gaugeEffect_3 = null;
			}
			if (gaugeEffect_Max != null)
			{
				Object.Destroy(gaugeEffect_Max.get_gameObject());
				gaugeEffect_Max = null;
			}
		}

		public void OnDisable()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			iconTexture.get_transform().set_localPosition(skillIconOnPos);
			iconTexture.get_transform().set_localScale(Vector3.get_one());
			iconTexture.alpha = 1f;
			if (alphaTween != null)
			{
				alphaTween.set_enabled(false);
				alphaTween = null;
			}
			if (scaleTween != null)
			{
				scaleTween.set_enabled(false);
				scaleTween = null;
			}
		}

		public void PlayTween()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected O, but got Unknown
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006b: Expected O, but got Unknown
			Transform val = iconTexture.get_transform();
			Vector3 localPosition = iconTexture.get_transform().get_localPosition();
			localPosition.z = 0f;
			val.set_localPosition(localPosition);
			alphaTween = TweenAlpha.Begin(val.get_gameObject(), 0.5f, 0.01f);
			scaleTween = TweenScale.Begin(val.get_gameObject(), 0.5f, new Vector3(2f, 2f, 2f));
		}
	}

	private enum GAUGE_GRADE
	{
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

	public UISkillButton()
		: this()
	{
		buttonIndex = -1;
	}

	public UIHGauge GetCoolTimeGauge()
	{
		return skillGaugeMask;
	}

	private void Awake()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		skillGauge1.maxEffect.Init(this);
		skillGauge2.maxEffect.Init(this);
		silenceBase.SetActive(false);
		if (skillGaugeMask != null)
		{
			UIWidget component = skillGaugeMask.get_gameObject().GetComponent<UIWidget>();
			skillGauge1.Init((float)component.height);
			skillGauge2.Init((float)component.height);
		}
		if (skillButton != null)
		{
			btnEnable = skillButton.isEnabled;
		}
		UIButtonEffect uIButtonEffect = this.get_gameObject().AddComponent<UIButtonEffect>();
		uIButtonEffect.isSimple = true;
	}

	private void OnDisable()
	{
		skillGauge1.maxEffect.Init(this);
		skillGauge2.maxEffect.Init(this);
		silenceBase.SetActive(false);
		if (playSkill)
		{
			this.StopCoroutine(routineWork);
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
		if ((float)(int)skillParam.useGauge2 <= 0f)
		{
			return GAUGE_GRADE.FIRST;
		}
		return (skillGauge1.percent == 0f) ? GAUGE_GRADE.SECOND : GAUGE_GRADE.FIRST;
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
		}
	}

	private void Update()
	{
		if (buttonIndex != -1)
		{
			skillGauge1.percent = 1f - target.skillInfo.GetPercentUseGauge(buttonIndex);
			skillGauge2.percent = 1f - target.skillInfo.GetPercentUseGauge2nd(buttonIndex);
			if (target.isUsingSecondGradeSkill)
			{
				SkillInfo.SkillParam skillParam = target.skillInfo.GetSkillParam(buttonIndex);
				if (skillParam.isUsingSecondGrade)
				{
					skillGauge1.percent = 1f;
					skillGauge2.percent = 1f;
				}
			}
			GetGauges(out SkillGauge activeGauge, out SkillGauge inactiveGauge);
			RequestCheck(activeGauge, inactiveGauge);
			if (upDateStop)
			{
				skillGauge1.SetActive(false);
				skillGauge2.SetActive(false);
				skillGaugeMask.SetPercent(1f, false);
			}
			else
			{
				skillGauge1.SetActive(true);
				if (activeGauge == skillGauge2)
				{
					skillGauge2.SetActive(true);
				}
				else
				{
					skillGauge2.SetActive(false);
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
		}
	}

	private void SetDepthOfGauges(SkillGauge activeGauge, SkillGauge inactiveGauge)
	{
		int depth = skillGaugeMask.GetComponent<UIWidget>().depth;
		activeGauge.MoveToFront(depth);
		inactiveGauge.MoveToBack(depth);
	}

	private void UpdateSkillButton(SkillGauge activeGauge, SkillGauge inactiveGauge)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (!(skillGaugeMask == null))
		{
			SetDepthOfGauges(activeGauge, inactiveGauge);
			bool isGraphicOptOverLow = MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0;
			if (UpdateSkillGauge(activeGauge, inactiveGauge, isGraphicOptOverLow))
			{
				activeGauge.effectTransform.get_gameObject().SetActive(false);
				activeGauge.effectTransform.get_gameObject().SetActive(true);
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
			skillGaugeMask.SetPercent(num, false);
			result = UpdateFullChargedGaugeEffect(activeGauge, isGraphicOptOverLow);
			if (!activeGauge.isPrevGaugeMax)
			{
				PlayFullChargeEffect(activeGauge, isGraphicOptOverLow);
			}
			activeGauge.isPrevGaugeMax = true;
		}
		else
		{
			skillGaugeMask.SetPercent(num, false);
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
		float percent = inactiveGauge.percent;
		if (percent <= 0f)
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
		bool flag = false;
		flag |= skillGauge.PlayEffect1(percent);
		flag |= skillGauge.PlayEffect2();
		skillGauge.HideEffect3();
		return flag;
	}

	private bool UpdateFullChargedGaugeEffect(SkillGauge skillGauge, bool isGraphicOptOverLow)
	{
		if (!isGraphicOptOverLow)
		{
			skillGauge.ReleaseEffects();
			return false;
		}
		bool flag = false;
		skillGauge.ReleaseEffect1();
		skillGauge.ReleaseEffect2();
		return flag | skillGauge.PlayEffect3();
	}

	private void PlayFullChargeEffect(SkillGauge skillGauge, bool isGraphicOptOverLow)
	{
		if (!target.IsValidBuffSilence())
		{
			SoundManager.PlayOneShotUISE(gaugeMaxSEId);
			if (isGraphicOptOverLow)
			{
				if (frame != null)
				{
					skillGauge.PlayEffectMax(frame);
				}
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
			if (silenceBase.get_activeInHierarchy() != flag)
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
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		ReleaseEffects();
		if (skillGaugeMask != null)
		{
			if (percent > 0f)
			{
				if (percent < 0.2f)
				{
					percent = 0.2f;
				}
				skillGaugeMask.SetPercent(percent, false);
			}
			else
			{
				skillGaugeMask.SetPercent(percent, false);
			}
		}
		if (skillIconOff != null)
		{
			skillIconOff.get_gameObject().SetActive(true);
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
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		buttonIndex = -1;
		ReleaseEffects();
		if (skillGaugeMask != null)
		{
			skillGaugeMask.SetPercent(1f, false);
		}
		if (skillIconOff != null)
		{
			skillIconOff.get_gameObject().SetActive(false);
		}
		if (skillGauge1 != null)
		{
			skillGauge1.iconTexture.get_gameObject().SetActive(false);
		}
		if (skillGauge2 != null)
		{
			skillGauge2.iconTexture.get_gameObject().SetActive(false);
		}
		SetSlotType(type);
		skillTypeOFF.alpha = 0.5f;
		isPrevGaugeMax = false;
		int depth = skillGaugeMask.GetComponent<UIWidget>().depth;
		skillGauge1.MoveToFront(depth);
		skillGauge2.MoveToFront(depth);
		skillGauge1.maxEffect.Init(this);
		skillGauge2.maxEffect.Init(this);
		silenceBase.SetActive(false);
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
		if (this.buttonIndex >= 0 && !(target == null))
		{
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
	}

	private void RequestCheck(SkillGauge activeGauge, SkillGauge inactiveGauge)
	{
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		if (requestCheck && !IsSelfCommandCheck())
		{
			activeGauge.HideEffect3();
			inactiveGauge.HideEffect3();
			requestCheck = false;
			if (target.actionID == (Character.ACTION_ID)21)
			{
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
						this.StopCoroutine(routineWork);
					}
					routineWork = SkillStart(activeGauge, inactiveGauge);
					this.StartCoroutine(routineWork);
				}
			}
		}
	}

	private IEnumerator SkillStart(SkillGauge activeGauge, SkillGauge inactiveGauge)
	{
		playSkill = true;
		yield return (object)new WaitForEndOfFrame();
		panelChange.UnLock();
		yield return (object)new WaitForSeconds(1.5f);
		panelChange.Lock();
		playSkill = false;
		routineWork = null;
	}
}
