// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.SnapshotMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class SnapshotMetadata
{
  [DllImport("gpg")]
  internal static extern void SnapshotMetadata_Dispose(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr SnapshotMetadata_CoverImageURL(
    HandleRef self,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  internal static extern UIntPtr SnapshotMetadata_Description(
    HandleRef self,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool SnapshotMetadata_IsOpen(HandleRef self);

  [DllImport("gpg")]
  internal static extern UIntPtr SnapshotMetadata_FileName(
    HandleRef self,
    [In, Out] byte[] out_arg,
    UIntPtr out_size);

  [DllImport("gpg")]
  [return: MarshalAs(UnmanagedType.I1)]
  internal static extern bool SnapshotMetadata_Valid(HandleRef self);

  [DllImport("gpg")]
  internal static extern long SnapshotMetadata_PlayedTime(HandleRef self);

  [DllImport("gpg")]
  internal static extern long SnapshotMetadata_LastModifiedTime(HandleRef self);
}
