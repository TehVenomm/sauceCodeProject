// Decompiled with JetBrains decompiler
// Type: Network.AutoModeStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class AutoModeStatus
{
  private double remainTime;

  public void Init(double remainTime_) => this.remainTime = remainTime_;

  public void SubTime(double subTime_) => this.remainTime -= subTime_;

  public bool IsRemain() => this.remainTime > 0.0;

  public string GetRemainTime()
  {
    return this.remainTime < 0.0 ? "00:00:00" : $"{(int) (this.remainTime / 3600.0):D2}:{(int) (this.remainTime / 60.0) % 60:D2}:{(int) this.remainTime % 60:D2}";
  }
}
