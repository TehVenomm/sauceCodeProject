// Decompiled with JetBrains decompiler
// Type: ChatSendLimitter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
internal class ChatSendLimitter
{
  private int maxCount;
  private float limitSec;
  private int firstPos;
  private int lastPos;
  private float[] entries;

  public ChatSendLimitter(int maxCount, float limitSec)
  {
    this.maxCount = maxCount + 1;
    this.limitSec = limitSec;
    this.entries = new float[this.maxCount];
  }

  public void Touch()
  {
    this.entries[this.lastPos] = Time.unscaledTime + this.limitSec;
    this.lastPos = this.Next(this.lastPos);
  }

  public void Update()
  {
    if (this.firstPos == this.lastPos || (double) this.entries[this.firstPos] >= (double) Time.unscaledTime)
      return;
    this.firstPos = this.Next(this.firstPos);
  }

  private int Next(int current)
  {
    ++current;
    return current < this.maxCount ? current : 0;
  }

  public bool IsLimit()
  {
    this.Update();
    return this.firstPos <= this.lastPos ? this.lastPos - this.firstPos == this.maxCount - 1 : this.lastPos - this.firstPos == -1;
  }
}
