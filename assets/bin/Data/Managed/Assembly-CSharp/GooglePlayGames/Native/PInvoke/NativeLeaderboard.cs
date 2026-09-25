// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeLeaderboard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeLeaderboard : BaseReferenceHolder
{
  internal NativeLeaderboard(IntPtr selfPtr)
    : base(selfPtr)
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    Leaderboard.Leaderboard_Dispose(selfPointer);
  }

  internal string Title()
  {
    return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, out_size) => Leaderboard.Leaderboard_Name(this.SelfPtr(), out_string, out_size)));
  }

  internal static NativeLeaderboard FromPointer(IntPtr pointer)
  {
    return pointer.Equals((object) IntPtr.Zero) ? (NativeLeaderboard) null : new NativeLeaderboard(pointer);
  }
}
