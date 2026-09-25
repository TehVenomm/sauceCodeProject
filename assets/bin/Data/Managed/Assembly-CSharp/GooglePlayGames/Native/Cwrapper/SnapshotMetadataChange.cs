// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.SnapshotMetadataChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class SnapshotMetadataChange
{
  [DllImport("gpg")]
  internal static extern UIntPtr SnapshotMetadataChange_Description(
    HandleRef self,
    [In, Out] char[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern IntPtr SnapshotMetadataChange_Image(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool SnapshotMetadataChange_PlayedTimeIsChanged(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool SnapshotMetadataChange_Valid(HandleRef self);

  [DllImport("gpg")]
  internal static extern ulong SnapshotMetadataChange_PlayedTime(HandleRef self);

  [DllImport("gpg")]
  internal static extern void SnapshotMetadataChange_Dispose(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool SnapshotMetadataChange_ImageIsChanged(HandleRef self);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool SnapshotMetadataChange_DescriptionIsChanged(HandleRef self);
}
