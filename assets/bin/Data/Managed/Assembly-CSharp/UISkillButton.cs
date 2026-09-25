// Decompiled with JetBrains decompiler
// Type: UISkillButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class UISkillButton : MonoBehaviour
{
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
  protected UISkillButton.SkillGauge skillGauge1;
  [SerializeField]
  protected UISkillButton.SkillGauge skillGauge2;
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

  public int buttonIndex { get; protected set; }

  public UIHGauge GetCoolTimeGauge() => this.skillGaugeMask;

  public UISkillButton() => this.buttonIndex = -1;

  private void Awake()
  {
    this.skillGauge1.maxEffect.Init(this);
    this.skillGauge2.maxEffect.Init(this);
    this.silenceBase.SetActive(false);
    if (Object.op_Inequality((Object) this.skillGaugeMask, (Object) null))
    {
      UIWidget component = ((Component) this.skillGaugeMask).gameObject.GetComponent<UIWidget>();
      this.skillGauge1.Init((float) component.height);
      this.skillGauge2.Init((float) component.height);
    }
    if (Object.op_Inequality((Object) this.skillButton, (Object) null))
      this.btnEnable = this.skillButton.isEnabled;
    ((Component) this).gameObject.AddComponent<UIButtonEffect>().isSimple = true;
  }

  private void OnDisable()
  {
    this.skillGauge1.maxEffect.Init(this);
    this.skillGauge2.maxEffect.Init(this);
    this.silenceBase.SetActive(false);
    if (!this.playSkill)
      return;
    this.StopCoroutine(this.routineWork);
    int depth = ((Component) this.skillGaugeMask).GetComponent<UIWidget>().depth;
    this.skillGauge1.MoveToFront(depth);
    this.skillGauge2.MoveToFront(depth);
    this.skillGauge1.OnDisable();
    this.skillGauge2.OnDisable();
    this.panelChange.Lock();
    this.playSkill = false;
    this.routineWork = (IEnumerator) null;
  }

  public void SetTareget(Player player) => this.target = player;

  public void SetButtonIndex(int button_index)
  {
    this.buttonIndex = button_index + this.target.skillInfo.weaponOffset;
    if (Object.op_Equality((Object) this.target, (Object) null))
      return;
    int buttonIndex = this.buttonIndex;
    SkillInfo.SkillParam skillParam = this.target.skillInfo.GetSkillParam(buttonIndex);
    if (skillParam == null || !skillParam.IsActiveType())
      return;
    float percent = 1f - this.target.skillInfo.GetPercentUseGauge(buttonIndex);
    this.ChengeSkillType(buttonIndex, skillParam.tableData, percent);
  }

  private UISkillButton.GAUGE_GRADE GetGaugeGrade()
  {
    SkillInfo.SkillParam skillParam = this.target.skillInfo.GetSkillParam(this.buttonIndex);
    if (skillParam == null)
      return UISkillButton.GAUGE_GRADE.NONE;
    return (double) (int) skillParam.useGauge2 <= 0.0 || (double) this.skillGauge1.percent != 0.0 ? UISkillButton.GAUGE_GRADE.FIRST : UISkillButton.GAUGE_GRADE.SECOND;
  }

  private void GetGauges(
    out UISkillButton.SkillGauge activeGauge,
    out UISkillButton.SkillGauge inactiveGauge)
  {
    switch (this.GetGaugeGrade())
    {
      case UISkillButton.GAUGE_GRADE.NONE:
        activeGauge = (UISkillButton.SkillGauge) null;
        inactiveGauge = (UISkillButton.SkillGauge) null;
        break;
      case UISkillButton.GAUGE_GRADE.SECOND:
        activeGauge = this.skillGauge2;
        inactiveGauge = this.skillGauge1;
        break;
      default:
        activeGauge = this.skillGauge1;
        inactiveGauge = this.skillGauge2;
        break;
    }
  }

  private void Update()
  {
    if (this.buttonIndex == -1)
      return;
    this.skillGauge1.percent = 1f - this.target.skillInfo.GetPercentUseGauge(this.buttonIndex);
    this.skillGauge2.percent = 1f - this.target.skillInfo.GetPercentUseGauge2nd(this.buttonIndex);
    if (this.target.isUsingSecondGradeSkill && this.target.skillInfo.GetSkillParam(this.buttonIndex).isUsingSecondGrade)
    {
      this.skillGauge1.percent = 1f;
      this.skillGauge2.percent = 1f;
    }
    UISkillButton.SkillGauge activeGauge;
    UISkillButton.SkillGauge inactiveGauge;
    this.GetGauges(out activeGauge, out inactiveGauge);
    if (activeGauge == null || inactiveGauge == null)
    {
      this.skillGauge1.SetActive(false);
      this.skillGauge2.SetActive(false);
      this.skillGaugeMask.SetPercent(1f, false);
    }
    else
    {
      this.RequestCheck(activeGauge, inactiveGauge);
      if (this.upDateStop)
      {
        this.skillGauge1.SetActive(false);
        this.skillGauge2.SetActive(false);
        this.skillGaugeMask.SetPercent(1f, false);
      }
      else
      {
        this.skillGauge1.SetActive(true);
        if (activeGauge == this.skillGauge2)
          this.skillGauge2.SetActive(true);
        else
          this.skillGauge2.SetActive(false);
        this.UpdateSilence();
        if (Object.op_Inequality((Object) this.skillButton, (Object) null))
        {
          bool flag = this.IsEnable();
          if (this.btnEnable != flag)
          {
            this.skillButton.isEnabled = flag;
            this.btnEnable = flag;
          }
        }
        this.UpdateSkillButton(activeGauge, inactiveGauge);
      }
    }
  }

  private void SetDepthOfGauges(
    UISkillButton.SkillGauge activeGauge,
    UISkillButton.SkillGauge inactiveGauge)
  {
    int depth = ((Component) this.skillGaugeMask).GetComponent<UIWidget>().depth;
    activeGauge.MoveToFront(depth);
    inactiveGauge.MoveToBack(depth);
  }

  private void UpdateSkillButton(
    UISkillButton.SkillGauge activeGauge,
    UISkillButton.SkillGauge inactiveGauge)
  {
    if (Object.op_Equality((Object) this.skillGaugeMask, (Object) null))
      return;
    this.SetDepthOfGauges(activeGauge, inactiveGauge);
    bool isGraphicOptOverLow = MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0;
    if (!this.UpdateSkillGauge(activeGauge, inactiveGauge, isGraphicOptOverLow))
      return;
    ((Component) activeGauge.effectTransform).gameObject.SetActive(false);
    ((Component) activeGauge.effectTransform).gameObject.SetActive(true);
  }

  private bool UpdateSkillGauge(
    UISkillButton.SkillGauge activeGauge,
    UISkillButton.SkillGauge inactiveGauge,
    bool isGraphicOptOverLow)
  {
    bool flag = false;
    float percent = activeGauge.percent;
    if ((double) percent > 0.0 && (double) percent < 0.30000001192092896)
      percent = 0.3f;
    if ((double) percent <= 0.0)
    {
      this.skillGaugeMask.SetPercent(percent, false);
      flag = this.UpdateFullChargedGaugeEffect(activeGauge, isGraphicOptOverLow);
      if (!activeGauge.isPrevGaugeMax)
        this.PlayFullChargeEffect(activeGauge, isGraphicOptOverLow);
      activeGauge.isPrevGaugeMax = true;
    }
    else
    {
      this.skillGaugeMask.SetPercent(percent, false);
      activeGauge.isPrevGaugeMax = false;
      activeGauge.maxEffect.Init(this);
      if ((double) percent >= 1.0)
        activeGauge.ReleaseEffects();
      else
        flag = this.UpdateChargingGaugeEffect(activeGauge, percent, isGraphicOptOverLow);
    }
    if ((double) inactiveGauge.percent <= 0.0)
    {
      if (!inactiveGauge.isPrevGaugeMax)
        this.PlayFullChargeEffect(inactiveGauge, isGraphicOptOverLow);
      inactiveGauge.isPrevGaugeMax = true;
    }
    else
    {
      inactiveGauge.ReleaseEffects();
      inactiveGauge.maxEffect.Init(this);
      inactiveGauge.isPrevGaugeMax = false;
    }
    return flag;
  }

  private bool UpdateChargingGaugeEffect(
    UISkillButton.SkillGauge skillGauge,
    float percent,
    bool isGraphicOptOverLow)
  {
    if (!isGraphicOptOverLow)
    {
      skillGauge.ReleaseEffects();
      return false;
    }
    int num = 0 | (skillGauge.PlayEffect1(percent) ? 1 : 0) | (skillGauge.PlayEffect2() ? 1 : 0);
    skillGauge.HideEffect3();
    return num != 0;
  }

  private bool UpdateFullChargedGaugeEffect(
    UISkillButton.SkillGauge skillGauge,
    bool isGraphicOptOverLow)
  {
    if (!isGraphicOptOverLow)
    {
      skillGauge.ReleaseEffects();
      return false;
    }
    skillGauge.ReleaseEffect1();
    skillGauge.ReleaseEffect2();
    return (0 | (skillGauge.PlayEffect3() ? 1 : 0)) != 0;
  }

  private void PlayFullChargeEffect(UISkillButton.SkillGauge skillGauge, bool isGraphicOptOverLow)
  {
    if (this.target.IsValidBuffSilence())
      return;
    SoundManager.PlayOneShotUISE(this.gaugeMaxSEId);
    if (!isGraphicOptOverLow)
      return;
    skillGauge.maxEffect.Play(this);
  }

  private void UpdateSilence()
  {
    bool flag = false;
    if (!Object.op_Inequality((Object) this.silenceBase, (Object) null))
      return;
    if (Object.op_Inequality((Object) this.target, (Object) null) && this.target.IsValidBuffSilence())
      flag = true;
    if (this.silenceBase.activeInHierarchy == flag)
      return;
    this.silenceBase.SetActive(flag);
  }

  public void ReleaseEffects()
  {
    this.skillGauge1.ReleaseEffects();
    this.skillGauge2.ReleaseEffects();
  }

  protected void ChengeSkillType(int index, SkillItemTable.SkillItemData data, float percent)
  {
    this.ReleaseEffects();
    if (Object.op_Inequality((Object) this.skillGaugeMask, (Object) null))
    {
      if ((double) percent > 0.0)
      {
        if ((double) percent < 0.20000000298023224)
          percent = 0.2f;
        this.skillGaugeMask.SetPercent(percent, false);
      }
      else
        this.skillGaugeMask.SetPercent(percent, false);
    }
    if (Object.op_Inequality((Object) this.skillIconOff, (Object) null))
    {
      ((Component) this.skillIconOff).gameObject.SetActive(true);
      ResourceLoad.LoadItemIconTexture(this.skillIconOff, data.iconID);
    }
    if (this.skillGauge1 != null && Object.op_Inequality((Object) this.skillGauge1.iconTexture, (Object) null))
      ResourceLoad.LoadItemIconTexture(this.skillGauge1.iconTexture, data.iconID);
    if (this.skillGauge2 != null && Object.op_Inequality((Object) this.skillGauge2.iconTexture, (Object) null))
      ResourceLoad.LoadItemIconTexture(this.skillGauge2.iconTexture, data.iconID);
    this.SetSlotType(data.type);
    this.skillTypeOFF.alpha = 1f;
    this.isPrevGaugeMax = (double) percent <= 0.0;
    this.skillGauge1.maxEffect.Init(this);
    this.skillGauge2.maxEffect.Init(this);
    this.UpdateSilence();
  }

  public void SetInActiveSlot(SKILL_SLOT_TYPE type)
  {
    this.buttonIndex = -1;
    this.ReleaseEffects();
    if (Object.op_Inequality((Object) this.skillGaugeMask, (Object) null))
      this.skillGaugeMask.SetPercent(1f, false);
    if (Object.op_Inequality((Object) this.skillIconOff, (Object) null))
      ((Component) this.skillIconOff).gameObject.SetActive(false);
    if (this.skillGauge1 != null)
      ((Component) this.skillGauge1.iconTexture).gameObject.SetActive(false);
    if (this.skillGauge2 != null)
      ((Component) this.skillGauge2.iconTexture).gameObject.SetActive(false);
    this.SetSlotType(type);
    this.skillTypeOFF.alpha = 0.5f;
    this.isPrevGaugeMax = false;
    int depth = ((Component) this.skillGaugeMask).GetComponent<UIWidget>().depth;
    this.skillGauge1.SetActive(false);
    this.skillGauge2.SetActive(false);
    this.skillGauge1.MoveToFront(depth);
    this.skillGauge2.MoveToFront(depth);
    this.skillGauge1.maxEffect.Init(this);
    this.skillGauge2.maxEffect.Init(this);
    this.silenceBase.SetActive(false);
    this.skillButton.isEnabled = false;
    this.btnEnable = false;
  }

  protected void SetSlotType(SKILL_SLOT_TYPE type)
  {
    switch (type)
    {
      case SKILL_SLOT_TYPE.ATTACK:
        this.skillGauge1.typeSprite.spriteName = "skill_plate_r_on";
        this.skillGauge2.typeSprite.spriteName = "skill_plate_y_on";
        this.skillTypeOFF.spriteName = "skill_plate_attack_off";
        this.silenceBg.spriteName = "skill_plate_r_on";
        this.silenceIcon.spriteName = "skill_plate_r_lock";
        this.skillGauge1.useEffectNames = UISkillButton.effect_red;
        this.skillGauge2.useEffectNames = UISkillButton.effect_yellow;
        this.gaugeMaxSEId = 40000120;
        break;
      case SKILL_SLOT_TYPE.SUPPORT:
        this.skillGauge1.typeSprite.spriteName = "skill_plate_b_on";
        this.skillGauge2.typeSprite.spriteName = "skill_plate_b_on";
        this.skillTypeOFF.spriteName = "skill_plate_off";
        this.silenceBg.spriteName = "skill_plate_b_on";
        this.silenceIcon.spriteName = "skill_plate_b_lock";
        this.skillGauge1.useEffectNames = UISkillButton.effect_blue;
        this.skillGauge2.useEffectNames = UISkillButton.effect_yellow;
        this.gaugeMaxSEId = 40000120;
        break;
      case SKILL_SLOT_TYPE.HEAL:
        this.skillGauge1.typeSprite.spriteName = "skill_plate_g_on";
        this.skillGauge2.typeSprite.spriteName = "skill_plate_g_on";
        this.skillTypeOFF.spriteName = "skill_plate_heal_off";
        this.silenceBg.spriteName = "skill_plate_g_on";
        this.silenceIcon.spriteName = "skill_plate_g_lock";
        this.skillGauge1.useEffectNames = UISkillButton.effect_green;
        this.skillGauge2.useEffectNames = UISkillButton.effect_yellow;
        this.gaugeMaxSEId = 40000120;
        break;
    }
    this.skillTypeMask.mainTexture = MonoBehaviourSingleton<UISkillButtonGroup>.I.GetMaskTexture(type);
  }

  public bool IsEnable()
  {
    if (this.buttonIndex < 0 || Object.op_Equality((Object) this.target, (Object) null))
      return false;
    int buttonIndex = this.buttonIndex;
    SkillInfo.SkillParam skillParam = this.target.skillInfo.GetSkillParam(buttonIndex);
    if (skillParam == null || !skillParam.IsActiveType())
      return false;
    SelfController controller = this.target.controller as SelfController;
    return !Object.op_Equality((Object) controller, (Object) null) && !controller.IsCancelNextNotCancel() && this.target.IsActSkillAction(buttonIndex);
  }

  private bool IsSelfCommandCheck()
  {
    int buttonIndex = this.buttonIndex;
    SelfController controller = this.target.controller as SelfController;
    return !Object.op_Equality((Object) controller, (Object) null) && controller.nextCommand != null && controller.nextCommand.type == SelfController.COMMAND_TYPE.SKILL && controller.nextCommand.skillIndex == buttonIndex;
  }

  public void OnClick()
  {
    if (this.buttonIndex < 0 || Object.op_Equality((Object) this.target, (Object) null))
      return;
    SelfController controller = this.target.controller as SelfController;
    if (Object.op_Equality((Object) controller, (Object) null) || controller.IsCancelNextNotCancel())
      return;
    int buttonIndex = this.buttonIndex;
    if (!controller.OnSkillButtonPress(buttonIndex))
      return;
    this.requestCheck = true;
    this.skillButton.isEnabled = false;
    this.btnEnable = false;
  }

  private void RequestCheck(
    UISkillButton.SkillGauge activeGauge,
    UISkillButton.SkillGauge inactiveGauge)
  {
    if (!this.requestCheck || this.IsSelfCommandCheck())
      return;
    activeGauge.HideEffect3();
    inactiveGauge.HideEffect3();
    this.requestCheck = false;
    if (this.target.actionID != (Character.ACTION_ID) 22)
      return;
    if (Object.op_Inequality((Object) this.frame, (Object) null))
      activeGauge.PlayEffectPlaySkill(this.frame);
    this.ReleaseEffects();
    if (Object.op_Inequality((Object) this.skillGaugeMask, (Object) null) && activeGauge == this.skillGauge2)
    {
      if ((double) this.skillGauge2.percent > 0.0)
      {
        inactiveGauge.percent = activeGauge.percent;
      }
      else
      {
        inactiveGauge.percent = 1f;
        activeGauge.percent = 1f;
      }
    }
    if (activeGauge == null || this.playSkill)
      return;
    if (this.routineWork != null)
      this.StopCoroutine(this.routineWork);
    this.routineWork = this.SkillStart(activeGauge, inactiveGauge);
    this.StartCoroutine(this.routineWork);
  }

  private IEnumerator SkillStart(
    UISkillButton.SkillGauge activeGauge,
    UISkillButton.SkillGauge inactiveGauge)
  {
    this.playSkill = true;
    yield return (object) new WaitForEndOfFrame();
    this.panelChange.UnLock();
    yield return (object) new WaitForSeconds(1.5f);
    this.panelChange.Lock();
    this.playSkill = false;
    this.routineWork = (IEnumerator) null;
  }

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
      if (this.work != null)
      {
        parent.StopCoroutine(this.work);
        this.work = (IEnumerator) null;
      }
      if (!this.isActive)
        return;
      this.isActive = false;
      if (!Object.op_Inequality((Object) this.obj, (Object) null))
        return;
      this.obj.SetActive(false);
    }

    public void Play(UISkillButton parent)
    {
      this.isActive = true;
      if (Object.op_Inequality((Object) this.obj, (Object) null))
        this.obj.SetActive(true);
      if (Object.op_Inequality((Object) this.alpha, (Object) null))
      {
        this.alpha.ResetToBeginning();
        this.alpha.PlayForward();
      }
      if (Object.op_Inequality((Object) this.scale, (Object) null))
      {
        this.scale.ResetToBeginning();
        this.scale.PlayForward();
      }
      if (this.work != null)
        parent.StopCoroutine(this.work);
      this.work = this.EndCheck();
      parent.StartCoroutine(this.work);
    }

    private IEnumerator EndCheck()
    {
      if (Object.op_Inequality((Object) this.alpha, (Object) null))
      {
        while (((Behaviour) this.alpha).enabled)
          yield return (object) null;
      }
      if (Object.op_Inequality((Object) this.scale, (Object) null))
      {
        while (((Behaviour) this.scale).enabled)
          yield return (object) null;
      }
      this.work = (IEnumerator) null;
      if (Object.op_Inequality((Object) this.obj, (Object) null))
        this.obj.SetActive(false);
      this.isActive = false;
    }
  }

  [Serializable]
  protected class SkillGauge
  {
    public UISprite typeSprite;
    public UITexture iconTexture;
    public UISkillButton.GaugeEffect maxEffect;
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
      this.effectTransform = ((Component) this.iconTexture).transform;
      this.skillIconOnPos = this.effectTransform.localPosition;
    }

    public void SetActive(bool isActive)
    {
      ((Component) this.typeSprite).gameObject.SetActive(isActive);
      ((Component) this.iconTexture).gameObject.SetActive(isActive);
    }

    private void SetDepth(int depth)
    {
      if ((double) this.depth == (double) depth)
        return;
      this.iconTexture.depth = depth + 1;
      this.typeSprite.depth = depth;
      this.depth = (float) depth;
    }

    public void MoveToFront(int depth) => this.SetDepth(depth + 1);

    public void MoveToBack(int depth) => this.SetDepth(depth - 2);

    public bool PlayEffect1(float dispPercent)
    {
      bool flag = false;
      if (Object.op_Equality((Object) this.gaugeEffect_1, (Object) null))
      {
        this.gaugeEffect_1 = EffectManager.GetUIEffect(this.useEffectNames[0], this.effectTransform, -1f);
        if (Object.op_Inequality((Object) this.gaugeEffect_1, (Object) null))
        {
          Vector3 localPosition = this.gaugeEffect_1.localPosition;
          localPosition.x = this.btnSize * 2f;
          this.gaugeEffect_1.localPosition = localPosition;
          flag = true;
        }
      }
      else
      {
        Vector3 localPosition = this.gaugeEffect_1.localPosition;
        localPosition.x = 0.0f;
        localPosition.y = (float) (-(double) this.btnSize * (double) dispPercent + (double) this.btnSize * 0.5);
        this.gaugeEffect_1.localPosition = localPosition;
      }
      return flag;
    }

    public bool PlayEffect2()
    {
      bool flag = false;
      if (Object.op_Equality((Object) this.gaugeEffect_2, (Object) null))
      {
        this.gaugeEffect_2 = EffectManager.GetUIEffect(this.useEffectNames[1], this.effectTransform);
        if (Object.op_Inequality((Object) this.gaugeEffect_2, (Object) null))
          flag = true;
      }
      return flag;
    }

    public bool PlayEffect3()
    {
      bool flag = false;
      if (Object.op_Equality((Object) this.gaugeEffect_3, (Object) null))
      {
        this.gaugeEffect_3 = EffectManager.GetUIEffect(this.useEffectNames[3], this.effectTransform);
        if (Object.op_Inequality((Object) this.gaugeEffect_3, (Object) null))
          flag = true;
      }
      ((Component) this.gaugeEffect_3).gameObject.SetActive(true);
      return flag;
    }

    public void PlayEffectMax(Transform frame)
    {
      this.gaugeEffect_Max = EffectManager.GetUIEffect(this.useEffectNames[2], frame);
    }

    public void PlayEffectPlaySkill(Transform frame)
    {
      EffectManager.GetUIEffect(this.useEffectNames[4], frame);
    }

    public void ReleaseEffect1()
    {
      if (Object.op_Equality((Object) this.gaugeEffect_1, (Object) null))
        return;
      Vector3 localPosition = this.gaugeEffect_1.localPosition;
      localPosition.y = this.btnSize * 0.5f;
      this.gaugeEffect_1.localPosition = localPosition;
      EffectManager.ReleaseEffect(((Component) this.gaugeEffect_1).gameObject);
      this.gaugeEffect_1 = (Transform) null;
    }

    public void ReleaseEffect2()
    {
      if (Object.op_Equality((Object) this.gaugeEffect_2, (Object) null))
        return;
      EffectManager.ReleaseEffect(((Component) this.gaugeEffect_2).gameObject);
      this.gaugeEffect_2 = (Transform) null;
    }

    public void HideEffect3()
    {
      if (Object.op_Equality((Object) this.gaugeEffect_3, (Object) null))
        return;
      ((Component) this.gaugeEffect_3).gameObject.SetActive(false);
    }

    public void ReleaseEffects()
    {
      if (Object.op_Inequality((Object) this.gaugeEffect_1, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.gaugeEffect_1).gameObject);
        this.gaugeEffect_1 = (Transform) null;
      }
      if (Object.op_Inequality((Object) this.gaugeEffect_2, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.gaugeEffect_2).gameObject);
        this.gaugeEffect_2 = (Transform) null;
      }
      if (Object.op_Inequality((Object) this.gaugeEffect_3, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.gaugeEffect_3).gameObject);
        this.gaugeEffect_3 = (Transform) null;
      }
      if (!Object.op_Inequality((Object) this.gaugeEffect_Max, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.gaugeEffect_Max).gameObject);
      this.gaugeEffect_Max = (Transform) null;
    }

    public void OnDisable()
    {
      ((Component) this.iconTexture).transform.localPosition = this.skillIconOnPos;
      ((Component) this.iconTexture).transform.localScale = Vector3.one;
      this.iconTexture.alpha = 1f;
      if (Object.op_Inequality((Object) this.alphaTween, (Object) null))
      {
        ((Behaviour) this.alphaTween).enabled = false;
        this.alphaTween = (UITweener) null;
      }
      if (!Object.op_Inequality((Object) this.scaleTween, (Object) null))
        return;
      ((Behaviour) this.scaleTween).enabled = false;
      this.scaleTween = (UITweener) null;
    }

    public void PlayTween()
    {
      Transform transform = ((Component) this.iconTexture).transform;
      Vector3 localPosition = ((Component) this.iconTexture).transform.localPosition;
      localPosition.z = 0.0f;
      transform.localPosition = localPosition;
      this.alphaTween = (UITweener) TweenAlpha.Begin(((Component) transform).gameObject, 0.5f, 0.01f);
      this.scaleTween = (UITweener) TweenScale.Begin(((Component) transform).gameObject, 0.5f, new Vector3(2f, 2f, 2f));
    }
  }

  private enum GAUGE_GRADE
  {
    NONE,
    FIRST,
    SECOND,
  }
}
