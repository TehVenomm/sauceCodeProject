// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Multiplayer.Participant
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace GooglePlayGames.BasicApi.Multiplayer;

public class Participant : IComparable<Participant>
{
  private string mDisplayName = string.Empty;
  private string mParticipantId = string.Empty;
  private Participant.ParticipantStatus mStatus = Participant.ParticipantStatus.Unknown;
  private Player mPlayer;
  private bool mIsConnectedToRoom;

  public string DisplayName => this.mDisplayName;

  public string ParticipantId => this.mParticipantId;

  public Participant.ParticipantStatus Status => this.mStatus;

  public Player Player => this.mPlayer;

  public bool IsConnectedToRoom => this.mIsConnectedToRoom;

  public bool IsAutomatch => this.mPlayer == null;

  internal Participant(
    string displayName,
    string participantId,
    Participant.ParticipantStatus status,
    Player player,
    bool connectedToRoom)
  {
    this.mDisplayName = displayName;
    this.mParticipantId = participantId;
    this.mStatus = status;
    this.mPlayer = player;
    this.mIsConnectedToRoom = connectedToRoom;
  }

  public override string ToString()
  {
    return $"[Participant: '{this.mDisplayName}' (id {this.mParticipantId}), status={this.mStatus.ToString()}, player={(this.mPlayer == null ? (object) "NULL" : (object) this.mPlayer.ToString())}, connected={this.mIsConnectedToRoom}]";
  }

  public int CompareTo(Participant other) => this.mParticipantId.CompareTo(other.mParticipantId);

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return !(obj.GetType() != typeof (Participant)) && this.mParticipantId.Equals(((Participant) obj).mParticipantId);
  }

  public override int GetHashCode()
  {
    return this.mParticipantId == null ? 0 : this.mParticipantId.GetHashCode();
  }

  public enum ParticipantStatus
  {
    NotInvitedYet,
    Invited,
    Joined,
    Declined,
    Left,
    Finished,
    Unresponsive,
    Unknown,
  }
}
