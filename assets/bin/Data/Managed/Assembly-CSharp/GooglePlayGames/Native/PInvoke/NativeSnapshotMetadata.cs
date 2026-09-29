// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeSnapshotMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeSnapshotMetadata : BaseReferenceHolder, ISavedGameMetadata
{
  internal NativeSnapshotMetadata(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  public bool IsOpen => SnapshotMetadata.SnapshotMetadata_IsOpen(this.SelfPtr());

  public string Filename
  {
    get
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => SnapshotMetadata.SnapshotMetadata_FileName(this.SelfPtr(), out_string, out_size)));
    }
  }

  public string Description
  {
    get
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => SnapshotMetadata.SnapshotMetadata_Description(this.SelfPtr(), out_string, out_size)));
    }
  }

  public string CoverImageURL
  {
    get
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => SnapshotMetadata.SnapshotMetadata_CoverImageURL(this.SelfPtr(), out_string, out_size)));
    }
  }

  public TimeSpan TotalTimePlayed
  {
    get
    {
      long num = SnapshotMetadata.SnapshotMetadata_PlayedTime(this.SelfPtr());
      return num < 0L ? TimeSpan.FromMilliseconds(0.0) : TimeSpan.FromMilliseconds((double) num);
    }
  }

  public DateTime LastModifiedTimestamp
  {
    get
    {
      return PInvokeUtilities.FromMillisSinceUnixEpoch(SnapshotMetadata.SnapshotMetadata_LastModifiedTime(this.SelfPtr()));
    }
  }

  public override string ToString()
  {
    if (this.IsDisposed())
      return "[NativeSnapshotMetadata: DELETED]";
    return $"[NativeSnapshotMetadata: IsOpen={this.IsOpen}, Filename={this.Filename}, Description={this.Description}, CoverImageUrl={this.CoverImageURL}, TotalTimePlayed={this.TotalTimePlayed}, LastModifiedTimestamp={this.LastModifiedTimestamp}]";
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    SnapshotMetadata.SnapshotMetadata_Dispose(this.SelfPtr());
  }
}
