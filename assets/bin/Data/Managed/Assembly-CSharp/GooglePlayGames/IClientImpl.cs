// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.IClientImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.Native.PInvoke;
using System;

#nullable disable
namespace GooglePlayGames;

internal interface IClientImpl
{
  PlatformConfiguration CreatePlatformConfiguration(PlayGamesClientConfiguration clientConfig);

  TokenClient CreateTokenClient(bool reset);

  void GetPlayerStats(IntPtr apiClientPtr, Action<CommonStatusCodes, PlayerStats> callback);
}
