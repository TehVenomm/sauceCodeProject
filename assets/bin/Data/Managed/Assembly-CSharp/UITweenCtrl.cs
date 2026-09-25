// Decompiled with JetBrains decompiler
// Type: UITweenCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UITweenCtrl : MonoBehaviour
{
  [SerializeField]
  private int _id;
  public UITweener[] tweens;
  protected bool isPlaying;
  private UITable uiTable;

  public static void Set(Transform root)
  {
    if (Object.op_Inequality((Object) ((Component) root).GetComponent<UITweenCtrl>(), (Object) null))
      return;
    UITweenCtrl uiTweenCtrl = ((Component) root).gameObject.AddComponent<UITweenCtrl>();
    UITweener[] componentsInChildren = ((Component) root).GetComponentsInChildren<UITweener>();
    int index = 0;
    for (int length = componentsInChildren.Length; index < length; ++index)
      ((Behaviour) componentsInChildren[index]).enabled = false;
    uiTweenCtrl.tweens = componentsInChildren;
  }

  private static UITweenCtrl SearchTweenCtrl(Transform root, int tween_ctrl_id)
  {
    UITweenCtrl[] components = ((Component) root).GetComponents<UITweenCtrl>();
    if (components == null || components.Length == 0)
      return (UITweenCtrl) null;
    UITweenCtrl c = (UITweenCtrl) null;
    if (components.Length == 1)
      c = components[0];
    else
      Array.ForEach<UITweenCtrl>(components, (Action<UITweenCtrl>) (tw =>
      {
        if (Object.op_Inequality((Object) c, (Object) null) || tw.id != tween_ctrl_id)
          return;
        c = tw;
      }));
    return c;
  }

  public static void Play(
    Transform root,
    bool forward = true,
    EventDelegate.Callback callback = null,
    bool is_input_block = true,
    int tween_ctrl_id = 0)
  {
    UITweenCtrl uiTweenCtrl = UITweenCtrl.SearchTweenCtrl(root, tween_ctrl_id);
    if (Object.op_Equality((Object) uiTweenCtrl, (Object) null))
      return;
    if (is_input_block)
      MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.UITWEEN_SMALL, true);
    uiTweenCtrl.Play(forward, (EventDelegate.Callback) (() =>
    {
      if (is_input_block)
        MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.UITWEEN_SMALL, false);
      if (callback == null)
        return;
      callback();
    }));
  }

  public static void Skip(Transform root, bool forward = true, int tween_ctrl_id = 0)
  {
    UITweenCtrl uiTweenCtrl = UITweenCtrl.SearchTweenCtrl(root, tween_ctrl_id);
    if (Object.op_Equality((Object) uiTweenCtrl, (Object) null))
      return;
    uiTweenCtrl.Skip(forward);
  }

  public static void Reset(Transform root, int tween_ctrl_id = 0)
  {
    UITweenCtrl uiTweenCtrl = UITweenCtrl.SearchTweenCtrl(root, tween_ctrl_id);
    if (Object.op_Equality((Object) uiTweenCtrl, (Object) null))
      return;
    uiTweenCtrl.Reset();
  }

  public static void SetDurationWithRate(Transform root, float rate, int tween_ctrl_id = 0)
  {
    foreach (UITweener tween in UITweenCtrl.SearchTweenCtrl(root, tween_ctrl_id).tweens)
      tween.duration *= rate;
  }

  public int id => this._id;

  private void Awake()
  {
    if (this.tweens == null || this.tweens.Length == 0)
      return;
    this.FillInTheBlanks();
    this.Reset();
  }

  public void Play(bool forward = true, EventDelegate.Callback onFinished = null)
  {
    this._Play(this.tweens, forward, onFinished);
  }

  protected void _Play(UITweener[] target_tweens, bool forward = true, EventDelegate.Callback onFinished = null)
  {
    if (target_tweens == null || target_tweens.Length == 0 || this.isPlaying)
      return;
    if (Object.op_Equality((Object) target_tweens[0], (Object) null))
    {
      Log.Error("tween[0] = null!");
    }
    else
    {
      this.isPlaying = true;
      this.uiTable = ((Component) this).gameObject.GetComponentInParent<UITable>();
      if (onFinished != null)
        EventDelegate.Add(target_tweens[0].onFinished, onFinished, true);
      EventDelegate.Add(target_tweens[0].onFinished, new EventDelegate.Callback(this.OnFinished));
      int index1 = 0;
      for (int length = target_tweens.Length; index1 < length; ++index1)
      {
        if (!Object.op_Equality((Object) target_tweens[index1], (Object) null))
          this._TweenPlay(target_tweens[index1], forward);
      }
      if (!GameSceneManager.isAutoEventSkip)
        return;
      MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() =>
      {
        if (!this.isPlaying)
          return;
        int index2 = 0;
        for (int length = target_tweens.Length; index2 < length; ++index2)
        {
          if (Object.op_Inequality((Object) target_tweens[index2], (Object) null))
            target_tweens[index2].tweenFactor = 1f;
        }
      });
    }
  }

  protected virtual void _TweenPlay(UITweener target, bool forward) => target.Play(forward);

  public void Reset() => this._Reset(this.tweens);

  protected void _Reset(UITweener[] target_tweens)
  {
    if (target_tweens == null || target_tweens.Length == 0)
      return;
    if (Object.op_Equality((Object) target_tweens[0], (Object) null))
    {
      Log.Error("tween[0] = null!");
    }
    else
    {
      this.isPlaying = false;
      this.uiTable = ((Component) this).gameObject.GetComponentInParent<UITable>();
      EventDelegate.Set(target_tweens[0].onFinished, new EventDelegate.Callback(this.OnFinished));
      int index = 0;
      for (int length = target_tweens.Length; index < length; ++index)
      {
        if (!Object.op_Equality((Object) target_tweens[index], (Object) null))
          this._TweenReset(target_tweens[index]);
      }
    }
  }

  protected virtual void _TweenReset(UITweener target)
  {
    float duration = target.duration;
    float delay = target.delay;
    UITweener.Style style = target.style;
    target.duration = 0.0f;
    target.delay = 0.0f;
    target.style = UITweener.Style.Once;
    target.Play(false);
    target.style = style;
    target.duration = duration;
    target.delay = delay;
  }

  public void Skip(bool forward = true) => this._Skip(this.tweens, forward);

  protected void _Skip(UITweener[] target_tweens, bool forward = true)
  {
    if (target_tweens == null || target_tweens.Length == 0)
      return;
    int index = 0;
    for (int length = target_tweens.Length; index < length; ++index)
    {
      if (!Object.op_Equality((Object) target_tweens[index], (Object) null))
      {
        float num = forward ? 1f : 0.0f;
        target_tweens[index].tweenFactor = num;
        target_tweens[index].Sample(target_tweens[index].tweenFactor, false);
      }
    }
  }

  private void OnFinished()
  {
    this.isPlaying = false;
    if (!Object.op_Inequality((Object) this.uiTable, (Object) null))
      return;
    this.uiTable.Reposition();
    this.uiTable = (UITable) null;
  }

  public void LateUpdate()
  {
    if (!Object.op_Inequality((Object) this.uiTable, (Object) null))
      return;
    this.uiTable.Reposition();
  }

  public void FillInTheBlanks()
  {
    int length1 = this.tweens.Length;
    for (int index1 = 0; index1 < length1; ++index1)
    {
      if (Object.op_Equality((Object) this.tweens[index1], (Object) null))
      {
        --length1;
        int index2 = index1;
        for (int length2 = this.tweens.Length; index2 < length2; ++index2)
          this.tweens[index2] = index2 >= length2 - 1 ? (UITweener) null : this.tweens[index2 + 1];
        Array.Resize<UITweener>(ref this.tweens, length1);
        --index1;
      }
    }
  }
}
