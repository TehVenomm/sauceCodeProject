// Decompiled with JetBrains decompiler
// Type: ChatUITweenGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public abstract class ChatUITweenGroup
{
  private UIRect root;
  private UITweener openTween;
  private UITweener closeTween;
  private ChatUITweenGroup.STATE state = ChatUITweenGroup.STATE.CLOSED;

  public UIRect rootRect => this.root;

  public bool isOpened => this.state == ChatUITweenGroup.STATE.OPENED;

  public bool isOpening => this.state == ChatUITweenGroup.STATE.OPENING;

  public bool isClosing => this.state == ChatUITweenGroup.STATE.CLOSING;

  public bool isTransitioning => this.isOpening || this.isClosing;

  public ChatUITweenGroup(UIRect root)
  {
    if (!Object.op_Implicit((Object) root))
      return;
    this.root = root;
  }

  public void Initialize()
  {
    this.openTween = this.CreateTween(true);
    this.closeTween = this.CreateTween(false);
  }

  protected abstract UITweener CreateTween(bool isOpenTween);

  protected virtual void OnPreClose()
  {
  }

  protected virtual void OnPostOpen()
  {
  }

  public void Open(System.Action on_finished)
  {
    if (!Object.op_Implicit((Object) this.root))
    {
      this.state = ChatUITweenGroup.STATE.OPENED;
    }
    else
    {
      if (((Behaviour) this.closeTween).enabled)
      {
        this.closeTween.SetOnFinished((EventDelegate) null);
        this.closeTween.SetStartToCurrentValue();
        this.closeTween.ResetToBeginning();
        ((Behaviour) this.closeTween).enabled = false;
      }
      if (!((Component) this.openTween).gameObject.activeSelf)
        ((Component) this.openTween).gameObject.SetActive(true);
      this.state = ChatUITweenGroup.STATE.OPENING;
      ((Behaviour) this.openTween).enabled = true;
      this.openTween.SetStartToCurrentValue();
      this.openTween.ResetToBeginning();
      this.openTween.SetOnFinished((EventDelegate.Callback) (() =>
      {
        this.state = ChatUITweenGroup.STATE.OPENED;
        this.OnPostOpen();
        on_finished();
      }));
      this.openTween.PlayForward();
    }
  }

  public void OpenImmediately()
  {
    if (!Object.op_Implicit((Object) this.root))
    {
      this.state = ChatUITweenGroup.STATE.OPENED;
    }
    else
    {
      this.state = ChatUITweenGroup.STATE.OPENED;
      this.openTween.Sample(1f, true);
    }
  }

  public void Close(System.Action on_finished)
  {
    if (!Object.op_Implicit((Object) this.root))
    {
      this.state = ChatUITweenGroup.STATE.CLOSED;
    }
    else
    {
      this.OnPreClose();
      if (!((Component) this.openTween).gameObject.activeSelf)
      {
        on_finished();
      }
      else
      {
        if (((Behaviour) this.openTween).enabled)
        {
          this.openTween.SetOnFinished((EventDelegate) null);
          this.openTween.SetStartToCurrentValue();
          this.openTween.ResetToBeginning();
          ((Behaviour) this.openTween).enabled = false;
        }
        this.state = ChatUITweenGroup.STATE.CLOSING;
        ((Behaviour) this.closeTween).enabled = true;
        this.closeTween.SetStartToCurrentValue();
        this.closeTween.ResetToBeginning();
        this.closeTween.SetOnFinished((EventDelegate.Callback) (() =>
        {
          this.state = ChatUITweenGroup.STATE.CLOSED;
          ((Component) this.root).gameObject.SetActive(false);
          on_finished();
        }));
        this.closeTween.PlayForward();
      }
    }
  }

  public void CloseImmediately()
  {
    if (!Object.op_Implicit((Object) this.root))
    {
      this.state = ChatUITweenGroup.STATE.CLOSED;
    }
    else
    {
      this.state = ChatUITweenGroup.STATE.CLOSED;
      this.closeTween.Sample(1f, true);
    }
  }

  private enum STATE
  {
    OPENED,
    OPENING,
    CLOSING,
    CLOSED,
  }
}
