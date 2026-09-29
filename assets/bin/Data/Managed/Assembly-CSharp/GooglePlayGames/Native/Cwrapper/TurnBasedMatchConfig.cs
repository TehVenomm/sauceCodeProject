// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.TurnBasedMatchConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class TurnBasedMatchConfig
{
  [DllImport("gpg")]
  internal static extern UIntPtr TurnBasedMatchConfig_PlayerIdsToInvite_Length(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr TurnBasedMatchConfig_PlayerIdsToInvite_GetElement(
    HandleRef self,
    UIntPtr index,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern uint TurnBasedMatchConfig_Variant(HandleRef self);

  [DllImport("gpg")]
  internal static extern long TurnBasedMatchConfig_ExclusiveBitMask(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool TurnBasedMatchConfig_Valid(HandleRef self);

  [DllImport("gpg")]
  internal static extern uint TurnBasedMatchConfig_MaximumAutomatchingPlayers(HandleRef self);

  [DllImport("gpg")]
  internal static extern uint TurnBasedMatchConfig_MinimumAutomatchingPlayers(HandleRef self);

  [DllImport("gpg")]
  internal static extern void TurnBasedMatchConfig_Dispose(HandleRef self);
}
