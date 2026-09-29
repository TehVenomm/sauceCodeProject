// Decompiled with JetBrains decompiler
// Type: QuestInvitationButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class QuestInvitationButton : UIBehaviour
{
  protected override void OnOpen()
  {
    this.PlayTween((Enum) QuestInvitationButton.UI.OBJ_TWEEN, is_input_block: false);
    if (MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.isOpen)
      MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.OnInvitationBtnOpen(true);
    base.OnOpen();
  }

  protected override void OnClose()
  {
    if (MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.isOpen)
      MonoBehaviourSingleton<UIManager>.I.blackMarkeButton.OnInvitationBtnOpen(false);
    base.OnClose();
  }

  private enum UI
  {
    OBJ_TWEEN,
  }
}
