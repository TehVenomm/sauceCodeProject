// Decompiled with JetBrains decompiler
// Type: StatusExchangeEntrance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class StatusExchangeEntrance : GameSection
{
  public override void Initialize() => base.Initialize();

  protected override void OnOpen()
  {
    MonoBehaviourSingleton<ItemExchangeManager>.I.SetExchangeType(EXCHANGE_TYPE.NONE);
    base.OnOpen();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) StatusExchangeEntrance.UI.LBL_TITLE_U, this.sectionData.GetText("WINDOW_TITLE"));
    this.SetLabelText((Enum) StatusExchangeEntrance.UI.LBL_TITLE_D, this.sectionData.GetText("WINDOW_TITLE"));
    this.SetLabelText((Enum) StatusExchangeEntrance.UI.LBL_HAVE_RARE, 0.ToString("N0"));
    this.SetLabelText((Enum) StatusExchangeEntrance.UI.LBL_HAVE_NORMAL, 0.ToString("N0"));
    this.SetLabelText((Enum) StatusExchangeEntrance.UI.LBL_HAVE_GOLD, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money.ToString("N0"));
    base.UpdateUI();
  }

  private void OnQuery_RARE()
  {
    MonoBehaviourSingleton<ItemExchangeManager>.I.SetExchangeType(EXCHANGE_TYPE.RARE);
    GameSection.ChangeEvent("EXCHANGE");
  }

  private void OnQuery_NORMAL()
  {
    MonoBehaviourSingleton<ItemExchangeManager>.I.SetExchangeType(EXCHANGE_TYPE.NORMAL);
    GameSection.ChangeEvent("EXCHANGE");
  }

  private void OnQuery_SELL()
  {
    MonoBehaviourSingleton<ItemExchangeManager>.I.SetExchangeType(EXCHANGE_TYPE.SELL);
    GameSection.ChangeEvent("EXCHANGE");
  }

  public enum UI
  {
    LBL_TITLE_U,
    LBL_TITLE_D,
    LBL_HAVE_RARE,
    LBL_HAVE_NORMAL,
    LBL_HAVE_GOLD,
  }
}
