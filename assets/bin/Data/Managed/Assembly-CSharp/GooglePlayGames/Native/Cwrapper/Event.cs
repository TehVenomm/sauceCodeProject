// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.Event
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class Event
{
  [DllImport("gpg")]
  internal static extern ulong Event_Count(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr Event_Description(
    HandleRef self,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern UIntPtr Event_ImageUrl(HandleRef self, [In, Out] byte[] out_arg, UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern Types.EventVisibility Event_Visibility(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr Event_Id(HandleRef self, [In, Out] byte[] out_arg, UIntPtr out_size);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool Event_Valid(HandleRef self);

  [DllImport("gpg")]
  internal static extern void Event_Dispose(HandleRef self);

  [DllImport("gpg")]
  internal static extern IntPtr Event_Copy(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr Event_Name(HandleRef self, [In, Out] byte[] out_arg, UIntPtr out_size);
}
