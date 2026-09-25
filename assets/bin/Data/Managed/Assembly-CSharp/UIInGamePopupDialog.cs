// Decompiled with JetBrains decompiler
// Type: UIInGamePopupDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIInGamePopupDialog : MonoBehaviourSingleton<UIInGamePopupDialog>
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
  protected UIInGamePopupDialog.DialogInfo[] dialogInfo;
  protected List<UIInGamePopupDialog.Desc> openInfoList = new List<UIInGamePopupDialog.Desc>();
  protected Transform windowTransform;

  public bool enableDialog { get; protected set; }

  public bool isOpenDialog { get; protected set; }

  public UITransition[] transitions { get; private set; }

  public static void PushOpen(string text, bool is_important, float _show_time = 1.8f)
  {
    if (MonoBehaviourSingleton<UIInGamePopupDialog>.IsValid() && MonoBehaviourSingleton<UIInGamePopupDialog>.I.enableDialog)
    {
      MonoBehaviourSingleton<UIInGamePopupDialog>.I.openInfoList.Add(new UIInGamePopupDialog.Desc()
      {
        text = text,
        type = is_important ? 1 : 0,
        showTime = _show_time
      });
    }
    else
    {
      if (!MonoBehaviourSingleton<InGameManager>.IsValid())
        return;
      MonoBehaviourSingleton<InGameManager>.I.dialogOpenInfoList.Add(new UIInGamePopupDialog.Desc()
      {
        text = text,
        type = is_important ? 1 : 0,
        showTime = _show_time
      });
    }
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
    this.enableDialog = false;
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.window, (Object) null) || !this.enableDialog || MonoBehaviourSingleton<TransitionManager>.I.isTransing || this.isOpenDialog || this.openInfoList.Count <= 0)
      return;
    this.StartCoroutine(this.DoShowDialog(this.openInfoList[0]));
    this.openInfoList.RemoveAt(0);
  }

  public void SetEnableDialog(bool enable)
  {
    this.enableDialog = enable;
    if (!MonoBehaviourSingleton<InGameManager>.IsValid())
      return;
    List<UIInGamePopupDialog.Desc> dialogOpenInfoList = MonoBehaviourSingleton<InGameManager>.I.dialogOpenInfoList;
    if (enable)
    {
      this.openInfoList.AddRange((IEnumerable<UIInGamePopupDialog.Desc>) dialogOpenInfoList.GetRange(0, dialogOpenInfoList.Count));
      dialogOpenInfoList.Clear();
    }
    else
    {
      dialogOpenInfoList.AddRange((IEnumerable<UIInGamePopupDialog.Desc>) this.openInfoList.GetRange(0, this.openInfoList.Count));
      this.openInfoList.Clear();
    }
  }

  public bool IsShowingDialog() => this.isOpenDialog || this.openInfoList.Count > 0;

  private IEnumerator DoShowDialog(UIInGamePopupDialog.Desc desc)
  {
    this.isOpenDialog = true;
    this.window.SetActive(true);
    this.tweenCtrl.Reset();
    if (this.dialogInfo.Length > desc.type)
    {
      this.windowTransform.parent = this.dialogInfo[desc.type].link;
      this.windowTransform.localPosition = Vector3.zero;
      int index = 0;
      for (int length = this.sprites.Length; index < length; ++index)
        this.sprites[index].spriteName = this.dialogInfo[desc.type].spritesNames[index];
      this.messageLabel.effectColor = this.dialogInfo[desc.type].lineColor;
    }
    this.messageLabel.supportEncoding = true;
    this.messageLabel.text = desc.text;
    if (this.transitions != null)
    {
      int index = 0;
      for (int length = this.transitions.Length; index < length; ++index)
        this.transitions[index].Open((System.Action) null);
    }
    bool play_tween = true;
    this.tweenCtrl.Play(onFinished: (EventDelegate.Callback) (() => play_tween = false));
    yield return (object) new WaitForSeconds(desc.showTime);
    while (play_tween)
      yield return (object) null;
    yield return (object) new WaitForSeconds(desc.showTime);
    if (this.transitions != null)
    {
      int index = 0;
      for (int length = this.transitions.Length; index < length; ++index)
        this.transitions[index].Close((System.Action) null);
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
    public int type;
    public float showTime;
  }
}
