// Decompiled with JetBrains decompiler
// Type: ResourceManagerProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
internal class ResourceManagerProgress : IProgress
{
  private bool hasProgress;

  public float GetProgress()
  {
    if (this.IsCompleted())
      return 1f;
    List<ResourceManager.LoadRequest> loadRequests = MonoBehaviourSingleton<ResourceManager>.I.loadRequests;
    int index = 0;
    for (int count = loadRequests.Count; index < count; ++index)
    {
      if (loadRequests[index].IsValid() && loadRequests[index].progressObject != null)
      {
        this.hasProgress = true;
        return loadRequests[index].GetProgress();
      }
    }
    this.hasProgress = false;
    return 1f;
  }

  public bool IsCompleted() => !MonoBehaviourSingleton<ResourceManager>.I.isLoading;

  public bool IsVisible() => this.hasProgress;
}
