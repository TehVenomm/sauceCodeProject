// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.SnapshotMetadataChangeBuilder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.Cwrapper;

internal static class SnapshotMetadataChangeBuilder
{
  [DllImport("gpg")]
  internal static extern void SnapshotMetadataChange_Builder_SetDescription(
    HandleRef self,
    string description);

  [DllImport("gpg")]
  internal static extern IntPtr SnapshotMetadataChange_Builder_Construct();

  [DllImport("gpg")]
  internal static extern void SnapshotMetadataChange_Builder_SetPlayedTime(
    HandleRef self,
    ulong played_time);

  [DllImport("gpg")]
  internal static extern void SnapshotMetadataChange_Builder_SetCoverImageFromPngData(
    HandleRef self,
    byte[] png_data,
    UIntPtr png_data_size);

  [DllImport("gpg")]
  internal static extern IntPtr SnapshotMetadataChange_Builder_Create(HandleRef self);

  [DllImport("gpg")]
  internal static extern void SnapshotMetadataChange_Builder_Dispose(HandleRef self);
}
