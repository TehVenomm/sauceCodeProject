// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Quests.IQuestMilestone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace GooglePlayGames.BasicApi.Quests;

[Obsolete("Quests are being removed in 2018.")]
public interface IQuestMilestone
{
  string Id { get; }

  string EventId { get; }

  string QuestId { get; }

  ulong CurrentCount { get; }

  ulong TargetCount { get; }

  byte[] CompletionRewardData { get; }

  MilestoneState State { get; }
}
