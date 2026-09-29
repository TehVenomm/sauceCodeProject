// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.TurnBasedMatch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class TurnBasedMatch
{
  [DllImport("gpg")]
  internal static extern uint TurnBasedMatch_AutomatchingSlotsAvailable(HandleRef self);

  [DllImport("gpg")]
  internal static extern ulong TurnBasedMatch_CreationTime(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr TurnBasedMatch_Participants_Length(HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr TurnBasedMatch_Participants_GetElement(
    HandleRef self,
    UIntPtr index);

  [DllImport("gpg")]
  internal static extern uint TurnBasedMatch_Version(HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr TurnBasedMatch_ParticipantResults(HandleRef self);

  [DllImport("gpg")]
  internal static extern Types.MatchStatus TurnBasedMatch_Status(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr TurnBasedMatch_Description(
    HandleRef self,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern IntPtr TurnBasedMatch_PendingParticipant(HandleRef self);

  [DllImport("gpg")]
  internal static extern uint TurnBasedMatch_Variant(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool TurnBasedMatch_HasPreviousMatchData(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr TurnBasedMatch_Data(
    HandleRef self,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern IntPtr TurnBasedMatch_LastUpdatingParticipant(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool TurnBasedMatch_HasData(HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr TurnBasedMatch_SuggestedNextParticipant(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr TurnBasedMatch_PreviousMatchData(
    HandleRef self,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern ulong TurnBasedMatch_LastUpdateTime(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr TurnBasedMatch_RematchId(
    HandleRef self,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern uint TurnBasedMatch_Number(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool TurnBasedMatch_HasRematchId(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool TurnBasedMatch_Valid(HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr TurnBasedMatch_CreatingParticipant(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr TurnBasedMatch_Id(
    HandleRef self,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern void TurnBasedMatch_Dispose(HandleRef self);
}
