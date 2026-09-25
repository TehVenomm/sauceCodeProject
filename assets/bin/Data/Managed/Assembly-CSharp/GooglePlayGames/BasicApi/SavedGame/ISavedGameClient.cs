// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.ISavedGameClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace GooglePlayGames.BasicApi.SavedGame;

public interface ISavedGameClient
{
  void OpenWithAutomaticConflictResolution(
    string filename,
    DataSource source,
    ConflictResolutionStrategy resolutionStrategy,
    Action<SavedGameRequestStatus, ISavedGameMetadata> callback);

  void OpenWithManualConflictResolution(
    string filename,
    DataSource source,
    bool prefetchDataOnConflict,
    ConflictCallback conflictCallback,
    Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback);

  void ReadBinaryData(
    ISavedGameMetadata metadata,
    Action<SavedGameRequestStatus, byte[]> completedCallback);

  void ShowSelectSavedGameUI(
    string uiTitle,
    uint maxDisplayedSavedGames,
    bool showCreateSaveUI,
    bool showDeleteSaveUI,
    Action<SelectUIStatus, ISavedGameMetadata> callback);

  void CommitUpdate(
    ISavedGameMetadata metadata,
    SavedGameMetadataUpdate updateForMetadata,
    byte[] updatedBinaryData,
    Action<SavedGameRequestStatus, ISavedGameMetadata> callback);

  void FetchAllSavedGames(
    DataSource source,
    Action<SavedGameRequestStatus, List<ISavedGameMetadata>> callback);

  void Delete(ISavedGameMetadata metadata);
}
