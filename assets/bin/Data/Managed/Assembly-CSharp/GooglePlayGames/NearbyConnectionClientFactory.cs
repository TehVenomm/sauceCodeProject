// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.NearbyConnectionClientFactory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using UnityEngine;

#nullable disable
namespace GooglePlayGames;

public static class NearbyConnectionClientFactory
{
  public static void Create(Action<INearbyConnectionClient> callback)
  {
    if (Application.isEditor)
    {
      Logger.d("Creating INearbyConnection in editor, using DummyClient.");
      callback((INearbyConnectionClient) new DummyNearbyConnectionClient());
    }
    Logger.d("Creating real INearbyConnectionClient");
    NativeNearbyConnectionClientFactory.Create(callback);
  }

  private static GooglePlayGames.BasicApi.Nearby.InitializationStatus ToStatus(
    NearbyConnectionsStatus.InitializationStatus status)
  {
    switch (status)
    {
      case NearbyConnectionsStatus.InitializationStatus.ERROR_VERSION_UPDATE_REQUIRED:
        return GooglePlayGames.BasicApi.Nearby.InitializationStatus.VersionUpdateRequired;
      case NearbyConnectionsStatus.InitializationStatus.ERROR_INTERNAL:
        return GooglePlayGames.BasicApi.Nearby.InitializationStatus.InternalError;
      case NearbyConnectionsStatus.InitializationStatus.VALID:
        return GooglePlayGames.BasicApi.Nearby.InitializationStatus.Success;
      default:
        Logger.w("Unknown initialization status: " + (object) status);
        return GooglePlayGames.BasicApi.Nearby.InitializationStatus.InternalError;
    }
  }
}
