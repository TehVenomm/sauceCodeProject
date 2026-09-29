// Decompiled with JetBrains decompiler
// Type: IBulletObservable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public interface IBulletObservable
{
  int GetObservedID();

  void SetObservedID(int id);

  void RegisterObserver();

  void NotifyBroken(bool isSendOnlyOriginal = true);

  void NotifyDestroy();

  void ForceBreak();
}
