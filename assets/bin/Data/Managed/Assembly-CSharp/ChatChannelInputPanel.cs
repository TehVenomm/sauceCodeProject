// Decompiled with JetBrains decompiler
// Type: ChatChannelInputPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ChatChannelInputPanel
{
  private ChatUITweenGroup rootPanelTween;
  private UILabel[] numberLabels;
  private UIButton okButton;
  private int currentPosition;
  private int[] number;
  private Action<int> onOK;
  private System.Action onClose;
  private static string NONE = "-";

  public bool isOpened => this.rootPanelTween.isOpened;

  public ChatChannelInputPanel(ChatUITweenGroup root) => this.rootPanelTween = root;

  public void SetNumLabels(params UILabel[] labels)
  {
    this.numberLabels = labels;
    this.number = new int[this.numberLabels.Length];
    this.ClearNumbers();
  }

  public void SetNumButtons(params UIButton[] buttons)
  {
    for (int num = 0; num < buttons.Length; ++num)
      buttons[num].onClick.Add(this.CreateNumButtonEvent(num));
  }

  private EventDelegate CreateNumButtonEvent(int num)
  {
    return new EventDelegate((EventDelegate.Callback) (() => this.OnNumber(num)));
  }

  public void SetOKButton(UIButton button)
  {
    button.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.OnOK)));
    this.okButton = button;
  }

  public void SetClearButton(UIButton button)
  {
    button.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.OnClear)));
  }

  public void SetCloseButton(UIButton button)
  {
    button.onClick.Add(new EventDelegate((EventDelegate.Callback) (() =>
    {
      SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
      this.OnClose();
    })));
  }

  public void SetOnOKDelegate(Action<int> onOK) => this.onOK = onOK;

  public void SetOnCloseButtonDelegate(System.Action onClose) => this.onClose = onClose;

  public void Open()
  {
    this.UpdateOKButton();
    this.UpdateNumberLabels();
    this.rootPanelTween.Open((System.Action) (() => { }));
  }

  public void Close() => this.rootPanelTween.Close((System.Action) (() => this.ClearNumbers()));

  private void OnNumber(int num)
  {
    SoundManager.PlaySystemSE(SoundID.UISE.CLICK);
    if (this.currentPosition < 0)
      return;
    this.number[this.currentPosition] = num;
    --this.currentPosition;
    this.UpdateOKButton();
    this.UpdateNumberLabels();
  }

  private void OnOK()
  {
    SoundManager.PlaySystemSE(SoundID.UISE.OK);
    int num = 0;
    for (int index = 0; index < this.number.Length; ++index)
    {
      if (this.number[index] >= 0)
        num += Mathf.RoundToInt((float) this.number[index] * Mathf.Pow(10f, (float) index));
    }
    if (this.onOK == null)
      return;
    this.onOK(num);
  }

  private void OnClear()
  {
    SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
    this.ClearNumbers();
    this.UpdateOKButton();
    this.UpdateNumberLabels();
  }

  private void OnClose()
  {
    if (this.onClose != null)
      this.onClose();
    this.Close();
  }

  private void ClearNumbers()
  {
    this.currentPosition = this.number.Length - 1;
    for (int index = 0; index < this.number.Length; ++index)
      this.number[index] = -1;
  }

  private void UpdateOKButton() => this.okButton.isEnabled = this.currentPosition < 0;

  private void UpdateNumberLabels()
  {
    for (int index = 0; index < this.numberLabels.Length; ++index)
      this.numberLabels[index].text = this.number[index] < 0 ? ChatChannelInputPanel.NONE : this.number[index].ToString();
  }
}
