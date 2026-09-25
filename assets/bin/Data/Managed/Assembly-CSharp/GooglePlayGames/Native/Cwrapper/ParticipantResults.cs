// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.ParticipantResults
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class ParticipantResults
{
  [DllImport("gpg")]
  internal static extern IntPtr ParticipantResults_WithResult(
    HandleRef self,
    string participant_id,
    uint placing,
    Types.MatchResult result);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool ParticipantResults_Valid(HandleRef self);

  [DllImport("gpg")]
  internal static extern Types.MatchResult ParticipantResults_MatchResultForParticipant(
    HandleRef self,
    string participant_id);

  [DllImport("gpg")]
  internal static extern uint ParticipantResults_PlaceForParticipant(
    HandleRef self,
    string participant_id);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool ParticipantResults_HasResultsForParticipant(
    HandleRef self,
    string participant_id);

  [DllImport("gpg")]
  internal static extern void ParticipantResults_Dispose(HandleRef self);
}
