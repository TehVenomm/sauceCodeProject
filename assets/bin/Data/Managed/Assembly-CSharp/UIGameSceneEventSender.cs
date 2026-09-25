// Decompiled with JetBrains decompiler
// Type: UIGameSceneEventSender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
[AddComponentMenu("ProjectUI/UIGameSceneEventSender")]
public class UIGameSceneEventSender : MonoBehaviour
{
  public string eventName = string.Empty;
  private bool enablePress;
  private bool enableRelease;
  private UIButtonTweenEventCtrl buttonTweenCtrl;
  private UIPlaySoundCustom playSoundCtrl;

  public object eventData { get; set; }

  public Action<string, object, string> callback { get; set; }

  private void Awake()
  {
    this.buttonTweenCtrl = ((Component) this).gameObject.GetComponent<UIButtonTweenEventCtrl>();
    this.playSoundCtrl = ((Component) this).gameObject.GetComponent<UIPlaySoundCustom>();
  }

  private void OnValidate()
  {
    UIButton component = ((Component) this).gameObject.GetComponent<UIButton>();
    if (!Object.op_Inequality((Object) component, (Object) null) || component.onClick.Find((Predicate<EventDelegate>) (o => Object.op_Equality((Object) o.target, (Object) this))) != null)
      return;
    component.onClick.Add(new EventDelegate((MonoBehaviour) this, "SendEvent"));
  }

  private void OnPress(bool isDown)
  {
    if (Object.op_Equality((Object) UICamera.currentTouch.current, (Object) null) || !this.IsActiveButton())
      return;
    bool flag = ((Object) ((Component) this).gameObject).GetInstanceID() == ((Object) UICamera.currentTouch.current).GetInstanceID();
    if (isDown)
    {
      this.enablePress = flag;
      this.enableRelease = false;
      if (!Object.op_Inequality((Object) this.buttonTweenCtrl, (Object) null) || !flag)
        return;
      this.buttonTweenCtrl.PlayPush(isDown);
    }
    else
    {
      if (!this.enablePress)
        return;
      this.enableRelease = flag;
      if (!Object.op_Implicit((Object) this.buttonTweenCtrl) || !this.enablePress)
        return;
      this.buttonTweenCtrl.PlayPush(isDown);
    }
  }

  private IEnumerator DoButtonTweenCtrlReset()
  {
    yield return (object) null;
    if (Object.op_Inequality((Object) this.buttonTweenCtrl, (Object) null))
      this.buttonTweenCtrl.Reset();
  }

  public void SendEvent()
  {
    if (!this.enablePress || !this.enableRelease || !this.IsActiveButton())
      return;
    this.enablePress = this.enableRelease = false;
    this.PlaySound();
    if (Object.op_Inequality((Object) this.buttonTweenCtrl, (Object) null) && this.buttonTweenCtrl.tweens.Length != 0 && Object.op_Inequality((Object) this.buttonTweenCtrl.tweens[0], (Object) null))
    {
      if (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
        return;
      if (MonoBehaviourSingleton<UIManager>.IsValid())
        MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.UITWEEN_SMALL, true);
      this.buttonTweenCtrl.Reset();
      this.buttonTweenCtrl.Play(onFinished: (EventDelegate.Callback) (() =>
      {
        if (MonoBehaviourSingleton<UIManager>.IsValid())
          MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.UITWEEN_SMALL, false);
        if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
          this._SendEvent();
        else
          this.StartCoroutine(this.DoButtonTweenCtrlReset());
      }));
    }
    else
      this._SendEvent();
  }

  public void _SendEvent()
  {
    UIGameSceneEventSender.SendEvent("UIButton", ((Component) this).gameObject, this.eventName, this.eventData, this.callback);
  }

  private void PlaySound()
  {
    if (Object.op_Inequality((Object) this.playSoundCtrl, (Object) null))
      this.playSoundCtrl.Play();
    else
      this.PlayUISE(this.ChooseSE());
  }

  private SoundID.UISE ChooseSE()
  {
    SoundID.UISE uise = SoundID.UISE.CLICK;
    if (this.eventName == "[BACK]" || this.eventName == "CLOSE" || this.eventName == "NO")
      return SoundID.UISE.CANCEL;
    return this.eventName == "DECIDE" || this.eventName == "OK" || this.eventName == "YES" ? SoundID.UISE.OK : uise;
  }

  private void PlayUISE(SoundID.UISE type)
  {
    if (type == SoundID.UISE.INVALID || !MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    SoundManager.PlaySystemSE(type);
  }

  private bool IsActiveButton()
  {
    return TutorialMessage.IsActiveButton(((Component) this).gameObject) || this.eventName == "TUTORIAL_NEXT" || MonoBehaviourSingleton<GameSceneManager>.I.isOpenImportantDialog || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "CommonErrorDialog";
  }

  public static void SendEvent(
    string caller,
    GameObject sender,
    string event_name,
    object event_data = null,
    Action<string, object, string> callback = null)
  {
    if (string.IsNullOrEmpty(event_name))
    {
      Log.Error(LOG.UI, "eventName == empty");
    }
    else
    {
      if (!MonoBehaviourSingleton<GameSceneManager>.IsValid() || GameSceneEvent.IsStay())
        return;
      if (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      {
        UIPanel componentInParent = sender.GetComponentInParent<UIPanel>();
        if (Object.op_Equality((Object) componentInParent, (Object) null) || componentInParent.depth != 9999)
        {
          Log.Error(LOG.UI, "GameSceneManager.I.isChangeing == true");
          return;
        }
      }
      string check_app_ver = (string) null;
      UIGameSceneEventSenderVersionRestriction component = sender.GetComponent<UIGameSceneEventSenderVersionRestriction>();
      if (Object.op_Inequality((Object) component, (Object) null))
        check_app_ver = component.GetCheckApplicationVersionText();
      if (callback == null)
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(caller, sender, event_name, event_data, check_app_ver);
      else
        callback(event_name, event_data, check_app_ver);
    }
  }
}
