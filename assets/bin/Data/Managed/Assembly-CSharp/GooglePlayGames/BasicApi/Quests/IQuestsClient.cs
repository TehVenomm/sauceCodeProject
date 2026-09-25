// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Quests.IQuestsClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace GooglePlayGames.BasicApi.Quests;

[Obsolete("Quests are being removed in 2018.")]
public interface IQuestsClient
{
  void Fetch(DataSource source, string questId, Action<ResponseStatus, IQuest> callback);

  void FetchMatchingState(
    DataSource source,
    QuestFetchFlags flags,
    Action<ResponseStatus, List<IQuest>> callback);

  void ShowAllQuestsUI(
    Action<QuestUiResult, IQuest, IQuestMilestone> callback);

  void ShowSpecificQuestUI(
    IQuest quest,
    Action<QuestUiResult, IQuest, IQuestMilestone> callback);

  void Accept(IQuest quest, Action<QuestAcceptStatus, IQuest> callback);

  void ClaimMilestone(
    IQuestMilestone milestone,
    Action<QuestClaimMilestoneStatus, IQuest, IQuestMilestone> callback);
}
