// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.RealTimeRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class RealTimeRoom
{
  [DllImport("gpg")]
  internal static extern Types.RealTimeRoomStatus RealTimeRoom_Status(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr RealTimeRoom_Description(
    HandleRef self,
    [In, Out] char[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern uint RealTimeRoom_Variant(HandleRef self);

  [DllImport("gpg")]
  internal static extern ulong RealTimeRoom_CreationTime(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr RealTimeRoom_Participants_Length(HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr RealTimeRoom_Participants_GetElement(HandleRef self, UIntPtr index);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool RealTimeRoom_Valid(HandleRef self);

  [DllImport("gpg")]
  internal static extern uint RealTimeRoom_RemainingAutomatchingSlots(HandleRef self);

  [DllImport("gpg")]
  internal static extern ulong RealTimeRoom_AutomatchWaitEstimate(HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr RealTimeRoom_CreatingParticipant(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr RealTimeRoom_Id(HandleRef self, [In, Out] byte[] out_arg, UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern void RealTimeRoom_Dispose(HandleRef self);
}
