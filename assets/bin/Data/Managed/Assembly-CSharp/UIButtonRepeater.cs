// Decompiled with JetBrains decompiler
// Type: UIButtonRepeater
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIGameSceneEventSender))]
public class UIButtonRepeater : UILongTouch
{
  public float firstInterval = 0.15f;
  public float interval = 0.05f;
  public float shortInterval = 0.025f;
  private const float FAST_MODE_PUSH_TIME = -1f;
  private float pushTime;
  private float repeatTime;
  private bool isFirstWait = true;
  private bool terminateRepeat;

  public static void SetRepeatButton(GameObject button, string event_name, object event_data = null)
  {
    if (Object.op_Equality((Object) button.GetComponent<UIButton>(), (Object) null))
      return;
    UIButtonRepeater uiButtonRepeater = button.GetComponent<UIButtonRepeater>();
    if (Object.op_Equality((Object) uiButtonRepeater, (Object) null))
      uiButtonRepeater = button.AddComponent<UIButtonRepeater>();
    uiButtonRepeater.eventName = event_name;
    uiButtonRepeater.eventData = event_data;
  }

  public void Terminate() => this.terminateRepeat = true;

  protected override void _SendEvent()
  {
    if (!this.CheckSendTime())
      return;
    UIGameSceneEventSender.SendEvent(nameof (UIButtonRepeater), ((Component) this).gameObject, this.eventName, this.eventData);
  }

  protected override void OnPress(bool isPressed)
  {
    this.repeatTime = 0.0f;
    this.pushTime = 0.0f;
    this.isFirstWait = true;
    this.terminateRepeat = false;
    base.OnPress(isPressed);
  }

  private bool CheckSendTime()
  {
    if (this.terminateRepeat)
    {
      this.time = 0.0f;
      return false;
    }
    this.time = 1f / 1000f;
    this.repeatTime -= Time.deltaTime;
    if ((double) this.repeatTime > 0.0)
      return false;
    this.repeatTime = this.GetIntervalTime();
    return true;
  }

  private float GetIntervalTime()
  {
    this.pushTime -= Time.deltaTime;
    float intervalTime;
    if (this.isFirstWait)
    {
      intervalTime = this.firstInterval;
      this.isFirstWait = false;
    }
    else
      intervalTime = (double) this.pushTime < -1.0 ? this.shortInterval : this.interval;
    return intervalTime;
  }
}
