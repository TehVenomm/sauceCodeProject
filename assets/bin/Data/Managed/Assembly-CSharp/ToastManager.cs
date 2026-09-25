// Decompiled with JetBrains decompiler
// Type: ToastManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ToastManager : MonoBehaviourSingleton<ToastManager>
{
  [SerializeField]
  protected GameObject window;
  [SerializeField]
  protected UILabel messageLabel;
  [SerializeField]
  protected UITweenCtrl tweenCtrl;
  [SerializeField]
  protected UISprite[] sprites;
  [SerializeField]
  protected ToastManager.DialogInfo dialogInfo;
  protected List<ToastManager.Desc> openInfoList = new List<ToastManager.Desc>();
  protected Transform windowTransform;

  public bool isOpenDialog { get; protected set; }

  public UITransition[] transitions { get; private set; }

  public static void PushOpen(string text, float _show_time = 1.8f)
  {
    if (!MonoBehaviourSingleton<ToastManager>.IsValid())
      return;
    MonoBehaviourSingleton<ToastManager>.I.openInfoList.Add(new ToastManager.Desc()
    {
      text = text,
      showTime = _show_time
    });
  }

  protected override void Awake()
  {
    base.Awake();
    if (Object.op_Equality((Object) this.window, (Object) null))
      return;
    this.windowTransform = this.window.transform;
    this.transitions = this.window.GetComponentsInChildren<UITransition>();
    this.window.SetActive(false);
    this.isOpenDialog = false;
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.window, (Object) null) || MonoBehaviourSingleton<TransitionManager>.IsValid() && MonoBehaviourSingleton<TransitionManager>.I.isTransing || this.isOpenDialog || this.openInfoList.Count <= 0)
      return;
    this.StartCoroutine(this.DoShowDialog(this.openInfoList[0]));
    this.openInfoList.RemoveAt(0);
  }

  public bool IsShowingDialog() => this.isOpenDialog || this.openInfoList.Count > 0;

  private IEnumerator DoShowDialog(ToastManager.Desc desc)
  {
    this.isOpenDialog = true;
    this.window.SetActive(true);
    this.tweenCtrl.Reset();
    this.windowTransform.parent = this.dialogInfo.link;
    this.windowTransform.localPosition = Vector3.zero;
    int index1 = 0;
    for (int length = this.sprites.Length; index1 < length; ++index1)
      this.sprites[index1].spriteName = this.dialogInfo.spritesNames[index1];
    this.messageLabel.effectColor = this.dialogInfo.lineColor;
    this.messageLabel.supportEncoding = true;
    this.messageLabel.text = desc.text;
    if (this.transitions != null)
    {
      int index2 = 0;
      for (int length = this.transitions.Length; index2 < length; ++index2)
        this.transitions[index2].Open((System.Action) null);
    }
    bool play_tween = true;
    this.tweenCtrl.Play(onFinished: (EventDelegate.Callback) (() => play_tween = false));
    yield return (object) new WaitForSeconds(desc.showTime);
    while (play_tween)
      yield return (object) null;
    yield return (object) new WaitForSeconds(desc.showTime);
    if (this.transitions != null)
    {
      int index3 = 0;
      for (int length = this.transitions.Length; index3 < length; ++index3)
        this.transitions[index3].Close((System.Action) null);
    }
    yield return (object) this.StartCoroutine(this.DoWaitTransitions());
    this.isOpenDialog = false;
  }

  private IEnumerator DoWaitTransitions()
  {
    if (this.transitions.Length != 0)
    {
      while (true)
      {
        bool flag = false;
        int index = 0;
        for (int length = this.transitions.Length; index < length; ++index)
        {
          if (this.transitions[index].isBusy)
          {
            flag = true;
            break;
          }
        }
        if (flag)
          yield return (object) null;
        else
          break;
      }
    }
  }

  [Serializable]
  public class DialogInfo
  {
    public Transform link;
    public Color lineColor;
    public string[] spritesNames;
  }

  public class Desc
  {
    public string text;
    public float showTime;
  }
}
