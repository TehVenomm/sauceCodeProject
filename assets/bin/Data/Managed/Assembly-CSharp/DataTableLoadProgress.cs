// Decompiled with JetBrains decompiler
// Type: DataTableLoadProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
internal class DataTableLoadProgress : IProgress
{
  private List<DataLoadRequest> loadings;
  private int total;
  private int endCount;

  public DataTableLoadProgress(List<DataLoadRequest> loadings)
  {
    this.loadings = loadings;
    this.total = loadings.Count;
  }

  public float GetProgress()
  {
    float num = 0.0f;
    this.endCount += this.loadings.RemoveAll((Predicate<DataLoadRequest>) (x => x.isCompleted));
    int count = this.loadings.Count;
    for (int index = 0; index < count; ++index)
      num += this.loadings[index].progress;
    return (num + (float) this.endCount) / (float) this.total;
  }

  public bool IsCompleted() => this.endCount == this.total;

  public bool IsVisible() => !this.IsCompleted();
}
