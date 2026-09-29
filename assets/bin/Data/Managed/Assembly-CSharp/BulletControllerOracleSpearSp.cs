// Decompiled with JetBrains decompiler
// Type: BulletControllerOracleSpearSp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerOracleSpearSp : BulletControllerBase
{
  private BulletData.BulletOracleSpearSp data;
  private string chargedEffect;
  private Vector3 basePos = Vector3.zero;
  private Quaternion baseRot = Quaternion.identity;

  public bool Charged { get; protected set; }

  public override void Update()
  {
    base.Update();
    Player fromObject = this.fromObject as Player;
    if (!Object.op_Inequality((Object) fromObject, (Object) null) || !fromObject.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
      return;
    this.UpdateBulletTransform();
    this.UpdateEffectScale(Mathf.Min(1f, Mathf.Max(0.0f, fromObject.GetChargingRate())));
  }

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, skillInfoParam, pos, rot);
    this.data = bullet.dataOracleSpearSp;
  }

  public override void RegisterFromObject(StageObject obj)
  {
    base.RegisterFromObject(obj);
    this.chargedEffect = this.data.GetEffectName(obj as Player);
    ((Component) this.bulletObject).transform.SetParent(((Component) obj).transform);
    this.basePos = this.bulletObject._transform.localPosition;
    this.baseRot = this.bulletObject._transform.localRotation;
  }

  private void UpdateBulletTransform()
  {
    this.bulletObject._transform.localPosition = this.basePos;
    this.bulletObject._transform.localRotation = this.baseRot;
  }

  public void UpdateChargedEffect()
  {
    this.Charged = true;
    EffectManager.ReleaseEffect(((Component) this.bulletObject.bulletEffect).gameObject, false, true);
    this.bulletObject.bulletEffect = EffectManager.GetEffect(this.chargedEffect, ((Component) this.bulletObject).transform);
    SoundManager.PlayUISE(this.data.chargedSEId);
  }

  private void UpdateEffectScale(float rate)
  {
    if (this.Charged)
      return;
    this.bulletObject.bulletEffect.localScale = Vector3.op_Multiply(Vector3.one, Mathf.Lerp(1f, 1.2f, rate));
  }
}
