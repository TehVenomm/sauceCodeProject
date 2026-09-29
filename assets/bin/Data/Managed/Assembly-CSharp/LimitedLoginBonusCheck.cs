// Decompiled with JetBrains decompiler
// Type: LimitedLoginBonusCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections;

#nullable disable
public class LimitedLoginBonusCheck : GameSection
{
  public override void Initialize()
  {
    base.Initialize();
    this.StartCoroutine("DoCheck");
  }

  private IEnumerator DoCheck()
  {
    while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      yield return (object) null;
    this.CheckNextLoginBonus();
  }

  private void CheckNextLoginBonus()
  {
    if (MonoBehaviourSingleton<AccountManager>.I.logInBonus == null)
      GameSection.BackSection();
    else if (MonoBehaviourSingleton<AccountManager>.I.logInBonus.Count == 0)
    {
      GameSection.BackSection();
    }
    else
    {
      LoginBonus logInBonu = MonoBehaviourSingleton<AccountManager>.I.logInBonus[0];
      if (logInBonu.priority > 0)
        this.DispatchEvent("LIMITED_LOGIN_BONUS");
      else if (logInBonu.type == 0)
        this.DispatchEvent("LOGIN_BONUS");
      else
        GameSection.BackSection();
    }
  }

  public void OnCloseDialog(string section_name) => this.StartCoroutine("DoCheck");
}
