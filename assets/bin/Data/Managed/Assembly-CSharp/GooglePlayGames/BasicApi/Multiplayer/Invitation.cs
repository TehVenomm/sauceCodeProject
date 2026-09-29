// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Multiplayer.Invitation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace GooglePlayGames.BasicApi.Multiplayer;

public class Invitation
{
  private Invitation.InvType mInvitationType;
  private string mInvitationId;
  private Participant mInviter;
  private int mVariant;

  internal Invitation(Invitation.InvType invType, string invId, Participant inviter, int variant)
  {
    this.mInvitationType = invType;
    this.mInvitationId = invId;
    this.mInviter = inviter;
    this.mVariant = variant;
  }

  public Invitation.InvType InvitationType => this.mInvitationType;

  public string InvitationId => this.mInvitationId;

  public Participant Inviter => this.mInviter;

  public int Variant => this.mVariant;

  public override string ToString()
  {
    return $"[Invitation: InvitationType={this.InvitationType}, InvitationId={this.InvitationId}, Inviter={this.Inviter}, Variant={this.Variant}]";
  }

  public enum InvType
  {
    RealTime,
    TurnBased,
    Unknown,
  }
}
