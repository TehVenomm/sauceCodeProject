// Decompiled with JetBrains decompiler
// Type: CheckTutorialBit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class CheckTutorialBit : MonoBehaviour
{
  public string TutorialBitString;

  private void Start()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady || MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit((TUTORIAL_MENU_BIT) Enum.Parse(typeof (TUTORIAL_MENU_BIT), this.TutorialBitString)))
      return;
    ((Component) this).gameObject.SetActive(false);
  }
}
