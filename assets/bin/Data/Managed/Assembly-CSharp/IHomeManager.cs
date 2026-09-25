// Decompiled with JetBrains decompiler
// Type: IHomeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public interface IHomeManager
{
  bool IsJumpToGacha { get; set; }

  bool IsInitialized { get; }

  HomeCamera HomeCamera { get; }

  IHomePeople IHomePeople { get; }

  HomeFeatureBanner HomeFeatureBanner { get; }

  bool IsPointShopOpen { get; }

  int PointShopBannerId { get; }

  void SetPointShop(bool isOpen, int bannerId);

  OutGameSettingsManager.HomeScene GetSceneSetting();
}
