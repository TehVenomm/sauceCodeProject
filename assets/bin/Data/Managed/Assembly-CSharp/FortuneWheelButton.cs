// Decompiled with JetBrains decompiler
// Type: FortuneWheelButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class FortuneWheelButton : UIBehaviour
{
  protected override void OnOpen()
  {
    this.PlayTween((Enum) FortuneWheelButton.UI.OBJ_TWEEN, is_input_block: false);
    base.OnOpen();
  }

  public void Show(bool isShow)
  {
    if (isShow)
      this.Open();
    else
      this.Close();
  }

  private enum UI
  {
    OBJ_TWEEN,
    TIME_COUNTDOWN_TXT,
    SPR_NOTE_UPDATE,
  }
}
