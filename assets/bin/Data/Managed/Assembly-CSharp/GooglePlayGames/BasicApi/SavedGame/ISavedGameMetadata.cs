// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.ISavedGameMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace GooglePlayGames.BasicApi.SavedGame;

public interface ISavedGameMetadata
{
  bool IsOpen { get; }

  string Filename { get; }

  string Description { get; }

  string CoverImageURL { get; }

  TimeSpan TotalTimePlayed { get; }

  DateTime LastModifiedTimestamp { get; }
}
