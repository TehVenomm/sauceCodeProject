// Decompiled with JetBrains decompiler
// Type: AccountRegistrationGoogle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AccountRegistrationGoogle : AccountRegistrationBase
{
  public override void Initialize()
  {
    this.isGoogleAccount = true;
    base.Initialize();
  }

  public override void UpdateUI() => base.UpdateUI();

  private new enum UI
  {
    OBJ_SECRET_QUESTION,
    POP_SECRET_QUESTION,
    LBL_SECRET_QUESTION,
    IPT_ADDRESS,
    POP_ADDRESS,
    IPT_PASSWORD,
    IPT_CONFIRM_PASSWORD,
    IPT_SECRET_ANSER,
    LBL_ADDRESS,
    LBL_PASSWORD,
    LBL_CONFIRM_PASSWORD,
    LBL_SECRET_ANSER,
    BTN_OK,
    BTN_INVALID,
  }
}
