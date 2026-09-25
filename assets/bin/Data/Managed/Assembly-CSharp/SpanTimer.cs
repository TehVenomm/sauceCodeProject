// Decompiled with JetBrains decompiler
// Type: SpanTimer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SpanTimer
{
  private float span;
  private float nextTime;
  private bool pause;

  public SpanTimer(float span) => this.span = span;

  public bool IsReady()
  {
    if ((double) this.span < 0.0 || this.pause)
      return false;
    if ((double) this.span == 0.0)
      return true;
    if ((double) this.nextTime > (double) Time.time)
      return false;
    this.ResetNextTime();
    return true;
  }

  public void ResetNextTime() => this.nextTime = Time.time + this.span;

  public void SetTempSpan(float temp_span) => this.nextTime = Time.time + temp_span;

  public void PauseOn() => this.pause = true;

  public void PauseOff() => this.pause = false;
}
