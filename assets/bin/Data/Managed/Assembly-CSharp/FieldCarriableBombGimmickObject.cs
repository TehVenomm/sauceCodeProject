// Decompiled with JetBrains decompiler
// Type: FieldCarriableBombGimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FieldCarriableBombGimmickObject : FieldCarriableGimmickObject
{
  public static readonly string kPutEffectName = "ef_btl_trap_01_02";
  public static readonly string kFuseEffectNameFormat = "ef_btl_trap_05_{0:D2}";
  public static readonly string kFuseEffectStateLoop = "LOOP";
  public static readonly string kFuseEffectStateEnd = "END";
  public static readonly int kPutSEId = 10000058;
  public static readonly float kTimerLimit = 5f;
  private static readonly string kDefaultAttackInfoName = "field_bomb";
  public static readonly int kBombSEId = 10000159;
  protected float timer;
  protected string attackInfoName = FieldCarriableBombGimmickObject.kDefaultAttackInfoName;
  protected Player ownerPlayer;
  protected EffectCtrl fuseEffectCtrl;

  protected override void ParseParam(string value2)
  {
    base.ParseParam(value2);
    if (value2.IsNullOrWhiteSpace())
      return;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "ai")
        this.attackInfoName = strArray[1];
    }
  }

  protected override void OnStartCarry(Player owner)
  {
    base.OnStartCarry(owner);
    this.ownerPlayer = owner;
    if (Object.op_Equality((Object) this.fuseEffectCtrl, (Object) null))
      this.fuseEffectCtrl = ((Component) EffectManager.GetEffect(FieldCarriableBombGimmickObject.GetFuseEffectNameByModelIndex(this.modelIndex), this.GetTransform())).GetComponent<EffectCtrl>();
    else
      this.fuseEffectCtrl.Play(FieldCarriableBombGimmickObject.kFuseEffectStateLoop);
  }

  protected override void OnEndCarry()
  {
    base.OnEndCarry();
    this.timer = 0.0f;
    EffectManager.OneShot(FieldCarriableBombGimmickObject.kPutEffectName, this.GetTransform().position, this.GetTransform().rotation);
    SoundManager.PlayOneShotSE(FieldCarriableBombGimmickObject.kPutSEId, this.GetTransform().position);
    if (!Object.op_Inequality((Object) this.fuseEffectCtrl, (Object) null))
      return;
    this.fuseEffectCtrl.Play(FieldCarriableBombGimmickObject.kFuseEffectStateEnd);
  }

  public override void RequestDestroy()
  {
    if (Object.op_Inequality((Object) this.fuseEffectCtrl, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.fuseEffectCtrl).gameObject);
      this.fuseEffectCtrl = (EffectCtrl) null;
    }
    base.RequestDestroy();
  }

  private void Update()
  {
    if (!this.isCarrying && this.hasDeploied)
      this.timer += Time.deltaTime;
    if ((double) this.timer < (double) FieldCarriableBombGimmickObject.kTimerLimit)
      return;
    this.Explosion();
  }

  protected void Explosion()
  {
    if (!((Component) this).gameObject.activeSelf || Object.op_Equality((Object) this.ownerPlayer, (Object) null))
      return;
    AttackInfo attackInfo = this.ownerPlayer.FindAttackInfo(this.attackInfoName);
    if (attackInfo != null)
    {
      SoundManager.PlayOneShotSE(FieldCarriableBombGimmickObject.kBombSEId);
      AnimEventShot.Create((StageObject) this.ownerPlayer, attackInfo, this.GetTransform().position, this.GetTransform().rotation);
    }
    this.RequestDestroy();
  }

  public static string GetAttackInfoName(string value2)
  {
    if (value2.IsNullOrWhiteSpace())
      return FieldCarriableBombGimmickObject.kDefaultAttackInfoName;
    string str1 = value2;
    char[] chArray1 = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ ':' };
      string[] strArray = str2.Split(chArray2);
      if (strArray != null && strArray.Length == 2 && strArray[0] == "ai")
        return strArray[1];
    }
    return FieldCarriableBombGimmickObject.kDefaultAttackInfoName;
  }

  public static string GetFuseEffectNameByModelIndex(int index)
  {
    return string.Format(FieldCarriableBombGimmickObject.kFuseEffectNameFormat, (object) (index + 1));
  }
}
