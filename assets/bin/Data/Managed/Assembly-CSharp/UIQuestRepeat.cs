// Decompiled with JetBrains decompiler
// Type: UIQuestRepeat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UIQuestRepeat : MonoBehaviourSingleton<UIQuestRepeat>
{
  [SerializeField]
  protected UILabel repeatStatus;
  [SerializeField]
  protected UIButton repeatOffBtn;

  public void OnVictory()
  {
    ((Component) this.repeatStatus).gameObject.SetActive(false);
    ((Component) this.repeatOffBtn).gameObject.SetActive(false);
  }

  public void InitData()
  {
    if (!PartyManager.IsValidInParty() || MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id != MonoBehaviourSingleton<PartyManager>.I.GetOwnerUserId())
      return;
    ((Component) this.repeatStatus).gameObject.SetActive(MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
    ((Component) this.repeatOffBtn).gameObject.SetActive(MonoBehaviourSingleton<PartyManager>.I.is_repeat_quest);
  }

  public void OnEndHunt()
  {
    this.repeatOffBtn.SetState(UIButtonColor.State.Disabled, true);
    MonoBehaviourSingleton<PartyManager>.I.SendRepeat(false, (Action<bool>) (is_success =>
    {
      if (is_success)
      {
        ((Component) this.repeatStatus).gameObject.SetActive(false);
        ((Component) this.repeatOffBtn).gameObject.SetActive(false);
        GameSaveData.instance.defaultRepeatPartyOn = false;
      }
      else
        this.repeatOffBtn.SetState(UIButtonColor.State.Normal, true);
    }));
  }
}
