// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.RealTimeRoomConfigBuilder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class RealTimeRoomConfigBuilder
{
  [DllImport("gpg")]
  internal static extern void RealTimeRoomConfig_Builder_PopulateFromPlayerSelectUIResponse(
    HandleRef self,
    IntPtr response);

  [DllImport("gpg")]
  internal static extern void RealTimeRoomConfig_Builder_SetVariant(HandleRef self, uint variant);

  [DllImport("gpg")]
  internal static extern void RealTimeRoomConfig_Builder_AddPlayerToInvite(
    HandleRef self,
    string player_id);

  [DllImport("gpg")]
  internal static extern IntPtr RealTimeRoomConfig_Builder_Construct();

  [DllImport("gpg")]
  internal static extern void RealTimeRoomConfig_Builder_SetExclusiveBitMask(
    HandleRef self,
    ulong exclusive_bit_mask);

  [DllImport("gpg")]
  internal static extern void RealTimeRoomConfig_Builder_SetMaximumAutomatchingPlayers(
    HandleRef self,
    uint maximum_automatching_players);

  [DllImport("gpg")]
  internal static extern IntPtr RealTimeRoomConfig_Builder_Create(HandleRef self);

  [DllImport("gpg")]
  internal static extern void RealTimeRoomConfig_Builder_SetMinimumAutomatchingPlayers(
    HandleRef self,
    uint minimum_automatching_players);

  [DllImport("gpg")]
  internal static extern void RealTimeRoomConfig_Builder_Dispose(HandleRef self);
}
