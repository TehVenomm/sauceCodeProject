// Decompiled with JetBrains decompiler
// Type: HomeLoginBonusNoticeBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Text;

#nullable disable
public class HomeLoginBonusNoticeBase : GameSection
{
  private LoginBonus bonus;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    this.bonus = MonoBehaviourSingleton<AccountManager>.I.logInBonus[0];
    MonoBehaviourSingleton<AccountManager>.I.logInBonus.Remove(this.bonus);
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (this.bonus == null)
      return;
    this.SetLabelText((Enum) HomeLoginBonusNoticeBase.UI.LBL_BONUS_NAME, this.bonus.name);
    StringBuilder sb = new StringBuilder();
    int count = this.bonus.reward.Count;
    int index = 0;
    this.bonus.reward.ForEach((Action<LoginBonus.LoginBonusReward>) (o =>
    {
      ++index;
      if (index < count)
        sb.AppendLine(o.name);
      else
        sb.Append(o.name);
    }));
    this.SetLabelText((Enum) HomeLoginBonusNoticeBase.UI.ProvisionalLabel, sb.ToString());
    this.UpdateAnchors();
  }

  private void OnQuery_CLOSE() => GameSection.BackSection();

  private enum UI
  {
    LBL_BONUS_NAME,
    ProvisionalLabel,
  }
}
