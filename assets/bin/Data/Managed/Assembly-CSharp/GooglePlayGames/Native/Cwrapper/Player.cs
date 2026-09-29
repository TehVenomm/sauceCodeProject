// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.Player
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class Player
{
  [DllImport("gpg")]
  internal static extern IntPtr Player_CurrentLevel(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr Player_Name(HandleRef self, [In, Out] byte[] out_arg, UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern void Player_Dispose(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr Player_AvatarUrl(
    HandleRef self,
    Types.ImageResolution resolution,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern ulong Player_LastLevelUpTime(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr Player_Title(HandleRef self, [In, Out] byte[] out_arg, UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern ulong Player_CurrentXP(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool Player_Valid(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool Player_HasLevelInfo(HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr Player_NextLevel(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr Player_Id(HandleRef self, [In, Out] byte[] out_arg, UIntPtr out_size);
}
