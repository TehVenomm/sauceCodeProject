// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Multiplayer.TurnBasedMatch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace GooglePlayGames.BasicApi.Multiplayer;

public class TurnBasedMatch
{
  private string mMatchId;
  private byte[] mData;
  private bool mCanRematch;
  private uint mAvailableAutomatchSlots;
  private string mSelfParticipantId;
  private List<Participant> mParticipants;
  private string mPendingParticipantId;
  private TurnBasedMatch.MatchTurnStatus mTurnStatus;
  private TurnBasedMatch.MatchStatus mMatchStatus;
  private uint mVariant;
  private uint mVersion;

  internal TurnBasedMatch(
    string matchId,
    byte[] data,
    bool canRematch,
    string selfParticipantId,
    List<Participant> participants,
    uint availableAutomatchSlots,
    string pendingParticipantId,
    TurnBasedMatch.MatchTurnStatus turnStatus,
    TurnBasedMatch.MatchStatus matchStatus,
    uint variant,
    uint version)
  {
    this.mMatchId = matchId;
    this.mData = data;
    this.mCanRematch = canRematch;
    this.mSelfParticipantId = selfParticipantId;
    this.mParticipants = participants;
    this.mParticipants.Sort();
    this.mAvailableAutomatchSlots = availableAutomatchSlots;
    this.mPendingParticipantId = pendingParticipantId;
    this.mTurnStatus = turnStatus;
    this.mMatchStatus = matchStatus;
    this.mVariant = variant;
    this.mVersion = version;
  }

  public string MatchId => this.mMatchId;

  public byte[] Data => this.mData;

  public bool CanRematch => this.mCanRematch;

  public string SelfParticipantId => this.mSelfParticipantId;

  public Participant Self => this.GetParticipant(this.mSelfParticipantId);

  public Participant GetParticipant(string participantId)
  {
    foreach (Participant mParticipant in this.mParticipants)
    {
      if (mParticipant.ParticipantId.Equals(participantId))
        return mParticipant;
    }
    Logger.w("Participant not found in turn-based match: " + participantId);
    return (Participant) null;
  }

  public List<Participant> Participants => this.mParticipants;

  public string PendingParticipantId => this.mPendingParticipantId;

  public Participant PendingParticipant
  {
    get
    {
      return this.mPendingParticipantId != null ? this.GetParticipant(this.mPendingParticipantId) : (Participant) null;
    }
  }

  public TurnBasedMatch.MatchTurnStatus TurnStatus => this.mTurnStatus;

  public TurnBasedMatch.MatchStatus Status => this.mMatchStatus;

  public uint Variant => this.mVariant;

  public uint Version => this.mVersion;

  public uint AvailableAutomatchSlots => this.mAvailableAutomatchSlots;

  public override string ToString()
  {
    return $"[TurnBasedMatch: mMatchId={this.mMatchId}, mData={this.mData}, mCanRematch={this.mCanRematch}, mSelfParticipantId={this.mSelfParticipantId}, mParticipants={string.Join(",", this.mParticipants.Select<Participant, string>((Func<Participant, string>) (p => p.ToString())).ToArray<string>())}, mPendingParticipantId={this.mPendingParticipantId}, mTurnStatus={this.mTurnStatus}, mMatchStatus={this.mMatchStatus}, mVariant={this.mVariant}, mVersion={this.mVersion}]";
  }

  public enum MatchStatus
  {
    Active,
    AutoMatching,
    Cancelled,
    Complete,
    Expired,
    Unknown,
    Deleted,
  }

  public enum MatchTurnStatus
  {
    Complete,
    Invited,
    MyTurn,
    TheirTurn,
    Unknown,
  }
}
