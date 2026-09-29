// Decompiled with JetBrains decompiler
// Type: UIButtonEventTransmitter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIButtonEventTransmitter : MonoBehaviour
{
  public GameObject transmit_target;

  private void OnPress(bool isPressed)
  {
    if (!Object.op_Inequality((Object) this.transmit_target, (Object) null))
      return;
    this.transmit_target.SendMessage(nameof (OnPress), (object) isPressed, (SendMessageOptions) 1);
  }

  private void OnHover(bool isOver)
  {
    if (!Object.op_Inequality((Object) this.transmit_target, (Object) null))
      return;
    this.transmit_target.SendMessage(nameof (OnHover), (object) isOver, (SendMessageOptions) 1);
  }
}
