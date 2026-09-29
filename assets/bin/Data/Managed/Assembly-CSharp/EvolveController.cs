// Decompiled with JetBrains decompiler
// Type: EvolveController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EvolveController
{
  private const float kGaugeMax = 1000f;
  private const uint kLeviathanId = 10000;
  private const uint kSphinxId = 10001;
  public const int kEvolveGaugeMaxSeId = 10000091;
  private InGameSettingsManager.Evolve parameter;
  private Player owner;
  private bool isSelf;
  private float[] gauge;
  private uint execEvolveId;
  private Transform execEffect;
  private Transform execEffect2;
  private Transform execEffect3;
  private float execSec;
  private float decreaseValue;
  private InGameSettingsManager.Evolve.TypeAbstract.EvolveBuff[] execBuffs;
  private const string TYPE10000_EXEC_EFFECT = "ef_btl_wex1_spear_01_01";
  private const string TYPE10000_EFFECT = "ef_btl_wex1_spear_01_02";
  private const string TYPE10000_EXRUSH_EFFECT = "ef_btl_wex1_spear_01_03";
  private const string TYPE10001_WING_EFFECT = "ef_btl_ast1_twinsword_01";
  private const string TYPE10001_WEAPON_EFFECT = "ef_btl_ast1_twinsword_02";
  private const string TYPE10001_ACTION_EFFECT = "ef_btl_ast1_twinsword_03";

  public static void Load(LoadingQueue queue, uint evolveId)
  {
    queue.CacheSE(10000091);
    if (evolveId != 10000U)
    {
      if (evolveId != 10001U)
        return;
      queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_ast1_twinsword_01");
      queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_ast1_twinsword_02");
      queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_ast1_twinsword_03");
    }
    else
    {
      queue.CacheSE(MonoBehaviourSingleton<InGameSettingsManager>.I.evolve.type10000.rushSeId);
      queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wex1_spear_01_01");
      queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wex1_spear_01_02");
      queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_wex1_spear_01_03");
    }
  }

  public bool isExec { get; private set; }

  public void Init(Player player)
  {
    this.parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.evolve;
    this.owner = player;
    this.isSelf = player is Self;
    this.gauge = new float[3];
    this.execEvolveId = 0U;
    this.execSec = 0.0f;
    this.decreaseValue = 0.0f;
    this.isExec = false;
  }

  public void SetWeaponInfo()
  {
    if (!this.isSelf)
      return;
    uint evolveWeaponId = this.owner.GetEvolveWeaponId();
    if (evolveWeaponId != 0U)
    {
      MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveIcon(evolveWeaponId);
      MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveIcon(evolveWeaponId);
    }
    MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveRate(this.gauge[this.owner.weaponIndex] / 1000f);
    MonoBehaviourSingleton<UIPlayerStatus>.I.EnableEvolveIcon(this.IsGaugeFull());
    MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveRate(this.gauge[this.owner.weaponIndex] / 1000f);
    MonoBehaviourSingleton<UIEnduranceStatus>.I.EnableEvolveIcon(this.IsGaugeFull());
  }

  public void Start(uint evolveId)
  {
    this.execEvolveId = evolveId;
    this.decreaseValue = 0.0f;
    this.isExec = false;
    this.execBuffs = (InGameSettingsManager.Evolve.TypeAbstract.EvolveBuff[]) null;
    InGameSettingsManager.Evolve.TypeAbstract typeAbstract;
    switch (this.execEvolveId)
    {
      case 10000:
        typeAbstract = this._start10000();
        break;
      case 10001:
        typeAbstract = this._start10001();
        break;
      default:
        return;
    }
    if (typeAbstract.healValue > 0)
      this.owner.ExecHealHp(new Character.HealData(typeAbstract.healValue, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>()
      {
        10
      }));
    if (typeAbstract.healTypes != null)
    {
      for (int index = 0; index < typeAbstract.healTypes.Length; ++index)
        this.owner.DoHealType(typeAbstract.healTypes[index]);
    }
    if (typeAbstract.buffs != null && typeAbstract.buffs.Length != 0)
    {
      this.execBuffs = typeAbstract.buffs;
      if (this.isSelf)
        MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
    }
    this.execSec = typeAbstract.execSec;
    if ((double) this.execSec > 0.0)
      return;
    this.ResetCurrentGauge();
  }

  public void End()
  {
    switch (this.execEvolveId)
    {
      case 10000:
        this._end10000();
        break;
      case 10001:
        this._end10001();
        break;
    }
    this.ReleaseEffect(ref this.execEffect);
    this.ReleaseEffect(ref this.execEffect2);
    this.ReleaseEffect(ref this.execEffect3);
    this.execEvolveId = 0U;
    this.isExec = false;
    this.execBuffs = (InGameSettingsManager.Evolve.TypeAbstract.EvolveBuff[]) null;
    if (!this.isSelf)
      return;
    MonoBehaviourSingleton<UIPlayerStatus>.I.PlayChangeEvolveIcon(false);
    MonoBehaviourSingleton<UIEnduranceStatus>.I.PlayChangeEvolveIcon(false);
    MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
  }

  public bool Execute(ref float rSec)
  {
    rSec = 0.0f;
    if (this.execEvolveId == 0U || this.isExec || (double) this.execSec <= 0.0)
      return false;
    rSec = this.execSec;
    this.isExec = true;
    this.decreaseValue = 1000f / this.execSec;
    return true;
  }

  public bool Update()
  {
    this.DecreaseCurrentGauge(this.decreaseValue * Time.deltaTime);
    return (double) this.GetCurrentGauge() <= 0.0;
  }

  private void ReleaseEffect(ref Transform t, bool isPlayEndAnimation = true)
  {
    if (!MonoBehaviourSingleton<EffectManager>.IsValid() || t == null)
      return;
    EffectManager.ReleaseEffect(((Component) t).gameObject, isPlayEndAnimation);
    t = (Transform) null;
  }

  public bool IsExistSpecialAction() => this.execEvolveId == 10001U;

  public float GetSpecialActionSec()
  {
    return this.execEvolveId == 10001U ? this.parameter.type10001.specialLoopSec : 0.0f;
  }

  public int GetExecBuffValue(BuffParam.BUFFTYPE type)
  {
    if (this.execBuffs == null)
      return 0;
    for (int index = 0; index < this.execBuffs.Length; ++index)
    {
      InGameSettingsManager.Evolve.TypeAbstract.EvolveBuff execBuff = this.execBuffs[index];
      if (execBuff.type == type)
        return execBuff.value;
    }
    return 0;
  }

  public void ResetGauge(bool isAll)
  {
    for (int index = 0; index < 3; ++index)
    {
      if (isAll || index == this.owner.weaponIndex)
        this.gauge[index] = 0.0f;
    }
    if (!this.isSelf)
      return;
    MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveRate(0.0f);
    MonoBehaviourSingleton<UIPlayerStatus>.I.EnableEvolveIcon(false);
    MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveRate(0.0f);
    MonoBehaviourSingleton<UIEnduranceStatus>.I.EnableEvolveIcon(false);
  }

  public void ResetCurrentGauge() => this.ResetGauge(false);

  public void SetGauge(float value, int index)
  {
    if (index < 0 || index >= 3)
      return;
    if ((double) value < 0.0)
      value = 0.0f;
    if ((double) value >= 1000.0)
      value = 1000f;
    this.gauge[index] = value;
    if (!this.isSelf || index != this.owner.weaponIndex)
      return;
    MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveRate(this.gauge[index] / 1000f);
    MonoBehaviourSingleton<UIPlayerStatus>.I.EnableEvolveIcon(this.IsGaugeFull());
    MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveRate(this.gauge[index] / 1000f);
    MonoBehaviourSingleton<UIEnduranceStatus>.I.EnableEvolveIcon(this.IsGaugeFull());
  }

  public void SetCurrentGauge(float value) => this.SetGauge(value, this.owner.weaponIndex);

  public float GetGauge(int index) => index < 0 || index >= 3 ? 0.0f : this.gauge[index];

  public float GetCurrentGauge() => this.GetGauge(this.owner.weaponIndex);

  public void IncreaseGauge(
    AttackHitInfo.ATTACK_TYPE atkType,
    Player.ATTACK_MODE atkMode,
    int index)
  {
    if (this.execEvolveId != 0U)
      return;
    float increaseValue = this._GetIncreaseValue(atkType, atkMode);
    if ((double) increaseValue <= 0.0)
      return;
    float num = this.gauge[index];
    this.gauge[index] += this.owner.CalcWaveMatchSpGauge(increaseValue);
    if ((double) this.gauge[index] > 1000.0)
      this.gauge[index] = 1000f;
    if (!this.isSelf || index != this.owner.weaponIndex || (double) num == (double) this.gauge[index])
      return;
    MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveRate(this.gauge[index] / 1000f);
    MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveRate(this.gauge[index] / 1000f);
    if (!this.IsGaugeFull())
      return;
    MonoBehaviourSingleton<UIPlayerStatus>.I.PlayChangeEvolveIcon(true);
    MonoBehaviourSingleton<UIEnduranceStatus>.I.PlayChangeEvolveIcon(true);
  }

  public void IncreaseCurrentGauge(AttackHitInfo.ATTACK_TYPE atkType, Player.ATTACK_MODE atkMode)
  {
    this.IncreaseGauge(atkType, atkMode, this.owner.weaponIndex);
  }

  public void DecreaseGauge(float value, int index)
  {
    this.gauge[index] -= value;
    if ((double) this.gauge[index] <= 0.0)
      this.gauge[index] = 0.0f;
    if (!this.isSelf || index != this.owner.weaponIndex)
      return;
    MonoBehaviourSingleton<UIPlayerStatus>.I.SetEvolveRate(this.gauge[index] / 1000f);
    MonoBehaviourSingleton<UIEnduranceStatus>.I.SetEvolveRate(this.gauge[index] / 1000f);
  }

  public void DecreaseCurrentGauge(float value)
  {
    this.DecreaseGauge(value, this.owner.weaponIndex);
  }

  public bool IsGaugeFull() => (double) this.gauge[this.owner.weaponIndex] >= 1000.0;

  private float _GetIncreaseValue(AttackHitInfo.ATTACK_TYPE atkType, Player.ATTACK_MODE atkMode)
  {
    EQUIPMENT_TYPE equipmentType = Player.ConvertAttackModeToEquipmentType(atkMode);
    int index = 0;
    for (int length = this.parameter.gaugeInfo.Length; index < length; ++index)
    {
      InGameSettingsManager.Evolve.GaugeInfo gaugeInfo = this.parameter.gaugeInfo[index];
      if (gaugeInfo.type == equipmentType)
        return gaugeInfo.value;
    }
    return 0.0f;
  }

  private InGameSettingsManager.Evolve.TypeAbstract _start10000()
  {
    AppMain.Delay(this.parameter.type10000.execEffectDelay, (System.Action) (() => EffectManager.GetEffect("ef_btl_wex1_spear_01_01", this.owner.FindNode(""))));
    this.ReleaseEffect(ref this.execEffect);
    this.execEffect = EffectManager.GetEffect("ef_btl_wex1_spear_01_02", this.owner.loader.wepR);
    return (InGameSettingsManager.Evolve.TypeAbstract) this.parameter.type10000;
  }

  private void _end10000()
  {
  }

  public bool IsExecLeviathan() => this.execEvolveId == 10000U;

  public float GetLeviathanRushDistanceRate()
  {
    return this.execEvolveId != 10000U ? 0.0f : this.parameter.type10000.rushDistanceRate;
  }

  public void GetLeviathanExRushDamageRate(out float oMin, out float oMax, out float oFull)
  {
    oMin = this.parameter.type10000.damageRateMin;
    oMax = this.parameter.type10000.damageRateMax;
    oFull = this.parameter.type10000.damageRateFull;
  }

  public void PlayLeviathanEffect()
  {
    if (this.execEvolveId != 10000U)
      return;
    SoundManager.PlayOneShotSE(this.parameter.type10000.rushSeId, (DisableNotifyMonoBehaviour) this.owner, this.owner.FindNode(""));
    EffectManager.GetEffect("ef_btl_wex1_spear_01_03", this.owner.FindNode("Move")).localPosition = new Vector3(0.0f, 1f, 0.0f);
  }

  private InGameSettingsManager.Evolve.TypeAbstract _start10001()
  {
    this.ReleaseEffect(ref this.execEffect);
    this.execEffect = EffectManager.GetEffect("ef_btl_ast1_twinsword_02", this.owner.loader.wepR);
    this.ReleaseEffect(ref this.execEffect2);
    this.execEffect2 = EffectManager.GetEffect("ef_btl_ast1_twinsword_02", this.owner.loader.wepL);
    this.ReleaseEffect(ref this.execEffect3);
    this.execEffect3 = EffectManager.GetEffect("ef_btl_ast1_twinsword_01", this.owner.FindNode("Spine01"));
    this.execEffect3.localRotation = Quaternion.Euler(new Vector3(90f, -90f, 0.0f));
    return (InGameSettingsManager.Evolve.TypeAbstract) this.parameter.type10001;
  }

  private void _end10001()
  {
  }

  public bool IsExecSphinx() => this.execEvolveId == 10001U;

  public float GetSphinxRangeUpRate()
  {
    return this.execEvolveId != 10001U ? 1f : this.parameter.type10001.rangeUp;
  }

  public float GetSphinxElementDamageUpRate()
  {
    return this.execEvolveId != 10001U ? 1f : this.parameter.type10001.elementDamageRate;
  }

  public void PlaySphinxActionEffect()
  {
    if (this.execEvolveId != 10001U)
      return;
    AppMain.Delay(1f, (System.Action) (() => EffectManager.GetEffect("ef_btl_ast1_twinsword_03", this.owner.FindNode("Move")).localPosition = new Vector3(0.0f, 3.7f, 1.5f)));
  }
}
