// Decompiled with JetBrains decompiler
// Type: UILongTouch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UILongTouch : MonoBehaviour
{
  private const float TOUCH_TIME = 0.75f;
  protected string eventName;
  protected object eventData;
  protected float time;

  public static void Set(GameObject button, string event_name, object event_data = null)
  {
    if (Object.op_Equality((Object) button.GetComponent<UIButton>(), (Object) null))
      return;
    UILongTouch uiLongTouch = button.GetComponent<UILongTouch>();
    if (Object.op_Equality((Object) uiLongTouch, (Object) null))
      uiLongTouch = button.AddComponent<UILongTouch>();
    uiLongTouch.eventName = event_name;
    uiLongTouch.eventData = event_data;
  }

  private void OnHover(bool isOver)
  {
    if (isOver)
      return;
    this.time = 0.0f;
  }

  protected virtual void OnPress(bool isPressed)
  {
    if (!TutorialMessage.IsActiveButton(((Component) this).gameObject))
      return;
    if (isPressed)
      this.time = 0.75f;
    else
      this.time = 0.0f;
  }

  private void OnDragOut(GameObject go) => this.time = 0.0f;

  private void Update()
  {
    if ((double) this.time <= 0.0)
      return;
    this.time -= Time.deltaTime;
    if ((double) this.time > 0.0)
      return;
    UIScrollView componentInParent = ((Component) this).GetComponentInParent<UIScrollView>();
    if (!Object.op_Equality((Object) componentInParent, (Object) null) && (!Object.op_Inequality((Object) componentInParent, (Object) null) || componentInParent.isDragging) || !TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing || MonoBehaviourSingleton<GameSceneManager>.I.isCallingOnQuery)
      return;
    this._SendEvent();
  }

  private void OnDisable() => this.time = 0.0f;

  protected virtual void _SendEvent()
  {
    UIGameSceneEventSender.SendEvent(nameof (UILongTouch), ((Component) this).gameObject, this.eventName, this.eventData);
  }
}
