// Decompiled with JetBrains decompiler
// Type: TweenTransform
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Tween/Tween Transform")]
public class TweenTransform : UITweener
{
  public Transform from;
  public Transform to;
  public bool parentWhenFinished;
  private Transform mTrans;
  private Vector3 mPos;
  private Quaternion mRot;
  private Vector3 mScale;

  protected override void OnUpdate(float factor, bool isFinished)
  {
    if (!Object.op_Inequality((Object) this.to, (Object) null))
      return;
    if (Object.op_Equality((Object) this.mTrans, (Object) null))
    {
      this.mTrans = ((Component) this).transform;
      this.mPos = this.mTrans.position;
      this.mRot = this.mTrans.rotation;
      this.mScale = this.mTrans.localScale;
    }
    if (Object.op_Inequality((Object) this.from, (Object) null))
    {
      this.mTrans.position = Vector3.op_Addition(Vector3.op_Multiply(this.from.position, 1f - factor), Vector3.op_Multiply(this.to.position, factor));
      this.mTrans.localScale = Vector3.op_Addition(Vector3.op_Multiply(this.from.localScale, 1f - factor), Vector3.op_Multiply(this.to.localScale, factor));
      this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
    }
    else
    {
      this.mTrans.position = Vector3.op_Addition(Vector3.op_Multiply(this.mPos, 1f - factor), Vector3.op_Multiply(this.to.position, factor));
      this.mTrans.localScale = Vector3.op_Addition(Vector3.op_Multiply(this.mScale, 1f - factor), Vector3.op_Multiply(this.to.localScale, factor));
      this.mTrans.rotation = Quaternion.Slerp(this.mRot, this.to.rotation, factor);
    }
    if (!(this.parentWhenFinished & isFinished))
      return;
    this.mTrans.parent = this.to;
  }

  public static TweenTransform Begin(GameObject go, float duration, Transform to)
  {
    return TweenTransform.Begin(go, duration, (Transform) null, to);
  }

  public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
  {
    TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(go, duration);
    tweenTransform.from = from;
    tweenTransform.to = to;
    if ((double) duration <= 0.0)
    {
      tweenTransform.Sample(1f, true);
      ((Behaviour) tweenTransform).enabled = false;
    }
    return tweenTransform;
  }
}
