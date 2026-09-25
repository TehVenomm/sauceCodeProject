// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.MultiplayerInvitation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class MultiplayerInvitation
{
  [DllImport("gpg")]
  internal static extern uint MultiplayerInvitation_AutomatchingSlotsAvailable(HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr MultiplayerInvitation_InvitingParticipant(HandleRef self);

  [DllImport("gpg")]
  internal static extern uint MultiplayerInvitation_Variant(HandleRef self);

  [DllImport("gpg")]
  internal static extern ulong MultiplayerInvitation_CreationTime(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr MultiplayerInvitation_Participants_Length(HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr MultiplayerInvitation_Participants_GetElement(
    HandleRef self,
    UIntPtr index);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool MultiplayerInvitation_Valid(HandleRef self);

  [DllImport("gpg")]
  internal static extern Types.MultiplayerInvitationType MultiplayerInvitation_Type(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr MultiplayerInvitation_Id(
    HandleRef self,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern void MultiplayerInvitation_Dispose(HandleRef self);
}
