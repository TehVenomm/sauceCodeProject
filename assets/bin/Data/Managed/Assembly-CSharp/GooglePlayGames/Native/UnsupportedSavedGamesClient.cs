// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.UnsupportedSavedGamesClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;

#nullable disable
namespace GooglePlayGames.Native;

internal class UnsupportedSavedGamesClient : ISavedGameClient
{
  private readonly string mMessage;

  public UnsupportedSavedGamesClient(string message)
  {
    this.mMessage = Misc.CheckNotNull<string>(message);
  }

  public void OpenWithAutomaticConflictResolution(
    string filename,
    DataSource source,
    ConflictResolutionStrategy resolutionStrategy,
    Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
  {
    throw new NotImplementedException(this.mMessage);
  }

  public void OpenWithManualConflictResolution(
    string filename,
    DataSource source,
    bool prefetchDataOnConflict,
    ConflictCallback conflictCallback,
    Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback)
  {
    throw new NotImplementedException(this.mMessage);
  }

  public void ReadBinaryData(
    ISavedGameMetadata metadata,
    Action<SavedGameRequestStatus, byte[]> completedCallback)
  {
    throw new NotImplementedException(this.mMessage);
  }

  public void ShowSelectSavedGameUI(
    string uiTitle,
    uint maxDisplayedSavedGames,
    bool showCreateSaveUI,
    bool showDeleteSaveUI,
    Action<SelectUIStatus, ISavedGameMetadata> callback)
  {
    throw new NotImplementedException(this.mMessage);
  }

  public void CommitUpdate(
    ISavedGameMetadata metadata,
    SavedGameMetadataUpdate updateForMetadata,
    byte[] updatedBinaryData,
    Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
  {
    throw new NotImplementedException(this.mMessage);
  }

  public void FetchAllSavedGames(
    DataSource source,
    Action<SavedGameRequestStatus, List<ISavedGameMetadata>> callback)
  {
    throw new NotImplementedException(this.mMessage);
  }

  public void Delete(ISavedGameMetadata metadata)
  {
    throw new NotImplementedException(this.mMessage);
  }
}
