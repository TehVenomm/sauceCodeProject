// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Multiplayer.ITurnBasedMultiplayerClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace GooglePlayGames.BasicApi.Multiplayer;

public interface ITurnBasedMultiplayerClient
{
  void CreateQuickMatch(
    uint minOpponents,
    uint maxOpponents,
    uint variant,
    Action<bool, TurnBasedMatch> callback);

  void CreateQuickMatch(
    uint minOpponents,
    uint maxOpponents,
    uint variant,
    ulong exclusiveBitmask,
    Action<bool, TurnBasedMatch> callback);

  void CreateWithInvitationScreen(
    uint minOpponents,
    uint maxOpponents,
    uint variant,
    Action<bool, TurnBasedMatch> callback);

  void CreateWithInvitationScreen(
    uint minOpponents,
    uint maxOpponents,
    uint variant,
    Action<UIStatus, TurnBasedMatch> callback);

  void GetAllInvitations(Action<Invitation[]> callback);

  void GetAllMatches(Action<TurnBasedMatch[]> callback);

  void AcceptFromInbox(Action<bool, TurnBasedMatch> callback);

  void AcceptInvitation(string invitationId, Action<bool, TurnBasedMatch> callback);

  void RegisterMatchDelegate(MatchDelegate del);

  void TakeTurn(
    TurnBasedMatch match,
    byte[] data,
    string pendingParticipantId,
    Action<bool> callback);

  int GetMaxMatchDataSize();

  void Finish(TurnBasedMatch match, byte[] data, MatchOutcome outcome, Action<bool> callback);

  void AcknowledgeFinished(TurnBasedMatch match, Action<bool> callback);

  void Leave(TurnBasedMatch match, Action<bool> callback);

  void LeaveDuringTurn(TurnBasedMatch match, string pendingParticipantId, Action<bool> callback);

  void Cancel(TurnBasedMatch match, Action<bool> callback);

  void Rematch(TurnBasedMatch match, Action<bool, TurnBasedMatch> callback);

  void DeclineInvitation(string invitationId);
}
