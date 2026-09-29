// Decompiled with JetBrains decompiler
// Type: UIEnduranceStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class UIEnduranceStatus : MonoBehaviourSingleton<UIEnduranceStatus>
{
  [SerializeField]
  protected UILabel enduranceName;
  [SerializeField]
  protected UILabel enduranceHp;
  [SerializeField]
  protected UIHGauge hpGaugeUI;
  [SerializeField]
  protected UIWeaponChange weaponChange;
  [SerializeField]
  protected UIEvolveGauge evolveGauge;
  [SerializeField]
  protected UIEnduranceStatus.SpActionGaugeInfo spActionGaugeInfo;
  [SerializeField]
  protected UIEnduranceStatus.ShieldHpGaugeEffect spActionGaugeADD_BOOST;
  [SerializeField]
  protected UIEnduranceStatus.ShieldHpGaugeEffect spActionGaugeADD_HALF;
  [SerializeField]
  protected StatusBoostAnimator boostAnimator;
  [SerializeField]
  protected UIEnduranceStatus.boostItem[] boostItems;
  [SerializeField]
  protected UILabel boostRate;
  [SerializeField]
  protected UILabel boostTime;
  [SerializeField]
  protected UIStaticPanelChanger panelChange;
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
  [SerializeField]
  private UIEnduranceStatus.CannonSpecialGaugeInfo cannonSpecialGaugePortrait;
  [SerializeField]
  private UIEnduranceStatus.CannonSpecialGaugeInfo cannonSpecialGaugeLandscape;
  private UIEnduranceStatus.CannonSpecialGaugeInfo validCannonSpecialGaugeInfo;
  private bool permitHGPBoostUpdate = true;
  private int preWeaponIndex;
  private int preUniqueEquipmentIndex;
  private UIBurstBulletUIController m_burstBulletCtrl;

  public Player targetPlayer { get; protected set; }

  public bool PermitHGPBoostUpdate => this.permitHGPBoostUpdate;

  protected override void Awake()
  {
    base.Awake();
    this.boostRate.fontStyle = (FontStyle) 2;
    this.boostTime.fontStyle = (FontStyle) 2;
    ((Component) this).gameObject.SetActive(false);
    this.CreateBurstBulletUI();
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
      if (Object.op_Inequality((Object) this.weaponChange, (Object) null))
        this.weaponChange.SetTarget(this.targetPlayer);
      this.UpdateUI();
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
  }

  private void UpdateUI()
  {
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && !string.IsNullOrEmpty(MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.enduranceObjectName))
      this.enduranceName.text = MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.enduranceObjectName;
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      this.enduranceHp.text = MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEndurance.ToString();
    if (Object.op_Inequality((Object) this.hpGaugeUI, (Object) null))
      this.hpGaugeUI.SetPercent((double) MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEnduranceMax > 0.0 ? MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEndurance / MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEnduranceMax : 0.0f);
    this.OnUpdateWeaponIndex();
    bool flag1 = this.targetPlayer.IsValidSpActionMemori();
    if (this.spActionGaugeInfo.gaugeMemoriObj.activeSelf != flag1)
      this.spActionGaugeInfo.gaugeMemoriObj.SetActive(flag1);
    bool flag2 = this.targetPlayer.IsValidSpActionGauge();
    if (Object.op_Inequality((Object) this.spActionGaugeInfo.root, (Object) null) && this.spActionGaugeInfo.root.activeSelf != flag2)
      this.spActionGaugeInfo.root.SetActive(flag2);
    if (flag2)
    {
      if (Object.op_Inequality((Object) this.spActionGaugeInfo.renderer, (Object) null))
      {
        int index = this.targetPlayer.CheckGaugeLevel();
        Color color = index == -1 ? (this.targetPlayer.spAttackType != SP_ATTACK_TYPE.SOUL ? (this.targetPlayer.IsSpActionGaugeHalfCharged() || this.targetPlayer.isBoostMode ? this.spActionGaugeInfo.gaugeColorCharged : this.spActionGaugeInfo.gaugeColorNormal) : (this.targetPlayer.IsSpActionGaugeHalfCharged() || this.targetPlayer.isBoostMode ? this.spActionGaugeInfo.gaugeColorSoul[1] : this.spActionGaugeInfo.gaugeColorSoul[0])) : (!this.targetPlayer.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) ? this.spActionGaugeInfo.gaugeColorJump[index] : this.spActionGaugeInfo.gaugeColorSoulPairSwords[index]);
        if (Color.op_Inequality(color, this.spActionGaugeInfo.renderer.material.color))
          this.spActionGaugeInfo.renderer.material.color = color;
      }
      float percent = (double) this.targetPlayer.CurrentWeaponSpActionGaugeMax > 0.0 ? this.targetPlayer.CurrentWeaponSpActionGauge / this.targetPlayer.CurrentWeaponSpActionGaugeMax : 0.0f;
      bool flag3 = false;
      if (Object.op_Inequality((Object) this.spActionGaugeInfo.gaugeUI, (Object) null))
      {
        flag3 = (double) this.spActionGaugeInfo.gaugeUI.nowPercent != (double) percent;
        if (flag3)
          this.spActionGaugeInfo.gaugeUI.SetPercent(percent, false);
      }
      UISprite sprite = this.spActionGaugeADD_BOOST.sprite;
      if (flag3)
        sprite.width = (int) ((double) percent * (double) this.spActionGaugeADD_BOOST.sizeMax + (1.0 - (double) percent) * (double) this.spActionGaugeADD_BOOST.sizeMin);
      if (Object.op_Inequality((Object) this.spActionGaugeInfo.root, (Object) null) && this.spActionGaugeInfo.root.activeInHierarchy)
      {
        if (this.targetPlayer.IsSpActionGaugeHalfCharged() && !this.spActionGaugeInfo.IsPlayAnim(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE.HALF))
        {
          ((Component) sprite).gameObject.SetActive(false);
          ((Component) this.spActionGaugeADD_HALF.sprite).gameObject.SetActive(true);
          UITweenCtrl.Reset(this.spActionGaugeInfo.root.transform, 1);
          UITweenCtrl.Play(this.spActionGaugeInfo.root.transform, is_input_block: false, tween_ctrl_id: 1);
          this.spActionGaugeInfo.SetState(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE.HALF);
          SoundManager.PlayOneShotUISE(40000358);
        }
        if (this.targetPlayer.IsSpActionGaugeFullCharged() && !this.spActionGaugeInfo.IsPlayAnim(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE.FULL))
        {
          ((Component) sprite).gameObject.SetActive(false);
          ((Component) this.spActionGaugeADD_HALF.sprite).gameObject.SetActive(true);
          UITweenCtrl.Reset(this.spActionGaugeInfo.root.transform, 2);
          UITweenCtrl.Play(this.spActionGaugeInfo.root.transform, is_input_block: false, tween_ctrl_id: 2);
          this.spActionGaugeInfo.SetState(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE.FULL);
          SoundManager.PlayOneShotUISE(40000359);
        }
        if (this.targetPlayer.isBoostMode && !this.spActionGaugeInfo.IsPlayAnim(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE.BOOST))
        {
          ((Component) sprite).gameObject.SetActive(true);
          ((Component) this.spActionGaugeADD_HALF.sprite).gameObject.SetActive(false);
          UITweenCtrl.Reset(this.spActionGaugeInfo.root.transform);
          UITweenCtrl.Play(this.spActionGaugeInfo.root.transform, is_input_block: false);
          this.spActionGaugeInfo.SetState(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE.BOOST);
        }
      }
      if (!this.targetPlayer.isBoostMode && this.spActionGaugeInfo.IsPlayAnim(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE.BOOST) || this.spActionGaugeInfo.IsPlayAnim(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE.FULL) && !this.targetPlayer.IsSpActionGaugeFullCharged())
        this.ResetSpActionGaugeState();
      if (this.targetPlayer.isDead && this.spActionGaugeInfo.state != 0)
        this.ResetSpActionGaugeState();
    }
    else if (this.spActionGaugeInfo.state != 0)
      this.ResetSpActionGaugeState();
    bool flag4 = this.targetPlayer.IsOnCannonMode();
    bool flag5 = this.targetPlayer.targetFieldGimmickCannon is FieldGimmickCannonSpecial;
    bool flag6 = MonoBehaviourSingleton<InGameCameraManager>.IsValid() && MonoBehaviourSingleton<InGameCameraManager>.I.IsCameraModeBeam();
    this.cannonSpecialGaugePortrait.root.SetActive(flag4 & flag5 && MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait && !flag6);
    this.cannonSpecialGaugeLandscape.root.SetActive(flag4 & flag5 && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait && !flag6);
    if (flag4 & flag5)
      this.validCannonSpecialGaugeInfo = !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? this.cannonSpecialGaugeLandscape : this.cannonSpecialGaugePortrait;
    if (this.validCannonSpecialGaugeInfo == null)
      return;
    float percent1 = (double) this.targetPlayer.GetCannonChargeMax() > 0.0 ? this.targetPlayer.GetCannonChargeRate() : 0.0f;
    if (Object.op_Inequality((Object) this.validCannonSpecialGaugeInfo.root, (Object) null) && this.validCannonSpecialGaugeInfo.root.activeInHierarchy && Object.op_Inequality((Object) this.validCannonSpecialGaugeInfo.gaugeUI, (Object) null) && (double) this.validCannonSpecialGaugeInfo.gaugeUI.nowPercent != (double) percent1)
      this.validCannonSpecialGaugeInfo.gaugeUI.SetPercent(percent1, false);
    this.validCannonSpecialGaugeInfo.gaugeNormalObj.SetActive(!this.targetPlayer.IsCannonFullCharged());
    this.validCannonSpecialGaugeInfo.gaugeChargedObj.SetActive(this.targetPlayer.IsCannonFullCharged());
    this.validCannonSpecialGaugeInfo.gaugeAddObj.SetActive(this.targetPlayer.IsCannonFullCharged());
    this.validCannonSpecialGaugeInfo.fullTipObj.SetActive(this.targetPlayer.IsCannonFullCharged());
    if (this.targetPlayer.IsCannonFullCharged())
    {
      if (this.validCannonSpecialGaugeInfo.IsPlayAnim(UIEnduranceStatus.CannonSpecialGaugeInfo.ANIM_STATE.FULL))
        return;
      UITweenCtrl.Reset(this.validCannonSpecialGaugeInfo.gaugeAddObj.transform, 3);
      UITweenCtrl.Play(this.validCannonSpecialGaugeInfo.gaugeAddObj.transform, is_input_block: false, tween_ctrl_id: 3);
      this.validCannonSpecialGaugeInfo.SetState(UIEnduranceStatus.CannonSpecialGaugeInfo.ANIM_STATE.FULL);
    }
    else
    {
      UITweenCtrl.Reset(this.validCannonSpecialGaugeInfo.gaugeAddObj.transform, 3);
      this.validCannonSpecialGaugeInfo.state = 0;
    }
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

  public void SetGaugeEffectColor(SP_ATTACK_TYPE type)
  {
    this.spActionGaugeADD_BOOST.sprite.color = this.spActionGaugeADD_BOOST.effectColor[(int) type];
  }

  public void ResetSpActionGaugeState()
  {
    ((Component) this.spActionGaugeADD_BOOST.sprite).gameObject.SetActive(false);
    ((Component) this.spActionGaugeADD_HALF.sprite).gameObject.SetActive(false);
    UITweenCtrl.Reset(this.spActionGaugeInfo.root.transform);
    UITweenCtrl.Reset(this.spActionGaugeInfo.root.transform, 1);
    UITweenCtrl.Reset(this.spActionGaugeInfo.root.transform, 2);
    this.spActionGaugeInfo.state = 0;
    if (Object.op_Equality((Object) this.targetPlayer, (Object) null))
      return;
    if (this.targetPlayer.IsSpActionGaugeHalfCharged())
    {
      ((Component) this.spActionGaugeADD_HALF.sprite).gameObject.SetActive(true);
      if (((Component) this.spActionGaugeADD_HALF.sprite).gameObject.activeInHierarchy)
      {
        UITweenCtrl.Play(this.spActionGaugeInfo.root.transform, is_input_block: false, tween_ctrl_id: 1);
        this.spActionGaugeInfo.SetState(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE.HALF);
      }
    }
    if (!this.targetPlayer.IsSpActionGaugeFullCharged())
      return;
    this.spActionGaugeInfo.SetState(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE.FULL);
  }

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

  public void CheckVisibleBulletUI()
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
    public Color gaugeColorNormal;
    [SerializeField]
    public Color gaugeColorCharged;
    [SerializeField]
    public UIHGauge gaugeUI;
    [SerializeField]
    public Renderer renderer;
    [NonSerialized]
    public int state;
    [SerializeField]
    public Color[] gaugeColorJump;
    [SerializeField]
    public Color[] gaugeColorSoul;
    [SerializeField]
    public Color[] gaugeColorSoulPairSwords;
    [SerializeField]
    public GameObject gaugeMemoriObj;

    public bool IsPlayAnim(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE s)
    {
      return (this.state & 1 << (int) (s & (UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE) 31 /*0x1F*/)) > 0;
    }

    public void SetState(UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE s)
    {
      this.state |= 1 << (int) (s & (UIEnduranceStatus.SpActionGaugeInfo.ANIM_STATE) 31 /*0x1F*/);
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
  public class CannonSpecialGaugeInfo
  {
    [SerializeField]
    public GameObject root;
    [SerializeField]
    public GameObject gaugeAddObj;
    [SerializeField]
    public GameObject fullTipObj;
    [SerializeField]
    public GameObject gaugeNormalObj;
    [SerializeField]
    public GameObject gaugeChargedObj;
    [SerializeField]
    public UIHGauge gaugeUI;
    [SerializeField]
    public Color gaugeColorNormal;
    [SerializeField]
    public Color gaugeColorCharged;
    [SerializeField]
    public Renderer renderer;
    [NonSerialized]
    public int state;

    public bool IsPlayAnim(
      UIEnduranceStatus.CannonSpecialGaugeInfo.ANIM_STATE s)
    {
      return (this.state & 1 << (int) (s & (UIEnduranceStatus.CannonSpecialGaugeInfo.ANIM_STATE) 31 /*0x1F*/)) > 0;
    }

    public void SetState(
      UIEnduranceStatus.CannonSpecialGaugeInfo.ANIM_STATE s)
    {
      this.state |= 1 << (int) (s & (UIEnduranceStatus.CannonSpecialGaugeInfo.ANIM_STATE) 31 /*0x1F*/);
    }

    public enum ANIM_STATE
    {
      RESET,
      FULL,
    }
  }
}
