// Decompiled with JetBrains decompiler
// Type: UIPlayerStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class UIPlayerStatus : MonoBehaviourSingleton<UIPlayerStatus>
{
  public static readonly string UI_BURST_BULLET = "InternalUI/UI_InGame/Burst/InGameUIBurstBullet";
  public static readonly Vector3 UI_BURST_BULLET_POS = new Vector3(-35.3f, -55.5f, 0.0f);
  [SerializeField]
  protected UILabel playerName;
  [SerializeField]
  protected UILabel playerHp;
  [SerializeField]
  protected UILabel playerShieldHp;
  [SerializeField]
  protected UIHGauge hpGaugeUI;
  [SerializeField]
  protected UIHGauge healHpGaugeUI;
  [SerializeField]
  protected UIHGauge shieldHpGaugeUI;
  [SerializeField]
  protected UIPlayerStatus.ShieldHpGaugeEffect shieldHpGaugeADD;
  [SerializeField]
  protected GameObject itemInfo;
  [SerializeField]
  protected Transform dropIconN;
  [SerializeField]
  protected UILabel dropInfoN;
  [SerializeField]
  protected Transform dropIconR;
  [SerializeField]
  protected UILabel dropInfoR;
  [SerializeField]
  protected UIWeaponChange weaponChange;
  [SerializeField]
  protected UIEvolveGauge evolveGauge;
  [SerializeField]
  protected UIStatusIcon statusIcons;
  [SerializeField]
  protected UILabel lv;
  [SerializeField]
  protected UIHGauge expGauge;
  [SerializeField]
  protected UIPlayerStatus.SpActionGaugeInfo spActionGaugeInfo;
  [SerializeField]
  protected UIPlayerStatus.ShieldHpGaugeEffect spActionGaugeADD_BOOST;
  [SerializeField]
  protected UIPlayerStatus.ShieldHpGaugeEffect spActionGaugeADD_HALF;
  [SerializeField]
  protected UIPlayerStatus.ShieldHpGaugeEffect spActionTimerGaugeADD_BOOST;
  [SerializeField]
  protected UIPlayerStatus.ShieldHpGaugeEffect spActionTimerGaugeADD_HALF;
  [SerializeField]
  protected UIPlayerStatus.ShieldHpGaugeEffect burstSpActionTimerGaugeADD_HALF;
  [SerializeField]
  protected UILabel coins;
  [SerializeField]
  protected GameObject[] fieldInfo;
  [SerializeField]
  protected StatusBoostAnimator boostAnimator;
  [SerializeField]
  protected UIPlayerStatus.boostItem[] boostItems;
  [SerializeField]
  protected UILabel boostRate;
  [SerializeField]
  protected UILabel boostTime;
  [SerializeField]
  protected UIStaticPanelChanger panelChange;
  [SerializeField]
  protected float dropEffectTime = 1f;
  [SerializeField]
  protected AnimationCurve dropEffectEaseCurve = Curves.CreateEaseInCurve();
  [SerializeField]
  protected float dropEffectAddRandomMax;
  [SerializeField]
  protected AnimationCurve dropEffectAddCurve;
  [SerializeField]
  private Transform soulEffectDirection;
  [SerializeField]
  private float soulEffectTime = 1f;
  [SerializeField]
  private float soulEffectAddRandomMax = 200f;
  [SerializeField]
  private AnimationCurve soulEffectEaseCurve = Curves.CreateEaseInCurve();
  [SerializeField]
  private AnimationCurve soulEffectAddCurve;
  public UIAutoBattleButton autoBattleButton;
  [SerializeField]
  private UIPlayerStatus.CoopFishingGaugeInfo coopFishingGaugeInfoPortrait;
  [SerializeField]
  private UIPlayerStatus.CoopFishingGaugeInfo coopFishingGaugeInfoLandscape;
  [SerializeField]
  private UIOracleStockUIController oracleStock;
  private UIPlayerStatus.CoopFishingGaugeInfo validCoopFishingGaugeInfo;
  private bool isField;
  private bool permitHGPBoostUpdate = true;
  private int lastHP = -1;
  private int lastShieldHP = -1;
  private int lastLV = -1;
  private int lastMoney = -1;
  private int preWeaponIndex;
  private int preUniqueEquipmentIndex;
  private UIBurstBulletUIController m_burstBulletCtrl;

  public Player targetPlayer { get; protected set; }

  public bool PermitHGPBoostUpdate => this.permitHGPBoostUpdate;

  protected UIPlayerStatus.ShieldHpGaugeEffect GetSpActionGaugeADD_BOOST(
    Player.ATTACK_MODE mode,
    SP_ATTACK_TYPE spAttackType)
  {
    switch (spAttackType)
    {
      case SP_ATTACK_TYPE.BURST:
        return this.spActionTimerGaugeADD_BOOST;
      case SP_ATTACK_TYPE.ORACLE:
        return mode == Player.ATTACK_MODE.SPEAR ? this.spActionTimerGaugeADD_BOOST : this.spActionGaugeADD_BOOST;
      default:
        return this.spActionGaugeADD_BOOST;
    }
  }

  protected UIPlayerStatus.ShieldHpGaugeEffect GetSpActionGaugeADD_HALF(
    Player.ATTACK_MODE mode,
    SP_ATTACK_TYPE spAttackType)
  {
    switch (spAttackType)
    {
      case SP_ATTACK_TYPE.BURST:
        return this.burstSpActionTimerGaugeADD_HALF;
      case SP_ATTACK_TYPE.ORACLE:
        if (mode == Player.ATTACK_MODE.SPEAR)
          return this.burstSpActionTimerGaugeADD_HALF;
        return mode == Player.ATTACK_MODE.PAIR_SWORDS ? this.spActionGaugeADD_HALF : this.spActionTimerGaugeADD_HALF;
      default:
        return this.spActionGaugeADD_HALF;
    }
  }

  protected override void Awake()
  {
    base.Awake();
    this.boostRate.fontStyle = (FontStyle) 2;
    this.boostTime.fontStyle = (FontStyle) 2;
    ((Component) this).gameObject.SetActive(false);
    this.CreateBurstBulletUI();
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.SyncRotatePosition();
  }

  protected override void OnDestroySingleton()
  {
    base.OnDestroySingleton();
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return;
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
  }

  private void OnScreenRotate(bool is_portrait) => this.SyncRotatePosition();

  private void SyncRotatePosition()
  {
    if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.NeedModifyInGamePlayerStatusPosition)
      return;
    DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
    UIWidget component = ((Component) this).gameObject.GetComponent<UIWidget>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    if (SpecialDeviceManager.IsPortrait)
    {
      component.leftAnchor.absolute = specialDeviceInfo.InGameStatusAnchorPortrait.left;
      component.rightAnchor.absolute = specialDeviceInfo.InGameStatusAnchorPortrait.right;
      component.bottomAnchor.absolute = specialDeviceInfo.InGameStatusAnchorPortrait.bottom;
      component.topAnchor.absolute = specialDeviceInfo.InGameStatusAnchorPortrait.top;
    }
    else
    {
      component.leftAnchor.absolute = specialDeviceInfo.InGameStatusAnchorLandscape.left;
      component.rightAnchor.absolute = specialDeviceInfo.InGameStatusAnchorLandscape.right;
      component.bottomAnchor.absolute = specialDeviceInfo.InGameStatusAnchorLandscape.bottom;
      component.topAnchor.absolute = specialDeviceInfo.InGameStatusAnchorLandscape.top;
    }
    component.UpdateAnchors();
  }

  private void CreateBurstBulletUI()
  {
    if (Object.op_Inequality((Object) this.m_burstBulletCtrl, (Object) null))
      return;
    Transform transform = ResourceUtility.Realizes(Resources.Load(UIPlayerStatus.UI_BURST_BULLET), ((Component) this).transform);
    if (Object.op_Equality((Object) transform, (Object) null))
      return;
    transform.localPosition = UIPlayerStatus.UI_BURST_BULLET_POS;
    this.m_burstBulletCtrl = ((Component) transform).GetComponent<UIBurstBulletUIController>();
    if (!Object.op_Inequality((Object) this.m_burstBulletCtrl, (Object) null))
      return;
    this.m_burstBulletCtrl.Initialize(new UIBurstBulletUIController.InitParam()
    {
      MaxBulletCount = 6,
      CurrentRestBulletCount = 6
    });
  }

  private void Start()
  {
    foreach (EffectCtrl componentsInChild in ((Component) this.boostAnimator).GetComponentsInChildren<EffectCtrl>(true))
      componentsInChild.SetRenderQueue(2000);
  }

  public void SetTarget(Player player)
  {
    this.targetPlayer = player;
    if (Object.op_Equality((Object) this.targetPlayer, (Object) null))
    {
      ((Component) this).gameObject.SetActive(false);
    }
    else
    {
      this.statusIcons.target = (Character) player;
      if (Object.op_Inequality((Object) this.weaponChange, (Object) null))
        this.weaponChange.SetTarget(this.targetPlayer);
      this.UpdateUI();
      this.isField = FieldManager.IsValidInGameNoQuest();
      this.itemInfo.SetActive(!this.isField);
      int index = 0;
      for (int length = this.fieldInfo.Length; index < length; ++index)
        this.fieldInfo[index].SetActive(this.isField);
      if (!this.isField)
        this.DropInfoUpdate();
      this.UpDateStatusIcon();
      this.SetUpBoostAnimator();
      ((Component) this).gameObject.SetActive(true);
    }
  }

  public void SetUpBoostAnimator()
  {
    this.boostAnimator.SetupUI((Action<BoostStatus>) (update_boost =>
    {
      if (update_boost != null)
        this.UpdateShowBoost(update_boost);
      else
        this.EndShowBoost();
    }), (Action<BoostStatus>) (change_boost =>
    {
      if (change_boost != null)
      {
        this.ChangeShowBoost((USE_ITEM_EFFECT_TYPE) change_boost.type);
        this.UpdateShowBoost(change_boost);
      }
      else
        this.EndShowBoost();
    }));
  }

  private void LateUpdate()
  {
    if (Object.op_Equality((Object) this.targetPlayer, (Object) null))
      return;
    this.UpdateUI();
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    if (Object.op_Inequality((Object) this.lv, (Object) null) && this.lastLV != (int) userStatus.level)
    {
      this.lv.text = userStatus.level.ToString();
      this.lastLV = (int) userStatus.level;
    }
    if (Object.op_Inequality((Object) this.expGauge, (Object) null))
      this.expGauge.SetPercent(userStatus.ExpProgress01);
    if (!this.isField || !Object.op_Inequality((Object) this.coins, (Object) null) || this.lastMoney == userStatus.Money)
      return;
    this.coins.text = userStatus.Money.ToString();
    this.lastMoney = userStatus.Money;
  }

  private void UpdateUI()
  {
    if (Object.op_Inequality((Object) this.playerName, (Object) null) && !string.IsNullOrEmpty(this.targetPlayer.charaName))
      this.playerName.text = this.targetPlayer.charaName;
    if (Object.op_Inequality((Object) this.playerHp, (Object) null) && this.lastHP != this.targetPlayer.hpShow)
    {
      this.lastHP = this.targetPlayer.hpShow;
      this.playerHp.text = this.targetPlayer.hpShow.ToString();
    }
    if (Object.op_Inequality((Object) this.playerShieldHp, (Object) null) && this.lastShieldHP != (int) this.targetPlayer.ShieldHp)
    {
      this.lastShieldHP = (int) this.targetPlayer.ShieldHp;
      ((Component) this.playerShieldHp).gameObject.SetActive(this.targetPlayer.IsValidShield());
      this.playerShieldHp.text = this.targetPlayer.ShieldHp.ToString();
    }
    if (Object.op_Inequality((Object) this.hpGaugeUI, (Object) null))
    {
      float percent = this.targetPlayer.hpMax > 0 ? (float) this.targetPlayer.hpShow / (float) this.targetPlayer.hpMax : 0.0f;
      if ((double) this.hpGaugeUI.nowPercent != (double) percent)
        this.hpGaugeUI.SetPercent(percent);
    }
    if (Object.op_Inequality((Object) this.healHpGaugeUI, (Object) null))
    {
      float percent = this.targetPlayer.hpMax > 0 ? (float) this.targetPlayer.healHp / (float) this.targetPlayer.hpMax : 0.0f;
      if ((double) this.healHpGaugeUI.nowPercent != (double) percent)
        this.healHpGaugeUI.SetPercent(percent, false);
    }
    if (Object.op_Inequality((Object) this.shieldHpGaugeUI, (Object) null) && Object.op_Inequality((Object) this.shieldHpGaugeADD.sprite, (Object) null))
    {
      float percent = (int) this.targetPlayer.ShieldHpMax > 0 ? (float) (int) this.targetPlayer.ShieldHp / (float) (int) this.targetPlayer.ShieldHpMax : 0.0f;
      ((Component) this.shieldHpGaugeUI).gameObject.SetActive(this.targetPlayer.IsValidShield());
      if (!this.targetPlayer.IsValidShield())
        this.shieldHpGaugeADD.sprite.alpha = 0.1f;
      if ((double) this.shieldHpGaugeUI.nowPercent != (double) percent)
      {
        this.shieldHpGaugeUI.SetPercent(percent, false);
        this.shieldHpGaugeADD.sprite.width = (int) ((double) percent * (double) this.shieldHpGaugeADD.sizeMax + (1.0 - (double) percent) * (double) this.shieldHpGaugeADD.sizeMin);
      }
    }
    this.OnUpdateWeaponIndex();
    bool flag1 = this.targetPlayer.IsValidSpActionMemori();
    if (this.spActionGaugeInfo.gaugeMemoriObj.activeSelf != flag1)
      this.spActionGaugeInfo.gaugeMemoriObj.SetActive(flag1);
    bool isActive = !this.targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) ? this.targetPlayer.IsValidSpActionGauge() : this.targetPlayer.isBoostMode && this.targetPlayer.IsValidSpActionGauge();
    GameObject root = this.spActionGaugeInfo.GetRoot(this.targetPlayer.spAttackType, this.targetPlayer.attackMode);
    this.spActionGaugeInfo.SetActiveRoot(this.targetPlayer.spAttackType, this.targetPlayer.attackMode, isActive);
    if (isActive)
    {
      Renderer renderer = this.spActionGaugeInfo.GetRenderer(this.targetPlayer.spAttackType, this.targetPlayer.attackMode);
      if (Object.op_Inequality((Object) renderer, (Object) null))
      {
        int index = this.targetPlayer.CheckGaugeLevel();
        Color color = index == -1 ? (this.targetPlayer.spAttackType != SP_ATTACK_TYPE.ORACLE ? (this.targetPlayer.spAttackType != SP_ATTACK_TYPE.BURST ? (this.targetPlayer.spAttackType != SP_ATTACK_TYPE.SOUL ? (this.targetPlayer.IsSpActionGaugeHalfCharged() || this.targetPlayer.isBoostMode ? this.spActionGaugeInfo.gaugeColorCharged : this.spActionGaugeInfo.gaugeColorNormal) : (this.targetPlayer.IsSpActionGaugeHalfCharged() || this.targetPlayer.isBoostMode ? this.spActionGaugeInfo.gaugeColorSoul[1] : this.spActionGaugeInfo.gaugeColorSoul[0])) : (this.targetPlayer.IsSpActionGaugeHalfCharged() || this.targetPlayer.isBoostMode ? this.spActionGaugeInfo.gaugeColorBurst[1] : this.spActionGaugeInfo.gaugeColorBurst[0])) : (this.targetPlayer.IsSpActionGaugeHalfCharged() || this.targetPlayer.isBoostMode ? this.spActionGaugeInfo.gaugeColorOracle[1] : this.spActionGaugeInfo.gaugeColorOracle[0])) : (!this.targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) ? this.spActionGaugeInfo.gaugeColorJump[index] : this.spActionGaugeInfo.gaugeColorSoulPairSwords[index]);
        if (Color.op_Inequality(color, renderer.material.color))
          renderer.material.color = color;
      }
      float percent = (double) this.targetPlayer.CurrentWeaponSpActionGaugeMax > 0.0 ? this.targetPlayer.CurrentWeaponSpActionGauge / this.targetPlayer.CurrentWeaponSpActionGaugeMax : 0.0f;
      bool flag2 = false;
      UIHGauge gaugeUi = this.spActionGaugeInfo.GetGaugeUI(this.targetPlayer.spAttackType, this.targetPlayer.attackMode);
      if (Object.op_Inequality((Object) gaugeUi, (Object) null))
      {
        flag2 = (double) gaugeUi.nowPercent != (double) percent;
        if (flag2)
          gaugeUi.SetPercent(percent, false);
      }
      UIPlayerStatus.ShieldHpGaugeEffect actionGaugeAddBoost = this.GetSpActionGaugeADD_BOOST(this.targetPlayer.attackMode, this.targetPlayer.spAttackType);
      UIPlayerStatus.ShieldHpGaugeEffect actionGaugeAddHalf = this.GetSpActionGaugeADD_HALF(this.targetPlayer.attackMode, this.targetPlayer.spAttackType);
      UISprite sprite = actionGaugeAddBoost.sprite;
      if (flag2)
        sprite.width = (int) ((double) percent * (double) actionGaugeAddBoost.sizeMax + (1.0 - (double) percent) * (double) actionGaugeAddBoost.sizeMin);
      if (Object.op_Inequality((Object) root, (Object) null) && root.activeInHierarchy)
      {
        if (this.targetPlayer.IsSpActionGaugeHalfCharged() && !this.spActionGaugeInfo.IsPlayAnim(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE.HALF))
        {
          ((Component) sprite).gameObject.SetActive(false);
          ((Component) actionGaugeAddHalf.sprite).gameObject.SetActive(true);
          UITweenCtrl.Reset(root.transform, 1);
          UITweenCtrl.Play(root.transform, is_input_block: false, tween_ctrl_id: 1);
          this.spActionGaugeInfo.SetState(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE.HALF);
          SoundManager.PlayOneShotUISE(40000358);
        }
        if (this.targetPlayer.IsSpActionGaugeFullCharged() && !this.targetPlayer.isBoostMode && !this.spActionGaugeInfo.IsPlayAnim(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE.FULL))
        {
          ((Component) sprite).gameObject.SetActive(false);
          ((Component) actionGaugeAddHalf.sprite).gameObject.SetActive(true);
          UITweenCtrl.Reset(root.transform, 2);
          UITweenCtrl.Play(root.transform, is_input_block: false, tween_ctrl_id: 2);
          this.spActionGaugeInfo.SetState(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE.FULL);
          SoundManager.PlayOneShotUISE(40000359);
        }
        if (this.targetPlayer.isBoostMode && !this.spActionGaugeInfo.IsPlayAnim(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE.BOOST))
        {
          ((Component) sprite).gameObject.SetActive(true);
          ((Component) actionGaugeAddHalf.sprite).gameObject.SetActive(false);
          UITweenCtrl.Reset(root.transform);
          UITweenCtrl.Play(root.transform, is_input_block: false);
          this.spActionGaugeInfo.SetState(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE.BOOST);
        }
      }
      if (!this.targetPlayer.isBoostMode && this.spActionGaugeInfo.IsPlayAnim(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE.BOOST) || this.spActionGaugeInfo.IsPlayAnim(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE.FULL) && !this.targetPlayer.IsSpActionGaugeFullCharged())
        this.ResetSpActionGaugeState();
      if (this.targetPlayer.isDead && this.spActionGaugeInfo.state != 0)
        this.ResetSpActionGaugeState();
    }
    else if (this.spActionGaugeInfo.state != 0)
      this.ResetSpActionGaugeState();
    this.CheckVisibleBulletUI();
    this.UpdateOracleStock();
    bool flag3 = this.targetPlayer.fishingCtrl.IsFighting();
    bool flag4 = this.targetPlayer.fishingCtrl.IsCooperating();
    this.coopFishingGaugeInfoPortrait.root.SetActive(flag3 | flag4 && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    this.coopFishingGaugeInfoLandscape.root.SetActive(flag3 | flag4 && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    if (flag3 | flag4)
      this.validCoopFishingGaugeInfo = MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? this.coopFishingGaugeInfoPortrait : this.coopFishingGaugeInfoLandscape;
    if (this.validCoopFishingGaugeInfo == null || !Object.op_Inequality((Object) this.validCoopFishingGaugeInfo.root, (Object) null) || !this.validCoopFishingGaugeInfo.root.activeInHierarchy)
      return;
    this.validCoopFishingGaugeInfo.SetRate(this.targetPlayer.fishingCtrl.GetCoopFishingGaugeRate());
    this.validCoopFishingGaugeInfo.SetPositive(this.targetPlayer.fishingCtrl.IsGaugePositive());
  }

  private void OnUpdateWeaponIndex()
  {
    if (Object.op_Equality((Object) this.targetPlayer, (Object) null) || this.preWeaponIndex == this.targetPlayer.weaponIndex && this.preUniqueEquipmentIndex == this.targetPlayer.uniqueEquipmentIndex)
      return;
    this.preWeaponIndex = this.targetPlayer.weaponIndex;
    this.preUniqueEquipmentIndex = this.targetPlayer.uniqueEquipmentIndex;
    this.ResetSpActionGaugeState();
    this.UpdateBurstUIInfo();
  }

  public void SetGaugeEffectColor(Player.ATTACK_MODE mode, SP_ATTACK_TYPE type)
  {
    UIPlayerStatus.ShieldHpGaugeEffect actionGaugeAddBoost = this.GetSpActionGaugeADD_BOOST(mode, type);
    actionGaugeAddBoost.sprite.color = actionGaugeAddBoost.effectColor[(int) type];
  }

  public void ResetSpActionGaugeState()
  {
    UIPlayerStatus.ShieldHpGaugeEffect actionGaugeAddBoost = this.GetSpActionGaugeADD_BOOST(this.targetPlayer.attackMode, this.targetPlayer.spAttackType);
    UIPlayerStatus.ShieldHpGaugeEffect actionGaugeAddHalf = this.GetSpActionGaugeADD_HALF(this.targetPlayer.attackMode, this.targetPlayer.spAttackType);
    ((Component) actionGaugeAddBoost.sprite).gameObject.SetActive(false);
    ((Component) actionGaugeAddHalf.sprite).gameObject.SetActive(false);
    GameObject root = this.spActionGaugeInfo.GetRoot(this.targetPlayer.spAttackType, this.targetPlayer.attackMode);
    UITweenCtrl.Reset(root.transform);
    UITweenCtrl.Reset(root.transform, 1);
    UITweenCtrl.Reset(root.transform, 2);
    this.spActionGaugeInfo.state = 0;
    if (Object.op_Equality((Object) this.targetPlayer, (Object) null))
      return;
    if (this.targetPlayer.IsSpActionGaugeHalfCharged())
    {
      ((Component) actionGaugeAddHalf.sprite).gameObject.SetActive(true);
      if (((Component) actionGaugeAddHalf.sprite).gameObject.activeInHierarchy)
      {
        UITweenCtrl.Play(root.transform, is_input_block: false, tween_ctrl_id: 1);
        this.spActionGaugeInfo.SetState(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE.HALF);
      }
    }
    if (!this.targetPlayer.IsSpActionGaugeFullCharged())
      return;
    this.spActionGaugeInfo.SetState(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE.FULL);
  }

  public void DropInfoUpdate()
  {
    if (!MonoBehaviourSingleton<CoopManager>.IsValid())
      return;
    if (Object.op_Inequality((Object) this.dropInfoR, (Object) null))
      this.dropInfoR.text = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropRare.ToString();
    if (!Object.op_Inequality((Object) this.dropInfoN, (Object) null))
      return;
    this.dropInfoN.text = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropNormal.ToString();
  }

  public void UpDateStatusIcon() => this.statusIcons.UpDateStatusIcon();

  private void EndShowBoost() => this.ChangeShowBoost(USE_ITEM_EFFECT_TYPE.NONE);

  private void ChangeShowBoost(USE_ITEM_EFFECT_TYPE type)
  {
    ((Component) this.boostRate).gameObject.SetActive(type != 0);
    ((Component) this.boostTime).gameObject.SetActive(type != 0);
    int index = 0;
    for (int length = this.boostItems.Length; index < length; ++index)
    {
      bool flag = this.boostItems[index].type == type;
      this.boostItems[index].obj.SetActive(flag);
      if (flag)
      {
        this.panelChange.UnLock();
        this.boostItems[index].anim.Reset();
        this.boostItems[index].anim.Play(onFinished: (EventDelegate.Callback) (() => this.panelChange.Lock()));
      }
    }
  }

  private void UpdateShowBoost(BoostStatus boost)
  {
    switch ((USE_ITEM_EFFECT_TYPE) boost.type)
    {
      case USE_ITEM_EFFECT_TYPE.EXP_UP:
      case USE_ITEM_EFFECT_TYPE.MONEY_UP:
      case USE_ITEM_EFFECT_TYPE.DROP_UP:
      case USE_ITEM_EFFECT_TYPE.EVENT_POINT_UP:
      case USE_ITEM_EFFECT_TYPE.NOVICE_DROP_UP:
      case USE_ITEM_EFFECT_TYPE.HAPPEN_QUEST_UP:
        this.boostRate.text = boost.GetBoostRateText();
        this.boostRate.color = this.boostAnimator.GetRateColor(boost.value);
        this.boostTime.text = boost.type == 210 ? "" : boost.GetRemainTime();
        break;
    }
  }

  public void AddItemNum(Vector3 world_hit_pos, int rarity, bool is_right)
  {
    if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0 && ((Component) this).gameObject.activeInHierarchy)
    {
      this.StartCoroutine(this._AddItemNum(world_hit_pos, rarity, is_right));
    }
    else
    {
      if (rarity > 0)
        ++MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropRare;
      else
        ++MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropNormal;
      MonoBehaviourSingleton<UIPlayerStatus>.I.DropInfoUpdate();
    }
  }

  private IEnumerator _AddItemNum(Vector3 world_hit_pos, int rarity, bool is_right)
  {
    Transform parent;
    Vector3 target;
    if (rarity > 0)
    {
      parent = this.dropIconR;
      target = Vector3.op_Subtraction(((Component) this.dropInfoR).transform.localPosition, this.dropIconR.localPosition);
    }
    else
    {
      parent = this.dropIconN;
      target = Vector3.op_Subtraction(((Component) this.dropInfoN).transform.localPosition, this.dropIconN.localPosition);
    }
    Transform uiEffect = EffectManager.GetUIEffect("ef_ui_downenergy_01", parent);
    if (!Object.op_Equality((Object) uiEffect, (Object) null))
    {
      Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_hit_pos));
      worldPoint.z = 1f;
      uiEffect.position = worldPoint;
      GameObject obj = ((Component) uiEffect).gameObject;
      TransformInterpolator transformInterpolator = obj.AddComponent<TransformInterpolator>();
      if (!Object.op_Equality((Object) transformInterpolator, (Object) null))
      {
        Vector3 add_value;
        // ISSUE: explicit constructor call
        ((Vector3) ref add_value).\u002Ector(is_right ? this.dropEffectAddRandomMax : (float) (-(double) this.dropEffectAddRandomMax * 2.0), this.dropEffectAddRandomMax * 2f, 0.0f);
        transformInterpolator.Translate(this.dropEffectTime, target, this.dropEffectEaseCurve, add_value, this.dropEffectAddCurve);
        yield return (object) new WaitForSeconds(this.dropEffectTime);
        EffectManager.ReleaseEffect(obj);
        if (rarity > 0)
        {
          ++MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropRare;
          SoundManager.PlayOneShotUISE(40000154);
        }
        else
        {
          ++MonoBehaviourSingleton<CoopManager>.I.coopStage.bossDropNormal;
          SoundManager.PlayOneShotUISE(40000153);
        }
        this.DropInfoUpdate();
      }
    }
  }

  public void SetDisableButtons(bool disable)
  {
    if (!Object.op_Inequality((Object) this.weaponChange, (Object) null))
      return;
    this.weaponChange.SetDisableButtons(disable);
  }

  public void DoEnable() => ((Component) this).gameObject.SetActive(true);

  public void DoDisable() => ((Component) this).gameObject.SetActive(false);

  public void SetHGPBoostUpdatePermitFlag(bool permit) => this.permitHGPBoostUpdate = permit;

  public void DirectionSoulGauge(SoulEnergy soulEnergy, Vector3 worldHitPos)
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    this.StartCoroutine(this._DirectionSoulGauge(soulEnergy, worldHitPos));
  }

  private IEnumerator _DirectionSoulGauge(SoulEnergy soulEnergy, Vector3 worldHitPos)
  {
    Transform effectTrans = soulEnergy.GetEffectTrans(this.soulEffectDirection);
    if (effectTrans != null)
    {
      Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(worldHitPos));
      worldPoint.z = 1f;
      effectTrans.position = worldPoint;
      TransformInterpolator transformInterpolator = ((Component) effectTrans).gameObject.GetComponent<TransformInterpolator>() ?? ((Component) effectTrans).gameObject.AddComponent<TransformInterpolator>();
      Vector3 add_value;
      // ISSUE: explicit constructor call
      ((Vector3) ref add_value).\u002Ector(Random.Range(-this.soulEffectAddRandomMax, this.soulEffectAddRandomMax), Random.Range(-this.soulEffectAddRandomMax, this.soulEffectAddRandomMax), 0.0f);
      transformInterpolator.Translate(this.soulEffectTime, Vector3.zero, this.soulEffectEaseCurve, add_value, this.soulEffectAddCurve);
      yield return (object) new WaitForSeconds(this.soulEffectTime);
      soulEnergy.Absorbed();
    }
  }

  public void PlayChangeEvolveIcon(bool start)
  {
    if (this.evolveGauge == null || this.evolveGauge.evolveIcon == null || ((Component) this.evolveGauge.evolveIcon).gameObject.activeSelf == start)
      return;
    if (this.weaponChange != null)
    {
      if (start)
        this.weaponChange.PlayEvolveIconAnim((System.Action) (() => this.EnableEvolveIcon(true)));
      else
        this.weaponChange.PlayEvolveIconAnim((System.Action) (() => this.EnableEvolveIcon(false)));
    }
    else
      this.EnableEvolveIcon(true);
  }

  public void SetEvolveIcon(uint evolveId)
  {
    if (this.evolveGauge == null)
      return;
    this.evolveGauge.SetEvolveIcon(evolveId);
  }

  public void EnableEvolveIcon(bool isEnable)
  {
    if (this.evolveGauge == null)
      return;
    this.evolveGauge.EnableEvolveIcon(isEnable);
  }

  public void SetEvolveRate(float rate)
  {
    if (this.evolveGauge == null)
      return;
    this.evolveGauge.SetRate(rate);
    if ((double) rate < 1.0)
      return;
    SoundManager.PlayOneShotUISE(10000091);
  }

  public void RestrictPopMenu(bool isRestrict)
  {
    if (this.weaponChange == null)
      return;
    this.weaponChange.SetRestrictPopMenu(isRestrict);
  }

  public void SetDisableRalltBtn(bool isDisable) => this.weaponChange.SetDisableRallyBtn(isDisable);

  public bool DoFullBurstAction()
  {
    return !Object.op_Equality((Object) this.m_burstBulletCtrl, (Object) null) && this.m_burstBulletCtrl.FullBurstAction();
  }

  public bool DoShootAction()
  {
    return !Object.op_Equality((Object) this.m_burstBulletCtrl, (Object) null) && this.m_burstBulletCtrl.ConsumeBulletAction();
  }

  public bool DoReloadAction()
  {
    return !Object.op_Equality((Object) this.m_burstBulletCtrl, (Object) null) && this.m_burstBulletCtrl.ReloadAction();
  }

  private void CheckVisibleBulletUI()
  {
    if (Object.op_Equality((Object) this.m_burstBulletCtrl, (Object) null))
      return;
    if (this.targetPlayer.IsValidBurstBulletUI())
      this.m_burstBulletCtrl.SetActivateIconRoot();
    else
      this.m_burstBulletCtrl.SetDeactivateIconRoot();
  }

  public void UpdateBurstUIInfo()
  {
    if (Object.op_Equality((Object) this.m_burstBulletCtrl, (Object) null) || Object.op_Equality((Object) this.targetPlayer, (Object) null) || this.targetPlayer.thsCtrl == null)
      return;
    this.m_burstBulletCtrl.Initialize(new UIBurstBulletUIController.InitParam()
    {
      MaxBulletCount = this.targetPlayer.thsCtrl.CurrentMaxBulletCount,
      CurrentRestBulletCount = this.targetPlayer.thsCtrl.CurrentRestBulletCount
    });
  }

  public static void OnLoadComplete()
  {
    if (!MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIPlayerStatus>.I.UpdateBurstUIInfo();
    MonoBehaviourSingleton<UIPlayerStatus>.I.InitializeOracleStock();
  }

  public void UpdateOracleStock()
  {
    if (this.targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
    {
      this.oracleStock.SetActive(true);
      this.oracleStock.UpdateStock(this.targetPlayer.spearCtrl.StockedCount);
    }
    else
      this.oracleStock.SetActive(false);
  }

  public void InitializeOracleStock()
  {
    if (!MonoBehaviourSingleton<UIPlayerStatus>.IsValid() || !this.targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
      return;
    this.oracleStock.Initialize(this.targetPlayer.spearCtrl.MaxStockCount);
  }

  public void ChangeUniqueEquipment() => this.weaponChange.InitWepIcons();

  public void SetEnableWeaponChangeButton(bool enabled)
  {
    this.weaponChange.SetEnableChangeButton(enabled);
  }

  public bool IsEnableWeaponChangeButton() => this.weaponChange.IsEnableChangeButton();

  [Serializable]
  public class boostItem
  {
    public GameObject obj;
    public UITweenCtrl anim;
    public USE_ITEM_EFFECT_TYPE type;
  }

  [Serializable]
  public class boosColor
  {
    public float rate;
    public Color color;
  }

  [Serializable]
  protected class ShieldHpGaugeEffect
  {
    [SerializeField]
    public UISprite sprite;
    [SerializeField]
    public int sizeMin;
    [SerializeField]
    public int sizeMax;
    [SerializeField]
    public Color[] effectColor;
  }

  [Serializable]
  protected class SpActionGaugeInfo
  {
    [SerializeField]
    public GameObject root;
    [SerializeField]
    public UIHGauge gaugeUI;
    [SerializeField]
    public Renderer renderer;
    [SerializeField]
    public GameObject timerRoot;
    [SerializeField]
    public UIHGauge timerGaugeUI;
    [SerializeField]
    public Renderer timerRenderer;
    [SerializeField]
    public Color gaugeColorNormal;
    [SerializeField]
    public Color gaugeColorCharged;
    [NonSerialized]
    public int state;
    [SerializeField]
    public Color[] gaugeColorJump;
    [SerializeField]
    public Color[] gaugeColorSoul;
    [SerializeField]
    public Color[] gaugeColorSoulPairSwords;
    [SerializeField]
    public Color[] gaugeColorBurst;
    [SerializeField]
    public Color[] gaugeColorOracle;
    [SerializeField]
    public GameObject gaugeMemoriObj;

    public GameObject GetRoot(SP_ATTACK_TYPE spAttackType, Player.ATTACK_MODE mode)
    {
      switch (spAttackType)
      {
        case SP_ATTACK_TYPE.BURST:
          return this.timerRoot;
        case SP_ATTACK_TYPE.ORACLE:
          return mode == Player.ATTACK_MODE.SPEAR ? this.timerRoot : this.root;
        default:
          return this.root;
      }
    }

    public UIHGauge GetGaugeUI(SP_ATTACK_TYPE spAttackType, Player.ATTACK_MODE mode)
    {
      switch (spAttackType)
      {
        case SP_ATTACK_TYPE.BURST:
          return this.timerGaugeUI;
        case SP_ATTACK_TYPE.ORACLE:
          return mode == Player.ATTACK_MODE.SPEAR ? this.timerGaugeUI : this.gaugeUI;
        default:
          return this.gaugeUI;
      }
    }

    public Renderer GetRenderer(SP_ATTACK_TYPE spAttackType, Player.ATTACK_MODE mode)
    {
      switch (spAttackType)
      {
        case SP_ATTACK_TYPE.BURST:
          return this.timerRenderer;
        case SP_ATTACK_TYPE.ORACLE:
          return mode == Player.ATTACK_MODE.SPEAR ? this.timerRenderer : this.renderer;
        default:
          return this.renderer;
      }
    }

    public void SetActiveRoot(SP_ATTACK_TYPE spAttackType, Player.ATTACK_MODE mode, bool isActive)
    {
      GameObject root = this.GetRoot(spAttackType, mode);
      GameObject gameObject = Object.op_Equality((Object) this.root, (Object) root) ? this.timerRoot : this.root;
      if (Object.op_Inequality((Object) root, (Object) null) && root.activeSelf != isActive)
        root.SetActive(isActive);
      if (!Object.op_Inequality((Object) gameObject, (Object) null) || !gameObject.activeSelf)
        return;
      gameObject.SetActive(false);
    }

    public bool IsPlayAnim(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE s)
    {
      return (this.state & 1 << (int) (s & (UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE) 31 /*0x1F*/)) > 0;
    }

    public void SetState(UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE s)
    {
      this.state |= 1 << (int) (s & (UIPlayerStatus.SpActionGaugeInfo.ANIM_STATE) 31 /*0x1F*/);
    }

    public enum ANIM_STATE
    {
      RESET,
      HALF,
      FULL,
      BOOST,
    }
  }

  [Serializable]
  protected class CoopFishingGaugeInfo
  {
    [SerializeField]
    public GameObject root;
    [SerializeField]
    public UISprite gaugeBlue;
    [SerializeField]
    public UISprite gaugeRed;
    [SerializeField]
    public GameObject fishBlue;
    [SerializeField]
    public GameObject fishRed;
    [SerializeField]
    public UISprite fishBlueSprite;
    [SerializeField]
    public UISprite fishRedSprite;
    [SerializeField]
    private Vector3 startPos = Vector3.zero;
    [SerializeField]
    private Vector3 endPos = Vector3.zero;

    public void SetRate(float rate)
    {
      this.gaugeBlue.fillAmount = rate;
      this.gaugeRed.fillAmount = rate;
      this.fishBlue.transform.localPosition = Vector3.Lerp(this.startPos, this.endPos, rate);
      this.fishRed.transform.localPosition = Vector3.Lerp(this.startPos, this.endPos, rate);
      this.fishBlueSprite.MarkAsChanged();
      this.fishRedSprite.MarkAsChanged();
    }

    public void SetPositive(bool isPositive)
    {
      ((Component) this.gaugeBlue).gameObject.SetActive(isPositive);
      ((Component) this.gaugeRed).gameObject.SetActive(!isPositive);
      this.fishBlue.SetActive(isPositive);
      this.fishRed.SetActive(!isPositive);
    }
  }
}
