// Decompiled with JetBrains decompiler
// Type: PredownloadProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
internal class PredownloadProgress : IProgress
{
  public float GetProgress()
  {
    if (this.IsCompleted())
      return 1f;
    int total;
    int loaded;
    MonoBehaviourSingleton<PredownloadManager>.I.GetCount(out total, out loaded);
    return (float) loaded / (float) total;
  }

  public bool IsCompleted()
  {
    return !MonoBehaviourSingleton<PredownloadManager>.IsValid() || MonoBehaviourSingleton<PredownloadManager>.I.loadedCount >= MonoBehaviourSingleton<PredownloadManager>.I.tutorialCount;
  }

  public bool IsVisible() => !this.IsCompleted();
}
