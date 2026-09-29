// Decompiled with JetBrains decompiler
// Type: UITouchAndRelease
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UITouchAndRelease : MonoBehaviour
{
  private string touchEventName;
  private string releaseEventName;
  private object eventData;
  private bool touched;

  public static void Set(
    GameObject button,
    string touch_event_name,
    string release_event_name = null,
    object event_data = null)
  {
    if (Object.op_Equality((Object) button.GetComponent<UIButton>(), (Object) null))
      return;
    UITouchAndRelease uiTouchAndRelease = button.GetComponent<UITouchAndRelease>();
    if (Object.op_Equality((Object) uiTouchAndRelease, (Object) null))
      uiTouchAndRelease = button.AddComponent<UITouchAndRelease>();
    uiTouchAndRelease.touchEventName = touch_event_name;
    uiTouchAndRelease.releaseEventName = release_event_name;
    uiTouchAndRelease.eventData = event_data;
  }

  public static void NoEventRelease(GameObject button)
  {
    UITouchAndRelease component = button.GetComponent<UITouchAndRelease>();
    if (!Object.op_Implicit((Object) component))
      return;
    component.touched = false;
  }

  private void OnHover(bool isOver)
  {
    if (isOver || !this.touched)
      return;
    this.Send(false);
  }

  private void OnPress(bool isPressed) => this.Send(isPressed);

  private void OnDisable()
  {
    if (AppMain.isApplicationQuit || !this.touched)
      return;
    this.Send(false);
  }

  private void Send(bool is_touch)
  {
    if (this.touched == is_touch)
      return;
    this.touched = is_touch;
    string event_name = !is_touch ? this.releaseEventName : this.touchEventName;
    if (string.IsNullOrEmpty(event_name))
      return;
    UIGameSceneEventSender.SendEvent(nameof (UITouchAndRelease), ((Component) this).gameObject, event_name, this.eventData);
  }
}
