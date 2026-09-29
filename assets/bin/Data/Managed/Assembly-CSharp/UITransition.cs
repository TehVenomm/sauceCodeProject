// Decompiled with JetBrains decompiler
// Type: UITransition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UITransition : MonoBehaviour
{
  public UITweener[] openTweens;
  public UITweener[] closeTweens;
  public UITweener[] nextTweens;
  public UITweener[] prevTweens;
  private int busyCount;
  private System.Action callback;

  public bool isBusy => this.busyCount != 0;

  private void Awake() => this.InitTweens();

  private void InitTweens(UITweener[] tweens)
  {
    if (tweens == null)
      return;
    int index = 0;
    for (int length = tweens.Length; index < length; ++index)
    {
      ((Behaviour) tweens[index]).enabled = false;
      tweens[index].AddOnFinished(new EventDelegate(new EventDelegate.Callback(this.OnFinished)));
    }
  }

  public void InitTweens()
  {
    this.InitTweens(this.openTweens);
    this.InitTweens(this.closeTweens);
    this.InitTweens(this.nextTweens);
    this.InitTweens(this.prevTweens);
  }

  private void OnFinished()
  {
    --this.busyCount;
    if (this.busyCount != 0 || this.callback == null)
      return;
    this.callback();
    this.callback = (System.Action) null;
  }

  private void StartAnim(UITweener[] tweens, System.Action _callback)
  {
    if (this.busyCount != 0)
      return;
    this.callback = _callback;
    if (tweens == null || tweens.Length == 0)
    {
      this.busyCount = 1;
      MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.OnFinished());
    }
    else
    {
      this.busyCount = tweens.Length;
      int index1 = 0;
      for (int length = tweens.Length; index1 < length; ++index1)
      {
        ((Behaviour) tweens[index1]).enabled = true;
        tweens[index1].ResetToBeginning();
      }
      if (!GameSceneManager.isAutoEventSkip || !this.isBusy)
        return;
      int index2 = 0;
      for (int length = tweens.Length; index2 < length; ++index2)
      {
        if (Object.op_Inequality((Object) tweens[index2], (Object) null))
        {
          tweens[index2].tweenFactor = 1f;
          tweens[index2].Sample(1f, false);
        }
      }
    }
  }

  public void Play(UITransition.TYPE type, System.Action callback)
  {
    this.StartAnim(this.GetTweens(type), callback);
  }

  public void Open(System.Action callback) => this.Play(UITransition.TYPE.OPEN, callback);

  public void Close(System.Action callback) => this.Play(UITransition.TYPE.CLOSE, callback);

  public UITweener[] GetTweens(UITransition.TYPE type)
  {
    switch (type)
    {
      case UITransition.TYPE.OPEN:
        return this.openTweens;
      case UITransition.TYPE.CLOSE:
        return this.closeTweens;
      case UITransition.TYPE.NEXT:
        return this.nextTweens != null && this.nextTweens.Length != 0 ? this.nextTweens : this.closeTweens;
      case UITransition.TYPE.PREV:
        return this.prevTweens != null && this.prevTweens.Length != 0 ? this.prevTweens : this.openTweens;
      default:
        return (UITweener[]) null;
    }
  }

  public static UITransition.TYPE GetType(char c)
  {
    switch (c)
    {
      case 'C':
      case 'c':
        return UITransition.TYPE.CLOSE;
      case 'N':
      case 'n':
        return UITransition.TYPE.NEXT;
      case 'O':
      case 'o':
        return UITransition.TYPE.OPEN;
      case 'P':
      case 'p':
        return UITransition.TYPE.PREV;
      default:
        return UITransition.TYPE.NONE;
    }
  }

  public enum TYPE
  {
    NONE,
    OPEN,
    CLOSE,
    NEXT,
    PREV,
  }
}
