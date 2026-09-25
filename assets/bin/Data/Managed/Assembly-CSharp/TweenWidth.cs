// Decompiled with JetBrains decompiler
// Type: TweenWidth
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Width")]
public class TweenWidth : UITweener
{
  public int from = 100;
  public int to = 100;
  public bool updateTable;
  private UIWidget mWidget;
  private UITable mTable;

  public UIWidget cachedWidget
  {
    get
    {
      if (Object.op_Equality((Object) this.mWidget, (Object) null))
        this.mWidget = ((Component) this).GetComponent<UIWidget>();
      return this.mWidget;
    }
  }

  [Obsolete("Use 'value' instead")]
  public int width
  {
    get => this.value;
    set => this.value = value;
  }

  public int value
  {
    get => this.cachedWidget.width;
    set => this.cachedWidget.width = value;
  }

  protected override void OnUpdate(float factor, bool isFinished)
  {
    this.value = Mathf.RoundToInt((float) ((double) this.from * (1.0 - (double) factor) + (double) this.to * (double) factor));
    if (!this.updateTable)
      return;
    if (Object.op_Equality((Object) this.mTable, (Object) null))
    {
      this.mTable = NGUITools.FindInParents<UITable>(((Component) this).gameObject);
      if (Object.op_Equality((Object) this.mTable, (Object) null))
      {
        this.updateTable = false;
        return;
      }
    }
    this.mTable.repositionNow = true;
  }

  public static TweenWidth Begin(UIWidget widget, float duration, int width)
  {
    TweenWidth tweenWidth = UITweener.Begin<TweenWidth>(((Component) widget).gameObject, duration);
    tweenWidth.from = widget.width;
    tweenWidth.to = width;
    if ((double) duration <= 0.0)
    {
      tweenWidth.Sample(1f, true);
      ((Behaviour) tweenWidth).enabled = false;
    }
    return tweenWidth;
  }

  [ContextMenu("Set 'From' to current value")]
  public override void SetStartToCurrentValue() => this.from = this.value;

  [ContextMenu("Set 'To' to current value")]
  public override void SetEndToCurrentValue() => this.to = this.value;

  [ContextMenu("Assume value of 'From'")]
  private void SetCurrentValueToStart() => this.value = this.from;

  [ContextMenu("Assume value of 'To'")]
  private void SetCurrentValueToEnd() => this.value = this.to;
}
