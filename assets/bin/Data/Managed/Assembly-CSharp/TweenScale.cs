// Decompiled with JetBrains decompiler
// Type: TweenScale
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Tween/Tween Scale")]
public class TweenScale : UITweener
{
  public Vector3 from = Vector3.one;
  public Vector3 to = Vector3.one;
  public bool updateTable;
  private Transform mTrans;
  private UITable mTable;

  public Transform cachedTransform
  {
    get
    {
      if (Object.op_Equality((Object) this.mTrans, (Object) null))
        this.mTrans = ((Component) this).transform;
      return this.mTrans;
    }
  }

  public Vector3 value
  {
    get => this.cachedTransform.localScale;
    set => this.cachedTransform.localScale = value;
  }

  [Obsolete("Use 'value' instead")]
  public Vector3 scale
  {
    get => this.value;
    set => this.value = value;
  }

  protected override void OnUpdate(float factor, bool isFinished)
  {
    this.value = Vector3.op_Addition(Vector3.op_Multiply(this.from, 1f - factor), Vector3.op_Multiply(this.to, factor));
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

  public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
  {
    TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration);
    tweenScale.from = tweenScale.value;
    tweenScale.to = scale;
    if ((double) duration <= 0.0)
    {
      tweenScale.Sample(1f, true);
      ((Behaviour) tweenScale).enabled = false;
    }
    return tweenScale;
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
