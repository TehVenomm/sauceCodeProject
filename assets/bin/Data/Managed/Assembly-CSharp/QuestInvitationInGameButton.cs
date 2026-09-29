// Decompiled with JetBrains decompiler
// Type: QuestInvitationInGameButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestInvitationInGameButton : UIBehaviour
{
  protected override void OnOpen()
  {
    this.PlayTween((Enum) QuestInvitationInGameButton.UI.OBJ_TWEEN, is_input_block: false);
    base.OnOpen();
  }

  public void SetDisableButton(bool flag)
  {
    UIButton componentInChildren = ((Component) this).GetComponentInChildren<UIButton>();
    if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
      return;
    componentInChildren.isEnabled = !flag;
  }

  private enum UI
  {
    OBJ_TWEEN,
  }
}
