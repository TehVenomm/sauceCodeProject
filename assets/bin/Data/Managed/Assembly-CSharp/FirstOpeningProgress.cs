// Decompiled with JetBrains decompiler
// Type: FirstOpeningProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
internal class FirstOpeningProgress : IProgress
{
  private PredownloadProgress predownload;
  private DataTableLoadProgress datatable;

  public float GetProgress()
  {
    return this.IsCompleted() ? 1f : (float) ((double) this.datatable.GetProgress() * 0.20000000298023224 + (double) this.predownload.GetProgress() * 0.800000011920929);
  }

  public bool IsCompleted() => this.datatable.IsCompleted() && this.predownload.IsCompleted();

  public FirstOpeningProgress(List<DataLoadRequest> loadings)
  {
    this.predownload = new PredownloadProgress();
    this.datatable = new DataTableLoadProgress(loadings);
  }

  public bool IsVisible() => !this.IsCompleted();
}
