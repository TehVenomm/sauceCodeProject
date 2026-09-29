// Decompiled with JetBrains decompiler
// Type: SmithExceedPerformance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SmithExceedPerformance : SmithPerformanceBase
{
  public override void Initialize() => base.Initialize();

  protected override void OnOpen()
  {
    this.director.Reset();
    this.director.StartExceed(new System.Action(((SmithPerformanceBase) this).OnEndDirection));
    base.OnOpen();
  }
}
