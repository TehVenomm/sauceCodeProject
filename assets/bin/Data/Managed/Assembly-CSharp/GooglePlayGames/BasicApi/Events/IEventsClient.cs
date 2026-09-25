// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Events.IEventsClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace GooglePlayGames.BasicApi.Events;

public interface IEventsClient
{
  void FetchAllEvents(DataSource source, Action<ResponseStatus, List<IEvent>> callback);

  void FetchEvent(DataSource source, string eventId, Action<ResponseStatus, IEvent> callback);

  void IncrementEvent(string eventId, uint stepsToIncrement);
}
