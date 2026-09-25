// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.PlayGamesClientFactory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Android;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native;
using GooglePlayGames.OurUtils;
using UnityEngine;

#nullable disable
namespace GooglePlayGames;

internal class PlayGamesClientFactory
{
  internal static IPlayGamesClient GetPlatformPlayGamesClient(PlayGamesClientConfiguration config)
  {
    if (Application.isEditor)
    {
      Logger.d("Creating IPlayGamesClient in editor, using DummyClient.");
      return (IPlayGamesClient) new DummyClient();
    }
    Logger.d("Creating Android IPlayGamesClient Client");
    return (IPlayGamesClient) new NativeClient(config, (IClientImpl) new AndroidClient());
  }
}
