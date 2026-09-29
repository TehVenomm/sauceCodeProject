// Decompiled with JetBrains decompiler
// Type: UIWigetCrossFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIWigetCrossFade : MonoBehaviour
{
  public UIWidget[] targetWidget;
  private readonly float kIdleSec = 2f;
  private readonly float kFadeSec = 0.5f;
  private bool bUpdate;
  private int targetIndex;
  private UIWigetCrossFade.eFadeState fadeState;
  private float processSec;

  public void Play()
  {
    if (this.bUpdate || ((IList<UIWidget>) this.targetWidget).IsNullOrEmpty<UIWidget>())
      return;
    this.bUpdate = true;
    this.targetIndex = 0;
    this.fadeState = UIWigetCrossFade.eFadeState.Idle;
    for (int index = 0; index < this.targetWidget.Length; ++index)
      this.targetWidget[index].alpha = index == this.targetIndex ? 1f : 0.0f;
  }

  public void Reset()
  {
    this.bUpdate = false;
    this.targetIndex = 0;
    this.fadeState = UIWigetCrossFade.eFadeState.None;
    if (((IList<UIWidget>) this.targetWidget).IsNullOrEmpty<UIWidget>())
      return;
    for (int index = 0; index < this.targetWidget.Length; ++index)
      this.targetWidget[index].alpha = 1f;
  }

  private void Update()
  {
    if (!this.bUpdate)
      return;
    switch (this.fadeState)
    {
      case UIWigetCrossFade.eFadeState.In:
        this.processSec += Time.deltaTime;
        float num1 = this.processSec / this.kFadeSec;
        if ((double) num1 > 1.0)
          num1 = 1f;
        this.targetWidget[this.targetIndex].alpha = num1;
        if ((double) this.processSec < (double) this.kFadeSec)
          break;
        this.processSec = 0.0f;
        this.fadeState = UIWigetCrossFade.eFadeState.Idle;
        break;
      case UIWigetCrossFade.eFadeState.Idle:
        this.processSec += Time.deltaTime;
        if ((double) this.processSec < (double) this.kIdleSec)
          break;
        this.processSec = 0.0f;
        this.fadeState = UIWigetCrossFade.eFadeState.Out;
        break;
      case UIWigetCrossFade.eFadeState.Out:
        this.processSec += Time.deltaTime;
        float num2 = (float) (1.0 - (double) this.processSec / (double) this.kFadeSec);
        if ((double) num2 < 0.0)
          num2 = 0.0f;
        this.targetWidget[this.targetIndex].alpha = num2;
        if ((double) this.processSec < (double) this.kFadeSec)
          break;
        this.processSec = 0.0f;
        this.fadeState = UIWigetCrossFade.eFadeState.In;
        if (++this.targetIndex < this.targetWidget.Length)
          break;
        this.targetIndex = 0;
        break;
    }
  }

  private enum eFadeState
  {
    None,
    In,
    Idle,
    Out,
  }
}
