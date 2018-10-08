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

	private const string SKILL_ON_ATK_GAUGE_NAME = "skill_plate_r_on";

	private const string SKILL_ON_HEAL_GAUGE_NAME = "skill_plate_g_on";

	private const string SKILL_ON_SUPPORT_GAUGE_NAME = "skill_plate_b_on";

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
	protected UITexture skillIconOn;

	[SerializeField]
	protected UISprite skillTypeON;

	[SerializeField]
	protected UISprite skillTypeOFF;

	[SerializeField]
	protected UITexture skillTypeMask;

	[SerializeField]
	protected UIButton skillButton;

	[SerializeField]
	protected UIHGauge coolTimeGuage;

	[SerializeField]
	protected GaugeEffect maxEffect = new GaugeEffect();

	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	[SerializeField]
	protected GameObject silenceBase;

	[SerializeField]
	protected UISprite silenceBg;

	[SerializeField]
	protected UISprite silenceIcon;

	protected Transform gaugeEffect_1;

	protected Transform gaugeEffect_2;

	protected Transform gaugeEffect_3;

	protected Transform gaugeEffect_Max;

	protected Transform effectTransform;

	protected Vector3 skillIconOnPos;

	protected bool btnEnable;

	protected float btnSize;

	protected string[] useEffectNames;

	protected bool isPrevGaugeMax;

	protected bool playSkill;

	protected Player target;

	public bool upDateStop;

	protected bool requestCheck;

	protected int gaugeMaxSEId;

	private IEnumerator routineWork;

	private UITweener alphaTween;

	private UITweener scaleTween;

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
		return coolTimeGuage;
	}

	private void Awake()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		maxEffect.Init(this);
		silenceBase.SetActive(false);
		if (skillIconOn != null)
		{
			effectTransform = skillIconOn.get_gameObject().get_transform();
			skillIconOnPos = effectTransform.get_localPosition();
		}
		else
		{
			effectTransform = this.get_gameObject().get_transform();
		}
		if (coolTimeGuage != null)
		{
			UIWidget component = coolTimeGuage.get_gameObject().GetComponent<UIWidget>();
			btnSize = (float)component.height;
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
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		maxEffect.Init(this);
		silenceBase.SetActive(false);
		if (playSkill)
		{
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
			this.StopCoroutine(routineWork);
			skillIconOn.get_transform().set_localPosition(skillIconOnPos);
			skillIconOn.get_transform().set_localScale(Vector3.get_one());
			skillIconOn.alpha = 1f;
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

	private void Update()
	{
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Expected O, but got Unknown
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		if (this.buttonIndex != -1)
		{
			RequestCheck();
			if (!upDateStop)
			{
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
				if (!playSkill)
				{
					bool flag2 = false;
					int buttonIndex = this.buttonIndex;
					float num = 1f - target.skillInfo.GetPercentUseGauge(buttonIndex);
					bool flag3 = MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0;
					if (coolTimeGuage != null)
					{
						if (num > 0f)
						{
							if (num < 0.3f)
							{
								num = 0.3f;
							}
							coolTimeGuage.SetPercent(num, false);
							if (flag3)
							{
								if (num < 1f)
								{
									if (gaugeEffect_1 == null)
									{
										gaugeEffect_1 = EffectManager.GetUIEffect(useEffectNames[0], effectTransform, -1f, 0, null);
										if (gaugeEffect_1 != null)
										{
											Vector3 localPosition = gaugeEffect_1.get_localPosition();
											localPosition.x = btnSize * 2f;
											gaugeEffect_1.set_localPosition(localPosition);
											flag2 = true;
										}
									}
									else
									{
										Vector3 localPosition2 = gaugeEffect_1.get_localPosition();
										localPosition2.x = 0f;
										localPosition2.y = (0f - btnSize) * num + btnSize * 0.5f;
										gaugeEffect_1.set_localPosition(localPosition2);
									}
									if (gaugeEffect_2 == null)
									{
										gaugeEffect_2 = EffectManager.GetUIEffect(useEffectNames[1], effectTransform, -0.001f, 0, null);
										if (gaugeEffect_2 != null)
										{
											flag2 = true;
										}
									}
								}
								else
								{
									ReleaseEffects();
								}
								if (gaugeEffect_3 != null)
								{
									EffectManager.ReleaseEffect(gaugeEffect_3.get_gameObject(), true, false);
									gaugeEffect_3 = null;
								}
							}
							isPrevGaugeMax = false;
							maxEffect.Init(this);
						}
						else
						{
							coolTimeGuage.SetPercent(num, false);
							if (flag3)
							{
								if (gaugeEffect_1 != null)
								{
									Vector3 localPosition3 = gaugeEffect_1.get_localPosition();
									localPosition3.y = btnSize * 0.5f;
									gaugeEffect_1.set_localPosition(localPosition3);
									EffectManager.ReleaseEffect(gaugeEffect_1.get_gameObject(), true, false);
									gaugeEffect_1 = null;
								}
								if (gaugeEffect_2 != null)
								{
									EffectManager.ReleaseEffect(gaugeEffect_2.get_gameObject(), true, false);
									gaugeEffect_2 = null;
								}
								if (gaugeEffect_3 == null)
								{
									gaugeEffect_3 = EffectManager.GetUIEffect(useEffectNames[3], effectTransform, -0.001f, 0, null);
									if (gaugeEffect_3 != null)
									{
										flag2 = true;
									}
								}
							}
							if (!isPrevGaugeMax && !target.IsValidBuffSilence())
							{
								if (flag3)
								{
									if (frame != null)
									{
										gaugeEffect_Max = EffectManager.GetUIEffect(useEffectNames[2], frame, -0.001f, 0, null);
									}
									maxEffect.Play(this);
								}
								SoundManager.PlayOneShotUISE(gaugeMaxSEId);
							}
							isPrevGaugeMax = true;
						}
					}
					if (flag2)
					{
						effectTransform.get_gameObject().SetActive(false);
						effectTransform.get_gameObject().SetActive(true);
					}
				}
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

	protected void ChengeSkillType(int index, SkillItemTable.SkillItemData data, float percent)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		ReleaseEffects();
		if (coolTimeGuage != null)
		{
			if (percent > 0f)
			{
				if (percent < 0.2f)
				{
					percent = 0.2f;
				}
				coolTimeGuage.SetPercent(percent, false);
			}
			else
			{
				coolTimeGuage.SetPercent(percent, false);
			}
		}
		if (skillIconOff != null)
		{
			skillIconOff.get_gameObject().SetActive(true);
			ResourceLoad.LoadItemIconTexture(skillIconOff, data.iconID);
		}
		if (skillIconOn != null)
		{
			skillIconOn.get_gameObject().SetActive(true);
			ResourceLoad.LoadItemIconTexture(skillIconOn, data.iconID);
		}
		SetSlotType(data.type);
		skillTypeOFF.alpha = 1f;
		isPrevGaugeMax = (percent <= 0f);
		maxEffect.Init(this);
		UpdateSilence();
	}

	public void SetInActiveSlot(SKILL_SLOT_TYPE type)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		buttonIndex = -1;
		ReleaseEffects();
		if (coolTimeGuage != null)
		{
			coolTimeGuage.SetPercent(1f, false);
		}
		if (skillIconOff != null)
		{
			skillIconOff.get_gameObject().SetActive(false);
		}
		if (skillIconOn != null)
		{
			skillIconOn.get_gameObject().SetActive(false);
		}
		SetSlotType(type);
		skillTypeOFF.alpha = 0.5f;
		isPrevGaugeMax = false;
		maxEffect.Init(this);
		silenceBase.SetActive(false);
		skillButton.isEnabled = false;
		btnEnable = false;
	}

	protected void SetSlotType(SKILL_SLOT_TYPE type)
	{
		switch (type)
		{
		case SKILL_SLOT_TYPE.ATTACK:
			skillTypeON.spriteName = "skill_plate_r_on";
			skillTypeOFF.spriteName = "skill_plate_attack_off";
			silenceBg.spriteName = "skill_plate_r_on";
			silenceIcon.spriteName = "skill_plate_r_lock";
			useEffectNames = effect_red;
			gaugeMaxSEId = 40000120;
			break;
		case SKILL_SLOT_TYPE.HEAL:
			skillTypeON.spriteName = "skill_plate_g_on";
			skillTypeOFF.spriteName = "skill_plate_heal_off";
			silenceBg.spriteName = "skill_plate_g_on";
			silenceIcon.spriteName = "skill_plate_g_lock";
			useEffectNames = effect_green;
			gaugeMaxSEId = 40000120;
			break;
		case SKILL_SLOT_TYPE.SUPPORT:
			skillTypeON.spriteName = "skill_plate_b_on";
			skillTypeOFF.spriteName = "skill_plate_off";
			silenceBg.spriteName = "skill_plate_b_on";
			silenceIcon.spriteName = "skill_plate_b_lock";
			useEffectNames = effect_blue;
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

	private void RequestCheck()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		if (requestCheck && !IsSelfCommandCheck())
		{
			if (gaugeEffect_3 != null)
			{
				Object.Destroy(gaugeEffect_3.get_gameObject());
				gaugeEffect_3 = null;
			}
			requestCheck = false;
			if (target.actionID == (Character.ACTION_ID)20)
			{
				if (frame != null)
				{
					EffectManager.GetUIEffect(useEffectNames[4], frame, -1f, 0, null);
				}
				ReleaseEffects();
				if (coolTimeGuage != null)
				{
					coolTimeGuage.SetPercent(1f, false);
				}
				if (skillIconOn != null && !playSkill)
				{
					if (routineWork != null)
					{
						this.StopCoroutine(routineWork);
					}
					routineWork = SkillStart();
					this.StartCoroutine(routineWork);
				}
			}
		}
	}

	private IEnumerator SkillStart()
	{
		playSkill = true;
		yield return (object)new WaitForEndOfFrame();
		panelChange.UnLock();
		Vector3 v = skillIconOn.get_transform().get_localPosition();
		v.z = 0f;
		skillIconOn.get_transform().set_localPosition(v);
		alphaTween = TweenAlpha.Begin(skillIconOn.get_gameObject(), 0.5f, 0.01f);
		scaleTween = TweenScale.Begin(skillIconOn.get_gameObject(), 0.5f, new Vector3(2f, 2f, 2f));
		yield return (object)new WaitForSeconds(1.5f);
		skillIconOn.get_transform().set_localPosition(skillIconOnPos);
		skillIconOn.get_transform().set_localScale(Vector3.get_one());
		skillIconOn.alpha = 1f;
		panelChange.Lock();
		playSkill = false;
		alphaTween = null;
		scaleTween = null;
		routineWork = null;
	}
}
