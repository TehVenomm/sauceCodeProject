// Decompiled with JetBrains decompiler
// Type: SoulEnergy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SoulEnergy
{
  private Vector3 kStartScale;
  private Vector3 kJustScale;
  private float kJustTapSec;
  private SoulEnergy.eState state;
  private Player cacheOwner;
  private Transform effectTrans;
  private float counter;
  private float baseValue;
  private bool isJustTap;

  public bool canWork()
  {
    return this.state == SoulEnergy.eState.None || this.state == SoulEnergy.eState.Sleep;
  }

  public void Init()
  {
    InGameSettingsManager.Player.TwoHandSwordActionInfo handSwordActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
    this.kStartScale = new Vector3(handSwordActionInfo.soulSoulEnergyNormalScale, handSwordActionInfo.soulSoulEnergyNormalScale, handSwordActionInfo.soulSoulEnergyNormalScale);
    this.kJustScale = new Vector3(handSwordActionInfo.soulSoulEnergyJustTapScale, handSwordActionInfo.soulSoulEnergyJustTapScale, handSwordActionInfo.soulSoulEnergyJustTapScale);
    this.kJustTapSec = handSwordActionInfo.soulJustTapEnableSec;
  }

  public void Exec(Player owner, float value)
  {
    this.cacheOwner = owner;
    this.baseValue = value;
    this.isJustTap = false;
    this.counter = this.kJustTapSec;
    this.state = SoulEnergy.eState.CanTap;
  }

  public Transform GetEffectTrans(Transform parent)
  {
    if (this.effectTrans == null)
      this.effectTrans = EffectManager.GetUIEffect("ef_btl_soul_energy_01", parent);
    if (this.effectTrans != null)
      this.effectTrans.localScale = this.isJustTap ? this.kJustScale : this.kStartScale;
    else
      this.Absorbed();
    return this.effectTrans;
  }

  public void Tap()
  {
    if (this.state != SoulEnergy.eState.CanTap)
      return;
    if (this.effectTrans != null)
      ((Component) this.effectTrans).transform.localScale = this.kJustScale;
    this.isJustTap = true;
    this.state = SoulEnergy.eState.CannotTap;
  }

  public void Absorbed()
  {
    if (this.canWork())
      return;
    if (this.cacheOwner != null)
      this.cacheOwner.IncreaseSoulGauge(this.baseValue, this.isJustTap);
    this.Sleep();
  }

  public void Sleep()
  {
    if (this.effectTrans != null)
    {
      EffectManager.ReleaseEffect(((Component) this.effectTrans).gameObject);
      this.effectTrans = (Transform) null;
    }
    this.state = SoulEnergy.eState.Sleep;
  }

  private void OnDestroy()
  {
    if (this.effectTrans == null)
      return;
    EffectManager.ReleaseEffect(((Component) this.effectTrans).gameObject);
    this.effectTrans = (Transform) null;
  }

  private void Update()
  {
    if (this.state != SoulEnergy.eState.CanTap)
      return;
    this.counter -= Time.deltaTime;
    if ((double) this.counter > 0.0)
      return;
    this.state = SoulEnergy.eState.CannotTap;
  }

  public enum eState
  {
    None,
    CanTap,
    CannotTap,
    Sleep,
  }
}
